using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Peterson Graph:");
        Parition.ByEdges(Edges.PetersonGraph);

        Console.WriteLine("Disconnected Graph");
        Parition.ByEdges(Edges.DisconnectedGraph);

        Console.WriteLine("Butterfly Graph");
        Parition.ByEdges(Edges.ButterflyGraph);

        Console.WriteLine("Binary Tree");
        Parition.ByEdges(Edges.BinaryTree);

        Console.WriteLine("Compelete Graph");
        Parition.ByEdges(Edges.CompleteGraph);
    }
}

public class Parition
{
    public static void ByEdges(List<Tuple<string, string>> edges)
    {
        var allNodes = new AllNodes();

        foreach (var edge in edges)
            allNodes.AddEdge(edge);

        allNodes.Partition();
    }
}

public class Node
{
    public List<Node> Neighbours { get; set; } = new List<Node>();
    public string Name { get; set; }
    public int Group { get; set; } = 1;
    public bool Confirmed { get; set; } = false;

    private List<Node> GetAllConfirmedNeighbours()
    {
        return Neighbours.Where(x => x.Confirmed).ToList();
    }

    private bool HasNeighbour(Node node)
    {
        return Neighbours.Any(x => x.Name == node.Name);
    }

    public void AddNeighbour(Node node)
    {
        if(!HasNeighbour(node))
            Neighbours.Add(node);
    }

    public void AssignGroup()
    {
        // This node is updating, change confirmed flag to true
        this.Confirmed = true;

        // If not confirmed neighbour is found, simply keep group as 1 (default)
        var confirmedNeighbours = GetAllConfirmedNeighbours();
        if (!confirmedNeighbours.Any()) return;

        // Find sorted unique groups of confirmed neighbours
        var groups = confirmedNeighbours.Select(x => x.Group).Distinct().ToList();
        groups.Sort();

        // Assign a minimum missing number
        for (var i = 1; i <= groups.Count; i++)
            if (!groups.Contains(i))
            {
                Group = i;
                return;
            }

        // Assign a new group (compared to neighbours) if there is no missing number
        Group = groups.Max() + 1;
    }

    public override string ToString()
    {
        return Name;
    }
}

public class AllNodes
{
    private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();

    public Node this[string name]
    {
        get
        {
            if (!_nodes.ContainsKey(name))
            {
                var node = new Node() {Name = name};
                _nodes.Add(name, node);
            }

            return _nodes[name];
        }
    }

    private List<Node> GetNodesOfGroup(int group)
    {
        return _nodes.Values.Where(x => x.Group == group)
                            .OrderBy(x => x.Name)
                            .ToList();
    }

    public void AddEdge(Tuple<string, string> edge)
    {
        var node1 = this[edge.Item1];
        var node2 = this[edge.Item2];

        node1.AddNeighbour(node2);
        node2.AddNeighbour(node1);
    }

    public void Partition()
    {
        // Assign group to each node
        foreach (var node in _nodes.Values)
            node.AssignGroup();

        // Print results
        var groupCount = _nodes.Values.Max(x => x.Group);
        Console.WriteLine($"Minimum {groupCount} groups are required.");
        for (int i = 1; i <= groupCount; i++)
        {
            var nodeNames = string.Join(", ", GetNodesOfGroup(i));
            Console.WriteLine($"Group {i} contains {{{nodeNames}}}.");
        }
        Console.WriteLine();
    }
}

public static class Edges
{
    //Refer to https://www.researchgate.net/profile/Ruben_Campoy/publication/311668725/figure/fig4/AS:439756455714823@1481857517283/Figure-11-A-3-coloring-of-Petersen-graph.png
    public static List<Tuple<string, string>> PetersonGraph = 
        new List<Tuple<string, string>>()
        {
            Tuple.Create("1", "2"),
            Tuple.Create("2", "3"),
            Tuple.Create("3", "4"),
            Tuple.Create("4", "5"),
            Tuple.Create("5", "1"),

            Tuple.Create("1", "6"),
            Tuple.Create("2", "7"),
            Tuple.Create("3", "8"),
            Tuple.Create("4", "9"),
            Tuple.Create("5", "10"),

            Tuple.Create("6", "8"),
            Tuple.Create("6", "9"),
            Tuple.Create("7", "10"),
            Tuple.Create("8", "10"),
            Tuple.Create("7", "9"),
        };

    public static List<Tuple<string, string>> ButterflyGraph = 
        new List<Tuple<string, string>>()
        {
            Tuple.Create("1", "2"),
            Tuple.Create("2", "3"),
            Tuple.Create("1", "3"),
            Tuple.Create("3", "4"),
            Tuple.Create("4", "5"),
            Tuple.Create("5", "3"),
        };

    public static List<Tuple<string, string>> DisconnectedGraph = 
        new List<Tuple<string, string>>()
        {
            Tuple.Create("1", "2"),
            Tuple.Create("2", "3"),
            Tuple.Create("1", "3"),

            Tuple.Create("x", "y"),
            Tuple.Create("z", "y"),
            Tuple.Create("x", "z")
        };

    public static List<Tuple<string, string>> CompleteGraph = 
        new List<Tuple<string, string>>()
        {
            Tuple.Create("1", "2"),
            Tuple.Create("1", "3"),
            Tuple.Create("1", "4"),
            Tuple.Create("1", "5"),

            Tuple.Create("2", "3"),
            Tuple.Create("2", "4"),
            Tuple.Create("2", "5"),

            Tuple.Create("3", "4"),
            Tuple.Create("3", "5"),

            Tuple.Create("4", "5"),
        };

    public static List<Tuple<string, string>> BinaryTree = 
        new List<Tuple<string, string>>()
        {
            Tuple.Create("1", "2"),
            Tuple.Create("1", "3"),

            Tuple.Create("2", "4"),
            Tuple.Create("2", "5"),

            Tuple.Create("3", "6"),
            Tuple.Create("3", "7"),

            Tuple.Create("4", "8"),
            Tuple.Create("4", "9"),

            Tuple.Create("5", "10"),
            Tuple.Create("5", "11"),

            Tuple.Create("6", "12"),
            Tuple.Create("6", "13"),

            Tuple.Create("7", "14"),
            Tuple.Create("7", "15"),
        };
}

