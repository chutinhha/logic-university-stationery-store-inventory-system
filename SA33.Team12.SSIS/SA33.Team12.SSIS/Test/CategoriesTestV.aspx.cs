﻿/***
 * Author: Victor Tong (A0066920E)
 * Initial Implementation: 1/Feb/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Catalog
{
    public partial class CategoriesTestV : AppCode.PageBase
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.DynamicDataManager.RegisterControl(this.CategoryGridView);
            this.DynamicDataManager.RegisterControl(this.StationeryGridView);

            this.CategoryGridView.EnableDynamicData(typeof(Category));
            this.StationeryGridView.EnableDynamicData(typeof(Stationery));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ClearStationeryGridViewData();
            }
        }

        private void ClearStationeryGridViewData()
        {
            List<Stationery> stationeries = new List<Stationery>();
            this.StationeryGridView.DataSource = stationeries;
            this.StationeryGridView.DataBind();
        }

        protected void CategoryGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = (int)CategoryGridView.SelectedDataKey.Value;
            Category category = null;
            using (CatalogManager cm = new CatalogManager())
            {
                category = cm.GetCategoryByID(selectedIndex);
                if (category != null)
                {
                    List<Stationery> stationeries = category.Stationeries.ToList();
                    this.StationeryGridView.DataSource = stationeries;
                    this.StationeryGridView.DataBind();
                }
            }
        }

        protected void transactionButton_Click(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                StationeryEntities ctx = new StationeryEntities();
                ApprovalAudit audit = (from s in ctx.ApprovalAudits
                                       select s).FirstOrDefault();
                ctx.ApprovalAudits.Attach(audit);
                ctx.ApprovalAudits.DeleteObject(audit);
                ctx.SaveChanges();
                ts.Complete();
            }
        }

        protected void CategoryGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int UserID = (int)DataBinder.Eval(e.Row.DataItem, "CreatedBy");
                if (UserID != 0)
                {
                    Literal userid = e.Row.FindControl("CreatedByLiteral") as Literal;
                    if (userid != null)
                    {
                        using (UserManager um = new UserManager())
                        {
                            User user = um.GetUserByID(UserID);
                            if (user != null) userid.Text = user.UserName;
                        }
                    }
                }
            }
        }
    }
}