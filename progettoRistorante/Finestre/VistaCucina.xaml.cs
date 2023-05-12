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
using System.Windows.Shapes;
using CCStatusOrder;

namespace progettoRistorante.Finestre
{
    /// <summary>
    /// Logica di interazione per VistaCucina.xaml
    /// </summary>
    public partial class VistaCucina : Window
    {
        private static List<fornelloVista> fornelli = new List<fornelloVista>();
        public VistaCucina()
        {
            InitializeComponent();
            fornelloVista fornello;
            int nuovonumero = Properties.Settings.Default.NumeroFornelli;
            for (int i = 0; i < nuovonumero; i++)
            {
                fornello = new fornelloVista();
                fornelli.Add(fornello);
                stack_fornelli.Children.Add(fornello);
            }

        }

        private void aggiungiFornello(object sender, RoutedEventArgs e)
        {
            fornelloVista fornello = new fornelloVista();
            fornelli.Add(fornello);
            stack_fornelli.Children.Add(fornello);
            Properties.Settings.Default.NumeroFornelli++ ;
            Properties.Settings.Default.Save();
        }
        private void rimuoviFornello(object sender, RoutedEventArgs e)
        {
            fornelli.RemoveAt(fornelli.Count - 1);
            stack_fornelli.Children.RemoveAt(stack_fornelli.Children.Count-1);
            Properties.Settings.Default.NumeroFornelli--;
            Properties.Settings.Default.Save();
        }

        public static void preparaPiatto()
        {
            foreach(fornelloVista fornello in fornelli)
            {
                if (fornello.status == 0)
                {
                    Piatto piatto = GestioneOrdini.prossimoPiatto();
                    fornello.cambiaDesc(piatto.desc);

                    foreach(PiattoMenu piattoMenu in MainWindow.menu)
                    {
                        if (piattoMenu.desc.Equals(piatto.desc))
                        {
                            fornello.setPorzioni(piattoMenu.porzioniPerCottura);
                            fornello.setTempoCottura(piattoMenu.tempoDiCottura);
                        }
                    }
                    fornello.inPreparazione();
                }
            }
        }

    }
}
