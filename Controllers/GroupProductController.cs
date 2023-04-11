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
    public class GroupProductController : Controller
    {
        public ActionResult Index()
        {
            //var list_level = DataAccess.DataAccessMenu.Menu_cboLevel();
            var list_level = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Menu_cboLevel");
            ViewBag.list_level = list_level;
            Session["list_level"] = list_level;

            return View();
        }

        public ActionResult Create(string Parent_ID)
        {
            if (Parent_ID != null && Parent_ID != "")
            {
                Session["Parent_ID"] = Parent_ID;
            }
            else
            {
                Session["Parent_ID"] = "-1";
            }

            return View();
        }

        public ActionResult Update(string Parent_ID)
        {
            if (Parent_ID != null && Parent_ID != "")
            {
                Session["Parent_ID"] = Parent_ID;
            }
            else
            {
                Session["Parent_ID"] = "-1";
            }
            //var list_detail = DataAccess.DataAccessMenu.Menu_detail(ID);
            var list_detail = BaseConnectionSql.ExecuteList_Helper<MenuModel>("Menu_detail", Parent_ID);
            ViewBag.list_detail = list_detail[0];
            return View();
        }
        public ActionResult UpdateStatus(List<MenuModel> lst)
        {
            try
            {
                foreach (MenuModel po in lst)
                {
                    //var sgrnlevel = "00001";
                    BaseConnectionSql.Execute_Update_Insert<MenuModel>("web", "Menu_UpdateStatus", po);
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "UpdateStatus");
                return Json(null);
            }
        }
        public ActionResult UpdatePageHome(List<MenuModel> lst)
        {
            try
            {
                foreach (MenuModel po in lst)
                {
                    //var sgrnlevel = "00001";
                    BaseConnectionSql.Execute_Update_Insert<MenuModel>("web", "Menu_UpdatePageHome", po);
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "UpdatePageHome");
                return Json(null);
            }
        }
        public ActionResult getList(string capp)
        {
            //DataTable table = DataAccess.DataAccessMenu.Menu_getMenu();
            DataTable table = BaseConnectionSql.ExecuteTable_Helper("Menu_getMenu", capp);
            return PartialView("~/Views/GroupProduct/Tables/__tblIndex.cshtml", table);
        }

        public ActionResult getName(string Name)
        {
            try
            {
                var pr = Session["Parent_ID"].ToString();
                if (pr == "-1")
                {
                    var getName = BaseConnectionSql.ExecuteList_Helper<MenuModel>("Menu_getName", Name);
                    if (getName.Count > 0)
                    {
                        ViewBag.getName = getName[0];
                        return Json(new { code = 1, link = getName[0] });
                    }
                    else
                    {
                        return Json(0);
                    }
                }
                else
                {
                    var getName = BaseConnectionSql.ExecuteList_Helper<MenuModel>("Menu_getLink1", Name, Session["Parent_ID"].ToString());
                    if (getName.Count > 0)
                    {
                        ViewBag.getName = getName[0];
                        return Json(new { code = 2, link = getName[0] });
                    }
                    else
                    {
                        var getLink = BaseConnectionSql.ExecuteList_Helper<MenuModel>("Menu_getLink", Session["Parent_ID"].ToString());
                        return Json(new { code = 3, link = getLink[0] });
                    }
                }
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "getName");
                return Json(null);
            }
        }

        public ActionResult createMenu(MenuModel lst)
        {
            try
            {
                var sgrnlevel = "";
                //var ab = DataAccess.DataAccessMenu.Menu_detail(Session["Parent_ID"].ToString());
                var pr = Session["Parent_ID"].ToString();
                var ab = BaseConnectionSql.ExecuteList_Helper<MenuModel>("Menu_detail", Session["Parent_ID"].ToString());
                if (ab.Count > 0)
                {
                    sgrnlevel = ab[0].Level;
                }
                else
                {
                    sgrnlevel = "00000";
                }
                if (pr == "-1")
                {
                    lst.Level = "00000";
                }
                else
                {
                    lst.Level = sgrnlevel + "00000";
                }
                lst.Parent_ID = Session["Parent_ID"].ToString();
                //ParentID
                //DataAccess.DataAccessMenu.inserted_create(lst);
                BaseConnectionSql.Execute_Update_Insert<MenuModel>("web", "inserted_create", lst);
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "createMenu");
                return Json(null);
            }
        }

        public ActionResult UpdateParent(List<MenuModel> lst)
        {
            try
            {
                foreach (MenuModel po in lst)
                {
                    //var sgrnlevel = "00001";
                    BaseConnectionSql.Execute_Update_Insert<MenuModel>("web", "Menu_UpdateParent", po);
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "UpdateParent");
                return Json(null);
            }
        }

        public ActionResult UpdateMenu(MenuModel lst)
        {
            try
            {
                //var sgrnlevel = "00000Menu_update
                BaseConnectionSql.Execute_Update_Insert<MenuModel>("web", "Menu_update", lst);
                //DataAccess.DataAccessMenu.Menu_update(lst, ID);
                //lst.Level = sgrnlevel + "00000";
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "UpdateMenu");
                return Json(null);
            }
        }

        public ActionResult DeleteMenu(List<MenuModel> lst)
        {
            try
            {
                foreach (MenuModel ar in lst)
                {
                    DataAccess.DataAccessMenu.Menu_delete(ar);
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "DeleteMenu");
                return Json(null);
            }
        }

        public ActionResult __tblIndex()
        {
            var list_level = DataAccess.DataAccessMenu.Menu_cboLevel();
            ViewBag.list_level = list_level;
            return PartialView();
        }
    }
}