using Experis.UI.ConsultaDTO;
using System;

namespace Experis.Data.Consulta
{
    public class MateriaConsulta : IMateriaConsultaDTO
    {
        public int IdMateria { get ; set ; }
        public string Nombre { get ; set ; }
        public string Codigo { get ; set ; }
        public bool? Estado { get ; set ; }
        public DateTime? FechaRegistro { get ; set ; }
    }
}
