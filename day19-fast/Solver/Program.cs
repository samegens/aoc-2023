namespace AoC;

class BinaryTreeNode
{
    public Dictionary<char, Range> Ranges = new();
    public BinaryTreeNode? Left;
    public BinaryTreeNode? Right;

    public bool? IsAccepted = null;

    public IEnumerable<BinaryTreeNode> GetAcceptedNodes()
    {
        if (IsAccepted == true) yield return this;

        if (Left != null)
            foreach (var node in Left.GetAcceptedNodes())
                yield return node;

        if (Right != null)
            foreach (var node in Right.GetAcceptedNodes())
                yield return node;
    }
}

class Rule
{
    public string Name = "";
    public List<string> Condititions = new();

    public static bool ConditionMatches(string condition, Part part)
    {
        if (!condition.Contains(':')) return true;

        var var_name = condition.Split(':')[0][0];
        var op = condition.Split(':')[0][1];
        int val = int.Parse(condition.Split(':')[0][2..]);

        switch (op)
        {
            case '>': return part.Props[var_name] > val;
            case '<': return part.Props[var_name] < val;
            default: throw new NotImplementedException();
        }
    }
}

class Part
{
    public Dictionary<char, int> Props = new()
    {
        ['x'] = 0,
        ['m'] = 0,
        ['a'] = 0,
        ['s'] = 0
    };

    internal void Print()
    {
        var s = string.Join(',', Props.Select(kv => $"{kv.Key}={kv.Value}"));
        Console.WriteLine($"{s}");
    }
}

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
        bool readingParts = false;
        List<Part> parts = new();
        Dictionary<string, Rule> rules = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            if (string.IsNullOrEmpty(line))
            {
                readingParts = true;
            }
            else if (readingParts)
            {
                line = line[1..(line.Length - 1)];
                Part part = new();
                foreach (var p in line.Split(','))
                {
                    part.Props[p.Split('=')[0][0]] = int.Parse(p.Split('=')[1]);
                }
                parts.Add(part);
            }
            else
            {
                var name = line.Split('{')[0];
                Rule rule = new() { Name = name };
                line = line.Split('{')[1].TrimEnd('}');
                rule.Condititions = line.Split(',').ToList();
                rules[name] = rule;
            }
        }

        List<Part> accepted = new();
        foreach (var part in parts)
        {
            string rule_name = "in";
            while (rule_name != "A" && rule_name != "R")
            {
                Rule rule = rules[rule_name];
                foreach (string condition in rule.Condititions)
                {
                    if (Rule.ConditionMatches(condition, part))
                    {
                        rule_name = condition.Split(':').Last();
                        break;
                    }
                }
            }
            if (rule_name == "A") accepted.Add(part);
        }

        int total = accepted.SelectMany(p => p.Props.Values).Sum();

        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        bool readingParts = false;
        List<Part> parts = new();
        Dictionary<string, Rule> rules = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            if (string.IsNullOrEmpty(line))
            {
                readingParts = true;
            }
            else if (readingParts)
            {
                line = line[1..(line.Length - 1)];
                Part part = new();
                foreach (var p in line.Split(','))
                {
                    part.Props[p.Split('=')[0][0]] = int.Parse(p.Split('=')[1]);
                }
                parts.Add(part);
            }
            else
            {
                var name = line.Split('{')[0];
                Rule rule = new() { Name = name };
                line = line.Split('{')[1].TrimEnd('}');
                rule.Condititions = line.Split(',').ToList();
                rules[name] = rule;
            }
        }

        var tree = new BinaryTreeNode();
        tree.Ranges.Add('x', new Range(1, 4000));
        tree.Ranges.Add('m', new Range(1, 4000));
        tree.Ranges.Add('a', new Range(1, 4000));
        tree.Ranges.Add('s', new Range(1, 4000));

        Queue<Tuple<Rule, int, BinaryTreeNode>> q = new();
        q.Enqueue(new Tuple<Rule, int, BinaryTreeNode>(rules["in"], 0, tree));
        while (q.Any())
        {
            var t = q.Dequeue();
            var rule = t.Item1;
            var conditionIndex = t.Item2;
            var treeNode = t.Item3;
            var condition = rule.Condititions[conditionIndex];
            if (!condition.Contains(':'))
            {
                if (condition == "A")
                {
                    treeNode.IsAccepted = true;
                }
                else if (condition == "R")
                {
                    treeNode.IsAccepted = false;
                }
                else
                {
                    q.Enqueue(new Tuple<Rule, int, BinaryTreeNode>(rules[condition], 0, treeNode));
                }
            }
            else
            {
                var ruleName = condition.Split(':')[1];
                var prop = condition[0];
                var op = condition[1];
                var value = int.Parse(condition.Split(':')[0][2..]);

                var firstRange = op == '<' ? new Range(treeNode.Ranges[prop].Start, value - 1) : new Range(value + 1, treeNode.Ranges[prop].End);
                var secondRange = op == '<' ? new Range(value, treeNode.Ranges[prop].End) : new Range(treeNode.Ranges[prop].Start, value);

                treeNode.Left = new BinaryTreeNode()
                {
                    Ranges = new(treeNode.Ranges)
                };
                treeNode.Left.Ranges[prop] = firstRange;
                if (ruleName == "A")
                {
                    treeNode.Left.IsAccepted = true;
                }
                else if (ruleName == "R")
                {
                    treeNode.Left.IsAccepted = false;
                }
                else
                {
                    q.Enqueue(new Tuple<Rule, int, BinaryTreeNode>(rules[ruleName], 0, treeNode.Left));
                }

                treeNode.Right = new BinaryTreeNode()
                {
                    Ranges = new(treeNode.Ranges)
                };
                treeNode.Right.Ranges[prop] = secondRange;
                q.Enqueue(new Tuple<Rule, int, BinaryTreeNode>(rule, conditionIndex + 1, treeNode.Right));
            }
        }

        long total = 0;
        var nodes = tree.GetAcceptedNodes();
        foreach (var node in nodes)
        {
            long subtotal = node.Ranges['x'].End - node.Ranges['x'].Start + 1;
            subtotal *= node.Ranges['m'].End - node.Ranges['m'].Start + 1;
            subtotal *= node.Ranges['a'].End - node.Ranges['a'].Start + 1;
            subtotal *= node.Ranges['s'].End - node.Ranges['s'].Start + 1;
            total += subtotal;
        }
        Console.WriteLine($"Part 2: {total}");
    }
}
