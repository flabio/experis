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
    public class SalonDAL : ISalonAccion
    {
        public List<ISalonConsultaDTO> ListadoSalon()
        {
            List<ISalonConsultaDTO> resultado = new List<ISalonConsultaDTO>();
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                try
                {

                    resultado = (from a in experisDBEntities.Salon
                                 select new SalonConsulta()
                                 {
                                     IdSalon = a.IdSalon,
                                     Nombre = a.Nombre,
                                     Estado = a.Estado,
                                     IdMateria =a.IdMateria,
                                     NombreMateria =a.Materia.Nombre,
                                     FechaRegistro = a.FechaRegistro
                                 }).ToList<ISalonConsultaDTO>();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return resultado;
        }
        public bool RegistrarActualizarSalon(ISalonDTO salonDTO)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Salon salon = new Salon();
                        if (salonDTO.IdSalon > 0)
                        {
                            salon = experisDBEntities.Salon.Find(salonDTO.IdSalon);
                            salon = salonDTO.MapearSalon<Salon>(salon);
                        }
                        else
                        {
                            salon = salonDTO.MapearSalon<Salon>();
                            experisDBEntities.Salon.Add(salon);
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
        public bool EliminarSalon(int id)
        {
            bool resultado = false;
            using (ExperisDBEntities experisDBEntities = new ExperisDBEntities())
            {
                using (var transacion = experisDBEntities.Database.BeginTransaction())
                {
                    try
                    {
                        Salon salon = experisDBEntities.Salon.Find(id);
                        experisDBEntities.Salon.Remove(salon);
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
