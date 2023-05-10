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

namespace CCStatusOrder
{
    /// <summary>
    /// Interaction logic for fornello.xaml
    /// </summary>
    public partial class fornello : UserControl
    {
        private int id = 0;
        public fornello()
        {
            InitializeComponent();
        }

        public void cambiaDesc(string desc)
        {
            lbl_desc.Content = desc;
        }

        public void Disponibile()
        {
            Resources["RectangleColorPreparazione"] = Resources["RectangleColorDisponibile"];
            icona_status.Source = new BitmapImage(new Uri("/progettoRistorante;component/Icon/Disponibile_icon.png", UriKind.Relative));
        }

        public void inPreparazione()
        {
            Resources["RectangleColorPreparazione"] = Resources["RectangleColorInCorso"];
            icona_status.Source = new BitmapImage(new Uri("/progettoRistorante;component/Icon/Preparazione.png", UriKind.Relative));
            id = 1;
        }

        public void Finito()
        {
            Resources["RectangleColorPreparazione"] = Resources["RectangleColorPronto"];
            icona_status.Source = new BitmapImage(new Uri("/progettoRistorante;component/Icon/DaFare_icon.png", UriKind.Relative));
            id = 2;
        }
    }
}
