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
    public class MateriaDAL : IMateriaAccion
    {
        public List<IMateriaConsultaDTO> ListadoMateria()
        {
            List<IMateriaConsultaDTO> resultado = new List<IMateriaConsultaDTO>();
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                try
                {

                    resultado = (from a in experisDBEntities.Materia
                                 select new MateriaConsulta()
                                 {
                                     IdMateria = a.IdMateria,
                                     Nombre = a.Nombre,
                                     Codigo = a.Codigo,
                                     Estado = a.Estado,
                                     FechaRegistro = a.FechaRegistro
                                 }).ToList<IMateriaConsultaDTO>();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return resultado;
        }
        public bool RegistrarActualizarMateria(IMateriaDTO materiaDTO)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Materia materia = new Materia();
                        if (materiaDTO.IdMateria > 0)
                        {
                            materia = experisDBEntities.Materia.Find(materiaDTO.IdMateria);
                            materia = materiaDTO.MapearMateria<Materia>(materia);
                        }
                        else
                        {
                            materia = materiaDTO.MapearMateria<Materia>();
                            experisDBEntities.Materia.Add(materia);
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

        public bool RegistrarActualizarSemestreMateria(int IdMateria, int IdSemestre)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        SemestreMateria obj = new SemestreMateria();
                        
                        obj.IdMateria = IdMateria;
                        obj.IdSemestre = IdSemestre;
                        experisDBEntities.SemestreMateria.Add(obj);
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
        public bool EliminarMateria(int id)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Materia materia = experisDBEntities.Materia.Find(id);
                        experisDBEntities.Materia.Remove(materia);
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
