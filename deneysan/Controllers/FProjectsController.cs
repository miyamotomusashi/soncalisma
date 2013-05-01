using deneysan_BLL.Project;
using System.Web.Mvc;

namespace deneysan.Controllers
{
    public class FProjectsController : Controller
    {
        //
        // GET: /FProjects/

        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {
            var news = ProjectManager.GetProjectListForFront(lang);
            return View(news);
        }

        public ActionResult ProjectContent(int id)
        {
            var project = ProjectManager.GetProjectById(id);
            return View(project);
        }

    }
}
