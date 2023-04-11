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
    public class MenuController : Controller
    {
        public ActionResult Index()
        {
            //var list_level = DataAccess.DataAccessMenu.Menu_cboLevel();
            var list_level = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Menu_cboLevel");
            ViewBag.list_level = list_level;
            Session["list_level"] = list_level;

            return View();
        }

        public ActionResult Create(string Parent_ID, string capp = "DM")
        {
            if (Parent_ID != null && Parent_ID != "")
            {
                Session["Parent_ID"] = Parent_ID;
            }
            else
            {
                Session["Parent_ID"] = "-1";
            }
            //var list_lvDM = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Menu_cboLevel", capp);
            //ViewBag.list_lvDM = list_lvDM;
            var list_MN = BaseConnectionSql.ExecuteList_Helper<MenuModel>("web", "Menu_GetMN");
            ViewBag.list_MN = list_MN;
            var list_DM = BaseConnectionSql.ExecuteList_Helper<MenuModel>("web", "Menu_GetDM");
            ViewBag.list_DM = list_DM;
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
            if (list_detail[0].Parent_ID == "-1")
            {
                ViewBag.list_detail_lv = list_detail[0];
            }
            else
            {
            var list_detail_lv = BaseConnectionSql.ExecuteList_Helper<MenuModel>("Menu_detail", list_detail[0].Parent_ID);
            ViewBag.list_detail_lv = list_detail_lv[0];
            }
            return View();
        }

        public ActionResult getList(string capp)
        {
            //DataTable table = DataAccess.DataAccessMenu.Menu_getMenu();
            DataTable table = BaseConnectionSql.ExecuteTable_Helper("Menu_getMenu", capp);
            return PartialView("~/Views/Menu/Tables/__tblIndex.cshtml", table);
        }

        public ActionResult getNameUpdate(string Name)
        {
            try
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
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "getNameUpdate");
                return Json(null);
            }
        }
        public ActionResult getName(string Name)
        {
            try
            {
                if (Session["Parent_ID"].ToString() != "-1")
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

        public ActionResult CoppyToMenu(List<MenuModel> lst)
        {
            try
            {
                var sgrnlevel = "";
                var sgrnlevelcha = "";

                lst = BaseConnectionSql.ExecuteList_Helper<MenuModel>("web", "Menu_GetDM");
                sgrnlevel = lst[0].Level.Substring(0, lst[0].Level.Length - 5);
                foreach (var po in lst)
                {
                    MenuModel obj = new MenuModel();
                    var cd = BaseConnectionSql.ExecuteList_Helper<MenuModel>("web", "Menu_GetTop1");
                    int id = Int32.Parse(cd[0].ID.ToString());
                    int congid = id + 1;
                    var pr = Session["Parent_ID"].ToString();
                    var ab = BaseConnectionSql.ExecuteList_Helper<MenuModel>("Menu_detail", Session["Parent_ID"].ToString());
                    if (ab.Count > 0)
                    {
                        sgrnlevelcha = ab[0].Level;
                    }
                    else
                    {
                        sgrnlevelcha = "00001";
                    }
                    if (pr == "-1")
                    {
                        if (po.Parent_ID == "-1")
                        {
                            obj.Level = sgrnlevel + "00000";
                            obj.Parent_ID = "-1";
                            //po.ID = id.ToString();
                        }
                        else
                        {
                            obj.Level = sgrnlevel + po.Level;
                            obj.Parent_ID = id.ToString();
                            //po.ID = congid.ToString();
                        }
                    }
                    else
                    {
                        if (po.Parent_ID == "-1")
                        {
                            obj.Level = sgrnlevelcha + sgrnlevel + "00000";
                            obj.Parent_ID = ab[0].ID;
                            //po.ID = id.ToString();
                        }
                        else
                        {
                            obj.Level = sgrnlevelcha + sgrnlevel + po.Level;
                            obj.Parent_ID = id.ToString();
                            //po.ID = congid.ToString();
                        }
                    }
                    obj.Type = po.Type;
                    ;obj.Lang           = po.Lang
                    ;obj.Name           = po.Name
                    ;obj.Url_Name       = po.Url_Name
                    ;obj.Link           = ab[0].Link + "/" + po.Link
                    ;obj.Styleshow      = po.Styleshow
                    ;obj.Equals         = po.Equals
                    ;obj.Images         = po.Images
                    ;obj.Description    = po.Description
                    ;obj.Views          = po.Views
                    ;obj.ShowID         = po.ShowID
                    ;obj.Orders         = po.Orders
                    ;obj.News           = po.News
                    ;obj.page_Home      = po.page_Home
                    ;obj.Status         = po.Status
                    ;obj.Titleseo       = po.Titleseo
                    ;obj.Meta           = po.Meta
                    ;obj.Keyword        = po.Keyword
                    ;obj.Check_01       = po.Check_01
                    ;obj.Check_02       = po.Check_02
                    ;obj.Check_03       = po.Check_03
                    ;obj.Check_04       = po.Check_04
                    ;obj.Check_05       = po.Check_05
                    ;obj.Noidung1       = po.Noidung1
                    ;obj.Noidung2       = po.Noidung2
                    ;obj.Noidung3       = po.Noidung3
                    ;obj.Noidung4       = po.Noidung4
                    ;obj.Noidung5       = po.Noidung5
                    ;obj.Module         = po.Module
                    ;obj.TangName       = po.TangName
                    ;obj.Detail         = po.Detail
                    ;obj.Content = po.Content;
                    obj.capp = "MN";
                    obj.Create_Date = "";
                    BaseConnectionSql.Execute_Update_Insert<MenuModel>("web", "inserted_create", obj);
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "CoppyToMenu");
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
                    sgrnlevel = "00001";
                }
                if (pr == "-1")
                {
                    lst.Level = "00000";
                }
                else
                {
                    lst.Level = sgrnlevel + "00001";
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