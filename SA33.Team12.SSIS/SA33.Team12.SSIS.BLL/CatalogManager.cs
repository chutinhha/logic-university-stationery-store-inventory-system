/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.BLL
{
    public class CatalogManager:BusinessLogic
    {
        public List<Category> GetAllCategory()
        {
                return (from c in context.Categories
                        select c).ToList();
        }

        public int CategoryCount()
        {
            return this.context.Categories.Count();
        }

        public void CreateCategory(Category category){
            this.context.Categories.AddObject(category);
            this.context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            this.context.Categories.Attach(category);
            this.context.ObjectStateManager.ChangeObjectState(category, System.Data.EntityState.Modified);
            this.context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            this.context.Categories.Attach(category);
            this.context.Categories.DeleteObject(category);
            this.context.SaveChanges();
        }

        public Category GetCategoryByID(int CategoryID)
        {
            Category category = (from c in context.Categories
                                 where c.CategoryID == CategoryID
                                 select c).FirstOrDefault();
            return category;
        }

        public List<Stationery> GetAllStationery()
        {
            return (from s in context.Stationeries
                    select s).ToList();
        }

        public int GetStationeryCount()
        {
            return context.Stationeries.Count();
        }

        public void CreateStationery(Stationery stationery)
        {
            this.context.Stationeries.AddObject(stationery);
            this.context.SaveChanges();
        }

        public void UpdateStationery(Stationery stationery)
        {
            Stationery stationeryTemp = (from s in context.Stationeries
                                         where s.StationeryID == stationery.StationeryID
                                         select s).FirstOrDefault();
            if (stationeryTemp != null)
            {
                stationeryTemp.ItemCode = stationery.ItemCode;
                // add more properties here
            }
            this.context.SaveChanges();
        }

        public void DeleteStationery(Stationery stationery)
        {
            this.context.Stationeries.Attach(stationery);
            this.context.Stationeries.DeleteObject(stationery);
            this.context.SaveChanges();
        }

    }
}