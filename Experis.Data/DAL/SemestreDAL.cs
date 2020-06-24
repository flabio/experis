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
    public class SemestreDAL : ISemestreAccion
    {
       
        public List<ISemestreConsultaDTO> ListadoSemestre()
        {
            List<ISemestreConsultaDTO> resultado = new List<ISemestreConsultaDTO>();
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                try
                {
                    resultado = (from a in experisDBEntities.Semestre
                                 select new SemestreConsulta()
                                 {
                                     IdSemestre = a.IdSemestre,
                                     Nombre = a.Nombre,
                                     Periodo = a.Periodo,
                                     Estado = a.Estado,
                                     FechaRegistro = a.FechaRegistro
                                 }).ToList<ISemestreConsultaDTO>();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return resultado;
        }

        public bool RegistrarActualizarSemestre(ISemestreDTO semestreDTO)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Semestre semestre = new Semestre();
                        if (semestreDTO.IdSemestre > 0)
                        {
                            semestre = experisDBEntities.Semestre.Find(semestreDTO.IdSemestre);
                            semestre = semestreDTO.MapearSemestre<Semestre>(semestre);
                        }
                        else
                        {
                            semestre = semestreDTO.MapearSemestre<Semestre>();
                            experisDBEntities.Semestre.Add(semestre);
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
        public bool EliminarSemestre(int id)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Semestre semestre = experisDBEntities.Semestre.Find(id);
                        experisDBEntities.Semestre.Remove(semestre);
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
