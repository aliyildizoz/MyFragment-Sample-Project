using MyFragment.Business.Abstract;
using MyFragment.Business.Manager;
using MyFragment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFragment.UI.Models
{
    public class CategoryViewModel
    {
        private CategoryManager categoryManager;
        public List<SelectListItem> Types { get; set; }
        public List<SelectListItem> Years { get; set; }
        public List<SelectListItem> ImdbPoints { get; set; }
        
        public CategoryViewModel()
        {
            categoryManager = new CategoryManager();
            Years = new List<SelectListItem>();
            Types = new List<SelectListItem>();
            ImdbPoints = new List<SelectListItem>();
            CategoryToSelectList();
        }
        private void CategoryToSelectList()
        {
            categoryManager.TypeList().ToList().ForEach(I =>
            {
                Types.Add(new SelectListItem() { Text = I.Value, Value = I.Id.ToString() });
            });
            categoryManager.YearList().ToList().ForEach(I =>
            {
                Years.Add(new SelectListItem() { Text = I.Value, Value = I.Id.ToString() });
            });
            Years = Years.OrderBy(I => I.Text).ToList();
            categoryManager.ImdbList().ToList().ForEach(I =>
            {
                ImdbPoints.Add(new SelectListItem() { Text = I.Value, Value = I.Id.ToString() });
            });
            ImdbPoints = ImdbPoints.OrderBy(I => I.Text).ToList();
        }
        
    }
}