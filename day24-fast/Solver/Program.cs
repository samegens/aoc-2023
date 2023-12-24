using Microsoft.Z3;

namespace AoC;

public struct PointD
{
    public double X;
    public double Y;

    public PointD(double x, double y)
    {
        X = x;
        Y = y;
    }

    public PointD(PointL p)
    {
        X = p.X;
        Y = p.Y;
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
        int total = 0;
        List<PointL> points = new();
        List<PointL> velos = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            var parts = line.Split('@');
            PointL p = new(long.Parse(parts[0].Split(',')[0].Trim()), long.Parse(parts[0].Split(',')[1].Trim()));
            points.Add(p);
            PointL v = new(long.Parse(parts[1].Split(',')[0].Trim()), long.Parse(parts[1].Split(',')[1].Trim()));
            velos.Add(v);
        }

        // double minX = 7;
        // double minY = 7;
        // double maxX = 27;
        // double maxY = 27;
        double minX = 200000000000000;
        double minY = 200000000000000;
        double maxX = 400000000000000;
        double maxY = 400000000000000;
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                var p1 = points[i];
                var v1 = velos[i];
                var p2 = points[j];
                var v2 = velos[j];

                PointD l1p1 = new(p1);
                PointD l1p2 = new(p1.Move((int)v1.X, (int)v1.Y));
                PointD l2p1 = new(p2);
                PointD l2p2 = new(p2.Move((int)v2.X, (int)v2.Y));

                // PointD? intersect = LineIntersection((l1p1, l1p2), (l2p1, l2p2));
                PointD? intersect = FindIntersection((l1p1, l1p2), (l2p1, l2p2));
                if (intersect == null) continue;

                var xi = intersect.Value.X;
                var yi = intersect.Value.Y;

                if (xi >= minX && xi <= maxX && yi >= minY && yi <= maxY)
                {
                    var dx = xi - p1.X;
                    var dy = yi - p1.Y;

                    if ((dx > 0) != (v1.X > 0) || (dy > 0) != (v1.Y > 0))
                    {
                        continue;
                    }

                    dx = xi - p2.X;
                    dy = yi - p2.Y;

                    if ((dx > 0) != (v2.X > 0) || (dy > 0) != (v2.Y > 0))
                    {
                        continue;
                    }

                    total++;
                }
            }
        }
        Console.WriteLine($"Part 1: {total}");
    }

    public static PointD? FindIntersection((PointD, PointD) line1, (PointD, PointD) line2)
    {
        // https://stackoverflow.com/questions/4543506/algorithm-for-intersection-of-2-lines
        // A = y2-y1; B = x1-x2; C = Ax1+By1
        double a1 = line1.Item2.Y - line1.Item1.Y;
        double a2 = line2.Item2.Y - line2.Item1.Y;
        double b1 = line1.Item1.X - line1.Item2.X;
        double b2 = line2.Item1.X - line2.Item2.X;
        double c1 = a1 * line1.Item1.X + b1 * line1.Item1.Y;
        double c2 = a2 * line2.Item1.X + b2 * line2.Item2.Y;

        double delta = a1 * b2 - a2 * b1;

        if (delta == 0)
            return null;

        double x = (b2 * c1 - b1 * c2) / delta;
        double y = (a1 * c2 - a2 * c1) / delta;

        return new PointD(x, y);
    }

    private static void SolvePart2(string[] lines)
    {
        List<Point3d> points = new();
        List<Point3d> velos = new();
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            var parts = line.Split('@');
            Point3d p = new(long.Parse(parts[0].Split(',')[0].Trim()), long.Parse(parts[0].Split(',')[1].Trim()), long.Parse(parts[0].Split(',')[2].Trim()));
            points.Add(p);
            Point3d v = new(long.Parse(parts[1].Split(',')[0].Trim()), long.Parse(parts[1].Split(',')[1].Trim()), long.Parse(parts[1].Split(',')[2].Trim()));
            velos.Add(v);
        }

        (long x, long y, long z) = FindRockStartPosition(points, velos);
        Console.WriteLine($"Part 2: {x + y + z}");
    }

    private static (long x, long y, long z) FindRockStartPosition(List<Point3d> points, List<Point3d> velos)
    {
        // Borrowed without permission from https://pastebin.com/fkpZWn8X

        var ctx = new Context();
        var solver = ctx.MkSolver();

        // Coordinates of the stone
        var x = ctx.MkIntConst("x");
        var y = ctx.MkIntConst("y");
        var z = ctx.MkIntConst("z");

        // Velocity of the stone
        var vx = ctx.MkIntConst("vx");
        var vy = ctx.MkIntConst("vy");
        var vz = ctx.MkIntConst("vz");

        // For each iteration, we will add 3 new equations and one new condition to the solver.
        // We want to find 9 variables (x, y, z, vx, vy, vz, t0, t1, t2) that satisfy all the equations, so a system of 9 equations is enough.
        for (var i = 0; i < 3; i++)
        {
            var t = ctx.MkIntConst($"t{i}"); // time for the stone to reach the hail

            var pos = points[i];
            var px = ctx.MkInt(Convert.ToInt64(pos.X));
            var py = ctx.MkInt(Convert.ToInt64(pos.Y));
            var pz = ctx.MkInt(Convert.ToInt64(pos.Z));

            var velo = velos[i];
            var pvx = ctx.MkInt(Convert.ToInt64(velo.X));
            var pvy = ctx.MkInt(Convert.ToInt64(velo.Y));
            var pvz = ctx.MkInt(Convert.ToInt64(velo.Z));

            var xLeft = ctx.MkAdd(x, ctx.MkMul(t, vx)); // x + t * vx
            var yLeft = ctx.MkAdd(y, ctx.MkMul(t, vy)); // y + t * vy
            var zLeft = ctx.MkAdd(z, ctx.MkMul(t, vz)); // z + t * vz

            var xRight = ctx.MkAdd(px, ctx.MkMul(t, pvx)); // px + t * pvx
            var yRight = ctx.MkAdd(py, ctx.MkMul(t, pvy)); // py + t * pvy
            var zRight = ctx.MkAdd(pz, ctx.MkMul(t, pvz)); // pz + t * pvz

            solver.Add(t >= 0); // time should always be positive - we don't want solutions for negative time
            solver.Add(ctx.MkEq(xLeft, xRight)); // x + t * vx = px + t * pvx
            solver.Add(ctx.MkEq(yLeft, yRight)); // y + t * vy = py + t * pvy
            solver.Add(ctx.MkEq(zLeft, zRight)); // z + t * vz = pz + t * pvz
        }

        solver.Check();
        var model = solver.Model;

        var rx = model.Eval(x);
        var ry = model.Eval(y);
        var rz = model.Eval(z);

        return (long.Parse(rx.ToString()), long.Parse(ry.ToString()), long.Parse(rz.ToString()));
    }
}
