using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        //Получение текстового файла. Нужно ввести полный путь к файлу
        string filePath = Console.ReadLine();

        Dictionary<string, int> wordCounts = GetWordCounts(filePath);

        //Проверяем содержит ли словарь какие-либо элементы, если да - создаем файл "output.txt", перечисляя слова в порядке убывания их кол-ва
        if (wordCounts.Any())
        {
            string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "output.txt");

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (KeyValuePair<string, int> v in wordCounts.OrderByDescending(x => x.Value))
                {
                    writer.WriteLine($"{v.Key} - {v.Value}");
                }
            }

            Console.WriteLine($"Результат сохранен в файл {outputFilePath}");
        }
        else
        {
            Console.WriteLine("Файл не содержит слов");
        }

        Console.ReadLine();
    }

    //Метод для получения словаря с количеством вхождений каждого уникального слова в файл
    static Dictionary<string, int> GetWordCounts(string filePath)
    {
        Dictionary<string, int> wordCounts = new Dictionary<string, int>();

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = " ";

                while ((line = reader.ReadLine()) != null)
                {
                    foreach (string word in Regex.Split(line.ToLower(), @"\W+"))
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            if (wordCounts.ContainsKey(word))
                            {
                                wordCounts[word]++;
                            }
                            else
                            {
                                wordCounts.Add(word, 1);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return wordCounts;
    }
}
