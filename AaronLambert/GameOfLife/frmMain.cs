using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class frmMain : Form
    {
        const int MAX_CELLS_X = 50;
        const int MAX_CELLS_Y = 50;
        const int CELL_WIDTH = 8;
        const int CELL_HEIGHT = 8;
        bool[,] ItsAlive;
        public int ChanceOfLife = 25;
        public int TimeInterval = 1000;
        Size CELL_SIZE = new Size(CELL_WIDTH, CELL_HEIGHT);
        frmSettings frmSettings = new GameOfLife.frmSettings();

        public frmMain()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | 
                BindingFlags.NonPublic, null, pnlWorld, new object[] { true });
        }

        private void pnlWorld_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.DarkGray, 2);
            int x1, y1, x2, y2;

            // Draw the grid
            for (int i = 0; i <= MAX_CELLS_X; i++)
            {
                x1 = (CELL_WIDTH * i) + i;
                x2 = x1;
                y1 = 0;
                y2 = MAX_CELLS_Y * (CELL_HEIGHT + 1);
                g.DrawLine(p, x1, y1, x2, y2);
            }
            for (int i = 0; i <= MAX_CELLS_Y; i++)
            {
                x1 = 0;
                x2 = MAX_CELLS_X * (CELL_WIDTH + 1);
                y1 = (CELL_HEIGHT * i) + i;
                y2 = y1;
                g.DrawLine(p, x1, y1, x2, y2);
            }

            List<Rectangle> AliveCells = new List<Rectangle>();
            List<Rectangle> DeadCells = new List<Rectangle>();

            if (ItsAlive == null)
            {
                // Pause the simulaton
                LifeTimer.Enabled = false;
                return;
            }

            // Draw the cells
            for (int x = 0; x < MAX_CELLS_X; x++)
            {
                for (int y = 0; y < MAX_CELLS_Y; y++)
                {
                    Rectangle rect = new Rectangle(GetCellLocation(x, y), CELL_SIZE);
                    if (ItsAlive[x, y])
                    { AliveCells.Add(rect); }
                    else
                    { DeadCells.Add(rect); }
                }
            }
            SolidBrush b = new SolidBrush(Color.Black);
            if (AliveCells.Count > 0)
                g.FillRectangles(b, AliveCells.ToArray());
        }

        private int GetCellXOffset(int x)
        {
            return (x * (CELL_WIDTH + 1)) + 1;
        }

        private int GetCellYOffset(int y)
        {
            return (y * (CELL_HEIGHT + 1)) + 1;
        }

        private Point GetCellLocation(int x, int y)
        {
            return new Point(GetCellXOffset(x), GetCellYOffset(y));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Pause the simulaton
            LifeTimer.Enabled = false;

            if (frmSettings.ShowDialog(this) != DialogResult.OK)
            {
                LifeTimer.Enabled = true;
                return;
            }

            ItsAlive = new bool[MAX_CELLS_X, MAX_CELLS_Y];
            Random rand = new Random();

            // Initialize the live cells
            for (int x = 0; x < MAX_CELLS_X; x++)
            {
                for (int y = 0; y < MAX_CELLS_Y; y++)
                {
                    ItsAlive[x, y] = (rand.Next(1, 100) <= ChanceOfLife);
                }
            }

            // Refresh the display
            pnlWorld.Invalidate();

            // Begin the simulation
            LifeTimer.Interval = TimeInterval;
            SetPlayMode(true);
        }

        private void LifeTimer_Tick(object sender, EventArgs e)
        {
            CircleOfLife();
        }

        private void CircleOfLife()
        {
            bool[,] NewAlive = new bool[MAX_CELLS_X, MAX_CELLS_Y];

            for (int x = 0; x < MAX_CELLS_X; x++)
            {
                for (int y = 0; y < MAX_CELLS_Y; y++)
                {
                    int nc = NeighborCount(x, y);
                    // Any live cell with fewer than two live neighbours dies, as if by needs caused by underpopulation.
                    // Any live cell with more than three live neighbours dies, as if by overcrowding.
                    // Any live cell with two or three live neighbours lives, unchanged, to the next generation.
                    // Any dead cell with exactly three live neighbours cells will come to life.
                    NewAlive[x, y] = (ItsAlive[x, y] && nc >= 2 && nc <= 3) || (!ItsAlive[x, y] && nc == 3);
                }
            }

            // Copy the new data into the main array
            Array.Copy(NewAlive, ItsAlive, NewAlive.Length);

            // Draw the updated grid
            pnlWorld.Invalidate();
        }

        private int NeighborCount(int x, int y)
        {
            int n = 0;
            int x1, y1;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;
                    x1 = x + dx;
                    y1 = y + dy;
                    if (x1 < 0)
                        x1 += MAX_CELLS_X;
                    if (x1 >= MAX_CELLS_X)
                        x1 -= MAX_CELLS_X;
                    if (y1 < 0)
                        y1 += MAX_CELLS_Y;
                    if (y1 >= MAX_CELLS_Y)
                        y1 -= MAX_CELLS_Y;
                    if (ItsAlive[x1, y1])
                        n++;
                }
            }

            return n;
        }

        private void SetPlayMode(bool Play)
        {
            LifeTimer.Enabled = Play;
            btnPlay.Enabled = !Play;
            btnPlay.Visible = !Play;
            btnNext.Enabled = !Play;
            btnPause.Enabled = Play;
            btnPause.Visible = Play;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            SetPlayMode(false);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            LifeTimer_Tick(sender, e);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            SetPlayMode(true);
        }
    }   
}
