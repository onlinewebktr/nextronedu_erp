<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="student-payment-summary.aspx.cs" Inherits="school_web.Admin.student_payment_summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Payment Summary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="slip/payment-slip.css" rel="stylesheet" />
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
                        url: 'student-payment-summary.aspx/GetRooPath',
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
                <div class="breadcrumb-title pe-3">Fees Report<a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a></div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student Payment History</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
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
                                                    <div class="col-xl-6 padd-rght-5">
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <asp:Panel ID="std_basic_infoS" runat="server" Visible="false">

                <div class="print-btn-sec" style="float: right">
                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                </div>

                <div class="print-dv-bx-wprrr" id="tblPrintIQ" runat="server">
                    <div class="print-dv">
                        <div class="print-dv-inr">
                            <div class="print-dv-bx-wpr">
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
                                            <span style="font-size: 14px; font-weight: bold;">Date & Time -<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="print-dv-bx-wpr">
                                <h2 class="print-dv-bx-wpr-title-h">Student Basic Information</h2>
                                <div class="print-dv-bx-wpr-inr">

                                    <div class="print-dv-bx-wpr-inr-row">
                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Student Name : </label>
                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="lbl_name" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Father's Name : </label>

                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="lbl_father_name" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="print-dv-bx-wpr-inr-row">
                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Class : </label>
                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="lblclass" runat="server" Font-Bold="true" Text=" " class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>


                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Section : </label>
                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="txtsection" runat="server" Text=" " Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="print-dv-bx-wpr-inr-row">
                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Admission No. : </label>
                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="lbl_admission_no" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Type : </label>
                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="lbl_studentype" runat="server" Text=" " Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="print-dv-bx-wpr-inr-row">
                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Transportation : </label>
                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="lbltransporttion" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Contact no. : </label>
                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="lbl_phone" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="print-dv-bx-wpr-inr-row">
                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Hostel : </label>
                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="lblhostel" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Category : </label>
                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="lbl_catogery" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="print-dv-bx-wpr-inr-row">
                                        <div class="print-dv-bx-wpr-inr-row-50">
                                            <label for="validationCustom01" class="print-dv-bx-wpr-inr-p">Sub Category : </label>
                                            <div class="print-dv-bx-wpr-inr-txt-bx ">
                                                <asp:Label ID="lbl_subcatogery" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                                       <div class="print-dv-bx-wpr" runat="server" id="ledgeRDV" visible="false">
                                <h2 class="print-dv-bx-wpr-title-h">Ledger Details</h2>
                                <div class="print-dv-bx-wpr-inr">
                                    <table class="table-bordered" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Receipt No</th>
                                                <th>Receipt Date</th>
                                                <th>Payment Type</th>
                                                <th>Received Mode</th>
                                                <th>Remarks</th>
                                                <th>Total Paid</th>
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
                                                            <asp:Label ID="lbl_Pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no") %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Bind("Description") %>' Style="word-break: break-all"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_amount" runat="server" Font-Bold="true" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <tr>
                                                <td colspan="6" class="txtalign-right txtbolds">Total : </td>

                                                <td class="txtbolds">
                                                    <asp:Label ID="lbl_ttl_ledger_amt" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>


                            <div class="print-dv-bx-wpr" style="display:none">
                                <h2 class="print-dv-bx-wpr-title-h">Admission/Annual Payment Details</h2>
                                <div class="print-dv-bx-wpr-inr">
                                    <asp:Panel ID="adm_anul_tbls" runat="server">
                                        <table class="table-bordered" style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Fee Type</th>
                                                    <th>Amount</th>
                                                    <th>Disc. Amt.</th>
                                                    <th>Amt. after Disc.</th>
                                                    <th>Paid Amt.</th>
                                                    <th>Dues Amt.</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rp_adm_fees" runat="server" OnItemDataBound="rp_adm_fees_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                               <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Fee_type" runat="server" Text='<%#Bind("Fee_type")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_payple" runat="server" Text='<%#Bind("Total_payable")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_discount_amtbe" runat="server" Text='<%#Bind("Total_disc")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_amt_after_disc" runat="server" Text='<%#Bind("Total_Payable_after_disc")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_paid_amt" runat="server" Text='<%#Bind("Total_paid")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_dues_amt" runat="server" Text='<%#Bind("Total_dues")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <tr>
                                                    <td colspan="2" class="txtalign-right txtbolds">Total : </td>
                                                    <td class="txtbolds">
                                                        <asp:Label ID="lbl_ttl_payble_a" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td class="txtbolds">
                                                        <asp:Label ID="lbl_ttl_disc_a" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td class="txtbolds">
                                                        <asp:Label ID="lbl_ttl_after_disc_a" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td class="txtbolds">
                                                        <asp:Label ID="lbl_ttl_paid_amt_a" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                    <td class="txtbolds">
                                                        <asp:Label ID="lbl_ttl_dues_a" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                    <p runat="server" id="adm_anul_no_msgs" visible="false" class="print-dv-bx-wpr-inr-pss-msg">No any Admission/Annual fee taken from this student. </p>
                                </div>
                            </div>


                             <div class="print-dv-bx-wpr">
                                <h2 class="print-dv-bx-wpr-title-h">Admission/Annual Dues Details</h2>
                                <div class="print-dv-bx-wpr-inr">
                                     <table class="table-bordered" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Fee Type</th>
                                                <th>Amount</th>
                                                <th>Disc. Amt.</th>
                                                <th>Amt. after Disc.</th>
                                                <th>Paid Amt.</th>
                                                <th>Dues Amt.</th>
                                                 
                                            </tr>
                                        </thead>
                                        <tbody>
                                             <tr>
                                                    <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='1'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Fee_type" runat="server"  ></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_payple" runat="server"  ></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_discount_amtbe" runat="server"  ></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_amt_after_disc" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_paid_amt" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_dues_amt" runat="server" ></asp:Label>
                                                        </td>

                                                       
                                                    </tr>

                                         
                                        </tbody>
                                    </table>
                                    </div>
                                 </div>
                            


                            <div class="print-dv-bx-wpr">
                                <h2 class="print-dv-bx-wpr-title-h">Monthly Payment Details</h2>
                                <div class="print-dv-bx-wpr-inr">
                                    <table class="table-bordered" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Month</th>
                                                <th>Amount</th>
                                                <th>Disc. Amt.</th>
                                                <th>Amt. after Disc.</th>
                                                <th>Paid Amt.</th>
                                                <th>Dues Amt.</th>
                                                 <th>Date</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Bind("month")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_payple" runat="server" Text='<%#Bind("Total_payable")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_discount_amtbe" runat="server" Text='<%#Bind("Total_disc")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_amt_after_disc" runat="server" Text='<%#Bind("Total_Payable_after_disc")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_paid_amt" runat="server" Text='<%#Bind("Total_paid")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_dues_amt" runat="server" Text='<%#Bind("Total_dues")%>'></asp:Label>
                                                        </td>

                                                         <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <tr>
                                                <td colspan="2" class="txtalign-right txtbolds">Total : </td>
                                                <td class="txtbolds">
                                                    <asp:Label ID="lbl_ttl_payble" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="txtbolds">
                                                    <asp:Label ID="lbl_ttl_disc" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="txtbolds">
                                                    <asp:Label ID="lbl_ttl_after_disc" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="txtbolds">
                                                    <asp:Label ID="lbl_ttl_paid_amt" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="txtbolds">
                                                    <asp:Label ID="lbl_ttl_dues" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>



                            <div class="print-dv-bx-wpr">
                                <h2 class="print-dv-bx-wpr-title-h">Monthly Dues Details</h2>
                                <div class="print-dv-bx-wpr-inr">
                                    <table class="table-bordered" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Month</th>
                                                <th>Dues Amount</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rp_dues_fees" runat="server" OnItemDataBound="rp_dues_fees_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_dues_amount" runat="server" Text='<%#Bind("Total_Dues")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <td colspan="2" class="txtalign-right txtbolds">Total Dues Amount</td>
                                                <td class="txtbolds">
                                                    <asp:Label ID="lbl_total_dues" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>


                 


                            <div class="sig-left" runat="server" id="signDVS">
                                <div class="rght-sig-img-dv">
                                    <asp:Image ID="Image1" class="rght-sig-img" runat="server" />
                                </div>
                                <p class="sig-ps ng-binding">Accountant</p>
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>
        </div>
    </div>




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
                <div style="width: 100%; max-height: 400px; overflow: auto;">
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


    <style type="text/css">
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

        table tr td {
            padding: 10px 5px !important;
        }
    </style>
</asp:Content>
