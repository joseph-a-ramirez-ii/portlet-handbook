<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default_View.ascx.cs" Inherits="CUS.ICS.Handbook.Default_View" %>
<%@ Register 
    TagPrefix="common"
	assembly="Jenzabar.Common"
	Namespace="Jenzabar.Common.Web.UI.Controls"
%>

<div id="divAdminLink" runat="server" visible="false">
	<table class="GrayBordered" width="50%" align="center" cellpadding="3" border="0">
		<tr>
			<td align="center"><IMG title="" alt="*" src="UI\Common\Images\PortletImages\Icons\portlet_admin_icon.gif">&nbsp;<common:globalizedlinkbutton OnClick="glnkAdmin_Click" id="glnkAdmin" runat="server" TextKey="TXT_CS_ADMIN_THIS_PORTLET"></common:globalizedlinkbutton></td>
		</tr>
	</table>
</div>
<div class="pSection">
<common:ErrorDisplay id="PageErrors" runat="server" />
    <br />
    <common:ErrorDisplay visible="false" ID="lblError" runat="server" />
    <common:GlobalizedLabel id="HandbookLetter" runat="server" TextKey="CUS_HANDBOOK_MESSAGE" />
    <asp:CheckBox ID="chkAccept" runat="server" Text="I Accept" /> &nbsp;
    <asp:Label ID="IDLabel" runat="server" Text="ID#:" />
    <asp:TextBox ID="IDText" Columns="9" runat="server" ReadOnly="true" /> &nbsp;
    <asp:Label ID="lblName" runat="server" Text="Full Name:" />
    <asp:TextBox Columns="30" ID="txtName" runat="server" />
    <br />
    <br />
    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
    <asp:Label ID="lblComplete" runat="server" />
</div>