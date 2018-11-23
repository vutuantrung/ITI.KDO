using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Models.CategoryPresentViewModels;
using ITI.KDO.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class CategoryPresentController : Controller
    {
        readonly CategoryPresentServices _categoryPresentServices;
        
        public CategoryPresentController(CategoryPresentServices categoryPresentServices)
        {
            _categoryPresentServices = categoryPresentServices;
        }

        [HttpGet("getCategoryPresents")]
        public IActionResult GetCategoryPresentList()
        {
            Result<IEnumerable<CategoryPresent>> result = _categoryPresentServices.GetAll();
            return this.CreateResult<IEnumerable<CategoryPresent>, IEnumerable<CategoryPresentViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToCategoryPresentViewModel());
            });
        }
    }
}
