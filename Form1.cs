using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Змейка_3._0
{
    public partial class Form1 : Form
    {
        private int _width = 900;
        private int _height = 800;
        private int _sizeOfSides = 40;
        private int dirx, diry;
        private int rI, rJ;
        private PictureBox fruit;
        private int score = 0;
        private PictureBox[] snake = new PictureBox[400];
        private Label LabelBox;
        public Form1()
        {
            InitializeComponent();
            dirx = 1;
            diry = 0;
            this.Width = _width;
            this.Height = _height;
            this.KeyDown += new KeyEventHandler(OKP);
            timer.Tick += new EventHandler(_update);
            timer.Interval = 200;
            timer.Start();
            _generateMap();
           
            fruit = new PictureBox();
            fruit.Size = new Size(_sizeOfSides, _sizeOfSides);
            fruit.BackColor = Color.Yellow;
            _generateFruit();
            LabelBox = new Label();
            LabelBox.Text = "Score: 0";
            LabelBox.Location = new Point(810, 10);
            this.Controls.Add(LabelBox);
            this.Text = "Snake";
            snake[0] = new PictureBox();
            snake[0].BackColor = Color.Red;
            snake[0].Size = new Size(_sizeOfSides - 1, _sizeOfSides - 1);
            snake[0].Location = new Point(201, 201);
            this.Controls.Add(snake[0]);
        }
        private void _checkBorders()
        {
            if (snake[0].Location.X < 0)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                LabelBox.Text = "Score: " + score;
                dirx = 1;
            }
            if (snake[0].Location.X > _height)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                LabelBox.Text = "Score: " + score;
                dirx = -1;
            }
            if (snake[0].Location.Y < 0)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                LabelBox.Text = "Score: " + score;
                diry = 1;
            }
            if (snake[0].Location.Y > _height)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                LabelBox.Text = "Score: " + score;
                diry = -1;
            }
        }
        private void _eatItself()
        {
            for(int i = 1; i < score; i++)
            {
                if (snake[0].Location == snake[i].Location)
                {
                    for (int j = i; j <= score; j++)
                        this.Controls.Remove(snake[j]);
                    score = score - (score - i + 1);
                    LabelBox.Text = "Score: " + score;
                }
            }
        }
        private void _moveSnake()
        {
            for(int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirx * _sizeOfSides, snake[0].Location.Y + diry * _sizeOfSides);
            _eatItself();
        }
        private void _eatFruit()
        {
            if (snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                LabelBox.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].BackColor = Color.Red;
                snake[score].Size = new Size(_sizeOfSides - 1, _sizeOfSides - 1);
                snake[score].Location = new Point(snake[score-1].Location.X + dirx * _sizeOfSides, snake[score-1].Location.Y - diry * _sizeOfSides);
                this.Controls.Add(snake[score]);
                _generateFruit();
            }
            
        }
        private void _generateFruit()
        {
            Random r = new Random();
            rI = r.Next(0, _height - _sizeOfSides);
           int tempI = rI % _sizeOfSides;
            rI -= tempI;
            rJ = r.Next(0, _height - _sizeOfSides);
            int tempJ = rJ % _sizeOfSides;
            rJ -= tempJ;
            rI++;
            rJ++;
            fruit.Location=new Point(rI, rJ);
            this.Controls.Add(fruit);
        }
        private void _generateMap()
        {
            for(int i = 0; i < _width / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sizeOfSides * i);
                pic.Size = new Size(_width - 100, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= _height / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sizeOfSides * i,0);
                pic.Size = new Size(1, _width);
                this.Controls.Add(pic);
            }
        }
        private void _update(object MyObject, EventArgs eventArgs)
        {
            //cube.Location = new Point(cube.Location.X + dirx * _sizeOfSides, cube.Location.Y + diry * _sizeOfSides);
            _moveSnake();
            _eatFruit();
            _checkBorders();
        }
        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    dirx = 1;
                    diry = 0;
                    break;
                case "Left":
                    dirx = -1;
                    diry = 0;
                    break;
                case "Up":
                    dirx = 0;
                    diry = -1;
                    break;
                case "Down":
                    dirx = 0;
                    diry = 1;
                    break;
            }
        }
    }
}
