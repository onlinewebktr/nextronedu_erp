<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="fee-collectionJs.aspx.cs" Inherits="school_web.Admin.fee_collectionJs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Fee Collection
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="slip/payment-slip.css" rel="stylesheet" />
    <style type="text/css">
        th {
            font-weight: 500;
            .select()
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 19px;
            height: 19px;
            position: relative;
            top: 5.4px;
            left: 0px;
            margin: 0px 5px 0px 0px;
            z-index: 9;
        }

        .hide1 {
            display: none;
        }

        .print-btn-sec {
            margin: 0px;
            padding: 3px;
            width: 100%;
            height: auto;
            float: left;
            text-align: center;
        }

        .discDetailSbtn {
            margin: 8px 0px 0px 0px;
            padding: 5px 10px 6px 5px;
            width: auto;
            float: right;
            position: absolute;
            right: 2px;
            background: #00a4a2;
            color: #fff;
            font-weight: 500;
            font-size: 13px;
        }

            .discDetailSbtn:before {
                content: "";
                position: absolute;
                width: 0px;
                height: 0px;
                border-top: 15px solid transparent;
                border-bottom: 15px solid transparent;
                border-right: 23px solid #00a4a2;
                left: -23px;
                top: 0px;
            }

            .discDetailSbtn:hover {
                background: #00a4a2;
                color: #fff;
            }

        .revisePaySbtn {
            margin: 8px 0px 0px 0px;
            padding: 5px 10px 6px 5px;
            width: auto;
            float: left;
            position: absolute;
            left: 2px;
            background: #00a4a2;
            color: #fff;
            font-weight: 500;
            font-size: 13px;
        }

            .revisePaySbtn:before {
                content: "";
                position: absolute;
                width: 0px;
                height: 0px;
                border-top: 15px solid transparent;
                border-bottom: 15px solid transparent;
                border-left: 23px solid #00a4a2;
                right: -23px;
                top: 0px;
            }

            .revisePaySbtn:hover {
                background: #00a4a2;
                color: #fff;
            }

        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }

        .modal {
            background: rgb(0 0 0 / 43%);
        }

        .disc-tbl-wprs {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .disc-tbl-wprs tr th {
                padding: 5px 5px !important;
            }

            .disc-tbl-wprs tr td {
                padding: 5px 5px !important;
            }

        .prowwprs {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .fnd-box-wpr-inr {
            padding: 0px 5px;
        }

        .p-4 {
            padding: 0.2rem !important;
        }

        .page-content {
            padding: 0.5rem 0.7rem 0.7rem 1.5rem
        }
    </style>
    <script src="../assets/Angular/angular.min.js"></script>
    <script type="text/javascript">
        <%--$(function () {
            var sessionid = $("#<%=ddl_session_student.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'fee-collection-monthly-wise.aspx/GetRooPath',
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
        });--%>

        $(function () {
            var sessionid = $("#<%=ddl_session.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'fee-collectionJs.aspx/GetRooPathAdmNo',
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_user_Type" runat="server" />
    <asp:HiddenField ID="hd_branch_id" runat="server" />
    <asp:HiddenField ID="hd_user_id" runat="server" />
    <!--start page wrapper -->
    <div class="page-wrapper" data-ng-app="RpFeeApp" data-ng-controller="RpFeeAppCtrl">
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

            <div class="ints-loader-wpr" id="intsLoader">
                <div class="ints-loader-wpr-inr">
                    <div class="ints-loader">
                        <p class="ints-loader-txt">
                            <img src="assets/images/loader-ico.gif" class="ints-loader-img" />
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>


            <asp:HiddenField ID="hd_admission_no" runat="server" />
            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row" id="findSection" runat="server">
                                    <div class="col-xl-6">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="row">
                                                    <div class="col-xl-5 padd-rght-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-5 padd-rght-5">
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
                                                    </div>
                                                    <div class="col-xl-2 padd-rght-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-12 padd-lft-5">
                                                                    <a href="#!" class="btn btn-primary form-fnd-btns" data-ng-click="BtnClickFindStudent()">Find</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row hidden" id="stdInfoDV">
                                    <div class="col-xl-12">
                                        <div class="fnd-box-wpr">
                                            <div class="fnd-box-row-wpr-h std-info-ch" data-bs-toggle="collapse" href="#multiCollapseExample1" role="button" aria-expanded="false" aria-controls="multiCollapseExample1">
                                                <span class="material-symbols-outlined fullscreenIco">close_fullscreen</span>
                                                <ul class="std-info-ul">
                                                    <li><i>Adm. No. : </i>
                                                        <asp:Label ID="lbl_admission_no_c" runat="server">{{stdDt[0].Admission_no}}</asp:Label></li>
                                                    <li><i>Name : </i>
                                                        <asp:Label ID="lbl_name_c" runat="server">{{stdDt[0].Studentname}}</asp:Label></li>
                                                    <li><i>Class : </i>
                                                        <asp:Label ID="lblclass_show_c" runat="server">{{stdDt[0].Class_name}} / {{stdDt[0].Section}}</asp:Label></li>
                                                    <li><i>Transport : </i>
                                                        <asp:Label ID="lbl_transport_c" runat="server">{{stdDt[0].Transport_ntaken}}</asp:Label></li>
                                                    <li><i>Hostel : </i>
                                                        <asp:Label ID="lbl_hostel_c" runat="server">{{stdDt[0].Hosteltaken}}</asp:Label></li>
                                                </ul>
                                            </div>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="collapse multi-collapse" id="multiCollapseExample1">
                                                    <div class="fnd-box-row-wpr stdinfo-lft">
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_name" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Studentname}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_father_name" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Fathername}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Type : </label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <asp:Label ID="lbl_studentype" runat="server" Text=" " Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Transfer_Status}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Admission No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_admission_no" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Admission_no}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lblclass_show" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Class_name}}</asp:Label>
                                                                        <asp:Label ID="lblclass" Visible="false" runat="server" Text=" " Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Class_name}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Roll No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <asp:Label ID="txtroll_no" runat="server" Font-Bold="true" Text=" " class="stdnt-info-fnds">{{stdDt[0].Rollnumber}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Transportation : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbltransporttion" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Transport_ntaken}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Contact no. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_phone" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Contact_no}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Hostel : </label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <asp:Label ID="lblhostel" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Hosteltaken}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Category : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_catogery" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Category_mame}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Sub Category : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_subcatogery" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Sub_category_name}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row {{stdDt[0].TransPortDv}}">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Start Month : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_start_month" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Month_name}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5" style="padding-right: 0px;">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Transportation Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_transportname" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Transport_name}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Seat No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <asp:Label ID="lbl_seatno" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].seatname}}</asp:Label>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-12 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-2 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Boarding Point :  </label>
                                                                    </div>
                                                                    <div class="col-xl-10 padd-lft-5">
                                                                        <asp:Label ID="lbl_boarding_point" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Boarding_Point}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-12 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-2 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Transport Route : </label>
                                                                    </div>
                                                                    <div class="col-xl-10 padd-lft-5">
                                                                        <asp:Label ID="lbl_transport_Route" runat="server" Font-Bold="true" class="stdnt-info-fnds">{{stdDt[0].Transportpathpath}}</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="fnd-box-row-wpr stdinfo-rght">
                                                        <div class="stdinfo-rght-imgwpr">
                                                            <asp:Image ID="Image1" runat="server" class="{{stdDt[0].StudentImgPath}}" ImageUrl="{{stdDt[0].StudentImgPath}}" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" data-ng-repeat="x in stdDt track by $index">
                                        <div class="col-xl-12">
                                            <div class="fnd-box-wpr" style="margin: 5px 0px 0px 0px;">
                                                <h2 class="fnd-box-row-wpr-h">Select Month 
                                                    <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-primary form-fnd-btns" Style="background: #f00; border: 1px solid #ddd; width: auto; margin: -3px 0px -2px 0px; padding: 2px 10px 3px; float: right;" /></h2>
                                                <div class="fnd-box-wpr-inr" style="padding: 0px 5px 5px 5px;">
                                                    <div class="month-checkbox">
                                                        <div data-ng-repeat="item in x.MyDuesMonthItem track by $index" style="width: auto; float: left; padding-right: 10px;">
                                                            <a href="#!" data-ng-click="BtnClickMonth(item.Month,item.Session_id,item.Session_name,item.Class_id,item.Admission_no,item.Parameter,item.Parameter_id,item.Is_quarterwise_payment,item.ParameteridS,item.Class_name)">{{item.Month}}</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                         <div class="col-xl-6" style="padding-right: 5px">
                                            <div class="fnd-box-wpr" style="margin: 5px 0px 0px 0px;">
                                                <h2 class="fnd-box-row-wpr-h" style="background: #f9f9f9;">MonthWise Fees Details</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <asp:Panel ID="pnl_month_wise_fee_details" runat="server" Visible="false">
                                                        <table style="width: 100%;" class="table table-hover table-bordered">
                                                            <tr>
                                                                <th>Month</th>
                                                                <th>Fees Head</th>
                                                                <th>Amount</th>
                                                                <th>Discount</th>
                                                                <th>Paid Prev.</th>
                                                                <th>Payable</th>
                                                            </tr>
                                                            <asp:Repeater ID="rp_fee_details" runat="server">
                                                                <ItemTemplate>
                                                                    <tr id="row" runat="server">
                                                                        <td>
                                                                            <asp:Label ID="lbl_mnth" runat="server" Text='<%#Bind("months") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblcontent" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="txtalignrights">
                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount","{0:n}") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="txtalignrights">
                                                                            <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("disc_amount") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="txtalignrights">
                                                                            <asp:Label ID="lbl_pre_paid" runat="server" Text='<%#Bind("previously_paid") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="txtalignrights">
                                                                            <asp:Label ID="lbl_tot_pble" runat="server" Text='<%#Bind("total_payable") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                            <tr>
                                                                <th colspan="2">Total :
                                                                </th>
                                                                <th class="txtalignrights">
                                                                    <asp:Label ID="lbl_fee_amount" runat="server" Text=""></asp:Label></th>
                                                                <th class="txtalignrights">
                                                                    <asp:Label ID="lbl_discount" runat="server" Text=""></asp:Label></th>
                                                                <th class="txtalignrights">
                                                                    <asp:Label ID="lbl_paid_prev" runat="server" Text=""></asp:Label></th>
                                                                <th class="txtalignrights">
                                                                    <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label></th>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
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

    <asp:HiddenField ID="hd_is_quarterwise_payment" runat="server" />
    <script type="text/javascript">
        var app = angular.module('RpFeeApp', []);
        app.controller('RpFeeAppCtrl', function ($scope, $http, $exceptionHandler) {

           <%-- var session_id = $("#<%=hd_session_id.ClientID%>").val();
            var class_id = $("#<%=hd_class_id.ClientID%>").val();
            var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
            var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
            var term_id = $("#<%=hd_term_id.ClientID%>").val();--%>


            $scope.BtnClickFindStudent = function () {

                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var session_id = $("#<%=ddl_session.ClientID%>").val();
                var admission_no = $("#<%=txt_admission_no.ClientID%>").val();
                var Is_quarterwise_payment = $("#<%=hd_is_quarterwise_payment.ClientID%>").val();

                if (session_id == "0") {
                    $("#<%=ddl_session.ClientID%>").focus();
                    alert("Please select session.");
                }
                else if (admission_no == "") {
                    $("#<%=txt_admission_no.ClientID%>").focus();
                    alert("Please enter admission no.");
                }
                else {
                    messge("Please Wait...");
                    $("#intsLoader").removeClass("hidden");

                    $http.get("webServices/Fees.asmx/fetch_student_details", { params: { "Session_id": session_id, "Admission_no": admission_no, "Branch_id": branch_id, "Is_quarterwise_payment": Is_quarterwise_payment } }).then(function (response) {
                        $scope.stdDt = response.data;
                        if ($scope.stdDt == "") {
                            $("#stdInfoDV").addClass("hidden");
                        }
                        else {
                            $("#stdInfoDV").removeClass("hidden");
                        }
                    })
                    $("#intsLoader").addClass("hidden");
                }
            }


            $scope.BtnClickMonth = function (Month, Session_id, Session_name, Class_id, Admission_no, Parameter, Parameter_id, Is_quarterwise_payment, ParameteridS, Class_name) {
                alert(Month + Session_id + Session_name + Class_id + Admission_no + Parameter + Parameter_id + Is_quarterwise_payment + ParameteridS + Class_name);
            }


            //$http.get("webService/report-card.asmx/fetch_rp_card_subjects", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
            //    $scope.reportCardSubS = response.data;
            //    $("#intsLoader").addClass("hidden");


            //    if ($scope.reportCardSubS == "") {
            //    }
            //    else {
            //        ////========================GRAPH
            //        $http.get("webService/report-card.asmx/fetch_rp_card_graph", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
            //            $scope.reportGraphs = response.data;
            //        })


            //        ////========================OverAll No.
            //        $http.get("webService/report-card.asmx/fetch_rp_card_total_no", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
            //            $scope.ttlNos = response.data;
            //        })
            //    }
            //})
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
            padding: 7px 5px !important;
            font-size: 12px;
            font-weight: 700;
        }

        table tr td {
            padding: 7px 5px !important;
            font-size: 12px;
            border: 1px solid #adadad;
        }

        tfoot, th, thead {
            background: #FFBA5F !important;
        }

        .table {
            border-color: #adadad;
        }

        .form-label-fnds {
            font-size: 12px;
        }

        .find-dv-txtbx {
            padding: 2px 6px;
            font-size: 12px;
            height: 25px;
            line-height: 19px;
        }

        label {
            font-size: 14px;
        }

        .txtalignrights {
            text-align: right;
        }

        .month-checkbox {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .month-checkbox label {
                margin: 0px 3px 0px 0px;
            }

        .fnd-box-row-wpr-h {
            background: #f5f3f3;
        }

        .card-body {
            padding: 0;
        }

        .card {
            background-color: rgb(255 255 255 / 0%);
            box-shadow: 0 2px 6px 0 rgb(218 218 253 / 0%), 0 2px 6px 0 rgb(206 206 238 / 0%);
        }

        .stdinfo-lft {
            width: 87%;
        }

        .stdinfo-rght {
            width: 13%;
        }

        .stdinfo-rght-imgwpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            height: 131px;
            overflow: hidden;
            border: 2px dashed #FFBA5F;
            border-radius: 2px;
        }

            .stdinfo-rght-imgwpr img {
                width: 100%;
            }

        .popup-adm-fees-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .popup-adm-std-wpr {
            margin: 0px;
            padding: 5px 7px 5px 7px;
            width: 100%;
            float: left;
            border-bottom: 1px solid #ddd;
        }

        .popup-adm-std-info {
            margin: 0px;
            padding: 0px;
            width: 85%;
            float: right;
        }

        .popup-adm-std-imgs {
            margin: 0px;
            padding: 0px;
            width: 13%;
            float: left;
            height: 131px;
            overflow: hidden;
            border: 2px dashed #FFBA5F;
            border-radius: 2px;
        }

            .popup-adm-std-imgs img {
                width: 100%;
            }

        .adm-box-wprs1 {
            margin: 0px;
            padding: 5px 5px 5px 5px;
            width: 100%;
            float: left;
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
        }

            .adm-box-wprs1 table {
                margin: 0px;
            }

        .adm-box-wprs2 {
            margin: 0px;
            padding: 5px 5px 5px 5px;
            width: 100%;
            float: left;
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
        }

        .adm-box-wprs3 {
            margin: 0px 0px 0px -1px;
            padding: 5px 5px 5px 5px;
            width: 100%;
            float: left;
            border-left: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
        }

        .lblfnts {
            font-size: 12.5px;
        }










        /* CSS */
        .button-37 {
            margin: 2px 0px 0px 0px;
            float: left;
            font-weight: 600;
            padding: 3px 25px 5px;
            background-color: #13aa52;
            border: 1px solid #13aa52;
            border-radius: 4px;
            box-shadow: rgba(0, 0, 0, .1) 0 2px 4px 0;
            box-sizing: border-box;
            color: #fff;
            cursor: pointer;
            font-size: 14px;
            text-align: center;
            transform: translateY(0);
            transition: transform 150ms, box-shadow 150ms;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
        }

            .button-37:hover {
                color: #fff;
                box-shadow: rgba(0, 0, 0, .15) 0 3px 9px 0;
                transform: translateY(-2px);
            }
    </style>
</asp:Content>
