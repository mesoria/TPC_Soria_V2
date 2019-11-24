using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMaster;
using Dominio;

namespace Negocio
{
    public class NegocioDireccion
    {
        public void Agregar(Direccion direccion)
        {
            Datos datos = new Datos();
            try
            {
                if ( GetDireccion(direccion).ID == 0)
                {
                    //datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.DIRECCIONES (CALLE, NUMERO, DEPARTAMENTO, PISO ) values (@Calle, @Numero, @Departamento, @Piso)");
                    datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.DIRECCIONES (CALLE, NUMERO ) values (@Calle, @Numero)");
                    datos.Comando.Parameters.Clear();
                    datos.Comando.Parameters.AddWithValue("@Calle",         direccion.Calle);
                    datos.Comando.Parameters.AddWithValue("@Numero",        direccion.Number);
                    //datos.Comando.Parameters.AddWithValue("@Departamento",  direccion.Departamento);
                    //datos.Comando.Parameters.AddWithValue("@Piso",          direccion.Piso);
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

        public void Modificar(Direccion direccion)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("update SORIA_TPC.dbo.DIRECCIONES Set CALLE=@Calle, NUMERO=@Numero Where ID=@IdDireccion");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Calle", direccion.Calle);
                datos.Comando.Parameters.AddWithValue("@Numero", direccion.Number);
                datos.Comando.Parameters.AddWithValue("@IdDireccion", direccion.ID);
                datos.Comando.Parameters.AddWithValue("@IdEstablecimiento", direccion.ID);
                // datos.Comando.Parameters.AddWithValue("@Ciudad", direccion.ciudad.ToString());
                //datos.Comando.Parameters.AddWithValue("@CP", direccion.CP.ToString());
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
                datos.SetearConsulta("delete from SORIA_TPC.dbo.DIRECCIONES where Id =" + id);
                datos.AbrirConexion();
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int64 UltimoRegistro()
        {
            int cant = 0;
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) as CANT FROM SORIA_TPC.dbo.DIRECCIONES");
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                if (datos.Reader.Read())
                {
                    cant = (int)datos.Reader["CANT"];
                }
                return cant;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Direccion GetDireccion( Direccion direccion)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT * FROM SORIA_TPC.dbo.DIRECCIONES WHERE CALLE like @calle AND NUMERO = @numero");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@calle", direccion.Calle);
                datos.Comando.Parameters.AddWithValue("@numero", direccion.Number);
                datos.AbrirConexion();
                datos.EjecutarConsulta();

                Direccion aux;

                if (datos.Reader.Read())
                {
                    aux = new Direccion
                    {
                        ID      = (Int64)datos.Reader[0],
                        Calle   = (string)datos.Reader[1],
                        Number  = (string)datos.Reader[2]
                    };
                    if (!Convert.IsDBNull(datos.Reader[3]))
                    {
                        aux.Departamento    = (string)datos.Reader[3];
                        aux.Piso            = (int)datos.Reader[4];
                    };
                }
                else
                {
                    aux = new Direccion();
                    aux.ID = 0;
                }
                return aux;
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
