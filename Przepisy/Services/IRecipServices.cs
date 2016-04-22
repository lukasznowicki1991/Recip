using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przepisy.Models;

namespace Przepisy.Services
{
    public interface IRecipServices
    {
        void addNewRecip(AddNewRecip model);
        ListViewModel ListToView();
        ListItemModelView getItemById(int id);
        /*bool ValidateFile(AddNewRecip model);*/
    }
}
