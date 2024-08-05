
using System.Threading.Channels;

namespace Core.Constants;

public static class Messages
{
    public static void InputMessage(string title) => Console.WriteLine($" 🧐 Input {title}");
    public static void InvalidInputMessage (string title) =>Console.WriteLine($"😠 {title} invalid!!");
    public static void WrongInputMessage (string title) =>Console.WriteLine($"🫷🏿 {title} wrongg!!!");
    public static void ErrorOccuredMessage() => Console.WriteLine(" 🤯 Error Ocuredd !!!");
    public static void SucceededMessage(string title,string operation) => Console.WriteLine($"🥰 {title} successfully {operation}");
    public static void WantToContinueMessage(string title) => Console.WriteLine($"😒 Do you want go {title}");
    public static void AlreadyExistMessage(string title) => Console.WriteLine($" 😉 {title} alreday exist");
    public static void WelcomeMessage(string title) => Console.WriteLine($" {title} Wellcome!");
    public static void NotFoundMessage(string title) => Console.WriteLine($" {title} not found!  🤣🤣🤣");
    public static void WantToCompleted(string title,string operation) => Console.WriteLine($" 🤩 want to {operation} {title}? yes or not");
    public static void GreaterInputMessage(string title,string value) => Console.WriteLine($" 😠🧐{title} must be great {value} ");

}
