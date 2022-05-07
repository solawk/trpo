using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TRPO_MVC.Services;
using TRPO_MVC.Models.Out;

namespace TRPO_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;

        public CategoryController(CategoryService service)
        {
            categoryService = service;
        }
        public async Task<IActionResult> List()
        {
            return View(await categoryService.GetAllCategories());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, int? parid)
        {
            CategoryOutModel categoryData = new CategoryOutModel();

            categoryData.Name = name;
            categoryData.ParentID = parid;

            await categoryService.CreateCategory(categoryData);

            return View("List", await categoryService.GetAllCategories());
        }

        public async Task<IActionResult> Read(int id)
        {
            return View("Read", await categoryService.ReadCategory(id));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await categoryService.ReadCategory(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string name, int? parid)
        {
            CategoryOutModel categoryData = new CategoryOutModel();

            categoryData.ID = id;
            categoryData.Name = name;
            categoryData.ParentID = parid;

            await categoryService.UpdateCategory(categoryData);

            return View("List", await categoryService.GetAllCategories());
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            return View("Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.DeleteCategory(id);

            return View("List", await categoryService.GetAllCategories());
        }
    }
}
