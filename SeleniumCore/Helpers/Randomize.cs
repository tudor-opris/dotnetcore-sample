using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCore.Helpers
{
    public static class Randomize
    {
        public static string GenerateRandomPassword()
        {
            var random = new Random();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string specialCharacters = "!@#$%^&*(";

            var randomLetters = new string(Enumerable.Repeat(letters, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            var randomNumbers = new string(Enumerable.Repeat(numbers, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            var randomSpecialCharacters = new string(Enumerable.Repeat(specialCharacters, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomLetters + randomNumbers + randomSpecialCharacters;
        }

        public static int GenerateNumberWithATopLimit(int limit)
        {
            var random = new Random();
            return random.Next(0, limit);
        }

        public static int GenerateNumberBetween(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        public static double GenerateNumberWithNumberOfDigits(int numberOfDigits)
        {
            var multiplier = (Math.Pow(10, numberOfDigits));
            var random = new Random();
            var number = random.NextDouble() * multiplier;
            number = Convert.ToInt32(number);

            return number;
        }

        public static double GenerateNumberWithNumberOfDigitsAndNumberOfDecimals(int numberOfDigits, int numberOfDecimals)
        {
            var multiplier = (Math.Pow(10, numberOfDigits));
            var random = new Random();
            var number = random.NextDouble() * multiplier;
            number = (Math.Round(number, numberOfDecimals));

            return number;
        }

        public static string GenerateRandomStringWithMaxLength(int maxLenght)
        {
            var random = new Random();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var lengthOfString = GenerateNumberBetween(1, maxLenght);

            return new string(Enumerable.Repeat(letters, lengthOfString)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }

    }
}
