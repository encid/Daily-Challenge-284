using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;

namespace DailyChallenge284 {
    class Program {

        public static List<string> wordList = GetListFromText(@"http://norvig.com/ngrams/enable1.txt");

        static void Main()
        {
            Console.WriteLine(FindWords("qwertyuytresdftyuioknn"));
            Console.WriteLine(FindWords("gijakjthoijerjidsdfnokg"));

            Console.ReadLine();
        }

        static IEnumerable<string> ReadTextLines(string textURL)
        {
            var webRequest = WebRequest.Create(textURL);

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content)) {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                    yield return line;
            }            
        }
     
        static List<string> GetListFromText(string textURL)
        {
            var textList = new List<string>();            
            var textFile = ReadTextLines(textURL);

            foreach (var s in textFile) 
                if (s.Length > 4) textList.Add(s);             

            return textList;
        }

        static bool CheckWord(string letters, string word)
        {
            if (word[0] == letters[0] & word[word.Length - 1] == letters[letters.Length - 1]) {
                string temp = "";
                string remainingLetters = letters;

                for (int i = 0; i < word.Length; i++) {
                    if (remainingLetters.Contains(word[i])) {
                        temp += word[i];
                        remainingLetters = TrimLetters(remainingLetters, word[i]);
                    }
                }
                if (temp == word) {
                    return true;
                }
                return false;
            }

            return false;            
        }

        static string TrimLetters(string letters, char letter)
        {
            string temp = "";

            for (int i = 0; i < letters.Length; i++) {
                if (letters.IndexOf(letter) != -1) {
                    temp = letters.Remove(0, letters.IndexOf(letter));
                    break;
                }
            }

            if (temp != letters) {
                return temp;
            }

            return letters;
        }

        static string FindWords(string letters)
        {
            var words = new List<string>();
            string result = "";

            foreach (var word in wordList) {
                if (CheckWord(letters, word)) {
                    result += word + " ";
                }
            }

            return result;
        }

    }
}
