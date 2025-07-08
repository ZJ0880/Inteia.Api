using Inteia.Api.Core;
using System.Collections.Generic;

public class ComunidadTecNivel2 : BaseEntity
{
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public Guid ComunidadTecNivel1Id { get; set; }
    public ComunidadTecNivel1? Padre { get; set; }
    public ICollection<ComunidadTecNivel3> Hijos { get; set; }
}
