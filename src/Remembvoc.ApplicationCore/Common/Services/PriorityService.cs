using AutoMapper;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.DTOs;
using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.ApplicationCore.Common.Services;

public class PriorityService
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

    public async Task UpdateSinglePriorityByIdAsync(Priority priority)
    {
        // TODO: Add a calculation of points of the priority
        
        var priorityEntity = _mapper.Map<PriorityEntity>(priority);
        
        await _repository.UpdateSinglePriorityByIdAsync(priorityEntity);
    }
}