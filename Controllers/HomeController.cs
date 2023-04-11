using System.Web.Mvc;
using WEB.DataAccess;
using static WEB.Models.MainModel;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {

        private string lang = Comond.SetLanguage();

        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["lang"] != null)
            {
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["lang"] = this.lang;
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            var list_detail = BaseConnectionSql.ExecuteList_Helper<objcheckbox>("web", "spCategories_GetByPK", null);
            ViewBag.list_detail = list_detail;
            return View();
        }

        public ActionResult Language(string lang)
        {
            System.Web.HttpContext.Current.Session["lang"] = lang;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}