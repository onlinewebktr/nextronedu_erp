<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Subject_Wise_Class_Taken_Report.aspx.cs" Inherits="school_web.LMS_VC_Admin.Subject_Wise_Class_Taken_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Subject Wise Class Taken Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .notificationpan {
            display: none;
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 66px!important;
            right: 10px;
            padding: 10px 10px;
            width: 290px;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgb(154 154 154 / 80%);
        }

        .dt-button-collection {
            margin-top: -59.4px!important;
        }

        .input-group > .form-control, .input-group > .form-control-plaintext, .input-group > .custom-select, .input-group > .custom-file {
            position: relative;
            flex: 1 1 auto;
            width: 1%;
            margin-bottom: 0;
            font-weight: bold!important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .calender-icon {
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
            top: -24px;
            left: 131px;
        }
    </style>
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>

    <script>
        $(function () {
            $("#<%=txt_startdate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-display1 icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Subject Wise Class Taken Report</asp:Literal>

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
                        <asp:Panel ID="pnltop" runat="server">
                            <div class="form-row" style="font-size: 16px;">
                                <div class="row" style="padding: 10px 0px 10px 0px; border: 1px solid #ccc; margin: 0px auto; background: #dbdbdb;">


                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label style="float: left;">Select Class</label>
                                            <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                                <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label style="float: left;">Select Section</label>
                                            <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                                <asp:DropDownList ID="dd_section" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dd_section_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label style="float: left;">Select Subject</label>
                                            <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                                <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label style="float: left;">Select Start Date</label>
                                            <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                                <asp:TextBox ID="txt_startdate" runat="server" CssClass="form-control calender-icon" Style="z-index: 99999999999;"></asp:TextBox>
                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label style="float: left;">Select End Date</label>
                                            <div class="input-group input-group-icon" style="display: flex; width: 100%; margin: 0px 0px 0px 0px;">
                                                <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control calender-icon" Style="z-index: 99999999999;"></asp:TextBox>
                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            </div>
                                        </div>
                                    </div>




                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label style="float: left;"></label>
                                            <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_find_Click" Style="margin: 32px 0px 0px 0px;" />

                                        </div>
                                    </div>



                                </div>
                            </div>
                            <hr />


                        </asp:Panel>

                        <asp:Panel ID="pnl_view" runat="server" Visible="false">
                            <h5 class="card-title">
                                <asp:Label ID="lbl_month_year" runat="server" Style="color: #f81b1b;"></asp:Label>&nbsp; | No of class taken :-<asp:Label ID="lbl_totalfranchise" runat="server" Style="color: #f81b1b;"></asp:Label>

                            </h5>


                            <hr />
                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                <thead>

                                    <tr>

                                        <th>Sl No.</th>
                                        <th>Date</th>
                                        <th>Teacher</th>
                                        <th>Class</th>
                                        <th>Section</th>
                                        <th>Subject</th>
                                        <th>Start Time</th>
                                        <th>End Time</th>
                                        <th>Student Strength</th>
                                        <th>Student present</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RpDetailsStudent" runat="server">
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_teacherName" runat="server" Text='<%#Bind("Name") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_CourseName" runat="server" Text='<%#Bind("CourseName") %>'></asp:Label>
                                                </td>




                                                <td>
                                                    <asp:Label ID="lbl_Meeting_start_at" runat="server" Text='<%#Bind("Meeting_start_at","{0:hh:mm:ss tt}") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_end_time" runat="server" Text='<%#Bind("End_Time","{0:hh:mm:ss tt}") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_student_count" runat="server" Text='<%#Bind("no_of_student") %>'></asp:Label>
                                                </td>
                                                <td>


                                                    <asp:Label ID="lbl_System_id" runat="server" Text='<%#Bind("System_id") %>' Visible="false"></asp:Label>

                                                    <a id="A3" href='Subject_Wise_Class_Taken_Student_Report.aspx?System_id=<%#Eval("System_id")%>' style="color: #000" target="_blank" class="dropdown-item"><span>
                                                        <asp:Label ID="lbl_student_present" runat="server" Text='<%#Bind("no_of_persent") %>'></asp:Label></span></a>



                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
