namespace DreamSoftModel.Models.Public;

public class Gender
{
    public int GenderId { get; set; }
    public string Name { get; set; } = null!;
    public bool Active { get; set; }
}