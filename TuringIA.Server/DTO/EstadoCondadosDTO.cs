namespace TuringIA.Server.DTO
{

    public class EstadoCondadosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Abreviacion { get; set; } = null!;
        public List<CondadoDTO> Condados { get; set; } = new List<CondadoDTO>();
    }
}
