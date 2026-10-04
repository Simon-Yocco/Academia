namespace DTOs {
    public class MateriaDTO {
        public int ID { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int HSSemanales { get; set; }
        public int HSTotales { get; set; }
        public int IDPlan { get; set; }
    }
}
