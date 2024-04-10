using Models.DTOs;
using Models.DTOs.ExamDTOs;
using Models.DTOs.RoleDTOs;
using Models.Entities;
using Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentExamifyEFCore;

namespace Repository.ExamRepository
{
    public class ExamRepository : IExamRepository
    {
        private readonly TalentExamifyContext _context;

        public ExamRepository(TalentExamifyContext context)
        {
            _context = context;
        }
        public async Task<PaginationEntityDTO<ExamDto>> GetAllExams()
        {
            var result = _context.Exams.Select(e => new ExamDto
            {
                ExamID = e.ExamID,
                Name = e.Name,
                FullMarks = e.FullMarks,
                Duration = e.Duration
            }).ToList();
            return new PaginationEntityDTO<ExamDto>
            {
                Entities = result.ToList(),
                TotalEntityCount = result.Count
            };
        }
        public async Task<ExamDto> GetExamById(string examId)
        {
            var exam = _context.Exams.Find(examId);
            if (exam == null)
                return null;

            return new ExamDto
            {
                ExamID = exam.ExamID,
                Name = exam.Name,
                FullMarks = exam.FullMarks,
                Duration = exam.Duration
            };
        }

        public async Task<Response> AddExam(ExamDto examDto)
        {
            try
            {
                var exam = new Exam
                {
                    ExamID = examDto.ExamID,
                    Name = examDto.Name,
                    FullMarks = examDto.FullMarks,
                    Duration = examDto.Duration
                };

                await _context.Exams.AddAsync(exam);
                await _context.SaveChangesAsync();
                return new Response();
            }
            catch (Exception ex)
            {
                return new Response { ErrorMessage=ex.Message};
            }
           

        }


        public async Task<Response> UpdateExam(ExamDto examDto)
        {
            try
            {
                var exam = _context.Exams.Find(examDto.ExamID);
                if (exam == null)
                    return new Response{ErrorMessage= "Exam not found" } ;

                exam.Name = examDto.Name;
                exam.FullMarks = examDto.FullMarks;
                exam.Duration = examDto.Duration;

               await _context.SaveChangesAsync();
                return new Response();
            }
            catch (Exception ex)
            {

                return new Response { ErrorMessage = ex.Message }; 
            }
           
        }
        public async Task<Response> DeleteExam(string examId)
        {
            var examToDelete = _context.Exams.Find(examId);
            if (examToDelete == null)
                return new Response { ErrorMessage = "Exam not found." };
                _context.Exams.Remove(examToDelete);
               await _context.SaveChangesAsync();
            return new Response();
          
        }

      

       
    }
}
