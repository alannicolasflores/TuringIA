namespace TuringIA.Server.DTO
{
    public class EstadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Abreviacion { get; set; } = null!;
        public int Fips { get; set; }
    }
}
