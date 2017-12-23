using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_4.DAL;
using Task_4.DAL.Models;
using WebApplication.Models;
using WebApplication.Models.DTO;

namespace WebApplication.Controllers
{
    public class ShopAssistantController : Controller
    {       
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Service service = new Service();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IdSortParm = sortOrder == "Id" ? "Id_desc" : "Id";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var shopAssistants = from shopAssistant in service.GetShopAssistants()
                            select shopAssistant;
            if (!String.IsNullOrEmpty(searchString))
            {
                shopAssistants = shopAssistants.Where(x => x.ShopAssistantName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    shopAssistants = shopAssistants.OrderByDescending(s => s.ShopAssistantName);
                    break;
                case "Id":
                    shopAssistants = shopAssistants.OrderBy(s => s.Id);
                    break;
                case "Id_desc":
                    shopAssistants = shopAssistants.OrderByDescending(s => s.Id);
                    break;
                default:  // Name ascending 
                    shopAssistants = shopAssistants.OrderBy(s => s.ShopAssistantName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(shopAssistants.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /ShopAssistant/Details/5

        public ViewResult Details(int id)
        {
            Service service = new Service();
            var shopAssistant = service.GetShopAssistant(id);
            return View(shopAssistant);
        }

        //
        // GET: ShopAssistant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ShopAssistant/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShopAssistantName")]ShopAssistantDTO shopAssistantDTO)
        {
            IUnitOfWork database = new EFUnitOfWork();

            var shopAssistant = new ShopAssistantDAL { ShopAssistantName = shopAssistantDTO.ShopAssistantName, Id = shopAssistantDTO.Id };

            try
            {
                if (ModelState.IsValid)
                {
                    database.ShopAssistants.Insert(shopAssistant);
                    database.ShopAssistants.Save();
                    return RedirectToAction("Index");
                }
            }

            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            return View(shopAssistantDTO);
        }

        //
        // GET: /ShopAssistant/Edit/5

        public ActionResult Edit(int id)
        {
            Service service = new Service();
            ShopAssistantDTO shopAssistan = service.GetShopAssistant(id);
            return View(shopAssistan);
        }

        //
        // POST: /ShopAssistant/Edit/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShopAssistantName")]ShopAssistantDTO shopAssistanDTO)
        {
            IUnitOfWork database = new EFUnitOfWork();

            var shopAssistan = new ShopAssistantDAL { ShopAssistantName = shopAssistanDTO.ShopAssistantName, Id = shopAssistanDTO.Id };

            try
            {
                if (ModelState.IsValid)
                {
                    database.ShopAssistants.Update(shopAssistan);
                    database.ShopAssistants.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(shopAssistanDTO);
        }

        //
        // GET: /ShopAssistant/Delete/5

        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            Service service = new Service();

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            ShopAssistantDTO shopAssistan = service.GetShopAssistant(id);
            return View(shopAssistan);
        }

        //
        // POST: /ShopAssistant/Delete/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Service service = new Service();
            IUnitOfWork database = new EFUnitOfWork();

            ShopAssistantDTO shopAssistanDTO = service.GetShopAssistant(id);
            var shopAssistan = new ShopAssistantDAL { ShopAssistantName = shopAssistanDTO.ShopAssistantName, Id = shopAssistanDTO.Id };

            try
            {
                database.ShopAssistants.Delete(id);
                database.ShopAssistants.Save();
            }

            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            IUnitOfWork database = new EFUnitOfWork();

            database.Dispose();
            base.Dispose(disposing);
        }
    }
}