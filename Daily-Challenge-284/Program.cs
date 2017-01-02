using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;

namespace DailyChallenge284 {
    class Program {

        readonly static List<string> wordList = GetListFromText(@"http://norvig.com/ngrams/enable1.txt");

        static void Main()
        {
            /*
            Software like Swype and SwiftKey lets smartphone users enter text by dragging their finger over 
            the on-screen keyboard, rather than tapping on each letter.

            You'll be given a string of characters representing the letters the user has dragged their finger over.
            For example, if the user wants "rest", the string of input characters might be "resdft" or "resert".
            
            Given the following input strings, find all possible output words 5 characters or longer.

            Input:
            qwertyuytresdftyuioknn
            gijakjthoijerjidsdfnokg
            
            Output:
            queen question
            gaeing garring gathering gating geeing gieing going goring
            */

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine(FindWords("qwertyutresdftyuioknn"));
            watch.Stop();
            Console.WriteLine("Elapsed time: {0}ms", watch.ElapsedMilliseconds);

            watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine(FindWords("gijakjthoijerjidsdfnokg"));
            watch.Stop();
            Console.WriteLine("Elapsed time: {0}ms", watch.ElapsedMilliseconds);

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

            foreach (var s in textFile.Where(p => p.Length > 4))             
                textList.Add(s);             

            return textList;
        }

        static bool CheckWord(string letters, string word)
        {
            if (word[0] == letters[0] && word[word.Length - 1] == letters[letters.Length - 1]) {
                string temp = "";

                for (int i = 0; i < word.Length; i++) {
                    if (letters.Contains(word[i])) {
                        temp += word[i];
                        letters = letters.Remove(0, letters.IndexOf(word[i]));
                    }
                }
                if (temp == word)
                {
                    return true;
                }

                return false;
            }

            return false;            
        }

        static string FindWords(string letters)
        {
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
