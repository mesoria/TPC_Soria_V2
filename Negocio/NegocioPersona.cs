using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMaster;
using Dominio;

namespace Negocio
{
    public class NegocioPersona
    {
        public List<Persona> ListarPersonas()
        {
            Datos datos = new Datos();
            List<Persona> personas = new List<Persona>();
            Persona aux;
            try
            {
                datos.SetearConsulta("Select p.ID, p.Nombre, p.Apellido, p.DNI, p.Nacimiento, p.Email, d.ID, d.Calle, d.Numero From SORIA_TPC.dbo.PERSONAS as p left JOIN SORIA_TPC.dbo.DIRECCIONES AS d ON d.ID = p.IDDIRECCION");
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    aux = new Persona
                    {
                        ID          = (Int64)datos.Reader[0],
                        Name        = (string)datos.Reader[1],
                        Apellido    = (string)datos.Reader[2],
                        DNI         = (string)datos.Reader[3],
                        Nacimiento  = (DateTime)datos.Reader[4],
                        Email       = (string)datos.Reader[5]
                    };
                    if (!Convert.IsDBNull(datos.Reader[6]))
                    {
                        aux.Direccion = new Direccion
                        {
                            ID = (Int64)datos.Reader[6],
                            Calle = (string)datos.Reader[7],
                            Number = (string)datos.Reader[8]
                        };
                    }
                    //if (!Convert.IsDBNull(datos.Reader[7]))
                    //{
                        //aux.Telefono = new Telefono
                        //{ 
                        //    ID           = (string)datos.Reader[8],
                        //    TipoTelefono = (string)datos.Reader[9]
                        //};
                    //}
                    personas.Add(aux);
                }
                return personas;
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


        public void Agregar(Persona persona)
        {
            Datos datos = new Datos();
            try
            {
                if ( this.GetID( persona.DNI) == 0)
                {
                    datos.SetearConsulta("insert into SORIA_TPC.dbo.PERSONAS (DNI, Nombre, Apellido, Email, Nacimiento) values (@DNI, @Nombre, @Apellido, @Email, @Nacimiento)");
                    datos.Comando.Parameters.Clear();
                    datos.Comando.Parameters.AddWithValue("@DNI",           persona.DNI.ToString());
                    datos.Comando.Parameters.AddWithValue("@Nombre",        persona.Name.ToString());
                    datos.Comando.Parameters.AddWithValue("@Apellido",      persona.Apellido.ToString());
                    datos.Comando.Parameters.AddWithValue("@Email",         persona.Email.ToString());
                    datos.Comando.Parameters.AddWithValue("@Nacimiento",    persona.Nacimiento.ToString());
                    datos.AbrirConexion();
                    datos.EjecutarAccion();
                }
                else
                {
                    //ya existe una persona con ese DNI. Deberia hacer un Trigger o un procedimiento almacenado.
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
        public Persona GetPersona(string DNI)
        {
            Datos datos = new Datos();
            try
            {   //Select p.ID, p.Nombre, p.Apellido, p.DNI, p.Nacimiento, p.Email, p.Calle, p.Numero, " +
                //"p.Contraseña, u.Perfil From PERSONAS as p INNER JOIN INSCRIPCIONES AS INST ON INST.IDP=p.ID " +
                //"INNER JOIN USUARIOS AS U ON U.ID=INST.IDU
                datos.SetearConsulta("Select p.ID, p.Nombre, p.Apellido, p.DNI, p.Nacimiento, p.Email, d.ID, d.Calle, d.Numero From SORIA_TPC.dbo.PERSONAS as p left JOIN SORIA_TPC.dbo.DIRECCIONES AS d ON d.ID = p.IDDIRECCION where p.DNI =@DNI");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@DNI", DNI);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Persona persona = new Persona();
                while (datos.Reader.Read())
                {
                    persona.ID                  = (Int64)datos.Reader[0];
                    persona.Name                = (string)datos.Reader[1];
                    persona.Apellido            = (string)datos.Reader[2];
                    persona.DNI                 = (string)datos.Reader[3];
                    persona.Nacimiento          = (DateTime)datos.Reader[4];
                    persona.Email               = (string)datos.Reader[5];
                    if (!Convert.IsDBNull(datos.Reader[7]))
                    {
                        persona.Direccion           = new Direccion();
                        persona.Direccion.ID        = (Int64)datos.Reader[6];
                        persona.Direccion.Calle     = (string)datos.Reader[7];
                        persona.Direccion.Number    = (string)datos.Reader[8];
                    }
                    /*
                    persona.Telefono            = new Telefono();
                    persona.Telefono.TipoTelefono = (string)datos.Reader[7];
                    */
                }
                return persona;
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
        public Persona GetPersonaWithId(Int64 ID)
        {
            Datos datos = new Datos();
            try
            {   //Select p.ID, p.Nombre, p.Apellido, p.DNI, p.Nacimiento, p.Email, p.Calle, p.Numero, " +
                //"p.Contraseña, u.Perfil From PERSONAS as p INNER JOIN INSCRIPCIONES AS INST ON INST.IDP=p.ID " +
                //"INNER JOIN USUARIOS AS U ON U.ID=INST.IDU
                datos.SetearConsulta("Select p.ID, p.Nombre, p.Apellido, p.DNI, p.Nacimiento, p.Email, d.ID, d.Calle, d.Numero From SORIA_TPC.dbo.PERSONAS as p left JOIN SORIA_TPC.dbo.DIRECCIONES AS d ON d.ID = p.IDDIRECCION where p.ID =@ID");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", ID);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Persona persona = new Persona();
                while (datos.Reader.Read())
                {
                    persona.ID = (Int64)datos.Reader[0];
                    persona.Name = (string)datos.Reader[1];
                    persona.Apellido = (string)datos.Reader[2];
                    persona.DNI = (string)datos.Reader[3];
                    persona.Nacimiento = (DateTime)datos.Reader[4];
                    persona.Email = (string)datos.Reader[5];
                    if (!Convert.IsDBNull(datos.Reader[7]))
                    {
                        persona.Direccion = new Direccion();
                        persona.Direccion.ID = (Int64)datos.Reader[6];
                        persona.Direccion.Calle = (string)datos.Reader[7];
                        persona.Direccion.Number = (string)datos.Reader[8];
                    }
                    /*
                    persona.Telefono            = new Telefono();
                    persona.Telefono.TipoTelefono = (string)datos.Reader[7];
                    */
                }
                return persona;
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

        public void Modificar(Persona persona)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("update SORIA_TPC.dbo.PERSONAS Set DNI=@DNI, Nombre=@Nombre, Apellido=@Apellido, Email=@Email, " +
                    "Direccion=@Direccion, Ciudad=@Ciudad, CodigoPostal=@CP, FechaRegistro=FechaRegistro Where ID=" + persona.ID);
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@DNI", persona.DNI);
                datos.Comando.Parameters.AddWithValue("@Nombre", persona.Name);
                datos.Comando.Parameters.AddWithValue("@Apellido", persona.Apellido.ToString());
                datos.Comando.Parameters.AddWithValue("@Email", persona.Email.ToString());
                datos.Comando.Parameters.AddWithValue("@Direccion", persona.Direccion.Calle.ToString());
               // datos.Comando.Parameters.AddWithValue("@Ciudad", persona.ciudad.ToString());
                //datos.Comando.Parameters.AddWithValue("@CP", persona.CP.ToString());
               // datos.Comando.Parameters.AddWithValue("@FechaRegistro", persona.fechaRegistro);
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

        public void Eliminar(int id)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("delete from SORIA_TPC.dbo.PERSONAS where Id =" + id);
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
