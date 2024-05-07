using ControlsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ControlsDemo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new MijViewModel
            {
                DepartmentItems = GetDepartmentItems(),
                CatItems = GetCatItems()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult SubmitForm(MijViewModel model) 
        {
			model.DepartmentItems = GetDepartmentItems();

			if (ModelState.IsValid)//para tener la validacion
			{
				var selectedDepartment = model.DepartmentItems.FirstOrDefault(x => x.Value == model.SelectedDepartmentId.ToString());
				model.SelectedDepartmentText = selectedDepartment?.Text ?? "Selecteer een afdeling";
			}
            if (model.Cat.IsActive) 
            {
                ViewBag.IsActive = "Ok ckeckbox";
            }
            else
			{
				ViewBag.IsActive = "Not Ok";
			}

			model.CatItems = GetCatItems();
            var selectedCatId = model.CatItems.FirstOrDefault(c => c.Value == model.SelectedCatId.ToString());
            model.SelectedCatText = selectedCatId?.Text ?? string.Empty;
            ViewBag.selectedCatId = selectedCatId;

            return View("Index",model);
        }

        private List<SelectListItem> GetDepartmentItems()//is zoals een DB
        {
            var itemsFromDB = GetItems().ToList();

            //convert to SelectListItem
            var selectListItems = itemsFromDB.Select(x => new SelectListItem
            {
                Value = x.Value,
                Text = x.Text
            }).ToList();
            return selectListItems;
        }

		private List<SelectListItem> GetCatItems()//is zoals een DB
		{
			var catItemsFromDB = GetItems().ToList();

			//convert to SelectListItem
			var selectCatListItems = catItemsFromDB.Select(x => new SelectListItem
			{
				Value = x.Value,
				Text = x.Text
			}).ToList();
			return selectCatListItems;
		}
        private List<SelectListItem> GetItems() //ophalen Data van DB
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value= "1", Text= "Optie 1"},
                new SelectListItem { Value= "2", Text= "Optie 2"},
                new SelectListItem { Value= "3", Text= "Optie 3"}
            };
        }

	}
}
