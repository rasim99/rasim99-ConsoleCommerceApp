

namespace Core.Extensions;

public static class CharExtensionscs
{
    public static bool isValidChoose(this char choose)
    {
        if (choose.ToString().ToLower() =="y" || choose.ToString().ToLower() == "n")
        {
          return true;

        }
        return false;
    }
}
