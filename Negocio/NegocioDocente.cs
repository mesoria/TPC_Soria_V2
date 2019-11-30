using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMaster;
using Dominio;

namespace Negocio
{
    public class NegocioDocente
    {
        public List<Docente> ListarDocentes()
        {
            Datos datos = new Datos();
            List<Docente> docentes = new List<Docente>();
            Docente aux;
            try
            {
                datos.SetearConsulta("Select DOC.ID, p.ID, p.NOMBRE, p.APELLIDO, DOC.NIVEL, p.DNI, p.NACIMIENTO, p.EMAIL, DIR.ID, DIR.CALLE, DIR.NUMERO FROM SORIA_TPC.dbo.DOCENTES AS DOC LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON DOC.IDPERSONA = p.ID left JOIN SORIA_TPC.dbo.DIRECCIONES AS DIR ON DIR.ID = p.IDDIRECCION");
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    aux = new Docente
                    {
                        IdDocente   = (Int64)datos.Reader[0],
                        ID          = (Int64)datos.Reader[1],
                        Name        = (string)datos.Reader[2],
                        Apellido    = (string)datos.Reader[3],
                        Nivel       = (string)datos.Reader[4],
                        DNI         = (string)datos.Reader[5],
                        Nacimiento  = (DateTime)datos.Reader[6],
                        Email       = (string)datos.Reader[7]
                    };
                    if (!Convert.IsDBNull(datos.Reader[8]))
                    {
                        aux.Direccion = new Direccion
                        {
                            ID      = (Int64)datos.Reader[8],
                            Calle   = (string)datos.Reader[9],
                            Number  = (string)datos.Reader[10]
                        };
                    }
                    docentes.Add(aux);
                }
                return docentes;
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


        public void Agregar(Docente docente)
        {
            Datos datos = new Datos();
            try
            {
                NegocioPersona negocioAux = new NegocioPersona();
                if (this.GetID(docente.DNI) == 0)
                {
                    negocioAux.Agregar(docente);
                    docente.ID = negocioAux.GetIDWithDNI(docente.DNI);
                    
                    datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.DOCENTES (IDPERSONA, NIVEL ) values (@IdPersona, @Nivel)");
                    datos.Comando.Parameters.Clear();
                    datos.Comando.Parameters.AddWithValue("@IdPersona", docente.ID);
                    datos.Comando.Parameters.AddWithValue("@Nivel", docente.Nivel);
                    datos.AbrirConexion();
                    datos.EjecutarAccion();
                }
                else
                {
                    //ya existe un docente con ese DNI. Deberia hacer un Trigger.
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
                datos.SetearConsulta("Select Id from SORIA_TPC.dbo.PERSONAS WHERE DNI=@DNI");
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
        public Docente GetDocenteWithDNI(string DNI)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT DOC.ID, p.ID, p.NOMBRE, p.APELLIDO, DOC.NIVEL, p.DNI, p.NACIMIENTO, "+
                    "p.EMAIL, DIR.ID, DIR.CALLE, DIR.NUMERO FROM SORIA_TPC.dbo.DOCENTES AS DOC "+
                    "LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON DOC.IDPERSONA = p.ID "+
                    "LEFT JOIN SORIA_TPC.dbo.DIRECCIONES AS DIR ON DIR.ID = p.IDDIRECCION WHERE p.DNI = @DNI");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@DNI", DNI);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Docente docente = new Docente();
                while (datos.Reader.Read())
                {
                    docente.IdDocente   = (Int64)datos.Reader[0];
                    docente.ID          = (Int64)datos.Reader[1];
                    docente.Name        = (string)datos.Reader[2];
                    docente.Apellido    = (string)datos.Reader[3];
                    docente.Nivel       = (string)datos.Reader[4];
                    docente.DNI         = (string)datos.Reader[5];
                    docente.Nacimiento  = (DateTime)datos.Reader[6];
                    docente.Email       = (string)datos.Reader[7];
                    if (!Convert.IsDBNull(datos.Reader[9]))
                    {
                        docente.Direccion = new Direccion();
                        docente.Direccion.ID     = (Int64)datos.Reader[8];
                        docente.Direccion.Calle  = (string)datos.Reader[9];
                        docente.Direccion.Number = (string)datos.Reader[10];
                    }
                    /*
                    docente.Telefono            = new Telefono();
                    docente.Telefono.TipoTelefono = (string)datos.Reader[7];
                    */
                }
                return docente;
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
        public Docente GetDocenteWithId(Int64 ID)
        {
            Datos datos = new Datos();
            try
            {   //Select p.ID, p.Nombre, p.Apellido, p.DNI, p.Nacimiento, p.Email, p.Calle, p.Numero, " +
                //"p.Contraseña, u.Perfil From PERSONAS as p INNER JOIN INSCRIPCIONES AS INST ON INST.IDP=p.ID " +
                //"INNER JOIN USUARIOS AS U ON U.ID=INST.IDU
                datos.SetearConsulta("SELECT DOC.ID, p.ID, p.NOMBRE, p.APELLIDO, DOC.NIVEL, p.DNI, p.NACIMIENTO, " +
                    "p.EMAIL, DIR.ID, DIR.CALLE, DIR.NUMERO FROM SORIA_TPC.dbo.DOCENTES AS DOC " +
                    "LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON DOC.IDPERSONA = p.ID " +
                    "LEFT JOIN SORIA_TPC.dbo.DIRECCIONES AS DIR ON DIR.ID = p.IDDIRECCION WHERE DOC.ID = @ID");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", ID);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Docente docente = new Docente();
                if (datos.Reader.Read())
                {
                    docente.IdDocente   = (Int64)datos.Reader[0];
                    docente.ID          = (Int64)datos.Reader[1];
                    docente.Name        = (string)datos.Reader[2];
                    docente.Apellido    = (string)datos.Reader[3];
                    docente.Nivel       = (string)datos.Reader[4];
                    docente.DNI         = (string)datos.Reader[5];
                    docente.Nacimiento  = (DateTime)datos.Reader[6];
                    docente.Email       = (string)datos.Reader[7];
                    if (!Convert.IsDBNull(datos.Reader[9]))
                    {
                        docente.Direccion = new Direccion();
                        docente.Direccion.ID     = (Int64)datos.Reader[8];
                        docente.Direccion.Calle  = (string)datos.Reader[9];
                        docente.Direccion.Number = (string)datos.Reader[10];
                    }
                }
                return docente;
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

        public void Modificar(Docente docente)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("update SORIA_TPC.dbo.DOCENTES Set IDPERSONA=@ID, NIVEL=@Nivel Where ID=" + docente.IdDocente);
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID",    docente.ID);
                datos.Comando.Parameters.AddWithValue("@Nivel", docente.Nivel);
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
                datos.SetearConsulta("delete from SORIA_TPC.dbo.DOCENTES where Id =" + id);
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
