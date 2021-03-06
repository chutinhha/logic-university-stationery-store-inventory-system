﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SA33.Team12.SSIS;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.BLL
{
    public class StationeryManager : BusinessLogic
    {
        /***
         * this is a sample do not use this
         * ***/
        private StationeryDAO stationeryDAO;

        public StationeryManager()
        {
            stationeryDAO = new StationeryDAO();
        }

        public void CreateStationery(DAL.Stationery stationery)
        {
            stationeryDAO.CreateStationery(stationery);
        }

        public List<DAL.Stationery> GetAllStationery()
        {
            return stationeryDAO.GetAllStationery();
        }

        public void UpdateStationery(DAL.Stationery stationery)
        {
            stationeryDAO.UpdateStationery(stationery);
        }

        public void DeleteStationery(DAL.Stationery stationery)
        {
            stationeryDAO.DeleteStationery(stationery);
        }

        //added by Wang Pinyi
        //29/1/2011
        public Stationery FindStationeryByID(int id)
        {
            return stationeryDAO.FindStationeryByID(id);
        }
    }
}