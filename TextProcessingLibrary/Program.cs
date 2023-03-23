using System;
using System.Collections.Generic;
using System.Linq;

namespace TextProcessingLibrary
{
    public interface IWordFrequency
    {
        string Word { get; }
        int Frequency { get; }
    }

    public class WordFrequency : IWordFrequency
    {
        public string Word { get; set; }
        public int Frequency { get; set; }
    }

    public interface IWordFrequencyAnalyzer
    {
        int CalculateHighestFrequency(string text);
        int CalculateFrequencyForWord(string text, string word);
        IList<IWordFrequency> CalculateMostFrequentNWords(string text, int n);
    }
    public class WordFrequencyAnalyzer : IWordFrequencyAnalyzer
    {
        public int CalculateHighestFrequency(string text)
        {
            var wordFrequency = GetWordFrequency(text);
            return wordFrequency.Any() ? wordFrequency.Max(x => x.Frequency) : 0;
        }

        public int CalculateFrequencyForWord(string text, string word)
        {
            var wordFrequency = GetWordFrequency(text);
            return wordFrequency.SingleOrDefault(x => string.Equals(x.Word, word, StringComparison.OrdinalIgnoreCase))?.Frequency ?? 0;
        }

        public IList<IWordFrequency> CalculateMostFrequentNWords(string text, int n)
        {
            var wordFrequency = GetWordFrequency(text);
            var mostFrequentWords = wordFrequency.OrderByDescending(x => x.Frequency)
                              .ThenBy(x => x.Word, StringComparer.OrdinalIgnoreCase)
                              .Take(n)
                              .ToList<IWordFrequency>();
            return mostFrequentWords;
        }

        private static List<WordFrequency> GetWordFrequency(string text)
        {
            var wordFrequency = new List<WordFrequency>();
            var words = text.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                var finalWord = new string(word.Where(char.IsLetter).ToArray());
                if (string.IsNullOrEmpty(finalWord)) continue;
                var existingWord = wordFrequency.SingleOrDefault(w => string.Equals(w.Word, finalWord, StringComparison.OrdinalIgnoreCase));
                if (existingWord != null)
                {
                    existingWord.Frequency++;
                }
                else
                {
                    wordFrequency.Add(new WordFrequency { Word = finalWord, Frequency = 1 });
                }
            }
            return wordFrequency;
        }
    }
}


namespace TextProcessingLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "The sun shines over the lake";
            var analyzer = new WordFrequencyAnalyzer();

            // Calculate highest frequency
            var highestFrequency = analyzer.CalculateHighestFrequency(text);
            Console.WriteLine($"Highest frequency: {highestFrequency}");

            // Calculate frequency for a specific word
            var wordFrequency = analyzer.CalculateFrequencyForWord(text, "the");
            Console.WriteLine($"Frequency for word 'the': {wordFrequency}");

            // Calculate most frequent n words
            var mostFrequentWords = analyzer.CalculateMostFrequentNWords(text, 3);
            Console.WriteLine($"Most frequent {mostFrequentWords.Count} words:");
            foreach (var word in mostFrequentWords)
            {
                Console.WriteLine($"{word.Word} - {word.Frequency}");
            }
            Console.ReadLine();
        }
    }
}