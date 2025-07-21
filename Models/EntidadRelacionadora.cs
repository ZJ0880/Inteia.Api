// Models/EntidadRelacionadora.cs
using Inteia.Api.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Inteia.Api.Core;


namespace Inteia.Api.Models;

public class EntidadRelacionadora : BaseEntity
{
    public TipoEntidad Tipo { get; set; }
    public string Nombre { get; set; } = default!;
    public string Ciudad { get; set; } = default!;
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
    public string Web { get; set; }
}
