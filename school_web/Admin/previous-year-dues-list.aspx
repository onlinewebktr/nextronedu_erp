<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="previous-year-dues-list.aspx.cs" Inherits="school_web.Admin.previous_year_dues_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Previous Year Dues List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
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
    <script src="../assets/js/table2excel.js"></script>

    <style>
        .btn i {
            vertical-align: middle;
            font-size: inherit;
            margin-top: -1em;
            margin-bottom: -1em;
            margin-right: 5px;
        }

        .paid-cat-div-p {
            font-weight: 600;
        }

        tbody, td, tfoot, th, thead, tr {
            font-size: 13px;
        }
    </style>

    <style>
        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
        }

        .modal {
            background: rgb(0 0 0 / 39%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translate(0, 0px);
            }

        .modal-header {
            padding: .5rem 1rem;
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
            top: 20%;
            margin: 0px auto;
            border-radius: 2px;
            padding: 20px;
            width: 450px;
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
                <div class="breadcrumb-title pe-3">Fees Collections</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Previous Year Dues List</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <div class="row" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                <div class="col-xl-12">
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
                </div>
                <div class="col-xl-12">
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
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
                                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <a href="javascript:" class="btn btn-primary find-dv-btn" data-ng-click="ButtonClickFind()">Find</a>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div id="excel" runat="server" visible="false">
                                                            <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>
                                                        </div>

                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr" id="tblCustomers">
                                                <div class="col-sm-12">
                                                    <div id="tblPrintIQ" runat="server">
                                                        <div class="head-printdv" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">
                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                            </div>
                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                                <h1 style="margin: 10px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                                                </h1>

                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                    &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <span style="font-size: 14px; font-weight: bold;">Time Period-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="prnt-dv-wpr printborder hidden" id="grdsdatA">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="float: right">
                                                                <div class="angularfilter">
                                                                    <input type="text" data-ng-model="searchs" class="form-control" style="margin: 0px;" placeholder="type here to filter data" />
                                                                </div>
                                                            </div>
                                                            <table class="table table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th class="hiddenOnPrint"></th>
                                                                        <th>Admission No.</th>
                                                                        <th>Name</th>
                                                                        <th>Father Name</th>
                                                                        <th>Class</th>
                                                                        <th>Section</th>
                                                                        <th>Roll No.</th>
                                                                        <th>Mobile No.</th>
                                                                        <th>Amount</th>
                                                                        <th class="hiddenOnPrint"></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr data-ng-repeat="x in reportAmountS | filter : searchs">
                                                                        <td>{{$index+1}}</td>
                                                                        <td class="hiddenOnPrint"><a style="min-width: 30px;" class="{{x.IsEditOption}} button-61 nowordbreak collect-feesss" href="#!" data-toggle="modal" data-target="#myModal" data-ng-click="ButtonEdit(x.Session,x.Session_id,x.Class_id,x.Admission_no,x.Amount,x.Studentname,x.AlreadyPaid,x.IsMonthAnuual,x.Perticular,x.RowiD)"><span class="material-symbols-outlined">edit_square</span></a></td>
                                                                        <td>{{x.Admission_no}}</td>
                                                                        <td>{{x.Studentname}}</td>
                                                                        <td>{{x.Fathername}}</td>
                                                                        <td>{{x.Class_name}}</td>
                                                                        <td>{{x.Section}}</td>
                                                                        <td>{{x.Rollnumber}}</td>
                                                                        <td>{{x.Mobilenumber}}</td>
                                                                        <td>{{x.Amount}}</td>
                                                                        <td class="hiddenOnPrint"><a style="background-color: #f7f100; min-width: 30px; color: #000;"
                                                                            class="{{x.IsDeleteOption}} button-61 nowordbreak collect-feesss" href="#!" data-toggle="modal" data-target="#myModal" data-ng-click="ButtonDelete(x.Session_id,x.Admission_no,x.Amount)"><span class="material-symbols-outlined">delete</span></a></td>
                                                                    </tr>
                                                                </tbody>
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

                <div class="conf-alrt-sec fade in hidden" id="conf_alrt">
                    <div class="conf-alrt-inr">
                        <div class="modal-header" style="padding: 0rem 0rem; margin: 0px 0px 10px 0px;">
                            <h3 class="modal-title" style="font-size: 20px;">Update Amount</h3>
                            <a href="javascript:" class="mdl-close-btn" data-ng-click="ButtonCancelDelete()"><i class="fa fa-times" aria-hidden="true"></i></a>
                        </div>
                        <asp:TextBox ID="txt_amount_old" Style="display: none" runat="server" class="form-control"></asp:TextBox>

                        <div class="mdl-frm-row">
                            <div class="row" style="pointer-events: none">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Admission No.</label>
                                </div>
                                <div class="col-sm-8" style="pointer-events: none">
                                    <asp:TextBox ID="txt_admission_no" runat="server" Style="pointer-events: none; background: #f2f2f2;" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="mdl-frm-row">
                            <div class="row" style="pointer-events: none">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Name</label>
                                </div>
                                <div class="col-sm-8" style="pointer-events: none">
                                    <asp:TextBox ID="txt_std_name" runat="server" Style="pointer-events: none; background: #f2f2f2;" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Back Year Dues</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_back_dues_amt" runat="server" class="form-control" Style="pointer-events: none; background: #f2f2f2;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Paid</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_paid" runat="server" class="form-control" Style="pointer-events: none; background: #f2f2f2;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Dues Amount</label>
                                </div>
                                <div class="col-sm-8">

                                    <asp:TextBox ID="txt_dues_amt" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>


                                    <asp:TextBox ID="txt_IsMonthAnuual" runat="server" Style="display: none" class="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txt_Perticular" runat="server" class="form-control" Style="display: none"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-8">
                                    <a href="#!" class="btn btn-primary" data-ng-click="ButtonUpdateFee()">Update</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <div class="conf-alrt-sec fade in hidden" id="conf_alrtDel">
                    <div class="conf-alrt-inr" style="width: 300px;">
                        <p class="conf-alrt-msg-p">Are you sure you want delete?</p>
                        <ul class="conf-btn-ul">
                            <li><a href="javascript:" data-ng-click="ButtonCancelDelete()" style="background: #fff; border: 1px solid #ddd; color: #0072ff;">Cancel</a></li>
                            <li><a href="javascript:" data-ng-click="ButtonConfDelete()">Ok</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
    <asp:HiddenField ID="hd_user_id" runat="server" />
    <style>
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
    </style>


    <script type="text/javascript">
        var app = angular.module('RpCardApp', []);
        app.controller('RpCardAppCtrl', function ($scope, $http) {

            var session_id = $("#<%=ddl_session.ClientID%>").val();
            var class_id = $("#<%=ddl_class.ClientID%>").val();




            var ddlID = '#' + '<%= ddl_session.ClientID %>';
            var session_name = $(ddlID + " option:selected").text();

            var ddlIDDD = '#' + '<%= ddl_class.ClientID %>';
            var class_name = $(ddlIDDD + " option:selected").text();

            if (session_id == "0") {
                alert("Please choose session.");
                $("#<%=ddl_session.ClientID%>").focus();
                return;
            }



            messge("Please Wait...");
            $("#intsLoader").removeClass("hidden");

            $http.get("webServices/previous-dues.asmx/fetch_report_of_back_year_dues", {
                params: { "Session_id": session_id, "Class_id": class_id }
            }).then(function (response) {
                $scope.reportAmountS = response.data;
                $("#intsLoader").addClass("hidden");

                if ($scope.reportAmountS == "") {
                    $("#grdsdatA").addClass("hidden");
                    $("#NotFounD").removeClass("hidden");
                    $("#excelbtnS").addClass("hidden");
                    $("#<%=print1.ClientID%>").addClass("hidden");
                }
                else {
                    $("#grdsdatA").removeClass("hidden");
                    $("#NotFounD").addClass("hidden");
                    $("#excelbtnS").removeClass("hidden");
                    $("#<%=print1.ClientID%>").removeClass("hidden");
                }
            })


            //====FIND
            $scope.ButtonClickFind = function () {
                var session_id = $("#<%=ddl_session.ClientID%>").val();
                var class_id = $("#<%=ddl_class.ClientID%>").val();

                var ddlID = '#' + '<%= ddl_session.ClientID %>';
                var session_name = $(ddlID + " option:selected").text();

                var ddlIDDD = '#' + '<%= ddl_class.ClientID %>';
                var class_name = $(ddlIDDD + " option:selected").text();



                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("webServices/previous-dues.asmx/fetch_report_of_back_year_dues", { params: { "Session_id": session_id, "Class_id": class_id } }).then(function (response) {
                    $scope.reportAmountS = response.data;
                    $("#intsLoader").addClass("hidden");

                    if ($scope.reportAmountS == "") {
                        $("#grdsdatA").addClass("hidden");
                        $("#NotFounD").removeClass("hidden");
                        $("#excelbtnS").addClass("hidden");
                        $("#<%=print1.ClientID%>").addClass("hidden");
                    }
                    else {
                        $("#grdsdatA").removeClass("hidden");
                        $("#NotFounD").addClass("hidden");
                        $("#excelbtnS").removeClass("hidden");
                        $("#<%=print1.ClientID%>").removeClass("hidden");
                    }
                })
            }

            var Session_edt = ""; var Session_id_edt = ""; var Class_id_edt = ""; var Admission_no_edt = ""; var Amount_edt = ""; var Studentname_edt = ""; var RowiDEdt = ""; var IsMonthAnuualEdt = ""; var PerticularEdtEdt = "";
            $scope.ButtonEdit = function (Session, Session_id, Class_id, Admission_no, Amount, Studentname, AlreadyPaidAmt, IsMonthAnuual, Perticular, RowiD) {
                $("#conf_alrt").removeClass("hidden");
                Session_id_edt = Session_id;
                Class_id_edt = Class_id;
                Admission_no_edt = Admission_no;
                Amount_edt = Amount;
                Studentname_edt = Studentname;
                Session_edt = Session;
                RowiDEdt = RowiD;
                IsMonthAnuualEdt = IsMonthAnuual;
                PerticularEdtEdt = Perticular;
                $("#<%=txt_admission_no.ClientID %>").val(Admission_no_edt);
                $("#<%=txt_std_name.ClientID %>").val(Studentname_edt);
                $("#<%=txt_back_dues_amt.ClientID %>").val(Amount);

                $("#<%=txt_amount_old.ClientID %>").val(Amount);
                $("#<%=txt_paid.ClientID %>").val(AlreadyPaidAmt);
                var actualDues = Amount - AlreadyPaidAmt;
                $("#<%=txt_dues_amt.ClientID %>").val(actualDues);
                $("#<%=txt_dues_amt.ClientID %>").focus();
            }

      
            $scope.ButtonUpdateFee = function () {
                var back_year_dues = parseFloat($("#<%=txt_back_dues_amt.ClientID%>").val());
                var paid_amt = parseFloat($("#<%=txt_paid.ClientID%>").val()); 

                var dues_amt = parseFloat($("#<%=txt_dues_amt.ClientID%>").val());
                var user_by = $("#<%=hd_user_id.ClientID%>").val();

                var dues_amtss = $("#<%=txt_dues_amt.ClientID%>").val();
                if (dues_amtss == "") {
                    $("#<%=txt_dues_amt.ClientID %>").focus();
                    alert("Please enter dues amount.");
                }
                else if (dues_amt == parseFloat(Amount_edt)) {
                    alert("Please change dues amount if you want to change.");
                }
                <%--else if (dues_amt > parseFloat(Amount_edt)) {
                    alert(Amount_edt); alert(dues_amt);
                    alert("The entered amount cannot be greater than the due amount.");
                    $("#<%=txt_dues_amt.ClientID%>").focus();
                }--%>
                else {
                    messge("Please Wait...");
                    $("#intsLoader").removeClass("hidden");
                    $http.get("webServices/previous-dues.asmx/update_fee", { params: { "Session_edt": Session_edt, "Session_id": Session_id_edt, "Admission_no": Admission_no_edt, "Amount_Old": Amount_edt, "Amount_New": dues_amt, "Updated_by": user_by, "Back_year_dues": back_year_dues, "Paid_amt": paid_amt, "IsMonthAnuual": IsMonthAnuualEdt, "Perticular": PerticularEdtEdt, "RowiDEdt": RowiDEdt } }).then(function (response) {
                        $scope.client_dt = response.data;
                        if ($scope.client_dt == "") {
                            $("#conf_alrt").addClass("hidden");
                            alert("Previous year dues amount has been updated successfully.");
                            var session_id = $("#<%=ddl_session.ClientID%>").val();
                            var class_id = $("#<%=ddl_class.ClientID%>").val();
                            $http.get("webServices/previous-dues.asmx/fetch_report_of_back_year_dues", { params: { "Session_id": session_id, "Class_id": class_id } }).then(function (response) {
                                $scope.reportAmountS = response.data;
                                $("#intsLoader").addClass("hidden");

                                if ($scope.reportAmountS == "") {
                                    $("#grdsdatA").addClass("hidden");
                                    $("#NotFounD").removeClass("fade in");
                                    $("#excelbtnS").addClass("hidden");
                                    $("#<%=print1.ClientID%>").addClass("hidden");
                                }
                                else {
                                    $("#grdsdatA").removeClass("hidden");
                                    $("#NotFounD").addClass("hidden");
                                    $("#excelbtnS").removeClass("hidden");
                                    $("#<%=print1.ClientID%>").removeClass("hidden");
                                }
                            })
                        }
                    })
                }
            }




            ///DELETE
            var Session_id_del = ""; var Admission_no_del = ""; var Amount_del = "";
            $scope.ButtonDelete = function (Session_id, Admission_no, Amount) {
                Session_id_del = Session_id;
                Admission_no_del = Admission_no;
                Amount_del = Amount;
                $("#conf_alrtDel").removeClass("hidden");
            }

            $scope.ButtonConfDelete = function () {
                var user_by = $("#<%=hd_user_id.ClientID%>").val();
                $http.get("webServices/previous-dues.asmx/delete_dues", { params: { "Session_id": Session_id_del, "Admission_no": Admission_no_del, "User_by": user_by } }).then(function (response) {
                    $scope.client_dt = response.data;
                    if ($scope.client_dt == "") {
                        $("#conf_alrtDel").addClass("hidden");
                        alert("Record has been deleted successfully.");

                        //FetchV 
                        var session_id = $("#<%=ddl_session.ClientID%>").val();
                        var class_id = $("#<%=ddl_class.ClientID%>").val();
                        $http.get("webServices/previous-dues.asmx/fetch_report_of_back_year_dues", { params: { "Session_id": session_id, "Class_id": class_id } }).then(function (response) {
                            $scope.reportAmountS = response.data;
                            $("#intsLoader").addClass("hidden");

                            if ($scope.reportAmountS == "") {
                                $("#grdsdatA").addClass("hidden");
                                $("#NotFounD").removeClass("fade in");
                                $("#excelbtnS").addClass("hidden");
                                $("#<%=print1.ClientID%>").addClass("hidden");
                            }
                            else {
                                $("#grdsdatA").removeClass("hidden");
                                $("#NotFounD").addClass("hidden");
                                $("#excelbtnS").removeClass("hidden");
                                $("#<%=print1.ClientID%>").removeClass("hidden");
                            }
                        })
                    }
                })
            }

            $scope.ButtonCancelDelete = function () {
                Session_id_edt = "0"; Class_id_edt = "0"; Admission_no_edt = "0"; Session_id_del = "0"; Admission_no_del = "0"; Amount_del = "0";
                $("#conf_alrt").addClass("hidden");
                $("#conf_alrtDel").addClass("hidden");
            }

            $scope.Export = function () {
                $("#tblCustomers").table2excel({
                    filename: "Student-Dues-List.xls"
                });
            }
        });

        function messge(msg) {
            $("#<%=lblmessage.ClientID%>").text(msg);
            $('.ints-loader-wpr').hide().slideDown(0);
        }


       <%-- $(document).ready(function () {
            $("#<%=txt_dues_amt.ClientID %>").focus(function () { $(this).select(); }); 
        }--%>
    </script>
</asp:Content>
