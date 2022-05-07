namespace TRPO_DM.Interfaces
{
    public interface ICategoryData
    {
        public int ID { get; set; }

        public int? ParentID { get; set; }

        public string Name { get; set; }
    }
}
