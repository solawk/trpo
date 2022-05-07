using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TRPO_BL.BusinessLogic;
using TRPO_DM.ViewModels;
using TRPO_DM.Models;
using TRPO_DM.Interfaces;
using TRPO_API.DataModels;

namespace TRPO_API.Controllers
{
    [ApiController]
    [Route("api/elements")]
    public class ElementController : ControllerBase
    {
        public ElementBL elementBL { get; }

        public ElementController(ElementBL bl)
        {
            elementBL = bl;
        }

        [HttpPut]
        [Route("")]
        public async Task<ElementVM> Create(ElementDM element)
        {
            Console.WriteLine("Create invoked");

            ElementVM newElement = await elementBL.Create(element);

            return newElement;
        }

        [HttpGet]
        [Route("")]
        public async Task<List<ElementVM>> GetAll()
        {
            Console.WriteLine("Get all invoked");

            List<ElementVM> elements = await elementBL.Read();

            return elements;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetById(int id)
        {
            ElementVM element;

            try
            {
                element = await elementBL.Read(id);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }
            

            return element;
        }

        [HttpPatch]
        [Route("")]
        public async Task<object> Update(ElementDM element)
        {
            ElementVM updatedElement;

            try
            {
                updatedElement = await elementBL.Update(element);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }

            return updatedElement;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            ElementVM deletedElement;

            try
            {
                deletedElement = await elementBL.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }

            return deletedElement;
        }
    }
}
