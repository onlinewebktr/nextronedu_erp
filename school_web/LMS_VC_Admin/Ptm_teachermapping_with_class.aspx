<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Ptm_teachermapping_with_class.aspx.cs" Inherits="school_web.LMS_VC_Admin.Ptm_teachermapping_with_class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
   Class & Teacher Mapping
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
                        <asp:Literal ID="ltUsertop" runat="server">Class & Teacher Mapping</asp:Literal>

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
            <div class="col-lg-4">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Class & Teacher Mapping</h5>
                        <div class="form-row">
                            <div class="col-md-12">



                                <div class="position-relative form-group">
                                    <label>Select Class</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Select Section</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Select Teacher</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_teacher" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="card-footer pull-right">
                                    <asp:Button ID="btn_submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">All Added Class & Teacher Mapping <a href="Upload_excel_Class_Subject_Mapping.aspx" style="float: right; display: none">Bulk Upload</a></h5>
                        <div class="form-row">
                            <div class="col-md-1">
                                <label>Class </label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddl_SearchCategory" runat="server" CssClass="form-control" Style="width: 150px; float: left;">
                                </asp:DropDownList>


                            </div>
                            <div class="col-md-2" style="text-align: right;">
                                <label>Section</label>
                            </div>
                            <div class="col-md-1">
                                <asp:DropDownList ID="ddl_section_serch" runat="server" CssClass="form-control" Style="width: 100px; float: left;">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2" style="text-align: right;">
                                <label>Teacher</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddl_searchInstructor" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btn_Find" runat="server" Text="Find" CssClass="btn btn-primary mb-3" OnClick="btn_Find_Click" />
                            </div>
                        </div>


                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Class</th>
                                    <th>Section</th>

                                    <th>Teacher Name</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>


                                               
                                                <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("Course_Name") %>'></asp:Label>
                                                <asp:Label ID="lbl_CategoryID" runat="server" Text='<%#Bind("CategoryID") %>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_UserName" runat="server" Text='<%#Bind("UserName") %>'></asp:Label>

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


                                                            <asp:Label ID="lbl_UserID" runat="server" Text='<%#Bind("UserID") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
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

    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
