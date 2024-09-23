// See https://aka.ms/new-console-template for more information

public class Algorithms
{
    public static void Main()
    {
        Console.WriteLine(FindMax([3,7,9,2,5]));
        FizzBuzz();
        Console.WriteLine(ReverseString("hello"));
        Console.WriteLine(IsPalindrome("radar"));
        Console.WriteLine(IsPalindrome("radr"));
        Console.WriteLine(CountVowels("hello world"));
        
        int FindMax(int[] numbers)
        {
            int max = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                {
                    max = numbers[i];
                }
            }
            return max;
        }

        void FizzBuzz()
        {
            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }
        }

        string ReverseString(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        bool IsPalindrome(string str)
        {
            string reversed = ReverseString(str);
            return str == reversed;
        }

        int CountVowels(string str)
        {
            int count = 0;
            string vowels = "aeiou";
            foreach (char c in str)
            {
                if (vowels.Contains(c))
                {
                    count++;
                }
            }
            return count;
        }
    }
}