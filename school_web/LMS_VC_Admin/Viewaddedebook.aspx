<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Viewaddedebook.aspx.cs" Inherits="school_web.LMS_VC_Admin.Viewaddedebook" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    View Added E-Book  
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
                        <asp:Literal ID="ltUsertop" runat="server">View Added E-Book</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                    <a href="Upload_E_book.aspx" class="btn-shadow btn btn-info">
                        <span class="btn-icon-wrapper pr-2 opacity-7">
                            <i class="pe-7s-plus fa-w-20"></i>
                        </span>
                        Add E-book
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

        <div class="clearfix"></div>
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-md-2">
                                <label>Select Class</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddl_CourseCat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>

                            </div>
                             <div class="col-md-2">
                                <label>Select Section</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddl_section" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>

                            </div>


                            <div class="col-md-1"></div>
                            <div class="col-md-3">
                                <asp:Label ID="lbl_total_student" runat="server"></asp:Label>
                            </div>

                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Class</th>
                                    <th>Section</th>

                                    <th>Subject</th>
                                    <th>E-Book Name</th>
                                    <th>Cover Image</th>
                                    <th>Document</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CourseName" runat="server" Font-Names="Arial" Text='<%#Bind("CourseName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Book_Name" runat="server" Font-Names="Arial" Text='<%#Bind("Book_Name") %>'></asp:Label>
                                            </td>
                                            <td><a href='<%#Eval("Cover_Photo") %>' target="_blank">View image
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Cover_Photo") %>' Width="100px" Visible="false" /></a> </td>
                                            <td><a href='<%#Eval("Path_of_ebook") %>' target="_blank">View Document</a>
                                                <asp:Label ID="lbl_Path_of_ebook" runat="server" Font-Names="Arial" Text='<%#Bind("Path_of_ebook") %>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="dropdown-item" OnClick="lnkEdit_Click"><i class="dropdown-icon lnr-inbox"></i><span>Edit</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>
                                                            <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
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

</asp:Content>
