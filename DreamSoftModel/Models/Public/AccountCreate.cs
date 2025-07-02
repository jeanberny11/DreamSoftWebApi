namespace DreamSoftModel.Models.Public
{
    public class AccountCreate: Account
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
