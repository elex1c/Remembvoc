namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IRepository<T>
{
    public Task<List<T>> GetAllAsync();
}