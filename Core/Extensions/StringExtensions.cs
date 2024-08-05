
using System.Text.RegularExpressions;

namespace Core.Extensions;

public static class StringExtensions
{
    public static bool isValidEmail(this string email)
    {
        if (Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]{4,}@(gmail\.com|[a-zA-Z0-9-]+\.edu\.com|hotmail\.ru|[a-zA-Z0-9-]+\.edu\.az)$"))
        {
            return true;
        }
        return false;
    }
    
    public static bool isValidPassword(this string password)
    {
        if (password.Length>7)
        {
            return true;
        }
        return false;
    }
    
    public static bool isValidSerialNumber(this string serialNumber)
    {
        if (Regex.IsMatch(serialNumber, @"^[A-F][A-F][1-9]\d{4}$"))
        {
            return true;
        }
        return false;
    } 
    public static bool isValidPin(this string pin)
    {
        if (Regex.IsMatch(pin, @"^[1-9][a-zA-Z0-9]{6}$"))
        {
            return true;
        }
        return false;
    } 
    
    public static bool isValidPhoneNumber(this string phoneNumber)
    {
        string pattern = @"^(\+994 (50|51|10|70|77|55|99) [2-9]\d{2} \d{2} \d{2}|0(50|51|10|70|77|55|99) [2-9]\d{2} \d{2} \d{2})$";


        if (Regex.IsMatch(phoneNumber, pattern))
        {
            return true;
        }
        return false;
    }



}
