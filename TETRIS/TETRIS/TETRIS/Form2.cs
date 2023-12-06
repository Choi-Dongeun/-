using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TETRIS
{
    public partial class Form2 : Form
    {
        Game game;
        int bx;
        int by;
        int bwidth;
        int bheight;
        int scorepoint;
        public Form2()
        {
            InitializeComponent();
        }


        private void DrawBoard(Graphics graphics) //보드의 크기를 결정함
        {
            for (int xx = 0; xx < bx; xx++)
            {
                for (int yy = 0; yy < by; yy++)
                {
                    if (game[xx, yy] != 0)
                    {
                        Rectangle now_rt = new Rectangle(xx * bwidth + 2, yy * bheight + 2, bwidth - 4, bheight - 4);
                        graphics.DrawRectangle(Pens.Gray, now_rt);
                        graphics.FillRectangle(Brushes.Gray, now_rt);
                    }
                }
            }
        }

        private void DrawDiagram(Graphics graphics)//블록을 그림
        {
            Pen dpen = new Pen(Color.Black, 4);
            Point now = game.NowPosition;
            int bn = game.BlockNum;
            int tn = game.Turn;
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle now_rt = new Rectangle((now.X + xx) * bwidth + 2, (now.Y + yy) * bheight + 2, bwidth - 4, bheight - 4);
                        graphics.DrawRectangle(dpen, now_rt);
                        if (bn == 0)
                        {
                            graphics.FillRectangle(Brushes.Red, now_rt);

                        }
                        else if (bn == 1)
                        {
                            graphics.FillRectangle(Brushes.SkyBlue, now_rt);
                        }
                        else if (bn == 2)
                        {
                            graphics.FillRectangle(Brushes.Green, now_rt);
                        }
                        else if (bn == 3)
                        {
                            graphics.FillRectangle(Brushes.Yellow, now_rt);
                        }
                        else if (bn == 4)
                        {
                            graphics.FillRectangle(Brushes.Orange, now_rt);
                        }
                        else if (bn == 5)
                        {
                            graphics.FillRectangle(Brushes.Navy, now_rt);
                        }
                        else if (bn == 6)
                        {
                            graphics.FillRectangle(Brushes.Violet, now_rt);
                        }
                    }
                }
            }
        }

        private void DrawGraduation(Graphics graphics) //모눈종이 만들기
        {
            DrawHorizons(graphics);
            DrawVerticals(graphics);
        }

        private void DrawVerticals(Graphics graphics)//모눈종이의 수직선
        {
            Point st = new Point();
            Point et = new Point();

            for (int cx = 0; cx < bx; cx++)
            {
                st.X = cx * bwidth;
                st.Y = 0;
                et.X = st.X;
                et.Y = by * bheight;

                graphics.DrawLine(Pens.Black, st, et);
            }
        }

        private void DrawHorizons(Graphics graphics)//모눈종이의 수평선
        {
            Point st = new Point();
            Point et = new Point();

            for (int cy = 0; cy < by; cy++)
            {
                st.X = 0;
                st.Y = cy * bheight;
                et.X = bx * bwidth;
                et.Y = cy * bheight;

                graphics.DrawLine(Pens.Black, st, et);
            }
        }

        private void MoveSSDown()
        {
            while (game.MoveDown())
            {
                Region rg = MakeRegion(0, -1);
                Invalidate(rg);
            }
            EndingCheck();
        }

        private void MoveTurn()
        {
            if (game.MoveTurn())
            {
                Region rg = MakeRegion();
                Invalidate(rg);
            }
        }

        private void MoveDown()
        {
            if (game.MoveDown())
            {
                Region rg = MakeRegion(0, -1);
                Invalidate(rg);
            }
            else
            {
                EndingCheck();
            }
        }

        private void EndingCheck()//엔딩조건 체크
        {
            if (game.Next())//진행가능한가?
            {
                Invalidate();
            }
            else //아니라면
            {
                timer1.Enabled = false;//타이머를 멈춤

                if (DialogResult.Yes == MessageBox.Show(" 계속 하실건가요?", "계속 진행 확인 창", MessageBoxButtons.YesNo))
                {
                    game.ReStart();
                    timer1.Enabled = true;
                    Invalidate();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void MoveLeft()
        {
            if (game.MoveLeft())
            {
                Region rg = MakeRegion(1, 0);
                Invalidate(rg);
            }
        }

        private Region MakeRegion(int cx, int cy) //방향키에 대한 리전(움직이고 그 전 블록 삭제)
        {
            Point now = game.NowPosition;

            int bn = game.BlockNum;
            int tn = game.Turn;
            Region region = new Region();
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * bwidth + 2, (now.Y + yy) * bheight + 2, bwidth - 4, bheight - 4);
                        Rectangle rect2 = new Rectangle((now.X + cx + xx) * bwidth, (now.Y + cy + yy) * bheight, bwidth, bheight);
                        Region rg1 = new Region(rect1);
                        Region rg2 = new Region(rect2);
                        region.Union(rg1);
                        region.Union(rg2);
                    }
                }
            }
            return region;
        }
        private Region MakeRegion() //turn에 대한 리전 (회전하고 그 전 모야 블럭 삭제)
        {
            Point now = game.NowPosition;

            int bn = game.BlockNum;
            int tn = game.Turn;
            int oldtn = (tn + 3) % 4;
            Region region = new Region();
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * bwidth + 2, (now.Y + yy) * bheight + 2, bwidth - 4, bheight - 4);
                        Region rg1 = new Region(rect1);
                        region.Union(rg1);
                    }
                    if (BlockValue.bvals[bn, oldtn, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * bwidth + 2, (now.Y + yy) * bheight + 2, bwidth - 4, bheight - 4);
                        Region rg1 = new Region(rect1);
                        region.Union(rg1);
                    }
                }
            }
            return region;
        }
        private void MoveRight()
        {
            if (game.MoveRight())
            {
                Region rg = MakeRegion(-1, 0);
                Invalidate(rg);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            scorepoint++;
            label1.Text = "점수 : " + scorepoint;
            MoveDown();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            DoubleBuffered = true;

            DrawGraduation(e.Graphics);
            DrawDiagram(e.Graphics);
            DrawBoard(e.Graphics);
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right: MoveRight(); return;
                case Keys.Left: MoveLeft(); return;
                case Keys.Space: MoveSSDown(); return;
                case Keys.Up: MoveTurn(); return;
                case Keys.Down: MoveDown(); return;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            game = Game.Singleton;
            bx = GameRule.BX;
            by = GameRule.BY;
            bwidth = GameRule.B_WIDTH;
            bheight = GameRule.B_HEIGHT;
            this.SetClientSizeCore(GameRule.BX * GameRule.B_WIDTH+250, GameRule.BY * GameRule.B_HEIGHT);
        }
    }
}