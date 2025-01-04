using Remembvoc.ApplicationCore.Common.Models.DomainModels;

namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IPriorityService
{
    public Task AddPriorityAsync(Word word);
    public Task UpdatePrioritiesAsync();
    public Task UpdateSinglePriorityByIdAsync(int priorityId, bool isTranslatedSuccessfully);
    public Task<Priority?> GetPriorityByIdAsync(int priorityId);
}