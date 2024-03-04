using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yapayzekaOdev1Deneme2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize the initial state
            List<int> state = new List<int> { 1, 2, 3, 0, 5, 6, 4, 7, 8 };
            int h = Count(state);
            int Level = 1;

            // Display the initial state
            PrintState(Level, state, h);
        }

        private void PrintState(int level, List<int> state, int h)
        {
            string stateString = "";
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0 && i > 0)
                    stateString += "\n";
                stateString += state[i] + " ";
            }

            // Display the state in a label or text box on the form
            labelState.Text = "------ Level " + level + " ------\n" + stateString + "\n\nHeuristic Value(Misplaced) : " + h;
        }

        private int Count(List<int> s)
        {
            int c = 0;
            List<int> ideal = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0 };

            for (int i = 0; i < 9; i++)
            {
                if (s[i] != 0 && s[i] != ideal[i])
                    c++;
            }
            return c;
        }

        private Tuple<List<int>, int> Move(List<int> ar, int p, List<int> st)
        {
            int rh = 9999;
            List<int> storeSt = new List<int>(st);

            for (int i = 0; i < ar.Count; i++)
            {
                List<int> duplSt = new List<int>(st);

                int tmp = duplSt[p];
                duplSt[p] = duplSt[ar[i]];
                duplSt[ar[i]] = tmp;

                int trh = Count(duplSt);

                if (trh < rh)
                {
                    rh = trh;
                    storeSt = new List<int>(duplSt);
                }
            }

            return new Tuple<List<int>, int>(storeSt, rh);
        }


        private void buttonNextStep_Click(object sender, EventArgs e)
        {
            // Retrieve current state from the label or text box on the form
            List<int> state = GetCurrentState();

            // Find the position of 0 (blank tile)
            int pos = state.IndexOf(0);

            int Level = GetCurrentLevel();

            List<int> newState = new List<int>();
            int h = 0;

            Level++;

            // Perform moves based on the position of the blank tile
            if (pos == 0)
            {
                List<int> arr = new List<int> { 1, 3 };
                var moveResult = Move(arr, pos, state);
                newState = moveResult.Item1;
                h = moveResult.Item2;
            }
            else if (pos == 1)
            {
                List<int> arr = new List<int> { 0, 2, 4 };
                var moveResult = Move(arr, pos, state);
                newState = moveResult.Item1;
                h = moveResult.Item2;
            }
            // Implement similar moves for other positions...

            // Display the new state
            PrintState(Level, newState, h);

        }


        // Implement GetCurrentState and GetCurrentLevel functions to retrieve current state and level from the form
        private List<int> GetCurrentState()
        {
            // Retrieve the current state from the label or text box on the form
            throw new NotImplementedException();
        }

        private int GetCurrentLevel()
        {
            // Retrieve the current level from the form
            throw new NotImplementedException();
        }
    }
}
