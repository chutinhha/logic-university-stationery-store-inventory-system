/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/


using System.Collections.Generic;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.BLL
{
    public class StationeryRetrievalManager : BusinessLogic
    {
        private StationeryRetrievalDAO stationeryRetrievalDAO;

        public StationeryRetrievalManager()
        {
            stationeryRetrievalDAO = new StationeryRetrievalDAO();
        }

        public StationeryRetrievalForm CreateStationeryRetrievalFormByAllRequisitions(User createdBy)
        {
            return stationeryRetrievalDAO.CreateStationeryRetrievalForm(createdBy, true, "");
        }

        public StationeryRetrievalForm CreateStationeryRetrievalForm(User createdBy, List<Requisition> requisitions)
        {
            string requisitionIds = string.Empty;
            for (int i = 0; i < requisitions.Count-1; i++)
            {
                Requisition r = requisitions[i];
                requisitionIds += r.RequisitionID + ",";
            }
            requisitionIds += requisitions[requisitions.Count - 1];
            return stationeryRetrievalDAO.CreateStationeryRetrievalForm(createdBy, false, requisitionIds);
        }

        public List<StationeryRetrievalForm> GetAllStationeryRetrievalForms()
        {
            return stationeryRetrievalDAO.GetAllStationeryRetrievalForms();
        }

        public StationeryRetrievalForm GetStationeryRetrievalFormByID(int stationeryRetrievalFormID)
        {
            return stationeryRetrievalDAO.GetStationeryRetrievalFormByID(stationeryRetrievalFormID);
        }

        public void CreateStationeryRetrievalForm(StationeryRetrievalForm stationeryRetrievalForm)
        {
            stationeryRetrievalDAO.CreateStationeryRetrievalForm(stationeryRetrievalForm);
        }

        public StationeryRetrievalForm UpdateReceivedQuantity(StationeryRetrievalForm stationeryRetrievalForm)
        {
            return stationeryRetrievalDAO.UpdateReceivedQuantity(stationeryRetrievalForm);
        }

        public void UpdateActualQuantity(StationeryRetrievalForm stationeryRetrievalForm)
        {
            stationeryRetrievalDAO.UpdateActualQuantity(stationeryRetrievalForm);
        }

        public List<StationeryRetrievalFormItem> FindStationeryRetrievalFormItemsByCriteria(StationeryRetrievalFormItemSearchDTO criteria)
        {
            return stationeryRetrievalDAO.FindStationeryRetrievalFormItemsByCriteria(criteria);
        }

        public List<StationeryRetrievalFormItemByDept> GetStationeryRetrievalFormItemByDeptsByFormID(int stationeryRetrievalFormID)
        {
            return stationeryRetrievalDAO.GetStationeryRetrievalFormItemByDeptByFormID(stationeryRetrievalFormID);
        }
        public List<vw_GetStationeryRetrievalFormItemByDept> GetVwStationeryRetrievalFormItemByDeptsByFormID(int stationeryRetrievalFormID)
        {
            return stationeryRetrievalDAO.GetVwStationeryRetrievalFormItemByDeptByFormID(stationeryRetrievalFormID);
        }

        public List<StationeryRetrievalForm> FindStationeryRetrievalFormByCriteria(StationeryRetrievalFormSearchDTO criteria)
        {
            return stationeryRetrievalDAO.FindStationeryRetrievalFormByCriteria(criteria);
        }
        
    }
}
