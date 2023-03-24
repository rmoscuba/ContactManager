namespace ContactManager.Interfaces
{
    public interface IRepositoryManager
    {
        IContactRepository Contact { get; }
        IUserRepository User { get; }
        IJwtRepository Jwt { get; }
        void Save();
    }
}
