using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan_BLL.MailBL;
using deneysan_DAL.Entities;

namespace deneysan.Areas.Admin.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Admin/Mail/

        public ActionResult Index()
        {
            if (RouteData.Values["type"] != null)
            {
                int type = Convert.ToInt32(RouteData.Values["type"].ToString());
                var list = MailManager.GetMailUsersList(type);
                ViewBag.SelVal = RouteData.Values["type"].ToString();
                return View(list);
            }
            else
            {
                var list = MailManager.GetMailUsersList(0);
                return View(list);
            }
        }


        public JsonResult DeleteRecord(int id)
        {
            return Json(MailManager.Delete(id));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(MailUsers model)
        {
//            record.TypeId = Convert.ToInt32(EnumInstituionalTypes.Misyon);
            ViewBag.ProcessMessage = MailManager.AddMailUsers(model);
            ModelState.Clear();
            return View();
        }

        public ActionResult Edit()
        {
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    MailUsers record = MailManager.GetMailUsersById(nid);
                    return View(record);
                }
                else
                    return View();
            }
            else
                return View();
        }


        [HttpPost]
        public ActionResult Edit(MailUsers model)
        {
            if (ModelState.IsValid)
            {

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        model.MailUserId = nid;
                        ViewBag.ProcessMessage = MailManager.EditMailUser(model);
                        return View(model);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(model);
                    }
                }
                else return View();


            }
            else
                return View();
        }
    }
}
