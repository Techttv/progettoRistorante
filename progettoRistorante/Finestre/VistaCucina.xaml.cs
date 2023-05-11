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
        public VistaCucina()
        {
            InitializeComponent();
            for(int i = 0; i < 6; i++)
            {
                fornelloVista fornelloVista = new fornelloVista();
                stack_fornelli.Children.Add(fornelloVista);
            }
        }
    }
}
