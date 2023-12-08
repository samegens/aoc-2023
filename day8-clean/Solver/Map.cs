
namespace AoC;

public class Map
{
    public Map(IEnumerable<Instruction> instructions, IEnumerable<Node> nodes)
    {
        Instructions = new(instructions);
        foreach (Node node in nodes)
        {
            Network[node.Name] = node;
        }
    }

    public List<Instruction> Instructions { get; private set; }

    public Dictionary<string, Node> Network { get; } = new();

    public Node GetNextNode(Node node, Instruction instruction)
    {
        return instruction == Instruction.Left ? Network[node.Left] : Network[node.Right];
    }
}