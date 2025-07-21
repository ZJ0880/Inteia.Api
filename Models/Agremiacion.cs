using Inteia.Api.Core;
using Inteia.Api.Models;


public class Agremiacion : BaseEntity
{
    public string Nombre { get; set; }
    public string Sigla { get; set; }
    public string RazonSocial { get; set; }
    public TipoVinculador TipoVinculador { get; set; } // Enum para relación con Vinculador
}
