using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMaster;
using Dominio;

namespace Negocio
{ 
    public class NegocioAsistencia
    {
        public List<Asistencia> ListarAsistencias()
        {
            Datos datos = new Datos();
            List<Asistencia> asistencias = new List<Asistencia>();
            Asistencia aux;
            try
            {
                datos.SetearConsulta("Select Asis.Id, Asis.IdPersona, Asis.Fecha from SORIA_TPC.dbo.ASISTENCIAS as Asis inner join Personas as P where p.ID = Asis.IdPersona");
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    aux = new Asistencia
                    {
                        Id          = (Int64)datos.Reader[0],
                        IdAlumno    = (Int64)datos.Reader[1],
                        Fecha       = (DateTime)datos.Reader[2],
                    };
                    asistencias.Add(aux);
                }
                return asistencias;
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

        public List<Asistencia> ListarAsistenciasActual(Int64 id)
        {
            Datos datos = new Datos();
            List<Asistencia> asistencias = new List<Asistencia>();
            Asistencia aux;
            try
            {
                datos.SetearConsulta("Select A.ID, A.Fecha from SORIA_TPC.dbo.ASISTENCIAS as A WHERE A.IDPERSONA = @ID AND MONTH(A.Fecha) = MONTH(GETDATE())");
                //Select A.ID, A.IDPERSONA, A.Fecha from ASISTENCIAS as A WHERE A.IDPERSONA = 1 AND MONTH(Fecha)= MONTH(GETDATE())
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", id);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    aux = new Asistencia
                    {
                        Id          = (Int64)datos.Reader[0],
                        IdAlumno    = id,
                        Fecha       = (DateTime)datos.Reader[1],
                    };
                    asistencias.Add(aux);
                }
                return asistencias;
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
        public string GetToday()
        {
            DateTime today = DateTime.Today;
            return today.Year.ToString() + "/"+ today.Month.ToString()+ "/" + today.Day.ToString();
        }
        public void Agregar(Int64 IDPersona)
        {
            Datos datos = new Datos();
            try
            {
                //if ()//Alguna validación
                //{
                    datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.ASISTENCIAS (IDPERSONA,FECHA) values (@IDPersona, @today");
                    datos.Comando.Parameters.Clear();
                    datos.Comando.Parameters.AddWithValue("@IDPersona", IDPersona);
                    datos.Comando.Parameters.AddWithValue("@today", GetToday() );

                    datos.AbrirConexion();
                    datos.EjecutarAccion();
                //}
                //else
                //{
                //    //ya existe una asistencia con ese DNI. Deberia hacer un Trigger o un procedimiento almacenado.
                //}
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

        public bool CheckAsistencia(Int64 id,int y, int m, int d)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("Select A.ID from SORIA_TPC.dbo.ASISTENCIAS as A WHERE A.IDPERSONA =@ID AND YEAR(A.FECHA)=@YEAR AND MONTH(A.Fecha)=@MONTH AND DAY(A.FECHA)=@DAY");

                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@ID", id);
                datos.Comando.Parameters.AddWithValue("@YEAR", y);
                datos.Comando.Parameters.AddWithValue("@MONTH", m);
                datos.Comando.Parameters.AddWithValue("@DAY", d);

                datos.AbrirConexion();
                datos.EjecutarConsulta();

                while (datos.Reader.Read())
                {
                    return true;
                }
                return false;
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