<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="VIew_Syllabus_Report.aspx.cs" Inherits="school_web.LMS_VC_Admin.VIew_Syllabus_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Syllabus Report Teacher Wise
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
                                <label>Class</label>
                                <asp:DropDownList ID="ddl_CourseCat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Teacher</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_teacher_list" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Start Date</label>

                                    <asp:TextBox ID="txt_date" runat="server" CssClass="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>End Date</label>

                                    <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn btn-primary" Text="Find" Style="margin-top: 5px;" />
                            </div>

                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl. No.</th>
                                    <th>Teacher Name</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Subject</th>
                                    <th>Sub-Subject</th>
                                    <th>Chapter</th>
                                    <th>Subchapter</th>
                                    <th>Status</th>
                                    <th>Date</th>
                                    <th>Remarks</th> 
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_teachername" runat="server" Font-Names="Arial" Text='<%#Bind("teachername") %>'></asp:Label>
                                            </td>
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
                                                <asp:Label ID="lbl_Sub_Subject_name" runat="server" Text='<%#Bind("SubSubjName") %>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Chapter_Name" runat="server" Font-Names="Arial" Text='<%#Bind("Chapter_Name") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Subchapter_Name" runat="server" Font-Names="Arial" Text='<%#Bind("SubChapterName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Status" runat="server" Font-Names="Arial" Text='<%#Bind("Status") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Date" runat="server" Font-Names="Arial" Text='<%#Bind("Date") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Remarks" runat="server" Font-Names="Arial" Text='<%#Bind("Remarks") %>'></asp:Label>
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
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />

    <script src="../Autocomplete/jquery-ui.js"></script>
    <div class="input-group input-group-icon">
    </div>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
</asp:Content>
