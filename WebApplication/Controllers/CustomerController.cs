using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_4.DAL;
using WebApplication.Models;
using Task_4;
using WebApplication.Models.DTO;
using PagedList;
using Task_4.DAL.Models;
using AutoMapper;
using System.Data;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
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

            var customers = from customer in service.GetCustomers()
                           select customer;
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(x => x.CustomerName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(s => s.CustomerName);
                    break;
                case "Id":
                    customers = customers.OrderBy(s => s.Id);
                    break;
                case "Id_desc":
                    customers = customers.OrderByDescending(s => s.Id);
                    break;
                default:  // Name ascending 
                    customers = customers.OrderBy(s => s.CustomerName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(customers.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Customer/Details/5
        public ViewResult Details(int id)
        {
            Service service = new Service();
            var customer = service.GetCustomer(id);
            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerName")]CustomerDTO customerDTO)
        {
            IUnitOfWork database = new EFUnitOfWork();

            var customer = new CustomerDAL {CustomerName = customerDTO.CustomerName, Id = customerDTO.Id };

            try
            {
                if (ModelState.IsValid)
                {
                    database.Customers.Insert(customer);
                    database.Customers.Save();
                    return RedirectToAction("Index");
                }
            }

            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            return View(customerDTO);
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(int id)
        {
            Service service = new Service();
            CustomerDTO customer = service.GetCustomer(id);
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, CustomerName")]CustomerDTO customerDTO)
        {
            IUnitOfWork database = new EFUnitOfWork();

            var customer = new CustomerDAL { CustomerName = customerDTO.CustomerName, Id = customerDTO.Id };

            try
            {
                if (ModelState.IsValid)
                {
                    database.Customers.Update(customer);
                    database.Customers.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(customerDTO);
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

            CustomerDTO customer = service.GetCustomer(id);
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            IUnitOfWork database = new EFUnitOfWork();

            try
            {
                database.Customers.Delete(id);
                database.Customers.Save();
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