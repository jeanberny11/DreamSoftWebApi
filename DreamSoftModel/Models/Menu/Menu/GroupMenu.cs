namespace DreamSoftModel.Models.Menu.Menu
{
    public class GroupMenu
    {
        public int GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public int SortOrder { get; set; }
        public List<OptionMenu> Options { get; set; } = [];
    }
}
