using Models.DTOs;
using Models.DTOs.ChoiceDtos;
using Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ChoiceRepository
{
    public interface IChoiceRepository
    {
       Task<PaginationEntityDTO<ChoiceDto>> GetAllChoices();
        Task<List<ChoiceDto>> GetChoicesByQuestionId(string questionId);
        Task<ChoiceDto> GetChoiceById(string choiceId);
         Task<Response> AddChoice(ChoiceDto choiceDto);
        Task<Response> UpdateChoice(ChoiceDto choiceDto);
        Task<Response> DeleteChoice(string choiceId);
    }
}
