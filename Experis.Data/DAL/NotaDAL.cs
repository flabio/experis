using Experis.Data.Consulta;
using Experis.Data.models;
using Experis.UI.Accion;
using Experis.UI.ConsultaDTO;
using Experis.UI.DTO;
using Experis.UI.Mapear;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Experis.Data.DAL
{
    public class NotaDAL : INotaAccion
    {

        public List<INotaConsultaDTO> ListadoNota(int IdAlumno,int IdMateria)
        {
            List<INotaConsultaDTO> resultado = new List<INotaConsultaDTO>();
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                try
                {
                    resultado = (from a in experisDBEntities.Nota.AsEnumerable()
                                 where a.IdAlumno == IdAlumno && a.IdMateria == IdMateria
                                 select new NotaConsulta()
                                 {
                                     IdNota = a.IdNota,
                                     PrimerNota=a.PrimerNota,
                                     SegundoNota=a.SegundoNota,
                                     TerceNota=a.TerceNota,
                                     Promedio=a.Promedio,
                                     NombreAlumno =a.Alumno.Nombre+" "+ a.Alumno.Apellidos,
                                     NombreMateria =a.Materia.Nombre,
                                     NombreProfesor =a.Profesor.Nombre + " " + a.Profesor.Apellidos,
                                     Estado = a.Estado,
                                     FechaRegistro = a.FechaRegistro,
                                     IdProfesor =a.IdProfesor,
                                     IdMateria =a.IdMateria
                                 }).ToList<INotaConsultaDTO>();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return resultado;
        }

        public bool RegistrarActualizarNota(INotaDTO notaDTO)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Nota nota = new Nota();
                        if (notaDTO.IdNota > 0)
                        {
                            nota = experisDBEntities.Nota.Find(notaDTO.IdNota);
                            nota = notaDTO.MapearNota<Nota>(nota);
                    
                        }
                        else
                        {
                            nota = notaDTO.MapearNota<Nota>();
                            experisDBEntities.Nota.Add(nota);
                        }
                            experisDBEntities.SaveChanges();
                        transacion.Commit();
                        resultado = true;
                    }
                    catch (Exception e)
                    {
                        transacion.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }
            return resultado;
        }
        public bool EliminarNota(int id)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Alumno alumno = experisDBEntities.Alumno.Find(id);
                        experisDBEntities.Alumno.Remove(alumno);
                        experisDBEntities.SaveChanges();
                        transacion.Commit();
                        resultado = true;
                    }
                    catch (Exception e)
                    {
                        transacion.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }
            return resultado;
        }
    }
}
