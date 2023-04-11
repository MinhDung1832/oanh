using Lib.Utils.Package;
using Newtonsoft.Json;
using ProductAllTool.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
using WEB.DataAccess;
using WEB.Models;

namespace WEB.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getList()
        {
            DataTable table = BaseConnectionSql.ExecuteTable_Helper("web", "Product_GetList");
            return PartialView("~/Views/Product/Tables/__tblIndex.cshtml", table);
        }

        public ActionResult Create()
        {
            var list_Color = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Color_cbo");
            ViewBag.list_Color = list_Color;
            var list_Size = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Size_cbo");
            ViewBag.list_Size = list_Size;
            var getCode = BaseConnectionSql.ExecuteList_Helper<objCombox>("web", "auto_code");
            ViewBag.getCode = getCode[0];
            return View();
        }

        public ActionResult Update(int id)
        {
            Session["id"] = id;
            var list_Color = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Color_cbo");
            ViewBag.list_Color = list_Color;
            var list_Size = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Size_cbo");
            ViewBag.list_Size = list_Size;
            var list_detail = BaseConnectionSql.ExecuteList_Helper<Pro>("web", "Product_Detail", id);
            ViewBag.list_detail = list_detail[0];
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(List<ProColor> lstColor, List<ProSize> lstSize, Pro lstPro)
        {
            try
            {
                BaseConnectionSql.Execute_Update_Insert<Pro>("web", "Product_Create", lstPro);
                foreach (ProColor item1 in lstColor)
                {
                    BaseConnectionSql.Execute_Update_Insert<ProColor>("web", "ProColor_Create", item1);
                }
                foreach (ProSize item2 in lstSize)
                {
                    BaseConnectionSql.Execute_Update_Insert<ProSize>("web", "ProSize_Create", item2);
                }

                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "CreateProduct");
                return Json(null);
            }
        }

        public ActionResult UpdateProduct(List<ProColor> lstColor, List<ProSize> lstSize, Pro lstPro, ProColor lstColorDel, ProSize lstSizeDel)
        {
            try
            {
                var cd = BaseConnectionSql.Execute_Update_Insert<ProColor>("web", "ProColor_Delete", lstColorDel);
                var ef = BaseConnectionSql.Execute_Update_Insert<ProSize>("web", "ProSize_Delete", lstSizeDel);
                if (lstPro.Code != "")
                {
                    var ab = BaseConnectionSql.Execute_Update_Insert<Pro>("web", "Product_Update", lstPro);
                    if (lstColor.Count > 0 && lstSize.Count > 0)
                    {
                        foreach (ProColor item1 in lstColor)
                        {
                            BaseConnectionSql.Execute_Update_Insert<ProColor>("web", "ProColor_Create", item1);
                        }
                        foreach (ProSize item2 in lstSize)
                        {
                            BaseConnectionSql.Execute_Update_Insert<ProSize>("web", "ProSize_Create", item2);
                        }
                    }
                }

                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "UpdateProduct");
                return Json(null);
            }
        }

        public ActionResult DeleteProduct(List<Pro> lst, List<ProColor> lstColor, List<ProSize> lstSize)
        {
            try
            {
                foreach (Pro item in lst)
                {
                    BaseConnectionSql.Execute_Update_Insert<Pro>("web","Product_Delete", item);
                    foreach (ProColor item1 in lstColor)
                    {
                        BaseConnectionSql.Execute_Update_Insert<ProColor>("web", "ProColor_Delete", item1);
                    }
                    foreach (ProSize item2 in lstSize)
                    {
                        BaseConnectionSql.Execute_Update_Insert<ProSize>("web", "ProSize_Delete", item2);
                    }
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "DeleteProduct");
                return Json(null);
            }
        }
    }
}