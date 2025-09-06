using Core.Interfaces;
using Core.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace WebATBApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController(ICategoriesService categoriesService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var model = await categoriesService.GetAllAsync();

            return Ok(model);
        }

        [HttpPost]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([FromForm] CategoryCreateModel model)
        {
            var result = await categoriesService.CreateAsync(model);

            return Ok(result);
        }

        [HttpPut]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update([FromForm] CategoryEditModel model)
        {
            var result = await categoriesService.UpdateAsync(model);

            return Ok(result);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var result = await categoriesService.GetBySlugAsync(slug);

            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await categoriesService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(long id)
        {
            var category = await categoriesService.DeleteAsync(id);

            if (category == null)
                return NotFound(new { message = $"Категорію з Id {id} не знайдено" });

            return Ok(new { message = "Категорію видалено успішно", category });
        }
    }
}
