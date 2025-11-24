using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FSI.PayManager.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class CategoriesController : BaseCrudController<CategoryDto>
    {
        public CategoriesController(ICrudAppService<CategoryDto> service)
            : base(service)
        {
        }
    }
}
