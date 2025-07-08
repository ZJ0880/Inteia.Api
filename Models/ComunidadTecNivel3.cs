using Inteia.Api.Core;

public class ComunidadTecNivel3 : BaseEntity
{
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public Guid ComunidadTecNivel2Id { get; set; }
    public ComunidadTecNivel2? Padre { get; set; }
}
