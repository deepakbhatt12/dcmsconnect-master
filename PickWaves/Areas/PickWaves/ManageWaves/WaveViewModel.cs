﻿using DcmsMobile.PickWaves.Helpers;
using DcmsMobile.PickWaves.ViewModels;
using System;

namespace DcmsMobile.PickWaves.Areas.PickWaves.ManageWaves
{
    [Obsolete]
    public class WaveViewModel : ViewModelBase
    {
        public BucketModel Bucket { get; set; }

        public static string BucketSummaryReportUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DcmsLiveBaseUrl"] + "Reports/Category_140/R140_02.aspx";
            }
        }
     
    }



}