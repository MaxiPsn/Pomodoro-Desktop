﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D84F45E738102D780FE3ACBF3D60EF281935383F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Pomodoro;
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


namespace Pomodoro {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Horas;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Minutos;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Segundos;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Estado;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TiempoProductivomTextBox;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PreviewLblTiempoProd;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TiempoDescansomTextBox;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PreviewLblTiempoDesc;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RbAuto;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RbManual;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Pommodoro;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Horas = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.Minutos = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.Segundos = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.Estado = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.TiempoProductivomTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 72 "..\..\..\MainWindow.xaml"
            this.TiempoProductivomTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBoxPreviewInput);
            
            #line default
            #line hidden
            
            #line 72 "..\..\..\MainWindow.xaml"
            this.TiempoProductivomTextBox.KeyUp += new System.Windows.Input.KeyEventHandler(this.TextBoxTiempo_KeyUp);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PreviewLblTiempoProd = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.TiempoDescansomTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 87 "..\..\..\MainWindow.xaml"
            this.TiempoDescansomTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBoxPreviewInput);
            
            #line default
            #line hidden
            
            #line 88 "..\..\..\MainWindow.xaml"
            this.TiempoDescansomTextBox.KeyUp += new System.Windows.Input.KeyEventHandler(this.TextBoxTiempo_KeyUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.PreviewLblTiempoDesc = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.RbAuto = ((System.Windows.Controls.RadioButton)(target));
            
            #line 104 "..\..\..\MainWindow.xaml"
            this.RbAuto.Click += new System.Windows.RoutedEventHandler(this.RadioButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.RbManual = ((System.Windows.Controls.RadioButton)(target));
            
            #line 105 "..\..\..\MainWindow.xaml"
            this.RbManual.Click += new System.Windows.RoutedEventHandler(this.RadioButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 110 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Comenzar_click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

