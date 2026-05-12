<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Syllabus_Chapter_And_Subchapte_List.aspx.cs" Inherits="school_web.LMS_VC_Admin.Syllabus_Chapter_And_Subchapte_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Chapter List
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
                        <asp:Literal ID="ltUsertop" runat="server">Chapter List</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                    <a href="Syllabus_Chapter_And_Subchapte.aspx" class="btn-shadow btn btn-info">
                        <span class="btn-icon-wrapper pr-2 opacity-7">
                            <i class="pe-7s-plus fa-w-20"></i>
                        </span>
                        Add Syllabus Chapter 
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
                                <label>Session</label>
                                <asp:DropDownList ID="ddl_sseion" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>Term</label>
                                <asp:DropDownList ID="ddl_term" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_term_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <label>Class</label>
                                <asp:DropDownList ID="ddl_CourseCat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <label>Subject</label>
                                <asp:DropDownList ID="ddl_subject" class="form-control" runat="server"></asp:DropDownList>
                            </div>


                            <div class="col-md-2">
                                <asp:Button ID="btn_find" runat="server" Style="margin: 29px 0px 0px 0px; padding: 6px 15px 7px;"
                                    Text="Find" CssClass="btn btn-primary" OnClick="btn_find_Click" />
                            </div>
                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Session</th>
                                    <th>Teram</th>
                                    <th>Class</th>
                                    <th>Subject</th>
                                    <th>Sub-Subject</th>
                                    <th>Chapter Name</th>
                                    <th>Subchapter Name</th>
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
                                                <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session") %>'></asp:Label>


                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Term_Name" runat="server" Text='<%#Bind("Term_Name") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("Course_Name") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Sub_Subject_name" runat="server" Text='<%#Bind("SubSubjName") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Chapter_Name" runat="server" Text='<%#Bind("Chapter_Name") %>'></asp:Label>

                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Subchapter_Name" runat="server" Text='<%#Bind("SubChapterName") %>'></asp:Label>

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
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
