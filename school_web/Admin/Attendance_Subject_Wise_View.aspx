<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Attendance_Subject_Wise_View.aspx.cs" Inherits="school_web.Admin.Attendance_Subject_Wise_View" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Attndance View Subject Wise
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
  
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .home-grph-wpr {
            width: 114%;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            padding: 4px 0px 3px 3px;
            position: relative;
        }

        .clndr-icon {
            font-size: 16px !important;
            color: #ff2956;
            position: absolute;
            top: 179px;
            right: 40px;
        }
    </style>
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />

    <script src="../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-wrapper">
        <div class="page-content">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-white">Success Alerts</h6>
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-dark">Warning Alerts</h6>
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                </div>
            </div>


            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Student Attndance</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student Attndance View Subject Wise</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
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
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">Period 
                                    </td>
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">Subject 
                                    </td>

                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold"></td>

                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:DropDownList ID="ddl_session" Style="width: 130px!important;" runat="server" CssClass="form-select find-dv-txtbx" AutoPostBack="false"></asp:DropDownList>
                                    </td>


                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                        <asp:TextBox ID="txt_date" runat="server"  ></asp:TextBox>
                                    </td>


                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:DropDownList ID="ddl_class" runat="server" Style="width: 100px!important;" CssClass="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </td>

                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                        <asp:DropDownList ID="ddl_section" runat="server" Style="width: 100px!important;" CssClass="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                    </td>

                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                        <asp:DropDownList ID="ddl_period" runat="server" Style="width: 100px!important;" CssClass="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_period_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                        <asp:DropDownList ID="ddl_subject" runat="server" Style="width: 100px!important;" CssClass="form-select find-dv-txtbx"></asp:DropDownList>
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
                                        <td style="padding: 5px;text-align:right">Total Student
                                        </td>

                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbltotal_student" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>
                                        <td style="padding: 5px;text-align:right">Total Prsent Student
                                        </td>

                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_persenstudent" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>

                                        <td style="padding: 5px;text-align:right">Total Absent Student
                                        </td>

                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_totalabsentstudent" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>
                                        <td style="padding: 5px;text-align:right">Total Leave Student
                                        </td>

                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_leave_student" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="GrdView" runat="server" class="table table-hover table-striped table-bordered" AutoGenerateColumns="False" Width="100%">
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
        <!--end row-->
    </div>
</asp:Content>
