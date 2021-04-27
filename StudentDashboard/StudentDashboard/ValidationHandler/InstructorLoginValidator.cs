using FluentValidation;
using StudentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.ValidationHandler
{
    public class InstructorLoginValidator: AbstractValidator<InstructorRegisterModel>
    {
        public InstructorLoginValidator()
        {
            RuleFor(request => request.m_strPassword).NotEmpty().MinimumLength(Constants.INSTRUCTOR_PASSWORD_MINIMIUM_LENGTH).MaximumLength(Constants.INSTRUCTOR_PASSWORD_MAX_LENGTH).
                Matches("(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})").Must(u => !string.IsNullOrWhiteSpace(u));
            RuleFor(request => request.m_strEmail).NotEmpty().MaximumLength(Constants.INSTRUCTOR_EMAIL_MAX_LENGTH).EmailAddress().Must(u => !string.IsNullOrWhiteSpace(u));
        }
    }
}