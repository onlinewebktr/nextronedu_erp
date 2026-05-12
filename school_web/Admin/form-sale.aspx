<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="form-sale.aspx.cs" Inherits="school_web.Admin.form_sale" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Form Sale
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 33px;
            right: 22px;
        }

        .mandatory {
            background-color: #ffffb4;
            border-left: 5px solid #cbcb00;
            box-shadow: inset 10px 9px 14px #a39e0d1a;
            -moz-appearance: none;
        }
    </style>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
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

        #pageFooter {
            display: none;
        }

        .home-grph-wpr {
            width: 114%;
        }

        tbody, td, tfoot, th, thead, tr {
            font-size: 13px;
            padding: 5px 5px !important;
        }
    </style>

    <style>
        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .mdl-close-btn {
            margin: 0px;
            padding: 0px 5px 0px 5px;
            border: 0px;
            background: #ed0000;
            font-size: 18px;
            color: #fff;
            line-height: 25px;
            border-radius: 2px;
        }

        .modal-header {
            padding: 7px 15px;
        }

        .form-ttleS {
            margin: 0px 0px 0px 0px;
            padding: 8px 10px 5px 10px;
            width: 100%;
            float: left;
            font-size: 18px;
            color: #0296bd;
            border-bottom: 1px solid #ddd;
        }

        tfoot, th, thead {
            color: #fff;
        }

        sup, sub {
            color: red;
            font-size: 15px;
            top: -0.2em;
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Admission</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Form Sale</li>
                        </ol>
                    </nav>
                </div>
                <a href="slip/registration-form.aspx?form_no=xx--0" target="_blank" class="button-29" style="float: right; position: absolute; right: 0px;"><span class="material-symbols-outlined" style="margin: 0px 5px 0px 0px; font-size: 18px;">print</span>Blank Form</a>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Form Sale"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Student Information</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select mandatory" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Index No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_indesx_no" runat="server" class="form-control mandatory"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_class" class="form-select mandatory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" id="payDatetxtbxDVS" runat="server">
                                        <label for="validationCustom01" class="form-label">Date<sup>*</sup></label>
                                        <div id="payDatetxtbx" runat="server">
                                            <asp:TextBox ID="txt_date" runat="server" class="form-control mandatory"></asp:TextBox>
                                        </div>
                                    </div>



                                    <div class="col-md-12">
                                        <div style="float: left; width: 100%; border: 1px solid #dcedd6; padding: 5px 10px; background: #f5fff2; border-radius: 4px;">
                                            <label for="validationCustom01" class="form-label" style="font-size: 16px; font-weight: 600;">
                                                Student Name<sup>*</sup></label>
                                            <div class="row g-3 needs-validation">
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_student_first_name" runat="server" class="form-control mandatory"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">First Name</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_student_middle_name" runat="server" class="form-control"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">Middle Name</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_student_last_name" runat="server" class="form-control"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">Last Name</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div style="float: left; width: 100%; border: 1px solid #dcedd6; padding: 5px 10px; background: #f5fff2; border-radius: 4px;">
                                            <label for="validationCustom01" class="form-label" style="font-size: 16px; font-weight: 600;">
                                                Father's Name<sup>*</sup></label>
                                            <div class="row g-3 needs-validation">
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_fathers_first_name" runat="server" class="form-control mandatory"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">First Name</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_fathers_middle_name" runat="server" class="form-control"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">Middle Name</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_fathers_last_name" runat="server" class="form-control"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">Last Name</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div style="float: left; width: 100%; border: 1px solid #dcedd6; padding: 5px 10px; background: #f5fff2; border-radius: 4px;">
                                            <label for="validationCustom01" class="form-label" style="font-size: 16px; font-weight: 600;">
                                                Mother's Name</label>
                                            <div class="row g-3 needs-validation">
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_mothers_first_name" runat="server" class="form-control"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">First Name</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_mothers_middle_name" runat="server" class="form-control"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">Middle Name</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_mothers_last_name" runat="server" class="form-control"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">Last Name</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div style="float: left; width: 100%; border: 1px solid #dcedd6; padding: 5px 10px; background: #f5fff2; border-radius: 4px;">
                                            <label for="validationCustom01" class="form-label" style="font-size: 16px; font-weight: 600;">
                                                Guardian's Name<sup runat="server" id="guardianNameMn" visible="false">*</sup></label>
                                            <div class="row g-3 needs-validation">
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_gaurdian_first_name" runat="server" class="form-control"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">First Name</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_gaurdian_middle_name" runat="server" class="form-control"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">Middle Name</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_gaurdian_last_name" runat="server" class="form-control"></asp:TextBox>
                                                    <label for="validationCustom01" class="form-label" style="font-size: 13px;">Last Name</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Nationality<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_nationality" class="form-select" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" style="position: relative;">
                                        <label for="validationCustom01" class="form-label">Date of Birth </label>
                                        <asp:TextBox ID="txt_dob" runat="server" class="form-control"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        <span id="result" style="float: left;color: #0015a7; font-size: 13px;"></span>

                                        <script type="text/javascript">
                                            $(document).ready(function () {
                                                $("#<%= txt_dob.ClientID %>").on("change input", function () {
                                                    var dobInput = $(this).val();
                                                    var dob;

                                                    // Parse DD/MM/YYYY format
                                                    if (dobInput.match(/^\d{2}\/\d{2}\/\d{4}$/)) {
                                                        var parts = dobInput.split("/");
                                                        dob = new Date(parts[2], parts[1] - 1, parts[0]);
                                                    } else {
                                                        $("#result").text("Please enter a valid date in DD/MM/YYYY format.");
                                                        return;
                                                    }
                                                    var today = new Date(); // Current date: May 28, 2025, 07:15 PM IST
                                                    if (isNaN(dob) || dob > today) {
                                                        $("#result").text("Please enter a valid past date in DD/MM/YYYY format.");
                                                        return;
                                                    }
                                                    var years = today.getFullYear() - dob.getFullYear();
                                                    var months = today.getMonth() - dob.getMonth();
                                                    var days = today.getDate() - dob.getDate();
                                                    if (days < 0) {
                                                        months--;
                                                        days += new Date(today.getFullYear(), today.getMonth(), 0).getDate();
                                                    }
                                                    if (months < 0) {
                                                        years--;
                                                        months += 12;
                                                    }
                                                    var resultText = "Age: " + years + " years, " + months + " months, " + days + " days";
                                                    $("#result").text(resultText);
                                                });
                                            });
                                        </script>
                                    </div>


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Gender<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_gender" class="form-select mandatory" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>MALE</asp:ListItem>
                                            <asp:ListItem>FEMALE</asp:ListItem>
                                            <asp:ListItem>TRANSGENDER</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Blood Group</label>
                                        <asp:DropDownList ID="ddl_blood_group" class="form-select" runat="server">
                                            <asp:ListItem>NA</asp:ListItem>
                                            <asp:ListItem>A+</asp:ListItem>
                                            <asp:ListItem>A-</asp:ListItem>
                                            <asp:ListItem>B+</asp:ListItem>
                                            <asp:ListItem>B-</asp:ListItem>
                                            <asp:ListItem>O+</asp:ListItem>
                                            <asp:ListItem>O-</asp:ListItem>
                                            <asp:ListItem>AB+</asp:ListItem>
                                            <asp:ListItem>AB-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Height</label>
                                        <asp:TextBox ID="txt_height" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Weight</label>
                                        <asp:TextBox ID="txt_weight" runat="server" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Category</label>
                                        <asp:DropDownList ID="ddl_cast" class="form-select" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>General</asp:ListItem>
                                            <asp:ListItem>OBC</asp:ListItem>
                                            <asp:ListItem>SC</asp:ListItem>
                                            <asp:ListItem>ST</asp:ListItem>
                                            <%--<asp:ListItem>EBC</asp:ListItem>--%>
                                            <%--<asp:ListItem>Others</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Religion</label>
                                        <asp:DropDownList ID="ddl_religion" class="form-select" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>HINDU</asp:ListItem>
                                            <asp:ListItem>ISLAM</asp:ListItem>
                                            <asp:ListItem>SIKH</asp:ListItem>
                                            <asp:ListItem>CHRISTIAN</asp:ListItem>
                                            <asp:ListItem>BUDDHISM</asp:ListItem>
                                            <asp:ListItem>JAIN</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Aadhaar No.</label>
                                        <asp:TextBox ID="txt_aadhar_no" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mother tongue<sup> </sup></label>
                                        <asp:DropDownList ID="ddl_student_mother_tongue" class="form-select" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Second Language<sup> </sup></label>
                                        <asp:DropDownList ID="ddl_Second_Language" class="form-select" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>HINDI</asp:ListItem>
                                            <asp:ListItem>BENGALI</asp:ListItem>
                                            <asp:ListItem>SANSKRIT</asp:ListItem>
                                            <asp:ListItem>URDU</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Language Spoken at Home</label>
                                        <asp:TextBox ID="txt_language_spoken_at_home" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mobile No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_mobile" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Alternate Mobile No.</label>
                                        <asp:TextBox ID="txt_alternate_mobile_no" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Amount<sup>*</sup></label>
                                        <asp:TextBox ID="txt_amount" runat="server" class="form-control mandatory" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Payment Mode<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_paymentmode" runat="server" class="form-select mandatory">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Netbanking</asp:ListItem>
                                            <asp:ListItem>Deposited In Bank</asp:ListItem>
                                            <asp:ListItem>Sbdebit</asp:ListItem>
                                            <asp:ListItem>Cheque</asp:ListItem>
                                            <asp:ListItem>NEFT</asp:ListItem>
                                            <asp:ListItem>Debitcard</asp:ListItem>
                                            <asp:ListItem>Creditcard</asp:ListItem>
                                            <asp:ListItem>Otherdcard</asp:ListItem>
                                            <asp:ListItem>UPI</asp:ListItem>
                                            <asp:ListItem>Pos</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3" id="bank_dt">
                                        <label for="validationCustom01" class="form-label">Bank Name<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_bank" runat="server" class="form-select find-dv-txtbx mandatory"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" id="pnl_mode_t_nS">
                                        <asp:Label ID="lbl_mode_trns_no" runat="server" class="form-label-fnds" Text="Transaction No."></asp:Label>
                                        <asp:TextBox ID="txt_trans_no" runat="server" class="form-control find-dv-txtbx mandatory"></asp:TextBox>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Remarks</label>
                                        <asp:TextBox ID="txt_remarks" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3" id="doe" runat="server" visible="false" style="position: relative;">
                                        <label for="validationCustom01" class="form-label">Exam Date<sup>*</sup></label>

                                        <asp:TextBox ID="txt_date_of_exam" runat="server" class="form-control"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    </div>


                                </div>
                            </div>

                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 10px 0px 0px 0px;">
                                <h2 class="form-ttleS">Address Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Address<sup> </sup></label>
                                        <asp:TextBox ID="txt_address" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">P.O.<sup> </sup></label>
                                        <asp:TextBox ID="txt_po" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">P.S.<sup> </sup></label>
                                        <asp:TextBox ID="txt_ps" runat="server" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">District<sup> </sup></label>
                                        <asp:TextBox ID="txt_district" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">City<sup> </sup></label>
                                        <asp:TextBox ID="txt_city" runat="server" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">State</label>
                                        <asp:TextBox ID="txt_c_state" runat="server" class="form_control" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddl_temp_state" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Pin Code<sup> </sup></label>
                                        <asp:TextBox ID="txt_pincode" runat="server" class="form-control" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Country<sup> </sup></label>
                                        <asp:DropDownList ID="ddl_country_c" runat="server" CssClass="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="online_frm-grp">
                                            <label for="validationCustom01" class="form-label">Mobile No.<sup> </sup></label>
                                            <div class="row">
                                                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px">
                                                    <asp:DropDownList ID="ddl_cunterycode3" runat="server" class="form-select" Style="border-radius: 4px 0px 0px 4px;"></asp:DropDownList>
                                                </div>
                                                <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-left: 0px">
                                                    <asp:TextBox ID="txt_temp_mobileno" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 10px 0px 0px 0px;">
                                <h2 class="form-ttleS">Sibling Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-12">
                                        <table class="table-bordered" style="width: 100%">
                                            <tr>
                                                <th>S. No.</th>
                                                <th>Name of Sibling</th>
                                                <th>Age</th>
                                                <th>School/College</th>
                                                <th>Class</th>
                                            </tr>
                                            <tr>
                                                <td>1.</td>
                                                <td>
                                                    <asp:TextBox ID="txt_name_of_sibling1" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_age_sibling1" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_school_sibling1" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_class_sb1" class="form-select" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>2.</td>
                                                <td>
                                                    <asp:TextBox ID="txt_name_of_sibling2" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_age_sibling2" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_school_sibling2" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_class_sb2" class="form-select" runat="server"></asp:DropDownList></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>


                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 10px 0px 0px 0px;">
                                <h2 class="form-ttleS">Record of Previous School Attended</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-12">
                                        <table class="table-bordered" style="width: 100%">
                                            <tr>
                                                <th>S. No.</th>
                                                <th>Name of School</th>
                                                <th>Address of School</th>
                                                <th>Class From</th>
                                                <th>Class To</th>
                                                <th>Year From</th>
                                                <th>Year To</th>
                                                <th>Board Type</th>
                                                <th>Board CBSE/ISE/State</th>
                                                <th>Medium of Instruction</th>
                                                <th>% of Marks</th>
                                            </tr>
                                            <tr>
                                                <td>1.</td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_school_name1" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_schho_address1" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_prv_class_from1" runat="server" class="form-select"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_prv_class_to1" runat="server" class="form-select"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_school_year_from1" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_school_year_to_1" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_prev_board_type" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_prev_board_SelectedIndexChanged">
                                                        <asp:ListItem>SELECT</asp:ListItem>
                                                        <asp:ListItem>COUNCIL</asp:ListItem>
                                                        <asp:ListItem>STATE</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_prv_school_board1" runat="server" class="form-select"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_school_medium" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_school_marks" runat="server" class="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>2.</td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_school_name2" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_schho_address2" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_prv_class_from2" runat="server" class="form-select"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_prv_class_to2" runat="server" class="form-select"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_school_year_from2" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_school_year_to_2" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_prev_board_type2" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_prev_board_type2_SelectedIndexChanged">
                                                        <asp:ListItem>SELECT</asp:ListItem>
                                                        <asp:ListItem>COUNCIL</asp:ListItem>
                                                        <asp:ListItem>STATE</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_prv_school_board2" runat="server" class="form-select"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_school_mediu2" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_prv_school_mark2" runat="server" class="form-control"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 10px 0px 0px 0px;">
                                <h2 class="form-ttleS">Particulars of Family</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-12">
                                        <table class="table-bordered" style="width: 100%">
                                            <tr>
                                                <th>Particular</th>
                                                <th>Aadhaar No.</th>
                                                <th>Educational Qualification</th>
                                                <th>Occupation</th>
                                                <th>Annual Income</th>
                                                <th>Contact No.</th>
                                                <th>Email ID</th>
                                            </tr>
                                            <tr>
                                                <td>Father</td>
                                                <td>
                                                    <asp:TextBox ID="txt_fthr_aadhar_no" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_f_education" runat="server" class="form-select"></asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_f_occupation" runat="server" class="form-select">
                                                        <asp:ListItem>OTHERS</asp:ListItem>
                                                        <asp:ListItem>STATE GOVT. JOB</asp:ListItem>
                                                        <asp:ListItem>CENTRAL GOVT. JOB</asp:ListItem>
                                                        <asp:ListItem>PRIVATE JOB</asp:ListItem>
                                                        <asp:ListItem>BUSINESS</asp:ListItem>
                                                        <asp:ListItem>FARMER</asp:ListItem>
                                                        <asp:ListItem>PUBLIC SECTOR EMPLOYEE</asp:ListItem>
                                                        <asp:ListItem>NA</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txt_f_annual_income" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_f_contact_no" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_f_email_id" runat="server" class="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Mother</td>
                                                <td>
                                                    <asp:TextBox ID="txt_m_aadhar_no" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_m_education" runat="server" class="form-select"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_m_occupation" runat="server" class="form-select">
                                                        <asp:ListItem>OTHERS</asp:ListItem>
                                                        <asp:ListItem>STATE GOVT. JOB</asp:ListItem>
                                                        <asp:ListItem>CENTRAL GOVT. JOB</asp:ListItem>
                                                        <asp:ListItem>PRIVATE JOB</asp:ListItem>
                                                        <asp:ListItem>BUSINESS</asp:ListItem>
                                                        <asp:ListItem>FARMER</asp:ListItem>
                                                        <asp:ListItem>HOUSE WIFE</asp:ListItem>
                                                        <asp:ListItem>PUBLIC SECTOR EMPLOYEE</asp:ListItem>
                                                        <asp:ListItem>NA</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_m_annual_income" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_m_contact_no" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_m_email_id" runat="server" class="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Local Guardian (Relation)</td>
                                                <td>
                                                    <asp:TextBox ID="txt_g_aadhar_no" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_g_education" runat="server" class="form-select"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_g_occupation" runat="server" class="form-select">
                                                        <asp:ListItem>OTHERS</asp:ListItem>
                                                        <asp:ListItem>STATE GOVT. JOB</asp:ListItem>
                                                        <asp:ListItem>CENTRAL GOVT. JOB</asp:ListItem>
                                                        <asp:ListItem>PRIVATE JOB</asp:ListItem>
                                                        <asp:ListItem>BUSINESS</asp:ListItem>
                                                        <asp:ListItem>FARMER</asp:ListItem>
                                                        <asp:ListItem>PUBLIC SECTOR EMPLOYEE</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_g_annual_income" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_g_contact_no" runat="server" class="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_g_email_id" runat="server" class="form-control"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col-3">
                                <asp:Button ID="btn_Submit" Style="margin: 15px 0px 0px 0px;" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                                <asp:Button ID="btn_cancel" Style="margin: 15px 0px 0px 0px;" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Form Sale</h6>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                        <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                        <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 20px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 20px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr">
                                                    <div id="content">

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

                                                                    &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                </div>
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <span style="font-size: 14px; font-weight: bold;">Form Sale List
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                </div>
                                                            </div>


                                                        </div>

                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table id="example2" data-page-length='999999999999' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th class="hiddenOnPrint">Action</th>
                                                                        <th>Form No</th>
                                                                        <th>Index no.</th>
                                                                        <th>Adm. no.</th>
                                                                        <th>Student Name</th>
                                                                        <th>Father's Name</th>
                                                                        <th>Gender</th>
                                                                        <th>Class</th>
                                                                        <th>Mobile</th>
                                                                        <th>Reg. Date</th>
                                                                        <th>Adm. Date</th>
                                                                        <th>Amount</th>
                                                                        <th>Mode</th>
                                                                        <th>Remarks</th>
                                                                        <th>Exam Date</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <tr runat="server" id="trR">
                                                                                <td>
                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                </td>
                                                                                <td class="hiddenOnPrint">
                                                                                    <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                        <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                            href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                            <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                                <i class="bx bx-grid-horizontal"></i>
                                                                                            </div>
                                                                                        </a>
                                                                                        <ul class="dropdown-menu dropdown-menu-end">
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit" class="dropdown-item"><i class="lni lni-pencil-alt"></i><span>Edit</span></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnkDel" runat="server" class="dropdown-item" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i><span>Delete</span></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_print" runat="server" class="dropdown-item" ToolTip="Print slip" CausesValidation="false" OnClick="lnk_print_Click"><i class="bx bx-printer"></i><span>Print Slip</span></asp:LinkButton>
                                                                                            </li>

                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_print_form" runat="server" class="dropdown-item" ToolTip="Generate Form" CausesValidation="false" OnClick="lnk_print_form_Click"><i class="bx bx-printer"></i><span>Generate Form</span></asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_transfer_to_admission" runat="server" class="dropdown-item" ToolTip="Generate Form" CausesValidation="false" OnClick="lnk_transfer_to_admission_Click"><i class="bx bx-plus-medical"></i><span>Make Admission</span></asp:LinkButton>
                                                                                            </li>

                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnk_adm_no" runat="server" class="dropdown-item" ToolTip="Generate Form" CausesValidation="false" OnClick="lnk_adm_no_Click"><i class="bx bx-plus-medical"></i><span>Update Admission No.</span></asp:LinkButton>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>






                                                                                    <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_cast" runat="server" Visible="false" Text='<%#Bind("cast")%>'></asp:Label>
                                                                                    <asp:Label ID="lbl_recpt_no" runat="server" Text='<%#Bind("recpt_no")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_Is_Generate" runat="server" Text='<%#Bind("Is_Generate")%>' Visible="false"></asp:Label>

                                                                                    <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_is_admission_no_updated" runat="server" Text='<%#Bind("Is_admission_no_updated")%>' Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_form_no" runat="server" Text='<%#Bind("Form_no")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_indesx_no" runat="server" Text='<%#Bind("form_indesx_no")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("Admission_no")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_student_name" runat="server" Text='<%#Bind("student_name")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_fathers_name" runat="server" Text='<%#Bind("fathers_name")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_gender" runat="server" Text='<%#Bind("gender")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_mobile" runat="server" Text='<%#Bind("mobile")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_adm_date" runat="server" Text='<%#Bind("Admission_date")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_amts" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_Mode")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_remarks" Style="word-break: break-all"
                                                                                        runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Exam_date" Style="word-break: break-all"
                                                                                        runat="server" Text='<%#Bind("Exam_date")%>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>




                                                            <div style="display: none">
                                                                <asp:GridView ID="grd_data_list" Visible="false" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" PageSize="100" AllowPaging="false" Font-Bold="False">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Form No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_form_no" runat="server" Text='<%#Bind("Form_no")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Index no.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_indesx_no" runat="server" Text='<%#Bind("form_indesx_no")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Admission no.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Admission_no")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Student Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_student_name" runat="server" Text='<%#Bind("student_name")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Fathers Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_fathers_name" runat="server" Text='<%#Bind("fathers_name")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Gender">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_gender" runat="server" Text='<%#Bind("gender")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Class">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobile">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_mobile" runat="server" Text='<%#Bind("mobile")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_amts" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_Mode")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Remark">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_remarks" Style="word-break: break-all"
                                                                                    runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>

                                                                                <asp:Label ID="lbl_recpt_no" runat="server" Visible="false" Text='<%#Bind("recpt_no")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>

                                                                    <FooterStyle BackColor="#CCCC99" />
                                                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <SelectedRowStyle BackColor="#EFEFEF" Font-Bold="True" ForeColor="#CC0000" />
                                                                    <HeaderStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#333333" Height="40px" />
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                </asp:GridView>
                                                            </div>
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
                    <!--end row-->
                </div>
            </div>
        </div>
    </div>



    <div class="modal fade" id="myModal" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Update Admission No.</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:Label ID="lbl_session_p" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Student Name</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:Label ID="lbl_std_name" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Amount</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:Label ID="lbl_amt_p" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Enter Admission No.</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_admission_no" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_update_adm" OnClick="btn_update_adm_Click" runat="server" Text="Submit" class="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_user_Type" runat="server" />


    <script type="text/javascript">
        function IsCharacter(e) {
            var charCode = (e.which) ? e.which : e.keyCode;
            if (!(charCode >= 65 && charCode <= 90) && !(charCode >= 97 && charCode <= 122) && (charCode != 32 && charCode != 8) && !(charCode == 9)) {
                return false;
            }
            if (e.which == 32) {
                console.log('Space Detected');
                return false;
            }
            return true;
        }



        function openModal() {
            $('#myModal').modal('show');

        }
    </script>

    <script>
        $(function () {
            $("#<%=txt_date_of_exam.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "2025:2027",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_dob.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });



    </script>


    <script type="text/javascript">
        $(document).ready(function () {
            on_payment_mode_selection();
            $("#<%=ddl_paymentmode.ClientID%>").on('change', function () {
                on_payment_mode_selection();
            })
        });

        function on_payment_mode_selection() {
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Select") {
                $("#pnl_mode_t_nS").hide();
                $("#bank_dt").hide();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Cash") {
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



</asp:Content>
