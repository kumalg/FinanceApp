﻿#pragma checksum "E:\Users\Kamil\Desktop\Programowanie Windows 10 Mobile\UWP\FinanseApp\Finanse\Szablony.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "780463FB992DA846C6FEC5A98EC09F77"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Finanse
{
    partial class Szablony : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.HamburgerTight = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 2:
                {
                    this.HamburgerWide = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 3:
                {
                    this.MyContentDialog = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                }
                break;
            case 4:
                {
                    this.checkBoxAgree = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.Button element5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 60 "..\..\..\Szablony.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element5).Click += this.ButtonShowContentDialog1_Click;
                    #line default
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.Button element6 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 63 "..\..\..\Szablony.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element6).Click += this.ButtonShowContentDialog3_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.FakeHamburgerButton = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
