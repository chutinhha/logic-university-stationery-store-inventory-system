/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.ComponentModel;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.BLL
{
    public class DisbursementManager : SA33.Team12.SSIS.BLL.BusinessLogic
    {
        private DisbursementDAO disbursementDAO;

        public DisbursementManager()
        {
            disbursementDAO = new DisbursementDAO();
        }

        public void CreateDisbursement(Disbursement disbursement)
        {
            disbursementDAO.CreateDisbursement(disbursement);
        }

        public void CreateDisbursementFromSRF()
        {
            disbursementDAO.CreateDisbursementFromSRF();
        }

        public void CancelDisbursement()
        {
            disbursementDAO.CancelDisbursement();
        }

        public void UpdateDisbursement(Disbursement disbursement,int newQuantity)
        {
            disbursementDAO.UpdateDisbursementQuantity(disbursement,newQuantity);
        }
    }
}
