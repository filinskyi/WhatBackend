﻿using CharlieBackend.Core.Entities;
using CharlieBackend.Core.Models;
using CharlieBackend.Core.Models.Account;
using CharlieBackend.Core.Models.Course;
using CharlieBackend.Core.Models.Lesson;
using CharlieBackend.Core.Models.Student;
using CharlieBackend.Core.Models.StudentGroup;
using CharlieBackend.Core.Models.Theme;
using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace CharlieBackend.Core
{
    public static class AdapterExtensionMethods
    {
        public static Account ToAccount(this BaseAccountModel accountModel)
        {
            return new Account
            {
                Id = accountModel.Id,
                Email = accountModel.Email,
                Password = accountModel.Password,
                FirstName = accountModel.FirstName,
                LastName = accountModel.LastName,
                Role = (sbyte)accountModel.Role
            };
        }

        public static BaseAccountModel ToAccountModel(this Account account)
        {
            return new BaseAccountModel
            {
                Id = account.Id,
                Email = account.Email,
                Password = account.Password,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Role = (sbyte)account.Role
            };
        }

        public static AccountInfoModel ToUserModel(this BaseAccountModel accountModel)
        {
            return new AccountInfoModel
            {
                FirstName = accountModel.FirstName,
                LastName = accountModel.LastName,
                Role = accountModel.Role
            };
        }

        public static Lesson ToLesson(this LessonModel lessonModel)
        {
            return new Lesson
            {
                Id = lessonModel.Id,
                StudentGroupId = lessonModel.GroupId,
                LessonDate = DateTime.Parse(lessonModel.LessonDate),
            };
        }

        public static LessonModel ToLessonModel(this Lesson lesson)
        {
            return new LessonModel
            {
                Id = lesson.Id,
                GroupId = lesson.StudentGroupId ?? 0, // TODO: remove
                LessonDate = lesson.LessonDate.ToString()
            };
        }

        public static Course ToCourse(this CourseModel courseModel)
        {
            return new Course
            {
                Id = courseModel.Id,
                Name = courseModel.Name
            };
        }

        public static CourseModel ToCourseModel(this Course course)
        {
            return new CourseModel
            {
                Id = course.Id,
                Name = course.Name,
            };
        }

        //public static StudentGroup ToStudentGroup(this StudentGroupModel studentGroupModel)
        //{
        //    return new StudentGroup
        //    {
        //        Name = studentGroupModel.name,
        //        CourseId = studentGroupModel.course_id,
        //        StartDate = DateTime.Parse(studentGroupModel.start_date),
        //        FinishDate = DateTime.Parse(studentGroupModel.finish_date),
        //    };
        //}

        public static Theme ToTheme(this ThemeModel themeModel)
        {
            return new Theme
            {
                Id = themeModel.Id,
                Name = themeModel.Name
            };
        }

        public static ThemeModel ToThemeModel(this Theme themeModel)
        {
            return new ThemeModel
            {
                Id = themeModel.Id,
                Name = themeModel.Name
            };
        }

        public static Mentor ToMentor(this MentorModel mentorModel)
        {
            return new Mentor
            { };
        }

        public static MentorModel ToMentorModel(this Mentor mentor)
        {
            return new MentorModel
            {
                FirstName = mentor.Account.FirstName,
                LastName = mentor.Account.LastName,
                Email = mentor.Account.Email,
                Password = mentor.Account.Password,
                Courses_id = mentor.MentorsOfCourses.Select(mentorOfCourse => mentorOfCourse.Id).ToArray()
            };
        }
        public static StudentModel ToStudentModel(this Student student)
        {
            return new StudentModel
            {
                FirstName = student.Account.FirstName,
                LastName = student.Account.LastName,
                Email = student.Account.Email,
                Password = student.Account.Password,

            };
        }
        public static Student ToStudent(this StudentModel studentModel)
        {
            return new Student
            { };
        }
        
        public static StudentGroupModel ToStudentGroupModel(this StudentGroup group)
        {
            return new StudentGroupModel
            {
                id = group.Id,
                name = group.Name,
                start_date = group.StartDate.ToString(),
                finish_date = group.FinishDate.ToString(),
                students_id = group.StudentsOfStudentGroups.Select(groupSt => (long)groupSt.StudentId).ToList(),
                course_id = (long)group.CourseId
            };
        }
        public static StudentGroup ToStudentGroup(this StudentGroupModel group)
        {
            return new StudentGroup
            {
                Id = group.id,
                Name = group.name,
                StartDate = Convert.ToDateTime(group.start_date.ToString()),
                FinishDate = Convert.ToDateTime(group.finish_date.ToString()),
                
                // добавить лист с группой
                CourseId = group.course_id

            };
        }
    }
}