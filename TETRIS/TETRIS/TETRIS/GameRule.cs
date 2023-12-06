using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TETRIS
{
    static class GameRule
    {
        internal const int B_WIDTH = 30; //게임 X좌표 1의 Pixel수
        internal const int B_HEIGHT = 30; //게임 Y좌표 1의 Pixel수
        internal const int BX = 12; //게임 보드의 폭(B_WIDTH*BX Pixels)
        internal const int BY = 20; //게임 보드의 높이(B_HEIGHT*BX Pixels)
        internal const int SX = 4; //시작 X 좌표
        internal const int SY = 0; //시작 Y 좌표
    }
}
//가로 30 PIxel 세로 30 Plxel 의 크기를 가진 블럭 생성
//가로 12 블록(BX) 세로 20 블록(BY) 크기의 게임 보드
//게임 시작 후 떨어지는 블록의 좌표 (4, 0)(시잗점)