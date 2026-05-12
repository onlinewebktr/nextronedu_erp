<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Add_Sub_Subject.aspx.cs" Inherits="school_web.LMS_VC_Admin.Add_Sub_Subject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Add Sub Subject 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .dt-button-collection {
            margin-top: 3.6px!important;
        }
    </style>
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
                        <asp:Literal ID="ltUsertop" runat="server"> Add Sub Subject </asp:Literal>

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

                        <div class="form-row">
                            <div class="col-md-12">



                                <div class="position-relative form-group">
                                    <label>Select Class </label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>


                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Select Subject </label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_subject" runat="server" class="form-control"></asp:DropDownList>


                                    </div>
                                </div>





                                <div class="position-relative form-group">
                                    <label>Enter Sub-Subject </label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_subsubject" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>



                                <div class="card-footer pull-right">
                                    <asp:Button ID="btn_submit" runat="server" Text="Add" Style="float: left;" CssClass="btn btn-primary" OnClick="btn_submit_Click" />

                                    <asp:Button ID="btn_cncel" runat="server" Text="Cancel" Style="float: right" CssClass="btn btn-dark" OnClick="btn_cncel_Click" Visible="false" />





                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Last Top 10  Added Sub Subject </h5>

                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Class Name</th>
                                    <th>Subject Name</th>
                                    <th>Sub Subject Name</th>
                                  
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

                                          


                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="dropdown-item" OnClick="lnkEdit_Click"><i class="dropdown-icon lnr-inbox"></i><span>Edit</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>


                                                            <asp:Label ID="lbl_Subject_position" runat="server" Text='<%#Bind("Sub_Subject_position")%>' Visible="false"></asp:Label>
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
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_subjectid" runat="server" />
    <asp:HiddenField ID="hd_sub_subjectid" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
