﻿#pragma checksum "..\..\..\fornelloVista.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "58DC79E043FF7049F4888E81E00A9BF7CD20DA74"
//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

using CCStatusOrder;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CCStatusOrder {
    
    
    /// <summary>
    /// fornelloVista
    /// </summary>
    public partial class fornelloVista : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 73 "..\..\..\fornelloVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_skip;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\fornelloVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_desc;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\fornelloVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_porzioni;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\fornelloVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image icona_status;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\fornelloVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_status;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\fornelloVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_conferma;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\fornelloVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_elimina;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CCStatusOrder;component/fornellovista.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\fornelloVista.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btn_skip = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\..\fornelloVista.xaml"
            this.btn_skip.Click += new System.Windows.RoutedEventHandler(this.btn_skip_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lbl_desc = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.lbl_porzioni = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.icona_status = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.lbl_status = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.btn_conferma = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.btn_elimina = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

