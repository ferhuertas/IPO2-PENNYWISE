using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApp1
{
    public partial class Graficas : Form
    {

        string pathDirectory = Environment.CurrentDirectory.Replace("\\Debug", "");
        int totalaciertos, totalfallos, totalbdormir, totalbcomer, totalbjugar, totalcomidaperdida;

        private void labelBalas_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public Graficas()
        {
            InitializeComponent();
        }


        private void Graficas_Load(object sender, EventArgs e)
        {
            chart4.Titles.Add("Tiempo de juego en cada vida:");
            chart4.Series["Segundos de vida."].Points.Clear();

            readDB();

            //chart1.Series["Series1"]["PieLabelStyle"] = "Outside";
            chart1.Titles.Add("Seguimiento de pulsaciones:");

            chart1.Series["Veces que ha pulsado el botón:"].Points.Clear();
            chart1.Series["Veces que ha pulsado el botón:"].Points.AddXY("Dormir",MainWindow.miAvatar.bdormir);
            chart1.Series["Veces que ha pulsado el botón:"].Points.AddXY("Comer", MainWindow.miAvatar.bcomer);
            chart1.Series["Veces que ha pulsado el botón:"].Points.AddXY("Jugar", MainWindow.miAvatar.bjugar);


            chart3.Titles.Add("Punteria en el minijuego:");
            chart3.Series["SeriesPie"].Points.Clear();
            chart3.Series["SeriesPie"].IsValueShownAsLabel = true;
            chart3.Series["SeriesPie"].IsVisibleInLegend = true;
            chart3.Series["SeriesPie"].Points.AddXY("Aciertos", MainWindow.miAvatar.aciertos);
            chart3.Series["SeriesPie"].Points.AddXY("Fallos", MainWindow.miAvatar.fallos);


            labelCazados.Text = "" + totalaciertos;
            labelFallos.Text = "" + totalfallos;
            labelComer.Text = "" + totalbcomer;
            labelDormir.Text = "" + totalbdormir;
            labelJugar.Text = "" + totalbjugar;
            labelBalas.Text = "" + (totalaciertos + totalfallos);
            labelComidaDesperdiciada.Text = "" + totalcomidaperdida;
            labelPunteria.Text = "" + ((Convert.ToDouble(totalaciertos) / Convert.ToDouble(totalaciertos + totalfallos)) * 100).ToString("F0") + "%";


        }
        private void readDB()
        {
            // BBDD Access
            OleDbConnection myconect;
            OleDbCommand mycommand;

            myconect = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + pathDirectory + "\\Atributos.accdb");
            mycommand = myconect.CreateCommand();
            mycommand.CommandText = "SELECT * FROM Estadisticas";
            mycommand.CommandType = CommandType.Text;

            myconect.Open();
            Console.WriteLine("Conectado");
            OleDbDataReader DBreader = mycommand.ExecuteReader();
            
            while (DBreader.Read())
            {
                totalbcomer += Convert.ToInt16(DBreader["PulsarComer"].ToString());
                totalbjugar += Convert.ToInt16(DBreader["PulsarJugar"].ToString());
                totalbdormir += Convert.ToInt16(DBreader["PulsarDormir"].ToString());
                totalaciertos += Convert.ToInt16(DBreader["Aciertos"].ToString());
                totalfallos += Convert.ToInt16(DBreader["Fallos"].ToString());
                totalcomidaperdida += Convert.ToInt16(DBreader["ComidaRestante"].ToString());
                chart4.Series["Segundos de vida."].Points.AddXY("Vida " + Convert.ToInt16(DBreader["Id"].ToString()), Convert.ToInt16(DBreader["SegundosVida"].ToString()));
            }
            myconect.Close();


            
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}
