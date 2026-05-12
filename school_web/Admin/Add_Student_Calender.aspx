<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Add_Student_Calender.aspx.cs" Inherits="school_web.Admin.Add_Student_Calender" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Student Calender
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type=checkbox], input[type=radio] {
            border-style: none;
            width: 17px;
            height: 17px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 8px;
            z-index: 9999;
            background-color: green;
        }

            input[type=checkbox], input[type=radio]:checked {
                background-color: #17a00e;
            }

        input[type="checkbox"]#der1_chk_per:checked + span {
            border-color: red;
            background-color: red;
        }

        tbody, td, tfoot, thead, tr {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            padding: 2px 0px 3px 6px;
        }

        .tbe1 {
            background: var(--secondary-bg-color) none repeat scroll 0 0;
            border: 1px solid #e1e1e1;
            padding: 15px 20px;
            margin-bottom: 0;
            width: 100%;
            min-width: 150px;
        }

        .form-label {
            margin-bottom: 4px !important;
            margin-top: 9px !important;
        }

        div.dd_chk_select {
            border-color: #CCCCCC;
            border-style: solid;
            border-width: 1px;
            height: 30px !important;
            padding: 0px 0px 0px 0px;
            text-align: left;
            vertical-align: middle;
            font-size: 14px;
            text-decoration: none;
            overflow: visible;
            color: Black;
            background-color: white;
            background-image: url(WebResource.axd?d=8mNM2oY0_MTbltHc0o1ItZnaj3eMWaRkOCe1ndy_hR9R3AdIBzoSPv_Ywene2l7sUuedivBnkluzSF6dczdfxuEsclUrEQ8FJ8fADG8qa5ySz0dBH1K8nfZq1zjp5ClriQLuWH6nrYavQri7uKTGSCpI2KI1&t=637959154290547877);
            background-position: right center;
            background-repeat: no-repeat;
            width: 99%;
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
    </script>
    <script>
        $(function () {
            $("#<%=txt_startdate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_end_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add School Calendar For Students</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-6">


                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx">
                                         
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Type<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_type" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Class</asp:ListItem>
                                            <asp:ListItem>Events</asp:ListItem>
                                            <asp:ListItem>Holiday</asp:ListItem>
                                            <asp:ListItem>Examination</asp:ListItem>
                                            <asp:ListItem>Non-Academic</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                        <span class="chkbx-all">
                                            <asp:CheckBox ID="chk_all" runat="server" Text="Select All" OnCheckedChanged="chk_all_CheckedChanged" AutoPostBack="true" /></span>
                                        <br />
                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_class" class="chkstle" runat="server" Text='<%#Eval("Course_Name") %>' />
                                                <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("course_id")%>'></asp:Label>
                                                <asp:Label ID="lbl_course_name" runat="server" Visible="false" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <div class="col-md-4">
                                        <label for="validationCustom01" class="form-label">Is Sunday Holiday<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_is_sunday" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-8">
                                        <label for="validationCustom01" class="form-label">Is Holiday or Non Academic<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_is_sat_holiday_or_no_acd" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6" id="saturdaySetting">
                                        <label for="validationCustom01" class="form-label">Is Saturday<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_is_saturday" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Holiday</asp:ListItem>
                                            <asp:ListItem>Non-Academic</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6" id="saturdaySetting1">
                                        <label for="validationCustom01" class="form-label">Day<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_holiday_or_non_acd_day" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Saturday</asp:ListItem>
                                            <asp:ListItem>Monday</asp:ListItem>
                                            <asp:ListItem>Tuesday</asp:ListItem>
                                            <asp:ListItem>Wednesday</asp:ListItem>
                                            <asp:ListItem>Thursday</asp:ListItem>
                                            <asp:ListItem>Friday</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Recurring Date<sup>*<asp:CheckBox ID="chk_Recurring" runat="server" Text="Recurring Date" OnCheckedChanged="chk_Recurring_CheckedChanged" AutoPostBack="true" /></sup></label>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" id="lblstartdate" runat="server" class="form-label">Date<sup>*</sup></label>

                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_startdate" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12" id="enddatpnl" runat="server" visible="false">
                                        <label for="validationCustom01" class="form-label">End Date<sup>*</sup></label>

                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_end_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Details</label>
                                        <asp:TextBox ID="txt_details" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>


                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3"></div>


            </div>
        </div>
    </div>


    <script type="text/javascript">
        //============================
        $(document).ready(function () {
            on_is_sat_holiday_or_no_acd_selection();
            $("#<%=ddl_is_sat_holiday_or_no_acd.ClientID%>").on('change', function () {
                on_is_sat_holiday_or_no_acd_selection();
            })
        });

        function on_is_sat_holiday_or_no_acd_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_is_sat_holiday_or_no_acd.ClientID %> option:selected').val() == "Yes") {
                $("#saturdaySetting").show();
                $("#saturdaySetting1").show();
            }
            else {
                $("#saturdaySetting").hide();
                $("#saturdaySetting1").hide();
            }
        }
    </script>
</asp:Content>
