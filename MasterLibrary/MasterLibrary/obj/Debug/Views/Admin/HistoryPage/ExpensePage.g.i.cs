﻿#pragma checksum "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "ACFFF956C1853D1AF808BB41818C167042CF7280D6C48643525182D77F7F1965"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MasterLibrary.Views.Admin.HistoryPage;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
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


namespace MasterLibrary.Views.Admin.HistoryPage {
    
    
    /// <summary>
    /// ExpensePage
    /// </summary>
    public partial class ExpensePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MasterLibrary.Views.Admin.HistoryPage.ExpensePage ExpensePageML;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilterBox;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox filtercbb;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbbMonth;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbbYear;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvExpesne;
        
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
            System.Uri resourceLocater = new System.Uri("/MasterLibrary;component/views/admin/historypage/expensepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml"
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
            this.ExpensePageML = ((MasterLibrary.Views.Admin.HistoryPage.ExpensePage)(target));
            return;
            case 2:
            this.FilterBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 57 "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml"
            this.FilterBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.filtercbb = ((System.Windows.Controls.ComboBox)(target));
            
            #line 68 "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml"
            this.filtercbb.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbbMonth = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.cbbYear = ((System.Windows.Controls.ComboBox)(target));
            
            #line 116 "..\..\..\..\..\Views\Admin\HistoryPage\ExpensePage.xaml"
            this.cbbYear.Loaded += new System.Windows.RoutedEventHandler(this.cbbYear_Loaded);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lvExpesne = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

