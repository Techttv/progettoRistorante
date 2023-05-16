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

namespace progettoRistorante.UserControllers
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ModificaTavoli : UserControl
    {
        private int tavoli;
        public MainWindow f1;
        
        public ModificaTavoli(int tavoli, double Height, double Width)
        {
            InitializeComponent();
            txt_numero.Text = tavoli.ToString();
            this.tavoli = tavoli;
            Window1.Height = Height; Window1.Width = Width;
        }
        

        private void btn_meno_Click(object sender, RoutedEventArgs e)
        {
            if (tavoli > 0)
            {
                tavoli--;
                txt_numero.Text = tavoli.ToString();
            }

        }

        private void btn_aggiungi_Click(object sender, RoutedEventArgs e)
        {
            tavoli++;
            txt_numero.Text = tavoli.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            f1.aggiungiTavoli(int.Parse(txt_numero.Text));
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            f1.aggiungiTavoli(int.Parse(txt_numero.Text));
        }


    }
}