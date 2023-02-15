namespace Framework.UnitOfWork.UnitOfWork
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}