<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ClassMaster.aspx.cs" ValidateRequest="false" Inherits="school_web.LMS_VC_Admin.ClassMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
      Class List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-network icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">  Class List</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
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
        <div class="row">
            <div class="col-lg-4" style="display:none">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Add Class</h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label>Class Name:</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_Category" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlhide" runat="server" Visible="false">
                                    <div class="position-relative form-group">
                                        <label>Class Details:</label>
                                        <div class="input-group">
                                            <textarea id="txt_info" runat="server" name="area" style="min-height: 200px; width: 100% !important;"></textarea>
                                        </div>
                                    </div>
                                    <div class="position-relative form-group">
                                        <label>Upload Class Image</label>
                                        <div class="input-group input-group-icon">
                                            <asp:FileUpload ID="fl_Photo" runat="server" />
                                            <asp:HiddenField ID="Hd_Photo" runat="server" />
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="card-footer pull-right">
                                    <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <%--<h5 class="card-title">All Added Class</h5>--%>
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Class Name</th>
                                    <%-- <th>Description</th>
                                    <th>Image</th>--%>
                                    <%--<th>Action</th>--%>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>

                                                <asp:LinkButton ID="LinkBtnEdit" OnClick="LinkBtnEdit_Click" runat="server" Visible="false">View</asp:LinkButton>
                                                <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description") %>' Visible="false"></asp:Label>

                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#Bind("Image") %>' Width="80px" Visible="false"></asp:Image>
                                            </td>
                                         <%--   <td  >
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnkEdit" Visible="false" runat="server" CssClass="dropdown-item" OnClick="lnkEdit_Click"><i class="dropdown-icon lnr-inbox"></i><span>Edit</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Delete" Visible="false" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Class Description</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <asp:Literal ID="ltDescription" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
    <div id="fadeup"></div>
</asp:Content>
