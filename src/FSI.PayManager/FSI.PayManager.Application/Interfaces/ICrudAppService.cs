using FSI.PayManager.Application.Dtos;

namespace FSI.PayManager.Application.Interfaces
{
    public interface ICrudAppService<TDto> where TDto : IHasId
    {
        Task<List<TDto>> GetAllAsync(CancellationToken ct = default);
        Task<TDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<TDto> CreateAsync(TDto dto, CancellationToken ct = default);
        Task<TDto?> UpdateAsync(int id, TDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}