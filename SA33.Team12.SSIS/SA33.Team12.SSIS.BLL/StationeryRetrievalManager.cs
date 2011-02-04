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

        public StationeryRetrievalForm CreateStationeryRetrievalForm(List<Requisition> requisitions)
        {
            return null;
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

        public void UpdateReceivedQuantity(StationeryRetrievalForm stationeryRetrievalForm)
        {
            stationeryRetrievalDAO.UpdateReceivedQuantity(stationeryRetrievalForm);
        }

        public void UpdateActualQuantity(StationeryRetrievalForm stationeryRetrievalForm)
        {
            stationeryRetrievalDAO.UpdateActualQuantity(stationeryRetrievalForm);
        }

        public List<StationeryRetrievalFormItem> FindStationeryRetrievalFormItemsByCriteria(StationeryRetrievalFormItemSearchDTO criteria)
        {
            return stationeryRetrievalDAO.FindStationeryRetrievalFormItemsByCriteria(criteria);
        }
    }
}
