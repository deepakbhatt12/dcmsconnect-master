﻿// <auto-generated />
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

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static partial class MVC_BoxManager
{
    static readonly BoxManagerClass s_BoxManager = new BoxManagerClass();
    public static BoxManagerClass BoxManager { get { return s_BoxManager; } }
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class BoxManagerClass
    {
        public readonly string Name = "BoxManager";
        public DcmsMobile.BoxManager.Areas.BoxManager.Controllers.AutoCompleteController AutoComplete = new DcmsMobile.BoxManager.Areas.BoxManager.Controllers.T4MVC_AutoCompleteController();
        public DcmsMobile.BoxManager.Areas.BoxManager.Controllers.HomeController Home = new DcmsMobile.BoxManager.Areas.BoxManager.Controllers.T4MVC_HomeController();
        public DcmsMobile.BoxManager.Areas.BoxManager.Controllers.MovePalletController MovePallet = new DcmsMobile.BoxManager.Areas.BoxManager.Controllers.T4MVC_MovePalletController();
        public DcmsMobile.BoxManager.Areas.BoxManager.Controllers.VasConfigurationController VasConfiguration = new DcmsMobile.BoxManager.Areas.BoxManager.Controllers.T4MVC_VasConfigurationController();
        public T4MVC.BoxManager.SharedController Shared = new T4MVC.BoxManager.SharedController();
    }
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ActionResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_JsonResult : System.Web.Mvc.JsonResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_JsonResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace Links_BoxManager
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Scripts {
        private const string URLPATH = "~/Scripts";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        public static readonly string jquery_2_1_1_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-2.1.1.min.js") ? Url("jquery-2.1.1.min.js") : Url("jquery-2.1.1.js");
        public static readonly string jquery_ui_1_10_0_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-ui-1.10.0.min.js") ? Url("jquery-ui-1.10.0.min.js") : Url("jquery-ui-1.10.0.js");
        public static readonly string jquery_validate_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate.min.js") ? Url("jquery.validate.min.js") : Url("jquery.validate.js");
        public static readonly string jquery_validate_unobtrusive_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate.unobtrusive.min.js") ? Url("jquery.validate.unobtrusive.min.js") : Url("jquery.validate.unobtrusive.js");
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Content {
        private const string URLPATH = "~/Content";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class themes {
            private const string URLPATH = "~/Content/themes";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Start1_10_0 {
                private const string URLPATH = "~/Content/themes/Start1.10.0";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string jquery_ui_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-ui.min.css") ? Url("jquery-ui.min.css") : Url("jquery-ui.css");
            }
        
        }
    
    }


    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Areas {
        private const string URLPATH = "~/Areas";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class BoxManager {
            private const string URLPATH = "~/Areas/BoxManager";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Scripts {
                private const string URLPATH = "~/Areas/BoxManager/Scripts";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string BoxEditor_all_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/BoxEditor-all.min.js") ? Url("BoxEditor-all.min.js") : Url("BoxEditor-all.js");
                public static readonly string BoxEditor_all_min_js_map = Url("BoxEditor-all.min.js.map");
                public static readonly string Index_mobile_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Index-mobile.min.js") ? Url("Index-mobile.min.js") : Url("Index-mobile.js");
                public static readonly string Index_mobile_min_js_map = Url("Index-mobile.min.js.map");
                public static readonly string VasConfiguration_Edit_all_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/VasConfiguration-Edit-all.min.js") ? Url("VasConfiguration-Edit-all.min.js") : Url("VasConfiguration-Edit-all.js");
                public static readonly string VasConfiguration_Edit_all_min_js_map = Url("VasConfiguration-Edit-all.min.js.map");
                public static readonly string VasConfiguration_Index_all_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/VasConfiguration-Index-all.min.js") ? Url("VasConfiguration-Index-all.min.js") : Url("VasConfiguration-Index-all.js");
                public static readonly string VasConfiguration_Index_all_min_js_map = Url("VasConfiguration-Index-all.min.js.map");
            }
        
        }
    }

    public static partial class Areas {
    
        public static partial class BoxManager {
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Content {
                private const string URLPATH = "~/Areas/BoxManager/Content";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string BoxManager_all_css_bundle = Url("BoxManager-all.css.bundle");
                public static readonly string BoxManager_all_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/BoxManager-all.min.css") ? Url("BoxManager-all.min.css") : Url("BoxManager-all.css");
                public static readonly string BoxManager_all_mobile_css_bundle = Url("BoxManager-all.mobile.css.bundle");
                public static readonly string BoxManager_all_mobile_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/BoxManager-all.mobile.min.css") ? Url("BoxManager-all.mobile.min.css") : Url("BoxManager-all.mobile.css");
                public static readonly string BoxManager_partial_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/BoxManager.partial.min.css") ? Url("BoxManager.partial.min.css") : Url("BoxManager.partial.css");
                public static readonly string BoxManager_pptx = Url("BoxManager.pptx");
                public static readonly string BoxManagerMobile_partial_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/BoxManagerMobile.partial.min.css") ? Url("BoxManagerMobile.partial.min.css") : Url("BoxManagerMobile.partial.css");
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class images {
                    private const string URLPATH = "~/Areas/BoxManager/Content/images";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string a1_gif = Url("a1.gif");
                }
            
                public static readonly string SiteMobile_partial_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/SiteMobile.partial.min.css") ? Url("SiteMobile.partial.min.css") : Url("SiteMobile.partial.css");
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class Sounds {
                    private const string URLPATH = "~/Areas/BoxManager/Content/Sounds";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string Error_wav = Url("Error.wav");
                    public static readonly string sucess_wav = Url("sucess.wav");
                    public static readonly string warning_wav = Url("warning.wav");
                }
            
                public static readonly string standardized_partial_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/standardized.partial.min.css") ? Url("standardized.partial.min.css") : Url("standardized.partial.css");
                public static readonly string VAS_pptx = Url("VAS.pptx");
            }
        
        }
    }
    
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Bundles
    {
        public static partial class Scripts 
        {
            public static class Assets
            {
                public const string jquery_2_1_1_js = "~/Scripts/jquery-2.1.1.js"; 
                public const string jquery_ui_1_10_0_js = "~/Scripts/jquery-ui-1.10.0.js"; 
                public const string jquery_validate_js = "~/Scripts/jquery.validate.js"; 
                public const string jquery_validate_unobtrusive_js = "~/Scripts/jquery.validate.unobtrusive.js"; 
            }
        }
        public static partial class Content 
        {
            public static partial class themes 
            {
                public static partial class Start1_10_0 
                {
                    public static class Assets
                    {
                        public const string jquery_ui_css = "~/Content/themes/Start1.10.0/jquery-ui.css";
                    }
                }
                public static class Assets
                {
                }
            }
            public static class Assets
            {
            }
        }
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    private static string ProcessVirtualPathDefault(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    public static Func<string, string> TimestampString = System.Web.Mvc.T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}





#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114

