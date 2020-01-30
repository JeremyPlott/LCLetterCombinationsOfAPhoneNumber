//this dictionary will be used in both solutions.
//it's fine to keep it with characters and strings because we can iterate and index strings,
//and iterating/indexing a string results in a character data type.
//this removes unnecessary parsing to int.
using System.Collections.Generic;

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

IList<string> LetterCombinations(string digits)
{
	//we will store all of our permutations here
	IList<string> letterCombinations = new List<string>();

	//get rid of edge cases with no digits
	if (string.IsNullOrEmpty(digits)) { return letterCombinations; }

	//there has to be a single empty value in our answer collection for this to work
	letterCombinations.Add(string.Empty);

	//for every phone button in our string of buttons...
	foreach (char digit in digits)
	{
		//this will track our current permutation
		IList<string> currentCombination = new List<string>();

		//for every letter on that phone button...
		foreach (char letter in keypadCharacterDictionary[digit])
		{
			//...add the letter.

			//on the first loop through it will add the letters of button 1.
			//now the letterCombinations collection has that many letters in it (ex: a, b, c). 
			//subsequent passes will add their letters to each of the previous buttons letters.
			//we had to create this list with the single empty string to start with so it would have 
			//one member, thus adding each letter of the first button once.

			foreach (var permutation in letterCombinations)
			{
				currentCombination.Add(permutation + letter);
			}
		}

		//we reach this point after having added all of the first buttons letters once,
		//because we instantiated the letterCombinations collection with a single empty string.
		//now on subsequent loops, letterCombinations will have 'a, b, c' in it, and the next button
		//will add all of its letters to each of those letters. etc. etc.
		letterCombinations = currentCombination;

		//step through this code in your editor if it's hard to understand the logic.
		//it still makes my head hurt a little to think about how the loops are connected.
	}

	return letterCombinations;
}

//~~~~~~~~~~~~~~~~~~
//Recursive Solution
//~~~~~~~~~~~~~~~~~~

IList<string> RecursiveCombinations(string digits)
{
	//we will store our solutions in this string collection, and will pass this in to our method
	IList<string> letterCombinations = new List<string>();

	//get rid of edge cases with empty inputs
	if (string.IsNullOrEmpty(digits)) { return letterCombinations; }

	//calling the recursive function which is defined below
	GetPermutations(string.Empty, digits, 0, letterCombinations);

	//return the final collection that we get from the resursive method
	return letterCombinations;
}

//recursive method
IList<string> GetPermutations(string permutation, string digits, int button, IList<string> letterCombinations)
{
	//this is our break condition. When we have gone through every phone button in the input,
	//our permutation is finished, so we add it to the result collection and return that permutation.
	//the return is required because when we call this method inside itself later, we need it
	//to return something and end.
	//this will make sense later.
	if (button == digits.Length)
	{
		letterCombinations.Add(permutation);
		return letterCombinations;
	}

	//this variable is here to add readability.
	//it is taking the character of the provided digit string at the index of [button],
	//and using that character as the key to get the values from our dictionary.
	string buttonLetters = keypadCharacterDictionary[digits[button]];

	foreach (var letter in buttonLetters)
	{
		//for every letter on that button, we want to add it to our existing permutation
		permutation += letter;

		//we have added a single letter from that button, now we have to add a letter from the
		//next button. If it is the last button our if statement at the start will catch it.
		//we call our recursive function again, but this time we pass in our permutation that we
		//just added a letter to and increment our button index.

		//if you use button++ here you'll get a stack overflow
		GetPermutations(permutation, digits, button + 1, letterCombinations);

		//the method will keep looping deeper into itself, adding letters until the break condition at the top is hit.
		//when that happens, the method call directly above this will return a completed permutation.
		//the line below will remove the last letter from our completed permutation, and then
		//we move on to the next letter in the foreach loop above.

		permutation = permutation.Remove(permutation.Length - 1);

		//think of it this way:
		//take the final button in an input. We have three letters on it, and each must be added once.
		//if we are at the last button, we need to add a letter, store that finished set in our solution collection,
		//and then remove the last letter added so we can add in the next letter on that button.

		//so if we then take that logic and modify it to account for the previous buttons,
		//we still need to do something to every letter on the button so we start with the foreach,
		//but we need to move to the next button after adding a letter once.
		//this means you add the letter to the permutation first, and then need to make the
		//recursive call. The two things that have to change between calls are the current permutation
		//and the button we are adding letters from. Hence we pass in the modified string and increment the button,
		//then once we have reached the last button we can let the initial logic play out.
		//when the final button has finished its foreach, the foreach of the previous button will
		//move on to the next letter and repeat the process until every button has finished
		//its foreach loop.

		//figure out what the last one in your set has to do since it is what we want to return,
		//then modify that to make sure your data reaches the last set in the form you need.
	}

	//once our layers of foreach have finished we can return the complete set.
	return letterCombinations;
}