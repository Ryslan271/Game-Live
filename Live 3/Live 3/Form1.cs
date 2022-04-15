using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Live_3
{
    public partial class Form1 : Form
    {
        Graphics gr;       //объявляем объект - графику, на которой будем рисовать
        Pen p;             //объявляем объект - карандаш, которым будем рисовать контур

        // нажатие мышки
        int Mx;
        int My;

        // остановка жизни
        bool stop = false;

        // цвет заливки черный(живой)
        SolidBrush brush = new SolidBrush(Color.Black);

        // цвет заливки белый(мертвый)
        SolidBrush brushDead = new SolidBrush(Color.FromKnownColor(KnownColor.Control));

        // перо для рисования
        Pen pen = new Pen(Color.Black);

        // тут хранятся все клетки
        public List<Rectangle> rectangles = new List<Rectangle>();

        // тут живые клетки
        public List<Rectangle> liveDr = new List<Rectangle>();

        public Form1()
        {
            InitializeComponent();
        }

        //опишем функцию, которая будет рисовать круг по координатам его центра
        void DrowRectangle(int x, int y, EventArgs у)
        {
            this.rectangles.Add(new Rectangle(x, y, 20, 20));
        }

        private void Print(EventArgs e)
        {
            foreach (Rectangle dr in this.rectangles)
            {
                gr.DrawRectangle(this.pen, dr);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            foreach (Rectangle dr in this.rectangles)
            {
                if (dr.Contains(this.Mx, this.My))
                {
                    gr.FillRectangle(this.brush, dr); // меняем цвет клетки если она была нажата
                    liveDr.Add(dr); // записывает живую клетку
                }
            }
            timer1.Enabled = false;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Visible = false;

            gr = pictureBox1.CreateGraphics();  //инициализируем объект типа графики

            p = new Pen(Color.Black);

            int x, y;

            for (int i = 0; i < 600; i += 20)
            {
                for (int j = 0; j < 600; j += 20)
                {
                    x = i;
                    y = j;
                    DrowRectangle(x, y, e);
                }
            }

            Print(e);
            
        }

        // определяем координаты нажатия мышью
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Mx = Convert.ToInt32(e.X);
            this.My = Convert.ToInt32(e.Y);

            timer1.Interval = 1; // Задаем интервал таймеру
            timer1.Enabled = true; // Запускаем таймер
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.rectangles.Clear();
            gr.Clear(Color.FromKnownColor(KnownColor.Control));
            button1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            List<Rectangle> XyDead = new List<Rectangle>();

            while (stop == false)
            {
                foreach (Rectangle dr in this.rectangles)
                {
                    foreach (Rectangle l in this.liveDr)
                    {
                        if (dr.X - 20 > 0 & dr.Y - 20 > 0 & dr.X + 20 != 600 & dr.Y + 20 != 600 & dr.Y + 20 <= 580 & dr.X - 20 <= 580 & dr.Y - 20 <= 0) // это вычисление когда вокруг есть место
                        {
                            if ((dr.Contains(l.X - 20, l.Y - 20) & dr.Contains(l.X, l.Y - 20) & dr.Contains(l.X + 20, l.Y - 20)
                                & dr.Contains(l.X - 20, l.Y) & dr.Contains(l.X - 20, l.Y + 20) & dr.Contains(l.X, l.Y + 20)
                                & dr.Contains(l.X + 20, l.Y + 20) & dr.Contains(l.X + 20, l.Y)) || (dr.Contains(l.X - 20, l.Y) &
                                dr.Contains(l.X, l.Y + 20) & dr.Contains(l.X + 20, l.Y)) || (dr.Contains(l.X - 20, l.Y + 20) &
                                dr.Contains(l.X - 20, l.Y) & dr.Contains(l.X, l.Y - 20)) || (dr.Contains(l.X + 20, l.Y - 20) &
                                dr.Contains(l.X + 20, l.Y) & dr.Contains(l.X, l.Y - 20)) || (dr.Contains(l.X + 20, l.Y) &
                                dr.Contains(l.X + 20, l.Y + 20) & dr.Contains(l.X, l.Y + 20)) || (dr.Contains(l.X, l.Y + 20) &
                                dr.Contains(l.X - 20, l.Y) & dr.Contains(l.X - 20, l.Y + 20)) || (dr.Contains(l.X - 20, l.Y - 20) &
                                dr.Contains(l.X + 20, l.Y - 20) & dr.Contains(l.X - 20, l.Y + 20)) || (dr.Contains(l.X - 20, l.Y - 20) &
                                dr.Contains(l.X + 20, l.Y + 20) & dr.Contains(l.X - 20, l.Y + 20)) || (dr.Contains(l.X, l.Y - 20) &
                                dr.Contains(l.X + 20, l.Y - 20) & dr.Contains(l.X + 20, l.Y + 20)) || (dr.Contains(l.X - 20, l.Y) &
                                dr.Contains(l.X + 20, l.Y + 20) & dr.Contains(l.X + 20, l.Y - 20)))
                            {
                                liveDr.Add(dr); // записывает живую клетку
                                gr.FillRectangle(this.brush, dr);
                                Thread.Sleep(1);
                            }
                        }
                        else if (dr.X + 20 == 600 & dr.Y + 20 == 600) // это вычисление правого нижнего угла
                        {
                            if (dr.Contains(l.X - 20, l.Y - 20) & dr.Contains(l.X, l.Y - 20) & dr.Contains(l.X - 20, l.Y))
                            {
                                liveDr.Add(dr); // записывает живую клетку
                                gr.FillRectangle(this.brush, dr);
                                Thread.Sleep(1);
                            }
                        }
                        else if (dr.X + 20 == 600 & dr.Y - 20 > 0 & dr.Y + 20 != 600) // это вычисление правого вверхнего угла
                        {
                            if (dr.Contains(l.X - 20, l.Y) & dr.Contains(l.X, l.Y + 20) & dr.Contains(l.X - 20, l.Y + 20))
                            {
                                liveDr.Add(dr); // записывает живую клетку
                                gr.FillRectangle(this.brush, dr);
                                Thread.Sleep(1);
                            }
                        }
                        else if (dr.X - 20 < 0 & dr.Y - 20 < 0) // это вычисление левого вверхнего угла
                        {
                            if (dr.Contains(l.X, l.Y + 20) & dr.Contains(l.X + 20, l.Y) & dr.Contains(l.X + 20, l.Y + 20))
                            {
                                liveDr.Add(dr); // записывает живую клетку
                                gr.FillRectangle(this.brush, dr);
                                Thread.Sleep(1);
                            }
                        }

                        else if (dr.X - 20 < 0 & dr.Y + 20 == 600) // это вычисление левого нижнего угла
                        {
                            if (dr.Contains(l.X, l.Y - 20) & dr.Contains(l.X + 20, l.Y - 20) & dr.Contains(l.X + 20, l.Y))
                            {
                                liveDr.Add(dr); // записывает живую клетку
                                gr.FillRectangle(this.brush, dr);
                                Thread.Sleep(1);
                            }
                        }
                        else
                        {
                            XyDead.Add(dr);
                            gr.FillRectangle(this.brushDead, dr);
                            Thread.Sleep(1);
                        }
                    }

                    foreach (Rectangle dead in XyDead)
                    {
                        liveDr.Remove(dead);
                    }

                    timer1.Interval = 10; // Задаем интервал таймеру
                    timer1.Enabled = true; // Запускаем таймер
                }
                Thread.Sleep(10);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.stop = true;
        }
    }
}

