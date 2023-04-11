using Newtonsoft.Json;
using ProductAllTool.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.DataAccess;
using WEB.Models;
using static WEB.Models.MainModel;

namespace WEB.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            var list_cate = BaseConnectionSql.ExecuteList_Helper<objcheckbox>("web", "spCategories_GetByPK", null);
            ViewBag.list_cate = list_cate;
            var list_size = BaseConnectionSql.ExecuteList_Helper<objcheckbox>("web", "sp_Sizes_GetAll");
            ViewBag.list_size = list_size;
            var list_detail = BaseConnectionSql.ExecuteList_Helper<Product>("web", "sp_Products_GetAll", null, null, null);
            ViewBag.list_detail = list_detail;
            return View();
        }
        public ActionResult ProductDetail(int id)
        {
            var list_detail = BaseConnectionSql.ExecuteList_Helper<Product>("web", "sp_Products_GetAll", id, null, null);
            ViewBag.list_detail = list_detail[0];
            return View();
        }

        public ActionResult AddToCart(AddToCart lst)
        {
            try
            {
                BaseConnectionSql.Execute_Update_Insert("web", "sp_AddToCart", lst);
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "AddToCart");
                return Json(null);
            }
        }
    }
}