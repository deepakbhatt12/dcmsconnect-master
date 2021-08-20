﻿using DcmsMobile.PickWaves.Helpers;
using EclipseLibrary.Oracle;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Web;

namespace DcmsMobile.PickWaves.Repository
{
    internal abstract class PickWaveRepositoryBase : IDisposable
    {
        #region Intialization

        protected OracleDatastore _db;

        //protected const string MODULE_CODE = "PickWaveManager";

        protected PickWaveRepositoryBase(TraceContext ctx, string userName, string clientInfo)
        {
            var db = new OracleDatastore(ctx);
            var connectString = ConfigurationManager.ConnectionStrings["dcms8"].ConnectionString;
            db.CreateConnection(connectString, userName);

            db.ModuleName = "PickWaveManager";
            db.ClientInfo = clientInfo;
            _db = db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        #endregion

        public DbTransaction BeginTransaction()
        {
            return _db.BeginTransaction();
        }

        /// <summary>
        /// Returns information about a specific customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public string GetCustomerName(string customerId)
        {
            const string QUERY = @"
                                    SELECT CUST.NAME AS NAME
                                     FROM <proxy />MASTER_CUSTOMER CUST
                                    WHERE CUST.CUSTOMER_ID = :SEARCH
                                    ";
            var binder = SqlBinder.Create(row => row.GetString("NAME"));
            binder.Parameter("SEARCH", customerId);
            return _db.ExecuteSingle(QUERY, binder);
        }

        /// <summary>
        ///  This method gets Buckets information. For performance reasons, cancelled box info is retrieved only when bucketId is passed
        /// </summary>       
        /// <param name="customerId">This is optional parameter, need to see only customer specific buckets</param>
        /// <param name="state">Passed to get bucket with specific status.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>
        /// 30 Mar 2012 Sharad: All buckets of the passed status are displayed. Earlier we were showing only those buckets for which
        /// boxes needed to be created.
        /// </para>
        /// <para>
        /// 30 Aug 2012 Sharad: As a defensive check, NULL pick modes are treated as PITCHING
        /// </para>
        /// <para>
        /// 25 Feb 2013: If expected pieces are null, we presume that they are same as current pieces. This avoids the anomaly of the box containing more than expected pieces.
        /// </para>
        /// <para>
        /// 14 Mar 2013: Pick area is returned as NULL for ADRE and ADREPPWSS buckets
        /// </para>
        /// </remarks>
        /// Discuss with Sharad sir on 15-May-2014 : 
        /// Get bucket for customer is not showing cancelled box.
        public BucketWithActivities GetBucket(int bucketId)
        {
                      var QUERY = @"
           WITH Q1 AS
 -- Bucket header information
(SELECT BKT.BUCKET_ID AS BUCKET_ID,
         PS.VWH_ID,
         BKT.NAME AS NAME,
         BKT.DATE_CREATED AS DATE_CREATED,
         BKT.CREATED_BY AS CREATED_BY,
         BKT.PITCH_IA_ID AS PITCH_IA_ID,
         BKT.PITCH_LIMIT AS PITCH_LIMIT,
         IA.SHORT_NAME AS PITCH_AREA_SHORT_NAME,
         IA.SHORT_DESCRIPTION AS PITCH_AREA_DESCRIPTION,
         IA.WAREHOUSE_LOCATION_ID AS BUILDING_PITCH_FROM,
         SUM(PS.TOTAL_QUANTITY_ORDERED) OVER(PARTITION BY BKT.BUCKET_ID) AS ORDERED_PIECES,
         PO.DC_CANCEL_DATE AS MIN_DC_CANCEL_DATE,
         PO.DC_CANCEL_DATE AS MAX_DC_CANCEL_DATE,
         PS.PO_ID AS PO_COUNT,      
         PS.CUSTOMER_ID AS CUSTOMER_ID,      
         PS.PICKSLIP_ID AS PICKSLIP_ID,
         BKT.FREEZE AS FREEZE,
CASE WHEN BKT.PITCH_TYPE = 'QUICK' THEN 'Y' END AS QUICK_PITCH_FLAG,
         BKT.PULL_CARTON_AREA AS PULL_AREA_ID,
         TIA.SHORT_NAME AS PULL_AREA_SHORT_NAME,
         TIA.DESCRIPTION AS PULL_AREA_DESCRIPTION,
         TIA.WAREHOUSE_LOCATION_ID AS BUILDING_PULL_FROM,
         BKT.PRIORITY AS PRIORITY,
         BKT.PULL_TYPE AS PULL_TYPE,
         BKT.BUCKET_COMMENT AS BUCKET_COMMENT,
         IA.DEFAULT_REPREQ_IA_ID AS DEFAULT_REPREQ_IA_ID
    FROM <proxy />BUCKET BKT
   INNER JOIN <proxy />PS PS
      ON PS.BUCKET_ID = BKT.BUCKET_ID
    LEFT OUTER JOIN <proxy />PO PO
      ON PS.CUSTOMER_ID = PO.CUSTOMER_ID
     AND PS.PO_ID = PO.PO_ID
     AND PS.ITERATION = PO.ITERATION
    LEFT OUTER JOIN <proxy />TAB_INVENTORY_AREA TIA
      ON TIA.INVENTORY_STORAGE_AREA = BKT.PULL_CARTON_AREA
    LEFT OUTER JOIN <proxy />IA IA
      ON IA.IA_ID = BKT.PITCH_IA_ID
   WHERE
bkt.bucket_ID = :bucket_ID
        ),
TOTAL_ORDERED_PIECES AS
 (SELECT Q1.BUCKET_ID AS BUCKET_ID,
         MAX(Q1.VWH_ID) AS VWH_ID,
         MAX(Q1.NAME) AS NAME,
         MAX(Q1.DATE_CREATED) AS DATE_CREATED,
         MAX(Q1.CREATED_BY) AS CREATED_BY,
         MAX(Q1.PITCH_IA_ID) AS PITCH_IA_ID,
         MAX(Q1.PITCH_LIMIT) AS PITCH_LIMIT,
         MAX(Q1.PITCH_AREA_SHORT_NAME) AS PITCH_AREA_SHORT_NAME,
         MAX(Q1.PITCH_AREA_DESCRIPTION) AS PITCH_AREA_DESCRIPTION,
         MAX(Q1.BUILDING_PITCH_FROM) AS BUILDING_PITCH_FROM,
         MAX(Q1.ORDERED_PIECES) AS ORDERED_PIECES,
         MAX(Q1.MIN_DC_CANCEL_DATE) AS MIN_DC_CANCEL_DATE,
         MAX(Q1.MAX_DC_CANCEL_DATE) AS MAX_DC_CANCEL_DATE,
         COUNT(UNIQUE Q1.PO_COUNT) AS PO_COUNT,
         MAX(Q1.CUSTOMER_ID) AS CUSTOMER_ID,
         COUNT(UNIQUE Q1.PICKSLIP_ID) AS PICKSLIP_COUNT,
         MAX(Q1.FREEZE) AS FREEZE,
         MAX(Q1.QUICK_PITCH_FLAG) AS QUICK_PITCH_FLAG,
         MAX(Q1.PULL_AREA_ID) AS PULL_AREA_ID,
         MAX(Q1.PULL_AREA_SHORT_NAME) AS PULL_AREA_SHORT_NAME,
         MAX(Q1.PULL_AREA_DESCRIPTION) AS PULL_AREA_DESCRIPTION,
         MAX(Q1.BUILDING_PULL_FROM) AS BUILDING_PULL_FROM,
         MAX(Q1.PRIORITY) AS PRIORITY,
         MAX(Q1.PULL_TYPE) AS PULL_TYPE,
         MAX(Q1.BUCKET_COMMENT) AS BUCKET_COMMENT,
         MAX(Q1.DEFAULT_REPREQ_IA_ID) AS DEFAULT_REPREQ_IA_ID,
         COUNT(UNIQUE PD.SKU_ID) AS COUNT_TOTAL_SKU,
         COUNT(UNIQUE (SELECT MAX(ASSIGNED_UPC_CODE)
                  FROM <proxy />IALOC IL
                 WHERE IL.ASSIGNED_UPC_CODE = PD.UPC_CODE
                   AND IL.VWH_ID = Q1.VWH_ID
                   AND IL.IA_ID = Q1.PITCH_IA_ID)) AS COUNT_ASSIGNED_SKU
    FROM Q1
   INNER JOIN <proxy />PSDET PD
      ON PD.PICKSLIP_ID = Q1.PICKSLIP_ID
<if c='not($BUCKET_ID)'>
   WHERE PD.TRANSFER_DATE IS NULL
</if>
   GROUP BY Q1.BUCKET_ID),
TOTAL_PICKED_PIECES AS
 (SELECT PS.BUCKET_ID AS BUCKET_ID,

         SUM(CASE
               WHEN BOX.CARTON_ID IS NULL AND BOX.STOP_PROCESS_REASON = '$BOXCANCEL' THEN
                NVL(BD.EXPECTED_PIECES, BD.CURRENT_PIECES)
             END) AS CAN_EXP_PCS_PITCH,
         SUM(CASE
               WHEN BOX.CARTON_ID IS NULL AND BOX.STOP_PROCESS_REASON = '$BOXCANCEL' THEN
                BD.CURRENT_PIECES
             END) AS CAN_CUR_PCS_PITCH,
         SUM(CASE
               WHEN BOX.CARTON_ID IS NOT NULL AND BOX.STOP_PROCESS_REASON = '$BOXCANCEL' THEN
                NVL(BD.EXPECTED_PIECES, BD.CURRENT_PIECES)
             END) AS CAN_EXP_PCS_PULL,
         SUM(CASE
               WHEN BOX.CARTON_ID IS NOT NULL AND
                    BOX.STOP_PROCESS_REASON = '$BOXCANCEL' THEN
                BD.CURRENT_PIECES
             END) AS CAN_CUR_PCS_PULL,


              COUNT(UNIQUE CASE
                            WHEN BOX.CARTON_ID IS NULL AND BOX.STOP_PROCESS_REASON = '$BOXCANCEL' THEN
                            BOX.UCC128_ID END)                AS CAN_BOXES_PITCH,
  
              COUNT(UNIQUE CASE WHEN BOX.CARTON_ID IS NOT NULL AND BOX.STOP_PROCESS_REASON = '$BOXCANCEL' THEN
                            BOX.UCC128_ID END)                AS CAN_BOXES_PULL,

         SUM(CASE
               WHEN BOX.CARTON_ID IS NULL AND BOX.VERIFY_DATE IS NOT NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL')  THEN
                NVL(BD.EXPECTED_PIECES, BD.CURRENT_PIECES)
             END) AS VALIDATED_EXP_PCS_PITCH,
         SUM(CASE
               WHEN BOX.CARTON_ID IS NULL AND BOX.VERIFY_DATE IS NOT NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') THEN
                BD.CURRENT_PIECES
             END) AS VALIDATED_CUR_PCS_PITCH,
         SUM(CASE
               WHEN BOX.CARTON_ID IS NOT NULL AND BOX.VERIFY_DATE IS NOT NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') THEN
                NVL(BD.EXPECTED_PIECES, BD.CURRENT_PIECES)
             END) AS VALIDATED_EXP_PCS_PULL,
         SUM(CASE
               WHEN BOX.CARTON_ID IS NOT NULL AND BOX.VERIFY_DATE IS NOT NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') THEN
                BD.CURRENT_PIECES
             END) AS VALIDATED_CUR_PCS_PULL,
         SUM(CASE
               WHEN BOX.CARTON_ID IS NULL AND BOX.VERIFY_DATE IS NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') and box.ia_id is not null THEN
                NVL(BD.EXPECTED_PIECES, BD.CURRENT_PIECES)
             END) AS INPROGRESS_EXP_PCS_PITCH,
         SUM(CASE
               WHEN BOX.CARTON_ID IS NULL AND BOX.VERIFY_DATE IS NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') and box.ia_id is not null THEN
                BD.CURRENT_PIECES
             END) AS INPROGRESS_CUR_PCS_PITCH,
         SUM(CASE
               WHEN BOX.CARTON_ID IS NOT NULL AND BOX.VERIFY_DATE IS NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') and box.ia_id is not null THEN
                NVL(BD.EXPECTED_PIECES, BD.CURRENT_PIECES)
             END) AS INPROGRESS_EXP_PCS_PULL,
         SUM(CASE
               WHEN BOX.CARTON_ID IS NOT NULL AND BOX.VERIFY_DATE IS NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') and box.ia_id is not null THEN
                BD.CURRENT_PIECES
             END) AS INPROGRESS_CUR_PCS_PULL,

         SUM(CASE
               WHEN BOX.CARTON_ID IS NOT NULL  AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') and box.ia_id is null THEN
                BD.EXPECTED_PIECES
             END) AS NONPHYSICAL_EXP_PCS_PULL,

         SUM(CASE
               WHEN BOX.CARTON_ID IS NULL  AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') and box.ia_id is null THEN
                BD.EXPECTED_PIECES
             END) AS NONPHYSICAL_EXP_PCS_PITCH,

         COUNT(UNIQUE CASE
                 WHEN BOX.CARTON_ID IS NULL AND BOX.VERIFY_DATE IS NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') and box.ia_id is not null THEN
                  BOX.UCC128_ID
               END) AS INPROGRESS_BOXES_PITCH,
         COUNT(UNIQUE CASE
                 WHEN BOX.CARTON_ID IS NULL AND BOX.VERIFY_DATE IS NOT NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') THEN
                  BOX.UCC128_ID
               END) AS VALIDATED_BOXES_PITCH,

         COUNT(UNIQUE CASE
                 WHEN BOX.CARTON_ID IS NOT NULL AND BOX.VERIFY_DATE IS NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') and box.ia_id is not null THEN
                  BOX.UCC128_ID
               END) AS INPROGRESS_BOXES_PULL,
         COUNT(UNIQUE CASE
                 WHEN BOX.CARTON_ID IS NOT NULL AND BOX.VERIFY_DATE IS NOT NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') THEN
                  BOX.UCC128_ID
               END) AS VALIDATED_BOXES_PULL,

         COUNT(UNIQUE CASE
                 WHEN BOX.CARTON_ID IS NOT NULL AND BOX.VERIFY_DATE IS NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') and box.ia_id is null THEN
                  BOX.UCC128_ID
               END) AS NONPHYSICAL_BOXES_PULL,
         COUNT(UNIQUE CASE
                 WHEN BOX.CARTON_ID IS NULL AND (BOX.STOP_PROCESS_REASON IS NULL OR BOX.STOP_PROCESS_REASON != '$BOXCANCEL') AND BOX.IA_ID IS NULL THEN
                  BOX.UCC128_ID
               END) AS NONPHYSICAL_BOXES_PITCH,
         MAX(CASE
               WHEN BOX.CARTON_ID IS NULL THEN
                BOX.PITCHING_END_DATE
             END) AS MAX_PITCHING_END_DATE,
         MIN(CASE
               WHEN BOX.CARTON_ID IS NULL THEN
                BOX.PITCHING_END_DATE
             END) AS MIN_PITCHING_END_DATE,
         MAX(CASE
               WHEN BOX.CARTON_ID IS NOT NULL THEN
                BOX.PITCHING_END_DATE
             END) AS MAX_PULLING_END_DATE,
         MIN(CASE
               WHEN BOX.CARTON_ID IS NOT NULL THEN
                BOX.PITCHING_END_DATE
             END) AS MIN_PULLING_END_DATE
    FROM <proxy />PS PS
   INNER JOIN <proxy />BOX BOX
      ON PS.PICKSLIP_ID = BOX.PICKSLIP_ID
   INNER JOIN <proxy />BOXDET BD
      ON BOX.PICKSLIP_ID = BD.PICKSLIP_ID
     AND BOX.UCC128_ID = BD.UCC128_ID
   WHERE 
PS.bucket_id = :bucket_id

   GROUP BY PS.BUCKET_ID)
SELECT OP.BUCKET_ID               AS BUCKET_ID,
       PP.CAN_BOXES_PITCH       AS CAN_BOXES_PITCH,
       PP.CAN_BOXES_PULL         AS CAN_BOXES_PULL,
       PP.CAN_EXP_PCS_PITCH       AS CAN_EXP_PCS_PITCH,
       PP.CAN_CUR_PCS_PITCH       AS CAN_CUR_PCS_PITCH,
       PP.CAN_EXP_PCS_PULL        AS CAN_EXP_PCS_PULL,
       PP.CAN_CUR_PCS_PULL        AS CAN_CUR_PCS_PULL,
        OP.NAME                    AS NAME,
       OP.DATE_CREATED            AS DATE_CREATED,
       OP.CREATED_BY              AS CREATED_BY,
       OP.CUSTOMER_ID             AS CUSTOMER_ID,
       --OP.CUSTOMER_NAME           AS CUSTOMER_NAME,
       OP.PO_COUNT                AS PO_COUNT,
      -- OP.MIN_PO                  AS MIN_PO,
      -- OP.MAX_PO                  AS MAX_PO,
       OP.PICKSLIP_COUNT          AS PICKSLIP_COUNT,
       OP.ORDERED_PIECES          AS ORDERED_PIECES,
       PP.VALIDATED_EXP_PCS_PITCH      AS VALIDATED_EXP_PCS_PITCH,
       PP.VALIDATED_CUR_PCS_PITCH      AS VALIDATED_CUR_PCS_PITCH,
       PP.INPROGRESS_EXP_PCS_PITCH    AS INPROGRESS_EXP_PCS_PITCH,
       PP.INPROGRESS_CUR_PCS_PITCH    AS INPROGRESS_CUR_PCS_PITCH,
       PP.INPROGRESS_BOXES_PITCH  AS INPROGRESS_BOXES_PITCH,
       PP.VALIDATED_BOXES_PITCH   AS VALIDATED_BOXES_PITCH,
       PP.VALIDATED_EXP_PCS_PULL       AS VALIDATED_EXP_PCS_PULL,
       PP.VALIDATED_CUR_PCS_PULL       AS VALIDATED_CUR_PCS_PULL,
       PP.INPROGRESS_EXP_PCS_PULL     AS INPROGRESS_EXP_PCS_PULL,
       PP.INPROGRESS_CUR_PCS_PULL     AS INPROGRESS_CUR_PCS_PULL,
       PP.INPROGRESS_BOXES_PULL   AS INPROGRESS_BOXES_PULL,
       PP.VALIDATED_BOXES_PULL    AS VALIDATED_BOXES_PULL,
       PP.NONPHYSICAL_BOXES_PULL  AS NONPHYSICAL_BOXES_PULL,
       PP.NONPHYSICAL_BOXES_PITCH AS NONPHYSICAL_BOXES_PITCH,
PP.NONPHYSICAL_EXP_PCS_PULL AS NONPHYSICAL_EXP_PCS_PULL,
PP.NONPHYSICAL_EXP_PCS_PITCH AS NONPHYSICAL_EXP_PCS_PITCH,
       OP.MIN_DC_CANCEL_DATE      AS MIN_DC_CANCEL_DATE,
       OP.MAX_DC_CANCEL_DATE      AS MAX_DC_CANCEL_DATE,
       OP.FREEZE                  AS FREEZE,
       OP.QUICK_PITCH_FLAG        AS QUICK_PITCH_FLAG,
       OP.PULL_AREA_ID            AS PULL_AREA_ID,
       OP.PULL_AREA_SHORT_NAME    AS PULL_AREA_SHORT_NAME,
       OP.PULL_AREA_DESCRIPTION   AS PULL_AREA_DESCRIPTION,
       OP.BUILDING_PULL_FROM      AS BUILDING_PULL_FROM,
       OP.PRIORITY                AS PRIORITY,
       OP.PITCH_IA_ID             AS PITCH_IA_ID,
       OP.PITCH_AREA_SHORT_NAME   AS PITCH_AREA_SHORT_NAME,
       OP.PITCH_AREA_DESCRIPTION  AS PITCH_AREA_DESCRIPTION,
       OP.BUILDING_PITCH_FROM     AS BUILDING_PITCH_FROM,
       OP.DEFAULT_REPREQ_IA_ID    AS REPLENISH_AREA_ID,
       OP.PULL_TYPE            AS PULL_TYPE,
       OP.BUCKET_COMMENT          AS BUCKET_COMMENT,
       PP.MAX_PITCHING_END_DATE   AS MAX_PITCHING_END_DATE,
       PP.MIN_PITCHING_END_DATE   AS MIN_PITCHING_END_DATE,
       PP.MAX_PULLING_END_DATE    AS MAX_PULLING_END_DATE,
       PP.MIN_PULLING_END_DATE    AS MIN_PULLING_END_DATE,
       OP.PITCH_LIMIT             AS PITCH_LIMIT,
       OP.COUNT_TOTAL_SKU         AS COUNT_TOTAL_SKU,
       OP.COUNT_ASSIGNED_SKU      AS COUNT_ASSIGNED_SKU
  FROM TOTAL_ORDERED_PIECES OP
  LEFT OUTER JOIN TOTAL_PICKED_PIECES PP
    ON OP.BUCKET_ID = PP.BUCKET_ID";
            var binder = SqlBinder.Create(row =>
            {
                var bucket = new BucketWithActivities
                {
                    BucketId = row.GetInteger("BUCKET_ID").Value,
                    BucketComment = row.GetString("BUCKET_COMMENT"),
                    CreatedBy = row.GetString("CREATED_BY"),
                    CreationDate = row.GetDate("DATE_CREATED").Value,
                    BucketName = row.GetString("NAME"),
                    OrderedPieces = row.GetInteger("ORDERED_PIECES") ?? 0,
                    CountPurchaseOrder = row.GetInteger("PO_COUNT") ?? 0,
                    MaxCustomerId = row.GetString("CUSTOMER_ID"),
                    CountPickslips = row.GetInteger("PICKSLIP_COUNT").Value,
                    PriorityId = row.GetInteger("PRIORITY").Value,
                    MinDcCancelDate = row.GetDate("MIN_DC_CANCEL_DATE"),
                    MaxDcCancelDate = row.GetDate("MAX_DC_CANCEL_DATE"),
                    IsFrozen = row.GetString("FREEZE") == "Y",
                    RequireBoxExpediting = row.GetString("PULL_TYPE") == "EXP",
                    QuickPitch = row.GetString("QUICK_PITCH_FLAG") == "Y",
                    PitchLimit = row.GetInteger("PITCH_LIMIT"),
                    CountAssignedSku = row.GetInteger("COUNT_ASSIGNED_SKU") ?? 0,
                    CountTotalSku = row.GetInteger("COUNT_TOTAL_SKU") ?? 0
                };
                var activity = bucket.Activities[BucketActivityType.Pulling];
                activity.Area = new InventoryArea
                {
                    AreaId = row.GetString("PULL_AREA_ID"),
                    ShortName = row.GetString("PULL_AREA_SHORT_NAME"),
                    Description = row.GetString("PULL_AREA_DESCRIPTION"),
                    BuildingId = row.GetString("BUILDING_PULL_FROM")
                };
                activity.Stats[PiecesKind.Expected, BoxState.Completed] = row.GetInteger("VALIDATED_EXP_PCS_PULL");
                activity.Stats[PiecesKind.Current, BoxState.Completed] = row.GetInteger("VALIDATED_CUR_PCS_PULL");
                activity.Stats[PiecesKind.Expected, BoxState.InProgress] = row.GetInteger("INPROGRESS_EXP_PCS_PULL");
                activity.Stats[PiecesKind.Current, BoxState.InProgress] = row.GetInteger("INPROGRESS_CUR_PCS_PULL");
                activity.Stats[PiecesKind.Expected, BoxState.NotStarted] = row.GetInteger("NONPHYSICAL_EXP_PCS_PULL");

                activity.Stats[BoxState.Cancelled] = row.GetInteger("CAN_BOXES_PULL");
                activity.Stats[BoxState.InProgress] = row.GetInteger("INPROGRESS_BOXES_PULL");
                activity.Stats[BoxState.Completed] = row.GetInteger("VALIDATED_BOXES_PULL");
                activity.Stats[BoxState.NotStarted] = row.GetInteger("NONPHYSICAL_BOXES_PULL");

    
                activity.Stats[PiecesKind.Expected, BoxState.Cancelled] = row.GetInteger("CAN_EXP_PCS_PULL");
                activity.Stats[PiecesKind.Current, BoxState.Cancelled] = row.GetInteger("CAN_CUR_PCS_PULL");

                activity.MaxEndDate = row.GetDateTimeOffset("MAX_PULLING_END_DATE");
                activity.MinEndDate = row.GetDateTimeOffset("MIN_PULLING_END_DATE");

                activity = bucket.Activities[BucketActivityType.Pitching];
                activity.Area = new InventoryArea
                {
                    AreaId = row.GetString("PITCH_IA_ID"),
                    ShortName = row.GetString("PITCH_AREA_SHORT_NAME"),
                    Description = row.GetString("PITCH_AREA_DESCRIPTION"),
                    BuildingId = row.GetString("BUILDING_PITCH_FROM"),
                    ReplenishAreaId = row.GetString("REPLENISH_AREA_ID")
                };
                activity.Stats[PiecesKind.Expected, BoxState.Completed] = row.GetInteger("VALIDATED_EXP_PCS_PITCH");
                activity.Stats[PiecesKind.Current, BoxState.Completed] = row.GetInteger("VALIDATED_CUR_PCS_PITCH");
                activity.Stats[PiecesKind.Expected, BoxState.InProgress] = row.GetInteger("INPROGRESS_EXP_PCS_PITCH");
                activity.Stats[PiecesKind.Current, BoxState.InProgress] = row.GetInteger("INPROGRESS_CUR_PCS_PITCH");
                activity.Stats[PiecesKind.Expected, BoxState.NotStarted] = row.GetInteger("NONPHYSICAL_EXP_PCS_PITCH");
  

                // Count of unverified boxes
                activity.Stats[BoxState.Cancelled] = row.GetInteger("CAN_BOXES_PITCH");
                activity.Stats[BoxState.InProgress] = row.GetInteger("INPROGRESS_BOXES_PITCH");
                activity.Stats[BoxState.Completed] = row.GetInteger("VALIDATED_BOXES_PITCH");
                activity.Stats[BoxState.NotStarted] = row.GetInteger("NONPHYSICAL_BOXES_PITCH");

                activity.Stats[PiecesKind.Expected, BoxState.Cancelled] = row.GetInteger("CAN_EXP_PCS_PITCH");
                activity.Stats[PiecesKind.Current, BoxState.Cancelled] = row.GetInteger("CAN_CUR_PCS_PITCH");

                activity.MaxEndDate = row.GetDateTimeOffset("MAX_PITCHING_END_DATE");
                activity.MinEndDate = row.GetDateTimeOffset("MIN_PITCHING_END_DATE");
                return bucket;
            });

            binder.Parameter("FrozenState", ProgressStage.Frozen.ToString())
                  .Parameter("InProgressState", ProgressStage.InProgress.ToString())
                  .Parameter("CompletedState", ProgressStage.Completed.ToString())
                  .Parameter("bucket_ID", bucketId);
            return _db.ExecuteSingle(QUERY, binder);
        }

        /// <summary>
        /// Returns the highest priority bucket which has most boxes for expediting
        /// </summary>
        /// <returns>The best bucket id</returns>
   
        public IList<BucketBase> GetExpeditableBuckets(int maxRows)
        {
            const string QUERY = @"                    
                                    SELECT BUCKET.BUCKET_ID AS BUCKET_ID,
                                           max(BUCKET.NAME) AS BUCKET_NAME,
                                           max(BUCKET.BUCKET_COMMENT) AS  BUCKET_COMMENT,
                                            max(BUCKET.FREEZE) AS FREEZE,
                                            max(BUCKET.PRIORITY) AS PRIORITY ,
                                            max(BUCKET.PITCH_LIMIT) AS PITCH_LIMIT,
                                            NULL AS QUICK_PITCH_FLAG_O,  /* max(BUCKET.QUICK_PITCH_FLAG_O) Obsolete*/
                                           max( BUCKET.CREATED_BY) AS  CREATED_BY,
                                           max( BUCKET.DATE_CREATED) AS DATE_CREATED,   
                                          MAX(BUCKET.PULL_TYPE) AS PULL_TYPE                                       
                                      FROM <proxy />BUCKET BUCKET
                                     INNER JOIN <proxy />PS P
                                        ON P.BUCKET_ID = BUCKET.BUCKET_ID
                                     INNER JOIN <proxy />BOX B
                                        ON P.PICKSLIP_ID = B.PICKSLIP_ID
                                     WHERE B.IA_ID IS NULL
                                       --AND B.STOP_PROCESS_DATE IS NULL
                                       AND B.PALLET_ID IS NULL
                                       AND BUCKET.PULL_TYPE = 'EXP'
                                       AND P.TRANSFER_DATE IS NULL
                                       AND BUCKET.FREEZE IS NULL and b.carton_id is not null
                                     GROUP BY BUCKET.BUCKET_ID
                                     ORDER BY MAX(BUCKET.PRIORITY) DESC,
                                              COUNT(P.PICKSLIP_ID) DESC                 ";
            var binder = SqlBinder.Create(row =>
                new BucketBase
                {
                    BucketId = row.GetInteger("BUCKET_ID") ?? 0,
                    BucketName = row.GetString("BUCKET_NAME"),
                    BucketComment = row.GetString("BUCKET_COMMENT"),
                    IsFrozen = row.GetString("FREEZE") == "Y",
                    CreatedBy = row.GetString("CREATED_BY"),
                    CreationDate = row.GetDate("DATE_CREATED").Value,
                    PriorityId = row.GetInteger("PRIORITY").Value,
                    RequireBoxExpediting = row.GetString("PULL_TYPE") == "EXP",
                    QuickPitch = row.GetString("QUICK_PITCH_FLAG_O") == "Y",
                    PitchLimit = row.GetInteger("PITCH_LIMIT")
                }
                );
            return _db.ExecuteReader(QUERY, binder, maxRows);
        }

    }
}