﻿#pragma checksum "..\..\..\..\Pages\Warehouse\WindowCountProducReg.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A8FA98A8699E87204A81DED1359BEECFC4DEAB32F7F74BB5E4E04DEF8568613F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using PC_Service.Pages.Warehouse;
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


namespace PC_Service.Pages.Warehouse {
    
    
    /// <summary>
    /// WindowCountProducReg
    /// </summary>
    public partial class WindowCountProducReg : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Pages\Warehouse\WindowCountProducReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextProduct;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Pages\Warehouse\WindowCountProducReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbCount;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Pages\Warehouse\WindowCountProducReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbPrice;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Pages\Warehouse\WindowCountProducReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddProduct;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PC-Service;component/pages/warehouse/windowcountproducreg.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\Warehouse\WindowCountProducReg.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TextProduct = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.TbCount = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\..\..\Pages\Warehouse\WindowCountProducReg.xaml"
            this.TbCount.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbPrice_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TbPrice = ((System.Windows.Controls.TextBox)(target));
            
            #line 31 "..\..\..\..\Pages\Warehouse\WindowCountProducReg.xaml"
            this.TbPrice.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbPrice_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AddProduct = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\..\Pages\Warehouse\WindowCountProducReg.xaml"
            this.AddProduct.Click += new System.Windows.RoutedEventHandler(this.AddProduct_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

