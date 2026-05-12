<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="available-seat-of-vehicle.aspx.cs" Inherits="school_web.Admin.available_seat_of_vehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Available Seat of Vehicle
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/Angular/angular.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="slip/css/printstyle.css" rel="stylesheet" />');
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
        .wide100 {
            margin: 0px 0px;
            text-align: center;
            text-transform: uppercase;
            padding: 0px 0px;
            font-weight: 900;
            transform: rotate(-90.0deg);
            white-space: nowrap;
            line-height: 40px;
            float: right;
        }

        .margin26 {
            margin-top: -26%;
        }

        .show {
            display: block;
        }

        .hide {
            display: block;
        }

        .widelong {
            font-size: 24px;
            font-weight: 500;
        }

        .hide {
            display: none;
        }

        .tooltip1 {
            border: 1px solid #008399;
            width: 230px;
            margin-left: 23px;
            background-color: #029ab3;
            border-radius: 3px;
            color: white;
            position: absolute;
            z-index: 99999;
            text-align: left;
            padding: 5px;
            top: -192px;
            left: -88px;
        }

        .fnd-btn-ul {
            margin: 29px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
        }

            .fnd-btn-ul li {
                margin: 0px;
                padding: 0px;
                width: 100%;
                height: auto;
                float: left;
                list-style-type: none;
            }

                .fnd-btn-ul li a {
                    margin: 0px;
                    padding: 5px 0px;
                    width: 100%;
                    height: auto;
                    float: left;
                    text-decoration: none;
                    background: #f7a13e;
                    text-align: center;
                    color: #fff;
                    border-radius: 2px;
                }

        .reset-btn {
            margin: 21px 0px 0px 10px;
            padding: 4px 11px;
            width: auto;
            height: auto;
            float: left;
            text-decoration: none;
            background: #807f7e;
            text-align: center;
            color: #fff;
            border-radius: 4px;
            border: 0px;
        }


        .default-main {
            margin: 50px 0px 50px 0px;
        }

        .find-sec {
            margin: 0px 0px 30px 0px;
            padding: 5px 10px;
            width: 100%;
            height: auto;
            float: left;
            background: #ffe5af;
            border-radius: 2px;
            border: 1px solid #ffca5e;
        }

        .find-para {
            margin: 0px 0px 5px 0px;
            padding: 0px 0px;
            width: 100%;
            height: auto;
            float: left;
        }

        .default-main-h3 {
            font-weight: 500;
            color: #f5992e !important;
        }

        .bb_blue {
            border-bottom: 2px solid #e9ab2e;
            margin: 10px 0px 0px 0px;
        }

        .p-dv {
            border: 1px solid #e5ffe5;
        }

        .mob-view {
            display: none;
        }

        .spanminus {
            text-align: center;
        }

        .plot-fnt {
            font-size: 14px;
            font-weight: 600;
        }

        .plot-dt-bx {
            margin: 0px;
            width: 100%;
            float: left;
            text-align: center;
            padding: 5px;
            background: rgb(10, 204, 10);
            color: white;
        }

        @media screen and (max-width: 800px) {
            .margin26 {
                margin-top: 0%;
            }

            .find-para {
                margin: 15px 0px 5px 0px;
            }

            .fnd-btn-ul {
                margin: 15px 0px 0px 0px;
            }

            .reset-btn {
                margin: 15px 0px 0px 0px;
            }

            .bb_blue {
                border-bottom: 0px solid #e9ab2e;
            }

            .grid-wraper {
                padding: 10px 0px 0px 0px;
                margin: 0px;
                width: 100%;
                float: left;
            }

            .default-main-h3 {
                font-size: 22px;
                margin-bottom: 1.1rem !important;
            }

            .tooltip1 {
                border: 1px solid #008399;
                width: 230px;
                margin-left: 23px;
                background-color: #029ab3;
                border-radius: 3px;
                color: white;
                position: absolute;
                z-index: 99999;
                text-align: left;
                padding: 5px;
                top: 0px;
                left: 0%;
                right: 0%;
            }

            .p-dv {
                position: static;
                border: 1px solid #efac23;
            }

            .mob-view {
                display: inherit;
            }

            .tooltip1 {
                display: none;
            }


            .bed-box-wpr {
                width: 25% !important;
            }
        }

        @media screen and (max-width: 360px) {
            .plot-dt-bx {
                min-height: 185px;
            }
        }

        .hostl-dv-wpr {
            margin: 0px;
            padding: 10px 8px;
            width: 100%;
            float: left;
            border: 1px solid #e9ab2e;
        }

        .hostl-bed-dt-dv-wpr {
            margin: 0px;
            padding: 10px 8px;
            width: 100%;
            float: left;
            border: 1px solid #e9ab2e;
            border-top: 0px solid #e9ab2e;
        }

        .hostl-room-name-h {
            margin: 0px 0px 15px 0px;
            padding: 0px 0px;
            width: 100%;
            float: left;
            font-size: 20px;
            text-align: center;
            line-height: 25px;
            font-weight: 500;
        }

        .bed-box-wpr {
            margin: 0px 0px 0px 0px;
            padding: 0px 1px 1px 0px;
            width: 10%;
            float: left;
            position: relative;
        }

        .table-responsive {
            overflow-x: inherit;
        }

        .bedAvals {
            background: rgb(10, 204, 10);
        }

        .bedNotAvals {
            background: red;
            color: black;
        }

        .hidden {
            display: none;
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
                <div class="breadcrumb-title pe-3">Transport</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Available Seat of Vehicle</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <div class="row" data-ng-app="myApp" data-ng-controller="MainCtrl"> 
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
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Vehicle Name</label>
                                                        <asp:DropDownList ID="ddl_vehicle_name" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_vehicle_name_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Transport Route</label>
                                                        <asp:DropDownList ID="ddl_route" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <a href="javascript:" data-ng-click="ButtonClick()" class="btn btn-primary find-dv-btn">Find</a>
                                                        <asp:Button ID="btn_reset" class="reset-btn" runat="server" Text="Reset" OnClick="btn_reset_Click" />
                                                    </div>

                                                    <div class="col-sm-3" style="display: none">
                                                        <div id="excel" runat="server" visible="false">
                                                            <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>
                                                        </div>

                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>


                                            <div id="tblPrintIQ" runat="server">
                                                <div class="grid-wraper hidden" id="bedsInfoS">
                                                    <div id="all_data">
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="hostl-dv-wpr">
                                                                    <p class="txtbx-name-p" style="text-align: center; color: #000; padding: 0px 8px; font-size: 18px; font-weight: bold; margin: 0px 0px 0px 0px;">Vehicle Name : {{seatss[0].Transport_name}}</p>

                                                                    <p class="txtbx-name-p" style="text-align: center; color: #000; padding: 0px 8px; font-size: 15px; margin: 0px 0px 0px 0px;">
                                                                        Vehicle Registration No. : <span style="text-transform: uppercase">{{seatss[0].Vehicle_Registration_No}}</span>
                                                                    </p>


                                                                    <p class="txtbx-name-p" style="text-align: center; color: #000; padding: 0px 8px; font-size: 15px; margin: 0px 0px 0px 0px;">
                                                                        Total No. of Seat : {{seatss[0].No_of_seat}},  Total No. of Occupied Seat : {{seatss[0].Occupied}},  Total No. of Vacant Seat : {{seatss[0].Vaccant}}
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" data-ng-repeat="x in seatss track by $index">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="hostl-bed-dt-dv-wpr">
                                                                    <div class="row">
                                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                            <div class="col-lg-12  bb_blue">
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                            <h3 class="hostl-room-name-h">Transportation Route : {{x.Transportation_route}}</h3>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                            <div class="col-lg-12  bb_blue">
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="clearfix"></div>
                                                                    <div class="table_col">
                                                                        <div class="bed-box-wpr" data-ng-repeat="item in x.MyVechilesSeats track by $index">
                                                                            <div style="text-align: center; padding: 5px;" class="plot-dt-bx {{item.BackgrounDS}}" data-ng-mouseover="tooltip1=true" data-ng-mouseleave="tooltip1=false">
                                                                                <div class="spanminus">
                                                                                    <span style="font-weight: 500; font-size: 13px; line-height: 24px;">
                                                                                        <b class="plot-fnt">{{item.Sheet_No}}</b>
                                                                                        <br />
                                                                                    </span>
                                                                                </div>



                                                                                <span class="tooltip1 {{item.Avaltop }}" data-ng-show="tooltip1">Status :  {{item.Is_seat_assigned }}
                                                                                    <br />
                                                                                    <span class="{{item.AvalHidden }}">Student Name : {{item.studentname }}
                                                                                    

                                                                                   
                                                                                        <br />
                                                                                        Admission No. : {{item.Admission_no }}
                                                                                    <br />
                                                                                        Session : {{item.session }}
                                                                                    <br />
                                                                                        Class : {{item.Class_name }}
                                                                                    <br />
                                                                                        Section  : {{item.Current_Semester_or_Year }}
                                                                                    <br />
                                                                                        Assign Date : {{item.Assign_date }} </span>

                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <%--<div id="my_profile">
                                                <div data-ng-repeat="x in beds track by $index">
                                                    <div class="profile-odr-id-h-sec">
                                                        <h2 class="profile-odr-id-h">Room No. : {{x.Room_name}}</h2>
                                                    </div>
                                                    <div class="profile-tbl-wpr">
                                                        <table class="table-bordered">
                                                            <tr data-ng-repeat="item in x.MyBookingBeDs track by $index">
                                                                <td>{{item.Bed_name}}</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>--%>
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
        var app = angular.module('myApp', []);
        app.controller('MainCtrl', function ($scope, $http) {
            $scope.ButtonClick = function () {
                var session_id = $('#<%= ddl_session.ClientID %>').val();
                var vehicle_id = $('#<%= ddl_vehicle_name.ClientID %>').val();
                var route_id = $('#<%= ddl_route.ClientID %>').val();


                if (session_id == "0") {
                    alert("Please select session.");
                    $('#<%= ddl_session.ClientID %>').focus();
                    return;
                }
                <%--if (vehicle_id == "0") {
                    alert("Please select vehicle.");
                    $('#<%= ddl_vehicle_name.ClientID %>').focus();
                    return;
                }
                if (route_id == "0") {
                    alert("Please select route.");
                    $('#<%= ddl_route.ClientID %>').focus();
                    return;
                }--%>

                //=================
                //-----------Fetch_My_Order
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("webServices/transport.asmx/seat_available_ststua", { params: { "Session_id": session_id, "Vehicle_id": vehicle_id, "Route_id": route_id } }).then(function (response) {
                    $scope.seatss = response.data;
                    $("#intsLoader").addClass("hidden");
                    if ($scope.seatss == "") {
                        $("#bedsInfoS").addClass("hidden");
                    }
                    else {
                        $("#bedsInfoS").removeClass("hidden");
                    }
                })
            }



            var session_id = $('#<%= ddl_session.ClientID %>').val();
            var vehicle_id = $('#<%= ddl_vehicle_name.ClientID %>').val();
            var route_id = $('#<%= ddl_route.ClientID %>').val();

            messge("Please Wait...");
            $("#intsLoader").removeClass("hidden");
            $http.get("webServices/transport.asmx/seat_available_ststua", { params: { "Session_id": session_id, "Vehicle_id": vehicle_id, "Route_id": route_id } }).then(function (response) {
                $scope.seatss = response.data;
                $("#intsLoader").addClass("hidden");
                if ($scope.seatss == "") {
                    $("#bedsInfoS").addClass("hidden");
                }
                else {
                    $("#bedsInfoS").removeClass("hidden");
                }
            })


            $scope.Export = function () {
                $("#bedsInfoS").table2excel({
                    filename: "Seat-status-of-vehicle.xls"
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

        .topminus50 {
            top: -50px !important;
        }
    </style>
</asp:Content>
