namespace TRPO_MVC.Models
{
    public class FilterDataModel
    {
        public FilterDataModel(int p, object v)
        {
            predicate = p;
            value = v;
        }

        public int predicate; // -1 lesser, 0 equal, 1 greater

        public object value;
    }
}
