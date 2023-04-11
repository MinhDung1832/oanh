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
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getList()
        {
            DataTable table = BaseConnectionSql.ExecuteTable_Helper("web", "sp_CartItem_getAll");
            return PartialView("~/Views/Cart/Tables/__tblIndex.cshtml", table);
        }

        public ActionResult UpdateCartItem(List<update> lst)
        {
            try
            {
                foreach (var item in lst)
                {
                    var it = new update
                    {
                        ID = item.ID,
                        Quantity = item.Quantity
                    };
                    BaseConnectionSql.Execute_Update_Insert("web", "sp_CartItem_UpdateQty", it);
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "UpdateCartItem");
                return Json(null);
            }
        }
        public ActionResult DeleteCartItem(List<delete> lst)
        {
            try
            {
                bool ischeck = false;
                foreach (var item in lst)
                {

                    var a = BaseConnectionSql.Execute_Update_Insert("web", "sp_CartItem_Delete", item);
                    if (a)
                    {
                        ischeck = true;
                    }
                }
                if (ischeck = true)
                {
                    return Json(1);
                }
                else
                {
                    return Json(2);
                }
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "DeleteCartItem");
                return Json(null);
            }
        }

    }
}