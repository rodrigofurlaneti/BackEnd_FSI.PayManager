using FSI.PayManager.Application.Dtos;

namespace FSI.PayManager.Application.Interfaces
{
    public interface IAuthAppService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken ct = default);
    }
}
