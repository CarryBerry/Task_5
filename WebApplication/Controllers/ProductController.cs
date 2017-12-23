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
    public class ProductController : Controller
    {        
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Service service = new Service();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IdSortParm = sortOrder == "Id" ? "Id_desc" : "Id";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }

            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var products = from product in service.GetProducts()
                            select product;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.ProductName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.ProductName);
                    break;
                case "Id":
                    products = products.OrderBy(s => s.Id);
                    break;
                case "Id_desc":
                    products = products.OrderByDescending(s => s.Id);
                    break;
                case "Price":
                    products = products.OrderBy(s => s.ProductPrice);
                    break;
                case "Price_desc":
                    products = products.OrderByDescending(s => s.ProductPrice);
                    break;
                default:  // Name ascending 
                    products = products.OrderBy(s => s.ProductName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Customer/Details/5

        public ViewResult Details(int id)
        {
            Service service = new Service();
            var product = service.GetProduct(id);
            return View(product);
        }

        //// GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductName, ProductPrice")]ProductDTO productDTO)
        {
            IUnitOfWork database = new EFUnitOfWork();

            var product = new ProductDAL { Id = productDTO.Id, ProductName = productDTO.ProductName, ProductPrice = productDTO.ProductPrice};

            try
            {
                if (ModelState.IsValid)
                {
                    database.Products.Insert(product);
                    database.Products.Save();
                    return RedirectToAction("Index");
                }
            }

            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            return View(productDTO);
        }


        //GET: /Customer/Edit/5

        public ActionResult Edit(int id)
        {
            Service service = new Service();
            ProductDTO product = service.GetProduct(id);
            return View(product);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductName, ProductPrice")]ProductDTO productDTO)
        {
            IUnitOfWork database = new EFUnitOfWork();

            var product = new ProductDAL { Id = productDTO.Id, ProductName = productDTO.ProductName,  ProductPrice = productDTO.ProductPrice };

            try
            {
                if (ModelState.IsValid)
                {
                    database.Products.Update(product);
                    database.Products.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(productDTO);
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            Service service = new Service();

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            ProductDTO product = service.GetProduct(id);
            return View(product);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Service service = new Service();
            IUnitOfWork database = new EFUnitOfWork();

            ProductDTO productDTO = service.GetProduct(id);
            var product = new ProductDAL { ProductName = productDTO.ProductName, Id = productDTO.Id, ProductPrice = productDTO.ProductPrice };

            try
            {
                database.Products.Delete(id);
                database.Products.Save();
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