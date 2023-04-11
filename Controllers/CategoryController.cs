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
using static WEB.Models.CategoryModel;

namespace WEB.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            //var getCode = BaseConnectionSql.ExecuteList_Helper<objCombox>("auto_code_size");
            //ViewBag.getCode = getCode[0];
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
            var getCode = BaseConnectionSql.ExecuteList_Helper<objCombox>("web", "test");
            DataTable table = BaseConnectionSql.ExecuteTable_Helper("web", "spCategories_GetByPK",null);
            return PartialView("~/Views/Category/Tables/__tblIndex.cshtml", table);
        }

        public ActionResult CreateCategory(CreateCate lst)
        {
            try
            {
                BaseConnectionSql.Execute_Update_Insert("web", "spCategories_Insert", lst);
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "CreateCategory");
                return Json(null);
            }
        }

        public ActionResult UpdateCategory(CreateCate lst)
        {
            try
            {
                BaseConnectionSql.Execute_Update_Insert("web", "spCategories_UpdateByPK", lst);
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "UpdateCategory");
                return Json(null);
            }
        }

        public ActionResult DeleteCategory(List<DeleteCate> lst)
        {
            try
            {
                foreach (var item in lst)
                {
                    BaseConnectionSql.Execute_Update_Insert("web", "spCategories_DeleteByPK", item);
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "DeleteCategory");
                return Json(null);
            }
        }
    }
}