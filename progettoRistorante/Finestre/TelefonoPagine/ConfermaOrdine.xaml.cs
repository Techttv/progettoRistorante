using Notification.Wpf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace progettoRistorante.Finestre.TelefonoPagine
{
    /// <summary>
    /// Logica di interazione per ConfermaOrdine.xaml
    /// </summary>
    public partial class ConfermaOrdine : Page
    {
        Frame frame;
        public ConfermaOrdine(Frame frame, string tipo)
        {
            InitializeComponent();
            this.frame = frame;
            if (tipo == "provvisorio")
            {
                stampaScontrinoProvvisorio();
            }
            NuovoOrdine.tavolo.cambiaStatus(1);
            MainWindow.tavoli.RemoveAt(NuovoOrdine.tavolo.numeroTavolo - 1);
            MainWindow.tavoli.Insert(NuovoOrdine.tavolo.numeroTavolo - 1, NuovoOrdine.tavolo);
            NuovoOrdine.tavolo = new Tavolo();
            ModificaNuovoOrdine.tavolo = new Tavolo();
            MainWindow.ricarica();
        }

        private void stampaScontrinoProvvisorio()
        {
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("it-IT");
            

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            page.Width = 300;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            var alignment = XStringFormats.TopCenter;
            XFont font = new XFont("Gabriola", 25);

            gfx.DrawString("Ristorante Caricato's", font, XBrushes.Black, new XRect(10, 10, page.Width, page.Height), alignment);
            font = new XFont("Merchant Copy Doublesize", 10, XFontStyle.Italic);
            var headerfont = new XFont("Merchant Copy Doublesize", 16, XFontStyle.Bold);
            gfx.DrawString("Via Brodolini 14", font, XBrushes.Black, new XRect(5, 50, page.Width, page.Height), alignment);
            gfx.DrawString("Recanati MC", font, XBrushes.Black, new XRect(5, 70, page.Width, page.Height), alignment);
            gfx.DrawString("Tavolo "+ NuovoOrdine.tavolo.numeroTavolo, headerfont, XBrushes.Black, new XRect(5, 90, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawString("-------------------------------", font, XBrushes.Black, new XRect(5, 110, page.Width, page.Height), alignment);
            gfx.DrawString(DateTime.Now.ToString(), font, XBrushes.Black, new XRect(5, 130, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawString("-------------------------------", font, XBrushes.Black, new XRect(5, 150, page.Width, page.Height), alignment);
            gfx.DrawString("Descrizione", font, XBrushes.Black, new XRect(5, 165, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Costo", font, XBrushes.Black, new XRect(-5, 165, page.Width, page.Height), XStringFormats.TopRight);
            int y = 200;
            foreach (Piatto piatto in NuovoOrdine.tavolo.ordine)
            {
                gfx.DrawString(piatto.desc , font, XBrushes.Black, new XRect(5, y, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(piatto.prezzo.ToString("F",culture ), font, XBrushes.Black, new XRect(-5, y, page.Width, page.Height), XStringFormats.TopRight);
                y += 20;
            }
            y+= 10;
            gfx.DrawString("-------------------------------", font, XBrushes.Black, new XRect(5, y, page.Width, page.Height), alignment);
            gfx.DrawString("Totale provvisorio: " + NuovoOrdine.tavolo.getTotale().ToString("F", culture) + " euro", font, XBrushes.Black, new XRect(5, y + 20, page.Width, page.Height), alignment);
            
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path += "\\scontrini\\";
            Directory.CreateDirectory(path);
            string nomefile = "scontrino_provvisorio_"+DateTime.Now.Minute+DateTime.Now.Second+".pdf";
            path = path + nomefile;
            document.Save(path);

            var notificationManager = new NotificationManager();
            notificationManager.Show("Scontrino provvisorio", "Clicca qui per visualizzare lo scontrino", NotificationType.Information, onClick: () => OpenPdf(path));

        }


        private void OpenPdf(string path)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(path)
            {
                UseShellExecute = true
            };
            p.Start();

        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {

            frame.Content = new Home(frame);
            
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
