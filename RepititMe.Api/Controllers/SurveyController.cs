using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Surveis.Queries;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Surveis;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class SurveyController : Controller
    {
        private readonly ISurveyQueryService _surveyQueryService;
        public SurveyController(ISurveyQueryService surveyQueryService)
        {
            _surveyQueryService = surveyQueryService;
        }

        [HttpPut("Api/Survey/Student")]
        public async Task<bool> SurveyStudent(SurveyStudentObject surveyStudentObject)
        {
            return await _surveyQueryService.SurveyStudent(surveyStudentObject);
        }

        [HttpPut("Api/Survey/Teacher")]
        public async Task<bool> SurveyTeacher(SurveyTeacherObject surveyTeacherObject)
        {
            return await _surveyQueryService.SurveyTeacher(surveyTeacherObject);
        }
    }
}
