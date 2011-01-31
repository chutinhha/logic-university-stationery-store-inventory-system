/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.ComponentModel;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using System.Collections.Generic;

namespace SA33.Team12.SSIS.BLL
{
    public class DisbursementManager : SA33.Team12.SSIS.BLL.BusinessLogic
    {
        private DisbursementDAO disbursementDAO;

        public DisbursementManager()
        {
            disbursementDAO = new DisbursementDAO();
        }

        public List<Disbursement> GetAllDisbursement()
        {
           return disbursementDAO.GetAllDisbursement();
        }

        public void CreateDisbursement(Disbursement disbursement)
        {
            disbursementDAO.CreateDisbursement(disbursement);
        }

        public void CreateDisbursementFromSRF(StationeryRetrievalForm SRF)
        {
           // disbursementDAO.CreateDisbursementFromSRF(SRF);
        }

        public void CancelDisbursement()
        {
           // disbursementDAO.CancelDisbursement();
        }

        public void UpdateDisbursement(Disbursement disbursement,int newQuantity)
        {
           // disbursementDAO.UpdateDisbursementQuantity(disbursement,newQuantity);
        }
    }
}
