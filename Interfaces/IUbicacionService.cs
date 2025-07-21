using Inteia.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inteia.Api.Interfaces
{
    public interface IUbicacionService
    {
        Task<UbicacionReadDto?> CreateAsync(UbicacionCreateDto dto);
        Task<List<UbicacionReadDto>> GetAllAsync();
        Task<UbicacionReadDto?> GetByIdAsync(string id);
        Task<bool> UpdateAsync(string id, UbicacionCreateDto dto);
        Task<bool> DeleteAsync(string id);
    }
}
