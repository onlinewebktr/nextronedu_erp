<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Markas_Entery_Personality_Traits.aspx.cs" Inherits="school_web.InstructorProfile.Markas_Entery_Personality_Traits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Marks Entry Personality Traits
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table td, .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px !important;
        }

        .modal-backdrop.fade, .fade.blockOverlay {
            opacity: 0;
            display: none;
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
                        <asp:Literal ID="ltUsertop" runat="server">   Marks Entry Personality Traits</asp:Literal>
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



                        <div class="row">
                            <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                <asp:DropDownList ID="ddl_CourseCat" class="form-control" runat="server" Style="width: 100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                            </div>




                            <div class="form-group col-xs-10 col-sm-3 col-md-10 col-lg-10">
                                <div class="row" style="padding: 0px 0px 0px 15px;">
                                    <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                        <label>Section<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_section" class="form-control" runat="server" Style="width: 98%"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                        <label>Term<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_term" class="form-control" runat="server" Style="width: 98%"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                        <label for="validationCustom01" class="form-label">Personality Traits<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_subject" class="form-control" runat="server" Style="width: 98%"></asp:DropDownList>
                                    </div>




                                    <div class="form-group col-xs-10 col-sm-3 col-md-1 col-lg-1">
                                        <asp:Button ID="btn_find" runat="server" class="btn btn-sm btn-success" Text="Find" Style="margin: 28px 0px 0px 0px; padding: 6px 10px 8px;" OnClick="btn_find_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>



                        <hr />
                        <table style="width: 100%;" id="example1" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Student</th>
                                    <th>Roll No.</th>
                                    <th>
                                        <asp:Label ID="lbl_activity_type" runat="server"></asp:Label></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Font-Names="Arial" Text='<%#Bind("studentname") %>'></asp:Label>
                                                (<asp:Label ID="lbl_adm_no" runat="server" Font-Names="Arial" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>)
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_marks" runat="server" class="grd-txtbx-clas"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>

                        <div class="form-group col-xs-10 col-sm-3 col-md-12 col-lg-12">
                            <asp:Button ID="btn_save" runat="server" class="btn btn-sm btn-success" Text="Save" Style="margin: 0px 0px 0px 0px; padding: 6px 10px 8px; float: right;"
                                OnClick="btn_save_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
