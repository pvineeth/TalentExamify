using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.DTOs.ExamDTOs;
using Models.DTOs.QuestionDTOs;
using Models.Entities;
using Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentExamifyEFCore;

namespace Repository.QuestionRepository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly TalentExamifyContext _context;

        public QuestionRepository(TalentExamifyContext context)
        {
            _context = context;
        }
        public async Task<PaginationEntityDTO<QuestionDto>> GetAllQuestions()
        {
            var result = _context.Questions
                           .Include(q => q.Exam)
                           .Select(q => new QuestionDto
                           {
                               QuestionID = q.QuestionID,
                               QuestionType = q.QuestionType,
                               DisplayText = q.DisplayText,
                               ExamID = q.ExamID,
                               ExamName = q.Exam.Name
                           });
            return new PaginationEntityDTO<QuestionDto>
            {
                Entities =await result.ToListAsync(),
                TotalEntityCount =await result.CountAsync(),
            };
        }
        public async Task<QuestionDto> GetQuestionById(string questionId)
        {
            return await _context.Questions
                           .Include(q => q.Exam)
                           .Where(q => q.QuestionID == questionId)
                           .Select(q => new QuestionDto
                           {
                               QuestionID = q.QuestionID,
                               QuestionType = q.QuestionType,
                               DisplayText = q.DisplayText,
                               ExamID = q.ExamID,
                               ExamName = q.Exam.Name
                           })
                           .FirstOrDefaultAsync();
        }
        public async Task<List<QuestionDto>> GetQuestionsByExamId(string examId)
        {
            return await _context.Questions
                           .Include(q => q.Exam)
                           .Where(q => q.ExamID == examId)
                           .Select(q => new QuestionDto
                           {
                               QuestionID = q.QuestionID,
                               QuestionType = q.QuestionType,
                               DisplayText = q.DisplayText,
                               ExamID = q.ExamID,
                               ExamName = q.Exam.Name
                           })
                           .ToListAsync();
        }
        public async Task<Response> AddQuestion(QuestionDto questionDto)
        {
            var question = new Question
            {
                QuestionID = questionDto.QuestionID,
                QuestionType = questionDto.QuestionType,
                DisplayText = questionDto.DisplayText,
                ExamID = questionDto.ExamID
            };

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return new Response();
        }
        public async Task<Response> UpdateQuestion(QuestionDto questionDto)
        {
            var question = _context.Questions.Find(questionDto.QuestionID);
            if (question == null)
                return new Response { ErrorMessage = "Question not found" };

            question.QuestionType = questionDto.QuestionType;
            question.DisplayText = questionDto.DisplayText;
            question.ExamID = questionDto.ExamID;

            await _context.SaveChangesAsync();
            return new Response();
        }
        public async Task<Response> DeleteQuestion(string questionId)
        {
            var questionToDelete = _context.Questions.Find(questionId);
            if (questionToDelete == null)
                return new Response { ErrorMessage = "Question not found" };
              _context.Questions.Remove(questionToDelete);
             await _context.SaveChangesAsync();
            return new Response();
            
        }
    }
}
