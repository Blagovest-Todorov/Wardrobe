using System;
using System.Collections.Generic;

namespace Wardrobe
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, int>> clothes = new
               Dictionary<string, Dictionary<string, int>>();
            
            int countLines = int.Parse(Console.ReadLine());

            for (int i = 0; i < countLines; i++)
            {
                string currLine = Console.ReadLine();

                if (currLine.Contains(" -> "))  //// "{color} -> {item1},{item2},{item3}…"
                {
                    string[] data = currLine.Split(" -> ");
                    string color = data[0];   // ColorKey
                    string clothesItems = data[1];
                    string[] dresses = clothesItems
                        .Split(",", StringSplitOptions.RemoveEmptyEntries);

                    FillDictionary(clothes, dresses, color);                   
                }
                else
                {
                    break;
                }
            }

            string anotherCommand = Console.ReadLine();
            string[] commandData = anotherCommand
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            bool isCommandValid = IsCommandValid(anotherCommand, commandData);

            if (isCommandValid == false)
            {
                PrintClothesIfCommandInvalid(clothes);
            }
            else if (isCommandValid == true)// "{color} {clothing}"
            {    
                string currColor = commandData[0];
                string currClothe = commandData[1];
                
                if (clothes.ContainsKey(currColor) && clothes[currColor].ContainsKey(currClothe))
                {
                    PrintClothes(clothes, currColor, currClothe);                    
                }
                else
                {
                    PrintClothesIfCommandInvalid(clothes);
                }
            }            
        }

        private static bool IsCommandValid(string anotherCommand, string[] commandData)
        {
            return anotherCommand.Contains(" ") && commandData.Length == 2;
        }

        private static void PrintClothes(Dictionary<string, Dictionary<string, int>> clothes, string currColor, string currClothe)
        {
            foreach (var color in clothes)  // kvp = Color
            {
                Console.WriteLine($"{color.Key} clothes:");

                foreach (var item in color.Value)   // color.Value - collection ,incide Dictionary
                {
                    if (item.Key == currClothe && color.Key == currColor)
                    {
                        Console.WriteLine($"* {item.Key} - {item.Value} (found!)");
                        continue;
                    }

                    Console.WriteLine($"* {item.Key} - {item.Value}");  // how to do (found!)
                }
            }
        }

        private static void FillDictionary(Dictionary<string, Dictionary<string, int>> clothes,
           string[] dresses, string color)
        {
            for (int j = 0; j < dresses.Length; j++)
            {
                string currDress = dresses[j];

                if (!clothes.ContainsKey(color))
                {
                    clothes.Add(color, new Dictionary<string, int>());
                }

                if (!clothes[color].ContainsKey(currDress))
                {
                    clothes[color].Add(currDress, 0);
                }

                clothes[color][currDress]++;
            }
        }

        private static void PrintClothesIfCommandInvalid(Dictionary<string, Dictionary<string, int>> clothes)
        {
            foreach (var color in clothes)  // kvp = Color
            {
                Console.WriteLine($"{color.Key} clothes:");

                foreach (var item in color.Value)   // color.Value - collection ,incide Dictionary
                {
                    Console.WriteLine($"* {item.Key} - {item.Value}"); 
                }
            }  
        }
    }
}
