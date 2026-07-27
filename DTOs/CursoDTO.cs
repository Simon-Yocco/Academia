namespace DTOs
{
    public class CursoDTO
    {
        public int ID { get; set; }
        public int AnioCalendario { get; set; }
        public int Cupo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int IDcomision { get; set; }
        public int IDmateria { get; set; }
    }
}
