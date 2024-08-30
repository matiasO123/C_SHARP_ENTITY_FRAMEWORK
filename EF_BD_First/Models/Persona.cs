using System;
using System.Collections.Generic;

namespace EF_BD_First.Models;

public partial class Persona
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Edad { get; set; }

    public int DepartamentoId { get; set; }

    public string? Direccion { get; set; }

    public virtual Departamento Departamento { get; set; } = null!;
}
