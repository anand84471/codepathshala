using FluentValidation;
using StudentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.ValidationHandler
{
    public  class InstructorAccountRegisterValidator: AbstractValidator<InstructorRegisterModel>
    {
        public InstructorAccountRegisterValidator()
        {
            RuleFor(request => request.m_strFirstName).NotEmpty().MaximumLength(Constants.INSTRUCTOR_FIRST_NAME_MAX_LENGTH);
            RuleFor(request => request.m_strLastName).NotNull().MaximumLength(Constants.INSTRUCTOR_LAST_NAME_MAX_LENGTH);
            RuleFor(request => request.m_strPassword).NotEmpty().MinimumLength(Constants.INSTRUCTOR_PASSWORD_MINIMIUM_LENGTH).MaximumLength(Constants.INSTRUCTOR_PASSWORD_MAX_LENGTH).
                Matches("(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})").Must(u => !string.IsNullOrWhiteSpace(u));
            RuleFor(request => request.m_strEmail).NotEmpty().MaximumLength(Constants.INSTRUCTOR_EMAIL_MAX_LENGTH).EmailAddress().Must(u => !string.IsNullOrWhiteSpace(u));
            RuleFor(request => request.m_strPhoneNo).NotEmpty().MaximumLength(Constants.INSTRUCTOR_PHONE_NO_MAX_LENGTH).Must(u => !string.IsNullOrWhiteSpace(u));
        }
    }
}