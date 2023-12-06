using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TETRIS 
{ 
    class Diagram //벽돌의 움직임
    {
        internal int X // X좌표 캡슐화
        {
            get;
            private set;
        }
        internal int Y //Y 좌표 캡슐화
        {
            get;
            private set;
        }
        internal int Turn
        {
            get;
            private set;
        }
        internal int BlockNum
        {
            get;
            private set;
        }

        internal Diagram() //벽돌이 바닥에 닿은후 다시 시작좌표로
        {
            Reset();
        }

        internal void Reset()
        {
            Random random = new Random();
            X = GameRule.SX; //GameRule에 있는 벽돌 시작좌표
            Y = GameRule.SY;
            Turn = random.Next() % 4; //
            BlockNum = random.Next() % 7; //7개 랜덤으로
        }
        internal void MoveLeft() //벽돌이 왼쪽으로 이동
        {
            X--;
        }
        internal void MoveRight() //벽돌이 오른쪽으로 이동
        {
            X++;
        }
        internal void MoveDown() //벽돌이 아래쪽으로 이동
        {
            Y++;
        }
        internal void MoveTurn()
        {
            Turn = (Turn + 1) % 4; //0번,1번,2번,3번 순으로 회전
        }
    }
}