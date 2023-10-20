using RepititMe.Application.Services.Surveis.Common;
using RepititMe.Domain.Object.Surveis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Surveis.Queries
{
    public class SurveyQueryService : ISurveyQueryService
    {
        private readonly ISurveyRepository _surveyRepository;
        public SurveyQueryService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<bool> SurveyStudent(SurveyStudentObject surveyStudentObject)
        {
            return await _surveyRepository.SurveyStudent(surveyStudentObject);
        }

        public async Task<bool> SurveyTeacher(SurveyTeacherObject surveyTeacherObject)
        {
            return await _surveyRepository.SurveyTeacher(surveyTeacherObject);
        }
    }
}
