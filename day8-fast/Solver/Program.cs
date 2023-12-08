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
        string instructions = lines[0];
        Dictionary<string, Tuple<string, string>> nodes = new();
        for (int y = 2; y < lines.Length; y++)
        {
            string line = lines[y];
            string name = line.Split('=')[0].Trim();
            string left = line.Split('(')[1].Split(',')[0].Trim();
            string right = line.Split(',')[1].Split(')')[0].Trim();
            nodes[name] = new Tuple<string, string>(left, right);
        }

        int turn = 0;
        string current_node = "AAA";
        while (current_node != "ZZZ")
        {
            char instruction = instructions[turn % instructions.Length];
            current_node = instruction == 'L' ? nodes[current_node].Item1 : nodes[current_node].Item2;
            turn++;
        }

        Console.WriteLine($"Part 1: {turn}");
    }

    private static void SolvePart2(string[] lines)
    {
        string instructions = lines[0];
        Dictionary<string, Tuple<string, string>> nodes = new();
        for (int y = 2; y < lines.Length; y++)
        {
            string line = lines[y];
            string name = line.Split('=')[0].Trim();
            string left = line.Split('(')[1].Split(',')[0].Trim();
            string right = line.Split(',')[1].Split(')')[0].Trim();
            nodes[name] = new Tuple<string, string>(left, right);
        }

        long turn = 0;
        List<string> current_nodes = nodes.Keys.Where(k => k[2] == 'A').ToList();
        List<string> start_nodes = new(current_nodes);
        List<long> node_cycles = start_nodes.Select(n => 0L).ToList();
        while (node_cycles.Any(c => c == 0))
        {
            char instruction = instructions[(int)(turn % instructions.Length)];
            for (int i = 0; i < current_nodes.Count; i++)
            {
                string current_node = current_nodes[i];
                current_nodes[i] = instruction == 'L' ? nodes[current_node].Item1 : nodes[current_node].Item2;
                if (current_nodes[i][2] == 'Z' && node_cycles[i] == 0)
                {
                    node_cycles[i] = (int)(turn + 1);
                }
            }
            turn++;
        }

        long current_turn = turn;
        do
        {
            current_turn += turn;
        }
        while (node_cycles.Any(c => (current_turn % c) != 0));

        Console.WriteLine($"Part 2: {current_turn}");
    }
}
