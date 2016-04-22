using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.ModelBinding;
using Przepisy.Models;
using System.Web.Mvc;

namespace Przepisy.Services
{

    public class RecipServices : IRecipServices
    {
/*
        private ModelStateDictionary _modelState;

        public RecipServices(ModelStateDictionary dictionary)
        {
            _modelState = dictionary;
        }

        public bool ValidateFile(AddNewRecip modelToValidate)
        {
            if (modelToValidate.File.ContentLength < ValidateDefinitions.maxSizeOfFile)
                _modelState.AddModelError(ValidateDefinitions.nameOfField, ValidateDefinitions.sizeOfFile);
            if (!(modelToValidate.File.ContentType == ValidateDefinitions.firstOfType || modelToValidate.File.ContentType == ValidateDefinitions.secondOfType))
                _modelState.AddModelError(ValidateDefinitions.nameOfField,ValidateDefinitions.validFormatOfFile);
            return _modelState.IsValid;
        }
*/

        public void addNewRecip(AddNewRecip model)
        {

            var receipModel = new RecipModel();
            using (ApplicationDBContext ctx = new ApplicationDBContext())
            {
                receipModel.Title = model.Title;
                receipModel.Content = model.Content;
                receipModel.ShortDescription = model.ShortDescription;
                receipModel.FileName = model.File.FileName;
                receipModel.ImageSize = model.File.ContentLength;
                byte[] data = new byte[model.File.ContentLength];
                model.File.InputStream.Read(data, 0, model.File.ContentLength);
                receipModel.ImageData = data;
                ctx.Recipts.Add(receipModel);
                ctx.SaveChanges();
            }
        }

        public ListViewModel ListToView()
        {
            var listRecips = new ListViewModel();

            using (ApplicationDBContext ctx = new ApplicationDBContext())
            {
                var recips = ctx.Recipts.Select(receip => new ItemOnList()
                {
                    ID = receip.ID,
                    Title = receip.Title,
                    ImageData = receip.ImageData,
                    ShortDescription = receip.ShortDescription
                }).ToList();

                listRecips.ListViewModels = recips;
            }
            return listRecips;
        }

        public ListItemModelView getItemById(int id)
        {
            var model = new ListItemModelView();
            using (ApplicationDBContext ctx = new ApplicationDBContext())
            {
                var items = ctx.Recipts.Single(rec => rec.ID == id);
                model.Title = items.Title;
                model.Content = items.Content;
                model.ID = items.ID;
                model.ImageData = items.ImageData;
                model.ShortDescription = items.ShortDescription;

            }
            return model;
        }
         
    }

    public static class ValidateDefinitions
    {
        public const string sizeOfFile = "Plik musi być mniejszy niż 2MB";
        public const string validFormatOfFile = "Plik jest w nieobsługiwanym formacie";
        public const int maxSizeOfFile = 2*1024*1024;
        public const string nameOfField = "CustomError";
        public const string firstOfType = "image/jpeg";
        public const string secondOfType = "image/png";
    }
}