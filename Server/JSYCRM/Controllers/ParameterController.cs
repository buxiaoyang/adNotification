using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JSYCRM.Filters;

namespace JSYCRM.Controllers
{
    [Authentication]
    public class ParameterController : Controller
    {

        public ActionResult Index(String Type, String message)
        {
            message = HttpUtility.UrlDecode(message);
            if (Type == "" || Type == null)
            {
                ViewBag.Type = "Client";
            }
            else
            {
                ViewBag.Type = Type;
            }
            if (message != null && message != "")
            {
                ViewBag.message = message;
            }
            DAL.z_parameter dal_z_parameter = new DAL.z_parameter();
            List<Models.z_parameter> z_parameter_list = dal_z_parameter.GetModelList(ViewBag.Type);
            return View(z_parameter_list);
        }

        public ActionResult Edit(String Type, String ID)
        {
            ViewBag.Type = Type;
            DAL.z_parameter dal_z_parameter = new DAL.z_parameter();
            Models.z_parameter model_z_parameter = dal_z_parameter.GetModel(new Guid(ID));
            return View(model_z_parameter);
        }

        [HttpPost]
        public ActionResult Edit(String Type, String ID, FormCollection collection)
        {
            try
            {
                DAL.z_parameter dal_z_parameter = new DAL.z_parameter();
                Models.z_parameter model_z_parameter = dal_z_parameter.GetModel(new Guid(ID));
                String VALUE = collection["VALUE"].Trim();
                String DESCRIPTION = collection["DESCRIPTION"].Trim();
                if (VALUE == null || VALUE == "")
                {
                    ViewBag.Type = Type;
                    ViewBag.message = "Edit failed. Value can't be blank";
                    return View(model_z_parameter);
                }
                model_z_parameter.VALUE = VALUE;
                model_z_parameter.DESCRIPTION = DESCRIPTION;
                model_z_parameter.UPDATE_USER_ID = ViewBag.model_z_user.ID;
                model_z_parameter.UPDATE_DATETIME = DateTime.Now;
                dal_z_parameter.Update(model_z_parameter);
                return RedirectToAction("Index", new { Type = HttpUtility.UrlEncode(Type), message = HttpUtility.UrlEncode("Edit Succeed.") });
            }
            catch
            {
                ViewBag.Type = Type;
                ViewBag.message = "Edit Failed.";
                return View();
            }
        }

    }
}
