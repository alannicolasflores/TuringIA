using System;
using System.Collections.Generic;

namespace TuringIA.Server.Models;

public partial class Hospitalizacione
{
    public int Id { get; set; }

    public DateOnly Fecha { get; set; }

    public int EstadoId { get; set; }

    public int EnUciActualmente { get; set; }

    public int EnVentiladorActualmente { get; set; }

    public int HospitalizadosActualmente { get; set; }

    public int HospitalizacionesAcumuladas { get; set; }

    public virtual Estado Estado { get; set; } = null!;
}
