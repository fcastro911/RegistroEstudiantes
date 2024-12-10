namespace RegistroEstudiantes.Modelos
{
    public class Estudiante
    {
        public string? NombreCompleto { get; set; }
        public string? CorreoElectronico { get; set; }
        public int Edad { get; set; }
        public string? Curso { get; set; }
        public bool Activo { get; set; }
    }
}
