using System.Collections.Specialized;
using System.Collections;

namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        int total = 0;
        string[] parts = lines[0].Split(',');
        foreach (var part in parts)
        {
            int hash = 0;
            foreach (char ch in part)
            {
                hash = ((hash + ch) * 17) % 256;
            }
            total += hash;
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        List<OrderedDictionary> boxes = new();
        for (int i = 0; i < 256; i++)
        {
            boxes.Add(new OrderedDictionary());
        }
        string[] parts = lines[0].Split(',');
        foreach (var part in parts)
        {
            int boxIndex;
            string label;
            if (part.Contains('='))
            {
                var subparts = part.Split('=');
                label = subparts[0];
                boxIndex = Hash(subparts[0]);
                int focalLength = int.Parse(subparts[1]);
                boxes[boxIndex][label] = focalLength;
            }
            if (part.Contains('-'))
            {
                var subparts = part.Split('-');
                label = subparts[0];
                boxIndex = Hash(subparts[0]);
                boxes[boxIndex].Remove(label);
            }
        }
        long total = 0;
        for (int i = 0; i < 256; i++)
        {
            var box = boxes[i];
            int slot = 1;
            foreach (DictionaryEntry entry in box)
            {
                total += (i + 1) * slot * (int)entry.Value!;
                slot++;
            }
        }
        Console.WriteLine($"Part 2: {total}");
    }

    private static int Hash(string s)
    {
        int hash = 0;
        foreach (char ch in s)
        {
            hash = ((hash + ch) * 17) % 256;
        }
        return hash;
    }
}
