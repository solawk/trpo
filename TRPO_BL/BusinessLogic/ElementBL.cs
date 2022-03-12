using TRPO_DA.DataAccess;
using TRPO_DM.Models;

namespace TRPO_BL.BusinessLogic
{
    public class ElementBL
    {
        private ElementDataAccess dataAccess { get; }

        public ElementBL(ElementDataAccess elementDataAccess)
        {
            dataAccess = elementDataAccess;
        }

        public Task<List<Element>> Read()
        {
            return dataAccess.GetAsync();
        }
    }
}
