using Azure;
using Models.DTOs;
using Models.DTOs.ExamDTOs;
using Models.Reponses;
using Response = Models.Reponses.Response;



namespace Repository.ExamRepository
{
    public interface IExamRepository
    {
        Task<PaginationEntityDTO<ExamDto>> GetAllExams();
        Task<ExamDto> GetExamById(string examId);
        Task<Response> AddExam(ExamDto examDto);
        Task<Response> UpdateExam(ExamDto examDto);
        Task<Response> DeleteExam(string examId);
    }
}
