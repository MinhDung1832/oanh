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
    public class ColorController : Controller
    {
        // GET: Color
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var getCode = BaseConnectionSql.ExecuteList_Helper<objCombox>("web", "test");
            ViewBag.getCode = getCode[0];
            return View();
        }

        public ActionResult Update(string ID)
        {
            var list_detail = BaseConnectionSql.ExecuteList_Helper<ColorModel>("Color_GetDetail", ID);
            ViewBag.list_detail = list_detail[0];
            return View();
        }

        public ActionResult getList()
        {
            //DataTable table = DataAccess.DataAccessMenu.Menu_getMenu();
            DataTable table = BaseConnectionSql.ExecuteTable_Helper("web", "Color_GetList");
            return PartialView("~/Views/Color/Tables/__tblIndex.cshtml", table);
        }

        public ActionResult CreateColor(ColorModel lst)
        {
            try
            {
                BaseConnectionSql.Execute_Update_Insert<ColorModel>("web", "Color_Create", lst);
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "CreateColor");
                return Json(null);
            }
        }

        public ActionResult UpdateActive(List<ColorModel> lst)
        {
            try
            {
                foreach (ColorModel po in lst)
                {
                    BaseConnectionSql.Execute_Update_Insert<ColorModel>("web", "Color_UpdateActive", po);
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "UpdateActive");
                return Json(null);
            }
        }

        public ActionResult UpdateColor(ColorModel lst)
        {
            try
            {
                BaseConnectionSql.Execute_Update_Insert<ColorModel>("web", "Color_Update", lst);
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "UpdateColor");
                return Json(null);
            }
        }
    }
}