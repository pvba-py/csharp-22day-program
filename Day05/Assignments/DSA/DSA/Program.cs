using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewQuestions
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=================================");
                Console.WriteLine(" INTERVIEW QUESTIONS CONSOLE APP");
                Console.WriteLine("=================================");
                Console.WriteLine();

                Console.WriteLine("1. Leaders in Array");
                Console.WriteLine("2. Anagram Check");
                Console.WriteLine("3. Stack Using Queue");
                Console.WriteLine("4. First Repeating Element");
                Console.WriteLine("5. Lucky Ticket (XOR)");
                Console.WriteLine("6. Lonely Element (XOR)");
                Console.WriteLine("7. String Compression");
                Console.WriteLine("8. Missing and Duplicate");
                Console.WriteLine("9. Run All");
                Console.WriteLine("0. Exit");

                Console.Write("\nChoose option: ");
                string choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1": Question1_Leaders(); break;
                    case "2": Question2_Anagram(); break;
                    case "3": Question3_StackUsingQueue(); break;
                    case "4": Question4_FirstRepeating(); break;
                    case "5": Question5_LuckyTicket(); break;
                    case "6": Question6_And_7_LonelyElement(); break;
                    case "7": Question8_StringCompression(); break;
                    case "8": Question9_MissingAndDuplicate(); break;

                    case "9":
                        Question1_Leaders();
                        Question2_Anagram();
                        Question3_StackUsingQueue();
                        Question4_FirstRepeating();
                        Question5_LuckyTicket();
                        Question6_And_7_LonelyElement();
                        Question8_StringCompression();
                        Question9_MissingAndDuplicate();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

                Console.WriteLine("\nPress any key...");
                Console.ReadKey();
            }
        }

        // ================= QUESTIONS =================

        static void Question1_Leaders()
        {
            Console.WriteLine("\nQ1 Leaders");

            int[] arr = { 16, 17, 4, 3, 5, 2 };

            List<int> leaders = new();

            int maxRight = arr[arr.Length - 1];
            leaders.Add(maxRight);

            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i] > maxRight)
                {
                    leaders.Add(arr[i]);
                    maxRight = arr[i];
                }
            }

            leaders.Reverse();
            Console.WriteLine(string.Join(" ", leaders));
        }

        static void Question2_Anagram()
        {
            Console.WriteLine("\nQ2 Anagram");

            string str1 = "Listen";
            string str2 = "Silent";

            Dictionary<char, int> freq = new();

            foreach (char c in str1.ToLower())
                freq[c] = freq.GetValueOrDefault(c, 0) + 1;

            foreach (char c in str2.ToLower())
            {
                if (!freq.ContainsKey(c))
                {
                    Console.WriteLine(false);
                    return;
                }
                freq[c]--;
            }

            Console.WriteLine(freq.Values.All(v => v == 0));
        }

        static void Question3_StackUsingQueue()
        {
            Console.WriteLine("\nQ3 Stack Using Queue");

            StackUsingQueue stack = new();

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());

            stack.Push(40);
            Console.WriteLine(stack.Pop());
        }

        static void Question4_FirstRepeating()
        {
            Console.WriteLine("\nQ4 First Repeating");

            int[] arr = { 10, 5, 3, 4, 3, 5, 6 };

            Dictionary<int, int> map = new();

            int answer = -1;
            int minIndex = int.MaxValue;

            for (int i = 0; i < arr.Length; i++)
            {
                if (map.ContainsKey(arr[i]))
                {
                    if (map[arr[i]] < minIndex)
                    {
                        minIndex = map[arr[i]];
                        answer = arr[i];
                    }
                }
                else
                {
                    map[arr[i]] = i;
                }
            }

            Console.WriteLine(answer);
        }

        static void Question5_LuckyTicket()
        {
            Console.WriteLine("\nQ5 Lucky Ticket");

            int[] ticket = { 7, 3, 5, 3, 5 };

            int result = 0;

            foreach (int num in ticket)
                result ^= num;

            Console.WriteLine(result);
        }

        static void Question6_And_7_LonelyElement()
        {
            Console.WriteLine("\nQ6/Q7 Lonely Element");

            int[] arr = { 4, 1, 2, 1, 2 };

            int result = 0;

            foreach (int num in arr)
                result ^= num;

            Console.WriteLine(result);
        }

        static void Question8_StringCompression()
        {
            Console.WriteLine("\nQ8 String Compression");

            string str = "aabcccccaaa";

            StringBuilder sb = new();
            int count = 1;

            for (int i = 1; i <= str.Length; i++)
            {
                if (i < str.Length && str[i] == str[i - 1])
                {
                    count++;
                }
                else
                {
                    sb.Append(str[i - 1]);
                    sb.Append(count);
                    count = 1;
                }
            }

            string compressed = sb.ToString();

            Console.WriteLine(compressed.Length < str.Length ? compressed : str);
        }

        static void Question9_MissingAndDuplicate()
        {
            Console.WriteLine("\nQ9 Missing And Duplicate");

            int[] arr = { 4, 3, 6, 2, 1, 1 };

            Dictionary<int, int> freq = new();

            foreach (int num in arr)
                freq[num] = freq.GetValueOrDefault(num, 0) + 1;

            int duplicate = -1;
            int missing = -1;

            for (int i = 1; i <= arr.Length; i++)
            {
                if (!freq.ContainsKey(i))
                    missing = i;
                else if (freq[i] > 1)
                    duplicate = i;
            }

            Console.WriteLine($"Duplicate = {duplicate}");
            Console.WriteLine($"Missing = {missing}");
        }
    }

    class StackUsingQueue
    {

        private Queue<int> queue = new();

        public void Push(int value)
        {
            queue.Enqueue(value);

            for (int i = 0; i < queue.Count - 1; i++)
                queue.Enqueue(queue.Dequeue());
        }

        public int Pop()
        {
            return queue.Dequeue();
        }
        public int Peek()
        {
            return queue.Peek();
        }


    }
}