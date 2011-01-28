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
                    && (EntityFunctions.DiffDays(c.DateCreated, (criteria.StartDateCreated == null || criteria.StartDateCreated == DateTime.MinValue ? c.DateCreated : criteria.StartDateCreated)) <= 0
                      && EntityFunctions.DiffDays(c.DateCreated, (criteria.EndDateCreated == null || criteria.EndDateCreated == DateTime.MinValue ? c.DateCreated : criteria.EndDateCreated)) >= 0)
                    && (EntityFunctions.DiffDays(c.DateCreated, (criteria.ExactDateCreated == null || criteria.ExactDateCreated == DateTime.MinValue ? c.DateCreated : criteria.ExactDateCreated)) == 0)
                    && (EntityFunctions.DiffDays(c.DateModified, (criteria.StartDateModified == null || criteria.StartDateModified == DateTime.MinValue ? c.DateModified : criteria.StartDateModified)) <= 0
                      && EntityFunctions.DiffDays(c.DateCreated, (criteria.EndDateModified == null || criteria.EndDateModified == DateTime.MinValue ? c.DateModified : criteria.EndDateModified)) >= 0)
                    && (EntityFunctions.DiffDays(c.DateCreated, (criteria.ExactDateModified == null || criteria.ExactDateModified == DateTime.MinValue ? c.DateModified : criteria.ExactDateModified)) == 0)
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
                                 select c).FirstOrDefault<Category>();
            return category;
        }

        public int CategoryCount()
        {
            return this.context.Categories.Count();
        }

        public Category CreateCategory(Category category)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Categories.AddObject(category);
                    context.SaveChanges();
                    ts.Complete();
                    return category;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Category UpdateCategory(Category category)
        {
            try
            {
                Category tempCategory = (from s in context.Categories
                                         where s.CategoryID == category.CategoryID
                                         select s).First<Category>();
                
                tempCategory.Name = category.Name;
                tempCategory.UnitOfMeasure = category.UnitOfMeasure;
                tempCategory.IsApproved = category.IsApproved;
                tempCategory.DateCreated = category.DateCreated;
                tempCategory.DateModified = category.DateModified;
                tempCategory.CreatedBy = category.CreatedBy;
                tempCategory.ModifiedBy = category.ModifiedBy;
                tempCategory.ApprovedBy = category.ApprovedBy;
              
                using (TransactionScope ts = new TransactionScope())
                {
                context.Attach(tempCategory);
                context.ObjectStateManager.ChangeObjectState(tempCategory, EntityState.Modified);
                context.SaveChanges();
                ts.Complete();
                return tempCategory;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteCategory(Category category)
        {
            try
            {
                Category persistedCategory = (from c in context.Categories
                                      where c.Name.ToLower() == category.Name.ToLower()
                                      select c).First();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Categories.DeleteObject(persistedCategory);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Stationeries

        //public List<Category> FindCategoryByCriteria(DTO.CategorySearchDTO criteria)
        //{
        //    try
        //    {
        //        var Query =
        //            from c in context.Categories
        //            where c.CategoryID == (criteria.CategoryID == 0 ? c.CategoryID : criteria.CategoryID)
        //            && c.Name.Contains((criteria.Name == null || criteria.Name == "" ? c.Name : criteria.Name))
        //            && c.UnitOfMeasure == (criteria.UnitOfMeasure == null || criteria.UnitOfMeasure == "" ? c.UnitOfMeasure : criteria.UnitOfMeasure)
        //            && c.IsApproved == (criteria.IsApproved == null ? c.IsApproved : criteria.IsApproved)
        //            && (EntityFunctions.DiffDays(c.DateCreated, (criteria.StartDateCreated == null || criteria.StartDateCreated == DateTime.MinValue ? c.DateCreated : criteria.StartDateCreated)) <= 0
        //              && EntityFunctions.DiffDays(c.DateCreated, (criteria.EndDateCreated == null || criteria.EndDateCreated == DateTime.MinValue ? c.DateCreated : criteria.EndDateCreated)) >= 0)
        //            && (EntityFunctions.DiffDays(c.DateCreated, (criteria.ExactDateCreated == null || criteria.ExactDateCreated == DateTime.MinValue ? c.DateCreated : criteria.ExactDateCreated)) == 0)
        //            && (EntityFunctions.DiffDays(c.DateModified, (criteria.StartDateModified == null || criteria.StartDateModified == DateTime.MinValue ? c.DateModified : criteria.StartDateModified)) <= 0
        //              && EntityFunctions.DiffDays(c.DateCreated, (criteria.EndDateModified == null || criteria.EndDateModified == DateTime.MinValue ? c.DateModified : criteria.EndDateModified)) >= 0)
        //            && (EntityFunctions.DiffDays(c.DateCreated, (criteria.ExactDateModified == null || criteria.ExactDateModified == DateTime.MinValue ? c.DateModified : criteria.ExactDateModified)) == 0)
        //            && c.CreatedBy == (criteria.CreatedBy == null ? c.CreatedBy : criteria.CreatedBy)
        //            && c.ModifiedBy == (criteria.ModifiedBy == null ? c.ModifiedBy : criteria.ModifiedBy)
        //            && c.ApprovedBy == (criteria.ApprovedBy == null ? c.ApprovedBy : criteria.ApprovedBy)
        //            select c;
        //        List<Category> categories = Query.ToList<Category>();
        //        return categories;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public List<Stationery> GetAllStationery()
        {
            return (from s in context.Stationeries
                    select s).ToList();
        }

        //public Stationery GetStationeryByID(int StationeryID)
        //{
        //    Stationery stationery = (from s in context.Stationeries
        //                         where s.StationeryID == StationeryID
        //                         select s).First<Stationery>();
        //    return category;
        //}

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