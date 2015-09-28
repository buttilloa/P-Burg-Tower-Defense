using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PburgTowerDefense
{
    public class Tile
    {
        
        public Rectangle boudningBox;
        public String Direction;
        public Tile(Rectangle rect, String Direction)
        {
            boudningBox = rect;
            this.Direction = Direction;
           
        }
    }
}
