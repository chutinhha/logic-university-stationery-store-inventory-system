﻿using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Test
{
    public partial class TestStationeryPrices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                using(CatalogManager cm = new CatalogManager())
                {
                    this.StationeriesGridView.DataSource = cm.GetAllStationeries();
                    this.StationeriesGridView.DataBind();
                }

                Stationery stationery = new Stationery();
                StationeryPrice stationeryPrice = new StationeryPrice();
            }
        }

        protected void StationeriesGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                int stationeryID = (int) DataBinder.Eval(e.Row.DataItem, "StationeryID");
                DropDownList SupplierDrowDownList = e.Row.FindControl("SupplierDrowDownList") as DropDownList;
                using (CatalogManager cm = new CatalogManager())
                {
                    List<StationeryPrice> stationeryPrices
                        = cm.FindStationeryPricesByCriteria(
                            new StationeryPriceSearchDTO() {StationeryID = stationeryID});
                    List<Supplier> suppliers = new List<Supplier>();
                    foreach (StationeryPrice stationeryPrice in stationeryPrices)
                    {
                        suppliers.Add(stationeryPrice.Supplier);
                    }
                    SupplierDrowDownList.DataSource = suppliers;
                    SupplierDrowDownList.DataBind();
                }
            }

        }

    }
}