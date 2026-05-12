<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="View_Syllabus_Report_Subject_Class_wise.aspx.cs" Inherits="school_web.LMS_VC_Admin.VIew_Syllabus_Report_Subject_Class_wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    VIew Syllabus Report Subject Class wise
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .gridcss th {
            font-size: 14px !important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 5px;
            left: 71px;
        }

        .texbox-border {
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 1px solid #00000038;
        }
    </style>
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
                        <asp:Literal ID="ltUsertop" runat="server"> Syllabus Report Teacher Wise</asp:Literal>

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
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Session</label>
                                    <asp:DropDownList ID="ddl_sseion" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Term</label>
                                    <asp:DropDownList ID="ddl_term" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_term_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Class</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Section</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Subject</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Chapter</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_chapter" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn btn-primary" Text="Find" Style="margin-top: 5px;" />
                            </div>

                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl. No.</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Subject</th>
                                    <th>Chapter</th>
                                    <th>Subchapter</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbl_CategoryName" runat="server" Font-Names="Arial" Text='<%#Bind("Course_Name") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section_Name" runat="server" Font-Names="Arial" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Subject_name" runat="server" Font-Names="Arial" Text='<%#Bind("Subject_name") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Chapter_Name" runat="server" Font-Names="Arial" Text='<%#Bind("Chapter_Name") %>'></asp:Label>

                                                <asp:Label ID="lbl_Session_id" runat="server" Font-Names="Arial" Text='<%#Bind("Session_id") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Term_id" runat="server" Font-Names="Arial" Text='<%#Bind("Term_id") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Subject_id" runat="server" Font-Names="Arial" Text='<%#Bind("Subject_id") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Class_id" runat="server" Font-Names="Arial" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Chapter_and_Subchapter_id" runat="server" Font-Names="Arial" Text='<%#Bind("Chapter_and_Subchapter_id") %>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Subchapter_Name" runat="server" Font-Names="Arial" Text='<%#Bind("SubChapterName") %>'></asp:Label>
                                                <i class="fa fa-check" aria-hidden="true" id="a1" runat="server" visible="false"></i>
                                                <i class="fa fa-times" aria-hidden="true" id="a2" runat="server" visible="false"></i>
                                                <i class="fa fa-clock-o" aria-hidden="true" id="a3" runat="server" visible="false"></i>

                                            </td>



                                        </tr>
                                        <tr style="border-bottom: 2px solid #000" id="subtable" runat="server" visible="false">

                                            <td></td>
                                            <td colspan="5" style="border-top: 1px solid #000; border-bottom: 1px solid #000;">
                                                <table style="width: 100%; margin-bottom: 0rem; background-color: white;" id="example" class="table-bordered">
                                                    <thead>
                                                        <tr style="background-color: rgb(255 255 255 / 3%);">
                                                            <th>Sl. No.</th>
                                                            <th>Status</th>
                                                            <th>Date</th>
                                                            <th>Remarks</th>


                                                        </tr>
                                                    </thead>
                                                    <tbody>

                                                        <asp:Repeater ID="rd_subject_view" runat="server">
                                                            <ItemTemplate>
                                                                <tr style="background-color: rgb(255 255 255 / 3%);">
                                                                    <td>

                                                                        <asp:Label ID="Label1" Style="font-weight: 600" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>. 
                                                          

                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_Status" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                                    </td>
                                                                    <td>

                                                                        <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                    </td>

                                                                    <td>

                                                                        <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
                                                                    </td>
                                                                </tr>


                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                    </tbody>

                                                </table>
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
