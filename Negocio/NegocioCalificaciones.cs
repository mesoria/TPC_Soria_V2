using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMaster;
using Dominio;

namespace Negocio
{
    public class NegocioCalificaciones
    {
        //Calificaciones R, B, MB, S
        public Calificaciones GetCalificacion(long IDCXE, long IDA, short anio)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT ID, NOTA1, NOTA2, NOTA3, OBSERVACION FROM SORIA_TPC.dbo.CALIFICACIONESXCURSO " +
                    "WHERE IDCXE=@IDCXE AND IDALUMNO=@IDA AND ANIO=@Anio");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE", IDCXE);
                datos.Comando.Parameters.AddWithValue("@IDA"  , IDA);
                datos.Comando.Parameters.AddWithValue("@Anio" , anio);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Calificaciones calificaciones = new Calificaciones();
                Letras Letra = new Letras();
                if (datos.Reader.Read())
                {
                    calificaciones.Id = (long)datos.Reader[0];
                    if (!Convert.IsDBNull(datos.Reader[1]))
                    {
                        Letra.Letra1 = (string)datos.Reader[1];
                    }
                    if (!Convert.IsDBNull(datos.Reader[2]))
                    {
                        Letra.Letra2 = (string)datos.Reader[2];
                    }
                    if (!Convert.IsDBNull(datos.Reader[3]))
                    {
                        Letra.Letra3 = (string)datos.Reader[3];
                    }
                    if (!Convert.IsDBNull(datos.Reader[4]))
                    {
                        calificaciones.Observaciones = (string)datos.Reader[4];
                    }
                    else
                    {
                        calificaciones.Observaciones = "";
                    }
                    calificaciones.Letras = Letra;
                }
                else
                {
                    AgregarCalificaciones(IDCXE, IDA, anio);
                    GetCalificacion(IDCXE, IDA, anio);
                }
                return calificaciones;
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
        public void AgregarCalificaciones(long IDCXE, long IDA, short anio)
        {
            Calificaciones calificaciones = new Calificaciones();
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.CALIFICACIONESXCURSO (IDCXE, IDALUMNO, ANIO ) values (@IDCXE, @IdAlumno, @Anio)");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE"   , IDCXE);
                datos.Comando.Parameters.AddWithValue("@IdAlumno", IDA);
                datos.Comando.Parameters.AddWithValue("@Anio"    , anio);
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
        public void ModificarCalificaciones(long IDCXE, long IDA, short anio, string nota1, string nota2, string nota3)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("UPDATE SORIA_TPC.dbo.CALIFICACIONESXCURSO SET NOTA1=@Nota1, NOTA2=@Nota2, NOTA3=@Nota3 WHERE IDCXE=@IDCXE AND IDALUMNO=@IdAlumno AND ANIO=@Anio");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE"   , IDCXE);
                datos.Comando.Parameters.AddWithValue("@IdAlumno", IDA);
                datos.Comando.Parameters.AddWithValue("@Anio"    , anio);
                datos.Comando.Parameters.AddWithValue("@Nota1"   , nota1);
                datos.Comando.Parameters.AddWithValue("@Nota2"   , nota2);
                datos.Comando.Parameters.AddWithValue("@Nota3"   , nota3);
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
        //Calificaciones de 0 a 10
        public Calificaciones GetNota(long IDCXE, long IDA, short anio)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT ID, NOTA1, NOTA2, NOTA3, NOTA4, OBSERVACION FROM SORIA_TPC.dbo.NOTASXCURSO " +
                    "WHERE IDCXE=@IDCXE AND IDALUMNO=@IDA AND ANIO=@Anio");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE", IDCXE);
                datos.Comando.Parameters.AddWithValue("@IDA", IDA);
                datos.Comando.Parameters.AddWithValue("@Anio", anio);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Calificaciones calificaciones = new Calificaciones();
                Notas notas = new Notas();
                if (datos.Reader.Read())
                {
                    calificaciones.Id = (long)datos.Reader[0];
                    if (!Convert.IsDBNull(datos.Reader[1]))
                    {
                        notas.Nota1 = (byte)datos.Reader[1];
                    }
                    if (!Convert.IsDBNull(datos.Reader[2]))
                    {
                        notas.Nota2 = (byte)datos.Reader[2];
                    }
                    if (!Convert.IsDBNull(datos.Reader[3]))
                    {
                        notas.Nota3 = (byte)datos.Reader[3];
                    }
                    if (!Convert.IsDBNull(datos.Reader[4]))
                    {
                        notas.Nota4 = (byte)datos.Reader[4];
                    }
                    if (!Convert.IsDBNull(datos.Reader[5]))
                    {
                        calificaciones.Observaciones = (string)datos.Reader[5];
                    }
                    calificaciones.Notas = notas;
                }
                else
                {
                    AgregarNotas(IDCXE, IDA, anio);
                    GetNota(IDCXE, IDA, anio);
                }
                return calificaciones;
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
        public void AgregarNotas(long IDCXE, long IDA, short anio)
        {
            Calificaciones calificaciones = new Calificaciones();
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.NOTASXCURSO (IDCXE, IDALUMNO, ANIO ) values (@IDCXE, @IdAlumno, @Anio)");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE", IDCXE);
                datos.Comando.Parameters.AddWithValue("@IdAlumno", IDA);
                datos.Comando.Parameters.AddWithValue("@Anio", anio);
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
        public void ModificarNotas(long IDCXE, long IDA, short anio, byte nota1, byte nota2, byte nota3)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("UPDATE SORIA_TPC.dbo.NOTASXCURSO SET NOTA1=@Nota1, NOTA2=@Nota2, NOTA3=@Nota3 WHERE IDCXE=@IDCXE AND IDALUMNO=@IdAlumno AND ANIO=@Anio");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE", IDCXE);
                datos.Comando.Parameters.AddWithValue("@IdAlumno", IDA);
                datos.Comando.Parameters.AddWithValue("@Anio", anio);
                datos.Comando.Parameters.AddWithValue("@Nota1", nota1);
                datos.Comando.Parameters.AddWithValue("@Nota2", nota2);
                datos.Comando.Parameters.AddWithValue("@Nota3", nota3);
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
        public void ModificarNota1(long IDCXE, long IDA, short anio, byte nota)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("UPDATE SORIA_TPC.dbo.NOTASXCURSO SET NOTA1=@Nota WHERE IDCXE=@IDCXE AND IDALUMNO=@IdAlumno AND ANIO=@Anio");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE"   , IDCXE);
                datos.Comando.Parameters.AddWithValue("@Nota"    , nota);
                datos.Comando.Parameters.AddWithValue("@IdAlumno", IDA);
                datos.Comando.Parameters.AddWithValue("@Anio"    , anio);
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
        public void ModificarNota2(long IDCXE, long IDA, short anio, byte nota)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("UPDATE SORIA_TPC.dbo.NOTASXCURSO SET NOTA2=@Nota WHERE IDCXE=@IDCXE AND IDALUMNO=@IdAlumno AND ANIO=@Anio");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE", IDCXE);
                datos.Comando.Parameters.AddWithValue("@Nota", nota);
                datos.Comando.Parameters.AddWithValue("@IdAlumno", IDA);
                datos.Comando.Parameters.AddWithValue("@Anio", anio);
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
        public void ModificarNota3(long IDCXE, long IDA, short anio, byte nota)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("UPDATE SORIA_TPC.dbo.NOTASXCURSO SET NOTA3=@Nota WHERE IDCXE=@IDCXE AND IDALUMNO=@IdAlumno AND ANIO=@Anio");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE", IDCXE);
                datos.Comando.Parameters.AddWithValue("@Nota", nota);
                datos.Comando.Parameters.AddWithValue("@IdAlumno", IDA);
                datos.Comando.Parameters.AddWithValue("@Anio", anio);
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
        public void ModificarObservacion(long IDCXE, long IDA, short anio, string nota)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("UPDATE SORIA_TPC.dbo.NOTASXCURSO SET OBSERVACION=@Nota WHERE IDCXE=@IDCXE AND IDALUMNO=@IdAlumno AND ANIO=@Anio");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDCXE", IDCXE);
                datos.Comando.Parameters.AddWithValue("@Nota", nota);
                datos.Comando.Parameters.AddWithValue("@IdAlumno", IDA);
                datos.Comando.Parameters.AddWithValue("@Anio", anio);
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
                datos.SetearConsulta("DELETE FROM SORIA_TPC.dbo.NOTASXCURSO WHERE ID=" + ID);
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
