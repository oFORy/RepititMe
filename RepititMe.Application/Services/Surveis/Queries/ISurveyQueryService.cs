using RepititMe.Domain.Object.Surveis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Surveis.Queries
{
    public interface ISurveyQueryService
    {
        Task<bool> SurveyStudentFirst(SurveyStudentFirstObject surveyStudentFirstObject);
        Task<bool> SurveyTeacherFirst(SurveyTeacherFirstObject surveyTeacherFirstObject);
        Task<bool> SurveyStudentSecond(SurveyStudentSecondObject surveyStudentSecondObject);
        Task<bool> SurveyTeacherSecond(SurveyTeacherSecondObject surveyTeacherSecondObject);
    }
}
