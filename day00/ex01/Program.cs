using System;
using System.IO;
namespace ex01
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = string.Empty;
            var line = string.Empty;
            char key;
            while (true)
            {
                Console.WriteLine(">Enter name:");
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Your name was not found");
                    continue;
                }
                key = 'n';
                using (var sr = new StreamReader("/Users/aleksandrsamkov/RiderProjects/ex01/ex01/names.txt", System.Text.Encoding.Default))
                {
                    
                    while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                    {
                        if (LevenshteinDistance(name, line) < 3)
                        {
                            Console.WriteLine("Did you mean “" + line + "”? Y/N");
                            while (true)
                            {
                                key = Console.ReadKey().KeyChar;
                                if (key == 'y' | key == 'Y' | key == 'n' | key == 'N')
                                    break;
                            }
                            if (key == 'y' | key == 'Y')
                                break;
                        }
                    }
                }
                if (key == 'n' | key == 'N')
                    Console.WriteLine("Your name was not found.");
                Console.ReadKey();
            }
        }
       static int LevenshteinDistance(string string1, string string2)
        {
            if (string1 == null) throw new ArgumentNullException("string1");
            if (string2 == null) throw new ArgumentNullException("string2");
            int diff;
            int[,] m = new int[string1.Length + 1, string2.Length + 1];

            for (int i = 0; i <= string1.Length; i++) { m[i, 0] = i; }
            for (int j = 0; j <= string2.Length; j++) { m[0, j] = j; }

            for (int i = 1; i <= string1.Length; i++)
            {
                for (int j = 1; j <= string2.Length; j++)
                {
                    diff = (string1[i - 1] == string2[j - 1]) ? 0 : 1;

                    m[i, j] = Math.Min(Math.Min(m[i - 1, j] + 1, m[i, j - 1] + 1), m[i - 1, j - 1] + diff);
                }
            }
            return m[string1.Length, string2.Length];
        }
    }
}