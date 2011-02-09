using System;
using System.Web.UI;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Administration
{
    public partial class MaintainCollectionPoint : AppCode.PageBase
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.CollectionPointGridView.EnableDynamicData(typeof(CollectionPoint));
            this.CollectionPointDetailView.EnableDynamicData(typeof(CollectionPoint));
            this.DynamicDataManager.RegisterControl(this.CollectionPointGridView);
            this.DynamicDataManager.RegisterControl(this.CollectionPointDetailView);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}