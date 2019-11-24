using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMaster;
using Dominio;

namespace Negocio
{
    public class NegocioEstablecimiento
    {
        public List<Establecimiento> ListarEstablecimiento()
        {
            Datos datos = new Datos();
            List<Establecimiento> establecimientos = new List<Establecimiento>();
            Establecimiento aux;
            try
            {
                datos.SetearConsulta("Select esc.ID, esc.NOMBRE, esc.NUMERO, esc.NIVEL, dir.ID, dir.CALLE, dir.NUMERO from SORIA_TPC.dbo.ESTABLECIMIENTOS as esc inner join SORIA_TPC.dbo.DIRECCIONES as dir on dir.ID = esc.IDDIRECCION");
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    aux = new Establecimiento
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
                            ID      = (Int64)datos.Reader[4],
                            Calle   = (string)datos.Reader[5],
                            Number  = (string)datos.Reader[6]
                        };
                    }
                    //Telefono = new Telefono
                    //{ 
                    //  ID             = (string)datos.Reader[8],
                    //  TipoTelefono   = (string)datos.Reader[9]
                    //}
                    establecimientos.Add(aux);
                }
                return establecimientos;
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


        public void Agregar(Establecimiento establecimiento)
        {
            Datos datos = new Datos();
            try
            {
                if (this.GetEstablecimiento( establecimiento ).ID == 0)
                {
                    datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.ESTABLECIMIENTOS (NOMBRE, NUMERO, NIVEL, IDDIRECCION) values (@Nombre, @Numero, @Nivel, @IDDireccion)");
                    datos.Comando.Parameters.Clear();
                    datos.Comando.Parameters.AddWithValue("@Nombre",        establecimiento.Name);
                    datos.Comando.Parameters.AddWithValue("@Numero",        establecimiento.Number);
                    datos.Comando.Parameters.AddWithValue("@Nivel",         establecimiento.Nivel);
                    datos.Comando.Parameters.AddWithValue("@IDDireccion",   establecimiento.Direccion.ID);
                    datos.AbrirConexion();
                    datos.EjecutarAccion();
                }
                else
                {
                    //ya existe un establecimiento asi... Deberia hacer un Trigger o un procedimiento almacenado.
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
        public void Modificar(Establecimiento establecimiento)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("update SORIA_TPC.dbo.ESTABLECIMIENTOS Set NOMBRE=@Nombre, NUMERO=@Numero, NIVEL=@Nivel, IDDIRECCION=@IdDireccion Where ID=@IdEstablecimiento");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Nombre",            establecimiento.Name);
                datos.Comando.Parameters.AddWithValue("@Numero",            establecimiento.Number);
                datos.Comando.Parameters.AddWithValue("@Nivel",             establecimiento.Nivel);
                datos.Comando.Parameters.AddWithValue("@IdDireccion",       establecimiento.Direccion.ID);
                datos.Comando.Parameters.AddWithValue("@IdEstablecimiento", establecimiento.ID);
                // datos.Comando.Parameters.AddWithValue("@Ciudad", establecimiento.ciudad.ToString());
                //datos.Comando.Parameters.AddWithValue("@CP", establecimiento.CP.ToString());
                // datos.Comando.Parameters.AddWithValue("@FechaRegistro", establecimiento.fechaRegistro);
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

        public Establecimiento GetEstablecimiento( Establecimiento Establecimiento)
        {
            Datos datos = new Datos();
            try
            {   
                datos.SetearConsulta("Select esc.ID, esc.NOMBRE, esc.NUMERO, esc.NIVEL, dir.ID, dir.CALLE, dir.NUMERO from SORIA_TPC.dbo.ESTABLECIMIENTOS as esc inner join SORIA_TPC.dbo.DIRECCIONES as dir on dir.ID = esc.IDDIRECCION where esc.NUMERO=@number AND esc.NOMBRE=@Nombre");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Nombre", Establecimiento.Name);
                datos.Comando.Parameters.AddWithValue("@number", Establecimiento.Number);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Establecimiento establecimiento = new Establecimiento();
                if (datos.Reader.Read())
                {
                    establecimiento = new Establecimiento
                    {
                        ID     = (Int64)datos.Reader[0],
                        Name   = (string)datos.Reader[1],
                        Nivel  = (string)datos.Reader[3]
                    };
                    if (!Convert.IsDBNull(datos.Reader[2]))
                    {
                        establecimiento.Number = (int)datos.Reader[2];
                    }
                    else
                    {
                        establecimiento.Number = 0;
                    }
                    if (!Convert.IsDBNull(datos.Reader[4]))
                    {
                        establecimiento.Direccion = new Direccion
                        {
                            ID     = (Int64)datos.Reader[4],
                            Calle  = (string)datos.Reader[5],
                            Number = (string)datos.Reader[6]
                        };
                    }
                }
                else
                {
                    establecimiento.ID = 0;
                }
                return establecimiento;
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
        public Establecimiento GetEstablecimientoWithId( Int64 ID)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("Select esc.ID, esc.NOMBRE, esc.NUMERO, esc.NIVEL, dir.ID, dir.CALLE, dir.NUMERO from SORIA_TPC.dbo.ESTABLECIMIENTOS as esc inner join SORIA_TPC.dbo.DIRECCIONES as dir on dir.ID = esc.IDDIRECCION where esc.ID=@ID");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", ID);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Establecimiento establecimiento = new Establecimiento();
                if (datos.Reader.Read())
                {
                    establecimiento = new Establecimiento
                    {
                        ID = (Int64)datos.Reader[0],
                        Name = (string)datos.Reader[1],
                        Nivel = (string)datos.Reader[3],
                    };
                    if (!Convert.IsDBNull(datos.Reader[2]))
                    {
                        establecimiento.Number = (int)datos.Reader[2];
                    }
                    else
                    {
                        establecimiento.Number = 0;
                    }
                    if (!Convert.IsDBNull(datos.Reader[4]))
                    {
                        establecimiento.Direccion = new Direccion
                        {
                            ID = (Int64)datos.Reader[4],
                            Calle = (string)datos.Reader[5],
                            Number = (string)datos.Reader[6]
                        };
                    }
                }
                else
                {
                    establecimiento.ID = 0;
                }
                return establecimiento;
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
    }
}
