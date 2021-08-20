﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace EclipseLibrary.Oracle.Helpers
{
    /// <summary>
    /// Represents an instance of a row returned from an SQL query
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class is used extensively by the querying functions of <see cref="OracleDatastore"/> class.
    /// In debug mode, this class throws exception if the same column name is used more than once within the query. It also raises
    /// exception if the query retrieves 
    /// </para>
    /// </remarks>
    public class OracleDataRow2
    {
        //private readonly OracleDataReader _reader;
        private readonly object[] _values;

        /// <summary>
        /// Maps field name to its ordinal. It is possible that the same field name will be retrieved multiple times from the query.
        /// We are using Lookup instead of Dictionary because multiple columns in the query may have same fieldname. This can happen when the caller intends
        /// to access values by index. Inquiry does this.
        /// </summary>
        private ILookup<string, int> _fieldMap;

        /// <summary>
        /// Create an instance once you have recieved a reader
        /// </summary>
        /// <param name="schemaTable"></param>
        public OracleDataRow2(DataTable schemaTable)
        {
            _values = new object[schemaTable.Rows.Count];
            //_reader = reader;
            //var x = _reader.GetSchemaTable();

            //var query = (from DataRow item in schemaTable.Rows
            //            select new
            //            {
            //                ColumnName = (string)item["ColumnName"],
            //                ColumnOrdinal = (int)item["ColumnOrdinal"]
            //            }).ToLookup(p => p.ColumnName, p => p.ColumnOrdinal, StringComparer.InvariantCultureIgnoreCase);

            _fieldMap = (from DataRow item in schemaTable.Rows
                        select new
                        {
                            ColumnName = (string)item["ColumnName"],
                            ColumnOrdinal = (int)item["ColumnOrdinal"]
                        }).ToLookup(p => p.ColumnName, p => p.ColumnOrdinal, StringComparer.InvariantCultureIgnoreCase);

            //var dict = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            //dict.Add()
        }

        ///// <summary>
        ///// Called after the reader has been positioned at the next row
        ///// </summary>
        //[Obsolete]
        //internal void RefreshValues()
        //{
        //    //_reader.GetProviderSpecificValues(_values);
        //    _reader.GetOracleValues(_values);
        //}

        /// <summary>
        /// Call this each time the reader is repositioned
        /// </summary>
        /// <param name="reader"></param>
        internal void SetValues(OracleDataReader reader)
        {
            reader.GetOracleValues(_values);
        }


        public string GetString(int index)
        {
            var val = GetValueAs<OracleString>(index);
            if (val.IsNull)
            {
                return string.Empty;
            }
            return val.Value;
        }

        public string GetString(string fieldName)
        {
            var val = GetValueAs<OracleString>(fieldName);
            if (val.IsNull)
            {
                return string.Empty;
            }
            return val.Value;
        }

        /// <summary>
        /// Use this to retrieve the XML inside an XMLType as an XElement
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        [Obsolete("Use GetString instead")]
        public XElement GetXml(string fieldName)
        {
            //var val = GetValueAs<OracleXmlType>(fieldName);
            //if (val.IsNull)
            //{
            //    return null;
            //}
            //return XElement.Parse(val.Value);
            throw new NotImplementedException();
        }

        public int? GetInteger(string fieldName)
        {
            var val = GetValueAs<OracleDecimal>(fieldName);
            if (val.IsNull)
            {
                return null;
            }
            if (!val.IsInt)
            {
                throw new InvalidCastException(fieldName + " is not an integer. Retrieved value is " + val.ToString());
            }
            return val.ToInt32();
        }

        public int? GetInteger(int index)
        {
            var val = GetValueAs<OracleDecimal>(index);
            if (val.IsNull)
            {
                return null;
            }
            if (!val.IsInt)
            {
                throw new InvalidCastException(string.Format("Field {0} is not an integer. Retrieved value is {1}", index, val.ToString()));
            }
            return val.ToInt32();
        }

        //Ankit: Added long data handler function to handle long integer data send from SAP.

        public long? GetLong(string fieldName)
        {
            var val = GetValueAs<OracleDecimal>(fieldName);
            if (val.IsNull)
            {
                return null;
            }
            if (!val.IsInt)
            {
                throw new InvalidCastException(fieldName + " is not an long. Retrieved value is " + val.ToString());
            }
            return val.ToInt64();
        }

        public long? GetLong(int index)
        {
            var val = GetValueAs<OracleDecimal>(index);
            if (val.IsNull)
            {
                return null;
            }
            if (!val.IsInt)
            {
                throw new InvalidCastException(string.Format("Field {0} is not an long. Retrieved value is {1}", index, val.ToString()));
            }
            return val.ToInt64();
        }

        /// <summary>
        /// Returns a fractional value stored in the database
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        /// <remarks>
        /// If you attempt to retrieve <c>Weight/12</c> from the database, you will get an overflow error because the precision of the returned result is not
        /// sufficient to fit into a decimal. We catch this exception and reduce the precision before returning the value.
        /// </remarks>
        public Decimal? GetDecimal(string fieldName)
        {
            var val = GetValueAs<OracleDecimal>(fieldName);
            if (val.IsNull)
            {
                return null;
            }
            try
            {
                return val.Value;
            }
            catch (OverflowException)
            {
                return OracleDecimal.SetPrecision(val, 28).Value;
            }
        }

        /// <summary>
        /// Expects database field to be of string type
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public TValue GetEnum<TValue>(string fieldName)
        {
            var val = this.GetString(fieldName);
            if (string.IsNullOrEmpty(val))
            {
                return default(TValue);
            }
            return (TValue)Enum.Parse(Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue), val, true);
        }

        /// <summary>
        /// Attempts the parse the string returned by the database into an enum of type TValue
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public TValue GetEnum<TValue>(int index)
        {
            var val = this.GetString(index);
            if (string.IsNullOrEmpty(val))
            {
                return default(TValue);
            }
            return (TValue)Enum.Parse(Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue), val, true);
        }

        /// <summary>
        /// Retrieves the date value from an OracleDate, OracleTimeStamp, OracleTimeStampTZ or OracleTimeStampLTZ column
        /// </summary>
        /// <param name="fieldName">Column name</param>
        /// <returns></returns>
        //[Obsolete("Use GetDateTimeOffset() instead")]
        public DateTime? GetDate(string fieldName)
        {
            var val = GetValueAs<INullable>(fieldName);
            if (val.IsNull)
            {
                return null;
            }
            var type = val.GetType();
            if (type == typeof(OracleDate))
            {
                return ((OracleDate)val).Value;
            }
            if (type == typeof(OracleTimeStamp))
            {
                return ((OracleTimeStamp)val).Value;
            }
            if (type == typeof(OracleTimeStampTZ))
            {
                // The caller does not care about the time zone. If he did care, he would have called GetTimeStampTZ
                return ((OracleTimeStampTZ)val).Value;
            }
            if (type == typeof(OracleTimeStampLTZ))
            {
                return ((OracleTimeStampLTZ)val).Value;
            }
            throw new NotSupportedException("Unsupported Date type " + type.FullName);
        }

        public DateTimeOffset? GetDateTimeOffset(string fieldName)
        {
            var val = GetValueAs<INullable>(fieldName);
            if (val.IsNull)
            {
                return null;
            }
            var type = val.GetType();
            if (type == typeof(OracleDate))
            {
                return ((OracleDate)val).Value;
            }
            if (type == typeof(OracleTimeStamp))
            {
                return ((OracleTimeStamp)val).Value;
            }
            if (type == typeof(OracleTimeStampTZ))
            {
                // The caller does not care about the time zone. If he did care, he would have called GetTimeStampTZ
                return ((OracleTimeStampTZ)val).Value;
            }
            if (type == typeof(OracleTimeStampLTZ))
            {
                return ((OracleTimeStampLTZ)val).Value;
            }
            throw new NotSupportedException("Unsupported Date type " + type.FullName);
        }

        /// <summary>
        /// For Timestamp with TimeZone database columns
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public DateTimeOffset? GetTimeStampTZ(string fieldName)
        {
            var val = GetValueAs<OracleTimeStampTZ>(fieldName);
            if (val.IsNull)
            {
                return null;
            }
            return new DateTimeOffset(val.Value, val.GetTimeZoneOffset());
        }

        public TimeSpan? GetInterval(string fieldName)
        {
            var val = GetValueAs<OracleIntervalDS>(fieldName);
            if (val.IsNull)
            {
                return null;
            }
            return val.Value;
        }

#if DEBUG
        private HashSet<string> _debugFieldNames;
#endif

        private T GetValueAs<T>(string fieldName)
        {
#if DEBUG
            // Additional debug check to ensure that same field is not being retrieved twice.
            if (_debugFieldNames == null)
            {
                _debugFieldNames = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
                foreach (var item in _fieldMap)
                {
                    var b = _debugFieldNames.Add(item.Key);
                    if (!b)
                    {
                        // Key already added
                        var msg = string.Format("DEBUG only exception: Query retrieves column {0} more than once.", item.Key);
                        throw new OperationCanceledException(msg);
                    }
                }
            }
            // The dispose method raises exception if all fields are not accessed
            _debugFieldNames.Remove(fieldName);
#endif
            object obj;
            try
            {
                obj = _values[_fieldMap[fieldName].Single()];
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(fieldName, ex);
            }
#if DEBUG
            catch (KeyNotFoundException ex)
            {
                // Reraise the exception with a clearer message
                throw new KeyNotFoundException(fieldName, ex);
            }
#else
            catch (KeyNotFoundException)
            {
                // Eat the exception to prevent the application from crashing because some column was dropped.
                obj = null;
            }
#endif
            if (obj == null)
            {
                return default(T);
            }
            try
            {
                return (T)obj;
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException(string.Format("Field {0}: Could not cast {1} to {2}", fieldName, obj.GetType().FullName, typeof(T).FullName), ex);
            }
        }

        private T GetValueAs<T>(int index)
        {
            object obj = _values[index];
            if (obj == null)
            {
                return default(T);
            }
            try
            {
                return (T)obj;
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException(string.Format("Field {0}: Could not cast {1} to {2}", index, obj.GetType().FullName, typeof(T).FullName), ex);
            }
        }

#if DEBUG
        /// <summary>
        /// This is called after we are done with retrieving all values. It ensures that every field retrieved by the query is used.
        /// </summary>
        internal void DebugCheck()
        {
            if (_debugFieldNames == null || _debugFieldNames.Count == 0)
            {
                return;
            }
            var unusedColumns = _debugFieldNames.Where(p => !p.EndsWith("_"));
            // Make sure that all fields retrieved by the query were accessed
            if (unusedColumns.Any())
            {
                // If you have query columns which are conditionally used, you should suffix them with _ to suppress this exception.
                throw new OperationCanceledException(string.Format("DEBUG only exception: Unused columns retrieved by the query: {0}",
                    string.Join(",", unusedColumns)));
            }
        }
#endif
    }
}
