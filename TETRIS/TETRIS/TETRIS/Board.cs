using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TETRIS
{
    class Board
    {
        internal static Board GameBoard
        {
            get;
            private set;
        }
        static Board()
        {
            GameBoard = new Board();
        }
        Board()
        {
        }
        int[,] board = new int[GameRule.BX, GameRule.BY];
        internal int this[int x, int y]
        {
            get
            {
                return board[x, y];
            }
        }
        internal bool MoveEnable(int bn, int tn, int x, int y) //움직일 수 있는지를 판단하는 함수
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        if (board[x + xx, y + yy] != 0) //BlockValue.bvals와 보드가 겹치면
                        {
                            return false; //움직이지 말라
                        }
                    }
                }
            }
            return true; //나머지는 움직여라
        }
        internal void Store(int bn, int turn, int x, int y) //블럭의 쌓임
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (((x + xx) >= 0) && (x + xx < GameRule.BX) && (y + yy >= 0) && (y + yy < GameRule.BY))
                        //x값이 보드와 안겹치고 y값도 보드에 안겹치면
                    {
                        board[x + xx, y + yy] += BlockValue.bvals[bn, turn, xx, yy]; //그 자리에 넣어라
                    }
                }
            }
            CheckLines(y + 3); //밑에서부터 체크
        }

        private void CheckLines(int y)//라인 체크
        {
            int yy = 0;
            for (yy = 0; (yy < 4); yy++)//밑에서부터 4개 라인
            {
                if (y - yy < GameRule.BY)//보드보단 크지않은
                {
                    if (CheckLine(y - yy))
                    {
                        ClearLine(y - yy);
                        y++;

                    }
                }
            }
        }

        private void ClearLine(int y) //꽉찬 라인은 밑으로 한칸씩 내려오게 만드는 함수
        {
            for (; y > 0; y--)
            {
                for (int xx = 0; xx < GameRule.BX; xx++)
                {
                    board[xx, y] = board[xx, y - 1];
                }
            }
        }

        private bool CheckLine(int y)
        {
            for (int xx = 0; xx < GameRule.BX; xx++)
            {
                if (board[xx, y] == 0) //하나라도 비어있으면
                {
                    return false; //지우지 마라
                }
            }
            return true;
        }

        internal void ClearBoard() //다시 게임할때 보드판을 지우는 함수
        {
            for (int xx = 0; xx < GameRule.BX; xx++)
            {
                for (int yy = 0; yy < GameRule.BY; yy++)
                {
                    board[xx, yy] = 0;
                }
            }
        }
    }
}