﻿#pragma checksum "..\..\..\Pages\FinanceCategories.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D74DB3B77AEB2F5624126A0962341C8012F89643B8331B210B49C043486AAD49"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using PC_Service.Pages;
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


namespace PC_Service.Pages {
    
    
    /// <summary>
    /// FinanceCategories
    /// </summary>
    public partial class FinanceCategories : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 11 "..\..\..\Pages\FinanceCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridStatus;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Pages\FinanceCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddWarehouseClick;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Pages\FinanceCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGrid;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\Pages\FinanceCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Count;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\Pages\FinanceCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border FormContainer;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\Pages\FinanceCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button WatchtCases;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\Pages\FinanceCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbName;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\Pages\FinanceCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TbAddress;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\Pages\FinanceCategories.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtAdd;
        
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
            System.Uri resourceLocater = new System.Uri("/PC-Service;component/pages/financecategories.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\FinanceCategories.xaml"
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
            this.GridStatus = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.AddWarehouseClick = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Pages\FinanceCategories.xaml"
            this.AddWarehouseClick.Click += new System.Windows.RoutedEventHandler(this.AddWarehouseClickClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.Count = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.FormContainer = ((System.Windows.Controls.Border)(target));
            return;
            case 8:
            this.WatchtCases = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\Pages\FinanceCategories.xaml"
            this.WatchtCases.Click += new System.Windows.RoutedEventHandler(this.WatchtCases_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.TbName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.TbAddress = ((System.Windows.Controls.ComboBox)(target));
            
            #line 108 "..\..\..\Pages\FinanceCategories.xaml"
            this.TbAddress.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(this.TbAddress_TextChanged));
            
            #line default
            #line hidden
            return;
            case 11:
            this.BtAdd = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\..\Pages\FinanceCategories.xaml"
            this.BtAdd.Click += new System.Windows.RoutedEventHandler(this.BtAdd_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 4:
            
            #line 54 "..\..\..\Pages\FinanceCategories.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnEdit_Click);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 61 "..\..\..\Pages\FinanceCategories.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnDel_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

