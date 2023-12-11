using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public sealed class Day08 : AdventBase
{
    protected override object InternalPart1()
    {
        var instructions = Input.Lines[0];

        var rest = Input.Blocks.Skip(1).First().Lines;

        var tree = Parse(rest);

        var moves = 0;

        var currentNode = tree["AAA"];
        while (currentNode.Name != "ZZZ")
        {
            var nextInstruction = instructions[moves % instructions.Length];

            currentNode = nextInstruction == 'L' ? currentNode.Left : currentNode.Right;
            moves++;
        }

        return moves;
    }

    private Dictionary<string, Node> Parse(string[] list)
    {
        var dict = new Dictionary<string, Node>();

        foreach (string line in list)
        {
            var split = line.Split('=', StringSplitOptions.TrimEntries);
            var name = split[0];

            var options = split[1].Split(new[] { '(', ',', ')' },
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var left = options[0];
            var right = options[1];

            var self = dict.AddIfNotExists(name, new Node(name));
            var leftNode = dict.AddIfNotExists(left, new Node(left));
            var rightNode = dict.AddIfNotExists(right, new Node(right));

            self.Left = leftNode;
            self.Right = rightNode;
        }

        return dict;
    }

    protected override object InternalPart2()
    {
        throw new NotImplementedException();
    }

    public class Node
    {
        public Node(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}

public static class Day8Extensions
{
    public static Day08.Node AddIfNotExists(this Dictionary<string, Day08.Node> dict, string name, Day08.Node node)
    {
        return dict.TryAdd(name, node) ? node : dict[name];
    }
}