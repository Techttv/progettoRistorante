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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace progettoRistorante.Finestre.TelefonoPagine
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Frame frame;
        public Home(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }
        private void btn_nuovoOrdine_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new NuovoOrdine(frame);
        }

        private void btn_modificaOrdine_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new ModificaOrdine(frame);
        }

        private void btn_paga_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Paga(frame);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 1;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleAnimation.AutoReverse = false;

            frame.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
        }

       


    }
}
