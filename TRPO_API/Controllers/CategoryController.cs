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
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        public CategoryBL categoryBL { get; }

        public CategoryController(CategoryBL bl)
        {
            categoryBL = bl;
        }

        [HttpPut]
        [Route("")]
        public async Task<CategoryVM> Create(CategoryDM category)
        {
            CategoryVM newCategory = await categoryBL.Create(category);

            return newCategory;
        }

        [HttpGet]
        [Route("")]
        public async Task<List<CategoryVM>> GetAll()
        {
            Debug.WriteLine("Get all categories invoked");

            List<CategoryVM> categories = await categoryBL.Read();

            return categories;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetById(int id)
        {
            CategoryVM category;

            try
            {
                category = await categoryBL.Read(id);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }
            

            return category;
        }

        [HttpPatch]
        [Route("")]
        public async Task<object> Update(CategoryDM category)
        {
            CategoryVM updatedCategory;

            Debug.WriteLine(category.ID);
            Debug.WriteLine(category.Name);
            Debug.WriteLine(category.ParentID);

            try
            {
                updatedCategory = await categoryBL.Update(category);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }

            return updatedCategory;
        }

        [HttpDelete]
        [Route("")]
        public async Task<object> Delete(int id)
        {
            CategoryVM deletedCategory;

            try
            {
                deletedCategory = await categoryBL.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }

            return deletedCategory;
        }
    }
}
