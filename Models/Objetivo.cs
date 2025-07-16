using System.ComponentModel.DataAnnotations;

public class Objetivo
{
    public int Id { get; set; }

    [Required]
    public string Descripcion { get; set; } = string.Empty;

    public int PortfolioId { get; set; }
    public Portfolio Portfolio { get; set; } = null!;

    public ObjPorcentaje? ObjPorcentaje { get; set; }
}