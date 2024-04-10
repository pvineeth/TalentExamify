using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.DTOs.QuestionDTOs;
using Repository.QuestionRepository;

namespace TalentExamify.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        [HttpGet]
        public async Task<ActionResult<PaginationEntityDTO<QuestionDto>>> GetAllQuestions()
        {
            var questions = await _questionRepository.GetAllQuestions();
            return Ok(questions);
        }
        [HttpGet("{questionId}")]
        public async Task<ActionResult<QuestionDto>> GetQuestionById(string questionId)
        {
            var question = await _questionRepository.GetQuestionById(questionId);
            if (question == null)
                return NotFound();

            return Ok(question);
        }

        [HttpGet("exam/{examId}")]
        public async Task<ActionResult<List<QuestionDto>>> GetQuestionsByExamId(string examId)
        {
            var questions = await _questionRepository.GetQuestionsByExamId(examId);
            return Ok(questions);
        }

        [HttpPost]
        public async Task<ActionResult> AddQuestion([FromBody] QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _questionRepository.AddQuestion(questionDto);
            return Ok(res);
        }
        [HttpPut("{questionId}")]
        public async Task<ActionResult> UpdateQuestion(string questionId, [FromBody] QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (questionDto.QuestionID != questionId)
                return BadRequest("Question ID mismatch");

            try
            {
                var res = await _questionRepository.UpdateQuestion(questionDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{questionId}")]
        public async Task<ActionResult> DeleteQuestion(string questionId)
        {
            var existingQuestion = _questionRepository.GetQuestionById(questionId);
            if (existingQuestion == null)
                return NotFound();

           var res=await _questionRepository.DeleteQuestion(questionId);
            return Ok(res);
        }
    }
}
