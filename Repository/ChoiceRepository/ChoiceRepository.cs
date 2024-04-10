using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.DTOs.ChoiceDtos;
using Models.DTOs.QuestionDTOs;
using Models.Entities;
using Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentExamifyEFCore;

namespace Repository.ChoiceRepository
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly TalentExamifyContext _context;

        public ChoiceRepository(TalentExamifyContext context)
        {
            _context = context;
        }
        public async Task<PaginationEntityDTO<ChoiceDto>> GetAllChoices()
        {
            var res = _context.Choices
                           .Select(c => new ChoiceDto
                           {
                               ChoiceID = c.ChoiceID,
                               QuestionID = c.QuestionID,
                               DisplayText = c.DisplayText
                           });
            return new PaginationEntityDTO<ChoiceDto>
            {
                Entities = await res.ToListAsync(),
                TotalEntityCount = await res.CountAsync(),
            };
        }
        public async Task<List<ChoiceDto>> GetChoicesByQuestionId(string questionId)
        {
            return await _context.Choices
                           .Where(c => c.QuestionID == questionId)
                           .Select(c => new ChoiceDto
                           {
                               ChoiceID = c.ChoiceID,
                               QuestionID = c.QuestionID,
                               DisplayText = c.DisplayText
                           })
                           .ToListAsync();
        }
        public async Task<ChoiceDto> GetChoiceById(string choiceId)
        {
            return await _context.Choices
                           .Where(c => c.ChoiceID == choiceId)
                           .Select(c => new ChoiceDto
                           {
                               ChoiceID = c.ChoiceID,
                               QuestionID = c.QuestionID,
                               DisplayText = c.DisplayText
                           })
                           .FirstOrDefaultAsync();
        }
        public async Task<Response> AddChoice(ChoiceDto choiceDto)
        {
            var choice = new Choice
            {
                ChoiceID = choiceDto.ChoiceID,
                QuestionID = choiceDto.QuestionID,
                DisplayText = choiceDto.DisplayText
            };

            await _context.Choices.AddAsync(choice);
            await _context.SaveChangesAsync();
            return new Response();
        }
        public async Task<Response> UpdateChoice(ChoiceDto choiceDto)
        {
            var choice = await _context.Choices.FindAsync(choiceDto.ChoiceID);
            if (choice == null)
                return new Response { ErrorMessage = "Choice not found" };

            choice.QuestionID = choiceDto.QuestionID;
            choice.DisplayText = choiceDto.DisplayText;

            await _context.SaveChangesAsync();
            return new Response();
        }
        public async Task<Response> DeleteChoice(string choiceId)
        {
            var choiceToDelete =await _context.Choices.FindAsync(choiceId);
            if (choiceToDelete == null)
                return new Response { ErrorMessage = "Choice not found" };
               _context.Choices.Remove(choiceToDelete);
             await   _context.SaveChangesAsync();
            return new Response();
            
        }

    }
}
