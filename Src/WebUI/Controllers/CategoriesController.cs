using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Categories.Commands.DeleteCategory;
using Northwind.Application.Categories.Commands.UpsertCategory;
using Northwind.Application.Categories.Queries.GetCategoriesList;
using Northwind.Application.Categories.Queries.GetCategory;
using System.Threading.Tasks;

namespace Northwind.WebUI.Controllers {
    //[Authorize]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CategoriesListVm>> Index() {
            return View(await Mediator.Send(new GetCategoriesListQuery()));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<CategoriesListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCategoriesListQuery()));
        }

        [HttpGet]
        public async Task<IActionResult> UpsertAsync(long? id) {
            if (id == null) {
                return View();
            }
            var category = await Mediator.Send(new GetCategoryQuery { Id = id.Value });
            var categoryCommand = Mapper.Map<UpsertCategoryCommand>(category);
            return View(categoryCommand);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertCategoryCommand command) {
            if (ModelState.IsValid) {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            } else {
                return View(command);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
