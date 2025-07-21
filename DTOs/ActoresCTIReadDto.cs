namespace Inteia.Api.DTOs
{
    public class ActoresCTIReadDto
    {
        public string? Id { get; set; }
        public string? NombreEntidad { get; set; }
        public string? NombreActor { get; set; }
        public string? ReconocidoComo { get; set; }
        public string? CiudadDepartamento { get; set; }
        public string? PaginaWeb { get; set; }
        public string? Sector { get; set; }
        public string? Resolucion { get; set; }
        public DateTime? FechaExpedicion { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public DateTime? VigenciaHasta { get; set; }
        public string? UbicacionId { get; set; }
        public string? GrupoInvestigacionId { get; set; } // Relación con GrupoInvestigacion
    }
}
