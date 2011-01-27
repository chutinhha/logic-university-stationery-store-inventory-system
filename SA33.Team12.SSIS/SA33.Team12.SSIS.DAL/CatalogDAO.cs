/***
 * Author: Naing Myo Aung (A0076803A), Victor Tong(A0066920E)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.DAL
{
    public class CatalogDAO:DALLogic
    {
        public List<Category> FindCategoryByCriteria(DTO.CategorySearchDTO criteria)
        {
            try
            {
                var Query =
                    from c in context.Categories
                    where c.CategoryID == (criteria.CategoryID == 0 ? c.CategoryID : criteria.CategoryID)
                    && c.Name.Contains((criteria.Name == null || criteria.Name == "" ? c.Name : criteria.Name))
                    && c.UnitOfMeasure == (criteria.UnitOfMeasure == null || criteria.UnitOfMeasure == "" ? c.UnitOfMeasure : criteria.UnitOfMeasure)
                    && c.IsApproved == (criteria.IsApproved == null ? c.IsApproved : criteria.IsApproved)
                    //XX && c.DateCreated == (criteria.DateCreated == null || criteria.DateCreated == "" ? c.DateCreated : criteria.DateCreated)
                    && (EntityFunctions.DiffDays(c.DateCreated, (criteria.StartDateCreated == null || criteria.StartDateCreated == DateTime.MinValue ? c.DateCreated : criteria.StartDateCreated)) <= 0
                      && EntityFunctions.DiffDays(c.DateCreated, (criteria.EndDateCreated == null || criteria.EndDateCreated == DateTime.MinValue ? c.DateCreated : criteria.EndDateCreated)) >= 0)
                    && (EntityFunctions.DiffDays(c.DateCreated, (criteria.ExactDateCreated == null || criteria.ExactDateCreated == DateTime.MinValue ? c.DateCreated : criteria.ExactDateCreated)) == 0)
                    //XX && c.DateModified == (criteria.DateModified == null || criteria.DateModified == "" ? c.DateModified : criteria.DateModified)
                    //&& (EntityFunctions.DiffDays(bll.DateBlacklisted, (criteria.StartDateBlackListed == null || criteria.StartDateBlackListed == DateTime.MinValue ? bll.DateBlacklisted : criteria.StartDateBlackListed)) <= 0
                        //&& EntityFunctions.DiffDays(bll.DateBlacklisted, (criteria.EndDateBlackListed == null || criteria.EndDateBlackListed == DateTime.MinValue ? bll.DateBlacklisted : criteria.EndDateBlackListed)) >= 0)
                    //&& (EntityFunctions.DiffDays(bll.DateBlacklisted, (criteria.ExactDateBlackListed == null || criteria.ExactDateBlackListed == DateTime.MinValue ? bll.DateBlacklisted : criteria.ExactDateBlackListed)) == 0)
                    && c.CreatedBy == (criteria.CreatedBy == null ? c.CreatedBy : criteria.CreatedBy)
                    && c.ModifiedBy == (criteria.ModifiedBy == null ? c.ModifiedBy : criteria.ModifiedBy)
                    && c.ApprovedBy == (criteria.ApprovedBy == null ? c.ApprovedBy : criteria.ApprovedBy)
                    select c;
                List<Category> categories = Query.ToList<Category>();
                return categories;
            }
            catch (Exception)
            {
                throw;
            }
        }   

        public List<Category> GetAllCategory()
        {
                return (from c in context.Categories
                        select c).ToList();
        }

        
        public Category GetCategoryByID(int CategoryID)
        {
            Category category = (from c in context.Categories
                                 where c.CategoryID == CategoryID
                                 select c).FirstOrDefault();
            return category;
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
            Category tempCategory = (from s in context.Categories where s.CategoryID == category.CategoryID select s).FirstOrDefault<Category>();
            {
                tempCategory.Name = category.Name;                
            }
            this.context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            this.context.Categories.Attach(category);
            this.context.Categories.DeleteObject(category);
            this.context.SaveChanges();
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