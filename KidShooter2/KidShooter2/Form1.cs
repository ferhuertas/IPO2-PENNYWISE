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

        int timeleft = 21;

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
        public int _hits = 0;
        public int _misses = 0;
        int _totalShots = 0;
        double _averageHits = 0;
        public int comidaganada;

        CGeorgie _georgie;
        CSangre _sangre;
        CPuntuacion _puntuacion;
        Random rnd = new Random();

        public KidShooter()
        {
            InitializeComponent();

            Bitmap b = new Bitmap(Resources.mirilla);
            this.Cursor = CCursor.CreateCursor(b, b.Height / 2, b.Width / 2);

            _georgie = new CGeorgie() { Left = 10, Top = 200 };
            _puntuacion = new CPuntuacion() { Left = 650, Top = 40 };

            _sangre = new CSangre();

            
            timerFin.Start();
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
            TextFormatFlags flags = TextFormatFlags.Right | TextFormatFlags.EndEllipsis;
            Font _font = new System.Drawing.Font("Century Gothic", 9, FontStyle.Bold);



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

            TextRenderer.DrawText(e.Graphics, "Shoots: " + _totalShots.ToString(), _font, new Rectangle(635, 92, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Hits: " + _hits.ToString(), _font, new Rectangle(635, 118, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Misses: " + _misses.ToString(), _font, new Rectangle(635, 142, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Accu: " + _averageHits.ToString("F0") + "%", _font, new Rectangle(635, 166, 120, 20), SystemColors.ControlText, flags);

            base.OnPaint(e);
        }

        private void KidShooter_MouseMove(object sender, MouseEventArgs e)
        {

            this.Refresh();
        }

        private void KidShooter_MouseClick(object sender, MouseEventArgs e)
        {
            if (timeleft <= 15)
            {
                Shoot();

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
        public void countDown()
        {
            SoundPlayer simpleSound = new SoundPlayer(Resources.countdown);
            simpleSound.Play();
        }

        private void timerFin_Tick(object sender, EventArgs e)
        {
            timeleft--;
            if(timeleft <= 19 && timeleft > 15)
            {
                labelCentral.Text = (timeleft - 16 ).ToString();
            }
            if (timeleft == 20) countDown();
            if (timeleft == 16) timerGameLoop.Start();
            if(timeleft <= 15)
            {
                labelCentral.Text = "";
                labelTuto.Text = "";
                labelTuto2.Text = "";
                progressBarTiempo.Value = timeleft;
            }
            
            if (timeleft == 0)
            {
                timerFin.Stop();
                timerGameLoop.Stop();
                comidaganada = (int)(_averageHits / 10);
                DialogResult result = MessageBox.Show(this, " Has ganado "+ comidaganada +" pieza(s) de comida.", "Fin del juego", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   this.Close();
            }
        }
    }
}
