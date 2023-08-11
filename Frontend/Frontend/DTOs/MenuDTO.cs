namespace Frontend.DTOs
{
    public class MenuDTO
    {
        public string Id { get; set; }
        public string MenuCode { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int Level { get; set; }
        public string ParentCode { get; set; }
        public int Order { get; set; }
        public string IsActive { get; set; }
    }
}
