using TRPO_DA.DataAccess;
using TRPO_DM.Interfaces;
using TRPO_DM.Models;
using TRPO_DM.ViewModels;

namespace TRPO_BL.BusinessLogic
{
    public class ElementBL
    {
        private ElementDataAccess dataAccess { get; }

        public ElementBL(ElementDataAccess elementDataAccess)
        {
            dataAccess = elementDataAccess;
        }

        public Task<ElementVM> Create(IElementData element)
        {
            element.ID = 0;
            return dataAccess.CreateAsync(element);
        }

        public Task<List<ElementVM>> Read()
        {
            return dataAccess.GetAsync();
        }

        public Task<ElementVM> Read(int id)
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

        public Task<ElementVM> Update(IElementData element)
        {
            return dataAccess.UpdateAsync(element);
        }

        public Task<ElementVM> Delete(int id)
        {
            return dataAccess.DeleteAsync(id);
        }

        // Advanced

        public Task<List<ElementVM>> Search(List<Filter> filters)
        {
            return dataAccess.Search(filters);
        }
    }
}
