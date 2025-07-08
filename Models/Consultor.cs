using Inteia.Api.Core;

public class Consultor : BaseEntity
{
    public string Nombre { get; set; }
    public string? Especialidad { get; set; }
    public string? LinkedIn { get; set; }
    public string? Twitter { get; set; }
    public string? StartupUrl { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
}
