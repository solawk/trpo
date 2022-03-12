using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TRPO_DM.Models;

namespace TRPO_DA.DataAccess
{
    public class ElementDataAccess
    {
        private DataContext dataContext { get; }
        private IMapper mapper { get; }

        public ElementDataAccess(DataContext context, IMapper _mapper)
        {
            dataContext = context;
            mapper = _mapper;
        }

        public async Task<List<Element>> GetAsync()
        {
            return mapper.Map<List<Element>>(await dataContext.Elements.ToListAsync());
        }
    }
}
