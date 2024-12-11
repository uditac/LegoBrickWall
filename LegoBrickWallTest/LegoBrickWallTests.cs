
namespace LegoBrickWall.UnitTests;

public class LegoBrickWallTests
{

    [Theory]
    [InlineData(10, 5, new[] { 1, 2, 3, 4, 6, 8, 10, 12 })] // Valid input case
    [InlineData(8, 3, new[] { 1, 2, 3, 4, 6, 8, 10, 12 })]  // Another valid input case
    [InlineData(0, 5, new[] { 1, 2, 3, 4, 6, 8, 10, 12 })]  // Edge case: zero width
    [InlineData(10, 0, new[] { 1, 2, 3, 4, 6, 8, 10, 12 })] // Edge case: zero height
    [InlineData(-1, 5, new[] { 1, 2, 3, 4, 6, 8, 10, 12 })] // Edge case: negative width
    [InlineData(5, -1, new[] { 1, 2, 3, 4, 6, 8, 10, 12 })] // Edge case: negative height
    [InlineData(0, -1, new[] { 1, 2, 3, 4, 6, 8, 10, 12 })] // Edge case: negative height
    [InlineData(476, 345, new[] { 1, 2, 3, 4, 6, 8, 10, 12 })] // Edge case: negative height
    public void WhenWidthOrHeightIsZeroOrNegative_BuildWall_ReturnFalse(int width, int height, int[] brickSizes)
    {
        // Arrange
        using (var outputWriter = new StringWriter())
        {
            //Arrange

            Console.SetOut(outputWriter);

            // Act
            LegoWall.BuildWall(width, height, brickSizes);

            // Assert
            string consoleOutput = outputWriter.ToString();
            if (width <= 0 || height <= 0)
            {
                Assert.Contains(ErrorMessage.WidthHeightPositiveInteger, consoleOutput);
            }
            else

            {
                Assert.False(string.IsNullOrWhiteSpace(consoleOutput));
                Assert.Contains("|", consoleOutput);


            }
        }
    }


    [Fact]
    public void WhenOutputIsInvalidPattern_BuildWall_ReturnFalse()
    {
        // Arrange
        using (var outputWriter = new StringWriter())
        {
            Console.SetOut(outputWriter);
            int width = 15;
            int height = 3;
            int[] brickSizes = { 5, 7, 3 };

            // Act
            LegoWall.BuildWall(width, height, brickSizes);

            // Assert
            string consoleOutput = outputWriter.ToString();
            Assert.DoesNotContain("A", consoleOutput);
            Assert.DoesNotContain("D", consoleOutput);
        }
    }


    [Fact]
    public void WhenOutputIsValidPattern_BuildWall_ReturnTrue()
    {
        // Arrange
        using (var outputWriter = new StringWriter())
        {
            Console.SetOut(outputWriter);
            int width = 15;
            int height = 3;
            int[] brickSizes = { 5, 7, 3 };

            // Act
            LegoWall.BuildWall(width, height, brickSizes);

            // Assert
            string consoleOutput = outputWriter.ToString();


            Assert.Contains("X", consoleOutput);
            Assert.Contains("|", consoleOutput);
            Assert.False(string.IsNullOrWhiteSpace(consoleOutput));
        }

    }
}