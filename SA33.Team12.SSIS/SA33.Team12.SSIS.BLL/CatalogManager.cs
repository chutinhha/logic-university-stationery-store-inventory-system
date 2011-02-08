/***
 * Author: Victor Tong(A0066920E)
 * Initial Implementation: 30/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.Exceptions;

namespace SA33.Team12.SSIS.BLL
{
    public class CatalogManager : BusinessLogic
    {
        private CatalogDAO catalogDAO;

        public CatalogManager()
        {
            catalogDAO = new CatalogDAO();
        }

        #region Categories
        public List<Category> GetAllCategories()
        {
            return catalogDAO.GetAllCategories();
        }

        public Category GetCategoryByID(int CategoryID)
        {
            return catalogDAO.GetCategoryByID(CategoryID);
        }

        public List<Category> FindCategoriesByCriteria(CategorySearchDTO criteria)
        {
            return catalogDAO.FindCategoriesByCriteria(criteria);
        }

        public int GetCategoryCount()
        {
            return catalogDAO.GetCategoryCount();
        }

        public Category CreateCategory(Category category)
        {
            try
            {
                if (category != null)
                {
                    category.DateCreated = DateTime.Now;
                    category.DateModified = DateTime.Now;
                    category.IsApproved = false;
                    category.ApprovedByUser = null;
                    catalogDAO.CreateCategory(category);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog category creation failed.");
            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            try
            {
                if (category != null)
                {
                    category.DateModified = DateTime.Now;
                    catalogDAO.UpdateCategory(category);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog updating failed.");
            }
            return category;
        }

        public void DeleteCategory(Category category)
        {
            try
            {
                if (category != null)
                {
                    catalogDAO.DeleteCategory(category);
                }
            }
            catch (Exception)
            {
                //throw new Exceptions.UserException("Catalog deletion failed.");
            }
        }

        #endregion

        #region Stationeries
        public List<Stationery> GetAllStationeries()
        {
            return catalogDAO.GetAllStationeries();
        }

        public Stationery FindStationeryByID(int id)
        {
            return catalogDAO.GetStationeryByID(id);
        }

        public List<Stationery> FindStationeriesByCriteria(StationerySearchDTO criteria)
        {
            return catalogDAO.FindStationeriesByCriteria(criteria);
        }

        public int GetStationeryCount()
        {
            return catalogDAO.GetStationeryCount();
        }

        public List<Stationery> GetStationeriesByQuantityInHandLessThanReorderLevel()
        {
            return catalogDAO.GetStationeriesByQuantityInHandLessThanReorderLevel();
        }

        public List<SpecialStationery> GetSpecialStationeryToBeOrdered()
        {
            var Query = from ss in context.SpecialStationeries
                        where context.SpecialRequisitionItems.Any(
                            sritem => sritem.SpecialStationeryID == ss.SpecialStationeryID
                            && (int)sritem.QuantityIssued < sritem.QuantityRequested && ss != null)
                        select ss;
            return Query.ToList<SpecialStationery>();
        }

        public List<Stationery> GetStationeriesByCategory(int CategoryID)
        {
            return catalogDAO.GetStationeriesByCategory(CategoryID);
        }

        public Stationery CreateStationery(Stationery stationery)
        {
            try
            {
                if (stationery != null)
                {
                    stationery.DateCreated = DateTime.Now;
                    catalogDAO.CreateStationery(stationery);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog stationery creation failed.");
            }
            return stationery;
        }

        public Stationery UpdateStationery(Stationery stationery)
        {
            try
            {
                if (stationery != null)
                {
                    stationery.DateModified = DateTime.Now;
                    catalogDAO.UpdateStationery(stationery);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog stationery updating failed.");
            }
            return stationery;

        }

        public void DeleteStationery(Stationery stationery)
        {
            try
            {
                if (stationery != null)
                {
                    catalogDAO.DeleteStationery(stationery);
                }
            }
            catch (Exception)
            {
                //throw new Exceptions.UserException("Catalog stationery deletion failed.");
            }
        }
        #endregion

        #region SpecialStationeries
        public List<SpecialStationery> GetAllSpecialStationeries()
        {
            return catalogDAO.GetAllSpecialStationeries();
        }

        public SpecialStationery GetSpecialStationeryByID(int SpecialStationeryID)
        {
            return catalogDAO.GetSpecialStationeryByID(SpecialStationeryID);
        }

        public List<SpecialStationery> FindSpecialStationeriesByCriteria(SpecialStationerySearchDTO criteria)
        {
            return catalogDAO.FindSpecialStationeriesByCriteria(criteria);
        }

        public int GetSpecialStationeryCount()
        {
            return catalogDAO.GetSpecialStationeryCount();
        }

        public SpecialStationery CreateSpecialStationery(SpecialStationery specialStationery)
        {
            try
            {
                if (specialStationery != null)
                {
                    catalogDAO.CreateSpecialStationery(specialStationery);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog special stationery item creation failed.");
            }
            return specialStationery;
        }

        public SpecialStationery UpdateSpecialStationery(SpecialStationery specialStationery)
        {
            try
            {
                if (specialStationery != null)
                {
                    specialStationery.DateModified = DateTime.Now;
                    catalogDAO.UpdateSpecialStationery(specialStationery);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog special stationery item updating failed.");
            }
            return specialStationery;
        }

        public void DeleteSpecialStationery(SpecialStationery specialStationery)
        {
            try
            {
                if (specialStationery != null)
                {
                    catalogDAO.DeleteSpecialStationery(specialStationery);
                }
            }
            catch (Exception)
            {
                //throw new Exceptions.UserException("Catalog special stationery item deletion failed.");
            }
        }

        public string GenerateItemCode(string description, int count)
        {
            string result = string.Empty;
            if (description != string.Empty)
            {
                if (count < 10)
                    result = description.Substring(0, 1) + "00" + count;
                if (count >= 10 && count < 100)
                    result = description.Substring(0, 1) + "0" + count;
                if (count >= 100 && count < 1000)
                    result = description.Substring(0, 1) + count;
            }
            return result;
        }
        #endregion

        #region Locations
        public List<Location> GetAllLocations()
        {
            return catalogDAO.GetAllLocations();
        }

        public Location GetLocationByID(int LocationID)
        {
            return catalogDAO.GetLocationByID(LocationID);
        }

        public List<Location> FindLocationsByCriteria(LocationSearchDTO criteria)
        {
            return catalogDAO.FindLocationsByCriteria(criteria);
        }

        public int GetLocationCount()
        {
            return catalogDAO.GetLocationCount();
        }

        public Location CreateLocation(Location location)
        {
            try
            {
                if (location != null)
                {
                    catalogDAO.CreateLocation(location);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog item location creation failed.");
            }
            return location;
        }

        public Location UpdateLocation(Location location)
        {
            try
            {
                if (location != null && location.Name != string.Empty)
                {
                    catalogDAO.UpdateLocation(location);
                }

            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog item location updating failed.");
            }
            return location;
        }

        public void DeleteLocation(Location location)
        {
            try
            {
                if (location != null)
                {
                    catalogDAO.DeleteLocation(location);
                }
            }
            catch (Exception)
            {
                //throw new Exceptions.UserException("Catalog item location deletion failed.");
            }
        }
        #endregion

        #region StationeryPrices
        public List<StationeryPrice> GetAllStationeryPrices()
        {
            return catalogDAO.GetAllStationeryPrices();
        }

        public List<StationeryPrice> GetStationeryPricesByStationeryID(int stationeryID)
        {
            return catalogDAO.GetStationeryPriceByStationeryID(stationeryID);
        }

        public StationeryPrice GetStationeryPriceByID(int StationeryPriceID)
        {
            return catalogDAO.GetStationeryPriceByID(StationeryPriceID);
        }

        public List<StationeryPrice> FindStationeryPricesByCriteria(StationeryPriceSearchDTO criteria)
        {
            return catalogDAO.FindStationeryPricesByCriteria(criteria);
        }

        public int GetStationeryPriceCount()
        {
            return catalogDAO.GetStationeryPriceCount();
        }

        public StationeryPrice CreateStationeryPrice(StationeryPrice stationeryPrice)
        {
            try
            {
                if (stationeryPrice != null)
                {
                    catalogDAO.CreateStationeryPrice(stationeryPrice);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog stationery item price creation failed.");
            }
            return stationeryPrice;
        }

        public StationeryPrice UpdateStationeryPrice(StationeryPrice stationeryPrice)
        {
            try
            {
                if (stationeryPrice != null)
                {
                    catalogDAO.UpdateStationeryPrice(stationeryPrice);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog stationery item price updating failed.");
            }
            return stationeryPrice;
        }

        public void DeleteStationeryPrice(StationeryPrice stationeryPrice)
        {
            try
            {
                if (stationeryPrice != null)
                {
                    catalogDAO.DeleteStationeryPrice(stationeryPrice);
                }
            }
            catch (Exception)
            {
                //throw new Exceptions.UserException("Catalog stationery item price deletion failed.");
            }
        }
        #endregion

        #region Suppliers
        public List<Supplier> GetAllSuppliers()
        {
            return catalogDAO.GetAllSuppliers();
        }

        public Supplier GetSupplierByID(int SupplierID)
        {
            return catalogDAO.GetSupplierByID(SupplierID);
        }

        public List<Supplier> FindSuppliersByCriteria(SupplierSearchDTO criteria)
        {
            return catalogDAO.FindSuppliersByCriteria(criteria);
        }

        public List<Supplier> GetSuppliersByStationeryID(int stationeryID)
        {
            List<StationeryPrice> stationeryPrices
                = catalogDAO.FindStationeryPricesByCriteria(
                    new StationeryPriceSearchDTO() { StationeryID = stationeryID });
            List<Supplier> suppliers = new List<Supplier>();
            foreach (StationeryPrice stationeryPrice in stationeryPrices)
            {
                suppliers.Add(stationeryPrice.Supplier);
            }
            return suppliers;
        }


        public int GetSupplierCount()
        {
            return catalogDAO.GetSupplierCount();
        }

        public Supplier CreateSupplier(Supplier supplier)
        {
            try
            {
                if (supplier != null)
                {
                    catalogDAO.CreateSupplier(supplier);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Catalog supplier creation failed.");
            }
            return supplier;
        }

        public Supplier UpdateSupplier(Supplier supplier)
        {
            try
            {
                if (supplier != null)
                {
                    catalogDAO.UpdateSupplier(supplier);
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Update Supplier failed.");
            }
            return supplier;
        }

        public void DeleteSupplier(Supplier supplier)
        {
            try
            {
                if (supplier != null)
                {
                    catalogDAO.DeleteSupplier(supplier);
                }
            }
            catch (Exception)
            {
                //throw new Exceptions.UserException("Catalog supplier deletion failed.");
            }
        }
        #endregion
    }
}