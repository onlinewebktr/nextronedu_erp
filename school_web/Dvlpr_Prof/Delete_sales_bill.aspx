<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="Delete_sales_bill.aspx.cs" Inherits="school_web.Dvlpr_Prof.Delete_sales_bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Delete sales bill
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        Delete Student Monthly Bill
                    </div>
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title"></h5>
                        <div class="form-row">
                            <div class="col-md-12">

                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 65%;">
                                    <tr>
                                        <td>Invoice Number
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_invoice_number" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </td>






                                        <td>

                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click" />
                                        </td>






                                    </tr>

                                </table>

                                <div style="margin: 0px; float: left; height: auto; width: 100%;">
                                    <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnl_payment_history" runat="server" visible="false">
                                        <table class="table">

                                            <tr>
                                                <td colspan="8"><b>Bill History</b>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>

                                                    <asp:GridView ID="grd_fee" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable" ShowFooter="false">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lbl_CustomerName" runat="server" Text='<%#Bind("party_name")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Admission No./User Id">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_party_id1" runat="server" Text='<%#Bind("party_id")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Invoice No.">
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("Bill_No")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_purchase_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pay Mode">
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lbl_Payment_Mode" runat="server" Text='<%#Bind("Payment_Mode")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>Total</FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lbl_NetPayable" runat="server" Text='<%#Bind("NetPayable","{0:n}") %>'></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td2" Width="100px" />
                                                                <HeaderStyle CssClass="td2" />
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lbl_totalamount" runat="server" Font-Bold="true"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Delete Remarks">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_delete_remarks" TextMode="MultiLine" style="width:100%; height:100px;" runat="server"></asp:TextBox>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lbl_unique_no" runat="server" Text='<%#Bind("unique_no")%>' Visible="false"></asp:Label>

                                                                    <asp:Button ID="btn_delete_bill"  runat="server" Text="Delete Bill" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_delete_bill_Click" OnClientClick="return confirm('Are you sure you want to delete this?');" />


                                                                 <%--   <a class="dropdown-item" href="../Inventory_management/Slip/Print_Sale_slip.aspx?unique_entry_id=<%#Eval("unique_no") %>&partyid=<%#Eval("party_id") %>" target="_blank"><i class='bx bx-printer'></i><span>View slip</span></a>--%>

                                                                     

                                                                    
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>


                                                </td>
                                            </tr>



                                        </table>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>



    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
