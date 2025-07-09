using System.ComponentModel.DataAnnotations;

public class TipoDeApoyo
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;
}