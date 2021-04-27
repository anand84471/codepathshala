using FluentValidation;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.ValidationHandler
{
    public class StudentLoginValidator:AbstractValidator<StudentRegisterModal>
    {
        public StudentLoginValidator()
        {

            RuleFor(request => request.m_strPassword).NotEmpty().MinimumLength(Constants.STUDENT_PASSWORD_MINIMIUM_LENGTH).MaximumLength(Constants.STUDENT_PASSWORD_MAX_LENGTH).
                Matches("(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})");
            RuleFor(request => request.m_strUserId).NotEmpty().MaximumLength(Constants.STUDENT_EMAIL_MAX_LENGTH).EmailAddress().Must(u => !string.IsNullOrWhiteSpace(u));
        }
    }
}