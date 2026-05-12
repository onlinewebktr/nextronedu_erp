<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="Delete_Subject_Mapping.aspx.cs" Inherits="school_web.Dvlpr_Prof.Delete_Subject_Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Delete Subject Mapping
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
                        Delete Subject Mapping
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

                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 35%;">
                                    <tr>
                                        <td>Admission No.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
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
                                                <td colspan="8"><b>Subject List</b>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <sup><b>Note :-If any marks is alloted with the particular subject, then the marks will be auto deleted along with the subject.</b></sup>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>

                                                    <asp:GridView ID="grd_fee" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable"   ShowFooter="false">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Subject Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name") %>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                          
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Sub_id" runat="server" Text='<%#Bind("Sub_id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>' Visible="false"></asp:Label>
                                                                     <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id") %>' Visible="false"></asp:Label>
                                                                    <asp:Button ID="btn_Subject" runat="server" Text="Delete Subject" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_Subject_Click" OnClientClick="return confirm('Are you sure you want to delete this?');" />
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
