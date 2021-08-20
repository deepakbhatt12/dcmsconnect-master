﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DcmsMobile.PickWaves.Repository
{
    /// <summary>
    /// Contaqins very basic properties of a bucket. All these properties are available in the bucket table
    /// </summary>
    internal class BucketBase
    {
        [Key]
        public int BucketId { get; set; }

        /// <summary>
        /// Name of the bucket
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// Comment of the bucket
        /// </summary>
        public string BucketComment { get; set; }

        public bool IsFrozen { get; set; }

        /// <summary>
        /// Priority Id of Bucket
        /// </summary>
        public int PriorityId { get; set; }

        public int? PitchLimit { get; set; }

        public bool RequireBoxExpediting { get; set; }

        [Obsolete]
        public bool QuickPitch { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string CreatedBy { get; set; }
    }

    /// <summary>
    /// ManageWaves and BoxPickPallet
    /// IEquatable defines whether all important properties of two buckets are same. Used while updating bucket to ensure that old properties have not changed
    /// </summary>
    internal class BucketWithActivities:BucketBase
    {
        /// <summary>
        /// Earliest cancel date of the POs within the bucket
        /// </summary>
        public DateTime? MinDcCancelDate { get; set; }

        /// <summary>
        /// Latest cancel date of the POs within the bucket
        /// </summary>
        public DateTime? MaxDcCancelDate { get; set; }

        /// <summary>
        /// Customer for whom the bucket was created
        /// </summary>
        public string MaxCustomerId { get; set; }

        /// <summary>
        /// Number of pickslips in the bucket
        /// </summary>
        internal int CountPickslips { get; set; }

        /// <summary>
        /// Number of purchase orders in the bucket.
        /// </summary>
        public int CountPurchaseOrder { get; set; }

        private BucketActivityCollection _activities;

        /// <summary>
        /// Make sure that this never returns null
        /// </summary>
        public BucketActivityCollection Activities
        {
            get
            {
                return _activities ?? (_activities = new BucketActivityCollection());
            }
            set
            {
                _activities = value;
            }
        }

        #region pieces
        /// <summary>
        /// Total pieces ordered
        /// </summary>
        public int OrderedPieces { get; set; }


        #endregion

        #region Sku Assigned

        public int CountAssignedSku { get; set; }

        public int CountTotalSku { get; set; }

        #endregion

    }

}


/*
    $Id$ 
    $Revision$
    $URL$
    $Header$
    $Author$
    $Date$
*/
