namespace DreamSoftModel.Models.Authentication
{
    public class AccountCreate: Account
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
