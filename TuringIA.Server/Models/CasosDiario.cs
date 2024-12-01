using System;
using System.Collections.Generic;

namespace TuringIA.Server.Models;

public partial class CasosDiario
{
    public int Id { get; set; }

    public DateOnly Fecha { get; set; }

    public int EstadoId { get; set; }

    public int? CondadoId { get; set; }

    public int Casos { get; set; }

    public int Muertes { get; set; }

    public virtual Condado? Condado { get; set; }

    public virtual Estado Estado { get; set; } = null!;
}
