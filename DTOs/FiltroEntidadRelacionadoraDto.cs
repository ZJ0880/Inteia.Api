using Inteia.Api.Models;
using System.ComponentModel.DataAnnotations;
using Inteia.Api.Models;
using Inteia.Api.Models.Enums;


public class FiltroEntidadRelacionadoraDto
{
    public TipoEntidad? Tipo { get; set; }
    public string Ciudad { get; set; }
    public string NombreParcial { get; set; }
}
