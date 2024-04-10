using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.DTOs.ExamDTOs;
using Repository.ExamRepository;

namespace TalentExamify.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository _examRepository;

        public ExamController(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }
        [HttpGet]
        public async Task<ActionResult<PaginationEntityDTO<ExamDto>>> GetAllExams()
        {
            var exams = await _examRepository.GetAllExams();
            return Ok(exams);
        }

        [HttpGet("{examId}")]
        public async Task<ActionResult<ExamDto>> GetExamById(string examId)
        {
            var exam = await _examRepository.GetExamById(examId);
            if (exam == null)
                return NotFound();
            return Ok(exam);
        }
        [HttpPost]
        public async Task<ActionResult> AddExam([FromBody] ExamDto examDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

          var result= await _examRepository.AddExam(examDto);
            return Ok(result);
        }
        [HttpPut("{examId}")]
        public async Task<ActionResult> UpdateExam(string examId, [FromBody] ExamDto examDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (examDto.ExamID != examId)
                return BadRequest("Exam ID mismatch");

            try
            {
               var result= await _examRepository.UpdateExam(examDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{examId}")]
        public async Task<ActionResult> DeleteExam(string examId)
        {
            var existingExam = _examRepository.GetExamById(examId);
            if (existingExam == null)
                return NotFound();

          var result= await _examRepository.DeleteExam(examId);
            return Ok(result);
        }
    }
}
