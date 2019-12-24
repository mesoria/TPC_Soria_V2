using System;
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
        public List<Alumno> ListarAlumnosFromCurso(Int64 CXE)
        {
            Datos datos = new Datos();
            List<Alumno> alumnos = new List<Alumno>();
            Alumno alumno;
            try
            {
                datos.SetearConsulta("SELECT A.ID, p.ID, p.NOMBRE, p.APELLIDO, p.DNI, p.NACIMIENTO, p.EMAIL, DIR.ID, " +
                    "DIR.CALLE, DIR.NUMERO, T.ID, T.NOMBRE, T.APELLIDO, T.DNI, T.NACIMIENTO, T.EMAIL, DT.ID, " +
                    "DT.CALLE, DT.NUMERO FROM SORIA_TPC.dbo.ALUMNOSxCURSO AS AXC " +
                    "LEFT JOIN SORIA_TPC.dbo.ALUMNOS as A ON A.ID=AXC.IDALUMNO " +
                    "LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON A.IDPERSONA=p.ID " +
                    "LEFT JOIN SORIA_TPC.dbo.DIRECCIONES AS DIR ON DIR.ID=p.IDDIRECCION " +
                    "LEFT JOIN SORIA_TPC.dbo.PERSONAS as T ON A.IDTUTOR=T.ID " +
                    "LEFT JOIN SORIA_TPC.dbo.DIRECCIONES AS DT ON DT.ID=T.IDDIRECCION WHERE AXC.IDCXE=@ID ORDER BY P.APELLIDO");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", CXE);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    alumno = new Alumno
                    {
                        IdAlumno   = (Int64)datos.Reader[0],
                        ID         = (Int64)datos.Reader[1],
                        Name       = (string)datos.Reader[2],
                        Apellido   = (string)datos.Reader[3],
                        DNI        = (string)datos.Reader[4],
                        Nacimiento = (DateTime)datos.Reader[5],
                        Email      = (string)datos.Reader[6]
                    };
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
                    alumnos.Add(alumno);
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
        public List<Alumno> ListarAlumnosByte(long CXE)
        {
            Datos datos = new Datos();
            List<Alumno> alumnos = new List<Alumno>();
            Alumno alumno;
            try
            {
                datos.SetearConsulta("SELECT A.ID, p.ID, p.NOMBRE, p.APELLIDO FROM SORIA_TPC.dbo.ALUMNOSxCURSO AS AXC " +
                    "LEFT JOIN SORIA_TPC.dbo.ALUMNOS as A ON A.ID=AXC.IDALUMNO " +
                    "LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON A.IDPERSONA=p.ID WHERE AXC.IDCXE=@ID ORDER BY P.APELLIDO");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", CXE);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    alumno = new Alumno
                    {
                        IdAlumno = (Int64)datos.Reader[0],
                        ID       = (Int64)datos.Reader[1],
                        Name     = (string)datos.Reader[2],
                        Apellido = (string)datos.Reader[3],
                    };
                    alumnos.Add(alumno);
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

        public void AgregarFull(Alumno alumno, long IDCXE)
        {
            Agregar(alumno);
            alumno.IdAlumno = GetIDWithIDPersona(alumno.ID);
            if (alumno.IdAlumno == 0)
            {
                alumno.IdAlumno = GetAlumnoWithDNI(alumno.DNI).IdAlumno;
            }
            AgregarAlumnoXCurso(alumno.IdAlumno, IDCXE);
        }
        public void Agregar(Alumno alumno)
        {
            Datos datos = new Datos();
            try
            {
                NegocioPersona negocioAux = new NegocioPersona();
                if (GetIDWithIDPersona(alumno.ID) == 0 && GetAlumnoWithDNI(alumno.DNI).ID == 0 )
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

        //public long GetIDWithIDPersona(Alumno alumno)
        //{
        //    Datos datos = new Datos();
        //    long IDA;
        //    try
        //    {
        //        datos.SetearConsulta("SELECT A.ID FROM SORIA_TPC.dbo.ALUMNOS AS A INNER JOIN SORIA_TPC.dbo.PERSONAS AS P ON P.ID=A.IDPERSONA WHERE A.IDPERSONA=@IDP AND A.IDTUTOR=@IDT");
        //        datos.Comando.Parameters.Clear();
        //        datos.Comando.Parameters.AddWithValue("@IDP", alumno.ID);
        //        datos.Comando.Parameters.AddWithValue("@IDT", alumno.Tutor.ID);
        //        datos.AbrirConexion();
        //        datos.EjecutarConsulta();

        //        if (datos.Reader.Read())
        //        {
        //            IDA = (long)datos.Reader[0];
        //        }
        //        else
        //        {
        //            IDA = 0;
        //        }
        //        return IDA;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.CerrarConexion();
        //    }
        //}
        public long GetIDCursoWithIDCXE(long IDCXE)
        {
            Datos datos = new Datos();
            long IDC;
            try
            {
                datos.SetearConsulta("SELECT C.ID FROM SORIA_TPC.dbo.CURSOSxESTABLECIMIENTO AS CXE INNER JOIN SORIA_TPC.dbo.CURSOS AS C ON C.ID=CXE.IDCURSO WHERE CXE.ID=@IDCXE");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE", IDCXE);
                datos.AbrirConexion();
                datos.EjecutarConsulta();

                if (datos.Reader.Read())
                {
                    IDC = (long)datos.Reader[0];
                }
                else
                {
                    IDC = 0;
                }
                return IDC;
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

        public void AgregarAlumnoXCurso(long IDA, long IDCXE)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.ALUMNOSxCURSO (IDCXE, IDALUMNO ) values (@IDCXE, @IdAlumno)");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE", IDCXE);
                datos.Comando.Parameters.AddWithValue("@IdAlumno", IDA);
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
        public Int64 GetIDWithIDPersona(long IDP)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT ID FROM SORIA_TPC.dbo.ALUMNOS WHERE IDPERSONA=@IDP");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDP", IDP);
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
                datos.SetearConsulta("SELECT A.ID, p.ID, p.NOMBRE, p.APELLIDO, p.DNI, p.NACIMIENTO, p.EMAIL, DIR.ID, " +
                    "DIR.CALLE, DIR.NUMERO, T.ID, T.NOMBRE, T.APELLIDO, T.DNI, T.NACIMIENTO, T.EMAIL, DT.ID, " +
                    "DT.CALLE, DT.NUMERO FROM SORIA_TPC.dbo.ALUMNOS AS A " +
                    "LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON A.IDPERSONA=p.ID " +
                    "LEFT JOIN SORIA_TPC.dbo.DIRECCIONES AS DIR ON DIR.ID=p.IDDIRECCION " +
                    "LEFT JOIN SORIA_TPC.dbo.PERSONAS as T ON A.IDTUTOR=T.ID " +
                    "LEFT JOIN SORIA_TPC.dbo.DIRECCIONES AS DT ON DT.ID=T.IDDIRECCION WHERE p.DNI=@DNI");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@DNI", DNI);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Alumno alumno = new Alumno();
                if (datos.Reader.Read())
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
        public Alumno GetAlumnoWithId(Int64 IDA)
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
                    "LEFT JOIN SORIA_TPC.dbo.DIRECCIONES AS DT ON DT.ID=T.IDDIRECCION WHERE A.ID=@ID");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", IDA);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Alumno alumno = new Alumno();
                if (datos.Reader.Read())
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
