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

        [HttpPut("Api/Survey/Student/First")]
        public async Task<bool> SurveyStudentFirst(SurveyStudentFirstObject surveyStudentFirstObject)
        {
            return await _surveyQueryService.SurveyStudentFirst(surveyStudentFirstObject);
        }

        [HttpPut("Api/Survey/Student/Second")]
        public async Task<bool> SurveyStudentSecond(SurveyStudentSecondObject surveyStudentSecondObject)
        {
            return await _surveyQueryService.SurveyStudentSecond(surveyStudentSecondObject);
        }


        [HttpPut("Api/Survey/Teacher/First")]
        public async Task<bool> SurveyTeacherFirst(SurveyTeacherFirstObject surveyTeacherFirstObject)
        {
            return await _surveyQueryService.SurveyTeacherFirst(surveyTeacherFirstObject);
        }

        [HttpPut("Api/Survey/Teacher/Second")]
        public async Task<bool> SurveyTeacherSecond(SurveyTeacherSecondObject surveyTeacherSecondObject)
        {
            return await _surveyQueryService.SurveyTeacherSecond(surveyTeacherSecondObject);
        }
    }
}
