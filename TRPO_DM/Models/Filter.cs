namespace TRPO_DM.Models
{
    // Объект фильтра для поиска элементов
    // При поиске клиент отправляет серверу список фильтров
    public class Filter
    {
        public Filter(FilterType type, string key, object value, Predicate predicate)
        {
            this.type = type;
            this.key = key;
            this.value = value;
            this.predicate = predicate;
        }

        public enum FilterType { Data, Name, Category };

        public FilterType type; // Фильтр по свойству в данных, по имени или по категории

        public string key; // Свойство в данных, по которому производится поиск, не используется при типе фильтра не по данным

        public object value; // Значение, с которым происходит сравнение

        public enum Predicate { Equals, GreaterThan, LesserThen };

        public Predicate predicate; // Сравнивать ли значение точно или выбирать всё больше или меньше данного значения
    }
}
