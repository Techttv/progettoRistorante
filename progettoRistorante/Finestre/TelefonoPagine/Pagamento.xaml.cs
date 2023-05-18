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

namespace progettoRistorante.Finestre.TelefonoPagine
{
    /// <summary>
    /// Interaction logic for Pagamento.xaml
    /// </summary>
    public partial class Pagamento : Page
    {
        Frame Frame;
        public Pagamento(Frame frame)
        {
            InitializeComponent();
            this.Frame= frame;
        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Contactless(Frame);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ConfermaPagamento(Frame, "Contanti");
        }
    }
}
