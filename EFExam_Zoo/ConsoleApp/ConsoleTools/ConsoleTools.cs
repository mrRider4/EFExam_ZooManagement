namespace EFExam_Zoo.ConsooleApp.ConsoolTools;

public class ConsoleTools
{
    public void Write(string ToWrite, bool hasNewLine = true)
    {
        ToWrite += hasNewLine ? "\n" : "";
        Console.Write(ToWrite);
    }

    public string GetStringFromUser(int? maxLength=null)
    {
        string result = "";
        result = ReadLine();
        if (maxLength != null && result.Length > maxLength)
        {
            Write($"The input text can have {maxLength} characters\nTry again :");
            return GetStringFromUser(maxLength);
        }
        if (!string.IsNullOrWhiteSpace(result)) return result;
        Write("Write something :");
        return GetStringFromUser(maxLength);
    }

    public string ReadLine()
    {
        return Console.ReadLine();
    }

    public int GetIntFromUser(string? input = null)
    {
        int result;
        input ??= Console.ReadLine();
        if (int.TryParse(input, out result)) return result;
        else
        {
            Console.WriteLine("Wrong entry.please enter integer number");
            return GetIntFromUser();
        }
    }

    public int GetIntFromUser(int min, string? input = null)
    {
        int result;
        input ??= Console.ReadLine();
        if (int.TryParse(input, out result) && result >= min) return result;
        else
        {
            Console.WriteLine($"Wrong entry.please enter integer number bigger than <{min - 1}>");
            return GetIntFromUser(min);
        }
    }

    public int GetIntFromUser(int min, int max, string? input = null)
    {
        int result;
        input ??= Console.ReadLine();
        if (int.TryParse(input, out result) && result <= max && result >= min) return result;
        else
        {
            Console.WriteLine($"Wrong entry.please enter integer number from <{min}> to <{max}>");
            return GetIntFromUser(min, max);
        }
    }

    public string ShowAsNumericList(List<string> list)
    {
        string result = "";
        for (int i = 0; i < list.Count; i++)
        {
                result += $"[{i + 1}] {list[i]}";
                if (i < list.Count() - 1) result += "\n";
        }
        return result;
    }

    public bool HaveToReturn(string input, string keyWord = "exit")
    {
        if (input.Trim() == keyWord.Trim()) return true;
        return false;
    }

    public void ReadKey()
    {
        Console.ReadKey();
    }

    public void Clear()
    {
        Console.Clear();
    }
}