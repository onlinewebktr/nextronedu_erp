<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="syllabus.aspx.cs" Inherits="school_web.LMS_VC_Admin.syllabus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Syllabus
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-group {
            padding: 0px !important;
            margin-bottom: 15px !important;
        }
    </style>
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
                        <asp:Literal ID="ltUsertop" runat="server">Upload Syllabus</asp:Literal>
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
        <asp:HiddenField ID="hd_id" runat="server" />
        <div class="row">
            <div class="col-lg-4">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Upload Syllabus</h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label>Session</label>
                                    <asp:DropDownList ID="ddl_session" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Class</label>
                                    <asp:DropDownList ID="ddl_class" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Section</label>
                                    <asp:DropDownList ID="ddl_section" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Syllabus Details</label>
                                    <asp:TextBox ID="txt_syllabus_details" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Choose Document <sup>(.pdf)(10 MB)</sup></label>
                                    <asp:FileUpload ID="fl_Document" runat="server" class="form-control" />
                                    <asp:HiddenField ID="Hd_Document" runat="server" />
                                </div>
                                <div class="card-footer pull-right">
                                    <asp:HiddenField ID="hddId" runat="server" />
                                    <asp:Button ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" class="btn btn-primary" Text="Submit" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-md-2">
                                <label>Session</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddl_session_search" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn btn-primary" Text="Find" />
                            </div>
                        </div>
                        <hr />
                        <h5 class="card-title">Upload Syllabus List</h5>
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Syllabus Details</th>
                                    <th>Syllabus</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Course_Name") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_syllabus_info" runat="server" Font-Names="Arial" Text='<%#Bind("Syllabus_info") %>'></asp:Label>
                                            </td>
                                            <td><a href='<%#Eval("Syllabus_filepath") %>' target="_blank">View</a>
                                                <asp:Label ID="lbl_Path_of_ebook" runat="server" Font-Names="Arial" Text='<%#Bind("Syllabus_filepath") %>' Visible="false"></asp:Label>
                                            </td>


                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>

                                                            <%--<asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click"><i class="dropdown-icon lnr lnr-pencil"></i><span>Edit</span></asp:LinkButton>--%>
                                                             

                                                            <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
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
