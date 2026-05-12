<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Student_view_Attendance_class_wise.aspx.cs" Inherits="school_web.InstructorProfile.Student_view_Attendance_class_wise" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Class Wise View Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-bordered th {
            border: 1px solid #e9ecef;
            font-size: 13px;
        }

        .table-bordered td {
            border: 1px solid #e9ecef;
            font-size: 16px;
        }

        .notificationpan {
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 70px;
            right: 10px;
            padding: 10px 10px;
            width: 667px !important;
        }
    </style>
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
    <style>
        .waiting {
            padding: 15px 15px 15px 14px;
            font-size: 16px;
            bottom: 0px;
            left: 1px;
            top: 300px;
            background: #fff0;
            color: #1a1313;
            height: 55px !important;
            z-index: 1000;
            font-size: 17px;
            text-align: center;
            width: 99.8%;
            position: fixed;
        }

        .app-wrapper-footer {
            display: none;
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
            top: 32px;
            left: 318px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">
        
        <div class="app-main__inner">
            <div class="app-page-title">
                <div class="page-title-wrapper">
                    <div class="page-title-heading">
                        <div class="page-title-icon">
                            <i class="pe-7s-menu icon-gradient bg-mean-fruit"></i>
                        </div>
                        <div>
                            <asp:Literal ID="ltUsertop" runat="server">Class Wise View Attendance</asp:Literal>
                        </div>
                    </div>




                </div>
            </div>
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 100%; height: auto;">
                        <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hd_regid" runat="server" />
            <div class="row">

                <div class="col-lg-12">
                    <div class="main-card mb-3 card">
                        <div class="card-body">
                            <table class="tab-content table table-bordered">
                                <tr>
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">Session
                                    </td>
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">Date</td>

                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">Class 
                                    </td>
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">Section 
                                    </td>

                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold"></td>

                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:DropDownList ID="ddl_session" Style="width: 100px!important;" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                                    </td>


                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                        <asp:TextBox ID="txt_date" runat="server" CssClass="form-control calender-icon"></asp:TextBox>

                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
                                        <script src="../Autocomplete/jquery-ui.js"></script>
                                        <script>
                                            $(function () {

                                                $("#<%=txt_date.ClientID %>").datepicker({
                                                dateFormat: "dd/mm/yy",
                                                changeMonth: true,
                                                changeYear: true,
                                                yearRange: "1900:2100",

                                            }).attr("readonly", "true");
                                        });
                                        </script>
                                    </td>


                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:DropDownList ID="ddl_class" runat="server" Style="width: 100px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </td>

                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                        <asp:DropDownList ID="ddl_section" runat="server" Style="width: 100px!important;" CssClass="form-control"></asp:DropDownList>
                                    </td>



                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary" OnClick="btn_find_Click" ValidationGroup="a" Style="float: right" />
                                        <asp:ImageButton ID="imgexcel2" runat="server" Visible="false" ImageUrl="~/images/excel_con.png" CssClass="excelbutton22" OnClick="imgexcel2_Click"
                                            Style="height: 31px; width: 32px; margin-top: 1px; margin: 8px 0px 0px 13px;" />
                                    </td>
                                </tr>

                            </table>

                            <div runat="server" visible="false" id="grid111">

                                <table style="margin: 0px; padding: 0px; width: 64%" border="1">
                                    <tr>
                                        <td style="padding: 5px;">Total Students
                                        </td>

                                        <td style="padding: 5px; background-color: #020dea; color: #fff!important;">
                                            <asp:Label ID="lbltotal_student" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>
                                        <td style="padding: 5px;">Total Present Students
                                        </td>

                                        <td style="padding: 5px; background-color: #009f25; color: #fff!important;">
                                            <asp:Label ID="lbl_persenstudent" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>

                                        <td style="padding: 5px;">Total Absent Students
                                        </td>

                                        <td style="padding: 5px; background-color: #f00; color: #fff!important;">
                                            <asp:Label ID="lbl_totalabsentstudent" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>
                                        <td style="padding: 5px;">Total Leave Students
                                        </td>

                                        <td style="padding: 5px; background-color: #ff6a00; color: #fff!important;">
                                            <asp:Label ID="lbl_leave_student" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="GrdView" OnRowDataBound="GrdView_RowDataBound" runat="server" class="table table-hover table-striped table-bordered" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Student Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FullName" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Admission No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_reg_id" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Roll No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Attendance_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_day" runat="server" Text='<%#Bind("Day")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Attendance Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Attendance_Status" runat="server" Text='<%#Bind("Attendance_Status")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>







                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </div>



                </div>

            </div>
        </div>
        <asp:HiddenField ID="hd_id" runat="server" />
       
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
 
</asp:Content>
