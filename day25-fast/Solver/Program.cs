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
        // I only solved this partially by code.
        // First step was generating the graph in a dot file and visualizing using GraphViz:
        // neato -Tpng graph.dot -o graph.png
        // That immediately showed the three connections that needed to be cut.
        // With my input these were: ptj-qmr, lsv-lxt, dhn-xvh.
        // I then removed these connections from the input and used code to
        // compute the size of the graph segment of the first node and spit out the answer.
        NonDirectedGraph graph = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            var parts = line.Split(':');
            var name = parts[0];
            var other_names = parts[1].Trim().Split(' ');
            var node = graph.GetOrCreate(name);
            foreach (var on in other_names)
            {
                var otherNode = graph.GetOrCreate(on);
                graph.AddEdge(node, otherNode, 1);
            }
        }
        graph.Print();

        // Step 1: analyse the graph.
        File.WriteAllText("graph.dot", graph.ToGraphViz());

        // Step 2: remove the three connections.

        // Step 3: determine sizes of the two segments.
        HashSet<GraphNode> visited = new();
        var n = graph.GetAt("ssr");
        Queue<GraphNode> q = new();
        q.Enqueue(n);
        while (q.Any())
        {
            n = q.Dequeue();
            visited.Add(n);
            var edges = graph.GetEdges(n);
            foreach (var edge in edges)
            {
                if (!visited.Contains(edge.Second))
                {
                    q.Enqueue(edge.Second);
                }
            }
        }

        int total = visited.Count * (graph.Count - visited.Count);
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        // Just push the button.
    }
}
