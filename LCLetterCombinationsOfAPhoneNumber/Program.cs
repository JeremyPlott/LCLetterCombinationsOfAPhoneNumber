using System;
using System.Collections.Generic;
using System.Linq;

namespace LCLetterCombinationsOfAPhoneNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, string> keypadCharacterDictionary = new Dictionary<char, string>()
            {
                { '2', "abc"},
                { '3', "def"},
                { '4', "ghi"},
                { '5', "jkl"},
                { '6', "mno"},
                { '7', "pqrs"},
                { '8', "tuv"},
                { '9', "wxyz"},
            };

            string digits = "79";

            var result = LetterCombinations(digits);
            foreach (var combination in result)
            {
                Console.WriteLine(combination);
            }
            Console.WriteLine("~~~~~~~~");

            result = RecursiveCombinations(digits);
            foreach (var combination in result)
            {
                Console.WriteLine(combination);
            }


            //loop-ception solution
            IList<string> LetterCombinations(string digits)
            {
                IList<string> letterCombinations = new List<string>();

                if (string.IsNullOrEmpty(digits)) { return letterCombinations; }

                letterCombinations.Add(string.Empty);
                foreach (char digit in digits)
                {
                    IList<string> currentCombination = new List<string>();
                    foreach (char letter in keypadCharacterDictionary[digit])
                    {
                        foreach (var permutation in letterCombinations)
                        {
                            currentCombination.Add(permutation + letter);
                        }
                    }

                    letterCombinations = currentCombination;
                }

                return letterCombinations;
            }


            //recursive solution
            IList<string> RecursiveCombinations(string digits)
            {
                IList<string> letterCombinations = new List<string>();

                if (string.IsNullOrEmpty(digits)) { return letterCombinations; }

                GetPermutations(string.Empty, digits, 0, letterCombinations);

                return letterCombinations;
            }

            IList<string> GetPermutations(string permutation, string digits, int button, IList<string> letterCombinations)
            {
                if (button == digits.Length)
                {
                    letterCombinations.Add(permutation);
                    return letterCombinations;
                }

                string buttonLetters = keypadCharacterDictionary[digits[button]];

                foreach (var letter in buttonLetters)
                {
                    permutation += letter;
                    GetPermutations(permutation, digits, button + 1, letterCombinations);
                    permutation = permutation.Remove(permutation.Length - 1);
                }

                return letterCombinations;
            }
        }
    }
}
