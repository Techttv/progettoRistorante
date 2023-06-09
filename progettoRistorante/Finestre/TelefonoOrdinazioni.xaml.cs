﻿using progettoRistorante.Finestre.TelefonoPagine;
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
using System.Windows.Shapes;
using PdfSharp.Fonts;
using progettoRistorante.Classes;
using System.ComponentModel;
using System.Diagnostics;

namespace progettoRistorante.Finestre
{
    /// <summary>
    /// Interaction logic for TelefonoOrdinazioni.xaml
    /// </summary>
    public partial class TelefonoOrdinazioni : Window
    {

        public TelefonoOrdinazioni()
        {
            
            InitializeComponent();
            Home.Content = new Home(Home) ;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = WindowState.Minimized;

        }
    }
}
