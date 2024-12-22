using HavenHotel.Interfaces;
using System.Text.RegularExpressions;

namespace HavenHotel.Utilities;

public class EmailValidator : IEmailValidator
{
    public bool IsValidEmail(string email)
    {
        var emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z]{2,}\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailRegex);
    }

}
