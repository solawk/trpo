using TRPO_DA.DataAccess;
using TRPO_DM.Interfaces;
using TRPO_DM.ViewModels;

namespace TRPO_BL.BusinessLogic
{
    public class CategoryBL
    {
        private CategoryDataAccess dataAccess { get; }

        public CategoryBL(CategoryDataAccess categoryDataAccess)
        {
            dataAccess = categoryDataAccess;
        }

        public Task<CategoryVM> Create(ICategoryData category)
        {
            category.ID = 0;

            return dataAccess.CreateAsync(category);
        }

        public Task<List<CategoryVM>> Read()
        {
            return dataAccess.GetAsync();
        }

        public Task<CategoryVM> Read(int id)
        {
            try
            {
                return dataAccess.GetAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<CategoryVM> Update(ICategoryData category)
        {
            return dataAccess.UpdateAsync(category);
        }

        public Task<CategoryVM> Delete(int id)
        {
            return dataAccess.DeleteAsync(id);
        }
    }
}
