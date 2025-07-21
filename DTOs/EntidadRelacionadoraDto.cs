using Inteia.Api.Models;
using System.ComponentModel.DataAnnotations;
using Inteia.Api.Models.Enums;



namespace Inteia.Api.Dtos;

public class EntidadRelacionadoraDto
{
    public int Id { get; set; }
    public TipoEntidad Tipo { get; set; }
    public string Nombre { get; set; }
    public string Ciudad { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
    public string Web { get; set; }
}
