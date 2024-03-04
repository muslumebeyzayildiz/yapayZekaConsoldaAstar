using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yapayZekaOdev1
{
    public partial class Form1 : Form
    {
        private int[,] goalState = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };
        private List<int[,]> exploredStates;
        private List<Node> openList;
        public Form1()
        {
            InitializeComponent();
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            int[,] initialState = new int[3, 3];
            initialState[0, 0] = Convert.ToInt32(textBox1.Text);
            initialState[0, 1] = Convert.ToInt32(textBox2.Text);
            initialState[0, 2] = Convert.ToInt32(textBox3.Text);
            initialState[1, 0] = Convert.ToInt32(textBox4.Text);
            initialState[1, 1] = Convert.ToInt32(textBox5.Text);
            initialState[1, 2] = Convert.ToInt32(textBox6.Text);
            initialState[2, 0] = Convert.ToInt32(textBox7.Text);
            initialState[2, 1] = Convert.ToInt32(textBox8.Text);
            initialState[2, 2] = Convert.ToInt32(textBox9.Text);

            if (SolvePuzzle(initialState))
            {
                MessageBox.Show("Puzzle solved!");
            }
            else
            {
                MessageBox.Show("No solution found!");
            }
        }
        private bool SolvePuzzle(int[,] initialState)
        {
            Node initialNode = new Node(initialState, null, 0, CalculateManhattanDistance(initialState));
            exploredStates = new List<int[,]>();
            openList = new List<Node>();
            openList.Add(initialNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList.OrderBy(node => node.PathCost + node.Heuristic).First();
                openList.Remove(currentNode);

                if (IsGoalState(currentNode.State))
                {
                    ShowSolution(currentNode);
                    return true;
                }

                exploredStates.Add(currentNode.State);

                List<Node> childNodes = GenerateChildNodes(currentNode);
                foreach (Node childNode in childNodes)
                {
                    if (!exploredStates.Contains(childNode.State))
                    {
                        openList.Add(childNode);
                    }
                }
            }

            return false;
        }

        private bool IsGoalState(int[,] state)
        {
            return state.Cast<int>().SequenceEqual(goalState.Cast<int>());
        }

        private int CalculateManhattanDistance(int[,] state)
        {
            int distance = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int value = state[i, j];
                    if (value != 0)
                    {
                        int goalRow = (value - 1) / 3;
                        int goalCol = (value - 1) % 3;
                        distance += Math.Abs(i - goalRow) + Math.Abs(j - goalCol);
                    }
                }
            }
            return distance;
        }

        private List<Node> GenerateChildNodes(Node parent)
        {
            List<Node> childNodes = new List<Node>();

            int emptyRow = -1;
            int emptyCol = -1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (parent.State[i, j] == 0)
                    {
                        emptyRow = i;
                        emptyCol = j;
                        break;
                    }
                }
            }

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for (int k = 0; k < 4; k++)
            {
                int newRow = emptyRow + dx[k];
                int newCol = emptyCol + dy[k];

                if (newRow >= 0 && newRow < 3 && newCol >= 0 && newCol < 3)
                {
                    int[,] newState = (int[,])parent.State.Clone();
                    newState[emptyRow, emptyCol] = newState[newRow, newCol];
                    newState[newRow, newCol] = 0;

                    Node childNode = new Node(newState, parent, parent.PathCost + 1, CalculateManhattanDistance(newState));
                    childNodes.Add(childNode);
                }
            }

            return childNodes;
        }

        private void ShowSolution(Node solutionNode)
        {
            List<Node> path = new List<Node>();
            while (solutionNode != null)
            {
                path.Insert(0, solutionNode);
                solutionNode = solutionNode.Parent;
            }

            string solution = "";
            foreach (Node node in path)
            {
                solution += node.ToString() + "\n";
            }

            MessageBox.Show(solution);
        }
    }

    public class Node
    {
        public int[,] State { get; set; }
        public Node Parent { get; set; }
        public int PathCost { get; set; }
        public int Heuristic { get; set; }

        public Node(int[,] state, Node parent, int pathCost, int heuristic)
        {
            State = state;
            Parent = parent;
            PathCost = pathCost;
            Heuristic = heuristic;
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    output += State[i, j] + " ";
                }
                output += "\n";
            }
            return output;
        }
    }
}
