using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Portfolio
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Nombre { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime FechaApertura { get; set; }

    [DataType(DataType.Date)]
    public DateTime FechaCierre { get; set; }

    // Relación con Usuario
    public string UsuarioId { get; set; } = string.Empty;
    public Usuario Usuario { get; set; } = null!;

    public ICollection<Objetivo> Objetivos { get; set; } = new List<Objetivo>();
    public ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();
    public ICollection<Instrumento> Instrumentos { get; set; } = new List<Instrumento>();
}
