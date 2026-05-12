<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="View_Added_Lesson.aspx.cs" Inherits="school_web.InstructorProfile.View_Added_Lesson" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">View Added Lesson  
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
                        <asp:Literal ID="ltUsertop" runat="server">View Added Lesson  </asp:Literal>

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
    
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                    
                            <div class="form-row">
                            <div class="col-md-1">
                                <label>Class</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddl_SearchCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_SearchCategory_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                             <div class="col-md-2">
                                <label>Section</label>
                            </div>
                             <div class="col-md-3">
                                <asp:DropDownList ID="ddl_search_section" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btn_Find" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_Find_Click" Style="margin-top: 3px" />
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
                                    <th>Lesson No.</th>
                                    <th>Lesson Name</th>
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
                                                <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                                <asp:HiddenField ID="hdCategoryID" runat="server" Value='<%#Bind("CategoryID") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section_Subject" runat="server" Text='<%#Bind("Section_Subject") %>'></asp:Label>
                                               
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CourseName" runat="server" Text='<%#Bind("CourseName") %>'></asp:Label>
                                                <asp:HiddenField ID="hdCourseID" runat="server" Value='<%#Bind("CourseID") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_LessonNo" runat="server" Text='<%#Bind("LessonNo") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_SetionName" runat="server" Text='<%#Bind("SetionName") %>'></asp:Label>
                                                <asp:Label ID="lbl_SectionID" runat="server" Text='<%#Bind("SectionID") %>' Visible="false"></asp:Label>
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
