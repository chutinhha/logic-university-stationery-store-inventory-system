using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Test
{
    public partial class StationeryRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Create instance of new requisition
            Requisition r = new Requisition();
         
            UserDAO user = new UserDAO();
            RequisitionManager rq = new RequisitionManager(); 
            CatalogDAO cat = new CatalogDAO();           

            //set the properties of requisition object            
            r.DepartmentID = user.GetDepartmentByID(1).DepartmentID;
            r.CreatedBy = user.GetUserByID(1).UserID;
            r.StatusID = rq.GetStatusByID(new Status() { StatusID = 1}).StatusID;
            r.ApprovedBy = user.GetUserByID(2).UserID;
            r.UrgencyID = rq.GetUrgencyByID(new Urgency() { UrgencyID = 1 }).UrgencyID;
            r.RequisitionForm = "test";
            r.DateRequested = DateTime.Now;
            //r.DateApproved = DateTime.Now;

            //Create a new requisitionitem for the current requisition
            RequisitionItem rqi = new RequisitionItem()
            {
                RequisitionID = r.RequisitionID,
                StationeryID = cat.GetAllStationery().FirstOrDefault<Stationery>().StationeryID,
                QuantityRequested = 10,
                QuantityIssued = 10,
                Price = 5                

            };


            //Create a new specialrequisitionitem for the current requisition
            SpecialRequisitionItem spi = new SpecialRequisitionItem()
            {
                RequisitionID = r.RequisitionID,
                SpeicalStationeryID = 1,
                QuantityRequested = 10,
                QuantityIssued = 10,
                Price = 5,
                Name = "arav",
                Description = "test"           
                
            };


            //Add Child objects of the requisition objects
            //Add requisitionitem to requisition object
            r.RequisitionItems.Add(rqi);

            //Add specialitem to requisition object
            r.SpecialRequisitionItems.Add(spi);

            //Persist requisition to database
            //EF is very intelligent. It will also persist to requistionitem and specialrequistionitem
            rq.CreateRequisition(r);

            r = rq.GetAllRequisition().Last<Requisition>();
            r.RequisitionItems.Last<RequisitionItem>().QuantityIssued = 6;

           
            rq.UpdateRequisition(r);

            spi = rq.GetAllSpecialRequisitionItems(r).Last<SpecialRequisitionItem>();

            rq.DeleteSpecialRequisitionItem(spi);

            //Testing databinding after creation of requisitions
            if (!IsPostBack)
            {
                GridView1.DataSource = rq.GetAllRequisitionItems(r);
                GridView1.DataBind();

                if (r != null)
                {
                    GridView2.DataSource = rq.GetAllRequisition();
                    GridView2.DataBind();
                }
            }

        }
    }
}