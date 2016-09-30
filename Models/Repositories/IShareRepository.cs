namespace Alb.Models.Repositories
{
    public interface IShareRepository
    {
        Share Find(int id);
        int Create(CreateShare share);
    }
}