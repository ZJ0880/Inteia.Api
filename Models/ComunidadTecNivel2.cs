using Inteia.Api.Core;
using System.Collections.Generic;

public class ComunidadTecNivel2 : BaseEntity
{
    public ComunidadTecNivel2()
    {
        Startups = new List<ComunidadTecNivel3>();
    }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Link { get; set; }
    public string Pais { get; set; }
    public string Industria { get; set; }

    // FK a Nivel 1
    public string ComunidadId { get; set; }
    public ComunidadTecNivel1? Comunidad { get; set; }

    // Relación 1 a N con Nivel 3
    public ICollection<ComunidadTecNivel3> Startups { get; set; }
}
