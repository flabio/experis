using Experis.UI.ConsultaDTO;
using System;

namespace Experis.Data.Consulta
{
    public class AlumnoConsulta : IAlumnoConsultaDTO
    {
        public int IdAlumono { get ; set ; }
        public int IdMateria { get; set; }
        public string Nombre { get ; set ; }
        public string Apellidos { get ; set ; }
        public string Identificacion { get ; set ; }
        public string TipoIdentificacin { get ; set ; }
        public string NombreSemestre { get; set; }
        public string NombreMateria { get; set; }
        public string Periodo { get; set; }
        public bool? Estado { get ; set ; }
        public string FechaRegistro { get ; set ; }
    }
}
