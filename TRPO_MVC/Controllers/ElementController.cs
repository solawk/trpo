using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TRPO_MVC.Services;
using TRPO_MVC.Models.Out;

namespace TRPO_MVC.Controllers
{
    public class ElementController : Controller
    {
        private ElementService elementService;

        public ElementController(ElementService service)
        {
            elementService = service;
        }
        public async Task<IActionResult> List()
        {
            return View(await elementService.GetAllElements());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, int catid, string data, string image)
        {
            ElementOutModel elementData = new ElementOutModel();

            if (image == null) image = "";

            elementData.Name = name;
            elementData.CategoryID = catid;
            elementData.Data = data;
            elementData.ImageURI = image;

            await elementService.CreateElement(elementData);

            return View("List", await elementService.GetAllElements());
        }

        public async Task<IActionResult> Read(int id)
        {
            return View("Read", await elementService.ReadElement(id));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await elementService.ReadElement(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string name, int catid, string data, string image)
        {
            ElementOutModel elementData = new ElementOutModel();

            if (image == null) image = "";

            elementData.ID = id;
            elementData.Name = name;
            elementData.CategoryID = catid;
            elementData.Data = data;
            elementData.ImageURI = image;

            await elementService.UpdateElement(elementData);

            return View("List", await elementService.GetAllElements());
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            return View("Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await elementService.DeleteElement(id);

            return View("List", await elementService.GetAllElements());
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View("Search");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string? name, int? catid, string? data)
        {
            var searchResult = await elementService.SearchElements(name, catid, data);

            return View("List", searchResult);
        }
    }
}
