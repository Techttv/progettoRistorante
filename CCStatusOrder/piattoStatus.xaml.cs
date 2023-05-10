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
using static System.Net.Mime.MediaTypeNames;

namespace CCStatusOrder
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class piattoStatus : UserControl
    {
        public piattoStatus()
        {
            InitializeComponent();
        }

        public void CambiaTesto(string testo)
        {
            txt_statdesc.Text = "• " + testo;
        }

        public void Disponibile()
        {
            icon_statimage.Source = new BitmapImage(new Uri(@"/Icon/Disponibile_icon.png", UriKind.Relative));
        }

        public void InCorso()
        {
            icon_statimage.Source = new BitmapImage(new Uri(@"/Icon/Preparazione_icon.png", UriKind.Relative));
        }

        public void Pronto()
        {
            icon_statimage.Source = new BitmapImage(new Uri(@"/Icon/DaFare_icon.png", UriKind.Relative));
        }
        public void coloreVerde()
        {
            txt_statdesc.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#609966"));
        }
    }
}