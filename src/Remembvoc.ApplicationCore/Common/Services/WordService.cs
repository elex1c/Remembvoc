using AutoMapper;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.DTOs;
using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.ApplicationCore.Common.Services;

public class WordService
{
    private readonly IWordRepository _repository;
    private readonly IMapper _mapper;
    
    public WordService(IWordRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<Word>> GetAllAsync()
    {
        var wordEntities = await _repository.GetAllAsync();
        
        return wordEntities.Select(w => _mapper.Map<Word>(w))
            .ToList();
    }

    public async Task<Word?> GetWordByNameAsync(string word)
    {
        var wordEntity = await _repository.GetWordByNameAsync(word);
        
        return wordEntity is null ? null : _mapper.Map<Word>(wordEntity);
    }
    
    public async Task<List<Word>> GetWordsForRevisingAsync(int elementsPerPage, int pageNumber)
    {
        var wordEntities = await _repository.GetAllWithPrioritiesAsync();
            
        return wordEntities.Where(w => w.Priority.MinutesToRepeat <= 0)
            .OrderBy(w => w.Id)
            .Skip(pageNumber * elementsPerPage - elementsPerPage)
            .Take(elementsPerPage)
            .Select(w => _mapper.Map<Word>(w))
            .ToList();
    }
}