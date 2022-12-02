namespace AuthenticationService.Api.Application.Services.Password
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool IsValid(string password, string hashedPassword);
    }
}
