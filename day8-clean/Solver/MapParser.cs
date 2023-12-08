


namespace AoC;

public static class MapParser
{
    public static Map Parse(string[] lines)
    {
        IEnumerable<Instruction> instructions = ParseInstructions(lines[0]);
        return new Map(instructions, ParseNodes(lines.Skip(2)));
    }

    private static IEnumerable<Instruction> ParseInstructions(string line)
    {
        return line.Select(CharToInstruction);
    }

    private static Instruction CharToInstruction(char ch)
    {
        return ch == 'L' ? Instruction.Left : Instruction.Right;
    }

    private static IEnumerable<Node> ParseNodes(IEnumerable<string> lines)
    {
        return lines.Select(ParseNode);
    }

    private static Node ParseNode(string line)
    {
        string name = line.Split('=')[0].Trim();
        string left = line.Split('(')[1].Split(',')[0].Trim();
        string right = line.Split(',')[1].Split(')')[0].Trim();
        return new Node(name: name, left: left, right: right);
    }
}