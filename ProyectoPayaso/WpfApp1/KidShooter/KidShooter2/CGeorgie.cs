using KidShooter2.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidShooter2
{
    //ensure that CGeorgiee cannot act as a base class to others.
    sealed class CGeorgie : CImageBase
    {
        //Create _georgieRectangulo method at type rectangle
        private Rectangle _georgieRectangulo = new Rectangle();


        public CGeorgie()
            : base(Resources.georgie)
        {
            // intialize _georgieRectangulo rectangle
            _georgieRectangulo.X = Left + 20;
            _georgieRectangulo.Y = Top - 1;
            _georgieRectangulo.Width = 89;
            _georgieRectangulo.Height = 100;
        }



        //Update _georgieRectangulo rectangle when UpdateMole() method change mole (X,Y) position
        public void Update(int X, int Y)
        {
            Left = X;
            Top = Y;
            _georgieRectangulo.X = Left + 20;
            _georgieRectangulo.Y = Top - 1;
        }

        //Hit method at boolean type, return true or false
        //it will draw a rectangle (X, Y, 1, 1) with (X,Y) is center point coordinator and (1,1) 
        //is the length and the width of the rectangle
        //if _georgieRectangulo contains ( X, Y, 1,1) --> it means Hit and return true
        public bool Hit(int X, int Y)
        {
            Rectangle c = new Rectangle(X, Y, 1, 1);
            if (_georgieRectangulo.Contains(c))
            {
                return true;
            }
            return false;
        }
    }
}
