using Models.DTOs;
using Models.DTOs.QuestionDTOs;
using Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.QuestionRepository
{
    public interface IQuestionRepository
    {
        Task<PaginationEntityDTO<QuestionDto>> GetAllQuestions();
        Task<QuestionDto> GetQuestionById(string questionId);
        Task<List<QuestionDto>> GetQuestionsByExamId(string examId);
        Task<Response> AddQuestion(QuestionDto questionDto);
        Task<Response> UpdateQuestion(QuestionDto questionDto);
        Task<Response> DeleteQuestion(string questionId);
    }
}
