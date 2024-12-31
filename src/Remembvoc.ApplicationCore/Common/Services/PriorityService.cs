using AutoMapper;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;
using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.ApplicationCore.Common.Services;

public class PriorityService : IPriorityService
{
    private readonly IPriorityRepository _repository;
    private readonly IMapper _mapper;

    public PriorityService(IPriorityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task UpdatePrioritiesAsync()
    {
        var priorityEntities = await _repository.GetAllAsync();
        
        var priorities = priorityEntities.Select(pe => _mapper.Map<Priority>(pe))
            .ToList();
        
        foreach (var priority in priorities) priority.CountCheckTime();
    }
    
    public async Task UpdateSinglePriorityByIdAsync(int priorityId, bool isTranslatedSuccessfully)
    {
        var priority = await GetPriorityByIdAsync(priorityId);

        if (priority is null) return;
        
        priority.CountPoints(isTranslatedSuccessfully);
        
        var priorityEntity = _mapper.Map<PriorityEntity>(priority);
        
        await _repository.UpdateSinglePriorityAsync(priorityEntity);
    }

    public async Task<Priority?> GetPriorityByIdAsync(int priorityId)
    {
        var priorityEntity = await _repository.GetPriorityByIdAsync(priorityId);
        
        return priorityEntity is null ? null : _mapper.Map<Priority>(priorityEntity);
    }
}