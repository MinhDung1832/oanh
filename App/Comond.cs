using Lib.Utils.Package;
using System.Configuration;
using System.Linq;
using WEB.Models;

public class Comond
{
    public static string SetLanguage()
    {
        string language = "VIE";
        if (System.Web.HttpContext.Current.Session["lang"] != null)
        {
            return System.Web.HttpContext.Current.Session["lang"].ToString();
        }
        else
        {
            System.Web.HttpContext.Current.Session["lang"] = language;
            return System.Web.HttpContext.Current.Session["lang"].ToString();
        }
        return "VIE";
    }

    public static string Hienthihinhcay(string name, string treecode)
    {
        string chuoi = "<img src='/images/admin/icons8-documents-folder-32.png'  />";
        if (treecode.ToString().Trim().Length > 50)
        {
            chuoi = "<span style='margin-left:150px'><img src='/images/admin/icons8-down-right-32 (1).png'  /></span>";
        }
        else if (treecode.ToString().Trim().Length > 45)
        {
            chuoi = "<span style='margin-left:135px'><img src='/images/admin/icons8-down-right-32 (1).png'></span>";
        }
        else if (treecode.ToString().Trim().Length > 40)
        {
            chuoi = "<span style='margin-left:120px'><img src='/images/admin/icons8-down-right-32 (1).png'></span>";
        }
        else if (treecode.ToString().Trim().Length > 35)
        {
            chuoi = "<span style='margin-left:105px'><img src='/images/admin/icons8-down-right-32 (1).png'></span>";
        }
        else if (treecode.ToString().Trim().Length > 30)
        {
            chuoi = "<span style='margin-left:90px'><img src='/images/admin/icons8-down-right-32 (1).png'></span>";
        }
        else if (treecode.ToString().Trim().Length > 25)
        {
            chuoi = "<span style='margin-left:75px'><img src='/images/admin/icons8-down-right-32 (1).png'></span>";
        }
        else if (treecode.ToString().Trim().Length > 20)
        {
            chuoi = "<span style='margin-left:60px'><img src='/images/admin/icons8-down-right-32 (1).png'><span>";
        }
        else if (treecode.ToString().Trim().Length > 15)
        {
            chuoi = "<span style='margin-left:45px'><img src='/images/admin/icons8-down-right-32 (1).png'></span>";
        }
        else if (treecode.ToString().Trim().Length > 10)
        {
            chuoi = "<span style='margin-left:30px'><img src='/images/admin/icons8-down-right-32 (1).png'></span>";
        }
        else if (treecode.ToString().Trim().Length > 5)
        {
            chuoi = "<span style='margin-left:15px'><img src='/images/admin/icons8-down-right-32 (1).png'></span>";
        }
        else
        {
            chuoi = chuoi;
        }
        return chuoi = chuoi + "&nbsp;  " + name;
    }

    public static string LoadSelect(string Parent_ID, string capp)
    {
        string str = "";
        var dt = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Menu_cboLevel", capp);
        if (dt.Count > 0)
        {
            for (int i = 0; i < dt.Count; i++)
            {
                string temp = "";
                for (int j = 0; j < dt[i].Level.Length / 5 - 1; j++)
                {
                    temp += "--";
                }
                if (dt[i].Code.ToString().Trim() == Parent_ID)
                {
                    str += "<option selected=\"selected\" value=\"" + dt[i].Code.ToString().Trim() + " - " + dt[i].Level.ToString().Trim() + "\" >" + temp + dt[i].Name.ToString() + "</option>";
                }
                else
                {
                    str += "<option value=\"" + dt[i].Code.ToString().Trim() + " - " + dt[i].Level.ToString().Trim() + "\" >" + temp + dt[i].Name.ToString() + "</option>";
                }
            }
        }
        return str;
    }

    public static string LoadSelectModule(string capp, string id)
    {
        string str = "";
        var dt = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Menu_cboLevel", capp);
        if (id != "-1")
        {
            var ab = SqlHelper.ExecuteList<Pro>(ConfigurationManager.AppSettings.Get("web"), "Product_Detail", int.Parse(id));
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    string temp = "";
                    for (int j = 0; j < dt[i].Level.Length / 5 - 1; j++)
                    {
                        temp += "--";
                    }
                    if (dt[i].Code.ToString() == ab[0].IdGroupPro.ToString())
                    {
                        str += "<option selected=\"selected\" value=\"" + dt[i].Code.ToString() + " - " + dt[i].Link.ToString() + "\" >" + temp + dt[i].Name.ToString() + "</option>";
                    }
                    else
                    {
                        str += "<option value=\"" + dt[i].Code.ToString() + " - " + dt[i].Link.ToString() + "\" >" + temp + dt[i].Name.ToString() + "</option>";
                    }
                }
            }
        }
        else
        {
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    string temp = "";
                    for (int j = 0; j < dt[i].Level.Length / 5 - 1; j++)
                    {
                        temp += "--";
                    }

                    str += "<option value=\"" + dt[i].Code.ToString() + " - " + dt[i].Link.ToString() + "\" >" + temp + dt[i].Name.ToString() + "</option>";
                }
            }
        }
        return str;
    }

    public static string CheckBoxColor(string id)
    {
        string chuoi = "";
        var dt = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Color_cbo");
        if (id != "")
        {
            //get Code product
            var cd = SqlHelper.ExecuteList<Pro>(ConfigurationManager.AppSettings.Get("web"), "Product_Detail", id);
            // get list ProColor
            var gh = SqlHelper.ExecuteList<ProColor>(ConfigurationManager.AppSettings.Get("web"), "ProColor_GetDetail", cd[0].Code);
            var e = gh.Select(s => s.Color_ID).ToArray();
            //var ef = SqlHelper.ExecuteList<ProColor>(ConfigurationManager.AppSettings.Get("web"), "ProCol_Col", cd[0].Code);
            //get list Color
            var ab = SqlHelper.ExecuteList<ColorModel>(ConfigurationManager.AppSettings.Get("web"), "Color_GetList");
            int lengthColor = ab.Count;
            if (dt.Count > 0)
            {
                if (lengthColor == gh.Count)
                {
                    chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                        "<input type = \"checkbox\" id=\"colorAll\" checked name=\"colorAll\" value=\"colorAll\"  onclick=\"js_checkboxAll();\">" +
                        "<span class=\"input-span\"></span>" + "Tất cả" +
                        "</label>";
                    for (int i = 0; i < dt.Count; i++)
                    {
                        chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                        "<input type = \"checkbox\" id=\"color\" name=\"color\" value=\"" + dt[i].Code.ToString() + " - " + dt[i].ID.ToString() + "\" onclick=\"js_checkbox();\">" +
                        "<span class=\"input-span\"></span>" + "" + dt[i].Name.ToString() + "" +
                        "</label>";
                    }
                }
                else
                {
                    chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                        "<input type = \"checkbox\" id=\"colorAll\" name=\"colorAll\" value=\"colorAll\" onclick=\"js_checkboxAll();\">" +
                        "<span class=\"input-span\"></span>" + "Tất cả" +
                        "</label>";
                    for (int i = 0; i < dt.Count; i++)
                    {
                        var cID = dt[i].Code;
                        if (e.Contains(cID))
                        {
                            chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                        "<input type = \"checkbox\" checked id=\"color\" name=\"color\" value=\"" + dt[i].Code.ToString() + " - " + dt[i].ID.ToString() + "\" onclick=\"js_checkbox();\">" +
                        "<span class=\"input-span\"></span>" + "" + dt[i].Name.ToString() + "" +
                        "</label>";
                        }
                        else
                        {
                            chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                            "<input type = \"checkbox\" id=\"color\" name=\"color\" value=\"" + dt[i].Code.ToString() + " - " + dt[i].ID.ToString() + "\" onclick=\"js_checkbox();\">" +
                            "<span class=\"input-span\"></span>" + "" + dt[i].Name.ToString() + "" +
                            "</label>";
                        }
                    }
                }
            }
        }
        else
        {
            if (dt.Count > 0)
            {
                chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                    "<input type = \"checkbox\" id=\"colorAll\" name=\"colorAll\" value=\"colorAll\" onclick=\"js_checkboxAll();\">" +
                    "<span class=\"input-span\"></span>" + "Tất cả" +
                    "</label>";
                for (int i = 0; i < dt.Count; i++)
                {
                    chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                    "<input type = \"checkbox\" id=\"color\" name=\"color\" value=\"" + dt[i].Code.ToString() + " - " + dt[i].ID.ToString() + "\" onclick=\"js_checkbox();\">" +
                    "<span class=\"input-span\"></span>" + "" + dt[i].Name.ToString() + "" +
                    "</label>";
                }
            }
        }
        return chuoi;
    }

    public static string CheckBoxSize(string id)
    {
        string chuoi = "";
        var dt = SqlHelper.ExecuteList<objCombox>(ConfigurationManager.AppSettings.Get("web"), "Size_cbo");
        if (id != "")
        {
            //get Code product
            var cd = SqlHelper.ExecuteList<Pro>(ConfigurationManager.AppSettings.Get("web"), "Product_Detail", id);
            // get list ProColor
            var gh = SqlHelper.ExecuteList<ProSize>(ConfigurationManager.AppSettings.Get("web"), "ProSize_GetDetail", cd[0].Code);
            var e = gh.Select(s => s.Size_ID).ToArray();
            //get list Color
            var ab = SqlHelper.ExecuteList<SizeModel>(ConfigurationManager.AppSettings.Get("web"), "Size_GetList");
            int lengthSize = ab.Count;
            if (dt.Count > 0)
            {
                if (lengthSize == gh.Count)
                {
                    chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                        "<input type = \"checkbox\" id=\"sizeAll\" checked name=\"sizeAll\" value=\"sizeAll\" onclick=\"js_checkboxAll();\">" +
                        "<span class=\"input-span\"></span>" + "Tất cả" +
                        "</label>";
                    for (int i = 0; i < dt.Count; i++)
                    {
                        chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                        "<input type = \"checkbox\" id=\"size\" name=\"size\" value=\"" + dt[i].Code.ToString() + " - " + dt[i].ID.ToString() + "\" onclick=\"js_checkbox();\">" +
                        "<span class=\"input-span\"></span>" + "" + dt[i].Name.ToString() + "" +
                        "</label>";
                    }
                }
                else
                {
                    chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                    "<input type = \"checkbox\" id=\"sizeAll\" name=\"sizeAll\" value=\"sizeAll\" onclick=\"js_checkboxAll();\">" +
                    "<span class=\"input-span\"></span>" + "Tất cả" +
                    "</label>";
                    for (int i = 0; i < dt.Count; i++)
                    {
                        var cID = dt[i].Code;
                        if (e.Contains(cID))
                        {
                            chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                    "<input type = \"checkbox\" checked id=\"size\" name=\"size\" value=\"" + dt[i].Code.ToString() + " - " + dt[i].ID.ToString() + "\" onclick=\"js_checkbox();\">" +
                    "<span class=\"input-span\"></span>" + "" + dt[i].Name.ToString() + "" +
                    "</label>";
                        }
                        else
                        {
                            chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                            "<input type = \"checkbox\" id=\"size\" name=\"size\" value=\"" + dt[i].Code.ToString() + " - " + dt[i].ID.ToString() + "\" onclick=\"js_checkbox();\">" +
                            "<span class=\"input-span\"></span>" + "" + dt[i].Name.ToString() + "" +
                            "</label>";
                        }
                    }
                }
            }
        }
        else
        {
            if (dt.Count > 0)
            {
                chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                    "<input type = \"checkbox\" id=\"sizeAll\" name=\"sizeAll\" value=\"sizeAll\" onclick=\"js_checkboxAll();\">" +
                    "<span class=\"input-span\"></span>" + "Tất cả" +
                    "</label>";
                for (int i = 0; i < dt.Count; i++)
                {
                    chuoi += "<label class=\"ui-checkbox ui-checkbox-primary\" style =\"margin-right: 15px\">" +
                    "<input type = \"checkbox\" id=\"size\" name=\"size\" value=\"" + dt[i].Code.ToString() + " - " + dt[i].ID.ToString() + "\" onclick=\"js_checkbox();\">" +
                    "<span class=\"input-span\"></span>" + "" + dt[i].Name.ToString() + "" +
                    "</label>";
                }
            }
        }
        return chuoi;
    }
}