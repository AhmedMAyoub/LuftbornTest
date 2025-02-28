namespace Application.Interfaces
{
    public interface ITokenProvider
    {
        string GenerateToken(Domain.Entities.User user);
    }
}
