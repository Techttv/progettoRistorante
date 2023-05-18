using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Pagamento.xaml
    /// </summary>
    public partial class Contactless : Page
    {
        Frame Frame;
        public Contactless(Frame frame)
        {
            InitializeComponent();
            this.Frame= frame;
            animazioneFadeIn(frame);
            btn_avanti.Opacity = 1;
        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btn_avanti_Click(object sender, RoutedEventArgs e)
        {
            animazioneFadeOut(grid_contactless);
            grid_contactless.Visibility = Visibility.Hidden;
            animazioneFadeIn(tastierino);
            tastierino.Visibility = Visibility.Visible;
        }

        private void animazioneFadeIn(UIElement element)
        {
            btn_avanti.Opacity = 0;

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 1;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleAnimation.AutoReverse = false;

            element.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
        }

        private void animazioneFadeOut(UIElement element)
        {
            btn_avanti.Opacity = 1;

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 1;
            doubleAnimation.To = 0;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleAnimation.AutoReverse = false;

            element.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_pin.Text.Length < 4)
            {
                txt_pin.Text += '*';
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txt_pin.Text.Length > 0)
            {
                txt_pin.Text = txt_pin.Text.Remove(txt_pin.Text.Length - 1, 1);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txt_pin.Text.Length == 4)
            {
                Frame.Content = new ConfermaPagamento(Frame,"Visa");
            }
        }
    }
}
