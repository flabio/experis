using Experis.UI.ConsultaDTO;
using System;

namespace Experis.Data.Consulta
{
    public class NotaConsulta : INotaConsultaDTO
    {
        public int IdNota { get ; set ; }
        public double? PrimerNota { get ; set ; }
        public double? SegundoNota { get ; set ; }
        public double? TerceNota { get ; set ; }
        public double? Promedio { get ; set ; }
        public bool? Estado { get ; set ; }
        public DateTime? FechaRegistro { get ; set ; }
        public int? IdAlumno { get ; set ; }
        public int? IdMateria { get ; set ; }
        public int? IdProfesor { get; set; }

        public string    NombreAlumno { get; set; }
        public string    NombreMateria { get; set; }
        public string    NombreProfesor { get; set; }
    }
}
