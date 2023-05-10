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

namespace CCcustomPopup
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CCcustomPopup1 : UserControl
    {
        private int tavoli;
        public Window f1;
        public CCcustomPopup1(int tavoli, double Height, double Width)
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
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }


        public void aggiungiTavoli()
        {

        }
    }
}