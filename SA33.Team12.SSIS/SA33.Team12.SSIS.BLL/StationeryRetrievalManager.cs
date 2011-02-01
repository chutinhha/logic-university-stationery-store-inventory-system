/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/


using System.Collections.Generic;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.BLL
{
    public class StationeryRetrievalManager : BusinessLogic
    {
        private StationeryRetrievalDAO stationeryRetrievalDAO;

        public StationeryRetrievalManager()
        {
            stationeryRetrievalDAO = new StationeryRetrievalDAO();
        }

        public void CreateStationeryRetrievalForm(List<Requisition> requisitions)
        {
            List<Stationery> stationeries = new List<Stationery>();


            // Get all the requisition items from the requisitions
            List<RequisitionItem> requisitionItems = new List<RequisitionItem>();
            foreach (Requisition requisition in requisitions)
            {
                foreach(RequisitionItem requisitionItem in requisition.RequisitionItems)
                {
                    requisitionItems.Add(requisitionItem);
                }
            }

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
    }
}
