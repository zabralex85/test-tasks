namespace Medialink.RepositoryInterfaces
{
    public interface IDatabaseRepository
    {
        int Select();

        int Insert();

        int Update();

        int Delete();
    }
}
