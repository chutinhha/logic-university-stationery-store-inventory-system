using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SA33.Team12.SSIS;

namespace SA33.Team12.SSIS.DAL
{
    public class StationeryDAO : DALLogic
    {
        /***
         * this is a sample do not use this
         * ***/

        public void CreateStationery(DAL.Stationery stationery)
        {
            context.Stationeries.AddObject(stationery);
            context.SaveChanges();
        }

        public List<DAL.Stationery> GetAllStationery()
        {
            return (from s in context.Stationeries select s).ToList<DAL.Stationery>();
        }

        public void UpdateStationery(DAL.Stationery stationery)
        {
            DAL.Stationery tempStationery = (from s in context.Stationeries
                                             where s.StationeryID == stationery.StationeryID
                                             select s).FirstOrDefault<DAL.Stationery>();
            tempStationery.ItemCode = stationery.ItemCode;
            tempStationery.Description = stationery.Description;
            // ...
            // no ==>> tempStationery.CategoryID = stationery.CategoryID;
            tempStationery.Category = stationery.Category;
            context.ObjectStateManager.ChangeObjectState(tempStationery, System.Data.EntityState.Modified);
            context.SaveChanges();
        }

        public void DeleteStationery(DAL.Stationery stationery)
        {
            context.Stationeries.Attach(stationery);
            context.Stationeries.DeleteObject(stationery);
            context.SaveChanges();
        }

        //Method Name: FindStationeryByID()
        //Created By: Wang Pinyi
        //Date: 29/1/2011
        public Stationery FindStationeryByID(int id)
        {
            try
            {
                Stationery stationery = (from p in context.Stationeries where p.StationeryID == id select p).FirstOrDefault();
                return stationery;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}