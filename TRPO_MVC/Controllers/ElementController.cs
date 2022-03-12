using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TRPO_MVC.Services;

namespace TRPO_MVC.Controllers
{
    public class ElementController : Controller
    {
        private ElementService elementService;

        public ElementController(ElementService service)
        {
            elementService = service;
        }

        // GET: ElementController
        public async Task<IActionResult> List()
        {
            return View(await elementService.GetAllElements());
        }

        // GET: ElementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ElementController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ElementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ElementController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ElementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ElementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ElementController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
