using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FSI.PayManager.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class RemindersController : BaseCrudController<ReminderDto>
    {
        public RemindersController(ICrudAppService<ReminderDto> service) : base(service)
        {
        }
    }
}
