using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Przepisy.Models;

namespace Przepisy.Controllers
{
    public class RecipeController : Controller
    {
        // GET: Recipe
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AddNewReceip model)
        {
            if (model.File.ContentLength > (2 * 1024 * 1024))
            {
                ModelState.AddModelError("CustomError", "Plik musi być mniejszy niż 2MB");
            }
            if (!(model.File.ContentType=="image/jpeg"|| model.File.ContentType=="image/gif"))
            {
                ModelState.AddModelError("CustomError", "Plik jest w złym formacie!");
            }
            if (ModelState.IsValid)
            {
                var receipModel = new ReceipModel();
                using (ApplicationDBContext ctx = new ApplicationDBContext())
                {
//                    Mapper.CreateMap<AddNewReceip, ReceipModel>();
//                    receipModel = Mapper.Map<AddNewReceip, ReceipModel>(model);
                    receipModel.Title = model.Title;
                    receipModel.Content = model.Content;
                    receipModel.ShortDescription = model.ShortDescription;
                    receipModel.FileName = model.File.FileName;
                    receipModel.ImageSize = model.File.ContentLength;
                    byte[]data=new byte[model.File.ContentLength];
                    model.File.InputStream.Read(data, 0, model.File.ContentLength);
                    receipModel.ImageData = data;
                    ctx.Receipts.Add(receipModel);
                    ctx.SaveChanges();
                }
            ViewBag.Message = "Uzupelnij poprawnie pola formularza.";
            return RedirectToAction("Index");
            }
            return View(new AddNewReceip());
        }

        [HttpGet]
        public ActionResult List()
        {
            var listRecieps= new ListViewModel();

            using (ApplicationDBContext ctx = new ApplicationDBContext())
            {
                var receips = ctx.Receipts.Select(receip => new ItemOnList()
                {
                    ID = receip.ID,
                    Title = receip.Title,
                    ImageData = receip.ImageData,
                    ShortDescription = receip.ShortDescription
                }).ToList();

                listRecieps.ListViewModels = receips;
            }
            return View(listRecieps);
        }

        [HttpGet]
        public ActionResult ViewSelectedItem(int id)
        {
            var model = new ListItemModelView();
            using (ApplicationDBContext ctx = new ApplicationDBContext())
            {
                var items = ctx.Receipts.Single(rec => rec.ID == id);
                model.Title = items.Title;
                model.Content = items.Content;
                model.ID = items.ID;
                model.ImageData = items.ImageData;
                model.ShortDescription = items.ShortDescription;

            }
            return View(model);
        }

        public ActionResult test()
        {
            return View();
        }
    }
}