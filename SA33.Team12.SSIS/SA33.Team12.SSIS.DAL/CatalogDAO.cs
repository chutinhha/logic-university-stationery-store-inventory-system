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
using SA33.Team12.SSIS.Exceptions;

namespace SA33.Team12.SSIS.DAL
{
    public class CatalogDAO:DALLogic
    {
        #region Categories
        public List<Category> FindCategoriesByCriteria(DTO.CategorySearchDTO criteria)
        {
            try
            {
                var Query =
                    from c in context.Categories
                    where c.CategoryID == (criteria.CategoryID == 0 ? c.CategoryID : criteria.CategoryID)
                    && c.Name.Contains((criteria.Name == null || criteria.Name == "" ? c.Name : criteria.Name))
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

        public List<Category> GetAllCategories()
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

        public int GetCategoryCount()
        {
            return this.context.Categories.Count();
        }

        public Category CreateCategory(Category category)
        {
            try
            {
                Category c = GetAllCategories().Where(x => x.Name.ToLower() == category.Name.ToLower()).FirstOrDefault<Category>();

                using (TransactionScope ts = new TransactionScope())
                {                
                   
                        context.AddToCategories(category);
                        context.SaveChanges();
                        ts.Complete();
                   
                    return category;
                }
            }
            catch (Exception)
            {
                throw new CatalogException("Error Occured when creating category");
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
                tempCategory.DateModified = DateTime.Now;               
                tempCategory.ModifiedByUser = category.ModifiedByUser;            
              
                using (TransactionScope ts = new TransactionScope())
                {            
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
                                      select c).FirstOrDefault<Category>();

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
        public List<Stationery> FindStationeriesByCriteria(DTO.StationerySearchDTO criteria)
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
                    && (EntityFunctions.DiffDays(s.DateApproved, (criteria.StartDateApproved == null || criteria.StartDateApproved == DateTime.MinValue ? s.DateApproved : criteria.StartDateApproved)) <= 0
                      && EntityFunctions.DiffDays(s.DateApproved, (criteria.EndDateApproved == null || criteria.EndDateApproved == DateTime.MinValue ? s.DateApproved : criteria.EndDateApproved)) >= 0)
                    && (EntityFunctions.DiffDays(s.DateApproved, (criteria.ExactDateApproved == null || criteria.ExactDateApproved == DateTime.MinValue ? s.DateApproved : criteria.ExactDateApproved)) == 0)
                    && s.CreatedBy == (criteria.CreatedBy == null ? s.CreatedBy : criteria.CreatedBy)
                    && s.ModifiedBy == (criteria.ModifiedBy == null ? s.ModifiedBy : criteria.ModifiedBy)
                    && s.ApprovedBy == (criteria.ApprovedBy == null ? s.ApprovedBy : criteria.ApprovedBy)
                    && s.IsApproved == (criteria.IsApproved == null ? s.IsApproved : criteria.IsApproved)
                    && s.UnitOfMeasure == (criteria.UnitOfMeasure == null || criteria.UnitOfMeasure == "" ? s.UnitOfMeasure : criteria.UnitOfMeasure)
                    select s;
                List<Stationery> stationeries = Query.ToList<Stationery>();
                return stationeries;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<Stationery> GetAllStationeries()
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

        public List<Stationery> GetStationeriesByQuantityInHandLessThanReorderLevel()
        {
            return (from s in context.Stationeries
                    where s.QuantityInHand < s.ReorderLevel
                    select s).ToList();
        }

        public List<Stationery> GetStationeriesByCategory(int CategoryID)
        {
            return GetAllStationeries().Where(x => x.CategoryID == CategoryID).ToList<Stationery>();
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
                tempStationery.DateApproved = stationery.DateApproved;
                tempStationery.CreatedByUser = stationery.CreatedByUser;
                tempStationery.ModifiedByUser = stationery.ModifiedByUser;
                tempStationery.ApprovedByUser = stationery.ApprovedByUser;
                tempStationery.IsApproved = stationery.IsApproved;
                tempStationery.UnitOfMeasure = stationery.UnitOfMeasure;

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
        public List<SpecialStationery> FindSpecialStationeriesByCriteria(DTO.SpecialStationerySearchDTO criteria)
        {
            try
            {
                var Query =
                    from ss in context.SpecialStationeries
                    where ss.SpecialStationeryID == (criteria.SpecialStationeryID == 0 ? ss.SpecialStationeryID : criteria.SpecialStationeryID)
                    && ss.CategoryID == (criteria.CategoryID == 0 ? ss.CategoryID : criteria.CategoryID)
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
                    && ss.IsApproved == (criteria.IsApproved == null ? ss.IsApproved : criteria.IsApproved)
                    && ss.UnitOfMeasure == (criteria.UnitOfMeasure == null || criteria.UnitOfMeasure == "" ? ss.UnitOfMeasure : criteria.UnitOfMeasure)
                    select ss;
                List<SpecialStationery> specialStationeries = Query.ToList<SpecialStationery>();
                return specialStationeries;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SpecialStationery> GetAllSpecialStationeries()
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
                    context.AddToSpecialStationeries(specialStationery);
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

                tempSpecialStationery.Category = specialStationery.Category;
                tempSpecialStationery.ItemCode = specialStationery.ItemCode;
                tempSpecialStationery.Description = specialStationery.Description;
                tempSpecialStationery.Quantity = specialStationery.Quantity;
                tempSpecialStationery.DateCreated = specialStationery.DateCreated;
                tempSpecialStationery.DateModified = specialStationery.DateModified;
                tempSpecialStationery.DateApproved = specialStationery.DateApproved;
                tempSpecialStationery.CreatedByUser = specialStationery.CreatedByUser;
                tempSpecialStationery.ModifiedByUser = specialStationery.ModifiedByUser;
                tempSpecialStationery.ApprovedByUser = specialStationery.ApprovedByUser;
                tempSpecialStationery.IsApproved = specialStationery.IsApproved;
                tempSpecialStationery.UnitOfMeasure = specialStationery.UnitOfMeasure;

                using (TransactionScope ts = new TransactionScope())
                {
                  //  context.Attach(tempSpecialStationery);
                  //context.ObjectStateManager.ChangeObjectState(tempSpecialStationery, EntityState.Modified);
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

        # region Locations
        public List<Location> FindLocationsByCriteria(DTO.LocationSearchDTO criteria)
        {
            try
            {
                var Query =
                    from l in context.Locations
                    where l.LocationID == (criteria.LocationID == 0 ? l.LocationID : criteria.LocationID)
                    && l.Name.Contains((criteria.Name == null || criteria.Name == "" ? l.Name : criteria.Name))
                    && l.CreatedBy == (criteria.CreatedBy == null ? l.CreatedBy : criteria.CreatedBy)
                    && (EntityFunctions.DiffDays(l.CreatedDate, (criteria.StartCreatedDate == null || criteria.StartCreatedDate == DateTime.MinValue ? l.CreatedDate : criteria.StartCreatedDate)) <= 0
                      && EntityFunctions.DiffDays(l.CreatedDate, (criteria.EndCreatedDate == null || criteria.EndCreatedDate == DateTime.MinValue ? l.CreatedDate : criteria.EndCreatedDate)) >= 0)
                    && (EntityFunctions.DiffDays(l.CreatedDate, (criteria.ExactCreatedDate == null || criteria.ExactCreatedDate == DateTime.MinValue ? l.CreatedDate : criteria.ExactCreatedDate)) == 0)
                    select l;
                List<Location> locations = Query.ToList<Location>();
                return locations;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Location> GetAllLocations()
        {
            return (from l in context.Locations
                    select l).ToList();
        }

        public Location GetLocationByID(int LocationID)
        {
            Location location = (from l in context.Locations
                                 where l.LocationID == LocationID
                                 select l).FirstOrDefault<Location>();
            return location;
        }

        public int GetLocationCount()
        {
            return context.Locations.Count();
        }

        public Location CreateLocation(Location location)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Locations.AddObject(location);
                    context.SaveChanges();
                    ts.Complete();
                    return location;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Location UpdateLocation(Location location)
        {
            try
            {
                Location tempLocation = (from l in context.Locations
                                         where l.LocationID == location.LocationID
                                         select l).First<Location>();

                tempLocation.Name = location.Name;
                tempLocation.CreatedByUser = location.CreatedByUser;
                tempLocation.CreatedDate = location.CreatedDate;

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Attach(tempLocation);
                    context.ObjectStateManager.ChangeObjectState(tempLocation, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempLocation;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteLocation(Location location)
        {
            try
            {
                Location persistedLocation = (from l in context.Locations
                                              where l.LocationID == location.LocationID
                                              select l).First<Location>();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Locations.DeleteObject(persistedLocation);
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

        # region StationeryPrices
        // changed by Wang Pinyi on Feb 1st, 2011
        public List<StationeryPrice> FindStationeryPricesByCriteria(DTO.StationeryPriceSearchDTO criteria)
        {
            try
            {
                var Query =
                    from p in context.StationeryPrices
                    where p.StationeryPriceID == (criteria.StationeryPriceID == 0 ? p.StationeryPriceID : criteria.StationeryPriceID)
                    && p.StationeryID == (criteria.StationeryID == 0 ? p.StationeryID : criteria.StationeryID)
                    && p.SupplierID == (criteria.SupplierID == 0 ? p.SupplierID : criteria.SupplierID)
                    && p.Price == (criteria.Price == 0 ? p.Price : criteria.Price)
                    select p;
                List<StationeryPrice> stationeryPrices = Query.ToList<StationeryPrice>();
                return stationeryPrices;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<StationeryPrice> GetAllStationeryPrices()
        {
            return (from p in context.StationeryPrices
                    select p).ToList();
        }

        public StationeryPrice GetStationeryPriceByID(int StationeryPriceID)
        {
            StationeryPrice stationeryPrice = (from p in context.StationeryPrices
                                               where p.StationeryPriceID == StationeryPriceID
                                               select p).FirstOrDefault<StationeryPrice>();
            return stationeryPrice;
        }

        public int GetStationeryPriceCount()
        {
            return context.StationeryPrices.Count();
        }

        public StationeryPrice CreateStationeryPrice(StationeryPrice stationeryPrice)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.StationeryPrices.AddObject(stationeryPrice);
                    context.SaveChanges();
                    ts.Complete();
                    return stationeryPrice;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public StationeryPrice UpdateStationeryPrice(StationeryPrice stationeryPrice)
        {
            try
            {
                StationeryPrice tempStationeryPrice = (from p in context.StationeryPrices
                                                       where p.StationeryPriceID == stationeryPrice.StationeryPriceID
                                                       select p).First<StationeryPrice>();

                tempStationeryPrice.Stationery = stationeryPrice.Stationery;
                tempStationeryPrice.Supplier = stationeryPrice.Supplier;
                tempStationeryPrice.Price = stationeryPrice.Price;

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Attach(tempStationeryPrice);
                    context.ObjectStateManager.ChangeObjectState(tempStationeryPrice, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempStationeryPrice;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteStationeryPrice(StationeryPrice stationeryPrice)
        {
            try
            {
                StationeryPrice persistedStationeryPrice = (from p in context.StationeryPrices
                                                            where p.StationeryPriceID == stationeryPrice.StationeryPriceID
                                                            select p).First<StationeryPrice>();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.StationeryPrices.DeleteObject(persistedStationeryPrice);
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

        #region Suppliers
        public List<Supplier> FindSuppliersByCriteria(DTO.SupplierSearchDTO criteria)
        {
            try
            {
                var Query =
                    from r in context.Suppliers
                    where r.SupplierID == (criteria.SupplierID == 0 ? r.SupplierID : criteria.SupplierID)
                    && r.SupplierCode.Contains((criteria.SupplierCode == null || criteria.SupplierCode == "" ? r.SupplierCode : criteria.SupplierCode))
                    && r.CompanyName.Contains((criteria.CompanyName == null || criteria.CompanyName == "" ? r.SupplierCode : criteria.CompanyName))
                    && (EntityFunctions.DiffDays(r.TenderedYear, (criteria.StartTenderedYear == null || criteria.StartTenderedYear == DateTime.MinValue ? r.TenderedYear : criteria.StartTenderedYear)) <= 0
                      && EntityFunctions.DiffDays(r.TenderedYear, (criteria.EndTenderedYear == null || criteria.EndTenderedYear == DateTime.MinValue ? r.TenderedYear : criteria.EndTenderedYear)) >= 0)
                    && (EntityFunctions.DiffDays(r.TenderedYear, (criteria.ExactTenderedYear == null || criteria.ExactTenderedYear == DateTime.MinValue ? r.TenderedYear : criteria.ExactTenderedYear)) == 0)
                    && r.PreferredRank == (criteria.PreferredRank == null ? r.PreferredRank : criteria.PreferredRank)
                    select r;
                List<Supplier> suppliers = Query.ToList<Supplier>();
                return suppliers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Supplier> GetAllSuppliers()
        {
            return (from r in context.Suppliers
                    select r).ToList();
        }

        public Supplier GetSupplierByID(int SupplierID)
        {
            Supplier supplier = (from r in context.Suppliers
                                 where r.SupplierID == SupplierID
                                 select r).FirstOrDefault<Supplier>();
            return supplier;
        }

        public int GetSupplierCount()
        {
            return this.context.Suppliers.Count();
        }

        public Supplier CreateSupplier(Supplier supplier)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Suppliers.AddObject(supplier);
                    context.SaveChanges();
                    ts.Complete();
                    return supplier;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Supplier UpdateSupplier(Supplier supplier)
        {
            try
            {
                Supplier tempSupplier = (from r in context.Suppliers
                                         where r.SupplierID == supplier.SupplierID
                                         select r).First<Supplier>();

                tempSupplier.SupplierCode = supplier.SupplierCode;
                tempSupplier.CompanyName = supplier.CompanyName;
                tempSupplier.TenderedYear = supplier.TenderedYear;
                tempSupplier.PreferredRank = supplier.PreferredRank;

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Attach(tempSupplier);
                    context.ObjectStateManager.ChangeObjectState(tempSupplier, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempSupplier;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteSupplier(Supplier supplier)
        {
            try
            {
                Supplier persistedSupplier = (from r in context.Suppliers
                                              where r.SupplierID == supplier.SupplierID
                                              select r).First<Supplier>();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Suppliers.DeleteObject(persistedSupplier);
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

        // end
    }
}