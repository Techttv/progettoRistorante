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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Notification.Wpf;

namespace progettoRistorante
{
    /// <summary>
    /// Interaction logic for fornello.xaml
    /// </summary>
    public partial class fornelloVista : UserControl
    {
        DispatcherTimer timer, timeElasped;
        private int id = 0;
        public int tempoCottura;
        string currentTime;
        TimeSpan inizio;
        Stopwatch stopWatch;
        private int secondi =0;
        public int status=0;
        private int porzioni = 0;

        public fornelloVista()
        {
            InitializeComponent();
        }

        public void cambiaDesc(string desc)
        {
            lbl_desc.Content = desc;
        }
        public void setTempoCottura(int tempocottura)
        {
            this.tempoCottura = tempocottura;
        }

        public void setPorzioni(int porzioni)
        {
            this.porzioni = porzioni;
        }

        public void Disponibile()
        {
            this.Resources["RectangleColorPreparazione"] = this.Resources["RectangleColorDisponibile"];
            icona_status.Source = new BitmapImage(new Uri("/icon/Disponibile_icon.png", UriKind.Relative));
            lbl_desc.Content = "";
            lbl_porzioni.Content = "";
            lbl_status.Content = "Disponibile";
        }

        public void inPreparazione()
        {
            iniziaConteggio();

            //Timer tempo cottura
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Finito);
            timer.Interval = new TimeSpan(0, tempoCottura, 0);
            timer.Start();

            this.Resources["RectangleColorPreparazione"] = this.Resources["RectangleColorInCorso"];
            icona_status.Source = new BitmapImage(new Uri("/icon/Preparazione_icon.png", UriKind.Relative));
            id = 1;
            lbl_status.Content = "In corso";
            status = 1;
            btn_elimina.IsEnabled = true;
            btn_skip.IsEnabled = true;
            lbl_porzioni.Content = porzioni + " porzioni";
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}",
                inizio.Subtract(ts).Minutes, inizio.Subtract(ts).Seconds);
                lbl_status.Content = currentTime;
            }
        }

        private void Finito(Object sender, EventArgs e)
        {
            stopWatch.Stop();
            this.Resources["RectangleColorPreparazione"] = this.Resources["RectangleColorPronto"];
            icona_status.Source = new BitmapImage(new Uri("/icon/DaFare_icon.png", UriKind.Relative));
            id = 2;
            lbl_status.Content = "Finito";

            var notificationManager = new NotificationManager();
            notificationManager.Show("Piatto pronto!", lbl_desc.Content+ " ha finito di cucinare. Clicca qui per aprire la finestra", NotificationType.Success,"" ,new TimeSpan(0, 1, 0),onClick:()=>this.Focus());
            status = 2;
            btn_conferma.IsEnabled = true;
            btn_elimina.IsEnabled = true;
            stopWatch.Stop();
            timeElasped.Stop();
            timer.Stop();
        }

        private void btn_skip_Click(object sender, RoutedEventArgs e)
        {
            btn_skip.IsEnabled = false;
            tempoCottura = 0;
            secondi = 5;
            iniziaConteggio();
            timer.Stop();
            timer = new DispatcherTimer();
            timer.Tick += Finito;
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        private void btn_conferma_Click(object sender, RoutedEventArgs e)
        {
            foreach(PiattoMenu piatto in MainWindow.menu)
            {
                if (piatto.desc.Equals(lbl_desc.Content))
                {
                    piatto.porzioniPronte += porzioni;
                }
            }
            Disponibile();
        }

        void iniziaConteggio() {
            //timer che cambia ogni secondo
            timeElasped = new DispatcherTimer();
            timeElasped.Tick += dt_Tick;
            timeElasped.Interval = new TimeSpan(0, 0, 1);
            timeElasped.Start();

            //cronometro
            stopWatch = new Stopwatch();
            stopWatch.Start();
            TimeSpan ts = stopWatch.Elapsed;

            //Salva inizio
            inizio = new TimeSpan(0, tempoCottura, secondi);
        }
    }
}
