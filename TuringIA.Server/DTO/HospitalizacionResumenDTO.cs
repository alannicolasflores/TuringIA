namespace TuringIA.Server.DTO
{
    public class HospitalizacionResumenDTO
    {
        public int EstadoId { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public int PromedioHospitalizados { get; set; }
        public int PromedioEnUci { get; set; }
        public int PromedioEnVentilador { get; set; }
    }

}
