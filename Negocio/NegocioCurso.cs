using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMaster;
using Dominio;

namespace Negocio
{
    public class NegocioCurso
    {
        /*
        public List<Curso> ListarCurso()
        {
            Datos datos = new Datos();
            List<Curso> cursos = new List<Curso>();
            Curso aux;
            try
            {
                datos.SetearConsulta("Select esc.ID, esc.NOMBRE, esc.NUMERO, esc.NIVEL, dir.ID, dir.CALLE, dir.NUMERO from SORIA_TPC.dbo.ESTABLECIMIENTOS as esc inner join SORIA_TPC.dbo.DIRECCIONES as dir on dir.ID = esc.IDDIRECCION");
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    aux = new Curso
                    {
                        ID = (Int64)datos.Reader[0],
                        Name = (string)datos.Reader[1],
                        Nivel = (string)datos.Reader[3]
                    };
                    if (!Convert.IsDBNull(datos.Reader[2]))
                    {
                        aux.Number = (int)datos.Reader[2];
                    }

                    //else
                    //{
                    //    aux.Number = 0;
                    //}
                    if (!Convert.IsDBNull(datos.Reader[4]))
                    {
                        aux.Direccion = new Direccion
                        {
                            ID = (Int64)datos.Reader[4],
                            Calle = (string)datos.Reader[5],
                            Number = (string)datos.Reader[6]
                        };
                    }
                    //Telefono = new Telefono
                    //{ 
                    //  ID             = (string)datos.Reader[8],
                    //  TipoTelefono   = (string)datos.Reader[9]
                    //}
                    cursos.Add(aux);
                }
                return cursos;
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


        public void Agregar(Curso curso)
        {
            Datos datos = new Datos();
            try
            {
                if (this.GetCurso(curso).ID == 0)
                {
                    datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.ESTABLECIMIENTOS (NOMBRE, NUMERO, NIVEL, IDDIRECCION) values (@Nombre, @Numero, @Nivel, @IDDireccion)");
                    datos.Comando.Parameters.Clear();
                    datos.Comando.Parameters.AddWithValue("@Nombre", curso.Name);
                    datos.Comando.Parameters.AddWithValue("@Numero", curso.Number);
                    datos.Comando.Parameters.AddWithValue("@Nivel", curso.Nivel);
                    datos.Comando.Parameters.AddWithValue("@IDDireccion", curso.Direccion.ID);
                    datos.AbrirConexion();
                    datos.EjecutarAccion();
                }
                else
                {
                    //ya existe un curso asi... Deberia hacer un Trigger o un procedimiento almacenado.
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
        public void Modificar(Curso curso)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("update SORIA_TPC.dbo.ESTABLECIMIENTOS Set NOMBRE=@Nombre, NUMERO=@Numero, NIVEL=@Nivel, IDDIRECCION=@IdDireccion Where ID=@IdCurso");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Nombre", curso.Name);
                datos.Comando.Parameters.AddWithValue("@Numero", curso.Number);
                datos.Comando.Parameters.AddWithValue("@Nivel", curso.Nivel);
                datos.Comando.Parameters.AddWithValue("@IdDireccion", curso.Direccion.ID);
                datos.Comando.Parameters.AddWithValue("@IdCurso", curso.ID);
                // datos.Comando.Parameters.AddWithValue("@Ciudad", curso.ciudad.ToString());
                //datos.Comando.Parameters.AddWithValue("@CP", curso.CP.ToString());
                // datos.Comando.Parameters.AddWithValue("@FechaRegistro", curso.fechaRegistro);
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

        public void Eliminar(Int64 id)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("delete from SORIA_TPC.dbo.ESTABLECIMIENTOS where Id =" + id);
                datos.AbrirConexion();
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Curso GetCurso(Curso curso)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("Select esc.ID, esc.NOMBRE, esc.NUMERO, esc.NIVEL, dir.ID, dir.CALLE, dir.NUMERO from SORIA_TPC.dbo.ESTABLECIMIENTOS as esc inner join SORIA_TPC.dbo.DIRECCIONES as dir on dir.ID = esc.IDDIRECCION where esc.NUMERO=@number AND esc.NOMBRE=@Nombre");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Nombre", curso.Name);
                datos.Comando.Parameters.AddWithValue("@number", curso.Number);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Curso curso = new Curso();
                if (datos.Reader.Read())
                {
                    curso = new Curso
                    {
                        ID = (Int64)datos.Reader[0],
                        Name = (string)datos.Reader[1],
                        Nivel = (string)datos.Reader[3]
                    };
                    if (!Convert.IsDBNull(datos.Reader[2]))
                    {
                        curso.Number = (int)datos.Reader[2];
                    }
                    else
                    {
                        curso.Number = 0;
                    }
                    if (!Convert.IsDBNull(datos.Reader[4]))
                    {
                        curso.Direccion = new Direccion
                        {
                            ID = (Int64)datos.Reader[4],
                            Calle = (string)datos.Reader[5],
                            Number = (string)datos.Reader[6]
                        };
                    }
                }
                else
                {
                    curso.ID = 0;
                }
                return curso;
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
        public Curso GetCursoWithId(Int64 ID)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("Select esc.ID, esc.NOMBRE, esc.NUMERO, esc.NIVEL, dir.ID, dir.CALLE, dir.NUMERO from SORIA_TPC.dbo.ESTABLECIMIENTOS as esc inner join SORIA_TPC.dbo.DIRECCIONES as dir on dir.ID = esc.IDDIRECCION where esc.ID=@ID");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", ID);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Curso curso = new Curso();
                if (datos.Reader.Read())
                {
                    curso = new Curso
                    {
                        ID = (Int64)datos.Reader[0],
                        Name = (string)datos.Reader[1],
                        Nivel = (string)datos.Reader[3],
                    };
                    if (!Convert.IsDBNull(datos.Reader[2]))
                    {
                        curso.Number = (int)datos.Reader[2];
                    }
                    else
                    {
                        curso.Number = 0;
                    }
                    if (!Convert.IsDBNull(datos.Reader[4]))
                    {
                        curso.Direccion = new Direccion
                        {
                            ID = (Int64)datos.Reader[4],
                            Calle = (string)datos.Reader[5],
                            Number = (string)datos.Reader[6]
                        };
                    }
                }
                else
                {
                    curso.ID = 0;
                }
                return curso;
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
        */
    }
}
