﻿using DcmsMobile.PickWaves.ViewModels;
using EclipseLibrary.Mvc.Controllers;
using System.Web.Mvc;

namespace DcmsMobile.PickWaves.Helpers
{
    [RouteArea("PickWaves")]
    public abstract class PickWavesControllerBase : EclipseController
    {
        protected const string ROLE_WAVE_MANAGER = "DCMS8_SELECTPO";
        protected const string ROLE_EXPEDITE_BOXES = "DCMS8_CREATEBPP";

        /// <summary>
        /// Ask the derived controller which role is needed to perform managerial functions. All data modifying operations should be treated as managerial functions.
        /// </summary>
        protected abstract string ManagerRoleName
        {
            get;
        }
        /// <summary>
        /// Set Editable properties on the base view model
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var vr = filterContext.Result as ViewResultBase;            
            if (vr != null)
            {
                var model = vr.Model as ViewModelBase;
                if (model != null)
                {
                    model.UserIsManager = AuthorizeExAttribute.IsSuperUser(HttpContext) || HttpContext.User.IsInRole(ManagerRoleName);
                    model.ManagerRoleName = ManagerRoleName;
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
