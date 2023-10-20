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
        Task<bool> SurveyStudent(SurveyStudentObject surveyStudentObject);
        Task<bool> SurveyTeacher(SurveyTeacherObject surveyTeacherObject);
    }
}
