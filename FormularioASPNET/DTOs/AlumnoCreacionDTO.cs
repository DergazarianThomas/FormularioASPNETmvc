namespace FormularioASPNET.DTOs
{
    public class AlumnoCreacionDTO
    {
        public string? NombreAlumno { get; set; }
        public int? Documento { get; set; }
        public DateOnly? Nacimiento { get; set; }
        public bool Activo { get; set; }
    }
}
