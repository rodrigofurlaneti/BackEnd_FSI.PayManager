using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FSI.PayManager.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class RecurringTransactionsController : BaseCrudController<RecurringTransactionDto>
    {
        public RecurringTransactionsController(ICrudAppService<RecurringTransactionDto> service)
            : base(service)
        {
        }
    }
}
