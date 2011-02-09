using System;

namespace SA33.Team12.SSIS
{
    public partial class GenericErrorPage : AppCode.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Exception exception = Server.GetLastError();
                if(exception != null)
                {
                    this.ErrorMessage.Text = exception.Message;
                    while(exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                        this.ErrorMessage.Text += "<br />" + exception.Message;
                    }
                }
                Server.ClearError();
            }
        }
    }
}