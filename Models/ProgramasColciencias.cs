using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

    public class ProgramasColciencias : BaseEntity
    {
        public string NombreProgramaColciencias { get; set; }
        public string Codigo { get; set; }
    }
