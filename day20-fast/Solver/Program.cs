namespace AoC;

abstract class Module
{
    public List<string> Destinations = new();
    public string Name = "";
    public int NrPulses = 0;

    public abstract bool? Trigger(string origin, bool isHigh);
}

class FlipFlop : Module
{
    bool State;

    public override bool? Trigger(string origin, bool isHigh)
    {
        if (isHigh) return null;
        State = !State;
        return State;
    }
}

class Conjunction : Module
{
    public Dictionary<string, bool> LastPulse { get; private set; } = new();

    public override bool? Trigger(string origin, bool isHigh)
    {
        LastPulse[origin] = isHigh;

        return !LastPulse.All(p => p.Value == true);
    }
}

class Broadcast : Module
{
    public override bool? Trigger(string origin, bool isHigh)
    {
        return isHigh;
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
        Dictionary<string, Module> modules = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            var parts = line.Split(" -> ");
            var name = parts[0];
            var outputs = parts[1].Split(", ");
            if (name[0] == '%')
            {
                var module = new FlipFlop();
                module.Name = name[1..];
                module.Destinations = outputs.ToList();
                modules.Add(module.Name, module);
            }
            else if (name[0] == '&')
            {
                var module = new Conjunction();
                module.Name = name[1..];
                module.Destinations = outputs.ToList();
                modules.Add(module.Name, module);
            }
            else
            {
                var module = new Broadcast();
                module.Name = name;
                module.Destinations = outputs.ToList();
                modules.Add(module.Name, module);
            }
        }

        var conjunctions = modules.Values.Where(m => m is Conjunction).Select(m => m as Conjunction);
        foreach (var conjunction in conjunctions.Cast<Conjunction>())
        {
            foreach (var module in modules.Values)
            {
                if (module.Destinations.Contains(conjunction.Name))
                {
                    conjunction.LastPulse[module.Name] = false;
                }
            }
        }

        long highCount = 0;
        long lowCount = 0;
        for (int i = 0; i < 1000; i++)
        {
            lowCount++;     //button
            var module = modules["broadcaster"];
            Queue<Tuple<Module, string, bool>> q = new();
            q.Enqueue(new Tuple<Module, string, bool>(module, "button", false));
            while (q.Any())
            {
                var t = q.Dequeue();
                module = t.Item1;
                string origin = t.Item2;
                bool pulse = t.Item3;
                bool? resultPulse = module.Trigger(origin, pulse);
                if (resultPulse != null)
                {
                    foreach (var moduleName in module.Destinations)
                    {
                        if (resultPulse.Value)
                        {
                            highCount++;
                        }
                        else
                        {
                            lowCount++;
                        }
                        if (modules.ContainsKey(moduleName))
                        {
                            var nextModule = modules[moduleName];
                            q.Enqueue(new Tuple<Module, string, bool>(nextModule, module.Name, resultPulse.Value));
                        }
                    }
                }
            }
        }

        long total = highCount * lowCount;
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        Dictionary<string, Module> modules = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            var parts = line.Split(" -> ");
            var name = parts[0];
            var outputs = parts[1].Split(", ");
            if (name[0] == '%')
            {
                var module = new FlipFlop();
                module.Name = name[1..];
                module.Destinations = outputs.ToList();
                modules.Add(module.Name, module);
            }
            else if (name[0] == '&')
            {
                var module = new Conjunction();
                module.Name = name[1..];
                module.Destinations = outputs.ToList();
                modules.Add(module.Name, module);
            }
            else
            {
                var module = new Broadcast();
                module.Name = name;
                module.Destinations = outputs.ToList();
                modules.Add(module.Name, module);
            }
        }

        var conjunctions = modules.Values.Where(m => m is Conjunction).Select(m => m as Conjunction);
        foreach (var conjunction in conjunctions.Cast<Conjunction>())
        {
            foreach (var module in modules.Values)
            {
                if (module.Destinations.Contains(conjunction.Name))
                {
                    conjunction.LastPulse[module.Name] = false;
                }
            }
        }

        modules["rx"] = new Broadcast() { Name = "rx" };
        var rx = modules["rx"];
        int cycle = 0;
        Dictionary<string, long> cycleTimes = new()
        {
            ["cl"] = 0,
            ["rp"] = 0,
            ["lb"] = 0,
            ["nj"] = 0
        };
        while (cycleTimes.Values.Any(v => v == 0))
        {
            foreach (var m in modules.Values) m.NrPulses = 0;
            cycle++;
            var module = modules["broadcaster"];
            Queue<Tuple<Module, string, bool>> q = new();
            q.Enqueue(new Tuple<Module, string, bool>(module, "button", false));
            while (q.Any())
            {
                var t = q.Dequeue();
                module = t.Item1;
                module.NrPulses++;
                string origin = t.Item2;
                bool pulse = t.Item3;
                bool? resultPulse = module.Trigger(origin, pulse);
                if (resultPulse != null)
                {
                    if ((module.Name == "cl" || module.Name == "rp" || module.Name == "lb" || module.Name == "nj") && resultPulse.Value)
                    {
                        if (cycleTimes[module.Name] == 0)
                        {
                            cycleTimes[module.Name] = cycle;
                        }
                        if (cycleTimes.Values.All(v => v > 0)) break;
                    }
                    foreach (var moduleName in module.Destinations)
                    {
                        if (modules.ContainsKey(moduleName))
                        {
                            var nextModule = modules[moduleName];
                            q.Enqueue(new Tuple<Module, string, bool>(nextModule, module.Name, resultPulse.Value));
                        }
                    }
                }
            }
        }

        long total = Algorithms.LCM(cycleTimes.Select(kv => kv.Value));

        Console.WriteLine($"Part 2: {total}");
    }
}
