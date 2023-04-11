using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class NgonNgu
{
    public static string label(string id)
    {
        string language = Comond.SetLanguage();
        if (System.Web.HttpContext.Current.Session["lang"] != null)
        {
            language = System.Web.HttpContext.Current.Session["lang"].ToString();
        }
        else
        {
            System.Web.HttpContext.Current.Session["lang"] = language;
            language = System.Web.HttpContext.Current.Session["lang"].ToString();
        }
        return GetLabel(id, language);
    }

    private static bool CheckLanguage(string id, Hashtable hslang)
    {
        return hslang.Contains(id);
    }

    public static string GetLabel(string id, string fld)
    {
        if (fld.Trim().Length < 1)
        {
            fld = Comond.SetLanguage();
        }
        id = id.Trim().ToLower();
        Hashtable hslang = new Hashtable();
        hslang = LoadLanguageList(fld);
        if (CheckLanguage(id, hslang).Equals(true))
        {
            return hslang[id].ToString();
        }
        return "";
    }

    public static Hashtable LoadLanguageList(string lang)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "/Temp/" + lang + ".xml");
        Hashtable htblang = new Hashtable();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            htblang.Add(ds.Tables[0].Rows[i]["ID"].ToString().Trim().ToLower(), ds.Tables[0].Rows[i]["VALUE"].ToString());
        }
        return htblang;
    }
}