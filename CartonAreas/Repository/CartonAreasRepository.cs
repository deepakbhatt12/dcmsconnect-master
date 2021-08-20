﻿using EclipseLibrary.Oracle;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Web;

namespace DcmsMobile.CartonAreas.Repository
{
    internal partial class CartonAreasRepository : IDisposable
    {
        #region Intialization

        private readonly OracleDatastore _db;

        public OracleDatastore Db
        {
            get
            {
                return _db;
            }
        }

        /// <summary>
        /// Constructor of class used to create the connection to database.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="moduleName"></param>
        /// <param name="clientInfo"></param>
        /// <param name="trace"></param>
        public CartonAreasRepository(string userName, string moduleName, string clientInfo, TraceContext trace)
        {
            Contract.Assert(ConfigurationManager.ConnectionStrings["dcms8"] != null);
            var store = new OracleDatastore(trace);
            store.CreateConnection(ConfigurationManager.ConnectionStrings["dcms8"].ConnectionString,
                userName);
            store.ModuleName = moduleName;
            store.ClientInfo = clientInfo;
            _db = store;
        }

        /// <summary>
        /// For use in unit tests
        /// </summary>
        /// <param name="db"></param>
        public CartonAreasRepository(OracleDatastore db)
        {
            _db = db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        #endregion

        internal IList<Building> GetBuildings(string buildingId)
        {
            const string QUERY = @"
                                    WITH LOCATION_COUNTS AS
                                     (SELECT TIA.WAREHOUSE_LOCATION_ID AS WAREHOUSE_LOCATION_ID,
                                             COUNT(MSL.LOCATION_ID) AS COUNT_LOCATIONS,
                                             COUNT(UNIQUE TIA.INVENTORY_STORAGE_AREA) AS COUNT_AREAS,
                                             COUNT(UNIQUE MSL.STORAGE_AREA) AS COUNT_NUMBERED_AREAS,
                                             COUNT(UNIQUE I.IA_ID) AS PICKING_AREA_COUNT
                                        FROM <proxy />TAB_INVENTORY_AREA TIA
                                        LEFT OUTER JOIN <proxy />MASTER_STORAGE_LOCATION MSL
                                          ON TIA.INVENTORY_STORAGE_AREA = MSL.STORAGE_AREA
                                        LEFT OUTER JOIN <proxy />IA I
                                           ON I.WAREHOUSE_LOCATION_ID = TIA.WAREHOUSE_LOCATION_ID
                                       WHERE TIA.WAREHOUSE_LOCATION_ID IS NOT NULL
                                       GROUP BY TIA.WAREHOUSE_LOCATION_ID)
                                    SELECT T.WAREHOUSE_LOCATION_ID  AS WAREHOUSE_LOCATION_ID,
                                           T.DESCRIPTION            AS DESCRIPTION,
                                           T.INSERT_DATE            AS INSERT_DATE,
                                           T.INSERTED_BY            AS INSERTED_BY,
                                           T.RECEIVING_PALLET_LIMIT AS RECEIVING_PALLET_LIMIT,
                                           T.ADDRESS_1              AS ADDRESS_1,
                                           T.ADDRESS_2              AS ADDRESS_2,
                                           T.ADDRESS_3              AS ADDRESS_3,
                                           T.ADDRESS_4              AS ADDRESS_4,
                                           T.CITY                   AS CITY,
                                           T.STATE                  AS STATE,
                                           T.ZIP_CODE               AS ZIP_CODE,
                                           T.COUNTRY_CODE           AS COUNTRY_CODE,
                                           LC.COUNT_AREAS           AS COUNT_AREAS,
                                           LC.COUNT_NUMBERED_AREAS  AS COUNT_NUMBERED_AREAS,
                                           LC.COUNT_LOCATIONS       AS COUNT_LOCATIONS,
                                           LC.PICKING_AREA_COUNT    AS PICKING_AREA_COUNT
                                      FROM <proxy />TAB_WAREHOUSE_LOCATION T
                                      LEFT OUTER JOIN LOCATION_COUNTS LC
                                        ON LC.WAREHOUSE_LOCATION_ID = T.WAREHOUSE_LOCATION_ID
                                      WHERE 1 = 1
                                        <if>AND T.WAREHOUSE_LOCATION_ID = :WAREHOUSE_LOCATION_ID</if>
                                ";
            var binder = SqlBinder.Create(row => new Building
            {
                BuildingId = row.GetString("warehouse_location_id"),
                Description = row.GetString("description"),
                InsertDate = row.GetDate("insert_date"),
                InsertedBy = row.GetString("inserted_by"),
                ReceivingPalletLimit = row.GetInteger("receiving_pallet_limit"),
                Address = new Address
                {
                    Address1 = row.GetString("address_1"),
                    Address2 = row.GetString("address_2"),
                    Address3 = row.GetString("address_3"),
                    Address4 = row.GetString("address_4"),
                    City = row.GetString("City"),
                    State = row.GetString("State"),
                    ZipCode = row.GetString("zip_code"),
                    CountryCode = row.GetString("country_code")
                },
                CountCartonAreas = row.GetInteger("count_areas"),
                CountPickingAreas = row.GetInteger("PICKING_AREA_COUNT"),
                CountNumberedAreas = row.GetInteger("count_numbered_areas"),
                CountLocations = row.GetInteger("count_locations")
            });
            binder.Parameter("WAREHOUSE_LOCATION_ID", buildingId);
            return _db.ExecuteReader(QUERY, binder);
        }

        /// <summary>
        /// This method gets the carton area info and number of locations in it.
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public IEnumerable<CartonArea> GetCartonAreas(string areaId, string buildingId)
        {
            const string QUERY = @"
                SELECT MAX(TIA.INVENTORY_STORAGE_AREA)       AS INVENTORY_STORAGE_AREA,
                       TIA.SHORT_NAME                        AS SHORT_NAME,
                       MAX(TIA.DESCRIPTION)                  AS DESCRIPTION,
                       MAX(TIA.LOCATION_NUMBERING_FLAG)      AS LOCATION_NUMBERING_FLAG,                             
                       MAX(TIA.IS_PALLET_REQUIRED)           AS IS_PALLET_REQUIRED,
                       MAX(TIA.UNUSABLE_INVENTORY)           AS UNUSABLE_INVENTORY,
                       TIA.WAREHOUSE_LOCATION_ID             AS WAREHOUSE_LOCATION_ID,
                       COUNT(DISTINCT MSL.LOCATION_ID)       AS LOCATION_COUNT,
<if c='$AREA_ID'>
       count(distinct case
               when ctn.location_id is null then
                msl.location_id
             end) as empty_locations,
       count(distinct case
               when ctn.location_id is not null then
                msl.location_id
             end) as nonempty_locations,
       count(distinct case
               when msl.assigned_sku_id is not null then
                msl.location_id
             end) as assigned_locations,
       count(distinct case
               when msl.assigned_sku_id is not null and ctn.location_id is null then
                msl.location_id
             end) as empty_assigned_locations,
       count(distinct case
               when msl.assigned_sku_id is not null and
                    ctn.location_id is not null then
                msl.location_id
             end) as nonempty_assigned_locations,
       count(distinct case
               when msl.assigned_sku_id is null and ctn.location_id is null then
                msl.location_id
             end) as empty_unassigned_locations,
       count(distinct case
               when msl.assigned_sku_id is null and ctn.location_id is not null then
                msl.location_id
             end) as nonempty_unassigned_locations,
       count(distinct case
               when msl.assigned_sku_id is null then
                msl.location_id
             end) as total_unassigned_locations
</if>
<else>
       CAST(NULL AS NUMBER) as empty_locations,
       CAST(NULL AS NUMBER) as nonempty_locations,
       CAST(NULL AS NUMBER) as assigned_locations,
       CAST(NULL AS NUMBER) as empty_assigned_locations,
       CAST(NULL AS NUMBER) as nonempty_assigned_locations,
       CAST(NULL AS NUMBER) as empty_unassigned_locations,
       CAST(NULL AS NUMBER) as nonempty_unassigned_locations,
       CAST(NULL AS NUMBER) as total_unassigned_locations
</else>
                  FROM <proxy />TAB_INVENTORY_AREA TIA
                 LEFT OUTER JOIN <proxy />MASTER_STORAGE_LOCATION MSL
                    ON MSL.STORAGE_AREA = TIA.INVENTORY_STORAGE_AREA
<if c='$AREA_ID'>
  left outer join <proxy />src_carton ctn
    on ctn.location_id = msl.location_id
   and ctn.carton_storage_area = msl.storage_area
</if>
                 WHERE TIA.STORES_WHAT = 'CTN'
               <if>AND TIA.INVENTORY_STORAGE_AREA = :AREA_ID</if>
              <if> AND TIA.WAREHOUSE_LOCATION_ID=:WAREHOUSE_LOCATION_ID</if>
                 GROUP BY TIA.WAREHOUSE_LOCATION_ID, tia.short_name
                 ORDER BY TIA.WAREHOUSE_LOCATION_ID, tia.short_name
        ";
            var binder = SqlBinder.Create(row => new CartonArea
            {
                AreaId = row.GetString("INVENTORY_STORAGE_AREA"),
                Description = row.GetString("DESCRIPTION"),
                LocationNumberingFlag = row.GetString("LOCATION_NUMBERING_FLAG") == "Y",
                IsPalletRequired = row.GetString("IS_PALLET_REQUIRED") == "Y",
                UnusableInventory = row.GetString("UNUSABLE_INVENTORY") == "Y",
                BuildingId = row.GetString("WAREHOUSE_LOCATION_ID"),
                TotalLocations = row.GetInteger("LOCATION_COUNT") ?? 0,
                ShortName = row.GetString("SHORT_NAME"),
                CountEmptyLocations = row.GetInteger("empty_locations"),
                CountNonemptyLocations = row.GetInteger("nonempty_locations"),
                CountAssignedLocations = row.GetInteger("assigned_locations"),
                CountEmptyUnassignedLocations = row.GetInteger("empty_unassigned_locations"),
                CountUnassignedLocations = row.GetInteger("total_unassigned_locations"),
                CountEmptyAssignedLocations = row.GetInteger("empty_assigned_locations"),
                CountNonemptyAssignedLocations = row.GetInteger("nonempty_assigned_locations"),
                CountNonemptyUnassignedLocations = row.GetInteger("nonempty_unassigned_locations")
            });
            binder.Parameter("AREA_ID", areaId).Parameter("WAREHOUSE_LOCATION_ID", buildingId);
            return _db.ExecuteReader(QUERY, binder);
        }


        /// <summary>
        /// This function gets the location details in a specified area,
        /// and also give all information of any one location for assignment.
        /// </summary>
        /// <param name="locationIdPattern">The pattern can include % as the wildcard</param>
        /// <returns></returns>
        /// <remarks>
        /// SS 28/12/2011: Pallet count was wrong. Changed COUNT(SC.PALLET_ID) to COUNT(DISTINCT SC.PALLET_ID)
        /// </remarks>
        public IList<Location> GetCartonAreaLocations(string areaId, int? assignedSkuId, string locationPattern, bool? assignedLocations, bool? emptyLocations, int maxRows)
        {
            const string QUERY = @"
                SELECT MSL.LOCATION_ID                AS LOCATION_ID,
                       MAX(MSL.ASSIGNED_MAX_CARTONS)  AS ASSIGNED_MAX_CARTONS,
                       MAX(MSL.Assigned_Vwh_Id)       AS ASSIGNED_VWH_ID,
                       COUNT(DISTINCT SC.PALLET_ID)   AS NUMBER_OF_PALLET,
                       COUNT(SC.CARTON_ID)            AS NUMBER_OF_CARTONS,
                       COUNT(DISTINCT(SC.VWH_ID))     AS CARTON_VWH_COUNT,
                       MAX(SC.VWH_ID)                 AS CARTON_VWH_ID,
                       SUM(SCD.QUANTITY)              AS TOTAL_PIECES,
                       COUNT(DISTINCT(SCD.SKU_ID))    AS CARTON_SKU_COUNT,
                       MAX(SCD.SKU_ID)                AS CARTON_SKU_ID,
                       MAX(MSKU.SKU_ID)               AS SKU_ID,
                       MAX(MSKU.STYLE)                AS STYLE_,
                       MAX(MSKU.COLOR)                AS COLOR_,
                       MAX(MSKU.DIMENSION)            AS DIMENSION_,
                       MAX(MSKU.SKU_SIZE)             AS SKU_SIZE_,
                       MAX(MSKU.UPC_CODE)             AS UPC_CODE_,
                       MAX(MSKU2.SKU_ID)              AS CTN_SKU_ID_,
                       MAX(MSKU2.STYLE)               AS CTN_STYLE_,
                       MAX(MSKU2.COLOR)               AS CTN_COLOR_,
                       MAX(MSKU2.DIMENSION)           AS CTN_DIMENSION_,
                       MAX(MSKU2.SKU_SIZE)            AS CTN_SKU_SIZE_,
                       MAX(MSKU2.UPC_CODE)            AS CTN_UPC_CODE_,
count(*) over() as count_total_locations                      
                  FROM <proxy/>MASTER_STORAGE_LOCATION MSL
                  LEFT OUTER JOIN <proxy/>SRC_CARTON SC
                    ON SC.LOCATION_ID = MSL.LOCATION_ID
                   AND SC.CARTON_STORAGE_AREA = MSL.STORAGE_AREA
                  LEFT OUTER JOIN <proxy/>SRC_CARTON_DETAIL SCD
                    ON SCD.CARTON_ID = SC.CARTON_ID
                  LEFT OUTER JOIN <proxy/>MASTER_SKU MSKU
                    ON MSKU.SKU_ID = MSL.ASSIGNED_SKU_ID
                LEFT OUTER JOIN <proxy/>MASTER_SKU MSKU2
                    ON MSKU2.SKU_ID = SCD.SKU_ID
                 WHERE 1 = 1
               <if>AND MSL.STORAGE_AREA = :AREA_ID</if>
               <if>AND MSL.LOCATION_ID = :LOCATION_ID</if>
               <if>AND MSL.LOCATION_ID LIKE :SEARCHLOCATION</if>
               <if>AND MSL.ASSIGNED_SKU_ID = :SKU_ID</if>
               <if c=""$EMPTY_LOCATION_FLAG = 'true' "">
                   AND NOT EXISTS
                    (SELECT 1 FROM <proxy/>SRC_CARTON SRC WHERE SRC.LOCATION_ID = MSL.LOCATION_ID 
                      <if>AND SRC.LOCATION_ID = :LOCATION_ID</if>  )
               </if>
               <if c=""$EMPTY_LOCATION_FLAG = 'false' "">
                   AND MSL.LOCATION_ID IN
                    (SELECT LOCATION_ID FROM <proxy/>SRC_CARTON WHERE CARTON_STORAGE_AREA = :AREA_ID)
               </if>
               <if c=""$ASSIGNED_FLAG ='true' "">
                  AND MSL.ASSIGNED_SKU_ID IS NOT NULL
               </if>
               <if c=""$ASSIGNED_FLAG ='false' "">
                AND MSL.ASSIGNED_SKU_ID IS NULL
               </if>                              
              GROUP BY MSL.TRAVEL_SEQUENCE, MSL.LOCATION_ID
              ORDER BY MSL.TRAVEL_SEQUENCE, MSL.LOCATION_ID           
          ";
            var binder = SqlBinder.Create(row => new Location()
                {
                    LocationId = row.GetString("LOCATION_ID"),
                    PalletCount = row.GetInteger("NUMBER_OF_PALLET") ?? 0,
                    TotalPieces = row.GetInteger("TOTAL_PIECES"),
                    MaxAssignedCarton = row.GetInteger("ASSIGNED_MAX_CARTONS"),
                    CartonCount = row.GetInteger("NUMBER_OF_CARTONS") ?? 0,
                    AssignedVwhId = row.GetString("ASSIGNED_VWH_ID"),
                    CartonVwhCount = row.GetInteger("CARTON_VWH_COUNT") ?? 0,
                    CartonVwhId = row.GetString("CARTON_VWH_ID"),
                    CartonSkuCount = row.GetInteger("CARTON_SKU_COUNT") ?? 0,
                    CartonSku = row.GetInteger("CARTON_SKU_ID") == null ? null : new Sku
                        {
                            Style = row.GetString("CTN_STYLE_"),
                            Color = row.GetString("CTN_COLOR_"),
                            Dimension = row.GetString("CTN_DIMENSION_"),
                            SkuSize = row.GetString("CTN_SKU_SIZE_"),
                            SkuId = row.GetInteger("CTN_SKU_ID_").Value,
                            UpcCode = row.GetString("CTN_UPC_CODE_")
                        },
                    AssignedSku = row.GetInteger("SKU_ID") == null ? null : new Sku
                        {
                            Style = row.GetString("STYLE_"),
                            Color = row.GetString("COLOR_"),
                            Dimension = row.GetString("DIMENSION_"),
                            SkuSize = row.GetString("SKU_SIZE_"),
                            SkuId = row.GetInteger("SKU_ID").Value,
                            UpcCode = row.GetString("UPC_CODE_")
                        },
                    CountTotalLocations = row.GetInteger("count_total_locations") ?? 0
                }).Parameter("AREA_ID", areaId)
               .Parameter("EMPTY_LOCATION_FLAG", string.Format("{0}", emptyLocations).ToLower())
               .Parameter("SKU_ID", assignedSkuId)
               .Parameter("ASSIGNED_FLAG", string.Format("{0}", assignedLocations).ToLower());
            if (string.IsNullOrWhiteSpace(locationPattern))
            {
                binder.Parameter("LOCATION_ID", string.Empty)
                    .Parameter("SEARCHLOCATION", string.Empty);
            }
            else if (locationPattern.Contains("%"))
            {
                binder.Parameter("LOCATION_ID", string.Empty)
                    .Parameter("SEARCHLOCATION", locationPattern);
            }
            else
            {
                binder.Parameter("LOCATION_ID", locationPattern)
                    .Parameter("SEARCHLOCATION", string.Empty);
            }
            //if (filters.SkuId == null)
            //{
            //    binder.Parameter("ASSIGNED_FLAG", filters.AssignedLocations.HasValue ? filters.AssignedLocations.ToString().ToLower() : "");
            //}
            //else
            //{
            //    // Ignore the ASSIGNED_FLAG
            //    binder.Parameter("ASSIGNED_FLAG", "");
            //}
            return _db.ExecuteReader(QUERY, binder, maxRows);
        }

        /// <summary>
        /// This function gets the VwhId 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CodeDescriptionModel> GetVwhList()
        {
            const string QUERY =
             @"
                       SELECT VWH_ID AS VWH_ID, 
                               DESCRIPTION AS DESCRIPTION
                         FROM <proxy />TAB_VIRTUAL_WAREHOUSE
                        ORDER BY VWH_ID
                ";
            var binder = SqlBinder.Create(row => new CodeDescriptionModel
            {
                Code = row.GetString("VWH_ID"),
                Description = row.GetString("DESCRIPTION")
            });
            return _db.ExecuteReader(QUERY, binder);

        }

        internal IList<CodeDescriptionModel> GetCountryList()
        {
            const string QUERY = @"
                                SELECT T.COUNTRY_ID AS COUNTRY_ID, T.NAME AS NAME FROM <proxy />TAB_COUNTRY T
                                ";
            var binder = SqlBinder.Create(row => new CodeDescriptionModel
            {
                Code = row.GetString("COUNTRY_ID"),
                Description = row.GetString("NAME")
            });
            return _db.ExecuteReader(QUERY, binder);

        }

        internal IList<PickingArea> GetPickingAreas(string buildingId, string areaId)
        {
            const string QUERY = @"
                                 SELECT I.IA_ID                     AS IA_ID,
                                        I.WAREHOUSE_LOCATION_ID     AS WAREHOUSE_LOCATION_ID,
                                       MAX(I.SHORT_DESCRIPTION)     AS SHORT_DESCRIPTION,
                                       MAX(I.SHORT_NAME)            AS SHORT_NAME,
                                       MAX(I.SHIPPING_AREA_FLAG)    AS SHIPPING_AREA_FLAG,
                                       MAX(I.PICKING_AREA_FLAG)     AS PICKING_AREA_FLAG,
                                       MAX(I.RESOCK_AREA_FLAG)      AS RESOCK_AREA_FLAG,
                                       MAX(I.DEFAULT_IA_LOCATION)   AS LOCATION_NUMBERING_FLAG,
                                       COUNT(UNIQUE IL.LOCATION_ID) AS LOCATION_COUNT,
                                       <if c='$IA_ID'>
                                        COUNT(UNIQUE CASE
                                                       WHEN ILC.LOCATION_ID IS NULL THEN
                                                        IL.LOCATION_ID
                                                     END) AS EMPTY_LOCATIONS,
                                       COUNT(UNIQUE CASE
                                               WHEN ILC.LOCATION_ID IS NOT NULL THEN
                                                IL.LOCATION_ID
                                             END) AS NONEMPTY_LOCATIONS,
                                       COUNT(UNIQUE CASE
                                               WHEN IL.ASSIGNED_UPC_CODE IS NOT NULL THEN
                                                IL.LOCATION_ID
                                             END) AS TOTAL_ASSIGNED_LOCATIONS,
                                       COUNT(UNIQUE CASE
                                               WHEN IL.ASSIGNED_UPC_CODE IS NULL THEN
                                                IL.LOCATION_ID
                                             END) AS TOTAL_UNASSIGNED_LOCATIONS,
                                       COUNT(UNIQUE CASE
                                               WHEN IL.ASSIGNED_UPC_CODE IS NOT NULL AND ILC.LOCATION_ID IS NULL THEN
                                                IL.LOCATION_ID
                                             END) AS EMPTY_ASSIGNED_LOCATIONS,
                                       COUNT(UNIQUE CASE
                                               WHEN IL.ASSIGNED_UPC_CODE IS NOT NULL AND
                                                    ILC.LOCATION_ID IS NOT NULL THEN
                                                IL.LOCATION_ID
                                             END) AS NONEMPTY_ASSIGNED_LOCATIONS,
                                       COUNT(UNIQUE CASE
                                               WHEN IL.ASSIGNED_UPC_CODE IS NULL AND ILC.LOCATION_ID IS NULL THEN
                                                IL.LOCATION_ID
                                             END) AS EMPTY_UNASSIGNED_LOCATIONS,
                                       COUNT(UNIQUE CASE
                                               WHEN IL.ASSIGNED_UPC_CODE IS NULL AND ILC.LOCATION_ID IS NOT NULL THEN
                                                IL.LOCATION_ID
                                             END) AS NONEMPTY_UNASSIGNED_LOCATIONS
                                       </if>
                                        <else>
                                               CAST(NULL AS NUMBER) as EMPTY_LOCATIONS,
                                               CAST(NULL AS NUMBER) as NONEMPTY_LOCATIONS,
                                               CAST(NULL AS NUMBER) as TOTAL_ASSIGNED_LOCATIONS,
                                               CAST(NULL AS NUMBER) as TOTAL_UNASSIGNED_LOCATIONS,
                                               CAST(NULL AS NUMBER) as EMPTY_ASSIGNED_LOCATIONS,
                                               CAST(NULL AS NUMBER) as NONEMPTY_ASSIGNED_LOCATIONS,
                                               CAST(NULL AS NUMBER) as EMPTY_UNASSIGNED_LOCATIONS,
                                               CAST(NULL AS NUMBER) as NONEMPTY_UNASSIGNED_LOCATIONS
                                               
                                        </else>
                                  FROM <proxy />IA I
                                  LEFT OUTER JOIN <proxy />IALOC IL
                                    ON IL.IA_ID = I.IA_ID
                                   <if c='$IA_ID'>
                                    LEFT OUTER JOIN <proxy />IALOC_CONTENT ILC
                                        ON ILC.LOCATION_ID = IL.LOCATION_ID
                                   </if>
                                 WHERE 1 = 1
                                  <if>AND I.WAREHOUSE_LOCATION_ID = :WAREHOUSE_LOCATION_ID</if>
                                  <if>AND I.IA_ID = :IA_ID</if>
                                 GROUP BY I.WAREHOUSE_LOCATION_ID, I.IA_ID
                            ";
            var binder = SqlBinder.Create(row => new PickingArea
            {
                AreaId = row.GetString("IA_ID"),
                Description = row.GetString("SHORT_DESCRIPTION"),
                BuildingId = row.GetString("WAREHOUSE_LOCATION_ID"),
                LocationNumberingFlag = string.IsNullOrWhiteSpace(row.GetString("LOCATION_NUMBERING_FLAG")) ? true : false,
                ShortName = row.GetString("SHORT_NAME"),
                IsPickingArea = row.GetString("PICKING_AREA_FLAG") == "Y",
                IsRestockArea = row.GetString("RESOCK_AREA_FLAG") == "Y",
                IsShippingArea = row.GetString("SHIPPING_AREA_FLAG") == "Y",
                LocationCount = row.GetInteger("LOCATION_COUNT") ?? 0,
                CountEmptyLocations = row.GetInteger("EMPTY_LOCATIONS"),
                CountNonemptyLocations = row.GetInteger("NONEMPTY_LOCATIONS"),
                CountAssignedLocations = row.GetInteger("TOTAL_ASSIGNED_LOCATIONS"),
                CountEmptyUnassignedLocations = row.GetInteger("EMPTY_UNASSIGNED_LOCATIONS"),
                CountUnassignedLocations = row.GetInteger("TOTAL_UNASSIGNED_LOCATIONS"),
                CountEmptyAssignedLocations = row.GetInteger("EMPTY_ASSIGNED_LOCATIONS"),
                CountNonemptyAssignedLocations = row.GetInteger("NONEMPTY_ASSIGNED_LOCATIONS"),
                CountNonemptyUnassignedLocations = row.GetInteger("NONEMPTY_UNASSIGNED_LOCATIONS")
            });
            binder.Parameter("WAREHOUSE_LOCATION_ID", buildingId)
                .Parameter("IA_ID", areaId);
            return _db.ExecuteReader(QUERY, binder);
        }

        internal IList<PickingLocation> GetPickingAreaLocations(string areaId, bool? assignedLocations, bool? emptyLocations, int? assignedSkuId, string locationPattern, int maxRows)
        {
            const string QUERY = @"
                                SELECT COUNT(*) OVER()                  AS TOTAL_LOCATION,
                                       I.LOCATION_ID                    AS LOCATION_ID,
                                       MAX(I.ASSIGNED_UPC_MAX_PIECES)   AS ASSIGNED_UPC_MAX_PIECES,
                                       MAX(I.ASSIGNED_UPC_CODE)         AS ASSIGNED_UPC_CODE_,
                                       MAX(MS.STYLE)                    AS STYLE_,
                                       MAX(MS.COLOR)                    AS COLOR_,
                                       MAX(MS.DIMENSION)                AS DIMENSION_,
                                       MAX(MS.SKU_SIZE)                 AS SKU_SIZE_,
                                       MAX(MS.SKU_ID)                   AS SKU_ID,
                                       MAX(I.VWH_ID)                    AS VWH_ID,
                                       SUM(IL.NUMBER_OF_UNITS)          AS NUMBER_OF_UNITS
                                  FROM <proxy />IALOC I
                                  LEFT OUTER JOIN <proxy />IALOC_CONTENT IL
                                    ON IL.LOCATION_ID = I.LOCATION_ID
                                  LEFT OUTER JOIN <proxy />MASTER_SKU MS
                                    ON MS.UPC_CODE = I.ASSIGNED_UPC_CODE
                                 WHERE 1 = 1
                                <if>
                                AND I.IA_ID = :IA_ID
                                </if>
                                <if>AND I.LOCATION_ID = :LOCATION_ID</if>
                               <if>AND I.LOCATION_ID LIKE :SEARCHLOCATION</if>
                               <if>AND MS.SKU_ID = :SKU_ID</if>
                                <if c=""$EMPTY_LOCATION_FLAG = 'true' "">
                                       AND NOT EXISTS
                                        (SELECT 1 FROM <proxy/>IALOC_CONTENT ILCON WHERE ILCON.LOCATION_ID = I.LOCATION_ID
                                            <if>AND ILCON.LOCATION_ID = :LOCATION_ID</if>)
                                   </if>
                               <if c=""$EMPTY_LOCATION_FLAG = 'false' "">
                                   AND I.LOCATION_ID IN
                                    (SELECT LOCATION_ID FROM <proxy/>IALOC_CONTENT)
                               </if>
                               <if c=""$ASSIGNED_FLAG ='true' "">
                                  AND I.ASSIGNED_UPC_CODE IS NOT NULL
                               </if>
                               <if c=""$ASSIGNED_FLAG ='false' "">
                                AND I.ASSIGNED_UPC_CODE IS NULL
                               </if>                            
                                 GROUP BY I.LOCATION_ID";
            var binder = SqlBinder.Create(row => new PickingLocation()
            {
                LocationId = row.GetString("LOCATION_ID"),
                TotalPieces = row.GetInteger("NUMBER_OF_UNITS") ?? 0,
                AssignedVwhId = row.GetString("VWH_ID"),
                MaxAssignedPieces = row.GetInteger("ASSIGNED_UPC_MAX_PIECES") ?? 0,
                AssignedSku = row.GetInteger("SKU_ID") == null ? null : new Sku
                {
                    Style = row.GetString("STYLE_"),
                    Color = row.GetString("COLOR_"),
                    Dimension = row.GetString("DIMENSION_"),
                    SkuSize = row.GetString("SKU_SIZE_"),
                    SkuId = row.GetInteger("SKU_ID").Value,
                    UpcCode = row.GetString("ASSIGNED_UPC_CODE_")
                },
                CountTotalLocations = row.GetInteger("TOTAL_LOCATION") ?? 0
            }).Parameter("IA_ID", areaId)
              .Parameter("EMPTY_LOCATION_FLAG", string.Format("{0}", emptyLocations).ToLower())
              .Parameter("ASSIGNED_FLAG", string.Format("{0}", assignedLocations).ToLower())
              .Parameter("SKU_ID", assignedSkuId);
            if (string.IsNullOrWhiteSpace(locationPattern))
            {
                binder.Parameter("LOCATION_ID", string.Empty)
                    .Parameter("SEARCHLOCATION", string.Empty);
            }
            else if (locationPattern.Contains("%"))
            {
                binder.Parameter("LOCATION_ID", string.Empty)
                    .Parameter("SEARCHLOCATION", locationPattern);
            }
            else
            {
                binder.Parameter("LOCATION_ID", locationPattern)
                    .Parameter("SEARCHLOCATION", string.Empty);
            }
            return _db.ExecuteReader(QUERY, binder, maxRows);
        }

        internal Sku GetSku(int skuId)
        {
            if (skuId == 0)
            {
                throw new ArgumentNullException("skuId");
            }
            const string QUERY =
                             @"
                            SELECT  MS.UPC_CODE AS UPC_CODE,
                                    MS.SKU_ID AS SKU_ID,
                                    MS.STYLE AS STYLE,
                                    MS.COLOR AS COLOR,
                                    MS.DIMENSION AS DIMENSION,
                                    MS.SKU_SIZE AS SKU_SIZE
                                FROM <proxy />MASTER_SKU MS
                              WHERE MS.SKU_ID = :SKU_ID
                            ";

            var binder = SqlBinder.Create(row => new Sku
                {
                    SkuId = row.GetInteger("SKU_ID").Value,
                    Style = row.GetString("STYLE"),
                    Color = row.GetString("COLOR"),
                    Dimension = row.GetString("DIMENSION"),
                    SkuSize = row.GetString("SKU_SIZE"),
                    UpcCode = row.GetString("UPC_CODE")
                }).Parameter("SKU_ID", skuId);
            return _db.ExecuteSingle(QUERY, binder);

        }

    }
}
//$Id$