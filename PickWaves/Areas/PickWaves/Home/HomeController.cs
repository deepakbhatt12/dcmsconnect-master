﻿
using DcmsMobile.PickWaves.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace DcmsMobile.PickWaves.Areas.PickWaves.Home
{

    //[RoutePrefix(HomeController.NameConst)]
    public partial class HomeController : PickWavesControllerBase
    {

        #region Intialization

        public HomeController()
        {

        }

        private HomeService _service;

        protected override void Initialize(RequestContext ctx)
        {
            base.Initialize(ctx);
            var clientInfo = string.IsNullOrEmpty(ctx.HttpContext.Request.UserHostName) ?
                             ctx.HttpContext.Request.UserHostAddress : ctx.HttpContext.Request.UserHostName;
            if (_service == null)
            {
                _service = new HomeService(HttpContext.Trace,
                    HttpContext.User.IsInRole(ROLE_WAVE_MANAGER) ? HttpContext.User.Identity.Name : string.Empty, clientInfo);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_service != null)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        [Route(Name = DcmsLibrary.Mvc.PublicRoutes.DcmsConnect_ManagePickWave)]
        public virtual ActionResult Index()
        {
            
            var customerList = _service.GetImportedOrderSummary(SearchTextType.Unknown, null);
            var model = new IndexViewModel
            {
                ImportedOrders = customerList.Select(p => new CustomerListModel
                {
                    CustomerId = p.CustomerId,
                    CustomerName = p.CustonerName,
                    IsCustomerActive = p.IsActiveCustomer,
                    PickslipCount = p.PickslipCount,
                    ImportDateRange = new DateRange
                    {
                        From = p.MinPickslipImportDate,
                        To = p.MaxPickslipImportDate
                    },
                    InternationalFlag = p.InternationalFlag
                }).ToList(),
                RecentBuckets = _service.GetRecentCreatedBucket(20).Select(p => new IndexBucketModel(p)).ToList(),
                ExpediteBuckets = _service.GetBucketToExpedite(20).Select(p => new IndexBucketModel(p)).ToList()

            };
            return View(Views.Index, model);
        }
        /// <summary>
        /// Showing list of bucket summary for all customer.
        /// </summary>
        /// <returns></returns>
        [Route("customers")]
        public virtual ActionResult Customers()
        {
            return ShowCustomerPage(SearchTextType.Unknown, null);
        }

        /// <summary>
        /// Showing list of bucket summary for passed customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [Route("customer")]
        public virtual ActionResult Customer(string customerId)
        {
            return ShowCustomerPage(SearchTextType.CustomerId, customerId);
        }

        /// <summary>
        /// Search text.
        /// </summary>
        /// <param name="searchText">Should be BucketId,CustomerId,UserName</param>
        /// <returns></returns>
        [Route(HomeController.ActionNameConstants.Search, Name = DcmsLibrary.Mvc.PublicRoutes.DcmsConnect_ManagePickWave1)]
        public virtual ActionResult Search(string id)
        {
            var search = SearchTextType.Unknown;
            if (!string.IsNullOrEmpty(id))
            {
                id = id.Trim().ToUpper();
                search = _service.ParseSearchText(id);
            }

            switch (search)
            {
                // When search text is unknown.
                case SearchTextType.Unknown:
                    //if (!string.IsNullOrEmpty(id))
                    //{
                    //    AddStatusMessage(string.Format("Search text {0} was not understood and is being ignored", id));
                    //}
                    //if (Request.UrlReferrer != null)
                    //{
                    //    return Redirect(Request.UrlReferrer.PathAndQuery);
                    //}
                    var route = Url.RouteCollection[DcmsLibrary.Mvc.PublicRoutes.DcmsConnect_Search1];
                    if (route == null)
                    {
                        // Inquiry not installed
                        AddStatusMessage(string.Format("Search text {0} was not understood and is being ignored", id));
                        if (Request.UrlReferrer != null)
                        {
                            return Redirect(Request.UrlReferrer.PathAndQuery);
                        }
                    }
                    else
                    {
                        var url = Url.RouteUrl(DcmsLibrary.Mvc.PublicRoutes.DcmsConnect_Search1, new
                        {
                            id = id
                        });
                        return Redirect(url);
                    }
                    break;

                // When search text is bucket id.
                case SearchTextType.BucketId:
                    return RedirectToAction(MVC_PickWaves.PickWaves.ManageWaves.WavePickslips(int.Parse(id)));

                // When search text is customer id.
                case SearchTextType.CustomerId:
                    break;

                //When search text is UserName.
                case SearchTextType.UserName:
                    //AddStatusMessage(string.Format("Filter applied for User: {0}", searchText));
                    break;

                default:
#if DEBUG
                    throw new NotImplementedException();
#else
                    break;
#endif
            }
            return ShowCustomerPage(search, id);
        }

        /// <summary>
        /// Showing UI based on passed search Text
        /// </summary>
        /// <param name="search"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        private ActionResult ShowCustomerPage(SearchTextType search, string searchText)
        {
            var bucketSummary = _service.GetBucketSummary(search, searchText);

            var query = from bucket in Map(bucketSummary)
                        group bucket by bucket.BucketStatus into g
                        orderby g.Key
                        select new
                        {
                            Status = g.Key,
                            // Inactive customers are displayed last
                            Buckets = (from item in g
                                       orderby item.DcCancelDateRange.From,
                                       item.IsCustomerActive ? 0 : 1
                                       select item).ToArray()
                        };

            var model = new CustomerViewModel
            {
                IsCustomerFilterApplied = search == SearchTextType.CustomerId,
                IsUserNameFilterApplied = search == SearchTextType.UserName
            };
            if (model.IsUserNameFilterApplied)
            {
                model.SearchUserName = searchText;
            }
            if (model.IsCustomerFilterApplied)
            {
                model.SearchCustomerId = searchText;
            }
            foreach (var item in query)
            {
                model.BucketsByStatus.Add(item.Status, item.Buckets);
            }
            model.ImportedOrders = (from item in _service.GetImportedOrderSummary(search, searchText)
                                    orderby item.MinDcCancelDate, item.DollarsOrdered descending
                                    select new ImportedOrderSummaryModel
                                    {
                                        CustomerId = item.CustomerId,
                                        CustomerName = item.CustonerName,
                                        IsCustomerActive = item.IsActiveCustomer,
                                        PickslipCount = item.PickslipCount,
                                        PiecesOrdered = item.PiecesOrdered,
                                        DollarsOrdered = (double)item.DollarsOrdered,
                                        DcCancelDateRange = new DateRange
                                        {
                                            From = item.MinDcCancelDate,
                                            To = item.MaxDcCancelDate
                                        },
                                        ImportDateRange = new DateRange
                                        {
                                            From = item.MinPickslipImportDate,
                                            To = item.MaxPickslipImportDate
                                        },
                                        InternationalFlag = item.InternationalFlag
                                    }).ToArray();
            return View(Views.Customer, model);
        }

        /// <summary>
        /// </summary>
        ///<param name="bucketSummary">
        ///</param>
        /// <returns>
        /// Return bucket summery of passed customer
        /// </returns>
        private IEnumerable<CustomerBucketStateModel> Map(IEnumerable<BucketSummary> bucketSummary)
        {
            return from item in bucketSummary
                   orderby item.MinDcCancelDate, item.OrderedPieces descending
                   group item by new
                   {
                       BucketStatus = item.BucketState,
                       item.CustomerId
                   } into g
                   select new CustomerBucketStateModel
                                {
                                    BucketStatus = g.Key.BucketStatus,
                                    BucketCount = g.Sum(p => p.BucketCount),
                                    CustomerId = g.Key.CustomerId,
                                    CustomerName = g.First().CustomerName,
                                    IsCustomerActive = g.First().IsActiveCustomer,
                                    PickedPieces = g.Sum(p => p.CurrentPieces),
                                    OrderedPieces = g.Sum(p => p.OrderedPieces),
                                    ExpectedPieces = g.Sum(p => p.ExpectedPieces),
                                    MaxPriorityId = g.Sum(p => p.MaxPriorityId),
                                    DcCancelDateRange = new DateRange
                                    {
                                        To = g.Select(p => p.MaxDcCancelDate).FirstOrDefault(),
                                        From = g.Select(p => p.MinDcCancelDate).FirstOrDefault(),
                                    }
                                };
        }

        protected override string ManagerRoleName
        {
            get { return ROLE_WAVE_MANAGER; }
        }
    }
}