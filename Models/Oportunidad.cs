using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inteia.Api.Models
{
    public class Oportunidad : BaseEntity
    {
        public bool esPostulable { get; set; }

        public string idProceso { get; set; }

        public string descripcion { get; set; }

        public string fechaMaximaPostulacion { get; set; }

        public string departamento { get; set; }

        public List<string> aspectosImportantes { get; set; }

        public string urlConvocatoria { get; set; }
    }
}
