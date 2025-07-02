namespace DreamSoftModel.Models.Public.Menu
{
    public class MenuModule
    {
        public int ModuleId { get; set; }
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public int SortOrder { get; set; }
        public List<GroupMenu> GroupMenus { get; set; } = [];
    }
}
