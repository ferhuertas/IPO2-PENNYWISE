using KidShooter2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Avatar miAvatar;
        DispatcherTimer temporizador;
        int intervalo; // intervalo en msec
        Storyboard sbcomerBoca;
        Storyboard sbdormir;
        Storyboard sbcomerBrazo;
        Storyboard sbsueño, sbhambre, sbaburrido, sbmuerto;
        bool parpadeo = true;
        bool muerto = false;
        bool reproduciendohambre = false;
        bool reproduciendosueño = false;
        bool reproduciendoaburrido = false;
        string pathDirectory = Environment.CurrentDirectory.Replace("\\Debug", "");
                MediaPlayer fondo;



        public MainWindow()
        {
            InitializeComponent();
            inicializarStoryBoards();
            PlayMainMusic();
            miAvatar = new Avatar();
            readDB();
            iniciarBarrasProgreso();
            this.textNumComida.Text = miAvatar.GetComida().ToString();
            intervalo = 1000;
            temporizador = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(intervalo)
            };
            temporizador.Tick += tickConsumoHandler;
            temporizador.Start();
            Console.WriteLine(pathDirectory);

        }

        private void iniciarBarrasProgreso()
        {
            this.PBApetito.Value = miAvatar.Apetito;
            this.PBDiversion.Value = miAvatar.Diversion;
            this.PBEnergia.Value = miAvatar.Energia;


        }
        private void inicializarStoryBoards()
        {
            sbcomerBoca = (Storyboard)this.VentanaAnimacion.Resources["StoryComerBoca"];
            sbdormir = (Storyboard)this.VentanaAnimacion.Resources["StoryDormir"];
            sbcomerBrazo = (Storyboard)this.VentanaAnimacion.Resources["StoryComerBrazo"];
            sbsueño = (Storyboard)this.VentanaAnimacion.Resources["StorySueño"];
            sbhambre = (Storyboard)this.VentanaAnimacion.Resources["StoryHambre"];
            sbaburrido = (Storyboard)this.VentanaAnimacion.Resources["StoryAburrido"];
            sbmuerto = (Storyboard)this.VentanaAnimacion.Resources["StoryMuerto"];
        }
        private void tickConsumoHandler(object sender, EventArgs e)
        {

            
            this.PBApetito.Value -= aleatorizar(0, 7);
            this.PBDiversion.Value -= aleatorizar(0, 3);
            this.PBEnergia.Value -= aleatorizar(0, 5);

            miAvatar.Apetito = this.PBApetito.Value;
            miAvatar.Diversion = this.PBDiversion.Value;
            miAvatar.Energia = this.PBEnergia.Value;
            miAvatar.svida += 1;


        //CONTROL DE PARPADEO DE BARRAS 

            if (this.PBApetito.Value < 33)
            {
                if (parpadeo == true)
                {
                    this.PBApetito.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                    this.PBApetito.BorderBrush = new SolidColorBrush(Colors.DimGray);
                parpadeo = !parpadeo;
            }
            else this.PBApetito.BorderBrush = new SolidColorBrush(Colors.DimGray);
  

            if (this.PBDiversion.Value < 33)
            {
                if (parpadeo == true)
                {
                    this.PBDiversion.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                    this.PBDiversion.BorderBrush = new SolidColorBrush(Colors.DimGray);
                parpadeo = !parpadeo;
            }
            else this.PBDiversion.BorderBrush = new SolidColorBrush(Colors.DimGray);

            if (this.PBEnergia.Value < 33)
            {
                if (parpadeo == true)
                {
                    this.PBEnergia.BorderBrush = new SolidColorBrush(Colors.Red);              
                }
                else
                    this.PBEnergia.BorderBrush = new SolidColorBrush(Colors.DimGray);
                parpadeo = !parpadeo;

            }
            else  this.PBEnergia.BorderBrush = new SolidColorBrush(Colors.DimGray);


            //CONTROL ANIMACIONES DE ESTADO

            if(miAvatar.Energia == 0 && miAvatar.Apetito == 0 && miAvatar.Diversion == 0)
            {
                muerto = true;
                Morir();

            }


            if (!muerto)
            {
                if (miAvatar.Energia <= 30 && !reproduciendosueño)
                {
                    sbhambre.Stop();
                    sbaburrido.Stop();
                    sbsueño.Begin();
                    reproduciendoaburrido = false;
                    reproduciendosueño = true;
                    reproduciendohambre = false;
                }
                else if (miAvatar.Energia > 30 && miAvatar.Apetito <= 30 && !reproduciendohambre)
                {
                    sbsueño.Stop();
                    sbaburrido.Stop();
                    sbhambre.Begin();
                    reproduciendoaburrido = false;
                    reproduciendosueño = false;
                    reproduciendohambre = true;
                }
                else if (miAvatar.Energia > 30 && miAvatar.Apetito > 30 && miAvatar.Diversion <= 30)
                {
                    sbsueño.Stop();
                    sbhambre.Stop();
                    sbaburrido.Begin();
                    reproduciendoaburrido = true;
                    reproduciendosueño = false;
                    reproduciendohambre = false;
                }
                else if (miAvatar.Energia > 30 && miAvatar.Apetito > 30 && miAvatar.Diversion > 30)
                {
                    sbsueño.Stop();
                    sbhambre.Stop();
                    sbaburrido.Stop();
                }
            }




            if (miAvatar.GetComida() == 0)
            {

                if (parpadeo == true)
                {
                    this.textNumComida.Foreground = new SolidColorBrush(Colors.Red);
                    this.textXComida.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    this.textNumComida.Foreground = new SolidColorBrush(Colors.White);
                    this.textXComida.Foreground = new SolidColorBrush(Colors.White);
                }
                parpadeo = !parpadeo;
            }
            else
            {
                this.textNumComida.Foreground = new SolidColorBrush(Colors.White);
                this.textXComida.Foreground = new SolidColorBrush(Colors.White);
            }
        }
        /// <summary>
        /// Método que genera un numero aleaorio usando Random
        /// desde uno hasta el límite max.
        /// </summary>
        /// <param name="max">Límite superior para generar el aleatorio</param>
        /// <returns>El número aleatorio generado</returns>
        private int aleatorizar(int min,int max)
        {
            Random generadorAleat = new Random();
            return 1 + generadorAleat.Next(min,max);
        }
        private void BEnergia_Click(object sender, RoutedEventArgs e)
        {

            PararAnimaciones();
            sbdormir.SpeedRatio = 2;
            sbdormir.Begin();

            this.PBEnergia.Value += aleatorizar(12, 25);
            miAvatar.bdormir += 1;
        }
        /// <summary>
        /// Manejador del evento de click sobre el botón de apetito
        /// </summary>
        /// <param name="sender">Origen del evento</param>
        /// <param name="e">Params del evento</param>
        /// <remarks>
        /// Comentarios adicionales que no se verán en el autocompletado de
        //IntelliSense
        /// </remarks>
        private void BApetito_Click(object sender, RoutedEventArgs e)
        {
            if(miAvatar.GetComida()== 0)
            {
            }
            else
            {
                PararAnimaciones();
                sbcomerBoca.SpeedRatio = 2;
                sbcomerBrazo.SpeedRatio = 2;
                sbcomerBoca.Begin();
                sbcomerBrazo.Begin();
                PlayEat();
                this.PBApetito.Value += aleatorizar(10, 20);
                miAvatar.SetComida(miAvatar.GetComida() - 1);
                miAvatar.bcomer += 1;
                this.textNumComida.Text = miAvatar.GetComida().ToString();
            }

        }
        private void BDiversion_Click(object sender, RoutedEventArgs e)
        {

            PararAnimaciones();

            this.PBDiversion.Value += aleatorizar(28, 43);


            KidShooter2.KidShooter mw = new KidShooter2.KidShooter();
            temporizador.Stop();
            mw.ShowDialog();
            miAvatar.SetComida(miAvatar.GetComida() + mw.comidaganada);
            miAvatar.bjugar += 1;
            miAvatar.aciertos += mw._hits;
            miAvatar.fallos += mw._misses;
            this.textNumComida.Text = miAvatar.GetComida().ToString();
            temporizador.Start();
        }
        private void PararAnimaciones()
        {
            sbdormir.Stop();
            sbcomerBoca.Stop();
            sbcomerBrazo.Stop();
            sbsueño.Stop();
            sbhambre.Stop();
            sbaburrido.Stop();
            reproduciendoaburrido = false;
            reproduciendosueño = false;
            reproduciendohambre = false;
        }

        private void Morir()
        {
            PararAnimaciones();
            temporizador.Stop();
            PlayDead();
            sbmuerto.Begin();
            // Configure the message box to be displayed
            DialogResult respuesta = new DialogResult();
            Form mensaje = new MessageBoxCustom();
            respuesta = mensaje.ShowDialog();

            // Process message box results
            switch (respuesta)
            {
                case System.Windows.Forms.DialogResult.Yes:
                    Revivir();
                    break;
                case System.Windows.Forms.DialogResult.No:
                    Close();
                    break;

            }

        }

        private void Revivir()
        {
            sbmuerto.Stop();
            this.PBApetito.Value = 100;
            this.PBDiversion.Value = 100;
            this.PBEnergia.Value = 100;
            temporizador.Start();
            miAvatar.vida += 1;
            miAvatar.reiniciarvariables();
            muerto = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PararAnimaciones();
            temporizador.Stop();
            writeDB();
            // Configure the message box to be displayed
            DialogResult respuesta = new DialogResult();
            Form mensaje = new Graficas();
            respuesta = mensaje.ShowDialog();
            if (respuesta == System.Windows.Forms.DialogResult.Cancel)
            {
                temporizador.Start();
            }
            
        }

        private void readDB()
        {
            // BBDD Access
            OleDbConnection myconect;
            OleDbCommand mycommand;
            OleDbCommand mycommand2;

            myconect = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + pathDirectory + "\\Atributos.accdb");
            mycommand = myconect.CreateCommand();
            mycommand.CommandText = "SELECT * FROM Atributos WHERE Id=1";
            mycommand.CommandType = CommandType.Text;

            myconect.Open();
            Console.WriteLine("Conectado");
            OleDbDataReader DBreader = mycommand.ExecuteReader();
            while (DBreader.Read())
            {
                miAvatar.Energia = Convert.ToDouble(DBreader["Energia"].ToString());
                miAvatar.Apetito = Convert.ToDouble(DBreader["Apetito"].ToString());
                miAvatar.Diversion = Convert.ToDouble(DBreader["Diversion"].ToString());
                miAvatar.Comida = Convert.ToInt16(DBreader["Comida"].ToString());
                miAvatar.vida = Convert.ToInt16(DBreader["VidaActual"].ToString());
            }
            myconect.Close();


            mycommand2 = myconect.CreateCommand();
            mycommand2.CommandText = "SELECT * FROM Estadisticas WHERE Id="+ miAvatar.vida+"";
            mycommand2.CommandType = CommandType.Text;

            myconect.Open();
            Console.WriteLine("Conectado");
            DBreader = mycommand2.ExecuteReader();

            while (DBreader.Read())
            {
                miAvatar.bcomer = Convert.ToInt16(DBreader["PulsarComer"].ToString());
                miAvatar.bjugar = Convert.ToInt16(DBreader["PulsarJugar"].ToString());
                miAvatar.bdormir = Convert.ToInt16(DBreader["PulsarDormir"].ToString());
                miAvatar.aciertos = Convert.ToInt16(DBreader["Aciertos"].ToString());
                miAvatar.fallos = Convert.ToInt16(DBreader["Fallos"].ToString());
                miAvatar.svida = Convert.ToInt16(DBreader["SegundosVida"].ToString());
            }
            myconect.Close();


        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            temporizador.Stop();
            PararAnimaciones();
            miAvatar.Energia = this.PBEnergia.Value;
            miAvatar.Apetito = this.PBApetito.Value;
            miAvatar.Diversion = PBDiversion.Value;
            miAvatar.Comida = Int32.Parse(this.textNumComida.Text); 
            writeDB();
            
                }

        private void writeDB()
        {
            OleDbConnection myconect;
            OleDbCommand mycommand;
            OleDbCommand mycommand2;

            myconect = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + pathDirectory + "\\Atributos.accdb");
            mycommand = myconect.CreateCommand();
            mycommand.CommandText = "UPDATE Atributos SET Energia='" + miAvatar.Energia + "', Apetito='" + miAvatar.Apetito + "', Diversion='" + miAvatar.Diversion + "', Comida='" + miAvatar.Comida + "', VidaActual='"+ miAvatar.vida +"' WHERE Id=1;";
            mycommand.CommandType = CommandType.Text;

            mycommand2 = myconect.CreateCommand();
            mycommand2.CommandText = "SELECT COUNT(*) FROM Estadisticas WHERE ID="+miAvatar.vida+"";
            myconect.Open();
            int result = Convert.ToInt32(mycommand2.ExecuteScalar());
            myconect.Close();

            if (result == 0) {
                mycommand2.CommandText = "INSERT into Estadisticas(PulsarComer, PulsarJugar, PulsarDormir, Aciertos, Fallos, ComidaRestante, SegundosVida) values('" + miAvatar.bcomer + "','" + miAvatar.bjugar + "', '" + miAvatar.bdormir + "', '" + miAvatar.aciertos + "', '" + miAvatar.fallos + "', '" + miAvatar.Comida + "', '"+miAvatar.svida+"')";

            }
            else {
                mycommand2.CommandText = "UPDATE Estadisticas SET PulsarComer='" + miAvatar.bcomer + "', PulsarJugar='" + miAvatar.bjugar + "', PulsarDormir='" + miAvatar.bdormir + "', ComidaRestante='" + miAvatar.Comida + "', Aciertos='" + miAvatar.aciertos + "', Fallos='" + miAvatar.fallos + "', SegundosVida= '"+miAvatar.svida+"' WHERE Id=" + miAvatar.vida + "";

            }
            mycommand2.CommandType = CommandType.Text;
          

            myconect.Open();
            mycommand.ExecuteNonQuery();
            mycommand2.ExecuteNonQuery();
            myconect.Close();
        }

        public void PlayMainMusic()
        {

            string fullPath_sonidoFondo = pathDirectory + "\\MainTheme.wav";
            fondo = new MediaPlayer();
            fondo.Open(new Uri(fullPath_sonidoFondo));
            fondo.Volume = 0.10;
            fondo.Play();

        }
        public void PlayEat()
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.eating2);         
            simpleSound.Play();
        }

        public void PlayDead()
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.dead);
            simpleSound.Play();
        }




    }
}

