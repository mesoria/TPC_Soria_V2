﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Soria_v2
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logOut_Click(object sender, EventArgs e)
        {
            Application["Usuario"] = null;
            Application["Persona"] = null;
            Application["Docente"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }
}