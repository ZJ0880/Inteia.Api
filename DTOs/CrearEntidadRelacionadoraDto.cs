using Inteia.Api.Models;
using System.ComponentModel.DataAnnotations;
using Inteia.Api.Models.Enums;


public class CrearEntidadRelacionadoraDto
{
    [Required]
    public TipoEntidad Tipo { get; set; }

    [Required]
    [MaxLength(150)]
    public string Nombre { get; set; }

    public string Ciudad { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
    public string Web { get; set; }
}
