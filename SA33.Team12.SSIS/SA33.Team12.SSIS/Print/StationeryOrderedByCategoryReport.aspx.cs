using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Text;
using SA33.Team12.SSIS.Print;
using SA33.Team12.SSIS.Print.ReportDSTableAdapters;

namespace SA33.Team12.SSIS.Test
{
    public partial class StationeryOrderedByCategoryReport : System.Web.UI.Page
    {
        ReportDS ds;
        DataView dv;
        ReportDocument rep;
        protected void Page_Load(object sender, EventArgs e)
        {
            rep = new ReportDocument();
            rep.Load(Server.MapPath("~/Print/StationeryOrderedByCategoryCR.rpt"));

            ds = new ReportDS();
            vw_StationeryOrderedByCategoryTableAdapter ts = new vw_StationeryOrderedByCategoryTableAdapter();
            ts.Fill(ds.vw_StationeryOrderedByCategory);

            dv = ds.vw_StationeryOrderedByCategory.DefaultView;
            GenerateReport(dv);
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder();
            if (CategoryDDL.SelectedValue != "Select a category")
            {
                query.Append("Name='" + CategoryDDL.SelectedValue + "'");
                query.Append(" and ");
            }

            if (MonthListBox.SelectedValue != string.Empty)
            {
                query.Append("(");   
                foreach (ListItem item in MonthListBox.Items)
                {                    
                    if (item.Selected)
                    {
                        query.Append("Month='" + item.Text + "'");                        
                        query.Append(" or ");
                    }
                  
                }
                query.Append("1=-1)");
                dv.RowFilter = query.ToString();

                GenerateReport(dv);
            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            dv = ds.vw_StationeryOrderedByCategory.DefaultView;
            GenerateReport(dv);
        }

        private void GenerateReport(DataView dataview)
        {
            rep.SetDataSource(dataview);
            rep.VerifyDatabase();
            CrystalReportViewer1.ReportSource = rep;
        }
    }
}