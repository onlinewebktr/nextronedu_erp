<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="Delete_Bill.aspx.cs" Inherits="school_web.Dvlpr_Prof.Delete_Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Delete Student Monthly Bill
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
            <div class="col-lg-10">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title"></h5>
                        <div class="form-row">
                            <div class="col-md-12">

                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 65%;">
                                    <tr>
                                        <td>Admission No.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </td>

                                        <td>Select Session
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_session" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td> 
                                        <td> 
                                            <%--<asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click" />--%>
                                        </td> 
                                    </tr> 
                                </table>

                                <div style="margin: 0px; float: left; height: auto; width: 100%;">
                                    <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnl_payment_history" runat="server" visible="false">
                                        <table class="table">

                                            <tr>
                                                <td colspan="8"><b>Payment History</b>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>

                                                    <asp:GridView ID="grd_fee" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable" OnRowDataBound="grd_fee_RowDataBound" ShowFooter="True">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Slip No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_slipno" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_Addmission_no" runat="server" Text='<%#Bind("Addmission_no") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Branchid" runat="server" Text='<%#Bind("Branch") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Month Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_monthnam" runat="server"></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Payment Mode">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_paymenetmode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Type") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterTemplate>Total</FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td2" Width="100px" />
                                                                <HeaderStyle CssClass="td2" />
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lbl_totalamount" runat="server" Font-Bold="true"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Transection_in" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Transection_in") %>'> </asp:Label>

                                                                    <asp:Button ID="btn_delete_bill" Visible="false" runat="server" Text="Delete Bill" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_delete_bill_Click" OnClientClick="return confirm('Are you sure you want to delete this?');" />


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
