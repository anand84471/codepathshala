using FluentValidation;
using StudentDashboard.Models.Student;
using System;

namespace StudentDashboard.ValidationHandler
{
    public class StudentAccountRegisterValidator: AbstractValidator<StudentRegisterModal>
    {
        public StudentAccountRegisterValidator()
        {
            RuleFor(request => request.m_strFirstName).NotEmpty().MaximumLength(Constants.STUDENT_FIRST_NAME_MAX_LENGTH);
            RuleFor(request => request.m_strLastName).NotNull().MaximumLength(Constants.STUDENT_LAST_NAME_MAX_LENGTH);
            RuleFor(request => request.m_strPassword).NotEmpty().MinimumLength(Constants.STUDENT_PASSWORD_MINIMIUM_LENGTH).MaximumLength(Constants.STUDENT_PASSWORD_MAX_LENGTH).
                Matches("(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})");
            RuleFor(request => request.m_strEmail).NotEmpty().MaximumLength(Constants.STUDENT_EMAIL_MAX_LENGTH).EmailAddress().Must(u => !string.IsNullOrWhiteSpace(u));
            RuleFor(request => request.m_strPhoneNo).NotEmpty().MaximumLength(Constants.STUDENT_PHONE_NO_MAX_LENGTH).Must(u => !string.IsNullOrWhiteSpace(u));
        }
    }
}