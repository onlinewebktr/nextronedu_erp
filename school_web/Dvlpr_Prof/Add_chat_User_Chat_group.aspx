<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="Add_chat_User_Chat_group.aspx.cs" Inherits="school_web.Dvlpr_Prof.Add_chat_User_Chat_group" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Add Group Chat
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
                        Deleted Bill History
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

                            <div class="col-md-7">

                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                    <tr>


                                        <td>

                                            <asp:Button ID="btn_teachingstaff" runat="server" Text="Teaching Staff" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_teachingstaff_Click" />
                                        </td>

                                        <td>

                                            <asp:Button ID="btn_non_teaching_staff" runat="server" Text="Non Teaching Staff" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_non_teaching_staff_Click" />
                                        </td>

                                          <td>

                                            <asp:Button ID="btn_Administrative" runat="server" Text="Administrative Staff" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_Administrative_Click" />
                                        </td>


                                    </tr>

                                </table>


                            </div>
                            

                            <div class="col-md-12">
                                <div style="margin: 0px; float: left; height: auto; width: 100%;">
                                    <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnl_payment_history" runat="server" visible="false">
                                        <table class="table">

                                            <tr>
                                                <td  ><b><asp:Label ID="lbl_groupname" runat="server"  ></asp:Label> </b>

                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td  >
                                                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>

                                                    <asp:GridView ID="grd_view" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable" ShowFooter="True">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_name" runat="server" Text='<%#Bind("name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="User Id">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_userid" runat="server" Text='<%#Bind("user_id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="User Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_User_Type" runat="server" Text='<%#Bind("User_Type") %>'></asp:Label>
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
