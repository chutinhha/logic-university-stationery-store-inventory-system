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
        #region Categories
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
                tempCategory.CreatedByUser = category.CreatedByUser;
                tempCategory.ModifiedByUser = category.ModifiedByUser;
                tempCategory.ApprovedByUser = category.ApprovedByUser;
              
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
                                      where c.CategoryID == category.CategoryID
                                      select c).First<Category>();

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
        #endregion

        # region Stationeries
        public List<Stationery> FindStationeryByCriteria(DTO.StationerySearchDTO criteria)
        {
            try
            {
                var Query =
                    from s in context.Stationeries
                    where s.StationeryID == (criteria.StationeryID == 0 ? s.StationeryID : criteria.StationeryID)
                    && s.CategoryID == (criteria.CategoryID == 0 ? s.CategoryID : criteria.CategoryID)
                    && s.LocationID == (criteria.LocationID == 0 ? s.LocationID : criteria.LocationID)
                    && s.ItemCode.Contains((criteria.ItemCode == null || criteria.ItemCode == "" ? s.ItemCode : criteria.ItemCode))
                    && s.Description.Contains((criteria.Description == null || criteria.Description == "" ? s.Description : criteria.Description))
                    && s.ReorderLevel == (criteria.ReorderLevel == null ? s.ReorderLevel : criteria.ReorderLevel)
                    && s.ReorderQuantity == (criteria.ReorderQuantity == null ? s.ReorderQuantity : criteria.ReorderQuantity)
                    && s.QuantityInHand == (criteria.QuantityInHand == null ? s.QuantityInHand : criteria.QuantityInHand)
                    && (EntityFunctions.DiffDays(s.DateCreated, (criteria.StartDateCreated == null || criteria.StartDateCreated == DateTime.MinValue ? s.DateCreated : criteria.StartDateCreated)) <= 0
                      && EntityFunctions.DiffDays(s.DateCreated, (criteria.EndDateCreated == null || criteria.EndDateCreated == DateTime.MinValue ? s.DateCreated : criteria.EndDateCreated)) >= 0)
                    && (EntityFunctions.DiffDays(s.DateCreated, (criteria.ExactDateCreated == null || criteria.ExactDateCreated == DateTime.MinValue ? s.DateCreated : criteria.ExactDateCreated)) == 0)
                    && (EntityFunctions.DiffDays(s.DateModified, (criteria.StartDateModified == null || criteria.StartDateModified == DateTime.MinValue ? s.DateModified : criteria.StartDateModified)) <= 0
                      && EntityFunctions.DiffDays(s.DateModified, (criteria.EndDateModified == null || criteria.EndDateModified == DateTime.MinValue ? s.DateModified : criteria.EndDateModified)) >= 0)
                    && (EntityFunctions.DiffDays(s.DateModified, (criteria.ExactDateModified == null || criteria.ExactDateModified == DateTime.MinValue ? s.DateModified : criteria.ExactDateModified)) == 0)
                    && s.CreatedBy == (criteria.CreatedBy == null ? s.CreatedBy : criteria.CreatedBy)
                    && s.ModifiedBy == (criteria.ModifiedBy == null ? s.ModifiedBy : criteria.ModifiedBy)
                    && s.ApprovedBy == (criteria.ApprovedBy == null ? s.ApprovedBy : criteria.ApprovedBy)
                    && s.IsApproved == (criteria.IsApproved == null ? s.IsApproved : criteria.IsApproved)
                    select s;
                List<Stationery> stationeries = Query.ToList<Stationery>();
                return stationeries;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<Stationery> GetAllStationery()
        {
            return (from s in context.Stationeries
                    select s).ToList();
        }

        public Stationery GetStationeryByID(int StationeryID)
        {
            Stationery stationery = (from s in context.Stationeries
                                     where s.StationeryID == StationeryID
                                     select s).FirstOrDefault<Stationery>();
            return stationery;
        }

        public int GetStationeryCount()
        {
            return context.Stationeries.Count();
        }

        public Stationery CreateStationery(Stationery stationery)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Stationeries.AddObject(stationery);
                    context.SaveChanges();
                    ts.Complete();
                    return stationery;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Stationery UpdateStationery(Stationery stationery)
        {
            try
            {
                Stationery tempStationery = (from s in context.Stationeries
                                             where s.StationeryID == stationery.StationeryID
                                             select s).First<Stationery>();

                tempStationery.Category = stationery.Category;
                tempStationery.Location = stationery.Location;
                tempStationery.ItemCode = stationery.ItemCode;
                tempStationery.Description = stationery.Description;
                tempStationery.ReorderLevel = stationery.ReorderLevel;
                tempStationery.ReorderQuantity = stationery.ReorderQuantity;
                tempStationery.QuantityInHand = stationery.QuantityInHand;
                tempStationery.DateCreated = stationery.DateCreated;
                tempStationery.DateModified = stationery.DateModified;
                tempStationery.CreatedByUser = stationery.CreatedByUser;
                tempStationery.ModifiedByUser = stationery.ModifiedByUser;
                tempStationery.ApprovedByUser = stationery.ApprovedByUser;
                tempStationery.IsApproved = stationery.IsApproved;

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Attach(tempStationery);
                    context.ObjectStateManager.ChangeObjectState(tempStationery, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempStationery;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteStationery(Stationery stationery)
        {
            try
            {
                Stationery persistedStationery = (from s in context.Stationeries
                                                  where s.StationeryID == stationery.StationeryID
                                                select s).First<Stationery>();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Stationeries.DeleteObject(persistedStationery);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        # endregion

        # region SpecialStationeries
        public List<SpecialStationery> FindSpecialStationeryByCriteria(DTO.SpecialStationerySearchDTO criteria)
        {
            try
            {
                var Query =
                    from ss in context.SpecialStationeries
                    where ss.SpecialStationeryID == (criteria.SpecialStationeryID == 0 ? ss.SpecialStationeryID : criteria.SpecialStationeryID)
                    && ss.ItemCode.Contains((criteria.ItemCode == null || criteria.ItemCode == "" ? ss.ItemCode : criteria.ItemCode))
                    && ss.Description.Contains((criteria.Description == null || criteria.Description == "" ? ss.Description : criteria.Description))
                    && ss.Quantity == (criteria.Quantity == null ? ss.Quantity : criteria.Quantity)
                    && (EntityFunctions.DiffDays(ss.DateCreated, (criteria.StartDateCreated == null || criteria.StartDateCreated == DateTime.MinValue ? ss.DateCreated : criteria.StartDateCreated)) <= 0
                      && EntityFunctions.DiffDays(ss.DateCreated, (criteria.EndDateCreated == null || criteria.EndDateCreated == DateTime.MinValue ? ss.DateCreated : criteria.EndDateCreated)) >= 0)
                    && (EntityFunctions.DiffDays(ss.DateCreated, (criteria.ExactDateCreated == null || criteria.ExactDateCreated == DateTime.MinValue ? ss.DateCreated : criteria.ExactDateCreated)) == 0)
                    && (EntityFunctions.DiffDays(ss.DateModified, (criteria.StartDateModified == null || criteria.StartDateModified == DateTime.MinValue ? ss.DateModified : criteria.StartDateModified)) <= 0
                      && EntityFunctions.DiffDays(ss.DateModified, (criteria.EndDateModified == null || criteria.EndDateModified == DateTime.MinValue ? ss.DateModified : criteria.EndDateModified)) >= 0)
                    && (EntityFunctions.DiffDays(ss.DateModified, (criteria.ExactDateModified == null || criteria.ExactDateModified == DateTime.MinValue ? ss.DateModified : criteria.ExactDateModified)) == 0)
                    && (EntityFunctions.DiffDays(ss.DateApproved, (criteria.StartDateApproved == null || criteria.StartDateApproved == DateTime.MinValue ? ss.DateApproved : criteria.StartDateApproved)) <= 0
                      && EntityFunctions.DiffDays(ss.DateApproved, (criteria.EndDateApproved == null || criteria.EndDateApproved == DateTime.MinValue ? ss.DateApproved : criteria.EndDateApproved)) >= 0)
                    && (EntityFunctions.DiffDays(ss.DateApproved, (criteria.ExactDateApproved == null || criteria.ExactDateApproved == DateTime.MinValue ? ss.DateApproved : criteria.ExactDateApproved)) == 0)
                    && ss.CreatedBy == (criteria.CreatedBy == null ? ss.CreatedBy : criteria.CreatedBy)
                    && ss.ModifiedBy == (criteria.ModifiedBy == null ? ss.ModifiedBy : criteria.ModifiedBy)
                    && ss.ApprovedBy == (criteria.ApprovedBy == null ? ss.ApprovedBy : criteria.ApprovedBy)
                    && ss.CategoryID == (criteria.CategoryID == 0 ? ss.CategoryID : criteria.CategoryID)
                    && ss.IsApproved == (criteria.IsApproved == null ? ss.IsApproved : criteria.IsApproved)
                    select ss;
                List<SpecialStationery> specialStationeries = Query.ToList<SpecialStationery>();
                return specialStationeries;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SpecialStationery> GetAllSpecialStationery()
        {
            return (from ss in context.SpecialStationeries
                    select ss).ToList();
        }

        public SpecialStationery GetSpecialStationeryByID(int SpecialStationeryID)
        {
            SpecialStationery specialStationery = (from ss in context.SpecialStationeries
                                                   where ss.SpecialStationeryID == SpecialStationeryID
                                                   select ss).FirstOrDefault<SpecialStationery>();
            return specialStationery;
        }

        public int GetSpecialStationeryCount()
        {
            return context.SpecialStationeries.Count();
        }

        public SpecialStationery CreateSpecialStationery(SpecialStationery specialStationery)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.SpecialStationeries.AddObject(specialStationery);
                    context.SaveChanges();
                    ts.Complete();
                    return specialStationery;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SpecialStationery UpdateSpecialStationery(SpecialStationery specialStationery)
        {
            try
            {
                SpecialStationery tempSpecialStationery = (from ss in context.SpecialStationeries
                                             where ss.SpecialStationeryID == specialStationery.SpecialStationeryID
                                             select ss).First<SpecialStationery>();

                tempSpecialStationery.ItemCode = specialStationery.ItemCode;
                tempSpecialStationery.Description = specialStationery.Description;
                tempSpecialStationery.Quantity = specialStationery.Quantity;
                tempSpecialStationery.DateCreated = specialStationery.DateCreated;
                tempSpecialStationery.DateModified = specialStationery.DateModified;
                tempSpecialStationery.DateApproved = specialStationery.DateApproved;
                tempSpecialStationery.CreatedByUser = specialStationery.CreatedByUser;
                tempSpecialStationery.ModifiedByUser = specialStationery.ModifiedByUser;
                tempSpecialStationery.ApprovedByUser = specialStationery.ApprovedByUser;
                tempSpecialStationery.Category = specialStationery.Category;
                tempSpecialStationery.IsApproved = specialStationery.IsApproved;

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Attach(tempSpecialStationery);
                    context.ObjectStateManager.ChangeObjectState(tempSpecialStationery, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempSpecialStationery;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteSpecialStationery(SpecialStationery specialStationery)
        {
            try
            {
                SpecialStationery persistedSpecialStationery = (from ss in context.SpecialStationeries
                                                                where ss.SpecialStationeryID == specialStationery.SpecialStationeryID
                                                                select ss).First<SpecialStationery>();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.SpecialStationeries.DeleteObject(persistedSpecialStationery);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        # endregion

        //# region Prices
        //public List<Price> FindPriceByCriteria(DTO.PriceSearchDTO criteria)
        //{
        //    try
        //    {
        //        var Query =
        //            from p in context.Prices
        //            where p.PriceID == (criteria.PriceID == 0 ? p.PriceID : criteria.PriceID)
        //            && p.StationeryID == (criteria.StationeryID == 0 ? p.StationeryID : criteria.StationeryID)
        //            && p.SupplierID == (criteria.SupplierID == 0 ? p.SupplierID : criteria.SupplierID)
        //            && p.CreatedBy == (criteria.CreatedBy == null ? p.CreatedBy : criteria.CreatedBy)
        //            && (EntityFunctions.DiffDays(p.CreatedDate, (criteria.StartCreatedDate == null || criteria.StartCreatedDate == DateTime.MinValue ? p.CreatedDate : criteria.StartCreatedDate)) <= 0
        //              && EntityFunctions.DiffDays(p.CreatedDate, (criteria.EndCreatedDate == null || criteria.EndCreatedDate == DateTime.MinValue ? p.CreatedDate : criteria.EndCreatedDate)) >= 0)
        //            && (EntityFunctions.DiffDays(p.CreatedDate, (criteria.ExactCreatedDate == null || criteria.ExactCreatedDate == DateTime.MinValue ? p.CreatedDate : criteria.ExactCreatedDate)) == 0)
        //            select p;
        //        List<Price> prices = Query.ToList<Price>();
        //        return prices;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<Price> GetAllPrice()
        //{
        //    return (from p in context.Prices
        //            select p).ToList();
        //}

        //public Price GetPriceByID(int PriceID)
        //{
        //    Price price = (from p in context.Prices
        //                   where p.PriceID == PriceID
        //                   select p).FirstOrDefault<Price>();
        //    return price;
        //}

        //public int GetPriceCount()
        //{
        //    return context.Prices.Count();
        //}

        //public Price CreatePrice(Price price)
        //{
        //    try
        //    {
        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            context.Prices.AddObject(price);
        //            context.SaveChanges();
        //            ts.Complete();
        //            return price;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public Price UpdatePrice(Price price)
        //{
        //    try
        //    {
        //        Price tempPrice = (from p in context.Prices
        //                           where p.PriceID == price.PriceID
        //                           select p).First<Price>();

        //        tempPrice.Name = price.Name;
        //        tempPrice.CreatedByUser = price.CreatedByUser;
        //        tempPrice.CreatedDate = price.CreatedDate;

        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            context.Attach(tempPrice);
        //            context.ObjectStateManager.ChangeObjectState(tempPrice, EntityState.Modified);
        //            context.SaveChanges();
        //            ts.Complete();
        //            return tempPrice;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void DeletePrice(Price price)
        //{
        //    try
        //    {
        //        Price persistedPrice = (from p in context.Prices
        //                                where p.PriceID == price.PriceID
        //                                select p).First<Price>();

        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            context.Prices.DeleteObject(persistedPrice);
        //            context.SaveChanges();
        //            ts.Complete();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //# endregion



        // end
    }
}