using System.Data;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class ListGroupController : Controller
    {
        // GET: ListGroup
        public ActionResult Index()
        {
            var list_level = DataAccess.DataAccessListGroup.LG_cboLevel();
            ViewBag.list_level = list_level;
            Session["list_level_lg"] = list_level;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult getList()
        {
            DataTable table = DataAccess.DataAccessListGroup.LG_getList();

            return PartialView("~/Views/ListGruop/Tables/__tblIndex.cshtml", table);
        }
    }
}