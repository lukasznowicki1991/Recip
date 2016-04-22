using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Przepisy.Models;
using Przepisy.Services;

namespace Przepisy.Controllers
{
    public class RecipController : Controller
    {
        private IRecipServices serv;

        public RecipController(IRecipServices servicess)
        {
            serv = servicess;
        }  
        // GET: Recip
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AddNewRecip model)
        {

            if (model.File.ContentLength > (2 * 1024 * 1024))
            {
                ModelState.AddModelError("CustomError", "Plik musi być mniejszy niż 2MB");
            }
            if (!(model.File.ContentType == "image/jpeg" || model.File.ContentType == "image/gif"))
            {
                ModelState.AddModelError("CustomError", "Plik jest w złym formacie!");
            }
            if (ModelState.IsValid)
            {
               serv.addNewRecip(model);
                ViewBag.Message = "Uzupelnij poprawnie pola formularza.";
                return RedirectToAction("Index");
            }
            return View(new AddNewRecip());
        }

        [HttpGet]
        public ActionResult List()
        {
            var listRecips = serv.ListToView();
            return View(listRecips);
        }

        [HttpGet]
        public ActionResult ViewSelectedItem(int id)
        {
            var model = serv.getItemById(id);
            return View(model);
        }

        public ActionResult test()
        {
            return View();
        }

    }
}