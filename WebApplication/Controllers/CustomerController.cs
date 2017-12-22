using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_4.DAL;
using Task_4.BusinessLayer;
using Task_4;
using Task_4.BusinessLayer.DTO;
using PagedList;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
    {
        //EFUnitOfWork database = new EFUnitOfWork();

        // GET: Customer
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Task_4.BusinessLayer.Service service = new Service();

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
        // GET: /Student/Details/5

        public ViewResult Details(int id)
        {
            Service service = new Service();
            service.GetCustomer(id);
            return View(service);
        }
    }
}