using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TRPO_DM.Interfaces;
using TRPO_DM.Models;
using TRPO_DM.ViewModels;

namespace TRPO_DA.DataAccess
{
    public class ElementDataAccess
    {
        private DataContext dataContext { get; }
        private IMapper mapper { get; }

        public ElementDataAccess(DataContext context, IMapper mapper)
        {
            dataContext = context;
            this.mapper = mapper;
        }

        // CRUD

        public async Task<ElementVM> CreateAsync(IElementData elementData)
        {
            var created = await dataContext.AddAsync(mapper.Map<Element>(elementData));

            try
            {
                await dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                Debug.WriteLine("Creating failed: " + exception.InnerException.Message);
            }


            var result = await GetAsync(created.Entity.ID);
            var resultViewModel = mapper.Map<ElementVM>(result);

            return resultViewModel;
        }

        public async Task<List<ElementVM>> GetAsync()
        {
            var result = await dataContext.Elements.Include(e => e.Category).ToListAsync();
            var resultViewModel = mapper.Map<List<ElementVM>>(result);

            return resultViewModel;
        }

        public async Task<ElementVM> GetAsync(int id)
        {
            var result = await dataContext.Elements.Include(e => e.Category).FirstOrDefaultAsync(e => e.ID == id);

            if (result == null)
            {
                throw new Exception($"Element with ID {id} doesn't exist");
            }

            var resultViewModel = mapper.Map<ElementVM>(result);

            return resultViewModel;
        }

        public async Task<ElementVM> UpdateAsync(IElementData elementData)
        {
            Element? result = await dataContext.Elements.Include(e => e.Category).FirstOrDefaultAsync(e => e.ID == elementData.ID);

            if (result == null)
            {
                throw new Exception($"Element with ID {elementData.ID} doesn't exist");
            }

            result.Name = elementData.Name;
            result.Data = elementData.Data;
            result.ImageURI = elementData.ImageURI;
            result.CategoryID = elementData.CategoryID;
            
            dataContext.Update(result);

            await dataContext.SaveChangesAsync();

            var resultViewModel = mapper.Map<ElementVM>(result);
            return resultViewModel;
        }

        public async Task<ElementVM> DeleteAsync(int id)
        {
            Element? toBeDeleted = await dataContext.Elements.Include(e => e.Category).FirstOrDefaultAsync(e => e.ID == id);

            if (toBeDeleted == null)
            {
                throw new Exception($"Element with ID {id} doesn't exist");
            }
            
            var deleted = dataContext.Elements.Remove(toBeDeleted);

            await dataContext.SaveChangesAsync();

            var resultViewModel = mapper.Map<ElementVM>(deleted.Entity);
            return resultViewModel;
        }
    }
}
