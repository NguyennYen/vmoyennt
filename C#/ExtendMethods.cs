using System.Collections.Generic;

public static class ExtendMethods
{
    public static string ReplaceField(this string inputString, Dictionary<string, string> listValue)
    {
        string outputString = inputString;

        foreach (KeyValuePair<string, string> item in listValue)
            outputString = outputString.Replace(item.Key, item.Value);

        return outputString;
    }
}
