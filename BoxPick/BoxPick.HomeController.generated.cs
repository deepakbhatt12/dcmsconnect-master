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
namespace DcmsMobile.BoxPick.Areas.BoxPick.Controllers
{
    public partial class HomeController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected HomeController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AcceptBuilding()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptBuilding);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AcceptPallet()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptPallet);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AcceptCarton()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptCarton);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AcceptUccInCarton()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptUccInCarton);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AcceptPalletInCarton()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptPalletInCarton);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AcceptUcc()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptUcc);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AcceptCartonInUcc()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptCartonInUcc);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AcceptPalletInUcc()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptPalletInUcc);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController Actions { get { return MVC_BoxPick.BoxPick.Home; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "BoxPick";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Home";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Home";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string AcceptBuilding = "Index";
            public readonly string AcceptPallet = "Pallet";
            public readonly string AcceptCarton = "Carton";
            public readonly string AcceptUccInCarton = "Carton";
            public readonly string AcceptPalletInCarton = "Carton";
            public readonly string AcceptUcc = "Ucc";
            public readonly string AcceptCartonInUcc = "Ucc";
            public readonly string AcceptPalletInUcc = "Ucc";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string AcceptBuilding = "Index";
            public const string AcceptPallet = "Pallet";
            public const string AcceptCarton = "Carton";
            public const string AcceptUccInCarton = "Carton";
            public const string AcceptPalletInCarton = "Carton";
            public const string AcceptUcc = "Ucc";
            public const string AcceptCartonInUcc = "Ucc";
            public const string AcceptPalletInUcc = "Ucc";
        }


        static readonly ActionParamsClass_AcceptBuilding s_params_AcceptBuilding = new ActionParamsClass_AcceptBuilding();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AcceptBuilding AcceptBuildingParams { get { return s_params_AcceptBuilding; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AcceptBuilding
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_AcceptPallet s_params_AcceptPallet = new ActionParamsClass_AcceptPallet();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AcceptPallet AcceptPalletParams { get { return s_params_AcceptPallet; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AcceptPallet
        {
            public readonly string mm = "mm";
        }
        static readonly ActionParamsClass_AcceptCarton s_params_AcceptCarton = new ActionParamsClass_AcceptCarton();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AcceptCarton AcceptCartonParams { get { return s_params_AcceptCarton; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AcceptCarton
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_AcceptUccInCarton s_params_AcceptUccInCarton = new ActionParamsClass_AcceptUccInCarton();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AcceptUccInCarton AcceptUccInCartonParams { get { return s_params_AcceptUccInCarton; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AcceptUccInCarton
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_AcceptPalletInCarton s_params_AcceptPalletInCarton = new ActionParamsClass_AcceptPalletInCarton();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AcceptPalletInCarton AcceptPalletInCartonParams { get { return s_params_AcceptPalletInCarton; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AcceptPalletInCarton
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_AcceptUcc s_params_AcceptUcc = new ActionParamsClass_AcceptUcc();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AcceptUcc AcceptUccParams { get { return s_params_AcceptUcc; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AcceptUcc
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_AcceptCartonInUcc s_params_AcceptCartonInUcc = new ActionParamsClass_AcceptCartonInUcc();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AcceptCartonInUcc AcceptCartonInUccParams { get { return s_params_AcceptCartonInUcc; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AcceptCartonInUcc
        {
            public readonly string uvm = "uvm";
        }
        static readonly ActionParamsClass_AcceptPalletInUcc s_params_AcceptPalletInUcc = new ActionParamsClass_AcceptPalletInUcc();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AcceptPalletInUcc AcceptPalletInUccParams { get { return s_params_AcceptPalletInUcc; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AcceptPalletInUcc
        {
            public readonly string model = "model";
        }
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
                public readonly string Building = "Building";
                public readonly string Carton = "Carton";
                public readonly string Pallet = "Pallet";
                public readonly string Ucc = "Ucc";
            }
            public readonly string Building = "~/Areas/BoxPick/Views/Home/Building.cshtml";
            public readonly string Carton = "~/Areas/BoxPick/Views/Home/Carton.cshtml";
            public readonly string Pallet = "~/Areas/BoxPick/Views/Home/Pallet.cshtml";
            public readonly string Ucc = "~/Areas/BoxPick/Views/Home/Ucc.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_HomeController : DcmsMobile.BoxPick.Areas.BoxPick.Controllers.HomeController
    {
        public T4MVC_HomeController() : base(Dummy.Instance) { }

        [NonAction]
        partial void AcceptBuildingOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.BoxPick.ViewModels.BuildingViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult AcceptBuilding(DcmsMobile.BoxPick.ViewModels.BuildingViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptBuilding);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AcceptBuildingOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void AcceptPalletOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.BoxPick.ViewModels.PalletViewModel mm);

        [NonAction]
        public override System.Web.Mvc.ActionResult AcceptPallet(DcmsMobile.BoxPick.ViewModels.PalletViewModel mm)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptPallet);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "mm", mm);
            AcceptPalletOverride(callInfo, mm);
            return callInfo;
        }

        [NonAction]
        partial void AcceptCartonOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.BoxPick.ViewModels.CartonViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult AcceptCarton(DcmsMobile.BoxPick.ViewModels.CartonViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptCarton);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AcceptCartonOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void AcceptUccInCartonOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.BoxPick.ViewModels.CartonViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult AcceptUccInCarton(DcmsMobile.BoxPick.ViewModels.CartonViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptUccInCarton);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AcceptUccInCartonOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void AcceptPalletInCartonOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.BoxPick.ViewModels.CartonViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult AcceptPalletInCarton(DcmsMobile.BoxPick.ViewModels.CartonViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptPalletInCarton);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AcceptPalletInCartonOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void AcceptUccOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.BoxPick.ViewModels.UccViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult AcceptUcc(DcmsMobile.BoxPick.ViewModels.UccViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptUcc);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AcceptUccOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void AcceptCartonInUccOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.BoxPick.ViewModels.UccViewModel uvm);

        [NonAction]
        public override System.Web.Mvc.ActionResult AcceptCartonInUcc(DcmsMobile.BoxPick.ViewModels.UccViewModel uvm)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptCartonInUcc);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "uvm", uvm);
            AcceptCartonInUccOverride(callInfo, uvm);
            return callInfo;
        }

        [NonAction]
        partial void AcceptPalletInUccOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.BoxPick.ViewModels.UccViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult AcceptPalletInUcc(DcmsMobile.BoxPick.ViewModels.UccViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AcceptPalletInUcc);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AcceptPalletInUccOverride(callInfo, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114