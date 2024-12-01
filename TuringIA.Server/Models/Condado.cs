using System;
using System.Collections.Generic;

namespace TuringIA.Server.Models;

public partial class Condado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int EstadoId { get; set; }

    public int Fips { get; set; }

    public virtual ICollection<CasosDiario> CasosDiarios { get; set; } = new List<CasosDiario>();

    public virtual Estado Estado { get; set; } = null!;
}
