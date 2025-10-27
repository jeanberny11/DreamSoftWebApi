namespace DreamSoftModel.Models.Menu.Menu
{
    public class OptionMenu
    {
        public int OptionId { get; set; }
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int SortOrder { get; set; }
    }
}
