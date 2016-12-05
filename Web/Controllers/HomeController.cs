using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private static IEnumerable<Code_t> code;
        public IEnumerable<Code_t> GetCode() {
      
                CodeSampleEntities db = new CodeSampleEntities();
     


            return db.Code_t.ToList();
        }

        public ActionResult Index(string keyword)
        {
        
            if (keyword != null)
            {
                CodeSampleEntities db = new CodeSampleEntities();
                db.Search_t.Add(new Search_t() {
                    Search = keyword
                });
                db.SaveChanges();
                ViewBag.search = keyword;
                ViewBag.data = GetCode().Where(p => p.Tags.Contains(keyword) || p.Title.IndexOf(keyword) != -1 || p.SummaryBox.IndexOf(keyword) != -1).ToList();
            }
            else {
                ViewBag.search = "";
                ViewBag.data = GetCode().Take(100).ToList(); 
            }

            return View();
        }

        public ActionResult hot()
        {
                ViewBag.search = "";
 
                ViewBag.data = GetCode().Where(p=>p.Rating.HasValue||p.Viewer.HasValue).OrderByDescending(p=>p.Rating).OrderByDescending(p=>p.Viewer).Take(100).ToList();

            return View("index");
        }
        public ActionResult Random()
        {
            ViewBag.search = "";
            ViewBag.data = GetCode().OrderBy(x => Guid.NewGuid()).Take(10).ToList();
            return View("index");
        }

        public ActionResult Detail(int id,string href)
        {
            CodeSampleEntities db = new CodeSampleEntities();
            var result= db.Code_t.First(p => p.Id == id);
            result.Viewer= result.Viewer.HasValue?++result.Viewer:1;
            db.SaveChanges();
            return Redirect(href);
        }
        public JsonResult Rating(int id) {
            CodeSampleEntities db = new CodeSampleEntities();
            var result = db.Code_t.First(p => p.Id == id);
            result.Rating = result.Rating.HasValue ? ++result.Rating : 1;
            db.SaveChanges();
            return new JsonResult() { Data="成功"};
        }
 
    }
}