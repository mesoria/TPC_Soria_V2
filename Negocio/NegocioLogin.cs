using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMaster;
using Dominio;

namespace Negocio
{
    public class NegocioLogin
    {
        public bool Autenticar(string usuario, string password)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM SORIA_TPC.dbo.USUARIOS WHERE USERNAME=@usuario AND CONTRASEÑA=@password");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@usuario", usuario);
                datos.Comando.Parameters.AddWithValue("@password", password);
                datos.AbrirConexion();
                datos.EjecutarConsulta();

                if (datos.Reader.Read())
                {
                    if ((int)datos.Reader[0] > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
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
        public static void Security(int userid, string usuario, DateTime ultimoacc, string ip)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("INSERT INTO UsuarioSecurity( UsuarioID, Username, UltimoAcceso, IPAcceso) VALUES( @UsuarioID, @Username, @UltimoAcceso, @IPAcceso) SELECT SCOPE_IDENTITY()");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@UsuarioID",     userid);
                datos.Comando.Parameters.AddWithValue("@Username",      usuario);
                datos.Comando.Parameters.AddWithValue("@UltimoAcceso",  ultimoacc);
                datos.Comando.Parameters.AddWithValue("@IPAcceso",      ip);
                datos.AbrirConexion();
                datos.EjecutarConsulta();

                if (datos.Reader.Read())
                {
                    //datos.Reader[0].ExecuteScalar();
                }
                //int resultado = Convert.ToInt32(command.ExecuteScalar());
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
        public Usuario GetUsuario(string username, string password)
        {
            Datos datos = new Datos();
            Usuario user = new Usuario();
            try
            {
                datos.SetearConsulta("SELECT P.ID, PERFIL.NOMBRE FROM SORIA_TPC.dbo.USUARIOS AS USU LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON p.ID = USU.IDPERSONA LEFT JOIN SORIA_TPC.dbo.PERFILES AS PERFIL ON PERFIL.ID = USU.IDPERFIL  WHERE USU.USERNAME =@USUARIO AND USU.CONTRASEÑA =@CONTRASEÑA ");
                //datos.SetearConsulta("SELECT P.ID, PERFIL.NOMBRE FROM SORIA_TPC.dbo.USUARIOS AS USU "
                //    + "LEFT JOIN SORIA_TPC.dbo.PERSONAS as p ON p.ID = USU.IDPERSONA"
                //    + "LEFT JOIN SORIA_TPC.dbo.PERFILES AS PERFIL ON PERFIL.ID = USU.IDPERFIL"
                //    + "WHERE USU.USERNAME=@USUARIO AND USU.CONTRASEÑA=@CONTRASEÑA");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@USUARIO", username);
                datos.Comando.Parameters.AddWithValue("@CONTRASEÑA", password);
                datos.AbrirConexion();
                datos.EjecutarConsulta();

                if (datos.Reader.Read())
                {
                    user.ID     = (Int64)datos.Reader[0];
                    user.Perfil = (string)datos.Reader[1];
                }
                return user;
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
