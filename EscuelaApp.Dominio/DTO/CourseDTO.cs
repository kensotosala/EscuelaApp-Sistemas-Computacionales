namespace EscuelaApp.Dominio.DTO
{
    public class CourseDTO
    {
        public int CourseID { get; set; }
        public string Title { get; set; } = null!;
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
    }
}