namespace TuringIA.Server.DTO
{

    public class HospitalizacionDTO
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public int EstadoId { get; set; }
        public int HospitalizadosActualmente { get; set; }
        public int EnUciActualmente { get; set; }
        public int EnVentiladorActualmente { get; set; }
        public int HospitalizacionesAcumuladas { get; set; }
    }
}
