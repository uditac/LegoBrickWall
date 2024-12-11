using LegoBrickWall;


public class LegoWall
{
    static Random random = new Random();

    
    public static void Main(string[] args)
    {
       
        ProvideInput();

    }

    /// <summary>
    /// Provide valid input here
    /// </summary>
    public static void ProvideInput()
    {
        do
        {
            int width = InputValidator.GetPositiveInteger("Enter wall width: ");
            int height = InputValidator.GetPositiveInteger("Enter wall height: ");

            int[] brickSizes = { 1, 2, 3, 4, 6, 8, 10, 12 };
            BuildWall(width, height, brickSizes);

            Console.WriteLine("\nDo you want to build another wall? (Yes(y)/No(n))");
        }
        while (Console.ReadLine()?.Trim().ToLower() == "y");
    }

    /// <summary>
    /// Checking the input whether it is a positive integer, checking that the height in the first
    /// and the last two layers are filled, this comes from Rule 2 which needs to be implemented
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="brickSizes"></param>
    public static void BuildWall(int width, int height, int[] brickSizes)
    {
        if (width <= 0 || height <= 0)
        {
            Console.WriteLine(ErrorMessage.WidthHeightPositiveInteger);
            return; 
        }

        for (int layer = 0; layer < height; layer++)
        {
            List<int> layerBricks;

            if (layer <= 2 || layer > height - 2)
            {
                layerBricks = GetExactBricks(width, brickSizes);
            }
            else
            {
                layerBricks = (width % 2 == 0)
                    ? GetRandomBricksThatAddUp(width, brickSizes)
                    : GetExactBricks(width, brickSizes);
            }

            PrintLayer(layerBricks, layer % 2 == 0);
        }
    }

    /// <summary>
    /// if possible getting exact number of bricks for the width, like if the width is 8, then get bricksize 8.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="brickSizes"></param>
    /// <returns></returns>
    public static List<int> GetExactBricks(int width, int[] brickSizes)
    {
        List<int> bricks = new List<int>();
        int remainingWidth = width;

        foreach (var brick in brickSizes.OrderByDescending(b => b))
        {
            while (remainingWidth >= brick)
            {
                bricks.Add(brick);
                remainingWidth -= brick;
            }
        }

        return bricks;
    }


    /// <summary>
    /// Checking what is the bricksize if it 7, then adding a 6+1
    /// </summary>
    /// <param name="width"></param>
    /// <param name="brickSizes"></param>
    /// <returns></returns>
    public static List<int> GetRandomBricksThatAddUp(int width, int[] brickSizes)
    {
        var result = new List<int>();

        if (width == 0)
            return result;

        List<int> availableBricks = brickSizes.Where(b => b <= width).ToList();

        while (width > 0 && availableBricks.Count > 0)
        {
            int brick = availableBricks[random.Next(availableBricks.Count)];
            result.Add(brick);
            width -= brick;

            availableBricks = brickSizes.Where(b => b <= width).ToList();
        }

        return result;
    }

    /// <summary>
    /// printing the layers here, taking the primary ones as I or X and using its alternating bricks as - or .
    /// </summary>
    /// <param name="bricks"></param>
    /// <param name="useAlternate"></param>
   public  static void PrintLayer(List<int> bricks, bool useAlternate)
    {
        char primaryBrickChar = useAlternate ? '|' : 'X';
        char alternateBrickChar = useAlternate ? '.' : '-';

        // Loopng through each brick
        for (int i = 0; i < bricks.Count; i++)
        {
            char brickChar = (i % 2 == 0) ? primaryBrickChar : alternateBrickChar;
            Console.Write(new string(brickChar, bricks[i]));
        }
        Console.WriteLine();
    }
}

