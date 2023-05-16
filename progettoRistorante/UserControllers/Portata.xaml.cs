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
    /// Logica di interazione per Portata.xaml
    /// </summary>
    public partial class Portata : UserControl
    {
        public Portata()
        {
            InitializeComponent();
        }

        public void cambiaNome(string nome)
        {
            nome_piatto.Content = nome;
        }

        private void btn_aggiungi_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
