using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CCTavoli
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class IconaTavolo : UserControl
    {
        private int id =0;
        public IconaTavolo()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        public void cambiaNumero(int numero)
        {
            lbl_numeroTavolo.Content = numero;
        }

        public void Disponibile()
        {
            Resources["RectangleColorPreparazione"] = Resources["RectangleColorDisponibile"];
        }

        public void inPreparazione()
        {
            Resources["RectangleColorPreparazione"] = Resources["RectangleColorInCorso"];
            id = 1;
        }

        public void Finito()
        {
            Resources["RectangleColorPreparazione"] = Resources["RectangleColorPronto"];
            id = 2;
        }

         public int getNumeroTavolo()
        {
            return (int)lbl_numeroTavolo.Content;
        }
    }
}