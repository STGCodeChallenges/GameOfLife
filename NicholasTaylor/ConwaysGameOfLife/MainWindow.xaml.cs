using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;


namespace ConwaysGameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        private const int maxRows = 40;
        private const int maxCols = 40;
        private const int columnWidth = 10;
        private const int rowHeight = 10;
        private const int numGenerations = 50;

        /// <summary>
        /// Initialize the UI and begin the simulation.
        /// </summary>
        private void Start()
        {
            InitializeGrid();
            List<List<Cell>> firstGeneration = createFirstGeneration();
            ThreadStart childref = new ThreadStart(() => runSimulation(firstGeneration));
            Thread childThread = new Thread(childref);
            childThread.SetApartmentState(ApartmentState.STA);
            childThread.Start();
        }

        /// <summary>
        /// Initializes the row and column definitions of the UI grid.
        /// </summary>
        private void InitializeGrid()
        {
            for (int i = 0; i < maxRows; i++)
            {
                grdMain.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(rowHeight) });
            }
            for (int i = 0; i < maxCols; i++)
            {
                grdMain.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(columnWidth) });
            }
        }

        /// <summary>
        /// Creates the first generation by assigning random living status to cells.
        /// </summary>
        /// <returns>A List of Lists containing the cells representing the first generation.</returns>
        private static List<List<Cell>> createFirstGeneration()
        {
            List<List<Cell>> currentGeneration = new List<List<Cell>>();
            Random rand = new Random();
            for (int i = 0; i < maxRows; i++)
            {
                List<Cell> row = new List<Cell>();
                currentGeneration.Add(row);
                for (int j = 0; j < maxCols; j++)
                {
                    double probability = .3;
                    bool living = rand.NextDouble() < probability;
                    row.Add(new Cell(living, i, j, columnWidth, rowHeight));
                }
            }
            return currentGeneration;
        }

        /// <summary>
        /// Runs the simulation for the specified numGenerations.
        /// </summary>
        /// <param name="currentGeneration">The current generation of cells.</param>
        private void runSimulation(List<List<Cell>> currentGeneration)
        {
            for (int i = 0; i < numGenerations; i++)
            {
                displayBoard(currentGeneration);
                List<List<Cell>> nextGeneration = getNextGeneration(currentGeneration);
                currentGeneration = nextGeneration;
            }
            MessageBox.Show("End of Simulation.");
        }

        /// <summary>
        /// Creates the next generations of cells.
        /// </summary>
        /// <param name="currentGeneration">The current generation of cells</param>
        /// <returns>A List of Lists representing the next generation of cells.</returns>
        private List<List<Cell>> getNextGeneration(List<List<Cell>> currentGeneration)
        {
            List<List<Cell>> nextGeneration = new List<List<Cell>>();
            foreach (List<Cell> row in currentGeneration)
            {
                List<Cell> newRow = new List<Cell>();
                nextGeneration.Add(newRow);
                foreach (Cell cell in row)
                {
                    int numLivingNeighbors = getNumberOfLivingNeighbors(cell.Row, cell.Col, currentGeneration);
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        Cell newCell = new Cell(cell);
                        newCell.setLivingStatus(numLivingNeighbors);
                        newRow.Add(newCell);
                    }));
                }
            }
            return nextGeneration;
        }

        /// <summary>
        /// Checks all of the neighboring cells to determing how many are living.  This is used to determine
        /// the futures state of the given cell.
        /// </summary>
        /// <param name="row">Row on which the current cell resides</param>
        /// <param name="col">Column in which the current cell resides</param>
        /// <param name="currentGeneration">The entire current generation of cells</param>
        /// <returns>An integer representing the number of living neighbors.</returns>
        private int getNumberOfLivingNeighbors(int row, int col, List<List<Cell>> currentGeneration)
        {
            int livingNeighborCount = 0;
            //Check Neihbors on the row above if applicable
            if (row > 0 && col > 0) { livingNeighborCount += (currentGeneration[row - 1][col - 1].IsLiving ? 1 : 0); } //neighbor to the top and left 1
            if (row > 0) { livingNeighborCount += (currentGeneration[row - 1][col].IsLiving ? 1 : 0); } //neighbor directly above
            if (row > 0 && currentGeneration[row - 1].Count() - 1 > col) { livingNeighborCount += (currentGeneration[row - 1][col + 1].IsLiving ? 1 : 0); } //neighbor to the top and right 1

            //Check neighbors to the left and right
            if (col > 0) { livingNeighborCount += (currentGeneration[row][col - 1].IsLiving ? 1 : 0); } //neighbor to the left 1
            if (currentGeneration[row].Count() - 1 > col) { livingNeighborCount += (currentGeneration[row][col + 1].IsLiving ? 1 : 0); } //neighbor to the right 1

            //Check neighbors on the row below if applicable
            if (currentGeneration.Count() - 1 > row && col > 0) { livingNeighborCount += (currentGeneration[row + 1][col - 1].IsLiving ? 1 : 0); } //neighbor to the top and left 1
            if (currentGeneration.Count() - 1 > row) { livingNeighborCount += (currentGeneration[row + 1][col].IsLiving ? 1 : 0); } //neighbor directly above
            if (currentGeneration.Count() - 1 > row && currentGeneration[row + 1].Count() - 1 > col) { livingNeighborCount += (currentGeneration[row + 1][col + 1].IsLiving ? 1 : 0); } //neighbor to the top and right 1
            return livingNeighborCount;
        }

        /// <summary>
        /// Updates the UI with the current generation
        /// </summary>
        /// <param name="currentGeneration">The current generatio</param>
        private void displayBoard(List<List<Cell>> currentGeneration)
        {
            Thread.Sleep(500);
            this.Dispatcher.Invoke((Action)(() =>
            {
                grdMain.Children.Clear();
                foreach (List<Cell> row in currentGeneration)
                {
                    foreach (Cell cell in row)
                    {
                        grdMain.Children.Add(cell);
                    }
                }
            }));
        }
    }
}
