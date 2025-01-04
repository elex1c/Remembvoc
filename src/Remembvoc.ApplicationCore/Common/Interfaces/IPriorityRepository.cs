using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IPriorityRepository : IRepository<PriorityEntity>
{
    public Task AddAsync(PriorityEntity priority);
    public Task<PriorityEntity?> GetPriorityByIdAsync(int id);
    public Task UpdatePrioritiesAsync(IEnumerable<PriorityEntity> priorities);
    public Task UpdateSinglePriorityAsync(PriorityEntity priorityEntity);
}