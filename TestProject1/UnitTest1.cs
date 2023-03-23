using TextProcessingLibrary;
namespace TestProject1
{
    public class WordFrequencyAnalyzerTests
    {
        private readonly IWordFrequencyAnalyzer _wordFrequencyAnalyzer;

        public WordFrequencyAnalyzerTests()
        {
            _wordFrequencyAnalyzer = new WordFrequencyAnalyzer();
        }

        [Theory]
        [InlineData("The sun shines over the lake", 2)]
        [InlineData("As long as", 2)]
        public void CalculateHighestFrequency_ReturnsHighestFrequency(string text, int expectedFrequency)
        {
            // Act
            int actualFrequency = _wordFrequencyAnalyzer.CalculateHighestFrequency(text);

            // Assert
            Assert.Equal(expectedFrequency, actualFrequency);
        }

        [Theory]
        [InlineData("The sun shines over the lake", "the", 2)]
        [InlineData("My name is Aditi", "name", 1)]
        public void CalculateFrequencyForWord_ReturnsWordFrequency(string text, string word, int expectedFrequency)
        {
            // Act
            int actualFrequency = _wordFrequencyAnalyzer.CalculateFrequencyForWord(text, word);

            // Assert
            Assert.Equal(expectedFrequency, actualFrequency);
        }
}