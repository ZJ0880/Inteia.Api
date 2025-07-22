using System.ComponentModel.DataAnnotations;

public class ObjPorcentaje
{
    public int Id { get; set; }

    [Range(0, 100)]
    public decimal Porcentaje { get; set; }

    public int ObjetivoId { get; set; }
    public Objetivo Objetivo { get; set; } = null!;
}