using Notification.Wpf;
using progettoRistorante.Classes;
using progettoRistorante.Finestre;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace progettoRistorante.UserControllers
{
    /// <summary>
    /// Interaction logic for fornello.xaml
    /// </summary>
    public partial class fornelloVista : UserControl
    {
        DispatcherTimer timer, timeElasped;
        public int tavRef;
        public int tempoCottura;
        string currentTime;
        TimeSpan inizio;
        Stopwatch stopWatch;
        private int secondi = 0;
        public int status = 0;

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
        public void setTavolo(int tavolo)
        {
            tavRef = tavolo;
            lbl_tavolo.Content = "Tavolo n°" + tavRef;
        }



        public void Disponibile()
        {
            this.Resources["RectangleColorPreparazione"] = this.Resources["RectangleColorDisponibile"];
            icona_status.Source = new BitmapImage(new Uri("/icon/Disponibile_icon.png", UriKind.Relative));
            lbl_desc.Content = "";
            status = 0;
            lbl_tavolo.Content = "";
            lbl_status.Content = "Disponibile";
            btn_conferma.IsEnabled = false;
            btn_elimina.IsEnabled = false;
            btn_skip.IsEnabled = false;
            MainWindow.ricarica();
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
            lbl_status.Content = "In corso";
            status = 1;
            btn_elimina.IsEnabled = true;
            btn_skip.IsEnabled = true;
            Tavolo tavolo = MainWindow.tavoli.ElementAt(tavRef - 1);
            foreach (Piatto piatto in tavolo.ordine)
            {
                if (piatto.desc.Equals((string)lbl_desc.Content))
                {
                    piatto.inPreparazione();
                }
            }
            MainWindow.ricarica();
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
            lbl_status.Content = "Finito";

            var notificationManager = new NotificationManager();
            notificationManager.Show("Piatto pronto!", lbl_desc.Content + " ha finito di cucinare. Clicca qui per aprire la finestra", NotificationType.Success, "", new TimeSpan(0, 0, 10), onClick: () => this.Focus());
            status = 2;
            btn_conferma.IsEnabled = true;
            btn_elimina.IsEnabled = true;
            stopWatch.Stop();
            timeElasped.Stop();
            timer.Stop();

            Tavolo tavolo = MainWindow.tavoli.ElementAt(tavRef - 1);
            foreach (Piatto piatto in tavolo.ordine)
            {
                if (piatto.desc.Equals((string)lbl_desc.Content))
                {
                    piatto.pronto();
                }
            }

            MainWindow.ricarica();
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

            timer.Stop();
            foreach (Piatto piatto in MainWindow.tavoli[tavRef - 1].ordine.ToList())
            {
                if (piatto.desc.Equals((string)lbl_desc.Content))
                {
                    MainWindow.tavoli[tavRef - 1].rimuoviPiatto(piatto, "");
                }
            }
            Disponibile();
            VistaCucina.preparaPiatto();
            MainWindow.ricarica();
        }

        void iniziaConteggio()
        {
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
