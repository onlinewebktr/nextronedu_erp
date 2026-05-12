<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="fee-collection-temp.aspx.cs" Inherits="school_web.Admin.fee_collection_temp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/Angular/angular.min.js"></script>
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

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }


        .gotohostelbtn {
            margin: -2px 6px 0px 0px;
            float: right;
            align-items: center;
            appearance: none;
            border-top: 1px solid #c7c100;
            border-right: 1px solid #c7c100;
            border-left: 1px solid #c7c100;
            border-bottom: 0px;
            background-image: radial-gradient(100% 100% at 100% 0, #f0ff00 0, #efff07 100%);
            /* border: 0; */
            border-radius: 3px;
            box-shadow: rgb(61 66 35 / 40%) 0 2px 4px, rgb(64 66 35 / 30%) 0 7px 13px -3px, rgb(110 111 58 / 50%) 0 -3px 0 inset;
            box-sizing: border-box;
            color: #000000;
            cursor: pointer;
            display: inline-flex;
            height: 27px;
            justify-content: center;
            line-height: 9px;
            list-style: none;
            font-weight: 800;
            overflow: hidden;
            padding: 0px 10px 4px 10px;
            position: relative;
            text-align: left;
            text-decoration: none;
            transition: box-shadow .15s, transform .15s;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
            white-space: nowrap;
            will-change: box-shadow, transform;
            font-size: 13px;
        }

            .gotohostelbtn:focus {
                box-shadow: #3c4fe0 0 0 0 1.5px inset, rgba(45, 35, 66, .4) 0 2px 4px, rgba(45, 35, 66, .3) 0 7px 13px -3px, #3c4fe0 0 -3px 0 inset;
            }

            .gotohostelbtn:hover {
                box-shadow: rgba(45, 35, 66, .4) 0 4px 8px, rgba(45, 35, 66, .3) 0 7px 13px -3px, #3c4fe0 0 -3px 0 inset;
                transform: translateY(-2px);
            }

            .gotohostelbtn:active {
                box-shadow: #3c4fe0 0 3px 7px inset;
                transform: translateY(2px);
            }
    </style>

    <script type="text/javascript">
        $(function () {
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
        });

        $(function () {
            var sessionid = $("#<%=ddlsessionad.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'fee-collection-monthly-wise.aspx/GetRooPathAdmNo',
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
    <script type="text/javascript"> 
        //AdmissioN
        function save_data_admission() {
            var valsubmit = $('#<%=btn_submit_admission_fee.ClientID %>').val();
            if (valsubmit == "Pay Now") {
                $('#<%=btn_submit_admission_fee.ClientID %>').val('Submitting.. Please Wait..');
                ConfirmAdmission();
                document.getElementById("<%=btn_submit_admission_fee.ClientID %>").click();
            }
            else {
                alert("Already submitted")
            }
        }

        function ConfirmAdmission() {
            var confirm_value_adm
            var isSubmitted = false;
            confirm_value_adm = document.createElement("INPUT");
            confirm_value_adm.type = "hidden";
            confirm_value_adm.name = "confirm_value_adm";
            if (confirm("Do you want to print bill?")) {
                confirm_value_adm.value = "Yes";
            }
            else {
                confirm_value_adm.value = "No";
            }
            document.forms[0].appendChild(confirm_value_adm);
        }
    </script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" /><link href="slip/payment-slip.css" rel="stylesheet" type="text/css" />');
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

    <script type="text/javascript">
        function openModalRevised() {
            $('#MdlRevisedPayment').modal('show');
        }

        function openModalAdmFee() {
            $('#MdlAdmissionFeePayment').modal('show');

        }

        function openChequeAlert() {
            $('#mdlAlertMsgs').modal('show');
        }

        function openDiscountAlert() {
            $('#mdlDiscount').modal('show');
        }



        function openExtraFeeAlert() {
            $('#mdlExtraHead').modal('show');
        }

        function openEditPaymentInfo() {
            $('#myModal').modal('show');
        }
        function openTransport() {
            $('#myModalTransport').modal('show');
        }
        function openHostel() {
            $('#myModalHostel').modal('show');
        }
        function studentInfo() {
            $('#myModalStudentInfo').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_user_Type" runat="server" />
    <div data-ng-app="RpFeeApp" data-ng-controller="RpFeeAppCtrl">
        <asp:HiddenField ID="hd_user_id" runat="server" />
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


                <asp:HiddenField ID="hd_admission_no" runat="server" />
                <asp:HiddenField ID="hd_session_id" runat="server" />
                <asp:HiddenField ID="hd_class_id" runat="server" />
                <asp:HiddenField ID="hd_session_id_for_std" runat="server" />
                <asp:HiddenField ID="HdID" runat="server" />
                <div class="row">
                    <div class="col-xl-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="p-4 border rounded">
                                    <div class="row" id="findSection" runat="server">
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
                                                                <asp:DropDownList ID="ddlsessionad" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
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
                                                <h2 class="fnd-box-row-wpr-h">Find by Roll No. </h2>
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
                                                <h2 class="fnd-box-row-wpr-h" style="padding: 4px 5px 4px 5px;">Find by Student Name <a data-toggle="modal" style="margin: 0px 0px 0px 0px; color: #fff" data-target="#myModalStudentInfo2" class="button-6161 disc-pupup-btn floatrght"><span class="material-symbols-outlined">tune</span></a></h2>
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
                                        <div class="row">
                                            <div class="col-xl-12">
                                                <div class="fnd-box-wpr">
                                                    <div class="fnd-box-row-wpr-h std-info-ch" data-bs-toggle="collapse" href="#multiCollapseExample1" role="button" aria-expanded="false" aria-controls="multiCollapseExample1">
                                                        <span class="material-symbols-outlined fullscreenIco">close_fullscreen</span>
                                                        <ul class="std-info-ul">
                                                            <li><i>Adm. No. : </i>
                                                                <asp:Label ID="lbl_admission_no_c" runat="server"></asp:Label></li>
                                                            <li><i>Name : </i>
                                                                <asp:Label ID="lbl_name_c" runat="server"></asp:Label></li>

                                                            <li><i>F. Name : </i>
                                                                <asp:Label ID="lbl_father_name1" runat="server"></asp:Label></li>

                                                            <li><i>Class : </i>
                                                                <asp:Label ID="lblclass_show_c" runat="server"></asp:Label></li>
                                                            <li><i>Bus : </i>
                                                                <asp:Label ID="lbl_transport_c" runat="server"></asp:Label></li>
                                                            <li><i>Hostel : </i>
                                                                <asp:Label ID="lbl_hostel_c" runat="server"></asp:Label></li>
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
                                                                                <asp:Label ID="lbl_name" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-4 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                            </div>
                                                                            <div class="col-xl-8 padd-lft-5">
                                                                                <asp:Label ID="lbl_father_name" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-2 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-6 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Type : </label>
                                                                            </div>
                                                                            <div class="col-xl-6 padd-lft-5">
                                                                                <asp:Label ID="lbl_studentype" runat="server" Text=" " Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
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
                                                                                <asp:Label ID="lbl_admission_no" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-4 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                            </div>
                                                                            <div class="col-xl-8 padd-lft-5">
                                                                                <asp:Label ID="lblclass_show" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                                <asp:Label ID="lblclass" Visible="false" runat="server" Text=" " Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-xl-2 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-6 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Roll No. : </label>
                                                                            </div>
                                                                            <div class="col-xl-6 padd-lft-5">
                                                                                <asp:Label ID="txtroll_no" runat="server" Font-Bold="true" Text=" " class="stdnt-info-fnds"></asp:Label>
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
                                                                                <asp:Label ID="lbltransporttion" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-4 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Contact no. : </label>
                                                                            </div>
                                                                            <div class="col-xl-8 padd-lft-5">
                                                                                <asp:Label ID="lbl_phone" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-2 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-6 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Hostel : </label>
                                                                            </div>
                                                                            <div class="col-xl-6 padd-lft-5">
                                                                                <asp:Label ID="lblhostel" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-4 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Category : </label>
                                                                            </div>
                                                                            <div class="col-xl-8 padd-lft-5">
                                                                                <asp:Label ID="lbl_catogery" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-5 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Sub Category : </label>
                                                                            </div>
                                                                            <div class="col-xl-7 padd-lft-5">
                                                                                <asp:Label ID="lbl_subcatogery" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" id="transport_data" runat="server" visible="false">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-4 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Start Month : </label>
                                                                            </div>
                                                                            <div class="col-xl-8 padd-lft-5">
                                                                                <asp:Label ID="lbl_start_month" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-4 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Boarding Point :  </label>
                                                                            </div>
                                                                            <div class="col-xl-8 padd-lft-5">
                                                                                <asp:Label ID="lbl_boarding_point" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-2 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-6 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Seat No. : </label>
                                                                            </div>
                                                                            <div class="col-xl-6 padd-lft-5">
                                                                                <asp:Label ID="lbl_seatno" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-12 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-2 padd-rght-5" style="padding-right: 0px;">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Transportation Name : </label>
                                                                            </div>
                                                                            <div class="col-xl-10 padd-lft-5">
                                                                                <asp:Label ID="lbl_transportname" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xl-12 padd-rght-5">
                                                                        <div class="row">
                                                                            <div class="col-xl-2 padd-rght-5">
                                                                                <label for="validationCustom01" class="stdnt-info-fnds">Transport Route : </label>
                                                                            </div>
                                                                            <div class="col-xl-10 padd-lft-5">
                                                                                <asp:Label ID="lbl_transport_Route" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="fnd-box-row-wpr stdinfo-rght">
                                                                <div class="stdinfo-rght-imgwpr">
                                                                    <asp:Image ID="Image1" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="imp-info-sec">
                                                            <div class=" row">
                                                                <div class="col-md-7">
                                                                    <div class="imp-info-lft">
                                                                        <asp:Label ID="lbl_dues_pay_type" Visible="false" runat="server"></asp:Label>
                                                                        <%--<asp:Button ID="btn_admission_fee" runat="server" CssClass="button-61 collectadmissionfeebtn noclick txtbxdisabled" OnClick="btn_admission_fee_Click" />--%>

                                                                        <p runat="server" id="btn_admission_fees" data-toggle="modal" data-target="#MdlAdmAnnFeeInfo" class="button-61 collectadmissionfeebtn">Add Fee</p>
                                                                        <asp:Label ID="lbl_monthly_dues_till_now" class="monthlyduesshow" runat="server"></asp:Label>
                                                                        <asp:Label ID="lbl_adission_monthly_dues_till_now" class="ttnnetduesshow" runat="server" Visible="false"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-5">
                                                                    <div class="imp-info-rght">
                                                                        <p runat="server" id="P1" data-toggle="modal" data-target="#myModalNote" class="button-6161 disc-pupup-btn floatrght">Note</p>
                                                                        <p runat="server" id="P2" data-toggle="modal" data-target="#myModalPayStatus" class="button-6161 disc-pupup-btn floatrght">Paid Details</p>
                                                                        <p runat="server" id="ExtraHeadBtn" data-toggle="modal" data-target="#mdlExtraHead" class="button-6161 disc-pupup-btn floatrght">Add Fee</p>
                                                                        <p runat="server" id="TransportBTN" data-toggle="modal" data-target="#myModalTransport" class="button-6161 disc-pupup-btn floatrght" style="min-width: 50px;">Bus</p>
                                                                        <p runat="server" id="HostelBTN" data-toggle="modal" data-target="#myModalHostel" class="button-6161 disc-pupup-btn floatrght">Hostel</p>
                                                                        <p runat="server" id="discBTN" data-toggle="modal" data-target="#mdlDiscount" class="button-6161 disc-pupup-btn floatrght">Discount</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-xl-12">
                                                <div class="fnd-box-wpr" id="spilt_month_show" style="margin: 5px 0px 0px 0px;" runat="server" visible="false">
                                                    <h2 class="fnd-box-row-wpr-h">Split Month for 
                                                    <asp:Label ID="lbl_Monthsplit" runat="server"></asp:Label></h2>
                                                    <div class="month-checkbox" style="background-color: #fff; padding: 4px 0px 6px 6px;">
                                                        <asp:Repeater ID="rep_total_split" runat="server">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Month" runat="server" Text='<%#Bind("Month")%>' Style="border-left: 0px solid;"></asp:Label>
                                                                -
                                                                 <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>' ForeColor="Red"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>

                                                <div class="fnd-box-wpr" style="margin: 5px 0px 0px 0px;">
                                                    <h2 class="fnd-box-row-wpr-h">Select Month  
                                                        <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-primary form-fnd-btns" Style="background: #f00; border: 1px solid #ddd; width: auto; margin: -3px 0px -2px 0px; padding: 2px 10px 3px; float: right;"
                                                            OnClick="btn_reset_Click" />
                                                        <a href="#!" visible="false" runat="server" id="getHostelFee" target="_blank" class="gotohostelbtn">Get Hostel Fee</a>

                                                    </h2>
                                                    <div class="fnd-box-wpr-inr" style="padding: 0px 5px 5px 5px;">
                                                        <div class="month-checkbox">
                                                            <asp:Repeater ID="rd_months" runat="server" OnItemDataBound="rd_months_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_month" Text='<%#Eval("Month") %>' OnCheckedChanged="chk_month_CheckedChanged" AutoPostBack="true" runat="server" />
                                                                    <asp:Label ID="lbl_Month" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
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
                                                                    <th></th>
                                                                    <th>Month</th>
                                                                    <th>Fees Head</th>
                                                                    <th>Amount</th>
                                                                    <th>Discount</th>
                                                                    <th>Paid Prev.</th>
                                                                    <th>Payable</th>
                                                                </tr>
                                                                <asp:Repeater ID="rp_fee_details" runat="server" OnItemDataBound="rp_fee_details_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr id="row" runat="server">
                                                                            <td>
                                                                                <asp:CheckBox class="box" Checked="true" ID="chk_get_fee" runat="server" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_mnth" runat="server" Text='<%#Bind("months") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblcontent" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                                <asp:Label ID="lblcontent_id" Visible="false" runat="server" Text='<%#Bind("content_id") %>'></asp:Label>
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
                                                                    <th colspan="3">Total :
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

                                                <script type="text/javascript">   
                                                    $(document).ready(function () {
                                                        var chkPassport = $("#<%=chkChangeAmt.ClientID %>");
                                                        CheckChangeAdmAmt(chkPassport);
                                                        $("#<%=txt_adm_fee_paids.ClientID %>").focus(function () { $(this).select(); });

                                                        $('.boxadm').click(function (e) {
                                                            calculateSumAdm(15);
                                                        });
                                                    });

                                                    function CheckChangeAdmAmt(chkPassport) {
                                                        if ($(chkPassport).prop('checked') == true) {
                                                            var total = parseFloat($("#<%=txt_adm_fee_paids.ClientID %>").val());
                                                            $("#<%=lbl_collect_adm_ann_fee.ClientID %>").text(total);
                                                            $("#<%=txt_adm_ann_fee.ClientID %>").val(total);
                                                            $("#<%=txt_adm_ann_fee_no_cange.ClientID %>").val(total);
                                                            $("#<%=txt_adm_net_payble.ClientID %>").val(total);
                                                            var chkPassport = $("#<%=chk_collect_adm_ann_fee.ClientID %>");
                                                            ShowHideDiv(chkPassport);
                                                        }
                                                        else {
                                                            calculateSumAdm(15);
                                                        }
                                                    }


                                                    function calculateSumAdm(colidx) {
                                                        //alert("hh");
                                                        total = 0.0;
                                                        $("tr:has(:checkbox:checked) td:nth-child(" + colidx + ")").each(function () {
                                                            total += parseFloat($(this).text());
                                                        });

                                                        //alert(total);
                                                        $("#<%=txt_adm_fee_paids.ClientID %>").val(total);
                                                        $("#<%=lbl_collect_adm_ann_fee.ClientID %>").text(total);
                                                        $("#<%=txt_adm_ann_fee.ClientID %>").val(total);
                                                        $("#<%=txt_adm_ann_fee_no_cange.ClientID %>").val(total);
                                                        $("#<%=txt_adm_net_payble.ClientID %>").val(total);
                                                        var chkPassport = $("#<%=chk_collect_adm_ann_fee.ClientID %>");
                                                        ShowHideDiv(chkPassport);
                                                    }


                                                    $(function () {
                                                        $("#<%=txt_adm_fee_paids.ClientID %>").on('input', function () {
                                                            calculateaddmmsss();
                                                        });
                                                        function calculateaddmmsss() {
                                                           //. alert("h");
                                                            var ttrffff = parseFloat($("#<%=txt_adm_fee_paids.ClientID %>").val());
                                                            if (Number.isNaN(ttrffff) || ttrffff == "" || ttrffff === null) {
                                                                //alert("1");
                                                                $("#<%=txt_adm_fee_paids.ClientID %>").val("0"); 
                                                                ttrffff = 0;
                                                            }

                                                            var ttrffffNochange = parseFloat($("#<%=txt_adm_ann_fee_no_cange.ClientID %>").val());
                                                            if (ttrffffNochange < ttrffff) {
                                                                //alert("2");
                                                                $("#<%=txt_adm_fee_paids.ClientID %>").val(ttrffffNochange);
                                                                $("#<%=txt_adm_net_payble.ClientID %>").val(ttrffffNochange);
                                                                $("#<%=lbl_collect_adm_ann_fee.ClientID %>").text(ttrffffNochange);
                                                                $("#<%=txt_adm_ann_fee.ClientID %>").val(ttrffffNochange);
                                                            }
                                                            else {
                                                                //alert("3");
                                                                $("#<%=lbl_collect_adm_ann_fee.ClientID %>").text(ttrffff);
                                                                $("#<%=txt_adm_ann_fee.ClientID %>").val(ttrffff);
                                                                $("#<%=txt_adm_net_payble.ClientID %>").val(ttrffff);
                                                            }

                                                            var chkPassport = $("#<%=chk_collect_adm_ann_fee.ClientID %>");
                                                            ShowHideDiv(chkPassport);
                                                        }
                                                    });


                                                </script>



                                                <script type="text/javascript">   
                                                    $(document).ready(function () {
                                                    //var chkPassport = $("#<%=chk_collect_adm_ann_fee.ClientID %>");
                                                        calculateSum(7);

                                                        $('.box').click(function (e) {
                                                            calculateSum(7);
                                                        });
                                                    });






                                                    function calculateSum(colidx) {
                                                        total = 0.0;
                                                        $("tr:has(:checkbox:checked) td:nth-child(" + colidx + ")").each(function () {
                                                            total += parseFloat($(this).text());
                                                        });

                                                        //alert(total);

                                                        ///=============================================  
                                                        var paidsamts = parseFloat($("#<%=txt_paid_amount.ClientID %>").val());
                                                        var lateFines = parseFloat($("#<%=txtfineamount.ClientID %>").val());
                                                        //alert(paidsamts);
                                                        if (Number.isNaN(paidsamts) || paidsamts == "" || paidsamts === null) {
                                                            //alert("ffs");
                                                            $("#<%=txt_paid_amount.ClientID %>").val("0");
                                                        }

                                                        if (total > 0) {
                                                            $("#<%=txt_monthlyFee.ClientID %>").val(total + lateFines);
                                                        }
                                                        else {
                                                            $("#<%=txt_monthlyFee.ClientID %>").val(total);
                                                        }

                                                        var chkPassport = $("#<%=chk_collect_adm_ann_fee.ClientID %>");
                                                        if ($(chkPassport).prop('checked') == true) {
                                                            var ttl_adm_amt = parseFloat($("#<%=txt_adm_ann_fee.ClientID %>").val());
                                                            var ttl_mnthlyamt = parseFloat($("#<%=txt_monthlyFee.ClientID %>").val());
                                                            var totalFee = (ttl_adm_amt + ttl_mnthlyamt);
                                                            $("#<%=txttotalbill.ClientID %>").val(totalFee.toFixed());
                                                        }
                                                        else {
                                                            var ttl_mnthlyamt = parseFloat($("#<%=txt_monthlyFee.ClientID %>").val());
                                                            $("#<%=txttotalbill.ClientID %>").val(ttl_mnthlyamt.toFixed());

                                                            var paidAmt = parseFloat($("#<%=txt_paid_amount.ClientID %>").val());
                                                            var ttl_duess = parseFloat($("#<%=txttotalbill.ClientID %>").val());

                                                            if (paidAmt > ttl_duess) {
                                                                $("#<%=txt_paid_amount.ClientID %>").val(ttl_duess.toFixed());
                                                            }
                                                        }


                                                        var mnthly_paid_amt = parseFloat($("#<%=txt_paid_amount.ClientID %>").val());
                                                        var mnthly_ttl_bill_amt = parseFloat($("#<%=txttotalbill.ClientID %>").val());
                                                        var dues_amt = 0;
                                                        var final_amt = 0;



                                                        //======================
                                                        var paid_amt_of_mnth = $("#<%=txt_paid_amount.ClientID %>").val();
                                                        if (paid_amt_of_mnth.length == 0) {
                                                            $("#<%=txt_paid_amount.ClientID %>").val('0');
                                                        }
                                                        if (paid_amt_of_mnth.length == 2) {
                                                            var frstChar = paid_amt_of_mnth.substring(0, 1)
                                                            while (paid_amt_of_mnth.charAt(0) === '0') {
                                                                paid_amt_of_mnth = paid_amt_of_mnth.substring(1);
                                                            }
                                                            $("#<%=txt_paid_amount.ClientID %>").val(paid_amt_of_mnth);
                                                        }

                                                        //MonthlY
                                                        var mnthly_dues_amt = 0;
                                                        if (mnthly_ttl_bill_amt < mnthly_paid_amt) {
                                                            $("#<%=txt_paid_amount.ClientID %>").val('');
                                                            $("#<%=txt_paid_amount.ClientID %>").val(mnthly_ttl_bill_amt);
                                                            $("#<%=txt_total_dues.ClientID %>").val('0.00');
                                                        }
                                                        else {
                                                            mnthly_dues_amt = (mnthly_ttl_bill_amt - mnthly_paid_amt);
                                                            $("#<%=txt_total_dues.ClientID %>").val(mnthly_dues_amt.toFixed(2));
                                                        }
                                                    }
                                                </script>


                                                <script type="text/javascript">
                                                    $(document).ready(function () {
                                                        var chkPassport = $("#<%=chkChangeAmt.ClientID %>");
                                                        ChangeAdmAmt(chkPassport);
                                                    });

                                                    function ChangeAdmAmt(chkPassport) {
                                                        if ($(chkPassport).prop('checked') == true) {
                                                            $("#admamtsDV").removeClass("noclick ");
                                                            $("#<%=txt_adm_fee_paids.ClientID %>").removeClass("noclick freeze-txtbx");
                                                            $("#<%=txt_adm_fee_paids.ClientID %>").focus();
                                                        }
                                                        else {
                                                            $("#admamtsDV").addClass("noclick");
                                                            $("#<%=txt_adm_fee_paids.ClientID %>").addClass("noclick freeze-txtbx");

                                                            calculateSumAdm(15);
                                                        }
                                                    }



                                                    $(document).ready(function () {
                                                        // CHECK-UNCHECK ALL CHECKBOXES IN THE REPEATER 
                                                        // WHEN USER CLICKS THE HEADER CHECKBOX.
                                                        $('table [id*=chkAll]').click(function () {
                                                            if ($(this).is(':checked'))
                                                                $('table [id*=chkRowData]').prop('checked', true)
                                                            else
                                                                $('table [id*=chkRowData]').prop('checked', false)
                                                        });

                                                        // NOW CHECK THE HEADER CHECKBOX, IF ALL THE ROW CHECKBOXES ARE CHECKED.
                                                        $('table [id*=chkRowData]').click(function () {

                                                            var total_rows = $('table [id*=chkRowData]').length;
                                                            var checked_Rows = $('table [id*=chkRowData]:checked').length;

                                                            if (checked_Rows == total_rows)
                                                                $('table [id*=chkAll]').prop('checked', true);
                                                            else
                                                                $('table [id*=chkAll]').prop('checked', false);
                                                        });
                                                    });
                                                </script>


                                                <div class="fnd-box-wpr" style="margin: 5px 0px 0px 0px; position: relative">
                                                    <a href="#!" data-toggle="modal" data-target="#MdlRevisedPayment" style="display: none" class="revisePaySbtn" runat="server" target="_blank" id="revisedPayment" visible="false">Revise Payment</a>
                                                    <a href="#!" data-toggle="modal" data-target="#ModalDiscount" class="discDetailSbtn" style="display: none" runat="server" target="_blank" id="discDTbtn" visible="false">Discount Details</a>
                                                    <div class="print-btn-sec" style="float: right">
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                                                    </div>
                                                    <div id="tblPrintIQ" runat="server">

                                                        <div class="print-dv-bx-wpr hide1">
                                                            <div class="head-printdv" style="border-bottom: 1px solid #ddd; margin: 2px 0px 0px 0px; float: left; width: 100%;">
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
                                                                        <%-- <span style="font-size: 14px; font-weight: bold;">Payment History Details  </span>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <h2 class="fnd-box-row-wpr-h" style="border-top: 1px solid #ddd;">Student Ledger Details</h2>


                                                        <div class="fnd-box-wpr-inr">
                                                            <table class="table table-bordered table-striped" style="width: 100%">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Receipt No</th>
                                                                        <th>Receipt Date</th>
                                                                        <th>Payment Type</th>
                                                                        <th>Received Mode</th>
                                                                        <th>Total Paid</th>
                                                                        <th style="width: 53px;"></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rl_ledger" runat="server" OnItemDataBound="rl_ledger_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Receipt_No" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Receipt_Date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Type") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_mode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>
                                                                                </td>

                                                                                <td class="txtalignrights">
                                                                                    <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Bind("Description") %>' Visible="false" Style="word-break: break-all"></asp:Label>
                                                                                    <asp:Label ID="lbl_amount" runat="server" Font-Bold="true" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>
                                                                                    <asp:Label ID="lbl_fee_type" Visible="false" runat="server" Text='<%#Bind("FeeType") %>'></asp:Label>

                                                                                    <asp:Label ID="lbl_admission_no" Visible="false" runat="server" Text='<%#Bind("Addmission_no") %>'></asp:Label>
                                                                                    <asp:Label ID="lbl_class_id" Visible="false" runat="server" Text='<%#Bind("Class_id") %>'></asp:Label>
                                                                                    <asp:Label ID="lbl_session_id" Visible="false" runat="server" Text='<%#Bind("Session_id") %>'></asp:Label>
                                                                                    <asp:Label ID="lbl_entry_id" Visible="false" runat="server" Text='<%#Bind("Entry_id") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnk_edit" runat="server" OnClick="lnk_edit_Click"><span class="material-symbols-outlined" style="font-size: 18px;">edit_square</span></asp:LinkButton>
                                                                                    <a href="#!" runat="server" id="printLnk" target="_blank"><i class="bx bx-printer" style="font-size: 18px;"></i></a>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>

                                                                    <tr>
                                                                        <td colspan="5" class="txtalign-right txtbolds" style="text-align: right; font-weight: bold">Total : </td>
                                                                        <td class="txtbolds txtalignrights" colspan="2">
                                                                            <asp:Label ID="lbl_ttl_ledger_amt" runat="server" Text="0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-xl-6" style="padding-left: 5px">
                                                <div class="fnd-box-wpr" style="margin: 5px 0px 0px 0px;">
                                                    <h2 class="fnd-box-row-wpr-h" style="background: #f9f9f9;">Payment</h2>
                                                    <div class="fnd-box-wpr-inr">
                                                        <div class="fnd-box-row-wpr" style="background: #FFBA5F;">
                                                            <div class="col-md-5" style="background: #FFBA5F; padding: 3px 4px 4px 6px; float: left">
                                                                <asp:CheckBox ID="chk_latefineapplay" runat="server" Text="Is late fine applied" Checked="true" AutoPostBack="true" OnCheckedChanged="chk_latefineapplay_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-7" style="background: #FFBA5F; padding: 3px 4px 4px 6px; float: left">
                                                                <asp:CheckBox ID="chk_collect_adm_ann_fee" onclick="ShowHideDiv(this)" runat="server" />
                                                                <asp:Label ID="lbl_collect_adm_ann_fee" runat="server"></asp:Label>
                                                            </div>
                                                        </div>


                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <asp:CheckBox ID="chk_delete_slip" runat="server" Checked="false" Text="Manual Bill Entry" AutoPostBack="true" OnCheckedChanged="chk_delete_slip_CheckedChanged" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="fnd-box-row-wpr" id="oldslip_no" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <label for="validationCustom01" class="form-label-fnds">Slip No.</label>
                                                                </div>
                                                                <div class="col-md-6 padd-lft0">
                                                                    <asp:TextBox ID="txt_old_slip_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Total Amount</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0">
                                                                    <asp:TextBox ID="txttotal" runat="server" class="form-control find-dv-txtbx" ReadOnly="true"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Paid Previously</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0">
                                                                    <asp:TextBox ID="txt_paid_prev" runat="server" class="form-control find-dv-txtbx" ReadOnly="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Discount</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0">
                                                                    <asp:TextBox ID="txt_discount" runat="server" class="form-control find-dv-txtbx" ReadOnly="true"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Late Fine</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0">
                                                                    <asp:TextBox ID="txtfineamount" runat="server" class="form-control find-dv-txtbx" Text="0" ReadOnly="true" AutoPostBack="true" OnTextChanged="txtfineamount_TextChanged"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="fnd-box-row-wpr noclick">
                                                            <asp:TextBox ID="txt_adm_total" runat="server" Style="display: none"></asp:TextBox>
                                                            <asp:TextBox ID="txt_adm_disc_amt" runat="server" Style="display: none"></asp:TextBox>
                                                            <asp:TextBox ID="txt_adm_prev_paid" runat="server" Style="display: none"></asp:TextBox>
                                                            <asp:TextBox ID="txt_adm_net_payble" runat="server" Style="display: none"></asp:TextBox>

                                                            <asp:TextBox ID="txt_adm_ann_fee" runat="server" Style="display: none"></asp:TextBox>
                                                            <asp:TextBox ID="txt_adm_ann_fee_no_cange" runat="server" Style="display: none"></asp:TextBox>


                                                            <asp:TextBox ID="txt_month_total" runat="server" Style="display: none"></asp:TextBox>
                                                            <asp:TextBox ID="txt_month_disc_amt" runat="server" Style="display: none"></asp:TextBox>
                                                            <asp:TextBox ID="txt_month_prev_paid" runat="server" Style="display: none"></asp:TextBox>
                                                            <asp:TextBox ID="txt_month_net_payble" runat="server" Style="display: none"></asp:TextBox>

                                                            <asp:TextBox ID="txt_monthlyFee" runat="server" Style="display: none"></asp:TextBox>
                                                            <asp:TextBox ID="txt_is_added" runat="server" Style="display: none"></asp:TextBox>
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Other Fee</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0 noclick">
                                                                    <asp:TextBox ID="txt_other_fee" runat="server" Style="background-color: #e9ecef;" class="form-control find-dv-txtbx noclick" Text="0" AutoPostBack="true" OnTextChanged="txt_other_fee_TextChanged"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Payable Bill</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0 noclick">
                                                                    <asp:TextBox ID="txttotalbill" runat="server" class="form-control find-dv-txtbx noclick"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Paid Amount</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0">
                                                                    <asp:TextBox ID="txt_paid_amount" Style="background: #eaff98;" onkeypress="return isNumberKey(event)" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Total Dues</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0 noclick">
                                                                    <asp:TextBox ID="txt_total_dues" runat="server" class="form-control find-dv-txtbx noclick txtbxdisabled"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <%--<div id="dvPassport" style="display: none; background: rgb(247 149 230); float: left; width: 100%; padding: 2px; border-radius: 2px; border: 1px solid rgb(221 109 201);">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" runat="server" id="feeTypelbl" class="form-label-fnds">Admission Fee</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0">
                                                                    <asp:TextBox ID="txt_payble_adm_ann_fee" Style="display: none" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                    <asp:TextBox ID="txt_paid_adm_ann_fee_payment" Style="background: #eaff98;" onkeypress="return isNumberKey(event)" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Total Dues</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0 noclick">
                                                                    <asp:TextBox ID="txt_dues_adm_ann_fee" runat="server" class="form-control find-dv-txtbx noclick txtbxdisabled"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Final Amount</label>
                                                                </div>
                                                                <div class="col-md-9 padd-lft0 noclick">
                                                                    <asp:TextBox ID="txt_final_amt" runat="server" class="form-control find-dv-txtbx noclick txtbxdisabled"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>


                                                        <div class="fnd-box-row-wpr" id="payDatedv">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Payment Date</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0" runat="server" id="paydateDVS">
                                                                    <asp:Label ID="lbl_previous_year_dues" runat="server" Visible="false"></asp:Label>
                                                                    <asp:TextBox ID="txt_payment_date" runat="server" class="form-control find-dv-txtbx" OnTextChanged="txt_payment_date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    <script>
                                                                        $(function () {
                                                                            $("#<%=txt_payment_date.ClientID %>").datepicker({
                                                                                dateFormat: "dd/mm/yy",
                                                                                changeMonth: true,
                                                                                changeYear: true,
                                                                                yearRange: "2021:2030",
                                                                                //maxDate: '0',
                                                                            }).attr("readonly", "true");
                                                                        });




                                                                        function ShowHideDiv(chkPassport) {
                                                                            //var dvPassport = document.getElementById("dvPassport");
                                                                            //dvPassport.style.display = $(chkPassport).prop('checked') ? "block" : "none";


                                                                            if ($(chkPassport).prop('checked') == true) {
                                                                                <%--var ttl_mnthlyamt = 0;
                                                                                var ttl_adm_amt = parseFloat($("#<%=txt_adm_ann_fee.ClientID %>").val());
                                                                                var ttl_mnthlyamt = parseFloat($("#<%=txt_monthlyFee.ClientID %>").val());
                                                                                var totalFee = (ttl_adm_amt + ttl_mnthlyamt);
                                                                                $("#<%=txttotalbill.ClientID %>").val(totalFee.toFixed());--%>


                                                                                //Total AMount
                                                                                var ttl_m_amount = 0; var ttl_an_amount = 0; var ttl_m_an_amount = 0;
                                                                                var ttl_m_amount = parseFloat($("#<%=txt_month_total.ClientID %>").val());
                                                                                var ttl_an_amount = parseFloat($("#<%=txt_adm_total.ClientID %>").val());
                                                                                ttl_m_an_amount = (ttl_m_amount + ttl_an_amount);
                                                                                $("#<%=txttotal.ClientID %>").val(ttl_m_an_amount.toFixed());


                                                                                //Total Prev Paid Amount
                                                                                var ttl_m_prev_paid_amount = 0; var ttl_an_prev_paid_amount = 0; var ttl_m_an_prev_paid_amount = 0;
                                                                                var ttl_m_prev_paid_amount = parseFloat($("#<%=txt_month_prev_paid.ClientID %>").val());
                                                                                var ttl_an_prev_paid_amount = parseFloat($("#<%=txt_adm_prev_paid.ClientID %>").val());
                                                                                ttl_m_an_prev_paid_amount = (ttl_m_prev_paid_amount + ttl_an_prev_paid_amount);
                                                                                $("#<%=txt_paid_prev.ClientID %>").val(ttl_m_an_prev_paid_amount.toFixed());


                                                                                //Total Discount AMount
                                                                                var ttl_m_disc_amount = 0; var ttl_an_disc_amount = 0; var ttl_m_an_disc_amount = 0;
                                                                                var ttl_m_disc_amount = parseFloat($("#<%=txt_month_disc_amt.ClientID %>").val());
                                                                                var ttl_an_disc_amount = parseFloat($("#<%=txt_adm_disc_amt.ClientID %>").val());
                                                                                ttl_m_an_disc_amount = (ttl_m_disc_amount + ttl_an_disc_amount);
                                                                                $("#<%=txt_discount.ClientID %>").val(ttl_m_an_disc_amount.toFixed());



                                                                                //Total Net Payble Amount
                                                                                var ttl_m_net_apyable_amount = 0; var ttl_an_net_apyable_amount = 0; var ttl_m_an_net_apyable_amount = 0;
                                                                                var ttl_m_net_apyable_amount = parseFloat($("#<%=txt_month_net_payble.ClientID %>").val());
                                                                                var ttl_an_net_apyable_amount = parseFloat($("#<%=txt_adm_net_payble.ClientID %>").val());
                                                                                ttl_m_an_net_apyable_amount = (ttl_m_net_apyable_amount + ttl_an_net_apyable_amount);
                                                                                $("#<%=txttotalbill.ClientID %>").val(ttl_m_an_net_apyable_amount.toFixed());
                                                                            }
                                                                            else
                                                                            {
                                                                                //Total AMount
                                                                                var ttl_m_amount = 0;
                                                                                var ttl_m_amount = parseFloat($("#<%=txt_month_total.ClientID %>").val());  
                                                                                $("#<%=txttotal.ClientID %>").val(ttl_m_amount.toFixed());


                                                                                //Total Prev Paid Amount
                                                                                var ttl_m_prev_paid_amount = 0; 
                                                                                var ttl_m_prev_paid_amount = parseFloat($("#<%=txt_month_prev_paid.ClientID %>").val());  
                                                                                $("#<%=txt_paid_prev.ClientID %>").val(ttl_m_prev_paid_amount.toFixed());


                                                                                //Total Discount AMount
                                                                                var ttl_m_disc_amount = 0;  
                                                                                var ttl_m_disc_amount = parseFloat($("#<%=txt_month_disc_amt.ClientID %>").val());  
                                                                                $("#<%=txt_discount.ClientID %>").val(ttl_m_disc_amount.toFixed());



                                                                                //Total Net Payble Amount
                                                                                var ttl_m_net_apyable_amount = 0; 
                                                                                var ttl_m_net_apyable_amount = parseFloat($("#<%=txt_month_net_payble.ClientID %>").val());  
                                                                                $("#<%=txttotalbill.ClientID %>").val(ttl_m_net_apyable_amount.toFixed());
                                                                                 


                                                                                var ttl_mnthlyamt = parseFloat($("#<%=txt_monthlyFee.ClientID %>").val());
                                                                                $("#<%=txttotalbill.ClientID %>").val(ttl_mnthlyamt.toFixed());

                                                                                var paidAmt = parseFloat($("#<%=txt_paid_amount.ClientID %>").val());
                                                                                var ttl_duess = parseFloat($("#<%=txttotalbill.ClientID %>").val());

                                                                                if (paidAmt > ttl_duess) {
                                                                                    $("#<%=txt_paid_amount.ClientID %>").val(ttl_duess.toFixed());
                                                                                }
                                                                            }


                                                                            //========================
                                                                            var mnthly_paid_amt = parseFloat($("#<%=txt_paid_amount.ClientID %>").val());
                                                                            var mnthly_ttl_bill_amt = parseFloat($("#<%=txttotalbill.ClientID %>").val());
                                                                            var dues_amt = 0;
                                                                            var final_amt = 0;



                                                                            //======================
                                                                            var paid_amt_of_mnth = $("#<%=txt_paid_amount.ClientID %>").val();
                                                                            if (paid_amt_of_mnth.length == 0) {
                                                                                $("#<%=txt_paid_amount.ClientID %>").val('0');
                                                                            }
                                                                            if (paid_amt_of_mnth.length == 2) {
                                                                                var frstChar = paid_amt_of_mnth.substring(0, 1)
                                                                                while (paid_amt_of_mnth.charAt(0) === '0') {
                                                                                    paid_amt_of_mnth = paid_amt_of_mnth.substring(1);
                                                                                }
                                                                                $("#<%=txt_paid_amount.ClientID %>").val(paid_amt_of_mnth);
                                                                            }

                                                                            //MonthlY
                                                                            var mnthly_dues_amt = 0;
                                                                            if (mnthly_ttl_bill_amt < mnthly_paid_amt) {
                                                                                $("#<%=txt_paid_amount.ClientID %>").val('');
                                                                                $("#<%=txt_paid_amount.ClientID %>").val(mnthly_ttl_bill_amt);
                                                                                $("#<%=txt_total_dues.ClientID %>").val('0.00');
                                                                            }
                                                                            else {
                                                                                mnthly_dues_amt = (mnthly_ttl_bill_amt - mnthly_paid_amt);
                                                                                $("#<%=txt_total_dues.ClientID %>").val(mnthly_dues_amt.toFixed(2));
                                                                            }
                                                                        }


                                                                        $(document).ready(function () {
                                                                            $("#<%=txt_paid_amount.ClientID %>").focus(function () { $(this).select(); });
                                                                            var chkPassport = $("#<%=chk_collect_adm_ann_fee.ClientID %>");
                                                                            console.log(chkPassport.checked);
                                                                            console.log(chkPassport.prop('checked'));
                                                                            ShowHideDiv(chkPassport);
                                                                        });



                                                                        //======================
                                                                        $(function () {
                                                                            $("#<%=txt_paid_amount.ClientID %>").on('input', function () {
                                                                                calculate();
                                                                            });
                                                                            function calculate() {

                                                                                var mnthly_paid_amt = parseFloat($("#<%=txt_paid_amount.ClientID %>").val());
                                                                                var mnthly_ttl_bill_amt = parseFloat($("#<%=txttotalbill.ClientID %>").val());
                                                                                var dues_amt = 0;
                                                                                var final_amt = 0;



                                                                                //======================
                                                                                var paid_amt_of_mnth = $("#<%=txt_paid_amount.ClientID %>").val();
                                                                                if (paid_amt_of_mnth.length == 0) {
                                                                                    $("#<%=txt_paid_amount.ClientID %>").val('0');
                                                                                }
                                                                                if (paid_amt_of_mnth.length == 2) {
                                                                                    var frstChar = paid_amt_of_mnth.substring(0, 1)
                                                                                    while (paid_amt_of_mnth.charAt(0) === '0') {
                                                                                        paid_amt_of_mnth = paid_amt_of_mnth.substring(1);
                                                                                    }
                                                                                    $("#<%=txt_paid_amount.ClientID %>").val(paid_amt_of_mnth);
                                                                                }

                                                                                //MonthlY
                                                                                var mnthly_dues_amt = 0;
                                                                                if (mnthly_ttl_bill_amt < mnthly_paid_amt) {
                                                                                    $("#<%=txt_paid_amount.ClientID %>").val('');
                                                                                    $("#<%=txt_paid_amount.ClientID %>").val(mnthly_ttl_bill_amt);
                                                                                    $("#<%=txt_total_dues.ClientID %>").val('0.00');
                                                                                }
                                                                                else {
                                                                                    mnthly_dues_amt = (mnthly_ttl_bill_amt - mnthly_paid_amt);
                                                                                    $("#<%=txt_total_dues.ClientID %>").val(mnthly_dues_amt.toFixed(2));
                                                                                }
                                                                            }
                                                                        });
                                                                    </script>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Payment Mode</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0">
                                                                    <asp:DropDownList ID="ddl_paymentmode" runat="server" class="form-select find-dv-txtbx">
                                                                        <asp:ListItem>Cash</asp:ListItem>
                                                                        <asp:ListItem>Deposited In Bank</asp:ListItem>
                                                                        <asp:ListItem>UPI</asp:ListItem>
                                                                        <asp:ListItem>UPI_Cash</asp:ListItem>
                                                                        <asp:ListItem>Pos</asp:ListItem>
                                                                        <asp:ListItem>Pos_Cash</asp:ListItem>
                                                                        <asp:ListItem>Netbanking</asp:ListItem>
                                                                        <asp:ListItem>Sbdebit</asp:ListItem>
                                                                        <asp:ListItem>Cheque</asp:ListItem>
                                                                        <asp:ListItem>NEFT</asp:ListItem>
                                                                        <asp:ListItem>Debitcard</asp:ListItem>
                                                                        <asp:ListItem>Creditcard</asp:ListItem>
                                                                        <asp:ListItem>Otherdcard</asp:ListItem>
                                                                        <asp:ListItem>Demand Draft(DD)</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="fnd-box-row-wpr" id="bank_dt">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Bank Name</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0">
                                                                    <asp:DropDownList ID="ddl_bank" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Date</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0">
                                                                    <asp:TextBox ID="txt_bank_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                    <script>
                                                                        $(function () {
                                                                            $("#<%=txt_bank_date.ClientID %>").datepicker({
                                                                                dateFormat: "dd/mm/yy",
                                                                                changeMonth: true,
                                                                                changeYear: true,
                                                                                yearRange: "2021:2030",
                                                                                maxDate: '0',
                                                                            }).attr("readonly", "true");
                                                                        });
                                                                    </script>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="fnd-box-row-wpr" id="pnl_mode_t_nS">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <asp:Label ID="lbl_mode_trns_no" runat="server" class="form-label-fnds" Text="Transaction No."></asp:Label>

                                                                </div>
                                                                <div class="col-md-9 padd-lft0">
                                                                    <asp:TextBox ID="txt_trans_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <script type="text/javascript">
                                                            $(document).ready(function () {
                                                                on_payment_mode_selection();
                                                                $("#<%=ddl_paymentmode.ClientID%>").on('change', function () {
                                                                    on_payment_mode_selection();
                                                                })
                                                            });

                                                            function on_payment_mode_selection() {
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Cash") {
                                                                    $("#pnl_mode_t_nS").hide();
                                                                    $("#bank_dt").hide();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Paid to NBCS") {
                                                                    $("#pnl_mode_t_nS").hide();
                                                                    $("#bank_dt").hide();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Netbanking") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Deposited In Bank") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Sbdebit") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Cheque") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Cheque No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "NEFT") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("UTR No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Debitcard") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Creditcard") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Otherdcard") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "UPI") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Demand Draft(DD)") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Pos") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }
                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Other APP") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }


                                                                if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "UPI_Cash" || $('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Pos_Cash") {
                                                                    $("#pnl_mode_t_nS").show();
                                                                    $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                                                                    $("#bank_dt").show();
                                                                }

                                                            }
                                                        </script>



                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Remark</label>
                                                                </div>
                                                                <div class="col-md-9 padd-lft0">
                                                                    <asp:TextBox ID="txt_description" runat="server" TextMode="MultiLine" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>




                                                        <div class="fnd-box-row-wpr" style="border: 1px solid #ddd; padding: 3px 7px 6px; border-radius: 3px;"
                                                            runat="server" id="AssignRollDV" visible="false">
                                                            <div style="margin: 0px 5px 15px 0px; float: left; width: 100%">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <asp:CheckBox ID="chk_is_roll_sec_apply" Checked="true" runat="server" Text="Is roll & section update" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Roll No.</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0">
                                                                    <asp:TextBox ID="txt_assign_roll_no" runat="server" class="form-control find-dv-txtbx noclick txtbxdisabled"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label-fnds">Section</label>
                                                                </div>
                                                                <div class="col-md-3 padd-lft0 noclick">
                                                                    <asp:TextBox ID="txt_assign_section" runat="server" class="form-control find-dv-txtbx noclick txtbxdisabled"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>



                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-md-3">
                                                                </div>
                                                                <div class="col-md-6 padd-lft0">
                                                                    <div style="overflow: hidden; height: 1px;">
                                                                        <asp:Button ID="btn_save_payment" runat="server" Text="Pay" OnClick="btn_save_payment_Click" CssClass="btn btn-primary" Style="width: 1px; height: 1px" />
                                                                    </div>

                                                                    <a onclick="save_data()" class="button-37">Pay</a>
                                                                    <script type="text/javascript">
                                                                        function Confirm() {

                                                                            var confirm_value
                                                                            var isSubmitted = false;
                                                                            confirm_value = document.createElement("INPUT");
                                                                            confirm_value.type = "hidden";
                                                                            confirm_value.name = "confirm_value";

                                                                            if (confirm("Do you want to print bill?")) {
                                                                                confirm_value.value = "Yes";
                                                                            }
                                                                            else {
                                                                                confirm_value.value = "No";
                                                                            }
                                                                            document.forms[0].appendChild(confirm_value);
                                                                        }


                                                                        function save_data() {
                                                                            var valsubmit = $('#<%=btn_save_payment.ClientID %>').val();
                                                                            if (valsubmit == "Pay") {
                                                                                $('#<%=btn_save_payment.ClientID %>').val('Submitting.. Please Wait..');
                                                                                Confirm();
                                                                                document.getElementById("<%=btn_save_payment.ClientID %>").click();
                                                                            }
                                                                            else {
                                                                                alert("Already submitted")
                                                                            }
                                                                        }
                                                                    </script>
                                                                </div>
                                                            </div>
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
        </div>
        <!--end row-->



        <div class="modal left fade model-fltr" id="MdlAdmAnnFeeInfo" role="dialog" style="z-index: 9999; background: rgb(0 0 0 / 17%);">
            <div class="modal-dialog model-dialog-fltr">
                <div class="modal-content menusidec">
                    <div class="modal-body fltr-mdl-body">
                        <h3 class="fltr-mdl-title-h" runat="server" id="admfeeheadtittle"></h3>
                        <div class="adm-box-wprs1">
                            <table style="width: 100%;" class="table table-hover table-bordered">
                                <tr>
                                    <th class="hidden"></th>
                                    <th class="hidden"></th>
                                    <th class="hidden"></th>
                                    <th class="hidden"></th>
                                    <th class="hidden"></th>
                                    <th class="hidden"></th>
                                    <th class="hidden"></th>
                                    <th class="hidden"></th>
                                    <th class="hidden"></th>
                                    <th style="text-align: center">
                                        <asp:CheckBox class="boxadm" ID="chkAll" runat="server" Checked="true" /></th>
                                    <th style="white-space: nowrap; word-break: keep-all;">Fees Head</th>
                                    <th>Amount</th>
                                    <th>Discount</th>
                                    <th style="white-space: nowrap; word-break: keep-all;">Paid Prev.</th>
                                    <th>Payable</th>
                                </tr>
                                <asp:Repeater ID="grd_fee_adm_fee" runat="server" OnItemDataBound="grd_fee_adm_fee_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="row" runat="server">
                                            <td class="hidden"></td>
                                            <td class="hidden"></td>
                                            <td class="hidden"></td>
                                            <td class="hidden"></td>
                                            <td class="hidden"></td>
                                            <td class="hidden"></td>
                                            <td class="hidden">
                                                <asp:Label ID="Label11" runat="server" Text='0'></asp:Label>
                                            </td>
                                            <td class="hidden"></td>
                                            <td class="hidden"></td>
                                            <td style="text-align: center">
                                                <asp:CheckBox class="boxadm" Checked="true" ID="chkRowData" runat="server" />
                                            </td>

                                            <td>
                                                <asp:Label ID="lblcontent" runat="server" Text='<%#Bind("feetype") %>'></asp:Label>
                                                <asp:Label ID="lblcontent_id" Visible="false" runat="server" Text='<%#Bind("content_id") %>'></asp:Label>
                                            </td>
                                            <td class="txtalignrights">
                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("payable","{0:n}") %>'></asp:Label>
                                            </td>
                                            <td class="txtalignrights">
                                                <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("disc_amount") %>'></asp:Label>
                                            </td>
                                            <td class="txtalignrights">
                                                <asp:Label ID="lbl_pre_paid" runat="server" Text='<%#Bind("paid") %>'></asp:Label>
                                            </td>
                                            <td class="txtalignrights">
                                                <asp:Label ID="lbl_tot_pble" runat="server" Text='<%#Bind("net_payable") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <tr>
                                    <th colspan="2">Total :
                                    </th>
                                    <th class="txtalignrights">
                                        <asp:Label ID="lbl_ttl_adm_payble" runat="server" Text=""></asp:Label></th>
                                    <th class="txtalignrights">
                                        <asp:Label ID="lbl_ttl_adm_disc" runat="server" Text=""></asp:Label></th>
                                    <th class="txtalignrights">
                                        <asp:Label ID="lbl_ttl_prev_adm_paid" runat="server" Text=""></asp:Label></th>
                                    <th class="txtalignrights">
                                        <asp:Label ID="lbl_adm_net_payble" runat="server" Text=""></asp:Label></th>
                                </tr>

                                <tr>
                                    <th colspan="2">
                                        <asp:Label ID="lbl_amount_to_be_paid" runat="server" Style="font-size: 14px;"></asp:Label>
                                    </th>
                                    <th colspan="2" class="isChangeAdmAmt" style="white-space: nowrap; word-break: keep-all;">
                                        <asp:CheckBox ID="chkChangeAmt" onclick="ChangeAdmAmt(this)" runat="server" Text="Change Amount" />
                                    </th>
                                    <th colspan="2" id="admamtsDV">
                                        <asp:TextBox ID="txt_adm_fee_paids" runat="server" class="form-control"></asp:TextBox>
                                    </th>
                                </tr>
                            </table>



                        </div>
                    </div>
                </div>
            </div>
        </div>




        <asp:HiddenField ID="hd_paybaleamount" runat="server" />
        <asp:HiddenField ID="hd_adjustamount" runat="server" />
        <asp:HiddenField ID="hd_total_discount" runat="server" />
        <asp:HiddenField ID="hd_totalamount" runat="server" />
        <div id="MdlAdmissionFeePayment" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" style="max-width: 1050px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 3px 17px;">
                        <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Admission Fee Collection</h5>
                        <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                            class="btn btn-primary find-dv-btn">Close</a>
                    </div>
                    <div class="modal-body">
                        <div class="p-4 border rounded" style="float: left; width: 100%; padding: 0px !important;">
                            <div class="disc-tbl-wprs">
                                <div class="popup-adm-std-wpr">
                                    <div class="popup-adm-std-info">
                                        <div class="row">
                                            <div class="col-xl-6 padd-rght-5">
                                                <div class="row">
                                                    <div class="col-xl-4 padd-rght-5">
                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                    </div>
                                                    <div class="col-xl-8 padd-lft-5">
                                                        <asp:Label ID="lbl_studentname" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-6 padd-rght-5">
                                                <div class="row">
                                                    <div class="col-xl-4 padd-rght-5">
                                                        <label for="validationCustom01" class="stdnt-info-fnds">Admission No. : </label>
                                                    </div>
                                                    <div class="col-xl-8 padd-lft-5">
                                                        <asp:Label ID="lbl_admissionno" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-xl-6 padd-rght-5">
                                                <div class="row">
                                                    <div class="col-xl-4 padd-rght-5">
                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                    </div>
                                                    <div class="col-xl-8 padd-lft-5">
                                                        <asp:Label ID="lbl_class" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-6 padd-rght-5">
                                                <div class="row">
                                                    <div class="col-xl-4 padd-rght-5">
                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Type : </label>
                                                    </div>
                                                    <div class="col-xl-8 padd-lft-5">
                                                        <asp:Label ID="lbl_student_type" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>




                                            <div class="col-xl-6 padd-rght-5">
                                                <div class="row">
                                                    <div class="col-xl-4 padd-rght-5">
                                                        <label for="validationCustom01" class="stdnt-info-fnds">Session : </label>
                                                    </div>
                                                    <div class="col-xl-8 padd-lft-5">
                                                        <asp:Label ID="lbl_session" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-6 padd-rght-5">
                                                <div class="row">
                                                    <div class="col-xl-4 padd-rght-5">
                                                        <label for="validationCustom01" class="stdnt-info-fnds">Section : </label>
                                                    </div>
                                                    <div class="col-xl-8 padd-lft-5">
                                                        <asp:Label ID="lbl_section_adm" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="popup-adm-std-imgs">
                                        <asp:Image ID="Image2" runat="server" />
                                    </div>
                                </div>



                                <div class="popup-adm-fees-wpr">
                                    <div class="row">
                                        <div class="col-xl-6" style="padding-right: 0px">
                                            <div class="adm-box-wprs1">
                                                <asp:Label ID="Label3" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Admission Fees Deatils"></asp:Label>



                                                <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; display: none">
                                                    <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                                        <tr>
                                                            <th colspan="3">Adjust Amount from Money Receipt</th>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px">Enter Unique Receipt No.
                                                            </td>
                                                            <td style="padding: 5px">
                                                                <asp:TextBox ID="txt_Uniqueno" Font-Bold="true" runat="server" Style="width: 100%;"></asp:TextBox>
                                                            </td>
                                                            <%--<td>
                                                                <asp:Button ID="btn_adjustamount" runat="server" Text="Adjust Amount" OnClick="btn_adjustamount_Click" CssClass="btn btn-primary" Style="width: 117px;" />
                                                            </td>--%>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:GridView ID="grid_adjustamount" runat="server" CssClass="table table-bordered table-striped" OnRowDataBound="grid_adjustamount_RowDataBound" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center;" ShowFooter="True">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Pay Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("slipdate") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_idate" runat="server" Text='<%#Bind("slipIdate") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Paymentmode" runat="server" Text='<%#Bind("Paymentmode") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Payment_id" runat="server" Text='<%#Bind("Payment_id") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unique Receipt No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Unique_id" runat="server" Text='<%#Bind("Unique_id") %>'></asp:Label>

                                                                            </ItemTemplate>
                                                                            <FooterTemplate><b>Total</b></FooterTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>

                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lbl_totaldiscount" runat="server" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                    <RowStyle ForeColor="#000066" />
                                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>
                                            </div>

                                            <div class="adm-box-wprs2">
                                                <asp:Label ID="Label1" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Payment History"></asp:Label>
                                                <asp:GridView ID="grid_payment_history" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center;" ShowFooter="True">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Slip No.">
                                                            <ItemTemplate>
                                                                <a target="_blank" href="slip/Admission_slip.aspx?admissionno=<%#Eval("Addmission_no") %>&sessionid=<%#Eval("session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>">
                                                                    <asp:Label ID="lbl_slipno" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>
                                                                </a>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Decription">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description") %>'></asp:Label>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl_totaldiscount" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Payment Mode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_paymentmode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                    </Columns>
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-xl-6" style="padding-left: 0px">
                                            <div class="adm-box-wprs3">
                                                <asp:Label ID="Label2" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Payment Deatils"></asp:Label>

                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label for="validationCustom01" class="form-label lblfnts">Slip Type</label>
                                                        </div>
                                                        <div class="col-md-9">
                                                            <asp:RadioButton ID="rd_new_bill_no" runat="server" GroupName="a" Text="New Slip No." Checked="true" />
                                                            <asp:RadioButton ID="rd_old_bill" runat="server" GroupName="a" Text="Old Deleted Slip No." />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label for="validationCustom01" class="form-label lblfnts">Slip No</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txt_slip_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label for="validationCustom01" class="form-label lblfnts">Payment Mode</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddl_paymentmode_adm" runat="server" class="form-select find-dv-txtbx" OnSelectedIndexChanged="ddl_paymentmode_adm_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem>Pos</asp:ListItem>
                                                                <asp:ListItem>Cash</asp:ListItem>
                                                                <asp:ListItem>Netbanking</asp:ListItem>
                                                                <asp:ListItem>Deposited In Bank</asp:ListItem>
                                                                <asp:ListItem>Sbdebit</asp:ListItem>
                                                                <asp:ListItem>Cheque</asp:ListItem>
                                                                <asp:ListItem>NEFT</asp:ListItem>
                                                                <asp:ListItem>Debitcard</asp:ListItem>
                                                                <asp:ListItem>Creditcard</asp:ListItem>
                                                                <asp:ListItem>Otherdcard</asp:ListItem>
                                                                <asp:ListItem>Demand Draft(DD)</asp:ListItem>
                                                                <asp:ListItem>UPI</asp:ListItem>
                                                                <%--<asp:ListItem>Branch</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr" id="bank_dts_amd" runat="server" visible="false">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label8" class="form-label lblfnts" runat="server" Text="Bank Name"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddl_bank_adm" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label6" class="form-label lblfnts" runat="server" Text="Date"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txt_bank_date_adm" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            <script>
                                                                $(function () {
                                                                    $("#<%=txt_bank_date_adm.ClientID %>").datepicker({
                                                                        dateFormat: "dd/mm/yy",
                                                                        changeMonth: true,
                                                                        changeYear: true,
                                                                        yearRange: "2021:2023",

                                                                        maxDate: '0',
                                                                    }).attr("readonly", "true");
                                                                });
                                                            </script>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr" id="pnl_mode_t_n_dv" runat="server" visible="true">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lbl_mode_trns_no_adm" class="form-label lblfnts" runat="server" Text="Transaction No."></asp:Label>
                                                        </div>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txt_transaction_no_adm" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label for="validationCustom01" class="form-label lblfnts">Payment Date</label>
                                                        </div>
                                                        <div class="col-md-3" runat="server" id="admpayDateDV">
                                                            <div style="margin: 0px; padding: 0px; float: left; width: 100%; position: relative">
                                                                <asp:TextBox ID="txt_date" runat="server" CssClass="calender-icon form-control find-dv-txtbx"></asp:TextBox>
                                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true" style="font-size: 10px; right: 3px"></i>
                                                                <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
                                                                <script src="../Autocomplete/jquery-ui.js"></script>
                                                                <script>
                                                                    $(function () {
                                                                        $("#<%=txt_date.ClientID %>").datepicker({
                                                                            dateFormat: "dd/mm/yy",
                                                                            changeMonth: true,
                                                                            changeYear: true,
                                                                            yearRange: "2021:2023",
                                                                            maxDate: '0',
                                                                        }).attr("readonly", "true");
                                                                    });
                                                                </script>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <label for="validationCustom01" class="form-label lblfnts">Paybale Amount  </label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lbl_adjustamount" runat="server" class="form-control find-dv-txtbx"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-md-6" style="display: none">
                                                            <asp:Label ID="lbl_paybaleamount" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                                            <label for="validationCustom01" class="form-label lblfnts">Paybale Amount  </label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label for="validationCustom01" class="form-label lblfnts">Paid Amount  </label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txt_paid_amount_adm" Font-Bold="true" class="form-control find-dv-txtbx" runat="server" OnTextChanged="txt_paid_amount_adm_TextChanged" AutoPostBack="true" Style="width: 100%;"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <label for="validationCustom01" class="form-label lblfnts">Total Dues   </label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lbl_totaldues" runat="server" class=" lblfnts form-control find-dv-txtbx" Style="height: 24px;"></asp:Label>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <asp:CheckBox ID="chk_split_month" runat="server" Text="Is Split Month" Checked="false" AutoPostBack="true" OnCheckedChanged="chk_split_month_CheckedChanged" Visible="false" />

                                                            <table class="table tab-content" id="splitmonth" runat="server" visible="false" style="background: #fff; border: 1px solid;">
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <b>Split Month</b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="grid_split_amount" runat="server" CssClass="table table-bordered table-striped" OnRowDataBound="grid_split_amount_RowDataBound" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center; margin: 0px 0px 0px 0px;" ShowFooter="false">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Sl. No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Month">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_Month_name" runat="server" Text='<%#Bind("Month") %>'></asp:Label>

                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txt_split_month_fee" runat="server" onkeypress="return isNumberKey(event)" Style="width: 50px"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lbl_total_split_fee" runat="server"></asp:Label>

                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                            <RowStyle ForeColor="#000066" />
                                                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label for="validationCustom01" class="form-label  lblfnts">Remarks</label>
                                                        </div>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txt_remrks" Font-Bold="true" class="form-control find-dv-txtbx" runat="server" TextMode="MultiLine" Style="width: 100%; height: 50px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-md-3"></div>
                                                        <div class="col-9">
                                                            <div style="overflow: hidden; height: 1px;">
                                                                <asp:Button ID="btn_submit_admission_fee" runat="server" Text="Pay Now" OnClick="btn_submit_admission_fee_Click" CssClass="btn btn-primary" Style="width: 1px; height: 1px; margin: 0px 0px 0px 0px;" />
                                                            </div>
                                                            <a onclick="save_data_admission()" class="button-37">Pay Now</a>
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
        </div>







        <div id="MdlRevisedPayment" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" style="max-width: 800px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 3px 17px;">
                        <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Revise Payment</h5>
                        <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                            class="btn btn-primary find-dv-btn">Close</a>
                    </div>
                    <div class="modal-body">
                        <div class="p-4 border rounded" style="float: left; width: 100%;">
                            <div class="disc-tbl-wprs">
                                <p style="margin: 0px 0px 5px 0px; font-size: 14px;">
                                    Slip Id :
                                <asp:Label ID="lbl_rv_slip_id" runat="server"></asp:Label>
                                </p>
                                <table style="width: 100%;" class="table table-hover table-bordered ">
                                    <tr>
                                        <th>Month</th>
                                        <th>Fee Head</th>
                                        <th>Fee Amt.</th>
                                        <th>Disc. Amt.</th>
                                        <th>Payable Amt.</th>
                                        <th>Paid Amt.</th>
                                        <th>Dues Amt.</th>
                                    </tr>
                                    <asp:Repeater ID="rp_revised" runat="server" OnItemDataBound="rp_revised_ItemDataBound">
                                        <ItemTemplate>
                                            <tr id="row" runat="server">

                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Bind("month") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_Id" runat="server" Visible="false" Text='<%#Bind("Id") %>'></asp:Label>
                                                    <asp:Label ID="lbl_content_id" runat="server" Visible="false" Text='<%#Bind("content_id") %>'></asp:Label>
                                                    <asp:Label ID="lbl_feee_head" runat="server" Text='<%#Bind("feetype") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_payble_amt" runat="server" Text='<%#Bind("payable") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("Disc") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_payble_after_disc" runat="server" Text='<%#Bind("Payable_after_disc") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_paid" runat="server" Text='<%#Bind("paid") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_dues" runat="server" Text='<%#Bind("dues") %>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="2" style="text-align: right; font-weight: 700;">Total : </td>
                                        <td>
                                            <asp:Label ID="lbl_rv_ttl_payble_amt" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_rv_ttl_disc_amt" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_rv_ttl_payble_after_disc" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_rv_ttl_paid" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_rv_ttl_dues" runat="server"></asp:Label></td>
                                    </tr>
                                </table>

                                <div class="prowwprs">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <p>Total Amount</p>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txt_rd_ttl_amt" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="prowwprs">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <p>Paid Amount</p>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txt_rd_paid_amt" runat="server" AutoPostBack="true" OnTextChanged="txt_rd_paid_amt_TextChanged" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="prowwprs">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <p>Dues Amount</p>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txt_rv_dues_amt" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="prowwprs">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <p>Remarks</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txt_rv_remark" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="prowwprs">
                                    <div class="row">
                                        <div class="col-md-3"></div>
                                        <div class="col-md-6">
                                            <asp:Button ID="btn_revised_pay" OnClick="btn_revised_pay_Click" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="ModalDiscount" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 3px 17px;">
                        <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Discount Details</h5>
                        <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                            class="btn btn-primary find-dv-btn">Close</a>
                    </div>
                    <div class="modal-body">
                        <div class="p-4 border rounded" style="float: left; width: 100%;">
                            <div class="disc-tbl-wprs">
                                <table style="width: 100%;" class="table table-hover table-bordered ">
                                    <tr>
                                        <th>Type</th>
                                        <th>Month</th>
                                        <th style="text-align: right">Amount</th>
                                    </tr>


                                    <asp:Repeater ID="rp_admission_discount" runat="server" OnItemDataBound="rp_admission_discount_ItemDataBound">
                                        <ItemTemplate>
                                            <tr id="row" runat="server">
                                                <td>
                                                    <asp:Label ID="lbl_type" runat="server" Text='<%#Bind("Type") %>'></asp:Label>
                                                </td>
                                                <td>-</td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_total_disc" runat="server" Text='<%#Bind("Total_disc") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>



                                    <asp:Repeater ID="rp_monthly_discount" runat="server" OnItemDataBound="rp_monthly_discount_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_type" runat="server" Text='<%#Bind("Type") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("Month") %>'></asp:Label></td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_total_disc" runat="server" Text='<%#Bind("Total_disc") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <tr>
                                        <td style="text-align: right" colspan="2">
                                            <b>Total</b>
                                        </td>

                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_final_total_disc" Style="font-weight: 600" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <div id="myModalStudentInfo" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 820px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 5px 10px;">
                        <h5 class="modal-title">Student Details</h5>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    <div class="modal-body">
                        <div class="p-4 border rounded" style="float: left; width: 100%;">
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
            </div>
        </div>



        <div id="mdlAlertMsgs" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 800px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 3px 10px;">
                        <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Important Message</h5>
                        <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                            class="btn btn-primary find-dv-btn">Close</a>
                    </div>
                    <div class="modal-body" style="padding: 5px 5px;">
                        <div class="p-4 border rounded" style="float: left; width: 100%; padding: 5px 5px !important;">
                            <div class="disc-tbl-wprs">
                                <asp:Label ID="lbl_alert_msgs" runat="server" class="alertmsgChk" Text="The previous payment for this child by cheque is still pending. Therefore, you cannot make another payment until the previous cheque has cleared."></asp:Label>
                                <div style="width: 100%; float: left; overflow: auto;">
                                    <table style="width: 100%; margin: 0px;" class="table table-hover table-striped table-bordered">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Admission No.</th>
                                                <th>Slip No.</th>
                                                <th>Cheque No.</th>
                                                <th>Bank Name</th>
                                                <th>Cheque Date</th>
                                                <th>Cheque Amount</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RPChkDetails" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_admission_no" runat="server" Font-Names="Arial" Text='<%#Bind("Admission_no") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_monthly_slip_no" runat="server" Font-Names="Arial" Text='<%#Bind("Monthly_slip_no") %>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Text='<%#Bind("Cheque_no") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Text='<%#Bind("Bank_name") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Font-Names="Arial" Text='<%#Bind("Cheque_date") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Text='<%#Bind("Cheque_amount") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="mdlDiscount" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 800px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 3px 10px;">
                        <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Discount
                        <asp:CheckBox ID="chk_is_admission_disc" onclick="ShowHideDivAdm(this)" Text="Discount on Admission" runat="server" /></h5>
                        <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                            class="btn btn-primary find-dv-btn">Close</a>
                    </div>
                    <asp:HiddenField ID="hd_fee_group" runat="server" />
                    <div class="modal-body" style="padding: 5px 5px;">
                        <div class="p-4 border rounded" style="float: left; width: 100%; padding: 5px 5px !important;">
                            <div class="disc-pop-dv-wpr">
                                <div class="fnd-box-wpr-inr" style="padding: 0px 0px;">
                                    <div id="monthFeeDiscHead">
                                        <div class="disc-month-checkbox-sec">
                                            <asp:Repeater ID="rp_month_for_discount" runat="server">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_month_discount" Text='<%#Eval("Month") %>' runat="server" />
                                                    <asp:Label ID="lbl_Month_discount" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                                    <asp:Label ID="lbl_month_id" runat="server" Visible="false" Text='<%#Bind("Month_Id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="disc-pop-tbl-wpr">
                                            <table class="table table-striped table-bordered dataTable" style="border-bottom: 1px solid #adadad; border-left: 1px solid #adadad; border-top: 1px solid #adadad; margin-bottom: 0px !important;">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Fees Head</th>
                                                        <th>Fees Amount</th>
                                                        <th>Disc. Amount</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_discount_fee_head" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("content_id") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td class="noclick">
                                                                    <asp:TextBox ID="txt_head_fee" class="noclick" runat="server" Style="width: 80px;" Text='<%#Eval("amount") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_disc_fee" runat="server" Text='<%#Eval("discount") %>' Style="width: 80px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr>
                                                        <td></td>
                                                        <td style="text-align: right; font-weight: 700;">TOTAL</td>
                                                        <td>
                                                            <asp:Label ID="lbl_total_head_fee_for_head" Style="font-weight: bold;" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbl_total_disc" runat="server" Style="font-weight: bold;"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                    <div id="admissionFeeDiscHead">
                                        <div class="disc-pop-tbl-wpr">
                                            <table class="table table-striped table-bordered dataTable" style="border-bottom: 1px solid #adadad; border-left: 1px solid #adadad; border-top: 1px solid #adadad; margin-bottom: 0px !important; margin-top: 0px !important;">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Fees Head</th>
                                                        <th>Fees Amount</th>
                                                        <th>Disc. Amount</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rp_fee_head_admission" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_adm_content" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_adm_content_id" runat="server" Text='<%#Bind("content_id") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td class="noclick">
                                                                    <asp:TextBox ID="txt_adm_head_fee" class="noclick" runat="server" Style="width: 80px;" Text='<%#Eval("amount") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_adm_disc_fee" runat="server" Text='<%#Eval("discount") %>' Style="width: 80px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr>
                                                        <td></td>
                                                        <td style="text-align: right; font-weight: 700;">TOTAL</td>
                                                        <td>
                                                            <asp:Label ID="lbl_total_head_fee_for_head_adm" Style="font-weight: bold;" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbl_total_disc_adm" runat="server" Style="font-weight: bold;"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="disc-pop-txtbx-sec">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <p>Discount Mode</p>
                                                <asp:DropDownList ID="ddl_discount_mode" runat="server" class="form-select disc-pop-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-7">
                                                <p>Remark</p>
                                                <asp:TextBox ID="txt_discount_Remarks" runat="server" class="form-control disc-pop-txtbx"></asp:TextBox>
                                            </div>

                                            <div class="col-md-2">
                                                <asp:Button ID="btn_save_discount" runat="server" class="button-6161 disc-pop-save_disc" Text="Save" OnClick="btn_save_discount_Click" />
                                            </div>
                                        </div>
                                    </div>



                                    <div class="disc-pop-grid-sec" id="discgridDV" runat="server">
                                        <h2>Discount History</h2>
                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Discount on</th>
                                                    <th>Month</th>
                                                    <th>Fees Head</th>
                                                    <th>Fees Amount</th>
                                                    <th>Disc. Amount</th>
                                                    <th>After Disc.</th>
                                                    <th>Discount Mode</th>
                                                    <th>Date</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_discount" runat="server" OnItemDataBound="rd_discount_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lbl_discount_on" runat="server" Text='<%#Bind("Discount_on")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("month")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_disc_amount" runat="server" Text='<%#Bind("disc_amount")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_after_disc" runat="server" Text='<%#Bind("after_disc")%>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_lbl_Student_Discunt_Type" runat="server" Text='<%#Bind("Discunt_Type")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDel" runat="server" OnClick="lnkDel_Click" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("content_id")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("Admission_no")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <tr>
                                                    <td colspan="4">Total</td>
                                                    <td>
                                                        <asp:Label ID="lbl_ttl_disc_amtss1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_ttl_disc_amtss2" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_ttl_disc_amtss3" runat="server"></asp:Label></td>
                                                    <td colspan="3">-</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="mdlExtraHead" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 800px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 3px 10px;">
                        <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Add Extra Fee Head</h5>
                        <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                            class="btn btn-primary find-dv-btn">Close</a>
                    </div>
                    <div class="modal-body" style="padding: 5px 5px;">
                        <div class="p-4 border rounded" style="float: left; width: 100%; padding: 5px 5px !important;">
                            <div class="disc-pop-dv-wpr">
                                <div class="fnd-box-wpr-inr" style="padding: 0px 0px;">
                                    <div class="disc-pop-txtbx-sec">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <p>Fee Head For</p>
                                                <asp:DropDownList ID="ddl_extra_head_for" runat="server" class="form-control disc-pop-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2" id="extHeadMnth">
                                                <p>Month</p>
                                                <asp:DropDownList ID="ddl_month" runat="server" class="form-select disc-pop-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3" id="feetitlesdV">
                                                <p>Fee Title</p>
                                                <asp:TextBox ID="txt_fee_title" runat="server" class="form-control disc-pop-txtbx"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <p>Amount</p>
                                                <asp:TextBox ID="txt_ext_fee_amt" runat="server" class="form-control disc-pop-txtbx" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="btn_save_extra_head" OnClick="btn_save_extra_head_Click" runat="server" class="button-6161 disc-pop-save_disc" Text="Save" />
                                            </div>
                                        </div>
                                    </div>



                                    <div class="disc-pop-grid-sec" id="extrafeeGrid" runat="server">
                                        <h2>Added Extra Fee Details</h2>
                                        <table class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>For</th>
                                                    <th>Month</th>
                                                    <th>Fee Title</th>
                                                    <th>Amount</th>
                                                    <th>Created Date</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lbl_fee_for" runat="server" Text='<%#Bind("Type_Mode")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_perticular" runat="server" Text='<%#Bind("Perticular")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDelExtFee" runat="server" OnClick="lnkDelExtFee_Click" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("Admission_No")%>' Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="4" style="text-align: right; font-weight: 600">Total</td>
                                                    <td>
                                                        <asp:Label ID="lbl_ttl_extra_fee" runat="server" Style="font-weight: 600"></asp:Label></td>
                                                    <td colspan="2">-</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal" role="dialog" style="top: 0px">
            <div class="modal-dialog md-width">
                <!-- Modal content-->
                <div class="modal-content" style="position: relative">
                    <div class="modal-header">
                        <h3 class="modal-title" style="font-size: 20px;">Update Payment Info</h3>
                        <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                    </div>
                    <div class="modal-body md-bdy">
                        <asp:HiddenField ID="hd_admission_edt" runat="server" />
                        <asp:HiddenField ID="hd_session_id_edt" runat="server" />
                        <asp:HiddenField ID="hd_class_id_edt" runat="server" />
                        <asp:HiddenField ID="hd_bill_no_edt" runat="server" />
                        <asp:HiddenField ID="hd_old_payment_date" runat="server" />
                        <asp:HiddenField ID="hd_old_payment_mode" runat="server" />
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Bill No.</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_bill_no_edit" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Payment Date</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_payment_date_edit" runat="server" class="form-control"></asp:TextBox>
                                    <script type="text/javascript">
                                        $(function () {
                                            $("#<%=txt_payment_date_edit.ClientID %>").datepicker({
                                                dateFormat: "dd/mm/yy",
                                                changeMonth: true,
                                                changeYear: true,
                                                yearRange: "2021:2030",
                                                maxDate: '0',
                                            }).attr("readonly", "true");
                                        });
                                    </script>
                                </div>
                            </div>
                        </div>
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Payment Mode</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_payment_mode" runat="server" class="form-control">
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Pos</asp:ListItem>
                                        <asp:ListItem>Netbanking</asp:ListItem>
                                        <asp:ListItem>Deposited In Bank</asp:ListItem>
                                        <asp:ListItem>Sbdebit</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>NEFT</asp:ListItem>
                                        <asp:ListItem>Debitcard</asp:ListItem>
                                        <asp:ListItem>Creditcard</asp:ListItem>
                                        <asp:ListItem>Otherdcard</asp:ListItem>
                                        <asp:ListItem>Demand Draft(DD)</asp:ListItem>
                                        <asp:ListItem>UPI</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-8">
                                    <asp:Button ID="btn_update_bill_info" runat="server" Text="Update" OnClick="btn_update_bill_info_Click" class="btn btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="myModalTransport" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 820px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 5px 10px;">
                        <h5 class="modal-title">Transport</h5>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    <div class="modal-body">
                        <div class="p-4 border rounded">
                            <div class="row g-3 needs-validation" novalidate="" id="mappedTransporTDV" runat="server">
                                <div class="col-md-12" style="display: none">
                                    <asp:RadioButton ID="rd_change_month_no" runat="server" Text="Change Month" GroupName="ab" Checked="true" />
                                    <asp:RadioButton ID="rd_change_Changetransport" runat="server" Text="Change Transport" GroupName="ab" />
                                    <asp:RadioButton ID="rd_change_both" runat="server" Text="Both" Checked="true" GroupName="ab" />
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Month</label>
                                    <asp:DropDownList ID="ddl_monthname" runat="server" class="form-select">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Vehicle</label>
                                    <asp:DropDownList ID="ddl_bus_name" runat="server" class="form-select" OnSelectedIndexChanged="ddl_bus_name_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Route</label>
                                    <asp:DropDownList ID="ddl_path_root" runat="server" class="form-select" OnSelectedIndexChanged="ddl_path_root_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">Boarding Point</label>
                                    <asp:DropDownList ID="ddl_boarding_point" runat="server" class="form-select">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-12">
                                    <asp:Button ID="btn_Submit" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                    <asp:Button ID="btn_remove_transport" OnClick="btn_remove_transport_Click" OnClientClick="return confirm('Are you sure you want to remove transport from this student?');" runat="server" Text="Remove Transport From This Student" Style="float: right; background: #f00; padding: 2px 7px 4px 7px; border: 1px solid #c10000; font-weight: 600;"
                                        class="btn btn-dark" CausesValidation="false" />
                                </div>
                            </div>


                            <div class="row g-3 needs-validation" novalidate="" id="TransportMapping" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-2">
                                            <label for="validationCustom01" class="form-label">Month</label>
                                            <asp:DropDownList ID="ddl_tr_month" runat="server" class="form-select"></asp:DropDownList>
                                        </div>
                                        <div class="col-4">
                                            <label for="validationCustom01" class="form-label">Vehicle</label>
                                            <asp:DropDownList ID="ddl_trns_vehicle" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddl_trns_vehicle_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="col-3">
                                            <label for="validationCustom01" class="form-label">Route</label>
                                            <asp:DropDownList ID="ddl_trns_route" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_trns_route_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-3">
                                            <label for="validationCustom01" class="form-label">Boarding Point</label>
                                            <asp:DropDownList ID="ddl_boarding_point_map" runat="server" class="form-select"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-12">
                                            <asp:Button ID="btn_transport_mapping" Style="margin: 10px 0px 0px 0px;" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btn_transport_mapping_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div id="myModalHostel" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 820px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 5px 10px;">
                        <h5 class="modal-title">Hostel</h5>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    <div class="modal-body">
                        <div class="p-4 border rounded">
                            <div class="row g-3 needs-validation" novalidate="" id="mappedHostelDV" runat="server">
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Hostel Name</label>
                                    <asp:Label ID="lbl_hostel_name" runat="server" class="form-control"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Room Type</label>
                                    <asp:Label ID="lbl_room_type" runat="server" class="form-control"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Room No.</label>
                                    <asp:Label ID="lbl_room_no" runat="server" class="form-control"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Bed No.</label>
                                    <asp:Label ID="lbl_bed_no" runat="server" class="form-control"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <asp:Button ID="btn_remove_hostel" OnClick="btn_remove_hostel_Click" OnClientClick="return confirm('Are you sure you want to remove hostel from this student?');" runat="server" Text="Remove Hostel From This Student" Style="float: right; background: #f00; padding: 2px 7px 4px 7px; border: 1px solid #c10000; font-weight: 600;"
                                        class="btn btn-dark" CausesValidation="false" />
                                </div>
                            </div>


                            <div class="row g-3 needs-validation" novalidate="" id="mappingHostelDV" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-3">
                                            <label for="validationCustom01" class="form-label">Hostel</label>
                                            <asp:DropDownList ID="ddl_hostel" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddl_hostel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="col-3">
                                            <label for="validationCustom01" class="form-label">Room Type</label>
                                            <asp:DropDownList ID="ddl_room_cat" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddl_room_cat_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="col-2">
                                            <label for="validationCustom01" class="form-label">Room</label>
                                            <asp:DropDownList ID="ddl_room" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_room_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-4">
                                            <label for="validationCustom01" class="form-label">Bed</label>
                                            <asp:DropDownList ID="ddl_bed" runat="server" class="form-select"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Button ID="btn_assign_hostel" runat="server" Style="margin: -15px 0px 0px 0px;" Text="Save" CssClass="btn btn-primary" OnClick="btn_assign_hostel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div id="myModalNote" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 820px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 5px 10px;">
                        <h5 class="modal-title">Note</h5>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    <div class="modal-body">
                        <div class="p-4 border rounded" style="float: left; width: 100%;">
                            <div class="mdl-frm-row">
                                <label for="validationCustom01" class="find-dv-lbl">Enter Note</label>
                                <div class="row">
                                    <div class="col-sm-11">
                                        <asp:TextBox ID="txt_note" runat="server" Style="height: 60px" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1" style="padding-left: 0px">
                                        <a href="#!" class="btn btn-primary" data-ng-click="ButtonSubmitNoteClick()" style="height: 60px; line-height: 60px; padding: 0px 0px; width: 100%;">Save</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="p-4 border rounded" style="float: left; width: 100%;">
                            <table class="table table-bordered">
                                <tr>
                                    <th>#</th>
                                    <th>Date</th>
                                    <th>Notes Details</th>
                                    <th></th>
                                </tr>
                                <tr data-ng-repeat="x in reportNote">
                                    <td>{{$index+1}}</td>
                                    <td>{{x.Dates}}-{{x.Times}}</td>
                                    <td>{{x.Content}}</td>
                                    <td class="hiddenOnPrint"><a style="background-color: #f7f100; min-width: 30px; color: #000;"
                                        class="button-61 nowordbreak collect-feesss" href="#!" data-toggle="modal" data-ng-click="ButtonDelete(x.RowId)"><span class="material-symbols-outlined">delete</span></a></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="myModalPayStatus" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 1000px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 5px 10px;">
                        <h5 class="modal-title">Payment Status</h5>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    <div class="modal-body" style="padding: 5px 5px;">
                        <div class="p-4 border rounded" style="float: left; width: 100%;">
                            <div class="std-search-wpr" style="height: auto;">
                                <div class="paystatus-clr-ul-wpr">
                                    <ul class="paystatus-clr-ul">
                                        <li style="border-right: 0;"><i class="duesZero"></i>
                                            <span>Fully Paid</span></li>
                                        <li style="border-right: 0;"><i class="OverDues"></i>
                                            <span>Over Due</span></li>
                                        <li style="border-right: 0;"><i class="CurrentPayable"></i>
                                            <span>Current Payable</span></li>
                                        <li><i class="UpcomingPayment"></i>
                                            <span>Upcoming</span></li>
                                    </ul>
                                </div>
                                <table class="table table-bordered">
                                    <tr>
                                        <th>#</th>
                                        <th>Fee Head</th>
                                        <th>Month</th>
                                        <th>Amount</th>
                                        <th>Paid</th>
                                        <th>Discount</th>
                                        <th>To Pay</th>
                                    </tr>
                                    <tr data-ng-repeat="x in reportFeeStatus">
                                        <td>{{$index+1}}</td>
                                        <td>{{x.Fee_head}}</td>
                                        <td>{{x.Month_name}}</td>
                                        <td class="{{x.PaymentStatus}}">{{x.Amount}}</td>
                                        <td class="{{x.PaymentStatus}}">{{x.Prev_paid}}</td>
                                        <td class="{{x.PaymentStatus}}">{{x.Disc_amount}}</td>
                                        <td class="{{x.PaymentStatus}}">{{x.Dues_amt}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <style>
            .duesZero {
                background: #09a32c !important;
                color: #fff;
            }

            .OverDues {
                background: #f1172c !important;
                color: #fff;
            }

            .CurrentPayable {
                background: #e3aa00 !important;
                color: #fff;
            }

            .UpcomingPayment {
                background: #17a2b8 !important;
                color: #fff;
            }

            .paystatus-clr-ul-wpr {
                margin: 0px 0px 5px 0px;
                padding: 0px;
                width: 100%;
                float: left;
            }

            .paystatus-clr-ul {
                margin: 0px;
                padding: 0px;
                width: 100%;
                float: left;
            }

                .paystatus-clr-ul li {
                    margin: 0px;
                    padding: 5px 5px;
                    width: 25%;
                    float: left;
                    list-style-type: none;
                    border: 1px solid #dddddd;
                }

                    .paystatus-clr-ul li i {
                        margin: 0px;
                        padding: 0px;
                        width: 25px;
                        height: 25px;
                        float: left;
                    }

                    .paystatus-clr-ul li span {
                        margin: 0px;
                        padding: 2px 0px 0px 5px;
                        float: left;
                    }
        </style>

        <div id="myModalStudentInfo2" class="modal fade" role="dialog">
            <div class="modal-dialog" style="max-width: 1000px;">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 5px 10px;">
                        <h5 class="modal-title">Student Details</h5>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    <div class="modal-body" style="padding: 5px 5px;">
                        <div class="p-4 border rounded" style="float: left; width: 100%;">
                            <div class="angularfilter">
                                <input type="text" data-ng-model="searchs" class="form-control txtbx-ddl-style" style="margin: 0px;" placeholder="type here to filter data" />
                            </div>
                            <div class="std-search-wpr" runat="server" id="angStd">
                                <table class="table table-bordered">
                                    <tr>
                                        <th>#</th>
                                        <th>Admission No.</th>
                                        <th>Class</th>
                                        <th>Section</th>
                                        <th>Roll No.</th>
                                        <th>Student Name</th>
                                        <th>Father Name</th>
                                        <th>Mobile No.</th>
                                        <th></th>
                                    </tr>
                                    <tr data-ng-repeat="x in reportStd | filter : searchs">
                                        <td>{{$index+1}}</td>
                                        <td>{{x.Admission_no}}</td>
                                        <td>{{x.Class_name}}</td>
                                        <td>{{x.Section}}</td>
                                        <td>{{x.Rollnumber}}</td>
                                        <td>{{x.Studentname}}</td>
                                        <td>{{x.Fathername}}</td>
                                        <td>{{x.Mobile_no}}</td>
                                        <td class="hiddenOnPrint"><a style="background-color: #f7f100; min-width: 30px; color: #000;"
                                            class="button-61 nowordbreak collect-feesss" href="fees-collection-1.aspx?adm={{x.Admission_no}}&sessionid={{x.Session_id}}"><span class="material-symbols-outlined">done_all</span></a></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="conf-alrt-sec fade in hidden" id="conf_alrtDel" style="z-index: 99999999;">
            <div class="conf-alrt-inr" style="width: 300px;">
                <p class="conf-alrt-msg-p">Are you sure you want delete?</p>
                <ul class="conf-btn-ul">
                    <li><a href="javascript:" data-ng-click="ButtonCancelDelete()" style="background: #fff; border: 1px solid #ddd; color: #0072ff;">Cancel</a></li>
                    <li><a href="javascript:" data-ng-click="ButtonConfDelete()">Ok</a></li>
                </ul>
            </div>
        </div>





        <script type="text/javascript">
            var app = angular.module('RpFeeApp', []);
            app.controller('RpFeeAppCtrl', function ($scope, $http, $exceptionHandler) {

                var admission_no = $("#<%=hd_admission_no.ClientID %>").val();
                var session_id = $("#<%=hd_session_id.ClientID %>").val();
                var class_id = $("#<%=hd_class_id.ClientID %>").val();


                $http.get("webServices/student-note.asmx/fetch_student_note_data", { params: { "Admission_no": admission_no, "Session_id": session_id } }).then(function (response) {
                    $scope.reportNote = response.data;
                })

                var session_idstd = $("#<%=ddlsessionad.ClientID%>").val();
                $http.get("webServices/student-note.asmx/fetch_student_data", { params: { "Session_id": session_idstd } }).then(function (response) {
                    $scope.reportStd = response.data;
                })


                //====
                $http.get("webServices/student-note.asmx/fetch_student_payment_status", { params: { "Admission_no": admission_no, "Session_id": session_id, "Class_id": class_id } }).then(function (response) {
                    $scope.reportFeeStatus = response.data;
                })


                $scope.ButtonSubmitNoteClick = function () {
                    var NoteStd = $("#<%=txt_note.ClientID %>").val();
                    var admission_no = $("#<%=hd_admission_no.ClientID %>").val();
                    var session_id = $("#<%=hd_session_id.ClientID %>").val();
                    var user_id = $("#<%=hd_user_id.ClientID %>").val();
                    if (NoteStd == "") {
                        $("#<%=txt_note.ClientID %>").focus();
                        alert("Please enter note");
                    }
                    else {
                        // alert(session_id);
                        //Save-Data  
                        $http.get("webServices/student-note.asmx/send_student_note_data", { params: { "Admission_no": admission_no, "Session_id": session_id, "NoteStd": NoteStd, "User_id": user_id } }).then(function (response) {
                            console.log(response.data);
                            $scope.client_dt = response.data;
                            if ($scope.client_dt == "") {
                                $("#<%=txt_note.ClientID %>").val('');


                                $http.get("webServices/student-note.asmx/fetch_student_note_data", { params: { "Admission_no": admission_no, "Session_id": session_id } }).then(function (response) {
                                    $scope.reportNote = response.data;
                                })
                            }
                        })
                    }
                }




                ///DELETE
                var RowId_del = "";
                $scope.ButtonDelete = function (RowId) {
                    RowId_del = RowId;
                    $("#conf_alrtDel").removeClass("hidden");
                }

                $scope.ButtonConfDelete = function () {
                    var admission_no = $("#<%=hd_admission_no.ClientID %>").val();
                    var session_id = $("#<%=hd_session_id.ClientID %>").val();
                    var user_by = $("#<%=hd_user_id.ClientID%>").val();
                    $http.get("webServices/student-note.asmx/delete_student_note", { params: { "RowId_del": RowId_del } }).then(function (response) {
                        $scope.client_dt = response.data;
                        if ($scope.client_dt == "") {
                            $("#conf_alrtDel").addClass("hidden");
                            //FetchV 
                            $http.get("webServices/student-note.asmx/fetch_student_note_data", {
                                params: { "Admission_no": admission_no, "Session_id": session_id }
                            }).then(function (response) {
                                $scope.reportNote = response.data;
                            })
                        }
                    })
                }

                $scope.ButtonCancelDelete = function () {
                    RowId_del = "0";
                    $("#conf_alrtDel").addClass("hidden");
                }
            });

        </script>
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
            background: #312F7F !important;
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

        .isChangeAdmAmt label {
            color: #fff;
            font-size: 13px;
        }
    </style>



    <script type="text/javascript">  
        //=============================
        $(document).ready(function () {
            $("#<%=txt_ext_fee_amt.ClientID %>").focus(function () { $(this).select(); });

            CalculateTotalFee();
            $("[Id*=txt_head_fee]").on("keyup", function () {
                CalculateTotalFee();
            });



            CalculateTotal();
            $("[Id*=txt_disc_fee]").on("keyup", function () {
                CalculateTotal();
            });


            ///======================
            on_extra_type_selection();
            $("#<%=ddl_extra_head_for.ClientID%>").on('change', function () {
                on_extra_type_selection();
            })



            CalculateTotalFeeAdm();
            $("[Id*=txt_adm_head_fee]").on("keyup", function () {
                CalculateTotalFeeAdm();
            });



            CalculateTotalAdm();
            $("[Id*=txt_adm_disc_fee]").on("keyup", function () {
                CalculateTotalAdm();
            });

            //=========================
            var chkPassport = $("#<%=chk_is_admission_disc.ClientID %>");
            ShowHideDivAdm(chkPassport);
        });
        function CalculateTotalFee() {
            var total = 0;
            $("[Id*=txt_head_fee").each(function () {
                total += parseFloat($(this).val());
            });
            $("#<%=lbl_total_head_fee_for_head.ClientID%>").text(total);
        }


        function CalculateTotal() {
            var total = 0; var totalFee = 0;
            $("[Id*=txt_disc_fee").each(function () {
                total += parseFloat($(this).val());

                $("[Id*=txt_disc_fee").focus(function () { $(this).select(); });
            });

            $("[Id*=txt_head_fee").each(function () {
                totalFee = parseFloat($(this).val());
            });
            //alert(totalFee);
            //alert(total); 
            //if (total > totalFee) {
            //    $(this).val(totalFee);
            //    alert("Freight total should be less than 10000");
            //}
            //else {
            $("#<%=lbl_total_disc.ClientID%>").text(total);


            var ttlfeeheads = parseFloat($("#<%=lbl_total_head_fee_for_head.ClientID%>").text());
            var ttlfeedisc = parseFloat($("#<%=lbl_total_disc.ClientID%>").text());

            //alert(ttlfeeheads); alert(ttlfeedisc);
            if (ttlfeedisc > ttlfeeheads) {
                //alert(ttlfeeheads); alert(ttlfeedisc);
                $("#<%=lbl_total_disc.ClientID%>").text(ttlfeeheads);
            }
            //}
        }


        ///===EXTRA==HEAD  
        function on_extra_type_selection() {
            if ($('#<%= ddl_extra_head_for.ClientID %> option:selected').val() == "1") {
                $("#extHeadMnth").show();
                $("#feetitlesdV").removeClass("col-md-5");
                $("#feetitlesdV").addClass("col-md-3");
            }
            else {
                $("#extHeadMnth").hide();
                $("#feetitlesdV").removeClass("col-md-3");
                $("#feetitlesdV").addClass("col-md-5");
            }
        }


        function ShowHideDivAdm(chkPassport) {
            if ($(chkPassport).prop('checked') == true) {
                $("#monthFeeDiscHead").hide();
                $("#admissionFeeDiscHead").show();
            }
            else {
                $("#monthFeeDiscHead").show();
                $("#admissionFeeDiscHead").hide();
            }
        }


        function CalculateTotalFeeAdm() {
            var total = 0;
            $("[Id*=txt_adm_head_fee").each(function () {
                total += parseFloat($(this).val());
            });
            $("#<%=lbl_total_head_fee_for_head_adm.ClientID%>").text(total);
        }

        //==========
        function CalculateTotalAdm() {
            var total = 0; var totalFee = 0;
            $("[Id*=txt_adm_disc_fee").each(function () {
                total += parseFloat($(this).val());

                $("[Id*=txt_adm_disc_fee").focus(function () { $(this).select(); });
            });

            $("[Id*=txt_adm_head_fee").each(function () {
                totalFee = parseFloat($(this).val());
            });
            $("#<%=lbl_total_disc_adm.ClientID%>").text(total);


            var ttlfeeheads = parseFloat($("#<%=lbl_total_head_fee_for_head_adm.ClientID%>").text());
            var ttlfeedisc = parseFloat($("#<%=lbl_total_disc_adm.ClientID%>").text());

            if (ttlfeedisc > ttlfeeheads) {
                $("#<%=lbl_total_disc_adm.ClientID%>").text(ttlfeeheads);
            }
        }
    </script>
</asp:Content>
