


namespace AoC;

public class Solver
{
    public int SolvePart1(string[] lines)
    {
        Map map = MapParser.Parse(lines);

        const string StartNodeName = "AAA";
        const string EndNodeName = "ZZZ";

        Node current_node = map.Network[StartNodeName];
        Node end_node = map.Network[EndNodeName];
        int turn = 0;

        while (current_node != end_node)
        {
            Instruction instruction = map.Instructions[turn % map.Instructions.Count];
            current_node = map.GetNextNode(current_node, instruction);
            turn++;
        }

        return turn;
    }

    public long SolvePart2(string[] lines)
    {
        Map map = MapParser.Parse(lines);

        List<long> node_cycle_times = DetermineCycleTimesForEachStartNode(map);
        return DetermineTurnWhereAllGhostsAreAtEndNode(node_cycle_times);
    }

    /// <summary>
    /// Determines for each start node how many turns it takes to cycle back to that node.
    /// </summary>
    private List<long> DetermineCycleTimesForEachStartNode(Map map)
    {
        bool IsStartNode(Node node)
        {
            return node.Name.EndsWith('A');
        }

        bool IsEndNode(Node node)
        {
            return node.Name.EndsWith('Z');
        }

        List<Node> current_nodes = map.Network.Values.Where(IsStartNode).ToList();
        List<long> node_cycle_times = current_nodes.Select(n => 0L).ToList();

        long turn = 0;

        while (node_cycle_times.Any(c => c == 0))
        {
            Instruction instruction = map.Instructions[(int)(turn % map.Instructions.Count)];
            for (int i = 0; i < current_nodes.Count; i++)
            {
                Node node = current_nodes[i];
                current_nodes[i] = map.GetNextNode(node, instruction);
                if (IsEndNode(current_nodes[i]) && node_cycle_times[i] == 0)
                {
                    node_cycle_times[i] = (int)(turn + 1);
                }
            }
            turn++;
        }

        return node_cycle_times;
    }

    private long DetermineTurnWhereAllGhostsAreAtEndNode(List<long> node_cycle_times)
    {
        // Determine on which turn all ghosts are at the end node. This is when all
        // cycles align. Incidentally(?) this is the Lowest Common Multiple of all
        // cycle times.
        return Algorithms.LCM(node_cycle_times);
    }
}