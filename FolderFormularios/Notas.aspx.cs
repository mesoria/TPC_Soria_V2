using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Soria_v2.FolderFormularios
{
    public partial class Notas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnVolver.Attributes.Add("onclick", "history.back(); return false;");
        }
    }
}