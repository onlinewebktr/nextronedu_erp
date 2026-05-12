<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="class-routine-chart-with-teacher.aspx.cs" Inherits="school_web.Admin.class_routine_chart_with_teacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Class Routine Chart With Teacher
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
    <style>
        tbody, td, tfoot, th, thead, tr {
            text-align: center;
            font-size: 13px;
        }

        tfoot, th, thead {
            color: #fff;
            background: #13b1f3 !important;
        }

        .periodTimes {
            font-size: 11px;
        }

        .dys-monday {
            background: #9DCD33 !important;
            color: #fff;
        }

        .dys-tuesday {
            background: #F5A12F !important;
            color: #fff;
        }

        .dys-wednesday {
            background: #0698DB !important;
            color: #fff;
        }

        .dys-thursday {
            background: #CBDC46 !important;
            color: #fff;
        }

        .dys-friday {
            background: #FA69FA !important;
            color: #fff;
        }

        .dys-saturday {
            background: #33cd78 !important;
            color: #fff;
        }

        .BreakStyle {
            width: 10px;
            display: inline-block;
            text-transform: uppercase;
            font-weight: 500;
            font-size: 20px;
        }

        .BreakStyleTd {
            background: #FFFFC9 !important;
        }

        .r-day-date {
            margin: 0px;
            text-transform: uppercase;
        }

        .r-day {
            margin: 7px 0px 7px 0px;
            padding: 0px 7px 7px 7px;
            float: left;
            width: 100%;
            text-transform: uppercase;
            border-bottom: 1px solid #ddd;
        }

        .r-day-teacher {
            margin: 0px;
            text-transform: uppercase;
        }

        .teachers-names {
            margin: 7px 0px 7px 0px;
            padding: 7px 0px 0px 0px;
            float: left;
            width: 100%;
            text-transform: uppercase;
            border-top: 1px solid #ddd;
        }

        .editbttns {
            margin: 0px;
            padding: 0px;
            position: absolute;
            top: -1px;
            right: 2px;
            font-size: 17px;
            color: #ff5200;
        }

        .modal {
            background: rgb(0 0 0 / 53%);
        }

        .modal-dialog {
            top: 10%;
        }

        .modal-dialog {
            max-width: 800px;
        }

        .modal-header {
            padding: 8px 15px !important;
        }

        .mdl-txtbx-tyle {
            margin: 3px 0px 15px 0px;
            float: left;
            width: 100%;
            padding: 5px 5px;
        }

        .NotEditable {
            color: #d7d6d5;
            cursor: no-drop;
            pointer-events: none;
        }

        .assignedtblwpr {
            margin: 0px;
            padding: 10px 15px;
            width: 100%;
            float: left;
        }

        .hidden {
            display: none !important;
        }

        .fntbold {
            font-weight: 600;
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

        .conf-alrt-sec {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 999999;
            background: rgba(0, 0, 0, 0.26);
        }

        .conf-alrt-inr {
            position: relative;
            top: 30%;
            margin: 0px auto;
            border-radius: 2px;
            padding: 20px;
            width: 300px;
            height: auto;
            background: #fff;
            -webkit-transition: -webkit-transform .3s ease-out;
            -o-transition: -o-transform .3s ease-out;
            transition: transform .3s ease-out;
            -webkit-transform: translate(0,-25%);
            -ms-transform: translate(0,-25%);
            -o-transform: translate(0,-25%);
            transform: translate(0,-25%);
            -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
            box-shadow: 0 5px 15px rgba(0,0,0,.5);
        }

        .conf-alrt-msg-p {
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            font-size: 15px;
            color: #333;
            letter-spacing: .5px;
        }

        .conf-btn-ul {
            margin: 15px 0px 37px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            text-align: right;
        }

            .conf-btn-ul li {
                margin: 0px;
                padding: 0px;
                list-style-type: none;
                display: inline;
            }

                .conf-btn-ul li a {
                    margin: 0px 5px;
                    padding: 0px 10px 1px;
                    text-decoration: none;
                    background: #0072ff;
                    color: #fff;
                    width: 65px;
                    float: right;
                    text-align: center;
                    border-radius: 3px;
                    font-size: 13px;
                    line-height: 29px;
                    font-weight: 600;
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
                <div class="breadcrumb-title pe-3">Routine</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Class Routine Chart</li>
                        </ol>
                    </nav>
                </div>


                <asp:LinkButton ID="lnk_sync_teacher_routing" OnClick="lnk_sync_teacher_routing_Click" Style="float: right; position: absolute; right: 0px; font-size: 16px; top: 2px; padding: 0px 5px 2px 25px; border: 1px solid #0d6efd; border-radius: 2px;"
                    runat="server">
                    <i class="bx bx-refresh" style="font-size: 23px; line-height: 20px; margin: 0px 0px 0px 0px; position: absolute; left: 0px; top: 3px;"></i>Sync Teacher
                </asp:LinkButton>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-select"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()" style="padding: 7px 10px;">Find</a>
                                                    </div>
                                                    <div class="col-sm-1" style="padding-left: 0px;">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()" style="padding: 7px 10px;">Refresh</a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                        <table class="table table-striped table-bordered" id="tblCustomers">
                                                            <tr>
                                                                <th>Day</th>
                                                                <th data-ng-repeat="x in reportHeadinG">{{x.Period_Name}}
                                                                    <br />
                                                                    <span class="periodTimes">({{x.period_times}})</span> </th>
                                                            </tr>
                                                            <tr data-ng-repeat="x in reportdaY track by $index">
                                                                <td class="{{x.DaYcolors}}"><span class="r-day-date">{{x.Day_date}}</span><br />
                                                                    <span class="r-day">{{x.Day_name}}</span><br />
                                                                    <span class="r-day-teacher">{{x.Teachers}}</span></td>
                                                                <td style="position: relative" data-ng-repeat="item in x.MyRoutineSubjectTeacherItem track by $index" rowspan="{{item.Tblrowcount}}" class="{{item.Isclass_or_break}} {{item.Period_type_td}}"><span class="{{item.Period_type}}">{{item.Subjects_name}} </span>
                                                                    <br />
                                                                    <span class="teachers-names {{item.Teachers_name}}"><span>{{item.Teachers_name}}</span></span>
                                                                    <a href="#!" data-ng-click="ButtonClickUpdate(x.Day_date_actual_format,x.Day_name,item.Period_Name,item.Period_id)" class="editbttns {{item.Hide_if_break}} {{x.Is_editable}}" data-toggle="modal" data-target="#exampleModal"><i class="bx bx-edit"></i></a>
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



                <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Update Subject/Teacher</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                        <asp:DropDownList ID="ddl_class_mdl" runat="server" class="form-select  mdl-txtbx-tyle" Style="background: #f5f5f5; cursor: no-drop; pointer-events: none"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                        <asp:TextBox ID="txt_section_mdl" runat="server" class="form-control mdl-txtbx-tyle" Style="background: #f5f5f5; cursor: no-drop; pointer-events: none"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="validationCustom01" class="find-dv-lbl">Period</label>
                                        <asp:TextBox ID="txt_period_name_mdl" runat="server" class="form-control mdl-txtbx-tyle" Style="background: #f5f5f5; cursor: no-drop; pointer-events: none"></asp:TextBox>
                                        <asp:TextBox ID="txt_period_id" Style="display: none" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <label for="validationCustom01" class="find-dv-lbl">Subject</label>
                                        <select id="ddl_subject_mdl" ng-click="test()" runat="server" class="form-control mdl-txtbx-tyle">
                                            <option value="0">Select</option>
                                            <option data-ng-repeat="x in subjects" value="{{x.Subject_id}}" class="form-select mdl-txtbx-tyle">{{x.Subject_name}}</option>
                                        </select>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="validationCustom01" class="find-dv-lbl">Teacher</label>
                                        <select id="ddl_teacher_list" runat="server" class="form-control mdl-txtbx-tyle">
                                            <option value="0">Select</option>
                                            <option data-ng-repeat="x in teacher_list" value="{{x.UserID}}">{{x.Teacher_name}}</option>
                                        </select>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                <asp:TextBox ID="txt_date_from" runat="server" class="form-control mdl-txtbx-tyle"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6">
                                                <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                <asp:TextBox ID="txt_date_to" runat="server" class="form-control mdl-txtbx-tyle"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                <a data-ng-click="ButtonClickSaveUpdate()" href="#!" class="btn btn-primary">Save changes</a>
                            </div>


                            <div class="assignedtblwpr" id="assignedtbls">
                                <table class="table table-bordered">
                                    <tr>
                                        <th>#</th>
                                        <th>Period</th>
                                        <th>Class</th>
                                        <th>Subject</th>
                                        <th>Assigned Teacher</th>
                                        <th>Assign From</th>
                                        <th>Assign To</th>
                                        <th>Created on</th>
                                        <th></th>
                                    </tr>
                                    <tr data-ng-repeat="x in subjectstbls">
                                        <td>{{$index+1}}</td>
                                        <td>{{x.Period_name}}</td>
                                        <td>{{x.Class_name}}</td>
                                        <td>{{x.Subject_name}}</td>
                                        <td>{{x.Teacher_name}}</td>
                                        <td>{{x.From_date}}</td>
                                        <td>{{x.To_date}}</td>
                                        <td>{{x.Created_date}}</td>
                                        <td><a href="#!" data-ng-click="ButtonClickDelete(x.Id)"><i class="bx bx-trash"></i></a></td>
                                    </tr>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="conf-alrt-sec fade in hidden" id="conf_alrt">
                    <div class="conf-alrt-inr">
                        <p class="conf-alrt-msg-p">Are you sure you want delete?</p>
                        <ul class="conf-btn-ul">
                            <li><a href="javascript:" data-ng-click="ButtonCancelDelete()" style="background: #fff; border: 1px solid #ddd; color: #0072ff;">Cancel</a></li>
                            <li><a href="javascript:" data-ng-click="ButtonConfDelete()">Ok</a></li>
                        </ul>
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
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=ddlclass.ClientID%>").val();
                var section = $("#<%=ddl_section.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("webServices/routine.asmx/fetch_class_routine_details_teacher", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section } }).then(function (response) {
                    $scope.reportHeadinG = response.data;
                    $("#intsLoader").addClass("hidden");
                    if ($scope.reportHeadinG == "") {
                        $("#grdsdatA").addClass("hidden");
                        $("#NotFounD").removeClass("hidden");
                        <%--$("#excelbtnS").addClass("hidden");
                        $("#<%=print1.ClientID%>").addClass("hidden");--%>
                    }
                    else {
                        $("#grdsdatA").removeClass("hidden");
                        $("#NotFounD").addClass("hidden");
                        $http.get("webServices/routine.asmx/fetch_class_routine_details_day_teacher", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section } }).then(function (response) {
                            $scope.reportdaY = response.data;
                        })

                        <%--$("#excelbtnS").removeClass("hidden");
                        $("#<%=print1.ClientID%>").removeClass("hidden");--%>
                    }
                })
            }



            //Teacher/Subject Update
            $scope.ButtonClickUpdate = function (dates, day_name, Period_Name, Period_id) {
                //alert(Period_Name);
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=ddlclass.ClientID%>").val();
                var section = $("#<%=ddl_section.ClientID%>").val();
                $("#<%=txt_date_from.ClientID%>").val(dates);
                $("#<%=txt_date_to.ClientID%>").val(dates);
                $("#<%=ddl_class_mdl.ClientID%>").val(class_id);
                $("#<%=txt_section_mdl.ClientID%>").val(section);
                $("#<%=txt_period_name_mdl.ClientID%>").val(Period_Name);
                $("#<%=txt_period_id.ClientID%>").val(Period_id);
                $http.get("webServices/routine.asmx/fetch_subjects", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section } }).then(function (response) {
                    $scope.subjects = response.data;
                })


                $http.get("webServices/routine.asmx/fetch_added_subjects_table", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Period_id": Period_id } }).then(function (response) {
                    $scope.subjectstbls = response.data;
                    if ($scope.subjectstbls == "") {
                        $("#assignedtbls").addClass("hidden");
                    }
                    else {
                        $("#assignedtbls").removeClass("hidden");
                    }
                })
            }

            //---FetchTeacher-----------------
            $scope.test = function () {
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=ddlclass.ClientID%>").val();
                var section = $("#<%=ddl_section.ClientID%>").val();
                var subject_list = $("#<%=ddl_subject_mdl.ClientID %>").val();
                $http.get("webServices/routine.asmx/fetch_teacher_by_subject", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Subject_list": subject_list } }).then(function (response) {
                    $scope.teacher_list = response.data;
                })
            }



            //Teacher/Subject SAVE Update
            $scope.ButtonClickSaveUpdate = function () {
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var user_id = $("#<%=hd_user_id.ClientID%>").val();
                var class_id = $("#<%=ddlclass.ClientID%>").val();
                var section = $("#<%=ddl_section.ClientID%>").val();
                var subject_id = $("#<%=ddl_subject_mdl.ClientID%>").val();
                var teacher_id = $("#<%=ddl_teacher_list.ClientID%>").val();
                var date_from = $("#<%=txt_date_from.ClientID%>").val();
                var date_to = $("#<%=txt_date_to.ClientID%>").val();
                var period_id = $("#<%=txt_period_id.ClientID%>").val();

                if (subject_id == "0" || subject_id == "Select") {
                    alert("Please choose subject.");
                    ddl_subject_mdl.focus();
                }
                else if (teacher_id == "0" || teacher_id == "Select") {
                    alert("Please choose teacher.");
                    ddl_teacher_list.focus();
                }
                else if (date_from == "") {
                    alert("Please choose date from.");
                    txt_date_from.focus();
                }
                else if (date_to == "") {
                    alert("Please choose date to.");
                    txt_date_to.focus();
                }
                else {
                    //Save-Data  
                    $http.get("webServices/routine.asmx/save_updated_routine", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Subject_id": subject_id, "Teacher_id": teacher_id, "Date_from": date_from, "Date_to": date_to, "Period_id": period_id, "User_id": user_id } }).then(function (response) {
                        $scope.client_dt = response.data;
                        if ($scope.client_dt == "") {
                            alert("Record has been added successfully.");
                            <%--$("#<%=txt_name.ClientID %>").val('');
                            $("#<%=txt_email.ClientID %>").val('');
                            $("#<%=txt_message.ClientID %>").val('');
                            $("#<%=txt_captch_enter.ClientID %>").val('');
                            $("#txt_captch_enter").removeClass("txtbxError");
                            $("#<%=lbl_error_message.ClientID%>").removeClass("hidden");--%> 
                            //$("#con").addClass("hidden");
                            //$("#mesg").removeClass("hidden");


                            $http.get("webServices/routine.asmx/fetch_added_subjects_table", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Period_id": period_id } }).then(function (response) {
                                $scope.subjectstbls = response.data;
                                if ($scope.subjectstbls == "") {
                                    $("#assignedtbls").addClass("hidden");
                                }
                                else {
                                    $("#assignedtbls").removeClass("hidden");
                                }
                            })
                        }
                    })
                }
            }

            var r_id = "";
            $scope.ButtonClickDelete = function (Id) {
                r_id = Id;
                $("#conf_alrt").removeClass("hidden");
            }

            $scope.ButtonCancelDelete = function () {
                r_id = "0";
                $("#conf_alrt").addClass("hidden");
            }


            $scope.ButtonConfDelete = function () {
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=ddlclass.ClientID%>").val();
                var section = $("#<%=ddl_section.ClientID%>").val();
                var period_id = $("#<%=txt_period_id.ClientID%>").val();
                $http.get("webServices/routine.asmx/delete_subjects", { params: { "Id": r_id } }).then(function (response) {
                    $scope.client_dt = response.data;
                    if ($scope.client_dt == "") {
                        $("#conf_alrt").addClass("hidden");
                        alert("Record has been deleted successfully.");
                        $http.get("webServices/routine.asmx/fetch_added_subjects_table", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Period_id": period_id } }).then(function (response) {
                            $scope.subjectstbls = response.data;
                            if ($scope.subjectstbls == "") {
                                $("#assignedtbls").addClass("hidden");
                            }
                            else {
                                $("#assignedtbls").removeClass("hidden");
                            }
                        })
                    }
                })
            }
        });

        <%--function messgePopup(msg) {
            $("#<%=lbl_js_message.ClientID%>").text(msg);
            $('.notificationpan1').hide().slideDown(1000);
            $('.notificationpan1').delay(4000).show().slideUp(1000);
        }--%>

        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }


        $(function () {
            $("#<%=txt_date_from.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2021:2099",
                minDate: '0',
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_date_to.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2021:2099",
                minDate: '0',
            }).attr("readonly", "true");
        });
    </script>

</asp:Content>
