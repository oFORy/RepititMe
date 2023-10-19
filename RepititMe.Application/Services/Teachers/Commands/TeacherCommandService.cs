﻿using Microsoft.AspNetCore.Http;
using RepititMe.Application.Services.Teachers.Common;
using RepititMe.Application.Services.Users.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Teachers;
using RepititMe.Domain.Object.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Teachers.Commands
{
    public class TeacherCommandService : ITeacherCommandService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherCommandService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<bool> ChangeProfile(ChangeProfileTeacherObject changeProfileTeacherObject)
        {
            string imagePath = null;
            List<string> videoPaths = new List<string>();
            List<string> certificatePaths = new List<string>();

            Teacher model = new Teacher
            {
                Image = imagePath,
                StatusId = changeProfileTeacherObject.StatusId,
                ScienceId = changeProfileTeacherObject.ScienceId,
                LessonTargetId = changeProfileTeacherObject.LessonTargetId,
                AgeСategoryId = changeProfileTeacherObject.AgeСategoryId,
                Experience = changeProfileTeacherObject.Experience,
                AboutMe = changeProfileTeacherObject.AboutMe,
                Price = changeProfileTeacherObject.Price,
                VideoPresentation = videoPaths,
                Certificates = certificatePaths
            };

            int userId = await _teacherRepository.ChangeProfile(model, changeProfileTeacherObject.TelegramId);

            if (userId > 0)
            {
                string userFolderPath = $"media\\{userId}"; // Изменить на / когда отправиться на linux контейнер
                Directory.CreateDirectory(userFolderPath);

                if (changeProfileTeacherObject.Image != null)
                {
                    imagePath = await SaveFile(changeProfileTeacherObject.Image, $"{userFolderPath}\\images");
                }
                if (changeProfileTeacherObject.VideoPresentation != null)
                {
                    foreach (var video in changeProfileTeacherObject.VideoPresentation)
                    {
                        var videoPath = await SaveFile(video, $"{userFolderPath}\\presentations");
                        videoPaths.Add(videoPath);
                    }
                }
                if (changeProfileTeacherObject.Certificates != null)
                {
                    foreach (var certificate in changeProfileTeacherObject.Certificates)
                    {
                        var certificatePath = await SaveFile(certificate, $"{userFolderPath}\\certificates");
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

                return await _teacherRepository.UpdateTeacherDataFolder(updateTeacherDataFolderObject);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ChangeVisability(int telegramId)
        {
            return await _teacherRepository.ChangeVisability(telegramId);
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