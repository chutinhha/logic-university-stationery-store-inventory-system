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
            RequisitionDAO rq = new RequisitionDAO(); 
            CatalogDAO cat = new CatalogDAO();           

            //set the properties of requisition object            
            r.DepartmentID = user.GetDepartmentByID(1).DepartmentID;
            r.CreatedBy = user.GetUserByID(1).UserID;
            r.ApprovedBy = user.GetUserByID(2).UserID;
            r.UrgencyID = rq.GetUrgencyByID(new Urgency() { UrgencyID = 1 }).UrgencyID;
            r.RequisitionForm = "test";
            r.DateRequested = DateTime.Now;
            r.DateApproved = DateTime.Now;

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
                Description = "tes"
            };


            //Add Child objects of the requisition objects
            //Add requisitionitem to requisition object
            r.RequisitionItems.Add(rqi);

            //Add specialitem to requisition object
            r.SpecialRequisitionItems.Add(spi);

            //Persist requisition to database
            //EF is very intelligent. It will also persist to requistionitem and specialrequistionitem
            rq.CreateRequisition(r);


            DAL.DTO.RequisitionSearchDTO rsearch = new DAL.DTO.RequisitionSearchDTO()
            {
                RequisitionID = r.RequisitionID,
                StartDateRequested = DateTime.Now,        
                ExactDateRequested = DateTime.Now

            };

            //Testing databinding after creation of requisitions
            if (!IsPostBack)
            {
                if (r != null)
                {
                    GridView1.DataSource = rq.GetRequisitionByEmployee(user.GetUserByID(1), rsearch);
                    GridView1.DataBind();
                }

                if (r != null)
                {
                    GridView2.DataSource = rq.FindRequisitionByCriteria(rsearch);
                    GridView2.DataBind();
                }
            }

        }
    }
}