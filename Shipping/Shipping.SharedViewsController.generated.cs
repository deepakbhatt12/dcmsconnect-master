// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace T4MVC.Shipping
{
    public class SharedViewsController
    {

        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _layoutBootstrap = "_layoutBootstrap";
                public readonly string _layoutShipping = "_layoutShipping";
                public readonly string _layoutShipping2 = "_layoutShipping2";
                public readonly string _soundPartial = "_soundPartial";
                public readonly string GuidedTruckLoading = "GuidedTruckLoading";
                public readonly string Shipping_all_css = "Shipping-all.css";
                public readonly string Shipping_mobile_all_css = "Shipping-mobile-all.css";
            }
            public readonly string _layoutBootstrap = "~/Areas/Shipping/SharedViews/_layoutBootstrap.cshtml";
            public readonly string _layoutShipping = "~/Areas/Shipping/SharedViews/_layoutShipping.cshtml";
            public readonly string _layoutShipping2 = "~/Areas/Shipping/SharedViews/_layoutShipping2.cshtml";
            public readonly string _soundPartial = "~/Areas/Shipping/SharedViews/_soundPartial.cshtml";
            public readonly string GuidedTruckLoading = "~/Areas/Shipping/SharedViews/GuidedTruckLoading.ppt";
            public readonly string Shipping_all_css = "~/Areas/Shipping/SharedViews/Shipping-all.css.bundle";
            public readonly string Shipping_mobile_all_css = "~/Areas/Shipping/SharedViews/Shipping-mobile-all.css.bundle";
            static readonly _SoundsClass s_Sounds = new _SoundsClass();
            public _SoundsClass Sounds { get { return s_Sounds; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _SoundsClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string Error = "Error";
                    public readonly string success = "success";
                    public readonly string warning = "warning";
                }
                public readonly string Error = "~/Areas/Shipping/SharedViews/Sounds/Error.wav";
                public readonly string success = "~/Areas/Shipping/SharedViews/Sounds/success.wav";
                public readonly string warning = "~/Areas/Shipping/SharedViews/Sounds/warning.wav";
            }
        }
    }

}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114