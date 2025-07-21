using Inteia.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inteia.Api.Interfaces
{
    public interface IActoresCTIService
    {
        Task<ActoresCTIReadDto?> CreateAsync(ActoresCTICreateDto dto);
        Task<List<ActoresCTIReadDto>> GetAllAsync();
        Task<ActoresCTIReadDto?> GetByIdAsync(string id);
        Task<bool> UpdateAsync(string id, ActoresCTICreateDto dto);
        Task<bool> DeleteAsync(string id);
    }
}
