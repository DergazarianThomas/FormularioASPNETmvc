namespace FormularioASPNET.Models
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int IdAlumno { get; set; }
        public DateTime Date {  get; set; }
        public bool Presente { get; set; }

        public Alumno Alumno { get; set; }

    }
}
