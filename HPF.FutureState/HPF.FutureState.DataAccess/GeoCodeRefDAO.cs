using System;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.DataAccess
{
    public class GeoCodeRefDAO : BaseDAO
    {
        private static readonly GeoCodeRefDAO instance = new GeoCodeRefDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static GeoCodeRefDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected GeoCodeRefDAO()
        {
            
        }
               

        /// <summary>
        /// Select all RefCodeItem from database. 
        /// Use cache
        /// </summary>
        /// <param name=""></param>
        /// <returns>RefCodeItemDTOCollection</returns>
        public GeoCodeRefDTOCollection GetGeoCodeRef()
        {
            GeoCodeRefDTOCollection results = HPFCacheManager.Instance.GetData<GeoCodeRefDTOCollection>("geoCodeRef");
            if (results == null)
            {                
                var dbConnection = CreateConnection();
                var command = CreateCommand("hpf_geo_code_ref_get", dbConnection);            
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new GeoCodeRefDTOCollection();
                        while (reader.Read())
                        {
                            GeoCodeRefDTO item = new GeoCodeRefDTO();
                            item.GeocodeRefId  = ConvertToInt(reader["geocode_ref_id"]);
                            item.ZipCode = ConvertToString(reader["zip_code"]);
                            item.ZipType = ConvertToString(reader["zip_type"]);
                            item.CityName = ConvertToString(reader["city_name"]);
                            item.CityType = ConvertToString(reader["city_type"]);
                            item.CountyName = ConvertToString(reader["county_name"]);
                            item.CountyFIPS = ConvertToString(reader["county_FIPS"]);
                            item.StateName = ConvertToString(reader["state_name"]);
                            item.StateAbbr = ConvertToString(reader["state_abbr"]);
                            item.StateFIPS = ConvertToString(reader["state_FIPS"]);
                            item.MSACode = ConvertToString(reader["MSA_code"]);
                            item.AreaCode = ConvertToString(reader["area_code"]);
                            item.TimeZone = ConvertToString(reader["time_zone"]);
                            item.UTC = ConvertToDecimal(reader["utc"]);
                            item.DST = ConvertToString(reader["dst"]);
                            item.Latitude = ConvertToString(reader["latitude"]);
                            item.Longitude = ConvertToString(reader["longitude"]);
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    dbConnection.Close();
                    HPFCacheManager.Instance.Add("geoCodeRef", results);
                }
                catch (Exception Ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
                }
            }            
            return results;
        }   
    }
}
