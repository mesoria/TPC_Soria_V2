﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMaster;
using Dominio;

namespace Negocio
{
    public class NegocioAlumno
    {
        public List<Alumno> ListarAlumnos()
        {
            Datos datos = new Datos();
            List<Alumno> alumnos = new List<Alumno>();
            Alumno aux;
            try
            {
                datos.SetearConsulta("Select A.ID, p.ID, p.NOMBRE, p.APELLIDO, DOC.NIVEL, p.DNI, p.NACIMIENTO, p.EMAIL, DIR.ID, DIR.CALLE, DIR.NUMERO FROM SORIA_TPC.dbo.ALUMNOS AS A LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON A.IDALUMNO = p.ID left JOIN SORIA_TPC.dbo.DIRECCIONES AS DIR ON DIR.ID = p.IDDIRECCION");
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    aux = new Alumno
                    {
                        IdAlumno = (Int64)datos.Reader[0],
                        ID = (Int64)datos.Reader[1],
                        Name = (string)datos.Reader[2],
                        Apellido = (string)datos.Reader[3],
                        DNI = (string)datos.Reader[5],
                        Nacimiento = (DateTime)datos.Reader[6],
                        Email = (string)datos.Reader[7]
                    };
                    if (!Convert.IsDBNull(datos.Reader[8]))
                    {
                        aux.Direccion = new Direccion
                        {
                            ID = (Int64)datos.Reader[8],
                            Calle = (string)datos.Reader[9],
                            Number = (string)datos.Reader[10]
                        };
                    }
                    alumnos.Add(aux);
                }
                return alumnos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public void Agregar(Alumno alumno)
        {
            Datos datos = new Datos();
            try
            {
                NegocioPersona negocioAux = new NegocioPersona();
                if (this.GetID(alumno.DNI) == 0)
                {
                    negocioAux.Agregar(alumno);
                    alumno.ID = negocioAux.GetIDWithDNI(alumno.DNI);
                    negocioAux.Agregar(alumno.Tutor);
                    alumno.Tutor.ID = negocioAux.GetIDWithDNI(alumno.Tutor.DNI);
                    datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.ALUMNOS (IDPERSONA, IDTUTOR ) values (@IdPersona, @IdTutor)");
                    datos.Comando.Parameters.Clear();
                    datos.Comando.Parameters.AddWithValue("@IdPersona", alumno.ID);
                    datos.Comando.Parameters.AddWithValue("@IdTutor", alumno.Tutor.ID);
                    datos.AbrirConexion();
                    datos.EjecutarAccion();
                }
                else
                {
                    //ya existe un alumno con ese DNI. Deberia hacer un Trigger.
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public Int64 GetID(string DNI)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT ID FROM SORIA_TPC.dbo.PERSONAS WHERE DNI=@DNI");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@DNI", DNI);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    return (Int64)datos.Reader[0];
                }
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }
        public Alumno GetAlumnoWithDNI(string DNI)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT A.ID, p.ID, p.NOMBRE, p.APELLIDO, p.DNI, p.NACIMIENTO, " +
                    "p.EMAIL, DIR.ID, DIR.CALLE, DIR.NUMERO, A.IDTUTOR FROM SORIA_TPC.dbo.ALUMNOS AS A " +
                    "LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON A.IDPERSONA = p.ID " +
                    "LEFT JOIN SORIA_TPC.dbo.DIRECCIONES AS DIR ON DIR.ID = p.IDDIRECCION WHERE p.DNI = @DNI");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@DNI", DNI);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Alumno alumno = new Alumno();
                while (datos.Reader.Read())
                {
                    alumno.IdAlumno = (Int64)datos.Reader[0];
                    alumno.ID = (Int64)datos.Reader[1];
                    alumno.Name = (string)datos.Reader[2];
                    alumno.Apellido = (string)datos.Reader[3];
                    alumno.DNI = (string)datos.Reader[4];
                    alumno.Nacimiento = (DateTime)datos.Reader[5];
                    alumno.Email = (string)datos.Reader[6];
                    if (!Convert.IsDBNull(datos.Reader[7]))
                    {
                        alumno.Direccion = new Direccion()
                        {
                            ID = (Int64)datos.Reader[7],
                            Calle = (string)datos.Reader[8],
                            Number = (string)datos.Reader[9]
                        };
                    }
                    if (!Convert.IsDBNull(datos.Reader[10]))
                    {
                        alumno.Tutor = new Persona()
                        {
                            ID = (Int64)datos.Reader[10]
                        };
                    }
                }
                return alumno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public Alumno GetAlumnoWithId(Int64 ID)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT A.ID, p.ID, p.NOMBRE, p.APELLIDO, p.DNI, p.NACIMIENTO, p.EMAIL, DIR.ID, "+
                    "DIR.CALLE, DIR.NUMERO, T.ID, T.NOMBRE, T.APELLIDO, T.DNI, T.NACIMIENTO, T.EMAIL, DT.ID, "+
                    "DT.CALLE, DT.NUMERO FROM SORIA_TPC.dbo.ALUMNOS AS A " +
                    "LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON A.IDPERSONA=p.ID "+
                    "LEFT JOIN SORIA_TPC.dbo.DIRECCIONES AS DIR ON DIR.ID=p.IDDIRECCION "+
                    "LEFT JOIN SORIA_TPC.dbo.PERSONAS as T ON A.IDTUTOR=T.ID " +
                    "LEFT JOIN SORIA_TPC.dbo.DIRECCIONES AS DT ON DT.ID=T.IDDIRECCION WHERE p.ID=@ID");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", ID);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Alumno alumno = new Alumno();
                while (datos.Reader.Read())
                {
                    alumno.IdAlumno = (Int64)datos.Reader[0];
                    alumno.ID = (Int64)datos.Reader[1];
                    alumno.Name = (string)datos.Reader[2];
                    alumno.Apellido = (string)datos.Reader[3];
                    alumno.DNI = (string)datos.Reader[4];
                    alumno.Nacimiento = (DateTime)datos.Reader[5];
                    alumno.Email = (string)datos.Reader[6];
                    if (!Convert.IsDBNull(datos.Reader[7]))
                    {
                        alumno.Direccion = new Direccion()
                        {
                            ID = (Int64)datos.Reader[7],
                            Calle = (string)datos.Reader[8],
                            Number = (string)datos.Reader[9]
                        };
                    }
                    if (!Convert.IsDBNull(datos.Reader[10]))
                    {
                        alumno.Tutor = new Persona()
                        {
                            ID = (Int64)datos.Reader[10],
                            Name = (string)datos.Reader[11],
                            Apellido = (string)datos.Reader[12],
                            DNI = (string)datos.Reader[13],
                            Nacimiento = (DateTime)datos.Reader[14],
                            Email = (string)datos.Reader[15]
                        };
                        if (!Convert.IsDBNull(datos.Reader[16]))
                        {
                            alumno.Tutor.Direccion = new Direccion()
                            {
                                ID = (Int64)datos.Reader[16],
                                Calle = (string)datos.Reader[17],
                                Number = (string)datos.Reader[18]
                            };
                        }
                    }
                }
                return alumno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Alumno alumno)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("update SORIA_TPC.dbo.ALUMNOS Set IDPERSONA=@ID, IDTUTOR=@IDTUTOR Where ID=" + alumno.IdAlumno);
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", alumno.ID);
                datos.Comando.Parameters.AddWithValue("@IDTUTOR", alumno.Tutor.ID);
                datos.AbrirConexion();
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Eliminar(Int64 ID)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("DELETE FROM SORIA_TPC.dbo.ALUMNOS WHERE ID="+ID);
                datos.AbrirConexion();
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}