<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Added_Sub_Subject_list.aspx.cs" Inherits="school_web.LMS_VC_Admin.Added_Sub_Subject_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-notebook icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Sub-Subject List</asp:Literal>

                    </div>
                </div>
                 <div class="page-title-actions">
                    <a href="Add_Sub_Subject.aspx" class="btn-shadow btn btn-info">
                        <span class="btn-icon-wrapper pr-2 opacity-7">
                            <i class="pe-7s-plus fa-w-20"></i>
                        </span>
                        Add Sub-Subject
                    </a>
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

            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">


                        <div class="form-row">
                            <div class="col-md-1">
                                <label><b>Select Class</b></label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddl_SearchCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btn_Find" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_Find_Click" Style="margin-top: 3px" />
                            </div>
                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Class Name</th>
                                    <th>Subject Name</th>
                                    <th>Sub Subject Name</th>
                                    <th>Sub Subject Position</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_Sub_Subject_Master" runat="server" Text='<%#Bind("Sub_Subject_name")%>'></asp:Label>
                                            </td>

                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_Subject_position" runat="server" Text='<%#Bind("Sub_Subject_position")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnkEdit"   runat="server" CssClass="dropdown-item" OnClick="lnkEdit_Click"><i class="dropdown-icon lnr-inbox"></i><span>Edit</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Delete"  runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>
                                                            <asp:Label ID="lbl_Subject_id" runat="server" Text='<%#Bind("Subject_id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("course_id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Sub_Subject_id" runat="server" Text='<%#Bind("Sub_Subject_id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
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
                    <h5 class="modal-title">Subject Description</h5>
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
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
