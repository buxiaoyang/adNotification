﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace JSYCRM.DAL
{
    public class m_announcement_user
    {
        public m_announcement_user()
        { }
        #region  BasicMethod

        public bool AddList(Models.m_announcement model_m_announcement)
        {
            String[] toList = model_m_announcement.DEPARTMENT.Split(';');
            DAL.m_announcement_user dal_m_announcement_user = new DAL.m_announcement_user();
            dal_m_announcement_user.Delete(model_m_announcement.ID);
            foreach (string toItem in toList)
            {
                if (toItem.Trim() != "")
                {
                    Models.m_announcement_user model_m_announcement_user = new Models.m_announcement_user();
                    model_m_announcement_user.ANNOUNCEMENT_ID = model_m_announcement.ID;
                    model_m_announcement_user.USER_ACCOUNT = toItem.Trim();
                    model_m_announcement_user.STATUS = "";
                    dal_m_announcement_user.Add(model_m_announcement_user);
                }
            }
            return true;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Models.m_announcement_user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into m_announcement_user(");
            strSql.Append("ANNOUNCEMENT_ID,USER_ACCOUNT,STATUS)");
            strSql.Append(" values (");
            strSql.Append("@ANNOUNCEMENT_ID,@USER_ACCOUNT,@STATUS)");
            SqlParameter[] parameters = {
					new SqlParameter("@ANNOUNCEMENT_ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@USER_ACCOUNT", SqlDbType.NVarChar,50),
					new SqlParameter("@STATUS", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ANNOUNCEMENT_ID;
            parameters[1].Value = model.USER_ACCOUNT;
            parameters[2].Value = model.STATUS;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid ANNOUNCEMENT_ID, string USER_ACCOUNT)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from m_announcement_user ");
                strSql.Append(" where ANNOUNCEMENT_ID=@ANNOUNCEMENT_ID and USER_ACCOUNT=@USER_ACCOUNT ");
                SqlParameter[] parameters = {
					new SqlParameter("@ANNOUNCEMENT_ID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@USER_ACCOUNT", SqlDbType.NVarChar,50)
                    };
                parameters[0].Value = ANNOUNCEMENT_ID;
                parameters[1].Value = USER_ACCOUNT;

                int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid ANNOUNCEMENT_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from m_announcement_user ");
            strSql.Append(" where ANNOUNCEMENT_ID=@ANNOUNCEMENT_ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ANNOUNCEMENT_ID", SqlDbType.UniqueIdentifier,16)
                    };
            parameters[0].Value = ANNOUNCEMENT_ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.m_announcement_user GetModel(string USER_ACCOUNT)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ANNOUNCEMENT_ID,USER_ACCOUNT,STATUS from m_announcement_user ");
            strSql.Append(" where USER_ACCOUNT=@USER_ACCOUNT ");
            SqlParameter[] parameters = {
					    new SqlParameter("@USER_ACCOUNT", SqlDbType.NVarChar,50)
                    };
            parameters[0].Value = USER_ACCOUNT;

            Models.m_announcement_user model = new Models.m_announcement_user();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.m_announcement_user DataRowToModel(DataRow row)
        {
            Models.m_announcement_user model = new Models.m_announcement_user();
            if (row != null)
            {
                if (row["ANNOUNCEMENT_ID"] != null && row["ANNOUNCEMENT_ID"].ToString() != "")
                {
                    model.ANNOUNCEMENT_ID = new Guid(row["ANNOUNCEMENT_ID"].ToString());
                }
                if (row["USER_ACCOUNT"] != null)
                {
                    model.USER_ACCOUNT = row["USER_ACCOUNT"].ToString();
                }
                if (row["STATUS"] != null)
                {
                    model.STATUS = row["STATUS"].ToString();
                }
            }
            return model;
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}