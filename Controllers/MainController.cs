using System.Web.Mvc;
using WEB.DataAccess;
using static WEB.Models.MainModel;

namespace WEB.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Header()
        {
            
            return PartialView();
        }

        public ActionResult Footer()
        {
            return PartialView();
        }

        public ActionResult Menu()
        {
            var list_detail = BaseConnectionSql.ExecuteList_Helper<objcheckbox>("web","spCategories_GetByPK", null);
            ViewBag.list_detail = list_detail;
            return PartialView();
        }
    }
}