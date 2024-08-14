namespace FormularioASPNET.Models
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string? NombreAlumno { get; set;}
        public int? Documento { get; set; }
        public DateOnly? Nacimiento { get; set; }
        public bool Activo { get; set; }
        public ICollection<Asistencia> Asistencias { get; set; }

    }
}
