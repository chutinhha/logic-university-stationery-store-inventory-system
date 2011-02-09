using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.Test
{
    public partial class DisbursementTest : AppCode.PageBase
    {
        DisbursementDAO disbursementDAO = new DisbursementDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Disbursement> Disbursements = disbursementDAO.GetAllDisbursement();
            this.GridView1.DataSource = Disbursements;
            this.GridView1.DataBind();
        }

        protected void btnGetDisbursementByID_Click(object sender, EventArgs e)
        {
            int disbursementID = Convert.ToInt32(tbxDisbursementID.Text.ToString());
            Disbursement disbursement = disbursementDAO.GetDisbursementByID(disbursementID);
            List<Disbursement> disbursements=new List<Disbursement>();
            disbursements.Add(disbursement);
            this.GridView1.DataSource= disbursements;
            this.GridView1.DataBind();
        }

        protected void GetDisbursementByCriteria_Click(object sender, EventArgs e)
        {
            DisbursementSearchDTO searchCriteria = new DisbursementSearchDTO();
            searchCriteria.CreatedBy = Convert.ToInt32(txbCreateBy.Text.ToString());
            List<Disbursement> disbursements = disbursementDAO.FindDisbursementByCriteria(searchCriteria);
            GridView1.DataSource = disbursements;
            GridView1.DataBind();
        }

        protected void btnUpdateDisbursement_Click(object sender, EventArgs e)
        {
            int disbursementID = Convert.ToInt32(tbxDisbursementID.Text.ToString());
            Disbursement disbursement = disbursementDAO.GetDisbursementByID(disbursementID);
            disbursement.CreatedBy = Convert.ToInt32(txbCreateByforUpdate.Text.ToString());
            disbursement.StationeryRetrievalFormID = Convert.ToInt32(txbSRFID.Text.ToString());
            Disbursement newDisbursement = disbursementDAO.UpdateDisbursement(disbursement);
            List<Disbursement> disbursements = new List<Disbursement>();
            disbursements.Add(newDisbursement);
            GridView1.DataSource = disbursements;
            GridView1.DataBind();
        }

        protected void btnDeleteDisbursement_Click(object sender, EventArgs e)
        {
            int disbursementID = Convert.ToInt32(tbxIDForDelete.Text.ToString());
            Disbursement disbursement = disbursementDAO.GetDisbursementByID(disbursementID);
            disbursementDAO.DeleteDisbursement(disbursement);
            List<Disbursement> list = disbursementDAO.GetAllDisbursement();
            GridView1.DataSource = list;
            GridView1.DataBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            int disbursementID = Convert.ToInt32(tbxDisbursementID.Text.ToString());
            Disbursement disbursement = disbursementDAO.GetDisbursementByID(disbursementID);
            Disbursement newDisbursement = new Disbursement();
            newDisbursement.DateCreated = disbursement.DateCreated;
            newDisbursement.CreatedBy = disbursement.CreatedBy;
            newDisbursement.StationeryRetrievalFormID = disbursement.StationeryRetrievalFormID;
            Disbursement createdDisbursement = disbursementDAO.CreateDisbursement(newDisbursement);
            List<Disbursement> Items = new List<Disbursement>();
            Items.Add(createdDisbursement);
            GridView1.DataSource = Items;
            GridView1.DataBind();
        }

  

     
    }
}