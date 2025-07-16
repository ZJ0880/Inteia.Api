using System.ComponentModel.DataAnnotations;

public class Recurso
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;

    public int PortfolioId { get; set; }
    public Portfolio Portfolio { get; set; } = null!;
}