<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" ValidateRequest="false" Inherits="school_web.LMS_VC_Admin.AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Add User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function openModal() {

            $("#modalConfirm").modal('show');
        }
    </script>
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
                        <asp:Literal ID="ltUsertop" runat="server">Add User</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                    <%--  <a href="Category_List.aspx" class="btn-shadow btn btn-info">
                        <span class="btn-icon-wrapper pr-2 opacity-7">
                            <i class="pe-7s-plus fa-w-20"></i>
                        </span>
                        View All Category
                    </a>--%>
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-lg-4">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Add User</h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label>User Name</label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_UserName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Password</label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_Pswd" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Name</label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_Name" runat="server" CssClass="form-control"> </asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Email Id:</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_EmailID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Designation:</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_designation" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Posted Office:</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_postedin" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">All Added User</h5>
                        <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_No" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_UserID" runat="server" Text='<%#Bind("UserID") %>'></asp:Label>
                                        <asp:HiddenField ID="hdUserID" runat="server" Value='<%#Bind("UserID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Password">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Password" runat="server" Text='<%#Bind("Password") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Email" runat="server" Text='<%#Bind("EmailID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Designation" runat="server" Text='<%#Bind("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Office">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Office" runat="server" Text='<%#Bind("Office") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnActive" OnClientClick="OnProgress(this.id)" CssClass="btn btn-success btn-sm" runat="server" Text='<%# Bind("StatusD") %>' CommandName="Istatus" CommandArgument='<%# Container.DataItemIndex %>' />
                                        <asp:HiddenField ID="hdfActive" runat="server" Value='<%# Bind("Istatus") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" BackColor="Red" OnClientClick="OnProgress(this.id)" CssClass="btn btn-success btn-sm" runat="server" Text="Delete" CommandName="IsDelete" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdId" runat="server" Value='<%#Bind("Id") %>' />
                                        <asp:LinkButton ID="lnk_View" runat="server" Visible="false" OnClick="lnk_View_Click">View Details</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#880300" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                    </div>
                </div>
            </div>


        </div>
    </div>
    <div id="modalConfirm" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h5 class="modal-title">User Details</h5>
                </div>
                <div class="modal-body">
                    <asp:Literal ID="ltDescription" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
