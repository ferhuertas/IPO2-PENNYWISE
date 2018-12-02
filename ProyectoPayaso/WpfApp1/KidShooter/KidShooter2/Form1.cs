#define My_Debug

using KidShooter2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KidShooter2
{
    public partial class KidShooter : Form
    {

        // declare 2 variables that control the update time of the game.
        const int FrameNum = 8; // equivalent with period of 800ms
        const int SplatNum = 3;// equivalent with period of 300ms

        //we need this boolean splat to check if the splash symbol should be display or not.
        //if the mole is shoot : splat = true.
        //if splat = true, we check if splash symbol has been appeared enough in 300ms or not?.
        //if splat = true = splash symbol has been appeard enough in 300ms --> splat = false again
        //the intial value is false
        bool splat = false;

        //this _gameFrame variable will increase every time the timer trigger.
        //the timer interval is 100ms so the timer will trigger every 100ms.
        //means that this variable will increase by 1 every 100ms.
        //when _gameFrame> FrameNum = 8 --> _gameFrame = 0 again.
        int _gameFrame = 0;

        //this _splatTime counter only increase by 1 everytime the timer trigger.
        //when the boolean splat = true.
        //when _splatTime > SplatNum = 3 --> _splatTime = 0 again.
        int _splatTime = 0;

        //these 4 variables will display the score/game result status of gamer.
        //type of _avarageHits is double because it = _hits/_totalShots.
        int _hits = 0;
        int _misses = 0;
        int _totalShots = 0;
        double _averageHits = 0;


        int _cursX = 0;
        int _cursY = 0;
        CGeorgie _georgie;
        CSangre _sangre;
        CMenu _menu;
        CPuntuacion _puntuacion;
        Random rnd = new Random();

        public KidShooter()
        {
            InitializeComponent();

            Bitmap b = new Bitmap(Resources.mirilla);
            this.Cursor = CCursor.CreateCursor(b, b.Height / 2, b.Width / 2);

            _georgie = new CGeorgie() { Left = 10, Top = 200 };
            _menu = new CMenu() { Left = 480, Top = 10 };
            _puntuacion = new CPuntuacion() { Left = 5, Top = 10 };
            _sangre = new CSangre();
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            if (_gameFrame >= FrameNum)
            {
                UpdateGeorgie();
                _gameFrame = 0;
            }

            if (splat)
            {
                if (_splatTime >= SplatNum)
                {
                    splat = false;
                    _splatTime = 0;
                    UpdateGeorgie();
                }
                _splatTime++;
            }

            _gameFrame++;
            this.Refresh();
        }
        private void UpdateGeorgie()
        {
            _georgie.Update(rnd.Next(Resources.georgie.Width, this.Width - Resources.georgie.Width),
                         rnd.Next(this.Height / 3, this.Height - Resources.georgie.Height * 2)
                         );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font _font = new System.Drawing.Font("Stencil", 10, FontStyle.Regular);



            if (splat == true)
            {
                _georgie.DrawImage(dc);
                _sangre.DrawImage(dc);
            }
            //else draw _mole object
            else
            {
                _georgie.DrawImage(dc);
            }

            _puntuacion.DrawImage(dc);
            _menu.DrawImage(dc);


            TextRenderer.DrawText(e.Graphics, "Disparos :" + _totalShots.ToString(), _font, new Rectangle(0, 22, 120, 20), SystemColors.ControlLightLight, flags);
            TextRenderer.DrawText(e.Graphics, "Aciertos :" + _hits.ToString(), _font, new Rectangle(0, 42, 120, 20), SystemColors.ControlLightLight, flags);
            TextRenderer.DrawText(e.Graphics, "Fallos :" + _misses.ToString(), _font, new Rectangle(0, 62, 120, 20), SystemColors.ControlLightLight, flags);
            TextRenderer.DrawText(e.Graphics, "Punteria :" + _averageHits.ToString("F0") + "%", _font, new Rectangle(0, 82, 120, 20), SystemColors.ControlLightLight, flags);

            base.OnPaint(e);
        }

        private void KidShooter_MouseMove(object sender, MouseEventArgs e)
        {
            _cursX = e.X;
            _cursY = e.Y;
            this.Refresh();
        }

        private void KidShooter_MouseClick(object sender, MouseEventArgs e)
        {
            Shoot();
            if (e.X > 515 && e.X < 585 && e.Y > 55 && e.Y < 77)
            {
                timerGameLoop.Start();
            }

            else if (e.X > 515 && e.X < 585 && e.Y > 80 && e.Y < 102)
            {
                timerGameLoop.Stop();
            }

            else if (e.X > 515 && e.X < 585 && e.Y > 105 && e.Y < 128)
            {
 
            }

            else if (e.X > 515 && e.X < 585 && e.Y > 131 && e.Y < 157)
            {
                timerGameLoop.Stop();
                DialogResult result = MessageBox.Show(this, " ¿De verdad quieres salir de este maravilloso juego?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                //Hit condition which is called from Hit() method in CMole class
                //(e.X, e.Y) here is the center point position, not mole position
                if (_georgie.Hit(e.X, e.Y))
                {
                    splat = true;
                    _sangre.Left = _georgie.Left - Resources.sangre.Width / 9;
                    _sangre.Top = _georgie.Top - Resources.sangre.Height / 6;
                    _hits++;
                }
                else { _misses++; }

                _totalShots = _hits + _misses;
                _averageHits = (double)_hits / (double)_totalShots * 100.0;
            }
        }
        public void Shoot()
        {
            SoundPlayer simpleSound = new SoundPlayer(Resources.Shoot);
            simpleSound.Play();
        }

    }
}
