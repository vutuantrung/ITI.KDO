using ITI.KDO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class CategoryPresentServices
    {
        readonly CategoryPresentGateway _categoryPresentGateway;

        public CategoryPresentServices(CategoryPresentGateway categoryPresentGateway)
        {
            _categoryPresentGateway = categoryPresentGateway;
        }

        public Result<IEnumerable<CategoryPresent>> GetAll()
        {
            return Result.Success(Status.Ok, _categoryPresentGateway.GetAll());
        }
    }
}
