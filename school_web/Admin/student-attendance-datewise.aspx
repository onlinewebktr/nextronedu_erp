<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="student-attendance-datewise.aspx.cs" Inherits="school_web.Admin.student_attendance_datewise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Attendance Datewise
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
    <script src="../assets/js/table2excel.js"></script>

    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=hd_session_id.ClientID%>").val();
            $("#<%=txt_adm_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'student-attendance-datewise.aspx/GetRooPathAdmNo',
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

        .txtnoWrap {
            white-space: nowrap;
        }

        .wdth100 {
            width: 100%;
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_session_id" runat="server" />
    <asp:HiddenField ID="hd_user_id" runat="server" />
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Student Attendance</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student Attendance Datewise</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                <div id="notification1">
                    <div id="pan1" class="notificationpan">
                        <div style="float: left; width: 235px; height: auto;">
                            <asp:Label ID="lbl_js_message" runat="server" class="notif-txt"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                                <asp:TextBox ID="txt_from_date" runat="server" class="form-control datepicker"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                                <asp:TextBox ID="txt_to_date" runat="server" class="form-control datepicker"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-5">
                                                        <div class="row">
                                                            <div class="col-sm-5">
                                                                <label for="validationCustom01" class="find-dv-lbl">Find By</label>
                                                                <asp:DropDownList ID="ddl_find_by" runat="server" class="form-select">
                                                                    <asp:ListItem Value="1">Class & Section</asp:ListItem>
                                                                    <asp:ListItem Value="2">Admission No.</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>


                                                            <div class="col-sm-4" id="classDV">
                                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                                <asp:DropDownList ID="ddlclass" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>

                                                            <div class="col-sm-3" id="sectionDV">
                                                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                                <asp:DropDownList ID="ddl_section" runat="server" class="form-select"></asp:DropDownList>
                                                            </div>

                                                            <div class="col-sm-7" id="admissionDV">
                                                                <label for="validationCustom01" class="find-dv-lbl">Admission No.</label>
                                                                <asp:TextBox ID="txt_adm_no" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()">Find</a>
                                                    </div>
                                                    <div class="col-sm-3" style="padding-left: 0px;">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>


                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                                    </div>
                                                </div>
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
                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                        <div id="tblCustomers">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <p id="textwathfile" class="hidden" style="margin: 0px 0px 2px 0px; padding: 3px 10px 3px 10px; width: 100%; float: left; font-size: 15px; font-weight: 600; letter-spacing: 1px; border: 1px solid #ddd;">
                                                                        </p>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table class="table table-striped table-bordered">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th><span class="txtnoWrap">Adm. No.</span></th>
                                                                    <th><span class="txtnoWrap">Class</span></th>
                                                                    <th>Section</th>
                                                                    <th><span class="txtnoWrap">Roll No.</span></th>
                                                                    <th><span class="txtnoWrap">Name</span></th>
                                                                    <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Working Days</span></th>
                                                                    <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Holidays</span></th>
                                                                    <th class="txtcenter headgroup1"><span class="txtnoWrap">Total Days</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Present</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Absent</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Total Leave</span></th>
                                                                    <th class="txtcenter headgroup2"><span class="txtnoWrap">Present(%)</span></th>
                                                                </tr>
                                                                <tr data-ng-repeat="x in reportdaY track by $index">
                                                                    <td>{{$index+1}}</td>
                                                                    <td><span class="txtnoWrap">{{x.Admission_no}}</span> </td>
                                                                    <td><span class="txtnoWrap">{{x.Class_name}}</span></td>
                                                                    <td><span class="txtnoWrap">{{x.Section}}</span></td>
                                                                    <td><span class="txtnoWrap">{{x.Roll_no}}</span></td>
                                                                    <td><span class="txtnoWrap">{{x.Student_name}}</span></td>
                                                                    <td class="txtcenter">{{x.MyAttendanceSDateWiseItem[0].Total_working_days}}</td>
                                                                    <td class="txtcenter">{{x.MyAttendanceSDateWiseItem[0].Total_holiday_days}}</td>
                                                                    <td class="txtcenter tdgroup1">{{x.MyAttendanceSDateWiseItem[0].Total_no_of_days}}</td>
                                                                    <td class="txtcenter">{{x.MyAttendanceSDateWiseItem[0].Total_persent_days}}</td>
                                                                    <td class="txtcenter">{{x.MyAttendanceSDateWiseItem[0].Total_absent_days}}</td>
                                                                    <td class="txtcenter">{{x.MyAttendanceSDateWiseItem[0].Total_leave_days}}</td>
                                                                    <td class="txtcenter tdgroup1">{{x.MyAttendanceSDateWiseItem[0].Attendance_perc}}%</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="notFound hidden" id="NotFounD">
                                                        <p>No record found.</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>

    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {
            //====FIND
            $scope.ButtonClickFind = function () {
                var from_date = $("#<%=txt_from_date.ClientID%>").val();
                var to_date = $("#<%=txt_to_date.ClientID%>").val();
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=ddlclass.ClientID%>").val();
                var section = $("#<%=ddl_section.ClientID%>").val();

                var find_by = $("#<%=ddl_find_by.ClientID%>").val();
                var admission_no = $("#<%=txt_adm_no.ClientID%>").val();

                if (from_date == "") {
                    alert("Please choose from date.");
                    $("#<%=txt_from_date.ClientID%>").focus();
                    return;
                }
                if (to_date == "") {
                    alert("Please choose to date.");
                    $("#<%=txt_to_date.ClientID%>").focus();
                    return;
                }
                if (find_by == "1") {
                    if (class_id == "0") {
                        alert("Please select class.");
                        $("#<%=ddlclass.ClientID%>").focus();
                        return;
                    }
                    if (section == "Select") {
                        alert("Please select section.");
                        $("#<%=ddl_section.ClientID%>").focus();
                        return;
                    }
                }
                if (find_by == "2") {
                    if (admission_no == "") {
                        alert("Please enter admission no.");
                        $("#<%=ddlclass.ClientID%>").focus();
                        return;
                    }

                    if (class_id == "0") {
                        section = "0";
                    }
                }

                $("#textwathfile").addClass("hidden");
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $("#grdsdatA").removeClass("hidden");
                $("#NotFounD").addClass("hidden");
                $http.get("webServices/attendance.asmx/fetch_attendance_of_two_date", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "From_date": from_date, "To_date": to_date, "Find_by": find_by, "Admission_no": admission_no } }).then(function (response) {
                    $scope.reportdaY = response.data;
                    if ($scope.reportdaY == "") {
                        $("#intsLoader").addClass("hidden");
                        $("#grdsdatA").addClass("hidden");
                        $("#NotFounD").removeClass("hidden");
                    }
                    else {
                        $("#intsLoader").addClass("hidden");
                        $("#grdsdatA").removeClass("hidden");
                        $("#NotFounD").addClass("hidden");
                        $("#textwathfile").removeClass("hidden");
                        if (find_by == "1") {
                            var classx = $("#<%=ddlclass.ClientID%> option:selected").text();
                            var ggg = "Date " + from_date + " To " + to_date + ", Class : " + classx + ", Section : " + section;
                            document.getElementById('textwathfile').innerHTML = ggg;
                        }
                        else
                        { 
                            var ggg = "Date " + from_date + " To " + to_date + ", Admission no. : " + admission_no;
                            document.getElementById('textwathfile').innerHTML = ggg;
                        }
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


    <script type="text/javascript">
        $(document).ready(function () {
            on_find_by_selection();
            $("#<%=ddl_find_by.ClientID%>").on('change', function () {
                on_find_by_selection();
            })
        });

        function on_find_by_selection() {
            if ($('#<%= ddl_find_by.ClientID %> option:selected').val() == "1") {
                $("#classDV").show();
                $("#sectionDV").show();
                $("#admissionDV").hide();
            }
            else {
                $("#classDV").hide();
                $("#sectionDV").hide();
                $("#admissionDV").show();
            }
        }
    </script>

    <style>
        .hidden {
            display: none !important;
        }
    </style>
</asp:Content>
