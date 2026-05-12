<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Upload_E_book.aspx.cs" ValidateRequest="false" Inherits="school_web.LMS_VC_Admin.Upload_E_book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Upload E-Book
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-group {
            padding: 0px!important;
            margin-bottom: 15px!important;
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
                        <asp:Literal ID="ltUsertop" runat="server">Upload E-Book</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                    <a href="Viewaddedebook.aspx" class="btn-shadow btn btn-info">
                        <span class="btn-icon-wrapper pr-2 opacity-7">
                            <i class="pe-7s-plus fa-w-20"></i>
                        </span>
                        View Added E-Book
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
        <asp:HiddenField ID="hd_id" runat="server" />
        <div class="row">
            <div class="col-lg-6">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Upload E-Book</h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label>Class</label>
                                    <asp:DropDownList ID="ddl_CourseCat" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Section</label>
                                    <asp:DropDownList ID="ddl_section" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Subject Name</label>
                                    <asp:DropDownList ID="ddl_Course" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="position-relative form-group">
                                    <label>E-Book Name</label>
                                    <asp:TextBox ID="txt_ebookname" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Choose E-Book Cover Image</label>
                                    <asp:FileUpload ID="fl_images" runat="server" class="form-control" />
                                    <asp:HiddenField ID="hdAudio" runat="server" />
                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Choose Document <sup>(.pdf)(10 MB)</sup></label>
                                    <asp:FileUpload ID="fl_Document" runat="server" class="form-control" />
                                    <asp:HiddenField ID="Hd_Document" runat="server" />
                                </div>

                                <div class="card-footer pull-right">

                                    <asp:Button ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" class="btn btn-primary" Text="Submit" />
                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <asp:HiddenField ID="hddId" runat="server" />

                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Last Top 10 Upload E-Book</h5>
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
