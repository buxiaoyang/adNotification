using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JSYCRM.Controllers
{
    public class LDAPController : Controller
    {
        //
        // GET: /LDAP/

        public JsonResult GetList(string filter)
        {
            
            DAL.ldap_cn dal_ldap_cn = new DAL.ldap_cn();
            try
            {
                ArrayList data = null;
                if (filter.Trim() == "")
                {
                    data = dal_ldap_cn.getUserGroupRoot();
                }
                else
                {
                    filter = "*" + filter.Trim() + "*";
                    data = dal_ldap_cn.getUserGroupList(filter);
                }          
                return Json(new { result = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult GetListInGroup(string group)
        {
            DAL.ldap_cn dal_ldap_cn = new DAL.ldap_cn();
            try
            {
                ArrayList data = dal_ldap_cn.getUserGroupInGroup(group);
                return Json(new { result = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
