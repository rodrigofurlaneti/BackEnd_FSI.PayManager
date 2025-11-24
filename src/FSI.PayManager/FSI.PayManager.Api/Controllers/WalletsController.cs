using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FSI.PayManager.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class WalletsController : BaseCrudController<WalletDto>
    {
        public WalletsController(ICrudAppService<WalletDto> service)
            : base(service)
        {
        }
    }
}
