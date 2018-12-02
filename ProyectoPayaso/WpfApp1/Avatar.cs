using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Avatar
    {
       public double Apetito;
       public double Diversion;
        public double Energia;
        public int Comida;
        public int vida;
        public int bcomer, bdormir, bjugar, aciertos, fallos, svida;

        public Avatar (double apetito, double diversion, double energia)
        {
            this.Apetito = apetito;
            this.Diversion = diversion;
            this.Energia = energia;
            this.Comida = 10;
            
        }
        public Avatar()
        { }
        public void SetComida (int comida)
        {
            this.Comida = comida;
        }
        public int GetComida()
        {
            return this.Comida;
        }
        public void reiniciarvariables()
        {
            bcomer = 0;
            bdormir = 0;
            bjugar = 0;
            aciertos = 0;
            fallos = 0;
            svida = 0;
        }
    }
}
