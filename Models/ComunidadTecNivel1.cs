using Inteia.Api.Core;
using System.Collections.Generic;

public class ComunidadTecNivel1 : BaseEntity
{
    public ComunidadTecNivel1()
    {
        Hijos = new List<ComunidadTecNivel2>();
    }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public ICollection<ComunidadTecNivel2> Hijos { get; set; }
}
