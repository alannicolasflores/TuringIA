namespace TuringIA.Server.DTO
{

    public class CondadoCasosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int EstadoId { get; set; }
        public List<CasoDiarioDTO> CasosDiarios { get; set; } = new List<CasoDiarioDTO>();
    }
}
