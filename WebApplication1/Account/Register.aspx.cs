using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace Presentation.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard_CreatedUser(object sender, EventArgs e)
        {
            GameMaster.AddUserNameToQuizDb(CreateUserWizard.UserName);
        }
    }
}