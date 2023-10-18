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

            int userId = await _userRepository.UserSignUpTeacher(model, userSignUpTeacher.Name, userSignUpTeacher.SecondName, userSignUpTeacher.TelegramId);


            if (userId > 0)
            {
                string userFolderPath = $"media/{userId}/";
                Directory.CreateDirectory(userFolderPath);

                if (userSignUpTeacher.Image != null)
                {
                    imagePath = await SaveFile(userSignUpTeacher.Image, $"{userFolderPath}/images");
                }
                if (userSignUpTeacher.VideoPresentation != null)
                {
                    foreach (var video in userSignUpTeacher.VideoPresentation)
                    {
                        var videoPath = await SaveFile(video, $"{userFolderPath}/presentations");
                        videoPaths.Add(videoPath);
                    }
                }
                if (userSignUpTeacher.Certificates != null)
                {
                    foreach (var certificate in userSignUpTeacher.Certificates)
                    {
                        var certificatePath = await SaveFile(certificate, $"{userFolderPath}/certificates");
                        certificatePaths.Add(certificatePath);
                    }
                }

                UpdateTeacherDataFolderObject updateTeacherDataFolderObject = new UpdateTeacherDataFolderObject()
                {
                    UserId = userId,
                    Image = imagePath,
                    VideoPresentation = videoPaths,
                    Certificates = certificatePaths
                };

                return await _userRepository.UpdateTeacherDataFolder(updateTeacherDataFolderObject); // не сработал, почему???
            }
            else
            {
                return false;
            }
        }

        private async Task<string> SaveFile(IFormFile formFile, string folderName)
        {
            var userSpecificFolderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(userSpecificFolderPath))
            {
                Directory.CreateDirectory(userSpecificFolderPath);
            }

            var filePath = Path.Combine(userSpecificFolderPath, formFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return filePath;
        }
    }
}
