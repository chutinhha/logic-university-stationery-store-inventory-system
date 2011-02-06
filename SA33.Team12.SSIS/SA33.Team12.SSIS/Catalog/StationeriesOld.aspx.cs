/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Catalog
{
    public partial class StationeriesOld : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
           // this.DyanamicDataManager.RegisterControl(this.StationeryGridView);
          //  this.StationeryGridView.EnableDynamicData(typeof(Stationery));
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}