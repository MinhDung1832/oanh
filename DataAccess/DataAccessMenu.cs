using Newtonsoft.Json;
using ProductAllTool.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WEB.Models;

namespace WEB.DataAccess
{
    public class DataAccessMenu
    {
        private static string web = ConfigurationManager.AppSettings.Get("web");

        public static DataTable Menu_getMenu()
        {
            DataSet ds = new DataSet();
            try
            {
                using (var con = new SqlConnection(web))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Menu_getMenu", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 30000;
                    //cmd.Parameters.Add(new SqlParameter("NCC", NCC));
                    //cmd.Parameters.Add(new SqlParameter("TenHang", TenHang));
                    //cmd.Parameters.Add(new SqlParameter("TrangThai", Int32.Parse(TrangThai)));
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    { sda.Fill(ds); }

                    con.Close();
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Menu_getMenu");
                return ds.Tables[0];
            }
        }

        public static List<LanguagesModel> Lang_get()
        {
            List<LanguagesModel> it_r = new List<LanguagesModel>();

            using (var con = new SqlConnection(web))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("Lang_get", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("Code", Code));

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        LanguagesModel it = new LanguagesModel
                        {
                            LanguageCultureName = reader["LanguageCultureName"].ToString().Trim(),
                            LanguageFullName = reader["LanguageFullName"].ToString().Trim(),
                        };

                        it_r.Add(it);
                    }
                    con.Close();

                    return it_r;
                }
                catch (Exception ex)
                {
                    con.Close();
                    LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Lang_get");
                    return it_r;
                }
            }
        }

        //public static List<Product> Menu_Autocode()
        //{
        //    List<Product> it_r = new List<Product>();

        //    using (var con = new SqlConnection(web))
        //    {
        //        con.Open();
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand("Menu_Autocode", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            //cmd.Parameters.Add(new SqlParameter("Code", Code));

        //            var reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                Product it = new Product
        //                {
        //                    Code = reader["Code"].ToString().Trim(),
        //                };

        //                it_r.Add(it);
        //            }
        //            con.Close();

        //            return it_r;
        //        }
        //        catch (Exception ex)
        //        {
        //            con.Close();
        //            LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Menu_Autocode");
        //            return it_r;
        //        }
        //    }
        //}

        public static bool Menu_delete(MenuModel it)
        {
            using (var con = new SqlConnection(web))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("Menu_delete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("ID", Int32.Parse(it.ID)));
                    var reader = cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    con.Close();
                    LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Menu_delete");
                    return false;
                }
            }
        }

        public static bool Menu_UpdateParent(MenuModel it)
        {
            using (var con = new SqlConnection(web))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("Menu_UpdateParent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("ID", Int32.Parse(it.ID)));
                    cmd.Parameters.Add(new SqlParameter("Level", it.Level));
                    cmd.Parameters.Add(new SqlParameter("Parent_ID", Int32.Parse(it.Parent_ID)));
                    var reader = cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    con.Close();
                    LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Menu_UpdateParent");
                    return false;
                }
            }
        }

        public static bool Menu_update(MenuModel it, int ID)
        {
            using (var con = new SqlConnection(web))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("Menu_update", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("ID", ID));
                    cmd.Parameters.Add(new SqlParameter("Name", it.Name));
                    cmd.Parameters.Add(new SqlParameter("Styleshow", it.Styleshow));
                    cmd.Parameters.Add(new SqlParameter("Orders", Int32.Parse(it.Orders)));
                    var reader = cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    con.Close();
                    LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Menu_update");
                    return false;
                }
            }
        }

        public static bool inserted_create(MenuModel it)
        {
            using (var con = new SqlConnection(web))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("inserted_create", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Name", it.Name));
                    cmd.Parameters.Add(new SqlParameter("Styleshow", it.Styleshow));
                    cmd.Parameters.Add(new SqlParameter("Level", it.Level));
                    cmd.Parameters.Add(new SqlParameter("capp", it.capp));
                    cmd.Parameters.Add(new SqlParameter("Parent_ID", Int32.Parse(it.Parent_ID)));
                    cmd.Parameters.Add(new SqlParameter("Orders", Int32.Parse(it.Orders)));
                    cmd.Parameters.Add(new SqlParameter("Type", Int32.Parse(it.Type)));
                    cmd.Parameters.Add(new SqlParameter("Images", it.Images));
                    cmd.Parameters.Add(new SqlParameter("Url_Name", it.Url_Name));
                    cmd.Parameters.Add(new SqlParameter("Link", it.Link));
                    cmd.Parameters.Add(new SqlParameter("Equals", Int32.Parse(it.Equals)));
                    cmd.Parameters.Add(new SqlParameter("Description", it.Description));
                    cmd.Parameters.Add(new SqlParameter("Views", Int32.Parse(it.Views)));
                    cmd.Parameters.Add(new SqlParameter("ShowID", Int32.Parse(it.ShowID)));
                    cmd.Parameters.Add(new SqlParameter("News", Int32.Parse(it.News)));
                    cmd.Parameters.Add(new SqlParameter("page_Home", Int32.Parse(it.page_Home)));
                    cmd.Parameters.Add(new SqlParameter("Status", Int32.Parse(it.Status)));
                    cmd.Parameters.Add(new SqlParameter("Titleseo", it.Titleseo));
                    cmd.Parameters.Add(new SqlParameter("Meta", it.Meta));
                    cmd.Parameters.Add(new SqlParameter("Keyword", it.Keyword));
                    cmd.Parameters.Add(new SqlParameter("Check_01", Int32.Parse(it.Check_01)));
                    cmd.Parameters.Add(new SqlParameter("Check_02", Int32.Parse(it.Check_02)));
                    cmd.Parameters.Add(new SqlParameter("Check_03", Int32.Parse(it.Check_03)));
                    cmd.Parameters.Add(new SqlParameter("Check_04", Int32.Parse(it.Check_04)));
                    cmd.Parameters.Add(new SqlParameter("Check_05", Int32.Parse(it.Check_05)));
                    cmd.Parameters.Add(new SqlParameter("Noidung1", it.Noidung1));
                    cmd.Parameters.Add(new SqlParameter("Noidung2", it.Noidung2));
                    cmd.Parameters.Add(new SqlParameter("Noidung3", it.Noidung3));
                    cmd.Parameters.Add(new SqlParameter("Noidung4", it.Noidung4));
                    cmd.Parameters.Add(new SqlParameter("Noidung5", it.Noidung5));
                    cmd.Parameters.Add(new SqlParameter("Module", Int32.Parse(it.Module)));
                    cmd.Parameters.Add(new SqlParameter("TangName", it.TangName));
                    cmd.Parameters.Add(new SqlParameter("Lang", it.Lang));
                    cmd.Parameters.Add(new SqlParameter("Create_Date", it.Create_Date));
                    var reader = cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    con.Close();
                    LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "inserted_create");
                    return false;
                }
            }
        }

        public static List<MenuModel> Menu_detail(string ID)
        {
            List<MenuModel> it_r = new List<MenuModel>();
            using (var con = new SqlConnection(web))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("Menu_detail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("ID", Int32.Parse(ID)));

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MenuModel it = new MenuModel
                        {
                            ID = reader["ID"].ToString(),
                            Name = reader["Name"].ToString(),
                            Styleshow = reader["Styleshow"].ToString(),
                            Orders = reader["Orders"].ToString(),
                            Level = reader["Level"].ToString(),
                            Url_Name = reader["Url_Name"].ToString(),
                            Link = reader["Link"].ToString(),
                            Images = reader["Images"].ToString(),
                            Noidung1 = reader["Noidung1"].ToString(),
                            Titleseo = reader["Titleseo"].ToString(),
                            Description = reader["Description"].ToString(),
                            Keyword = reader["Keyword"].ToString(),
                        };
                        it_r.Add(it);
                    }
                    con.Close();
                    return it_r;
                }
                catch (Exception ex)
                {
                    con.Close();
                    LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Menu_detail");
                    return it_r;
                }
            }
        }

        public static List<objCombox> Menu_cboLevel()
        {
            List<objCombox> it_r = new List<objCombox>();
            using (var con = new SqlConnection(web))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("Menu_cboLevel", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        objCombox it = new objCombox
                        {
                            Code = reader["Code"].ToString(),
                            Name = reader["Name"].ToString(),
                            Level = reader["Level"].ToString()
                        };
                        it_r.Add(it);
                    }
                    con.Close();
                    return it_r;
                }
                catch (Exception ex)
                {
                    con.Close();
                    LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Menu_cboLevel");
                    return it_r;
                }
            }
        }
    }
}