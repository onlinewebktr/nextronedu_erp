<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Request_new_worldline.aspx.cs" Inherits="school_web.Online_Payment_admission.worldline.Request_new_worldline" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblResponse" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblCheckSum" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td>Request Type
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_requesttype" runat="server" ReadOnly="true">T</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Merchant Code
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_merchantcode" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Merchant Transaction Reference Number
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_MerchantTxnRefNo" runat="server" ReadOnly="true"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>ITC
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_ITC" runat="server" ReadOnly="true">Saleel_K</asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>Amount
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_Amount" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Currency code
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_Currencycode" runat="server" ReadOnly="true">INR</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Unique Customer ID
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_uniqueCustomerID" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>Return URL
                        </td>
                        <td>
                             <asp:TextBox ID="TXT_returnURL" runat="server"> </asp:TextBox>

                          <%--  <asp:TextBox ID="TXT_returnURL" runat="server">http://localhost:51684/ResponsePage.aspx</asp:TextBox>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>StoS Return URL
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_StoSreturnURL" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>TPSL Transaction ID
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_TPSLTXNID" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Shopping Cart details
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_Shoppingcartdetails" runat="server" ReadOnly="true">FIRST</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                              <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Txn Date
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_TxnDate" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Email
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_Email" runat="server" ReadOnly="true"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Mobile No
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_mobileNo" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>Bank code
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_Bankcode" runat="server">470</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Customer Name
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_customerName" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trProprty" runat="server" visible="false">
                        <td>Property Path
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_propertyPath" runat="server">D:\DotnetIntegrationKit\T3348\Merchant.property</asp:TextBox>
                        </td>
                    </tr>
                    <tr id="Tr1" runat="server">
                        <td>Card Holder Name
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_CardHolderName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td>Card Number
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_CardNumber" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="Tr3" runat="server">
                        <td>Expiry Month
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_ExpiryMonth" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="Tr4" runat="server">
                        <td>Expiry Year
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_ExpiryYear" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="Tr5" runat="server">
                        <td>CVV
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_CVV" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trCardId" runat="server" visible="true">
                        <td>Card ID
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_CardID" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trMMID" runat="server" visible="true">
                        <td>MMID
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_MMID" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trRegisteredMobileNumber" runat="server" visible="true">
                        <td>Registered Mobile Number
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_RegisteredMobileNumber" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trOTP" runat="server" visible="true">
                        <td>OTP
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_OTP" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr id="trKey" runat="server" visible="false">
                        <td>IsKey
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_IsKey" runat="server" ReadOnly="true"> </asp:TextBox> 
                        </td>
                    </tr>
                    <tr id="trIv" runat="server" visible="false">
                        <td>IsIv
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_IsIv" runat="server" ReadOnly="true"> </asp:TextBox> 
                        </td>
                    </tr>


                    <tr id="trAccountNo" runat="server" visible="true">
                        <td>ACCOUNT NO.
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_AccountNo" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                          
                        </td>
                    </tr>
                    
                </table>
            </div>
        </div>
    </form>
</body>
</html>
