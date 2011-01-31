﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SA33.Team12.SSIS
{
    public partial class GenericErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Exception exception = Server.GetLastError();
                if(exception != null)
                {
                    this.ErrorMessage.Text = exception.Message;
                }
            }
        }
    }
}