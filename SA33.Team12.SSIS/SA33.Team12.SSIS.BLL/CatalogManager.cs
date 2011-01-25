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
        private CatalogDAO catalogDAO;

        public CatalogManager()
        {
            catalogDAO = new CatalogDAO();
        }

        public List<Category> GetAllCategory()
        {
            return catalogDAO.GetAllCategory();
        }

        public int CategoryCount()
        {
            return catalogDAO.CategoryCount();
        }

        public void CreateCategory(Category category){
            catalogDAO.CreateCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            catalogDAO.UpdateCategory(category);
        }

        public void DeleteCategory(Category category)
        {
            catalogDAO.DeleteCategory(category);
        }

        public Category GetCategoryByID(int CategoryID)
        {
            return catalogDAO.GetCategoryByID(CategoryID);
        }

        public List<Stationery> GetAllStationery()
        {
            return catalogDAO.GetAllStationery();
        }

        public int GetStationeryCount()
        {
            return catalogDAO.GetStationeryCount();
        }

        public void CreateStationery(Stationery stationery)
        {
            catalogDAO.CreateStationery(stationery);
        }

        public void UpdateStationery(Stationery stationery)
        {
            catalogDAO.UpdateStationery(stationery);
        }

        public void DeleteStationery(Stationery stationery)
        {
            catalogDAO.DeleteStationery(stationery);
        }
    }
}