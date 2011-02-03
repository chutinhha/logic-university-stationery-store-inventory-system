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
          
        }

        public List<StationeryRetrievalForm> GetAllStationeryRetrievalForms()
        {
            return stationeryRetrievalDAO.GetAllStationeryRetrievalForms();
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
