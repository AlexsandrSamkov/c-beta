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
            var key = new char();
            Console.WriteLine(">Enter name:");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Your name was not found"); 
                return;
            }
            key = 'n';
            using (var sr = new StreamReader("./names.txt", System.Text.Encoding.Default))
            {
                while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                { 
                    if (line == name)
                    {
                        key = 'y';
                        break;
                    }
                    if (LevenshteinDistance(name, line) < 2)
                    { 
                        Console.WriteLine("Did you mean “" + line + "”? Y/N"); 
                        while (true) 
                        { 
                            key = Console.ReadKey().KeyChar; 
                            if (key == 'y' | key == 'Y' | key == 'n' | key == 'N') 
                            { 
                                Console.WriteLine(); 
                                break;
                            }
                        } 
                        if (key == 'y' | key == 'Y') 
                            break;
                    }
                }
            }
            if (key == 'y' | key == 'Y')
                Console.WriteLine("Hello, " + name + "!");
            if (key == 'n' | key == 'N')
                Console.WriteLine("Your name was not found.");
        }
       static int LevenshteinDistance(string string1, string string2)
       {
            var diff = new int();
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
