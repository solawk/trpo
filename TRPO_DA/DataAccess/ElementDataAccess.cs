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

        // Advanced

        // TODO: search (by name, by data properties, by category)

        public async Task<List<ElementVM>> Search(List<Filter> filters)
        {
            IQueryable<Element>? query = dataContext.Elements;

            // Поиск по имени (точное совпадение)
            if (filters.Exists(f => f.type == Filter.FilterType.Name))
            {
                var nameFilter = filters.Find(f => f.type == Filter.FilterType.Name);
                query = query.Where(e => e.Name == nameFilter.value.ToString());
            }

            // Поиск по категории (точное совпадение)
            if (filters.Exists(f => f.type == Filter.FilterType.Category))
            {
                var categoryFilter = filters.Find(f => f.type == Filter.FilterType.Category);
                query = query.Where(e => e.CategoryID == (long)categoryFilter.value);
            }

            // Поиск по данным
            // 1 (в составе запроса к БД): Элементы-результаты должны включать в себя все свойства, перечисленные в фильтрах
            // 2 (после запроса и парсинга поля Data) Проверка значений происходит только для элементов, найденных на этапе 1

            if (filters.Exists(f => f.type == Filter.FilterType.Data))
            {
                foreach (var f in filters)
                {
                    if (f.type == Filter.FilterType.Data)
                    {
                        query = query.Where(e => e.Data.Contains("\"" + f.key + "\""));
                    }
                }
            }
            
            var result = await query.Include(e => e.Category).ToListAsync();

            // Если есть поиск по данным, то данные всех элементов-результатов поиска парсятся и проверяется соответствие значений запросу
            if (filters.Exists(f => f.type == Filter.FilterType.Data))
            {
                List<Element> resultEligibleByDataFilters = new List<Element>();

                foreach (var e in result)
                {
                    var eData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(e.Data);
                    bool eligible = true;

                    foreach (var f in filters)
                    {
                        if (f.type != Filter.FilterType.Data) continue;

                        if (!eData.ContainsKey(f.key))
                        {
                            eligible = false;
                            break;
                        }
                        else
                        {
                            bool isValueAString = false;

                            if (f.predicate == Filter.Predicate.Equals)
                            {
                                if (f.value.GetType().Equals(typeof(string)))
                                {
                                    isValueAString = true;
                                }
                            }
                            bool areValuesEqual = false;

                            if (isValueAString)
                            {
                                areValuesEqual = eData[f.key].Equals(f.value);
                            }
                            else
                            {
                                areValuesEqual = (long)eData[f.key] == (long)f.value;
                            }

                            bool isEqualWhenNeeded = f.predicate == Filter.Predicate.Equals && areValuesEqual;
                            bool isGreaterWhenNeeded = f.predicate == Filter.Predicate.GreaterThan && (long)eData[f.key] >= (long)f.value;
                            bool isLesserWhenNeeded = f.predicate == Filter.Predicate.LesserThen && (long)eData[f.key] <= (long)f.value;

                            if (!isEqualWhenNeeded && !isGreaterWhenNeeded && !isLesserWhenNeeded)
                            {
                                eligible = false;
                                break;
                            }
                        }
                    }

                    if (eligible)
                    {
                        resultEligibleByDataFilters.Add(e);
                    }
                }

                result = resultEligibleByDataFilters;
            }

            var resultViewModel = mapper.Map<List<ElementVM>>(result);

            return resultViewModel;
        }
    }
}
