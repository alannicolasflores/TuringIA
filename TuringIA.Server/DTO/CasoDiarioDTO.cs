namespace TuringIA.Server.DTO
{
    public class CasoDiarioDTO
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public int EstadoId { get; set; }
        public int? CondadoId { get; set; }
        public int Casos { get; set; }
        public int Muertes { get; set; }
    }
}
