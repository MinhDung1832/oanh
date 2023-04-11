using Newtonsoft.Json;
using ProductAllTool.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WEB.Models;

namespace WEB.DataAccess
{
    public class DataAccessListGroup
    {
        private static string web = ConfigurationManager.AppSettings.Get("web");

        public static DataTable LG_getList()
        {
            DataSet ds = new DataSet();
            try
            {
                using (var con = new SqlConnection(web))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("LG_getList", con);
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
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "LG_getList");
                return ds.Tables[0];
            }
        }

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

        public static List<objCombox> LG_cboLevel()
        {
            List<objCombox> it_r = new List<objCombox>();
            using (var con = new SqlConnection(web))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("LG_cboLevel", con);
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
                    LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "LG_cboLevel");
                    return it_r;
                }
            }
        }
    }
}