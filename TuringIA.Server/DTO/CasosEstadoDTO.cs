namespace TuringIA.Server.DTO
{
    public class CasosEstadoDTO
    {
        public int EstadoId { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public int TotalCasos { get; set; }
        public int TotalMuertes { get; set; }
    }
}
