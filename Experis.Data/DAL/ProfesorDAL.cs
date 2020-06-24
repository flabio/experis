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
    public class ProfesorDAL : IProfesorAccion
    {
        public List<IProfesorConsultaDTO> ListadoProfesor()
        {
            List<IProfesorConsultaDTO> resultado = new List<IProfesorConsultaDTO>();
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                try
                {
                    resultado = (from a in experisDBEntities.Profesor
                                 select new ProfesorConsulta()
                                 {
                                     IdProfesor = a.IdProfesor,
                                     Nombre = a.Nombre,
                                     Apellidos = a.Apellidos,
                                     Estado = a.Estado,
                                     FechaRegistro = a.FechaRegistro
                                 }).ToList<IProfesorConsultaDTO>();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return resultado;
        }
        public bool RegistrarActualizarProfesor(IProfesorDTO profesorDTO)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Profesor obj = new Profesor();
                        if (profesorDTO.IdProfesor > 0)
                        {
                            obj = experisDBEntities.Profesor.Find(profesorDTO.IdProfesor);
                            obj = profesorDTO.MapearProfesor<Profesor>(obj);

                        }
                        else
                        {
                            obj = profesorDTO.MapearProfesor<Profesor>();
                            experisDBEntities.Profesor.Add(obj);
                          
                           
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
        public bool EliminarProfesor(int id)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Profesor obj = experisDBEntities.Profesor.Find(id);
                        experisDBEntities.Profesor.Remove(obj);
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
