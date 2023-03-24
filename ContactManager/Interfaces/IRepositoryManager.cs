namespace ContactManager.Interfaces
{
    public interface IRepositoryManager
    {
        IContactRepository Contact { get; }
        IUserRepository User { get; }
        void Save();
    }
}
