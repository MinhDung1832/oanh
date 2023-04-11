using Newtonsoft.Json;
using ProductAllTool.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.DataAccess;
using WEB.Models;

namespace WEB.Controllers
{
    public class SizeController : Controller
    {
        // GET: Size
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var getCode = BaseConnectionSql.ExecuteList_Helper<objCombox>("web", "auto_code_size");
            ViewBag.getCode = getCode[0];
            return View();
        }

        public ActionResult Update(string ID)
        {
            var list_detail = BaseConnectionSql.ExecuteList_Helper<SizeModel>("Size_GetDetail", ID);
            ViewBag.list_detail = list_detail[0];
            return View();
        }

        public ActionResult getList()
        {
            DataTable table = BaseConnectionSql.ExecuteTable_Helper("web", "Size_GetList");
            return PartialView("~/Views/Size/Tables/__tblIndex.cshtml", table);
        }

        public ActionResult CreateSize(SizeModel lst)
        {
            try
            {
                BaseConnectionSql.Execute_Update_Insert<SizeModel>("web", "Size_Create", lst);
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "CreateSize");
                return Json(null);
            }
        }

        public ActionResult UpdateSize(SizeModel lst)
        {
            try
            {
                BaseConnectionSql.Execute_Update_Insert<SizeModel>("web", "Size_Update", lst);
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "UpdateSize");
                return Json(null);
            }
        }

        public ActionResult DeleteSize(List<SizeModel> lst)
        {
            try
            {
                foreach (SizeModel ar in lst)
                {
                    BaseConnectionSql.Execute_Update_Insert<SizeModel>("web", "Size_Delete", ar);
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "DeleteSize");
                return Json(null);
            }
        }
    }
}