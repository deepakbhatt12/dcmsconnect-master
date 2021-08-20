﻿
using DcmsMobile.REQ2.Areas.REQ2.SharedViews;
namespace DcmsMobile.REQ2.Areas.REQ2.Home
{
    internal class RequestSku
    {

        public RequestSku()
        {
            this.SourceSku = new Sku();
            this.TargetSku = new Sku();
        }
        public Sku SourceSku { get; set; }
        public Sku TargetSku { get; set; }
        public int RequestedPieces { get; set; }
        public int AssignedPieces { get; set; }
        public int AssignedCartons { get; set; }
    }
}

//$Id$