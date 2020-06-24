using Experis.UI.ConsultaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experis.Data.Consulta
{
    public class SalonConsulta : ISalonConsultaDTO
    {
        public int IdSalon { get ; set ; }
        public string Nombre { get ; set ; }
        public string NombreMateria { get; set; }
        public int? IdMateria { get ; set ; }
        public bool? Estado { get ; set ; }
        public DateTime? FechaRegistro { get ; set ; }
    }
}
