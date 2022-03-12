using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TRPO_BL.BusinessLogic;
using TRPO_DM.Models;

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

        [HttpGet]
        public async Task<List<Element>> GetAll()
        {
            Debug.WriteLine("Get all invoked");

            List<Element> Elements = await elementBL.Read();

            return Elements;
        }
    }
}
