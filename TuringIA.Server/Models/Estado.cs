using System;
using System.Collections.Generic;

namespace TuringIA.Server.Models;

public partial class Estado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Abreviacion { get; set; } = null!;

    public int Fips { get; set; }

    public virtual ICollection<CasosDiario> CasosDiarios { get; set; } = new List<CasosDiario>();

    public virtual ICollection<Condado> Condados { get; set; } = new List<Condado>();

    public virtual ICollection<Hospitalizacione> Hospitalizaciones { get; set; } = new List<Hospitalizacione>();
}
