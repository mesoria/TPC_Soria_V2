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
        public List<Curso> ListarCurso(Int64 IDEstablecimineto)
        {
            Datos datos = new Datos();
            List<Curso> cursos = new List<Curso>();
            Curso aux;
            try
            {
                datos.SetearConsulta("SELECT C.ID, C.NOMBRE FROM CURSOSxESTABLECIMIENTO AS CXE INNER JOIN CURSOS AS C ON C.ID=CXE.IDCURSO WHERE CXE.IDESTABLECIMIENTO=@IDESTABLECIMIENTO");
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    aux = new Curso
                    {
                        ID = (Int64)datos.Reader[0],
                        Name = (string)datos.Reader[1],
                    };
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
                if (this.GetCursoWithName(curso).ID == 0)
                {
                    datos.SetearConsulta("INSERT INTO SORIA_TPC.dbo.CURSOS (NOMBRE) values (@Nombre)");
                    datos.Comando.Parameters.Clear();
                    datos.Comando.Parameters.AddWithValue("@Nombre", curso.Name);
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
                datos.SetearConsulta("update SORIA_TPC.dbo.CURSOS Set NOMBRE=@Nombre Where ID=@IdCurso");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@Nombre", curso.Name);
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

        public Curso GetCursoWithName(Curso curso)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("SELECT ID, NOMBRE FROM SORIA_TPC.dbo.CURSOS WHERE NOMBRE=@NAME");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@NAME", curso.Name);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                Curso aux = new Curso();
                if (datos.Reader.Read())
                {
                    aux = new Curso
                    {
                        ID = (Int64)datos.Reader[0],
                        Name = (string)datos.Reader[1]
                    };
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
                datos.SetearConsulta("SELECT ID, NOMBRE FROM SORIA_TPC.dbo.CURSOS WHERE ID=@ID");
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
                        Name = (string)datos.Reader[1]
                    };
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
        public List<Curso> GetMyCursoWithEstablecimiento(List<Curso> lista, Int64 IDEstablecimiento, Int64 IDDocente)
        {
            Datos datos = new Datos();
            Curso curso;
            try
            {
                datos.SetearConsulta("SELECT C.ID , C.NOMBRE FROM SORIA_TPC.dbo.CURSOSxESTABLECIMIENTO AS CXE"
                    +" INNER JOIN SORIA_TPC.dbo.ESTABLECIMIENTOS AS E ON E.ID = CXE.IDESTABLECIMIENTO"
                    +" INNER JOIN SORIA_TPC.dbo.CURSOS AS C ON C.ID = CXE.IDCURSO"
                    +" WHERE CXE.IDESTABLECIMIENTO=@IDEstablecimiento AND CXE.IDDOCENTE=@IDDocente");
                datos.Comando.Parameters.Clear();
                datos.Comando.Parameters.AddWithValue("@IDEstablecimiento", IDEstablecimiento);
                datos.Comando.Parameters.AddWithValue("@IDDocente", IDDocente);
                datos.AbrirConexion();
                datos.EjecutarConsulta();
                while (datos.Reader.Read())
                {
                    curso = new Curso
                    {
                        ID = (Int64)datos.Reader[0],
                        Name = (string)datos.Reader[1]
                    };
                    lista.Add(curso);
                }
                return lista;
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
