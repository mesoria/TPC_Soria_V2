using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataMaster
{
    public class Datos
    {
        public static string cadenaConexion = "data source=LAPTOP-4ROB6KSH\\SQLEXPRESS; initial catalog=SORIA_TPC; integrated security=sspi";
        private SqlCommand comando;
        private readonly SqlConnection conexion;
        private SqlDataReader reader;

        public SqlDataReader Reader
        {
            get { return reader; }
        }
        public SqlCommand Comando
        {
            get { return comando; }
        }

        public Datos()
        {
            conexion = new SqlConnection(cadenaConexion);
        }

        //setear consulta embebida.
        public void SetearConsulta(string consulta)
        {
            comando = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                CommandText = consulta
            };
        }
        //setear consulta embebida con condicion.
        public void SetearConsulta(string consulta, int id)
        {
            comando = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                CommandText = consulta + id.ToString()
            };
        }

        //esto para luego...
        public void SetearSP(string sp)//quería usarlo pero no llegué.
        {
            comando = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                CommandText = sp
            };
        }

        public void AbrirConexion()
        {
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CerrarConexion()
        {
            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EjecutarAccion()
        {
            try
            {
                comando.Connection = conexion;
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EjecutarAccionReturn()
        {
            try
            {
                comando.Connection = conexion;
                return (int)comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EjecutarConsulta()
        {
            try
            {
                comando.Connection = conexion;
                reader = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
