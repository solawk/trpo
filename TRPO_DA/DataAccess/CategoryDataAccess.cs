using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TRPO_DM.Interfaces;
using TRPO_DM.Models;
using TRPO_DM.ViewModels;

namespace TRPO_DA.DataAccess
{
    public class CategoryDataAccess
    {
        private DataContext dataContext { get; }
        private IMapper mapper { get; }

        public CategoryDataAccess(DataContext context, IMapper mapper)
        {
            dataContext = context;
            this.mapper = mapper;
        }

        // Basic

        private async Task<Category?> GetEntry(int id)
        {
            return await dataContext.Categories.FirstOrDefaultAsync(c => c.ID == id);
        }

        // CRUD

        public async Task<CategoryVM> CreateAsync(ICategoryData categoryData)
        {
            var created = await dataContext.AddAsync(mapper.Map<Category>(categoryData));

            try
            {
                await dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                Debug.WriteLine("Creating failed: " + exception.InnerException.Message);
            }

            var result = await GetAsync(created.Entity.ID);
            var resultViewModel = mapper.Map<CategoryVM>(result);

            return resultViewModel;
        }

        public async Task<List<CategoryVM>> GetAsync()
        {
            var result = await dataContext.Categories.ToListAsync();
            var resultViewModel = mapper.Map<List<CategoryVM>>(result);

            return resultViewModel;
        }

        public async Task<CategoryVM> GetAsync(int id)
        {
            var result = await GetEntry(id);

            if (result == null)
            {
                throw new Exception($"Element with ID {id} doesn't exist");
            }

            var resultViewModel = mapper.Map<CategoryVM>(result);

            return resultViewModel;
        }

        public async Task<CategoryVM> UpdateAsync(ICategoryData categoryData)
        {
            Category? result = await GetEntry(categoryData.ID);

            if (result == null)
            {
                throw new Exception($"Category with ID {categoryData.ID} doesn't exist");
            }

            result.Name = categoryData.Name;
            result.ParentID = categoryData.ParentID;
            
            dataContext.Update(result);

            await dataContext.SaveChangesAsync();

            var resultViewModel = mapper.Map<CategoryVM>(result);
            return resultViewModel;
        }

        public async Task<CategoryVM> DeleteAsync(int id)
        {
            Category? toBeDeleted = await GetEntry(id);

            if (toBeDeleted == null)
            {
                throw new Exception($"Category with ID {id} doesn't exist");
            }

            await ProcessChildrenOfCategory(id, ChildrenProcessAction.Unparent);
            
            var deleted = dataContext.Categories.Remove(toBeDeleted);

            await dataContext.SaveChangesAsync();

            var resultViewModel = mapper.Map<CategoryVM>(deleted.Entity);
            return resultViewModel;
        }

        // Advanced

        private async Task<List<Category>> GetChildrenOfCategoryEntries(int id)
        {
            return await dataContext.Categories.Where(c => c.ParentID == id).ToListAsync();
        }

        public async Task<List<CategoryVM>> GetChildrenOfCategoryAsync(int id)
        {
            var result = await GetChildrenOfCategoryEntries(id);
            var resultViewModel = mapper.Map<List<CategoryVM>>(result);

            return resultViewModel;
        }

        public enum ChildrenProcessAction
        {
            Unparent,
            Delete
        };

        public async Task ProcessChildrenOfCategory(int id, ChildrenProcessAction action)
        {
            var children = await GetChildrenOfCategoryEntries(id);

            foreach (var child in children)
            {
                if (action == ChildrenProcessAction.Unparent)
                {
                    child.ParentID = null;
                    dataContext.Categories.Update(child);
                }
                else
                {
                    await ProcessChildrenOfCategory(child.ID, ChildrenProcessAction.Delete);
                    dataContext.Categories.Remove(child);
                }
            }

            await dataContext.SaveChangesAsync();
        }
    }
}
