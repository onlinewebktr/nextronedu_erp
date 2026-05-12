<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="suject-allocation-subjectwise.aspx.cs" Inherits="school_web.Admin.suject_allocation_subjectwise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Allocated Subject Summary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
    <link href="../assets/css/Print.css" rel="stylesheet" />
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



        function PrintPanel1() {
            var panel = document.getElementById("<%=tblPrintIQ1.ClientID %>");
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
    <style>
        .head {
            display: none;
        }

        .pgslry-head-div {
            margin-top: 10px;
        }

        .notFound {
            margin: 0px;
            padding: 20px 0px;
            width: 100%;
            float: left;
            background: #effbe8;
            text-align: center;
            border: 1px solid #d6edc2;
            font-size: 18px;
        }

            .notFound p {
                margin: 0px;
                padding: 0px;
                width: 100%;
                float: left;
            }

        .notificationpancustom {
            display: none;
            width: 100%;
            background-color: #ff2e3d;
            position: fixed;
            top: 55px;
            right: 30px;
            padding: 6px 7px;
            width: 270px;
            height: auto;
            -webkit-border-radius: 9px 9px 9px 9px;
            border-radius: 2px;
            border: 1px solid #c7ff0b;
            box-shadow: 0px 3px 14px 1px rgba(87, 87, 87, 0.85);
        }


        #notification1 {
            margin: 0px;
            padding: 0px;
            position: relative;
            z-index: 999999;
        }

        .notif-txt {
            color: #fff;
            font-weight: 600;
            font-size: 14px;
            z-index: 999999;
            position: relative;
        }

        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }

        .modal {
            background: rgb(0 0 0 / 59%);
        }

        .modal-dialog {
            max-width: 700px;
        }

        .modal-header {
            padding: 7px 15px;
        }

        tfoot, th, thead {
            color: #fff;
        }

        .table td, .table th {
            background-color: #fff;
            color: #000;
        }

        .table td, .table th {
            background-color: #fff;
            color: #000;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_branch_id" runat="server" />
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
                <div class="breadcrumb-title pe-3">Subject</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Allocated Subject Summary</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <div class="row" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                <div id="notification1">
                    <div id="pan1" class="notificationpancustom">
                        <div style="float: left; width: 235px; height: auto;">
                            <asp:Label ID="lbl_js_message" runat="server" class="notif-txt"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
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
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Subject</label>
                                                        <asp:DropDownList ID="ddl_subject" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()">Find</a>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" Style="background-color: #009f25; border: 5px solid #009f25; cursor: pointer; float: none; display: inline-block; width: 33px; height: 32px; margin: 21px 0px 0px 11px;"
                                                            ToolTip="Print">
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">

                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">
                                                        <div class="pgslry-head-div head">

                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                            </div>
                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                                <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                                                </h1>

                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>




                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                </div>
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <span style="font-size: 14px; font-weight: bold;">Allocated Subject List-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                            <table class="table table-striped table-bordered" id="tblCustomers">
                                                                <tr>
                                                                    <th>Class</th>
                                                                    <th>Subject</th>
                                                                    <th>No. of Student</th>
                                                                    <th>No. of Student Assigned</th>
                                                                </tr>
                                                                <tr data-ng-repeat="x in reportSubjects">
                                                                    <td>{{x.Class_Name}}</td>
                                                                    <td><span class="r-day-date">{{x.Subject_Name}}</span>
                                                                        <span class="r-day">({{x.Subject_type}})</span></td>
                                                                    <td>{{x.Total_student}}</td>
                                                                    <td>
                                                                        <a href="#!" data-ng-click="ButtonClickStudent(x.Session_id,x.Branch_id,x.Class_id,x.Section,x.SubjId)" class="editbttns" data-toggle="modal" data-target="#exampleModal" style="text-decoration: underline; color: #000;">{{x.StdCount}}</a>
                                                                    </td>
                                                                </tr>
                                                            </table>
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



                <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Mapped Student</h5>

                                <asp:LinkButton ID="LinkButton1" OnClientClick="return PrintPanel1()" CssClass="print-btn" runat="server" Style="background-color: #009f25; border: 5px solid #009f25; cursor: pointer; float: none; display: inline-block; width: 33px; height: 32px; margin: 21px 0px 0px 11px; position: absolute; right: 60px; top: -14px;"
                                    ToolTip="Print">
                                </asp:LinkButton>

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div id="tblPrintIQ1" runat="server">
                                    <div class="prnt-dv-wpr">
                                        <div class="pgslry-head-div head">

                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                <asp:Image ID="imglogo1" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                            </div>
                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                    <asp:Label ID="lbl_heading1" runat="server"></asp:Label>
                                                </h1>

                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                    <asp:Label ID="lbl_address1" runat="server"></asp:Label>
                                                </div>
                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                    Email Id. :<asp:Label ID="lbl_emaiid1" runat="server"></asp:Label>
                                                </div>
                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                    Contact Details :<asp:Label ID="lbl_contact_details1" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="assignedtblwpr" id="assignedtbls">
                                            <table class="table table-bordered" style="margin: 0px">
                                                <tr>
                                                    <td style="font-weight: 600;"><span>Class Name : </span>{{studentstbls[0].className}}</td>
                                                    <td style="font-weight: 600;"><span>Subject Name : </span>{{studentstbls[0].Subject_name}}</td>
                                                </tr>
                                            </table>

                                            <table class="table table-bordered">
                                                <tr>
                                                    <th>#</th>
                                                    <th>Student Name</th>
                                                    <th>Admission No.</th>
                                                    <th>Section</th>
                                                    <th>Roll No.</th>
                                                </tr>
                                                <tr data-ng-repeat="x in studentstbls">
                                                    <td>{{$index+1}}</td>
                                                    <td>{{x.studentname}}</td>
                                                    <td>{{x.admissionserialnumber}}</td>
                                                    <td>{{x.Section}}</td>
                                                    <td>{{x.rollnumber}}</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>





    <!--end row-->


    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {
            //====FIND
            $scope.ButtonClickFind = function () {
                var session_id = $("#<%=ddl_session.ClientID%>").val();
                var class_id = $("#<%=ddlclass.ClientID%>").val();
                var section = $("#<%=ddl_section.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var subject_id = $("#<%=ddl_subject.ClientID%>").val();
                if (session_id == "0") {
                    $("#<%=ddl_session.ClientID%>").focus();
                    messgePopup("Please select session.");
                }
                else if (class_id == "0") {
                    $("#<%=ddlclass.ClientID%>").focus();
                    messgePopup("Please select class.");
                }
                else if (section == "Select") {
                    $("#<%=ddl_section.ClientID%>").focus();
                    messgePopup("Please select section");
                }
                else {
                    if (subject_id == 0) {
                        messge("Please Wait...");
                        $("#intsLoader").removeClass("hidden");
                        $http.get("webServices/subjectReport.asmx/fetch_classwise_allocated_subject", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Subject_id": subject_id } }).then(function (response) {
                            $scope.reportSubjects = response.data;
                            $("#intsLoader").addClass("hidden");
                            if ($scope.reportSubjects == "") {
                                $("#grdsdatA").addClass("hidden");
                                $("#NotFounD").removeClass("hidden");
                            }
                            else {
                                $("#grdsdatA").removeClass("hidden");
                                $("#NotFounD").addClass("hidden");
                            }
                        })
                    }
                    else {
                        messge("Please Wait...");
                        $("#intsLoader").removeClass("hidden");
                        $http.get("webServices/subjectReport.asmx/fetch_classwise_allocated_subject", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Subject_id": subject_id } }).then(function (response) {
                            $scope.reportSubjects = response.data;
                            $("#intsLoader").addClass("hidden");
                            if ($scope.reportSubjects == "") {
                                $("#grdsdatA").addClass("hidden");
                                $("#NotFounD").removeClass("hidden");
                            }
                            else {
                                $("#grdsdatA").removeClass("hidden");
                                $("#NotFounD").addClass("hidden");
                            }
                        })
                    }
                }
            }

            ///ViewStudent
            $scope.ButtonClickStudent = function (Session_id, Branch_id, Class_id, Section, SubjId) {
                $http.get("webServices/subjectReport.asmx/fetch_mapped_students", { params: { "Session_ids": Session_id, "Class_ids": Class_id, "Sections": Section, "Branch_ids": Branch_id, "Subject_Id": SubjId } }).then(function (response) {
                    $scope.studentstbls = response.data;
                    if ($scope.subjectstbls == "") {
                        $("#assignedtbls").addClass("hidden");
                    }
                    else {
                        $("#assignedtbls").removeClass("hidden");
                    }
                })
            }


        });

        function messgePopup(msg) {
            $("#<%=lbl_js_message.ClientID%>").text(msg);
            $('.notificationpancustom').hide().slideDown(1000);
            $('.notificationpancustom').delay(4000).show().slideUp(1000);
        }

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
