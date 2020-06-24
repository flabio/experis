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
    public class AlumnoDAL : IAlumnoAccion
    {
        public List<IAlumnoConsultaDTO> ListadoAlumno()
        {
            List<IAlumnoConsultaDTO> resultado = new List<IAlumnoConsultaDTO>();
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                try
                {

                    resultado = (from a in experisDBEntities.Alumno.AsEnumerable()
                                 select new AlumnoConsulta()
                                 {
                                     IdAlumono = a.IdAlumono,
                                     Nombre = a.Nombre,
                                     Apellidos = a.Apellidos,
                                     Identificacion = a.Identificacion,
                                     TipoIdentificacin = a.TipoIdentificacin,
                                     Estado = a.Estado,
                                     FechaRegistro = a.FechaRegistro.Value.ToString()
                                 }).ToList<IAlumnoConsultaDTO>();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return resultado;
        }

        public List<IAlumnoConsultaDTO> ListadoAlumnoSemestre()
        {
            List<IAlumnoConsultaDTO> resultado = new List<IAlumnoConsultaDTO>();
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                try
                {

                    resultado = (from a in experisDBEntities.AlumonSemestre.AsEnumerable()
                                 join sm in experisDBEntities.SemestreMateria on a.IdSemestre equals sm.IdSemestre
                                 select new AlumnoConsulta()
                                 {
                                     IdAlumono = a.IdAlumono,
                                     IdMateria = sm.IdMateria,
                                     Nombre = a.Alumno.Nombre,
                                     Apellidos = a.Alumno.Apellidos,
                                     Identificacion = a.Alumno.Identificacion,
                                     TipoIdentificacin = a.Alumno.TipoIdentificacin,
                                     Estado = a.Alumno.Estado,
                                     FechaRegistro = a.Alumno.FechaRegistro.Value.ToString("yyyy/MM/dd"),
                                     NombreSemestre=a.Semestre.Nombre,
                                     Periodo = a.Semestre.Periodo,
                                     NombreMateria =sm.Materia.Nombre,
                                   
                                     
                                 }).ToList<IAlumnoConsultaDTO>();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return resultado;
        }
        public bool RegistrarActualizarAlumon(IAlumnoDTO alumnoDTO)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Alumno alumno = new Alumno();
                        if (alumnoDTO.IdAlumono > 0)
                        {
                            alumno = experisDBEntities.Alumno.Find(alumnoDTO.IdAlumono);
                            alumno = alumnoDTO.MapearAlumno<Alumno>(alumno);
                        }
                        else
                        {
                            alumno = alumnoDTO.MapearAlumno<Alumno>();
                            experisDBEntities.Alumno.Add(alumno);
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

        public bool RegistrarActualizarAlumonSemestre(IAlumnoSemestreDTO alumnoDTO)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        AlumonSemestre ase = new AlumonSemestre();
                        ase.IdAlumono = alumnoDTO.IdAlumono;
                        ase.IdSemestre = alumnoDTO.IdSemestre;
                        experisDBEntities.AlumonSemestre.Add(ase);
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
        public bool EliminarAlumno(int id)
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
