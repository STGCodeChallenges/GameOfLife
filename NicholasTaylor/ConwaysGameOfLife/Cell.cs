using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace ConwaysGameOfLife
{
    public class Cell : Shape
    {
        private int row = 0;
        public int Row { get { return row; } set { row = value; } }
        private int col = 0;
        public int Col { get { return col; } set { col = value; } }
        private bool isLiving = false;
        public bool IsLiving { get { return isLiving; } set { isLiving = value; this.Fill = (value ? Brushes.Black : Brushes.White); } }
        private RectangleGeometry _geometry = new RectangleGeometry(new Rect());

        public Cell(bool living, int rowNum, int colNum, int width, int height)
        {
            Grid.SetColumn(this, colNum);
            Grid.SetRow(this, rowNum);

            row = rowNum;
            col = colNum;
            Width = width;
            Height = height;
            _geometry = new RectangleGeometry(new Rect(0, 0, Width, Height));
            isLiving = living;
            if (isLiving)
            {
                this.Fill = Brushes.Black;
            }
            else
            {
                this.Fill = Brushes.White;
            }
        }

        public Cell(Cell oldCell)
        {
            Grid.SetColumn(this, oldCell.col);
            Grid.SetRow(this, oldCell.row);
            row = oldCell.row;
            col = oldCell.col;
            Width = oldCell.Width;
            Height = oldCell.Height;
            _geometry = oldCell._geometry;
            isLiving = oldCell.isLiving;
            this.Fill = oldCell.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                return _geometry;
            }
        }

        public void setLivingStatus(int numLivingNeihbors)
        {
            if (this.IsLiving)
            {
                if (numLivingNeihbors < 2)
                {
                    this.IsLiving = false;
                }
                else if (numLivingNeihbors == 2 || numLivingNeihbors == 3) { /*do nothing.  cell lives on to next generation*/ }
                else if (numLivingNeihbors > 3) { this.IsLiving = false; } //over crowding
            }
            else
            {
                if (numLivingNeihbors == 3) { this.IsLiving = true; }
                else { /*Do nothing.  Cell remains dead in next generation*/ }
            }
        }
    }
}
