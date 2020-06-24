using Experis.UI.ConsultaDTO;
using System;

namespace Experis.Data.Consulta
{
    public class SemestreConsulta : ISemestreConsultaDTO
    {
        public int IdSemestre { get ; set ; }
        public string Nombre { get ; set ; }
        public string Periodo { get ; set ; }
        public bool? Estado { get ; set ; }
        public DateTime? FechaRegistro { get ; set ; }
    }
}
