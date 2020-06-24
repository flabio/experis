using Experis.UI.ConsultaDTO;
using System;

namespace Experis.Data.Consulta
{
    public class ProfesorConsulta : IProfesorConsultaDTO
    {
        public int IdProfesor { get ; set ; }
        public string Nombre { get ; set ; }
        public string Apellidos { get ; set ; }
        public bool? Estado { get ; set ; }
        public DateTime? FechaRegistro { get ; set ; }
    }
}
