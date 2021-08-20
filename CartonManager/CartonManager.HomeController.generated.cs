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
namespace DcmsMobile.CartonManager.Areas.CartonManager.Controllers
{
    public partial class HomeController
    {
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
        public virtual System.Web.Mvc.ActionResult UpdateCartonOrPallet()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCartonOrPallet);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult HandleDestinationPallet()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.HandleDestinationPallet);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult HandleDestinationAreaForMobile()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.HandleDestinationAreaForMobile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult HandleDestinationBuildingForMobile()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.HandleDestinationBuildingForMobile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult HandleDestinationPalletForMobile()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.HandleDestinationPalletForMobile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult CartonEditor()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CartonEditor);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RemovePieces()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RemovePieces);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController Actions { get { return MVC_CartonManager.CartonManager.Home; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "CartonManager";
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
            public readonly string ToggleEmulation = "ToggleEmulation";
            public readonly string Index = "Index";
            public readonly string AdvancedUi = "AdvancedUi";
            public readonly string PalletizeUi = "PalletizeUi";
            public readonly string MarkReworkCompleteUi = "MarkReworkCompleteUi";
            public readonly string AbandonReworkUi = "AbandonReworkUi";
            public readonly string PalletizeMobile = "PalletizeMobile";
            public readonly string UpdateCartonOrPallet = "UpdateCartonOrPallet";
            public readonly string HandleDestinationPallet = "HandleDestinationPallet";
            public readonly string HandleDestinationAreaForMobile = "HandleDestinationAreaForMobile";
            public readonly string HandleDestinationBuildingForMobile = "HandleDestinationBuildingForMobile";
            public readonly string HandleDestinationPalletForMobile = "HandleDestinationPalletForMobile";
            public readonly string CartonEditorIndex = "CartonEditorIndex";
            public readonly string Edit = "Edit";
            public readonly string CartonEditor = "CartonEditor";
            public readonly string RemovePieces = "RemovePieces";
            public readonly string Tutorial = "Tutorial";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string ToggleEmulation = "ToggleEmulation";
            public const string Index = "Index";
            public const string AdvancedUi = "AdvancedUi";
            public const string PalletizeUi = "PalletizeUi";
            public const string MarkReworkCompleteUi = "MarkReworkCompleteUi";
            public const string AbandonReworkUi = "AbandonReworkUi";
            public const string PalletizeMobile = "PalletizeMobile";
            public const string UpdateCartonOrPallet = "UpdateCartonOrPallet";
            public const string HandleDestinationPallet = "HandleDestinationPallet";
            public const string HandleDestinationAreaForMobile = "HandleDestinationAreaForMobile";
            public const string HandleDestinationBuildingForMobile = "HandleDestinationBuildingForMobile";
            public const string HandleDestinationPalletForMobile = "HandleDestinationPalletForMobile";
            public const string CartonEditorIndex = "CartonEditorIndex";
            public const string Edit = "Edit";
            public const string CartonEditor = "CartonEditor";
            public const string RemovePieces = "RemovePieces";
            public const string Tutorial = "Tutorial";
        }


        static readonly ActionParamsClass_UpdateCartonOrPallet s_params_UpdateCartonOrPallet = new ActionParamsClass_UpdateCartonOrPallet();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateCartonOrPallet UpdateCartonOrPalletParams { get { return s_params_UpdateCartonOrPallet; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateCartonOrPallet
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_HandleDestinationPallet s_params_HandleDestinationPallet = new ActionParamsClass_HandleDestinationPallet();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_HandleDestinationPallet HandleDestinationPalletParams { get { return s_params_HandleDestinationPallet; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_HandleDestinationPallet
        {
            public readonly string palletId = "palletId";
        }
        static readonly ActionParamsClass_HandleDestinationAreaForMobile s_params_HandleDestinationAreaForMobile = new ActionParamsClass_HandleDestinationAreaForMobile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_HandleDestinationAreaForMobile HandleDestinationAreaForMobileParams { get { return s_params_HandleDestinationAreaForMobile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_HandleDestinationAreaForMobile
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_HandleDestinationBuildingForMobile s_params_HandleDestinationBuildingForMobile = new ActionParamsClass_HandleDestinationBuildingForMobile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_HandleDestinationBuildingForMobile HandleDestinationBuildingForMobileParams { get { return s_params_HandleDestinationBuildingForMobile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_HandleDestinationBuildingForMobile
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_HandleDestinationPalletForMobile s_params_HandleDestinationPalletForMobile = new ActionParamsClass_HandleDestinationPalletForMobile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_HandleDestinationPalletForMobile HandleDestinationPalletForMobileParams { get { return s_params_HandleDestinationPalletForMobile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_HandleDestinationPalletForMobile
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_CartonEditor s_params_CartonEditor = new ActionParamsClass_CartonEditor();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CartonEditor CartonEditorParams { get { return s_params_CartonEditor; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CartonEditor
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_RemovePieces s_params_RemovePieces = new ActionParamsClass_RemovePieces();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_RemovePieces RemovePiecesParams { get { return s_params_RemovePieces; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_RemovePieces
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
                public readonly string AbandonRework = "AbandonRework";
                public readonly string AbandonRework_mobile = "AbandonRework.mobile";
                public readonly string AdvancedUi = "AdvancedUi";
                public readonly string CartonEditor = "CartonEditor";
                public readonly string DestinationArea_mobile = "DestinationArea.mobile";
                public readonly string DestinationBuilding_mobile = "DestinationBuilding.mobile";
                public readonly string DestinationPallet_mobile = "DestinationPallet.mobile";
                public readonly string Index = "Index";
                public readonly string Index_mobile = "Index.mobile";
                public readonly string MarkReworkComplete = "MarkReworkComplete";
                public readonly string MarkReworkComplete_mobile = "MarkReworkComplete.mobile";
                public readonly string Palletize_mobile = "Palletize.mobile";
                public readonly string PalletizeUi = "PalletizeUi";
                public readonly string Tutorial = "Tutorial";
            }
            public readonly string AbandonRework = "~/Areas/CartonManager/Views/Home/AbandonRework.cshtml";
            public readonly string AbandonRework_mobile = "~/Areas/CartonManager/Views/Home/AbandonRework.mobile.cshtml";
            public readonly string AdvancedUi = "~/Areas/CartonManager/Views/Home/AdvancedUi.cshtml";
            public readonly string CartonEditor = "~/Areas/CartonManager/Views/Home/CartonEditor.cshtml";
            public readonly string DestinationArea_mobile = "~/Areas/CartonManager/Views/Home/DestinationArea.mobile.cshtml";
            public readonly string DestinationBuilding_mobile = "~/Areas/CartonManager/Views/Home/DestinationBuilding.mobile.cshtml";
            public readonly string DestinationPallet_mobile = "~/Areas/CartonManager/Views/Home/DestinationPallet.mobile.cshtml";
            public readonly string Index = "~/Areas/CartonManager/Views/Home/Index.cshtml";
            public readonly string Index_mobile = "~/Areas/CartonManager/Views/Home/Index.mobile.cshtml";
            public readonly string MarkReworkComplete = "~/Areas/CartonManager/Views/Home/MarkReworkComplete.cshtml";
            public readonly string MarkReworkComplete_mobile = "~/Areas/CartonManager/Views/Home/MarkReworkComplete.mobile.cshtml";
            public readonly string Palletize_mobile = "~/Areas/CartonManager/Views/Home/Palletize.mobile.cshtml";
            public readonly string PalletizeUi = "~/Areas/CartonManager/Views/Home/PalletizeUi.cshtml";
            public readonly string Tutorial = "~/Areas/CartonManager/Views/Home/Tutorial.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_HomeController : DcmsMobile.CartonManager.Areas.CartonManager.Controllers.HomeController
    {
        public T4MVC_HomeController() : base(Dummy.Instance) { }

        [NonAction]
        partial void ToggleEmulationOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ToggleEmulation()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ToggleEmulation);
            ToggleEmulationOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AdvancedUiOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult AdvancedUi()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AdvancedUi);
            AdvancedUiOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PalletizeUiOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult PalletizeUi()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PalletizeUi);
            PalletizeUiOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void MarkReworkCompleteUiOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult MarkReworkCompleteUi()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MarkReworkCompleteUi);
            MarkReworkCompleteUiOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AbandonReworkUiOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult AbandonReworkUi()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AbandonReworkUi);
            AbandonReworkUiOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PalletizeMobileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult PalletizeMobile()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PalletizeMobile);
            PalletizeMobileOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void UpdateCartonOrPalletOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.CartonManager.ViewModels.ViewModelBase model);

        [NonAction]
        public override System.Web.Mvc.ActionResult UpdateCartonOrPallet(DcmsMobile.CartonManager.ViewModels.ViewModelBase model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCartonOrPallet);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            UpdateCartonOrPalletOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void HandleDestinationPalletOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string palletId);

        [NonAction]
        public override System.Web.Mvc.ActionResult HandleDestinationPallet(string palletId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.HandleDestinationPallet);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "palletId", palletId);
            HandleDestinationPalletOverride(callInfo, palletId);
            return callInfo;
        }

        [NonAction]
        partial void HandleDestinationAreaForMobileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.CartonManager.ViewModels.DestinationAreaForMobileViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult HandleDestinationAreaForMobile(DcmsMobile.CartonManager.ViewModels.DestinationAreaForMobileViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.HandleDestinationAreaForMobile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            HandleDestinationAreaForMobileOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void HandleDestinationBuildingForMobileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.CartonManager.ViewModels.DestinationBuildingForMobileViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult HandleDestinationBuildingForMobile(DcmsMobile.CartonManager.ViewModels.DestinationBuildingForMobileViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.HandleDestinationBuildingForMobile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            HandleDestinationBuildingForMobileOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void HandleDestinationPalletForMobileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.CartonManager.ViewModels.DestinationPalletForMobileViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult HandleDestinationPalletForMobile(DcmsMobile.CartonManager.ViewModels.DestinationPalletForMobileViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.HandleDestinationPalletForMobile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            HandleDestinationPalletForMobileOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void CartonEditorIndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult CartonEditorIndex()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CartonEditorIndex);
            CartonEditorIndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void CartonEditorOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.CartonManager.ViewModels.CartonEditorViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult CartonEditor(DcmsMobile.CartonManager.ViewModels.CartonEditorViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CartonEditor);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CartonEditorOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void RemovePiecesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, DcmsMobile.CartonManager.ViewModels.CartonEditorViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult RemovePieces(DcmsMobile.CartonManager.ViewModels.CartonEditorViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RemovePieces);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            RemovePiecesOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void TutorialOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Tutorial()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Tutorial);
            TutorialOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
