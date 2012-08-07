using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CUS.ICS.Handbook
{
    public partial class Default_View : Jenzabar.Portal.Framework.Web.UI.PortletViewBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentPortlet.AccessCheck("CanAdminPortlet"))
            {
                divAdminLink.Visible = true;
            }

            if (Jenzabar.Portal.Framework.PortalUser.Current.HostID != null)   
            { 
                //**************************************************
                // if the logged in user has an ID, check for Time
                //**************************************************
                IDText.Text = Jenzabar.Portal.Framework.PortalUser.Current.HostID;

                System.Data.SqlClient.SqlCommand sqlcmdSelectHandbookIDNumber = new System.Data.SqlClient.SqlCommand(
                    "SELECT * FROM TLU_HANDBOOK WHERE"
                    + " ID_NUM = " + Jenzabar.Portal.Framework.PortalUser.Current.HostID,
                    new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["JenzabarConnectionString"].ConnectionString));
                
                try
                {
                    sqlcmdSelectHandbookIDNumber.Connection.Open();
                    System.Data.SqlClient.SqlDataReader sqlReader = sqlcmdSelectHandbookIDNumber.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        txtName.Text = sqlReader["NAME"].ToString();
                        txtName.Enabled = false;
                        IDText.Enabled = false;
                        chkAccept.Checked = sqlReader["AGREED"].ToString().Trim().ToLower().Equals("true");
                        chkAccept.Enabled = false;
                        btnSubmit.Visible = false;
                        this.ParentPortlet.ShowFeedback(Jenzabar.Portal.Framework.Web.UI.FeedbackType.Message, "You have accepted the agreement on: " + sqlReader["SIGN_DTE"].ToString());
                    }
                }
                catch (Exception critical)
                {
                    lblError.ErrorMessage = "Error: Unable to read from database: <BR />" + critical.GetBaseException().Message;
                    lblError.Visible = true;
                }
                finally
                {
                    if (sqlcmdSelectHandbookIDNumber.Connection != null && sqlcmdSelectHandbookIDNumber.Connection.State == ConnectionState.Open)
                    {
                        sqlcmdSelectHandbookIDNumber.Connection.Close();
                    }
                }
            }
            else
            {
                ParentPortlet.ShowFeedbackGlobalized(Jenzabar.Portal.Framework.Web.UI.FeedbackType.Error, "MSG_NO_HOST_ID");
            }
        }


        protected void btnSubmit_Click(Object sender, EventArgs e)
        {
            this.ParentPortlet.State = Jenzabar.Portal.Framework.Web.UI.PortletState.Maximized;

            System.Data.SqlClient.SqlCommand sqlcmdInsert;
            if (chkAccept.Checked == true && txtName.Text.Trim() != String.Empty)
            {
                sqlcmdInsert = new System.Data.SqlClient.SqlCommand(
                    "INSERT INTO TLU_HANDBOOK (ID_NUM, AGREED, NAME, SIGN_DTE) VALUES ("
                    + Jenzabar.Portal.Framework.PortalUser.Current.HostID
                    + "," + "'" + chkAccept.Checked.ToString() + "'"
                    + "," + "'" + txtName.Text.Trim().Replace("'", "''") + "'"
                    + ", GETDATE() )", new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["JenzabarConnectionString"].ConnectionString));

                try
                {
                    sqlcmdInsert.Connection.Open();
                    sqlcmdInsert.ExecuteNonQuery();
                    this.ParentPortlet.NextScreen("Default");
                }
                catch (Exception critical)
                {
                    lblError.ErrorMessage = "Error: Unable to submit data: <BR />" + critical.GetBaseException().Message;
                    lblError.Visible = true;
                }
                finally
                {
                    if (sqlcmdInsert.Connection != null && sqlcmdInsert.Connection.State == ConnectionState.Open)
                    {
                        sqlcmdInsert.Connection.Close();
                    }
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.ErrorMessage = "You must fill in your name, and check the agreement box";
            }
        }

        protected void glnkAdmin_Click(object sender, EventArgs e)
        {
            this.ParentPortlet.NextScreen("Admin");
        }
    }
}