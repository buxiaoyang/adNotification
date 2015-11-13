using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSYCRM.Models
{
    /// <summary>
    /// m_announcement_user:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class m_announcement_user
    {
        public m_announcement_user()
        { }
        #region Model
        private Guid _announcement_id;
        private string _user_account;
        private string _status;
        /// <summary>
        /// 
        /// </summary>
        public Guid ANNOUNCEMENT_ID
        {
            set { _announcement_id = value; }
            get { return _announcement_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string USER_ACCOUNT
        {
            set { _user_account = value; }
            get { return _user_account; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

    }
}