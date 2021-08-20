﻿

using EclipseLibrary.Mvc.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace DcmsMobile.Receiving.Areas.Receiving.Rad
{
    [RouteArea("Receiving")]
    [RoutePrefix("Rad")]
    public partial class RadController : EclipseController
    {
        private const string ROLE_RAD_EDITING = "SRC_RECEIVING_MGR";

        private SelectListItem Map(SewingPlant src)
        {
            return new SelectListItem
            {
                Text = src.SewingPlantCode + ": " + src.PlantName,
                Value = src.SewingPlantCode
                //GroupText = src.GroupingColumn + ":" + src.CountryName
            };
        }

        /// <summary>
        /// Required by T4MVC
        /// </summary>
        // ReSharper disable UnusedMember.Global
        public RadController()
        // ReSharper restore UnusedMember.Global
        {

        }

        private Lazy<RadService> _service;

        protected override void Initialize(RequestContext requestContext)
        {
            if (_service == null)
            {
                _service = new Lazy<RadService>(() => new RadService(requestContext));
            }
            base.Initialize(requestContext);
        }


        [Route("index")]
        public virtual ActionResult Index()
        {
            var model = new IndexViewModel();
            var sc = _service.Value.GetSpotCheckList();

            model.EnableEditing = AuthorizeExAttribute.IsSuperUser(HttpContext) || this.HttpContext.User.IsInRole(ROLE_RAD_EDITING);
            model.SpotCheckAreaList = _service.Value.GetSpotCheckAreas().Select(p => new SpotCheckAreaModel(p)).ToList();


            var query = from item in sc
                        group item by
                            item.SewingPlantId into g
                        let defstyle = g.Where(p => string.IsNullOrWhiteSpace(p.Style)).FirstOrDefault()
                        orderby g.Key
                        select new SewingPlantGroupModel
                        {
                            SewingPlantId = g.Key,
                            PlantName = g.Where(p => p.SewingPlantId == g.Key).First().PlantName,
                            SpotCheckPercent = defstyle == null ? null : defstyle.SpotCheckPercent,
                            CreatedBy = defstyle == null ? "" : defstyle.CreatedBy,
                            CreatedDate = defstyle == null ? null : defstyle.CreatedDate,
                            IsSpotCheckEnabled = defstyle == null ? true : (defstyle.IsSpotCheckEnable ?? true),
                            ModifiedBy = defstyle == null ? "" : defstyle.ModifiedBy,
                            ModifiedDate = defstyle == null ? null : defstyle.ModifiedDate,
                            Styles = (from item2 in g
                                      where !string.IsNullOrWhiteSpace(item2.Style)
                                      orderby item2.Style, item2.Color
                                      select new SpotCheckConfigurationModel(item2)).ToList()

                        };

            model.SewingPlants = query.ToList();

            return View(Views.Index, model);
        }

        /// <summary>
        /// Returns the partial view for adding spot check setting
        /// </summary>
        /// <returns></returns>
        [Route("addsptchk")]
        public virtual ActionResult AddSpotCheckPartial()
        {
            var plantlist = _service.Value.GetSewingPlants().Select(p => Map(p));
            var model = new AddSpotCheckViewModel
            {
                SewingPlantList = plantlist
            };
            return PartialView(Views._addSpotCheckPartial, model);
        }



        [HttpPost]
        [AuthorizeEx("Updating Receiving Configuration {0}", Roles = ROLE_RAD_EDITING)]
        [Route("update")]
        public virtual ActionResult AddUpdateSpotCheckSetting(ModifyAction action, string style, string color, string sewingPlantId,
            int? spotCheckPercent, bool enabled = false)
        {

            if (action == ModifyAction.Delete)
            {

                var rows = _service.Value.DeleteSpotCheckSetting(style, color, sewingPlantId);
                AddStatusMessage(string.Format("{0} Spot check setting has been deleted for Sewing Plant: {0}, Style: {1}, Color: {2} ", rows, string.IsNullOrEmpty(sewingPlantId) ? "All" : sewingPlantId, string.IsNullOrEmpty(style) ? "All" : style, string.IsNullOrEmpty(color) ? "All" : color));

            }
            else
            {
                var inserted = _service.Value.AddUpdateSpotCheckSetting(style, color, sewingPlantId, spotCheckPercent, enabled);
                if (inserted)
                {
                    AddStatusMessage(string.Format("Spot check setting has been added for Sewing Plant: {0}, Style: {1}, Color: {2} ", string.IsNullOrEmpty(sewingPlantId) ? "All" : sewingPlantId, string.IsNullOrEmpty(style) ? "All" : style, string.IsNullOrEmpty(color) ? "All" : color));

                }
                else
                {
                    AddStatusMessage(string.Format("Spot check setting has been modified for Sewing Plant: {0}, Style: {1}, Color: {2} ", string.IsNullOrEmpty(sewingPlantId) ? "All" : sewingPlantId, string.IsNullOrEmpty(style) ? "All" : style, string.IsNullOrEmpty(color) ? "All" : color));

                }
            }
            return RedirectToAction(MVC_Receiving.Receiving.Rad.Index());

        }


        /// <summary>
        /// Returning Json result for Style Autocomplete
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns> 
        [Route("styles")]
        public virtual JsonResult StyleAutocomplete(string term)
        {
            term = term ?? string.Empty;

            var tokens = term.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToList();

            string searchId;
            string searchDescription;

            switch (tokens.Count)
            {
                case 0:
                    // All styles
                    searchId = searchDescription = string.Empty;
                    break;

                case 1:
                    // Try to match term with either id or description
                    searchId = searchDescription = tokens[0];
                    break;

                case 2:
                    // Try to match first token with id and second with description
                    searchId = tokens[0];
                    searchDescription = tokens[1];
                    break;

                default:
                    // For now, ignore everything after the second :
                    searchId = tokens[0];
                    searchDescription = tokens[1];
                    break;


            }
            var data = _service.Value.GetStyles(searchId, searchDescription).Select(p => new
            {
                label = string.Format("{0}: {1}", p.Item1, p.Item2),
                value = p.Item1
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returning Json result for Color Autocomplete
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        [Route("colors")]
        public virtual JsonResult ColorAutocomplete(string term)
        {
            // Change null to empty string
            term = term ?? string.Empty;

            var tokens = term.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToList();

            string searchId;
            string searchDescription;

            switch (tokens.Count)
            {
                case 0:
                    // All Colors
                    searchId = searchDescription = string.Empty;
                    break;

                case 1:
                    // Try to match term with either id or description
                    searchId = searchDescription = tokens[0];
                    break;

                case 2:
                    // Try to match first token with id and second with description
                    searchId = tokens[0];
                    searchDescription = tokens[1];
                    break;

                default:
                    // For now, ignore everything after the second :
                    searchId = tokens[0];
                    searchDescription = tokens[1];
                    break;


            }

            var data = _service.Value.GetColors(searchId, searchDescription).Select(p => new
            {
                label = string.Format("{0}: {1}", p.Item1, p.Item2),
                value = p.Item1
            }); ;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}




//$Id$