using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JSYCRM.Controllers
{
    public class MessageController : Controller
    {
        //
        // GET: /Message/

        public JsonResult Get(string user)
        {
            DAL.m_announcement_user dal_m_announcement_user = new DAL.m_announcement_user();
            DAL.z_parameter dal_z_parameter = new DAL.z_parameter();
            DAL.m_announcement dal_m_announcement = new DAL.m_announcement();
            Models.m_announcement_user model_m_announcement_user = dal_m_announcement_user.GetModel(user);
            Models.m_announcement model_m_announcement = null;
            Models.z_parameter model_z_parameter = dal_z_parameter.GetModel("Client", "Interval");
            if (model_m_announcement_user != null)
            {
                model_m_announcement = dal_m_announcement.GetModel(model_m_announcement_user.ANNOUNCEMENT_ID);
                if(model_m_announcement==null)
                {
                    dal_m_announcement_user.Delete(model_m_announcement_user.ANNOUNCEMENT_ID, model_m_announcement_user.USER_ACCOUNT);
                }
            }
            if (model_m_announcement == null)
            {
                return Json(new { result = false, interval = model_z_parameter.VALUE }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Common.Encrypt encrypt = new Common.Encrypt();
                return Json(new { 
                        result = true,
                        interval = model_z_parameter.VALUE,
                        ID = encrypt.EncryptString(model_m_announcement.ID.ToString()),
                        TITLE = encrypt.EncryptString(model_m_announcement.TITLE),
                        CONTENT = encrypt.EncryptString(model_m_announcement.BODY),
                        CREATE_USER = encrypt.EncryptString(model_m_announcement.CREATE_USER),
                        CREATE_DATETIME = model_m_announcement.CREATE_DATETIME
                        }, JsonRequestBehavior.AllowGet);
            }
            

        }

        public JsonResult Confirm(string announcement, string user)
        {
            DAL.m_announcement_user dal_m_announcement_user = new DAL.m_announcement_user();
            return Json(new { result = dal_m_announcement_user.Delete(new Guid(announcement), user)}, JsonRequestBehavior.AllowGet);
        }

    }
}
