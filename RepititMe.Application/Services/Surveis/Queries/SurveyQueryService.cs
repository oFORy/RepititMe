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

        public async Task<bool> SurveyStudentFirst(SurveyStudentFirstObject surveyStudentFirstObject)
        {
            return await _surveyRepository.SurveyStudentFirst(surveyStudentFirstObject);
        }

        public async Task<bool> SurveyStudentSecond(SurveyStudentSecondObject surveyStudentSecondObject)
        {
            return await _surveyRepository.SurveyStudentSecond(surveyStudentSecondObject);
        }

        public async Task<bool> SurveyTeacherFirst(SurveyTeacherFirstObject surveyTeacherFirstObject)
        {
            return await _surveyRepository.SurveyTeacherFirst(surveyTeacherFirstObject);
        }

        public async Task<bool> SurveyTeacherSecond(SurveyTeacherSecondObject surveyTeacherSecondObject)
        {
            return await _surveyRepository.SurveyTeacherSecond(surveyTeacherSecondObject);
        }
    }
}
