using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EightPuzzle
{
    // Direction matrix
    static class Directions
    {
        public static readonly Dictionary<char, int[]> DIRECTION_MAP = new Dictionary<char, int[]>
        {
            { 'U', new [] { -1, 0 } },
            { 'D', new [] { 1, 0 } },
            { 'L', new [] { 0, -1 } },
            { 'R', new [] { 0, 1 } }
        };
    }

    // Target matrix
    static class EndState
    {
        public static readonly int[,] END = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 }
        };
    }

    // Node class to store each state of the puzzle
    class Node
    {
        public int[,] CurrentNode { get; }
        public int[,] PreviousNode { get; }
        public int G { get; }
        public int H { get; }
        public char Direction { get; }

        public Node(int[,] currentNode, int[,] previousNode, int g, int h, char dir)
        {
            CurrentNode = currentNode;
            PreviousNode = previousNode;
            G = g;
            H = h;
            Direction = dir;
        }

        public int F()
        {
            return G + H;
        }
    }

    class Program
    {
        // Euclidean distance calculation method
        static int EuclideanCost(int[,] currentState)
        {
            int cost = 0;
            for (int row = 0; row < currentState.GetLength(0); row++)
            {
                for (int col = 0; col < currentState.GetLength(1); col++)
                {
                    int[] pos = GetPosition(EndState.END, currentState[row, col]);
                    cost += Math.Abs(row - pos[0]) + Math.Abs(col - pos[1]);
                }
            }
            return cost;
        }

        // Get position of an element in the matrix
        static int[] GetPosition(int[,] currentState, int element)
        {
            for (int row = 0; row < currentState.GetLength(0); row++)
            {
                for (int col = 0; col < currentState.GetLength(1); col++)
                {
                    if (currentState[row, col] == element)
                    {
                        return new[] { row, col };
                    }
                }
            }
            return null;
        }

        // Get adjacent nodes
        static List<Node> GetAdjNode(Node node)
        {
            List<Node> listNode = new List<Node>();
            int[] emptyPos = GetPosition(node.CurrentNode, 0);

            foreach (var dir in Directions.DIRECTION_MAP)
            {
                int[] newPos = { emptyPos[0] + dir.Value[0], emptyPos[1] + dir.Value[1] };
                if (newPos[0] >= 0 && newPos[0] < node.CurrentNode.GetLength(0) &&
                    newPos[1] >= 0 && newPos[1] < node.CurrentNode.GetLength(1))
                {
                    int[,] newState = (int[,])node.CurrentNode.Clone();
                    newState[emptyPos[0], emptyPos[1]] = node.CurrentNode[newPos[0], newPos[1]];
                    newState[newPos[0], newPos[1]] = 0;
                    listNode.Add(new Node(newState, node.CurrentNode, node.G + 1, EuclideanCost(newState), dir.Key));
                }
            }

            return listNode;
        }

        // Get the best node available among nodes
        static Node GetBestNode(Dictionary<string, Node> openSet)
        {
            bool firstIter = true;
            Node bestNode = null;
            int bestF = 0;

            foreach (var node in openSet.Values)
            {
                if (firstIter || node.F() < bestF)
                {
                    firstIter = false;
                    bestNode = node;
                    bestF = bestNode.F();
                }
            }
            return bestNode;
        }

        // Build the smallest path
        static List<(char dir, int[,] node)> BuildPath(Dictionary<string, Node> closedSet)
        {
            Node node = closedSet[ToString(EndState.END)];
            var branch = new List<(char dir, int[,] node)>();

            while (node.Direction != '\0')
            {
                branch.Add((node.Direction, node.CurrentNode));
                node = closedSet[ToString(node.PreviousNode)];
            }
            branch.Add(('\0', node.CurrentNode));
            branch.Reverse();

            return branch;
        }

        // Main function
        static List<(char dir, int[,] node)> Main(int[,] puzzle)
        {
            var openSet = new Dictionary<string, Node> { { ToString(puzzle), new Node(puzzle, puzzle, 0, EuclideanCost(puzzle), '\0') } };
            var closedSet = new Dictionary<string, Node>();

            while (true)
            {
                var testNode = GetBestNode(openSet);
                closedSet[ToString(testNode.CurrentNode)] = testNode;

                if (ToString(testNode.CurrentNode) == ToString(EndState.END))
                {
                    return BuildPath(closedSet);
                }

                var adjNode = GetAdjNode(testNode);
                foreach (var node in adjNode)
                {
                    if (closedSet.ContainsKey(ToString(node.CurrentNode)) ||
                        (openSet.ContainsKey(ToString(node.CurrentNode)) && openSet[ToString(node.CurrentNode)].F() < node.F()))
                    {
                        continue;
                    }
                    openSet[ToString(node.CurrentNode)] = node;
                }

                openSet.Remove(ToString(testNode.CurrentNode));
            }
        }

        // Convert matrix to string
        static string ToString(int[,] matrix)
        {
            var str = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    str += matrix[i, j];
                }
            }
            return str;
        }

        // Print puzzle
        static void PrintPuzzle(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == 0)
                    {
                        Console.Write("|" + " ");
                    }
                    else
                    {
                        Console.Write("|" + array[i, j]);
                    }
                }
                Console.WriteLine("|");
            }
        }


        static void Main(string[] args)
        {
            var br = Main(new int[,]
            {
        { 6, 2, 8 },
        { 4, 7, 1 },
        { 0, 3, 5 }
            });

            Console.WriteLine("Total steps: " + (br.Count - 1) + "\n");
            Console.WriteLine("--INPUT--");
            foreach (var (dir, node) in br)
            {
                if (dir != '\0')
                {
                    string letter = "";
                    switch (dir)
                    {
                        case 'U':
                            letter = "UP";
                            break;
                        case 'R':
                            letter = "RIGHT";
                            break;
                        case 'L':
                            letter = "LEFT";
                            break;
                        case 'D':
                            letter = "DOWN";
                            break;
                    }
                    Console.WriteLine(letter);
                }
                PrintPuzzle(node);
                Console.WriteLine();
            }
            Console.WriteLine("--ABOVE IS THE OUTPUT--");
        }

    }
}