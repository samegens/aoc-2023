namespace AoC;

public class Node
{
    public Node(string name, string left, string right)
    {
        Name = name;
        Left = left;
        Right = right;
    }

    public string Name { get; private set; }

    public string Left { get; private set; }

    public string Right { get; private set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Node other = (Node)obj;

        return other.Name == Name &&
            other.Left == Left &&
            other.Right == Right;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Left, Right);
    }
}