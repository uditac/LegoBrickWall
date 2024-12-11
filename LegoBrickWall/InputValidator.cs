
namespace LegoBrickWall;

public class InputValidator
{
    public static int GetPositiveInteger(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(ErrorMessage.NullSpace);
                continue; // Promptng again
            }

            if (int.TryParse(input, out int result) && result > 0)
            {
                return result; 
            }

            Console.WriteLine(ErrorMessage.EnterPositiveInteger); 
        }
    }
}
