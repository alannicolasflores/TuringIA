namespace TuringIA.Server.DTO
{
    public class EstadoHospitalizacionesDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public List<HospitalizacionDTO> Hospitalizaciones { get; set; } = new List<HospitalizacionDTO>();
    }
}
