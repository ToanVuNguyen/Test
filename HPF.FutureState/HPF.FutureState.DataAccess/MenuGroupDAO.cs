using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlClient;
using System.Data;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class MenuGroupDAO:BaseDAO
    {
        private static readonly MenuGroupDAO instance = new MenuGroupDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static MenuGroupDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected MenuGroupDAO()
        {
            
        }
        /// <summary>
        /// Check if menuGroup has been added to MenuGroupDTOCollection or not ?
        /// </summary>
        /// <param name="result">MenuGroupDTOCollection contains menubar base on userid</param>
        /// <param name="menuGroup">new MenuGroupDTO</param>
        /// <returns>-1 for nonexist, else return the index of menugroup in collection</returns>
        private int CheckExists(MenuGroupDTOCollection result,MenuGroupDTO menuGroup)
        {
            if(result==null)
                return -1;
            for(int i =0;i<result.Count;i++)
                if(result[i].GroupId==menuGroup.GroupId)
                    return i ;
            return -1;
        }
        /// <summary>
        /// Get the menu bar by userId
        /// </summary>
        /// <param name="userId">ccrc_userid</param>
        /// <returns>Menu Tree for user</returns>
        public MenuGroupDTOCollection GetMenuGroupCollectionByUserID(int userId)
        { 
            MenuGroupDTOCollection result = null;
            var dbConnection = CreateConnection();
            
            var command = CreateSPCommand("hpf_menu_group_get_from_userid", dbConnection);

            //<Parameter>
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_userid", userId);
            //</Parameter>            
            try
            {
                command.Parameters.AddRange(sqlParam);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result=new MenuGroupDTOCollection();
                    while (reader.Read())
                    {
                        MenuGroupDTO menuGroup = new MenuGroupDTO();
                        menuGroup.GroupId = ConvertToInt(reader["menu_group_id"]);
                        menuGroup.GroupName = ConvertToString(reader["group_name"]);
                        menuGroup.GroupSortOrder = ConvertToInt(reader["group_sort_order"]);
                        menuGroup.GroupTarget = ConvertToString(reader["group_target"]);

                        MenuItemDTO menuItem = new MenuItemDTO();
                        menuItem.ItemId = ConvertToInt(reader["menu_item_id"]);
                        menuItem.ItemName = ConvertToString(reader["item_name"]);
                        menuItem.ItemSearchOrder = ConvertToInt(reader["item_sort_order"]);
                        menuItem.ItemTarget = ConvertToString(reader["item_target"]);
                        menuItem.PermissionValue = ConvertToString(reader["permission_value"])[0];
                        menuItem.Visible = ConvertToBool(reader["visibled"]);
                        
                        int index =CheckExists(result,menuGroup);
                        
                        //MenuGroup Non exists
                        if(index==-1)
                        {
                            menuGroup.MenuItemList.Add(menuItem);
                            result.Add(menuGroup);
                        }
                        else// MenuGroup already exists in Collection
                        {
                            result[index].MenuItemList.Add(menuItem);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return result;
        }
    }
}
