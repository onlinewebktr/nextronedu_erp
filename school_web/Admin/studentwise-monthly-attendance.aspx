<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="studentwise-monthly-attendance.aspx.cs" Inherits="school_web.Admin.studentwise_monthly_attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
   Student Monthly Wise Summary Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        th {
            font-weight: 500;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 25px;
            height: 25px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session_student.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'studentwise-monthly-attendance.aspx/GetRooPath',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });

        $(function () {
            var sessionid = $("#<%=ddlsessionad.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'studentwise-monthly-attendance.aspx/GetRooPathAdmNo',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });
    </script>



    <script src="../../assets/Angular/angular.min.js"></script>
    <script src="../assets/js/table2excel.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>

    <style type="text/css">
        .daySunday {
            background: #ff7373 !important;
        }

        .tdwdth {
            width: 65px !important;
            display: inline-block;
            font-weight: 600;
            font-style: inherit;
        }

        .daypresenT {
            background: #5afb3d !important;
        }

        .dayabsenT {
            background: #fff84b !important;
        }

        .dayleavE {
            background: #ffb100 !important;
        }

        .txtcenter {
            text-align: center;
        }

        .notattendances {
            background: #ef91ff !important;
        }

        tfoot, th, thead {
            color: #fff;
        }

        .notesp {
            margin: 0px 0px 5px 0px;
            padding: 2px 5px 2px 5px;
        }

            .notesp span {
                font-weight: 600;
            }

        .headgroup1 {
            background: #c541c7 !important;
        }

        .headgroup2 {
            background: #58aac9 !important;
        }

        .tdgroup1 {
            background: #c5ef96 !important;
        }

        .monthsdVS {
            border: 1px solid #cefdb9;
            margin: 7px 0px 7px 0px;
            padding: 5px 10px;
            width: 100%;
            float: left;
            background: #eaffe1;
            border-radius: 2px;
        }

        .monthsdVS-p {
            margin: 0px 0px 5px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 16px;
            font-weight: 500;
            text-decoration: underline;
        }

            .monthsdVS-p span {
                margin: 0px;
                padding: 0px;
            }

        .mrgn-brm {
            margin: 0px;
        }

        .txtnoWrap {
            white-space: nowrap;
        }

        .std-infos {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .std-infos table {
                width: 100%;
            }

                .std-infos table tr th {
                    padding: 5px 10px !important;
                    background: #f5fff6 !important;
                    color: #000;
                    font-size: 16px;
                    font-weight: 600;
                    border: 1px solid #ddd;
                }

                .std-infos table tr td {
                    padding: 5px 10px;
                    border: 1px solid #ddd;
                }

        .wdth100 {
            width: 100%;
            float: left;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
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
                            <li class="breadcrumb-item active" aria-current="page">Student Monthly Wise Summary Attendance</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-xl-3">
                                    <div class="fnd-box-wpr">
                                        <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                        <div class="fnd-box-wpr-inr">
                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-xl-6 padd-rght-5">
                                                        <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                    </div>
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <asp:DropDownList ID="ddlsessionad" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-xl-6 padd-rght-5">
                                                        <label for="validationCustom01" class="form-label-fnds">Admission No.</label>
                                                    </div>
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-xl-6 padd-rght-5">
                                                    </div>
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xl-5">
                                    <div class="fnd-box-wpr">
                                        <h2 class="fnd-box-row-wpr-h">Find by Roll No.</h2>
                                        <div class="fnd-box-wpr-inr">
                                            <div class="row">
                                                <div class="col-xl-6 padd-rght-5">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <label for="validationCustom01" class="form-label-fnds">Class</label>
                                                            </div>
                                                            <div class="col-xl-7 padd-lft-5">
                                                                <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xl-6 padd-lft-5">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <label for="validationCustom01" class="form-label-fnds">Section</label>
                                                            </div>
                                                            <div class="col-xl-7 padd-lft-5">
                                                                <asp:TextBox ID="txt_section" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl-6 padd-rght-5">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                            </div>
                                                            <div class="col-xl-7 padd-lft-5">
                                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xl-6 padd-lft-5">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <label for="validationCustom01" class="form-label-fnds">Roll No.</label>
                                                            </div>
                                                            <div class="col-xl-7 padd-lft-5">
                                                                <asp:TextBox ID="txtrollnumber" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xl-6 padd-lft-5">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-5">
                                                            </div>
                                                            <div class="col-xl-7 padd-lft-5">
                                                                <asp:Button ID="btnfind" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btnfind_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-4">
                                    <div class="fnd-box-wpr">
                                        <h2 class="fnd-box-row-wpr-h">Find by Student Name</h2>
                                        <div class="fnd-box-wpr-inr">
                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-xl-5 padd-rght-5">
                                                        <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                    </div>
                                                    <div class="col-xl-7 padd-lft-5">
                                                        <asp:DropDownList ID="ddl_session_student" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-xl-5 padd-rght-5">
                                                        <label for="validationCustom01" class="form-label-fnds">Student Name</label>
                                                    </div>
                                                    <div class="col-xl-7 padd-lft-5">
                                                        <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="fnd-box-row-wpr">
                                                <div class="row">
                                                    <div class="col-xl-5 padd-rght-5">
                                                    </div>
                                                    <div class="col-xl-7 padd-lft-5">
                                                        <asp:Button ID="btn_find_name" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_name_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="std_basic_infoS" runat="server" Visible="false">
                                <asp:HiddenField ID="hd_admission_no" runat="server" />
                                <asp:HiddenField ID="hd_class_id" runat="server" />
                                <asp:HiddenField ID="hd_session_id" runat="server" />
                                <asp:HiddenField ID="hd_session_name" runat="server" />
                                <div class="row" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                                    <div id="notification1">
                                        <div id="pan1" class="notificationpan">
                                            <div style="float: left; width: 235px; height: auto;">
                                                <asp:Label ID="lbl_js_message" runat="server" class="notif-txt"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-12">
                                        <div class="find-dv">
                                            <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()" style="padding: 7px 10px;"><i class='bx bx-download'></i>Excel</a>


                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px; padding: 7px 10px;" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                        </div>
                                        <div class="ints-loader-wpr" id="intsLoader">
                                            <div class="ints-loader-wpr-inr">
                                                <div class="ints-loader">
                                                    <p class="ints-loader-txt">
                                                        <img src="../assets/images/icons/loader-ico.gif" class="ints-loader-img" />
                                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr" style="overflow: auto">
                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">

                                                    <div id="tblCustomers">
                                                        <div class="std-infos">
                                                            <table class=" mrgn-brm table-bordered">
                                                                <tr>
                                                                    <th colspan="3">Student Details 
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>Student Name :
                                                                    <asp:Label ID="lbl_name" runat="server" Font-Bold="true"></asp:Label></td>

                                                                    <td>Class :
                                                                    <asp:Label ID="lblclass" runat="server" Font-Bold="true"></asp:Label></td>

                                                                    <td>Section :
                                                                    <asp:Label ID="txtsection" runat="server" Font-Bold="true"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>Admission No. :
                                                                    <asp:Label ID="lbl_admission_no" runat="server" Font-Bold="true"></asp:Label></td>

                                                                    <td colspan="2">Roll No. :
                                                                    <asp:Label ID="lbl_roll_no" runat="server" Font-Bold="true"></asp:Label></td>

                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <div class="monthsdVS" data-ng-repeat="x in reportdaY track by $index">
                                                            <table class=" mrgn-brm">
                                                                <tr>
                                                                    <td>
                                                                        <p class="monthsdVS-p"><span>Month for </span>{{x.Month_name}}</p>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table class="table table-striped table-bordered mrgn-brm">
                                                                <tr>
                                                                    <th data-ng-repeat="item in x.MyStudentWiseAttendanceSItem track by $index" class="txtcenter {{item.dayNameClassHead}}"><i class="tdwdth">{{item.daYHead}} <span style="font-size: 12px; text-transform: uppercase;">({{item.dayNameHead}})</span></i></th>
                                                                    <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Working Days</span></th>
                                                                    <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Holidays</span></th>
                                                                    <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Days</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Present</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Absent</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Leave</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Present(%)</span></th>
                                                                </tr>
                                                                <tr>
                                                                    <td style="position: relative" data-ng-repeat="item in x.MyStudentWiseAttendanceSItem track by $index" class="txtcenter {{item.dayNameClass}}"><span class="txtnoWrap">{{item.AttendanceS}}</span></td>
                                                                    <td class="txtcenter">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_working_days}}</td>
                                                                    <td class="txtcenter">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_holiday_days}}</td>
                                                                    <td class="txtcenter tdgroup1">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_no_of_days}}</td>
                                                                    <td class="txtcenter">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_persent_days}}</td>
                                                                    <td class="txtcenter">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_absent_days}}</td>
                                                                    <td class="txtcenter">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Total_leave_days}}</td>
                                                                    <td class="txtcenter tdgroup1">{{x.MyStudentWiseAttendanceSItem[x.MyStudentWiseAttendanceSItem[0].Total_no_of_days_less_one].Attendance_perc}}%</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>


                                                    <div class="wdth100">
                                                        <p class="notesp" style="background: #ef91ff !important; width: auto; float: left;">
                                                            <span>*NA :</span>  Not Attendance
                                                        </p>
                                                    </div>
                                                    <div class="wdth100">
                                                        <p class="notesp" style="background: #5afb3d  !important; width: auto; float: left;"><span>*P :</span> Present</p>
                                                    </div>
                                                    <div class="wdth100">
                                                        <p class="notesp" style="background: #fff84b !important; width: auto; float: left;"><span>*A : </span>Absent</p>
                                                    </div>
                                                    <div class="wdth100">
                                                        <p class="notesp" style="background: #ffb100 !important; width: auto; float: left;"><span>*L : </span>Leave</p>
                                                    </div>

                                                </div>


                                                <div class="notFound hidden" id="NotFounD">
                                                    <p>No record found.</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--end row-->

    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 750px;">
            <div class="popupTablWpR">
                <div class="row">
                    <div class="col-md-6">
                        <h2 class="popup-dt-h">Student Details</h2>
                    </div>
                    <div class="col-md-6">
                        <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Close</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>

                <table style="width: 100%;" id="Table1" class="table table-hover table-bordered ">

                    <tr>
                        <th>Student Name</th>
                        <th>Admission No</th>
                        <th>Class</th>
                        <th>Section</th>
                        <th>Roll</th>
                        <th>Father's Name</th>
                        <th>Action</th>
                    </tr>


                    <asp:Repeater ID="rp_std" runat="server">
                        <ItemTemplate>
                            <tr id="row" runat="server">
                                <td>
                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbladmissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername") %>'></asp:Label>
                                    <asp:Label ID="lbl_session" Visible="false" runat="server" Text='<%#Bind("session") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnk_select" runat="server" OnClick="lnk_select_Click">Select</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>


            </div>

        </div>
    </div>
    <style>
        .conf-btn-ul li a {
            margin: 0px 5px;
            padding: 0px 0px 1px;
            text-decoration: none;
            background: #ff0000;
            color: #fff;
            width: 50px;
            float: right;
            text-align: center;
            border-radius: 3px;
            font-size: 13px;
            line-height: 25px;
            font-weight: 500;
        }

        table tr th {
            padding: 10px 5px !important;
        }
    </style>




    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {
            //====FIND

            var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
            var class_id = $("#<%=hd_class_id.ClientID%>").val();
            var session_id = $("#<%=hd_session_id.ClientID%>").val();
            var session_name = $("#<%=hd_session_name.ClientID%>").val();

            if (admission_no != "XZXZX") {
                $("#textwathfile").addClass("hidden");
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("webServices/attendance.asmx/fetch_attendance_of_monthwise_studentwise", { params: { "Session_id": session_id, "Session_name": session_name, "Class_id": class_id, "Admission_no": admission_no } }).then(function (response) {
                    $scope.reportdaY = response.data;
                    if ($scope.reportdaY == "") {
                        $("#intsLoader").addClass("hidden");
                        $("#grdsdatA").addClass("hidden");
                        $("#NotFounD").removeClass("hidden");
                    }
                    else {
                        $("#intsLoader").addClass("hidden");
                        $("#textwathfile").removeClass("hidden");
                        $("#grdsdatA").removeClass("hidden");
                        $("#NotFounD").addClass("hidden");
                        //var ggg = "Year : " + years + ", Month : " + monthsx + ", Class : " + classx + ", Section : " + section;
                        //document.getElementById('textwathfile').innerHTML = ggg;
                    }
                })
            }

            $scope.Export = function () {
                $("#tblCustomers").table2excel({
                    filename: "Attendance.xls"
                });
            }
        });

        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }
    </script>

    <style>
        .hidden {
            display: none !important;
        }
    </style>
</asp:Content>
