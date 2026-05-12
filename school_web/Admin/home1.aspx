<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="home1.aspx.cs" Inherits="school_web.Admin.home1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 300px;
            height: 140px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
            left: 602px;
            top: 102px;
        }


        .chart-data-figure {
            margin: 10px 0px;
            padding: 5px 0px;
            width: 98%;
            float: left;
            text-align: center;
            font-weight: 500;
            font-size: 14px;
        }

            .chart-data-figure i {
                margin: 0px;
                padding: 3px 5px 4px;
                font-style: normal;
                color: #fff;
                border-radius: 2px;
            }

            .chart-data-figure span {
                margin: 0px;
                padding: 0px;
                font-weight: 600;
            }

        .bg1i {
            background: #b97781;
        }

        .bg2i {
            background: #596fdd;
        }

        .bg3i {
            background: #00c13b;
        }

        .bg4i {
            background: #c5bf00;
        }
    </style>
    <script type="text/javascript">
        function ShowProgress() {
            // alert('sdsjgdhsdgfsd');
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        function ShowProgress_hide() {
            document.getElementsByClassName('loading').style.visibility = 'hidden';
        }

        $('form').on("submit", function () {
            ShowProgress();
        });
    </script>

    <script type="text/javascript">
        //function openPaymentAlert() {
        //    $('#mdlAlertMsgs').modal('show');
        //} 
        $(document).ready(function () {
            var ispayRem = $('#<%= hd_is_payment_remainder.ClientID %>').val();
            if (ispayRem == "1") {
                $('#mdlAlertMsgs').modal('show');
            }
        });
    </script>


    <style>
        .payrM {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .payrM table tr th {
                padding: 5px 10px;
                font-size: 14px;
                font-weight: 600;
                text-align: left;
            }

            .payrM table tr td {
                padding: 5px 10px;
                font-size: 14px;
                font-weight: 400;
                text-align: left;
            }

        .modal {
            background: rgb(0 0 0 / 50%);
        }

        .modal-dialog {
            width: 800px;
        }

        .modal.fade .modal-dialog {
            transform: translate(0, 0px);
        }

        @media only screen and (max-width:800px) {
            .modal-dialog {
                width: 100%;
            }

            .modal-dialog {
                margin: 0px;
                padding: 0px;
            }
        }
        .card-body {
    flex: 1 1 auto;
    padding: .5rem .5rem!important;
}
    </style>


    <script type="text/javascript"> 
        //=========================
        $(document).ready(function () {
            SEND_pushNotification();

            //Update the count down every 10 second
            var x = setInterval(function () {
                SEND_pushNotification();
            }, 1200000);
        });
        function SEND_pushNotification() {
            $.ajax({
                url: 'webServices/BackGroundProcess.asmx/send_pushNotification',
                data: "{}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_is_payment_remainder" runat="server" />
    <asp:HiddenField ID="hd_TenDayS" runat="server" />
    <asp:HiddenField ID="hd_SevenDayS" runat="server" />
    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_payment_collec_class" runat="server" />
    <asp:HiddenField ID="hd_payment_estd_class" runat="server" />
    <asp:HiddenField ID="hd_payment_estd_class_adm" runat="server" />
    <asp:HiddenField ID="hd_payment_estd_class_annual" runat="server" />
    <asp:HiddenField ID="hd_payment_estd_class_overall" runat="server" />
    <asp:HiddenField ID="hd_payment_estd_class_otherfee" runat="server" />
    <asp:HiddenField ID="hd_form_sale_class_name" runat="server" />
    <asp:HiddenField ID="hd_overall_collection_mnth_class" runat="server" />
    <!--start page wrapper -->
    <div class="page-wrapper">
        <div class="page-content">
            <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                <div class="col-xl-9" id="countLeft" runat="server">
                    <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                        <div class="col-xl-4">
                            <a href="student-list.aspx">
                                <div class="card radius-10 border-start border-0 border-3 border-success" style="border-color: #00b09b!important;">
                                    <div class="card-body">
                                        <div class="d-flex align-items-center">
                                            <div class="hmb-cntnt">
                                                <p class="mb-0 text-secondary">Students(Active)</p>
                                                <h4 class="my-1 text-success" runat="server" id="ttlodR">00</h4>
                                            </div>
                                            <div class="widgets-icons-2 rounded-circle bg-gradient-ohhappiness text-white ms-auto">
                                                <%--<i class='bx bxs-bar-chart-alt-2'></i>--%>
                                                <i class="material-symbols-outlined">person_check</i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-xl-4">
                            <a href="student-list.aspx?dayScholar=1">
                                <div class="card radius-10 border-start border-0 border-3 border-info">
                                    <div class="card-body">
                                        <div class="d-flex align-items-center">
                                            <div class="hmb-cntnt">
                                                <p class="mb-0 text-secondary">Day Scholar</p>
                                                <h4 class="my-1 text-info" runat="server" id="ttlCancelAmt">00</h4>
                                            </div>
                                            <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto">
                                                <i class="material-symbols-outlined">deployed_code_account</i>

                                                <%--<i class='bx bx-user'></i>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div class="col-xl-4">
                            <div class="card radius-10 border-start border-0 border-3 border-danger" style="border-color: #006ddd !important;">
                                <a href="student-list.aspx?hostel=1">
                                    <div class="card-body">
                                        <div class="d-flex align-items-center">
                                            <div class="hmb-cntnt">
                                                <p class="mb-0 text-secondary">Hostel Students</p>
                                                <h4 class="my-1 text-danger" runat="server" id="ttlRvnuE" style="color: #006ddd !important;">₹00</h4>
                                                <%--<p class="mb-0 font-13" runat="server" id="ttlRevenueLstWeeK">+00% from last week</p>--%>
                                            </div>
                                            <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto" style="background: linear-gradient( 45deg, #f207ff, #006ddd) !important;">
                                                <%--<i class='bx bx-buildings'></i>--%>
                                                <i class="material-symbols-outlined">home_work</i>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4" style="position: relative">
                        <div class="col-xl-4">
                            <a href="student-list.aspx">
                                <div class="card radius-10 border-start border-0 border-3 border-success" style="border-color: #cdcd00 !important;">
                                    <div class="card-body">
                                        <div class="d-flex align-items-center">
                                            <div class="hmb-cntnt">
                                                <p class="mb-0 text-secondary">Transport Students</p>
                                                <h4 class="my-1 text-info" runat="server" id="lbl_total_bus" style="color: #999900 !important;">00</h4>
                                            </div>
                                            <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto" style="background: linear-gradient( 45deg, #a1a11c, #ffff00) !important;">
                                                <%--<i class='bx bx-user'></i>--%>
                                                <i class="material-symbols-outlined">airport_shuttle</i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div class="col-xl-4">
                            <a href="inactive-student-list.aspx">
                                <div class="card radius-10 border-start border-0 border-3 border-success" style="border-color: #f54e4e !important;">
                                    <div class="card-body">
                                        <div class="d-flex align-items-center">
                                            <div class="hmb-cntnt">
                                                <p class="mb-0 text-secondary">Inactive Students</p>
                                                <h4 class="my-1 text-info" runat="server" id="h2_inactive" style="color: #ef0000 !important;">00</h4>
                                                <p class="mb-0 font-13" runat="server" id="P3"></p>
                                            </div>
                                            <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto" style="background: linear-gradient( 45deg, #f54e4e, #ff7676) !important;">
                                                <%--<i class='bx bxs-user-circle'></i>--%>
                                                <i class="material-symbols-outlined">person_cancel</i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-xl-4">
                            <a href="Bithday-list.aspx?todaybirthday=1">
                                <div class="card radius-10 border-start border-0 border-3 border-danger" style="border-color: #f54ea2 !important;">
                                    <div class="card-body">
                                        <div class="d-flex align-items-center">
                                            <div class="hmb-cntnt">
                                                <p class="mb-0 text-secondary">Birthday Students</p>
                                                <h4 class="my-1 text-info" runat="server" id="lbl_todaybirtday" style="color: #ff00f7 !important;">00</h4>
                                                <p class="mb-0 font-13" runat="server" id="P2"></p>
                                            </div>
                                            <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto" style="background: linear-gradient( 45deg, #ed268a, #ff76e8) !important;">
                                                <%--<i class='bx bx-user'></i>--%>
                                                <i class="material-symbols-outlined">cake</i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>




                        <%--<div class="dashboard-more-info" data-bs-toggle="collapse" href="#multiCollapseExample1" role="button" aria-expanded="false" aria-controls="multiCollapseExample1">
                            <span class="material-symbols-outlined">more_vert</span>
                        </div>--%>
                    </div>

                    <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4"  >
                               <div class="col-xl-4">
                                <a href="#!">
                                    <div class="card radius-10 border-start border-0 border-3 border-success" style="border-color: #0c6c92 !important;">
                                        <div class="card-body">
                                            <div class="d-flex align-items-center">
                                                <div class="hmb-cntnt">
                                                    <p class="mb-0 text-secondary">
                                                       New Admission Taken <span style="font-weight: 500; color: #c000bf;"> </span>  
                                                    </p>
                                                    <h4 class="my-1 text-info" runat="server" id="lbl_total_admission_fee_taken" style="color: #999900 !important;float: left;">00</h4>
                                                 <span style="float: left;
    margin: 3px 0px 0px 0px;
    font-size: 22px;
    color: #000;">/</span><h4 class="my-1 text-info" runat="server" id="lbl_total_new_student" style="color: #17a00e!important;float: left;">00</h4>
                                                    
                                                </div>
                                                <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto" style="background: linear-gradient(45deg, #0c6c92, #00ff9d) !important;">
                                                    <i class="material-symbols-outlined">person_check</i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="col-xl-4">
                                <a href="#!">
                                    <div class="card radius-10 border-start border-0 border-3 border-success" style="border-color: #ae14d6 !important;">
                                        <div class="card-body">
                                            <div class="d-flex align-items-center">
                                                <div class="hmb-cntnt">
                                                    <p class="mb-0 text-secondary">Readmission Taken <span style="font-weight: 500; color: #c000bf;"> </span>   </p>
                                                   <h4 class="my-1 text-info" runat="server" id="lbl_total_annwal_fee" style="color: #ef0000 !important;float: left;">00</h4>
                                                    
                                                  <span style="float: left;
    margin: 3px 0px 0px 0px;
    font-size: 22px;
    color: #000;">/</span>  <h4 class="my-1 text-info" runat="server" id="lbl_total_old_student" style="color: #17a00e !important;float: left;">00</h4>
                                                    <p class="mb-0 font-13" runat="server" id="P1"></p>
                                                </div>
                                                <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto" style="background: linear-gradient(45deg, #ae14d6, #faff00) !important;">
                                                    <i class="material-symbols-outlined">person_check</i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </div>

                         <div class="col-xl-4" id="notificationBell_payment"  runat="server">
                                <a href="Pending_Online_Payment_M.aspx">
                                    <div class="card radius-10 border-start border-0 border-3 border-success" style="border-color: #ae14d6 !important;">
                                        <div class="card-body">
                                            <div class="d-flex align-items-center">
                                                <div class="hmb-cntnt">
                                                    <p class="mb-0 text-secondary">Payment Failed <span style="font-weight: 500; color: #c000bf;"> </span>   </p>
                                                    <h4 class="my-1 text-info" runat="server" id="lblpaymentfailed" style="color: #ef0000 !important;">00</h4>
                                                    <p class="mb-0 font-13" runat="server" id="P4"></p>
                                                </div>
                                                <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto" style="background: linear-gradient(45deg, #ae14d6, #faff00) !important;">
                                                    <i class="material-symbols-outlined">currency_rupee</i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>

                </div>



                <div class="col-xl-3" id="todayCollection" runat="server">
                    <div class="card radius-10 border-start border-0 border-3 border-success" style="border-color: #0087b0!important;">
                        <div class="card-body">
                            <div class="tdycollectdv">
                                <table style="width: 100%" class="table-bordered">
                                    <tr>
                                        <td colspan="2" style="text-align: center; color: #007599; font-size: 15px;"><b>Today Collection</b></td>
                                    </tr>
                                    <tr>
                                        <td style="color: #007c6e;">Monthly Fee</td>
                                        <td class="tdycollectdvtxtcntr">
                                            <asp:Label ID="lbl_ttl_tuition_fee" runat="server" Style="background: linear-gradient( 45deg, #00b09b, #96c93d) !important;"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="color: #87870c;">Transport Fee</td>
                                        <td class="tdycollectdvtxtcntr">
                                            <asp:Label ID="lbl_ttl_transport_fee" runat="server" Style="background: linear-gradient( 45deg, #a1a11c, #dfdf00) !important;"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="color: #006ddd">Hostel Fee</td>
                                        <td class="tdycollectdvtxtcntr">
                                            <asp:Label ID="lbl_hostel_fee" runat="server" Style="background: linear-gradient( 45deg, #f207ff, #006ddd) !important;"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="color: #008383;">Admission Fee</td>
                                        <td class="tdycollectdvtxtcntr">
                                            <asp:Label ID="lbl_admission_fees" runat="server" Style="background: linear-gradient( 45deg, #0ba79a, #6078ea) !important;"></asp:Label></td>
                                    </tr>
                                    <%--<tr>
                                    <td>Other Fee</td>
                                    <td>1000</td>
                                </tr>--%>
                                    <tr>
                                        <td style="color: #d51274; font-weight: 600;">Total Collection</td>
                                        <td class="tdycollectdvtxtcntr">
                                            <asp:Label ID="lbl_total_collection" runat="server" Style="background: linear-gradient( 45deg, #ed268a, #d742be) !important; font-weight: 600;"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="collapse multi-collapse" id="multiCollapseExample1" style="width: 100%; display:none" >
                <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                    <div class="col-xl-9">
                        <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                     
                        </div>
                    </div>
                    <div class="col-xl-3" runat="server" id="todaysGathredEntry">
                        <div class="card radius-10 border-start border-0 border-3 border-success" style="border-color: #0087b0!important;">
                            <div class="card-body">
                                <div class="tdycollectdv">
                                    <table style="width: 100%" class="table-bordered">
                                        <tr>
                                            <td colspan="2" style="text-align: center; color: #007599; font-size: 15px;"><b style="font-size: 14px">Today's Gathered Entries</b></td>
                                        </tr>
                                        <tr>
                                            <td style="color: #007c6e;">Monthly Fee</td>
                                            <td class="tdycollectdvtxtcntr">
                                                <asp:Label ID="lbl_ttl_tuition_fee_entry" runat="server" Style="background: linear-gradient( 45deg, #00b09b, #96c93d) !important;"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="color: #87870c;">Transport Fee</td>
                                            <td class="tdycollectdvtxtcntr">
                                                <asp:Label ID="lbl_ttl_transport_fee_entry" runat="server" Style="background: linear-gradient( 45deg, #a1a11c, #dfdf00) !important;"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="color: #006ddd">Hostel Fee</td>
                                            <td class="tdycollectdvtxtcntr">
                                                <asp:Label ID="lbl_hostel_fee_entry" runat="server" Style="background: linear-gradient( 45deg, #f207ff, #006ddd) !important;"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="color: #008383;">Admission Fee</td>
                                            <td class="tdycollectdvtxtcntr">
                                                <asp:Label ID="lbl_admission_fees_entry" runat="server" Style="background: linear-gradient( 45deg, #0ba79a, #6078ea) !important;"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="color: #d51274; font-weight: 600;">Total Collection</td>
                                            <td class="tdycollectdvtxtcntr">
                                                <asp:Label ID="lbl_total_collection_entry" runat="server" Style="background: linear-gradient( 45deg, #ed268a, #d742be) !important; font-weight: 600;"></asp:Label></td>
                                        </tr>


                                        <tr>
                                            <td colspan="2" style="color: #d51274; font-weight: 600; text-align: center"><a href="todays-gathered-entries.aspx" style="background: linear-gradient(45deg, #19d6c5, #00aea5) !important; font-weight: 600; color: #fff; border-radius: 3px; padding: 1px 5px; box-shadow: 0 2px 6px 0 rgb(218 218 253 / 65%), 0 2px 6px 0 rgb(206 206 238 / 54%); }">View Details</a></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="card radius-10">
                <div class="card-body" style="padding: 1rem 0rem 2rem 1rem; display: none">
                    <div class="row">
                        <div class="col-sm-2">
                            <label for="validationCustom01" class="find-dv-lbl">Session</label>
                            <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <label for="validationCustom01" class="find-dv-lbl">Class</label>
                            <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <label for="validationCustom01" class="find-dv-lbl">Section</label>
                            <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <label for="validationCustom01" class="find-dv-lbl">Student Type</label>
                            <asp:DropDownList ID="ddl_studenttype" runat="server" class="form-control find-dv-txtbx">
                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                <asp:ListItem Value="New">New Admission</asp:ListItem>
                                <asp:ListItem Value="NT">Old Admission</asp:ListItem>
                                <asp:ListItem Value="Transferred">Transferred to Next Session</asp:ListItem>
                            </asp:DropDownList>
                        </div>


                        <div class="col-sm-1">
                            <asp:Button ID="btn_find_admission" runat="server" Text="Find" class="btn btn-primary find-dv-btn" />
                        </div>
                    </div>
                </div>



                <div class="home-grph-wprS" style="overflow: hidden">
                    <div class="row">
                        <div class="col-xl-8">
                            <div class="lftgrapHHS" style="margin: 0px 0px 0px -100px; float: left;">
                                <div id="chart"></div>
                            </div>
                        </div>
                        <div class="col-xl-4">
                            <div class="home-grph-wpr-smll1">
                                <%--<div id="daily_collection" class="card card-statistic-2" style="margin: 0px 0px 0px -70px; width: 100%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 10px;"></div>--%>
                                <canvas id="myDoughnutChart" width="150" height="150"></canvas>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" id="graph" runat="server">
                <div class="col-12 col-lg-12">
                    <div class="card radius-10">
                        <div class="card-body" style="padding: 1rem 0rem 2rem 1rem;">
                            <div class="d-flex align-items-center">
                                <div>
                                    <h6 class="mb-0">Last 15 Days Collection
                                    <asp:DropDownList ID="ddl_months" AutoPostBack="true" OnSelectedIndexChanged="ddl_months_SelectedIndexChanged" runat="server" class="form-select" Style="float: right; width: 110px; margin: -4px 0px 0px 20px; padding: 2px 1px 2px 10px; font-size: 14px; display: none">
                                    </asp:DropDownList></h6>
                                </div>
                            </div>
                            <div class="chart-container-0" style="height: auto;">
                                <div class="chart">
                                    <canvas id="barChartDays" width="400" height="120"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-lg-12">
                    <div class="card radius-10">
                        <div class="card-body" style="padding: 1rem 0rem 2rem 1rem;">
                            <div class="d-flex align-items-center">
                                <div>
                                    <h6 class="mb-0">Monthwise Collection Summary
                                    <asp:DropDownList ID="ddl_class_collection_month_overall" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_collection_month_overall_SelectedIndexChanged" runat="server" class="form-select" Style="float: right; width: 110px; margin: -4px 0px 0px 20px; padding: 2px 1px 2px 10px; font-size: 14px;">
                                    </asp:DropDownList></h6>
                                </div>
                            </div>
                            <div class="chart-container-0"> 
                                <div class="chart">
                                    <canvas id="barChartOverallCollection" style="min-height: 300px; height: 300px; max-height: 300px; max-width: 100%;"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end page wrapper -->
    </div>


    <div id="mdlAlertMsgs" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 800px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 10px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Payment Reminder</h5>
                    <a href="#!" data-dismiss="modal" style="position: absolute; top: 10px; right: 10px; color: #000; font-weight: 300; font-size: 15px; background: none; border: 0px;">❌</a>
                </div>
                <div class="modal-body" style="padding: 5px 5px;">
                    <div class="p-4 border rounded" style="float: left; width: 100%; padding: 5px 5px !important;">
                        <div class="payrM">
                            <p style="margin: -7px 0px 3px 0px; padding: 0px 0px 0px 0px; width: 100%; float: left; font-size: 15px; color: #000; line-height: 27px;">
                                Dear Sir/Madam, Follwing invoices are due/over-due for payment.
                            </p>

                            <div style="width: 100%; float: left; overflow: auto;">
                                <table style="width: 100%; margin: 0px;" class="table table-hover table-striped table-bordered">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Invoice No.</th>
                                            <th>Due Date</th>
                                            <th>Amount</th>
                                            <th>GST(18%)</th>
                                            <th>Total Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RPPDetails" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Invoice_no" runat="server" Text='<%#Bind("Invoice_no") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Dues_date" runat="server" Text='<%#Bind("Dues_date") %>'></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_gst_amount" runat="server" Text='<%#Bind("Gst_amount") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_Total_amount" runat="server" Text='<%#Bind("Total_amount") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr style="background: #f9ffe5;">
                                            <th colspan="5" style="text-align: right;">Total</th>
                                            <th>
                                                <asp:Label ID="lbl_Total_final_amount" runat="server"></asp:Label></th>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <p style="margin: 6px 0px 3px 0px; padding: 0px 0px 0px 0px; width: 100%; float: left; font-size: 15px; color: #f90000; line-height: 24px;">
                                Kindly make immediate payment for continued and better services. Kindly call us at 7546010004 for any billing related issues. Thank You.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <section>
        <div class="intrnlchatdv" id="chatDV" runat="server" visible="false">
            <a href="#!" target="_blank" id="chatLink" runat="server">
                <span class="material-symbols-outlined">chat</span> <i>Chat</i>
            </a>
        </div>
    </section>
    <style>
        .back-to-top {
            display: none !important;
        }
    </style>

    <asp:HiddenField ID="hd_session_id" runat="server" />
    <asp:HiddenField ID="hd_session_name" runat="server" />
    <asp:HiddenField ID="hd_branch_id" runat="server" />
    <asp:HiddenField ID="hd_months" runat="server" />
    <script src="../Echart/echarts.min.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            var session_id = $('#<%= hd_session_id.ClientID %>').val();
            var session_name = $('#<%= hd_session_name.ClientID %>').val();
            var branch_id = $('#<%= hd_branch_id.ClientID %>').val();
            var payment_estd_class = $('#<%= hd_payment_estd_class.ClientID %>').val();
            var payment_estd_class_adm = $('#<%= hd_payment_estd_class_adm.ClientID %>').val();
            var payment_estd_class_annual = $('#<%= hd_payment_estd_class_annual.ClientID %>').val();
            var payment_collec_class = $('#<%= hd_payment_collec_class.ClientID %>').val();
            var payment_collec_class_overall = $('#<%= hd_payment_estd_class_overall.ClientID %>').val();
            var payment_collec_class_otherfee = $('#<%= hd_payment_estd_class_otherfee.ClientID %>').val();
            var payment_collec_class_form_sale = $('#<%= hd_form_sale_class_name.ClientID %>').val();
            var payment_collec_overall_mnth_class = $('#<%= hd_overall_collection_mnth_class.ClientID %>').val();
            var monthsdays = $('#<%= hd_months.ClientID %>').val();


            //======================================
            $(function () {
                var type = "MonthlyFee";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "webServices/finance.asmx/find_months_collections_report_days",
                    data: '{"Session_id":"' + session_id + '","Session_name":"' + session_name + '","Branch_id":"' + branch_id + '", "Monthsdays":"' + monthsdays + '"}',
                    dataType: "json",
                    success: function (response) {
                        var chartData = JSON.parse(response.d);
                        load_daywise_chart(chartData)
                    },
                });
            })






            //===================================OVERALL Mothlywise
            $(function () {
                var type = "MonthlyFee";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "webServices/finance.asmx/find_overall_collections_report_monthwise",
                    data: '{"Session_id":"' + session_id + '","Session_name":"' + session_name + '", "Branch_id":"' + branch_id + '","Payment_estd_class_overall":"' + payment_collec_overall_mnth_class + '"}',
                    dataType: "json",
                    success: function (response) {
                        var chartData = JSON.parse(response.d);
                        load_monthlywise_chart(chartData)
                    },
                });
            })
        });



        function load_daywise_chart(data) { 
            const options = {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                },
                plugins: {
                    datalabels: {
                        anchor: 'end',
                        align: 'end',
                        formatter: (value) => value,
                        color: 'black',
                        font: {
                            weight: 'bold',
                            size: 12
                        }
                    }
                }
            };

            const ctx1 = document.getElementById('barChartDays').getContext('2d');
            const myBarChart = new Chart(ctx1, {
                type: 'bar',
                data: data,
                options: options,
                plugins: [ChartDataLabels] // Register the data labels plugin
            });
        }

        function load_monthlywise_chart(data) {
            const options = {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                },
                plugins: {
                    datalabels: {
                        anchor: 'end',
                        align: 'end',
                        formatter: (value) => value,
                        color: 'black',
                        font: {
                            weight: 'bold',
                            size: 12
                        }
                    }
                }
            };

            const ctx1 = document.getElementById('barChartOverallCollection').getContext('2d');
            const myBarChart = new Chart(ctx1, {
                type: 'bar',
                data: data,
                options: options,
                plugins: [ChartDataLabels] // Register the data labels plugin
            });
        }
    </script>


    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: 'Student Summary',
                width: 1000,
                height: 420,
                legend: { position: 'top', maxLines: 3 },
                bar: { groupWidth: '95%' },
                isStacked: true,
                is3D: true,
                colors: ['#15CA20', '#008CFF', '#F7F700', '#11FFFD', '#AA398F'],
                hAxis: {
                    textStyle: {
                        fontSize: 10, // or the number you want
                        is3D: true,
                        italic: true
                    }
                }
            };

            $.ajax({

                type: "POST",
                url: "student-list.aspx/GetChartData",
                data: "{Session: '" + $('#<%=ddlsession.ClientID%>').val() + "', Class_id: '" + $('#<%=ddlclass.ClientID%>').val() + "', Section: '" + $('#<%=ddl_section.ClientID%>').val() + "', Student_type: '" + $('#<%=ddl_studenttype.ClientID%>').val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.ColumnChart($("#chart")[0]);
                    chart.draw(data, options);
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            });
        }


        //==============================Order Status SummarY

        $(document).ready(function () {

            var Session = $('#<%= ddlsession.ClientID %>').val();
            var Class = $('#<%= ddlclass.ClientID %>').val();
            var Section = $('#<%= ddl_section.ClientID %>').val();
            var Student_Type = $('#<%= ddl_studenttype.ClientID %>').val();

            $(function () {
                var type = "Student Type";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "webServices/finance.asmx/find_old_new_student_count",
                    data: '{"Session":"' + Session + '","Class_id":"' + Class + '","Section":"' + Section + '", "Student_type":"' + Student_Type + '"}',
                    dataType: "json",
                    success: function (response) {
                        var chartData = JSON.parse(response.d);
                        load_chart111(chartData)
                    },
                });
            })
        });


        function load_chart111(data) {
            const config = {
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Doughnut Chart with Values',
                        },
                        datalabels: {
                            color: '#fff',
                            formatter: (value, context) => {
                                return value; // Display the value
                            },
                        },
                    },
                },
                plugins: [ChartDataLabels], // Register the datalabels plugin
            };
            const ctx = document.getElementById('myDoughnutChart').getContext('2d');
            const myDoughnutChart = new Chart(ctx, {
                type: 'doughnut',
                data: data,
                config: config,
                plugins: [ChartDataLabels] // Register the data labels plugin
            });
        }
    </script>







    <div style="height: 1px; overflow: hidden">
        <asp:Button ID="btnSubmit" runat="server" Text="Load Customers"
            OnClick="btnSubmit_Click" OnClientClick="retun ShowProgress();" />
    </div>
    <div class="loading" align="center" id="a1" runat="server">
        Please wait. for send push
        <br />
        <br />
        <img src="../images/loader.gif" />
    </div>
</asp:Content>
