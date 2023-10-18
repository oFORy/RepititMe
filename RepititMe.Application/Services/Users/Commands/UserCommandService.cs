using Microsoft.AspNetCore.Http;
using RepititMe.Application.Services.Users.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Users.Commands
{
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;

        public UserCommandService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> UserSignUpStudent(UserSignUpStudentObject userSignUpObject)
        {
            return await _userRepository.UserSignUpStudent(userSignUpObject);
        }

        public async Task<bool> UserSignUpTeacher(UserSignUpTeacherObject userSignUpTeacher)
        {
            string imagePath = null;
            List<string> videoPaths = new List<string>();
            List<string> certificatePaths = new List<string>();

            if (userSignUpTeacher.Image != null)
            {
                imagePath = await SaveFile(userSignUpTeacher.Image, "Images");
            }
            if (userSignUpTeacher.VideoPresentation != null)
            {
                foreach (var video in userSignUpTeacher.VideoPresentation)
                {
                    var videoPath = await SaveFile(video, "Presentations");
                    videoPaths.Add(videoPath);
                }
            }
            if (userSignUpTeacher.Certificates != null)
            {
                foreach (var certificate in userSignUpTeacher.Certificates)
                {
                    var certificatePath = await SaveFile(certificate, "Certificates");
                    certificatePaths.Add(certificatePath);
                }
            }

            Teacher model = new Teacher
            {
                Image = imagePath,
                StatusId = userSignUpTeacher.StatusId,
                ScienceId = userSignUpTeacher.ScienceId,
                LessonTargetId = userSignUpTeacher.LessonTargetId,
                AgeСategoryId = userSignUpTeacher.AgeСategoryId,
                Experience = userSignUpTeacher.Experience,
                AboutMe = userSignUpTeacher.AboutMe,
                Price = userSignUpTeacher.Price,
                VideoPresentation = videoPaths,
                Certificates = certificatePaths,
                Visibility = userSignUpTeacher.Visibility
            };

            return await _userRepository.UserSignUpTeacher(model, userSignUpTeacher.Name, userSignUpTeacher.SecondName, userSignUpTeacher.TelegramId);
        }

        private async Task<string> SaveFile(IFormFile formFile, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName, formFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return filePath;
        }
    }
}
