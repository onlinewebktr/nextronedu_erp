<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Day_End_Report_Summery_N.aspx.cs" Inherits="school_web.Admin.Day_End_Report_Summery_N" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Day End Report Summery 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .home-grph-wpr {
            width: 114%;
            margin: 0px 0px 0px -50px;
        }

        .home-grph-wpr-smll {
            margin: 50px 0px 0px -50px;
        }

        .amountStyle {
            text-align: right;
        }

        .amountHStyle {
            text-align: right;
        }

        .amountFStyle {
            text-align: right;
        }

        .trbg {
            background: #ddd;
        }

        .amountStyleTTL {
            text-align: right;
            font-weight: 600;
            font-size: 13px;
        }


        .amountStyleAmt {
            text-align: right;
            font-weight: 600;
            font-size: 13px;
        }

        .lnkediT {
            font-size: 14px;
            background: #2b9300;
            padding: 2px 3px 1px 3px;
            color: #fff;
            border-radius: 3px;
            height: 20px;
            line-height: 20px;
        }

            .lnkediT:hover {
                color: #fff;
                background: #2b9300;
            }

        .lnkdeletE {
            font-size: 14px;
            background: #f00;
            padding: 2px 3px 1px 3px;
            color: #fff;
            border-radius: 3px;
            height: 20px;
            line-height: 20px;
            display: none;
        }

            .lnkdeletE:hover {
                color: #fff;
                background: #f00;
            }

        .lnkediTDSB {
            font-size: 14px;
            background: #cdcdcd;
            padding: 2px 3px 1px 3px;
            color: #fff;
            border-radius: 3px;
            height: 20px;
            line-height: 20px;
            pointer-events: none;
        }

            .lnkediTDSB:hover {
                color: #fff;
                background: #cdcdcd;
            }

        .lnkdeletEDSB {
            font-size: 14px;
            background: #cdcdcd;
            padding: 2px 3px 1px 3px;
            color: #fff;
            border-radius: 3px;
            height: 20px;
            line-height: 20px;
            pointer-events: none;
        }

            .lnkdeletEDSB:hover {
                color: #fff;
                background: #cdcdcd;
            }

        .actionth {
            width: 65px;
        }

        .modal {
            background: rgb(0 0 0 / 28%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translate(0, 0px);
            }

        .modal-header {
            padding: 7px 10px 7px 10px;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            width: 100%;
            float: left;
        }

        .displYnone {
            display: none;
            pointer-events: none;
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


    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function openModalReason() {
            $('#mdl_reasoN').modal('show');
        }
    </script>
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Day End Report Summery </li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <li><a href="Today_Collection.aspx">Day End Collection Report</a></li>
                        <li><a href="overall-collection-report.aspx">Head Wise Collection Report</a></li>
                        <%--<li><a href="day-end-report-typewise.aspx">Monthly Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-admission-fee.aspx">Admission Fee Collection Report</a></li>
                        <li><a href="day-end-report-of-annual-fee.aspx">Annual Fee Collection Report</a></li>--%>
                        <li><a href="day-end-report-of-form-sale.aspx">Form Sale Report</a></li>
                        <li><a href="day_End_Report_Summery_N.aspx" class="sub-mnu-p-a-active">Day End Summary</a></li>
                        <%--<li><a href="day-end-report-summery-headwise.aspx">Day End Summary Headwise</a></li>
                        <li><a href="Fee_Collection_Report.aspx">Fees Collection Summary</a></li>--%>
                       <%-- <li><a href="userwise-payment-collection-report.aspx">User Wise Collection Report</a></li>--%>
                    </ul>
                </div>
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2" style="display: none">
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                        <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                        <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>

                                                    <div class="col-sm-3" style="display: none">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click1" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="grd-wpr">
                                                <div class="col-sm-12">
                                                    <div id="tblPrintIQ" runat="server">
                                                        <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">
                                                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                                                <div class="print-dv-bx-wpr">
                                                                    <div class="head-printdv" style="border-bottom: 1px solid #ddd; margin: 2px 0px 0px 0px; float: left; width: 100%;">
                                                                        <%--<div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                            <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px; display: none" />
                                                                        </div>--%>
                                                                        <div style="margin: 0px; padding: 0px; height: 110px; width: 100%; float: left;">
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
                                                                        </div>
                                                                    </div>
                                                                </div>



                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td colspan="4" style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 34px; width: 100%; font-family: Arial; font-size: 18px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">DAY END REPORT SUMMERY -
                                                                    <asp:Label ID="lbl_reportdate" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div class="pageheights">
                                                                    <div style="margin: 5px 0px 0px 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                        COLLECTION REPORT FOR OVER ALL CASH 
                                                                    </div>
                                                                    <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                        <table class="table table-bordered">
                                                                            <tr>
                                                                                <th>SR.</th>
                                                                                <th>SESSION</th>
                                                                                <th>COLLECTION TYPE</th>
                                                                                <th style="text-align: center">AMOUNT IN (CASH)</th>
                                                                            </tr>

                                                                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_parameter_New" Style="font-weight: 600" runat="server" Text='<%#Bind("parameter_New")%>'></asp:Label>
                                                                                            (<asp:Label ID="Label2" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>)
                                                                                        </td>
                                                                                        <td style="text-align: right;">
                                                                                            <asp:Label ID="lbl_sequence" Visible="false" runat="server" Text='<%#Bind("Sequence")%>'></asp:Label>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("AmountCash")%>'></asp:Label>
                                                                                        </td>
                                                                                        <%--<td style="text-align: right;">
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("AmountCash")%>'></asp:Label>
                                                                                    </td>--%>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>

                                                                            <tr style="background: #ddd;">
                                                                                <td class="graybg" colspan="3" style="text-align: right"><b>TOTAL COLLECTION</b></td>
                                                                                <td class="graybg" style="text-align: right">
                                                                                    <asp:Label ID="lbl_ttl_cash" Style="font-weight: 600" runat="server"></asp:Label></td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td colspan="3" style="text-align: right"><b>(-)TOTAL EXPENSES</b></td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lbl_exp_cash" Style="font-weight: 600" runat="server"></asp:Label></td>

                                                                            </tr>

                                                                            <tr style="background: #ddd;">
                                                                                <td class="graybg" colspan="3" style="text-align: right"><b>CASH IN HAND</b></td>

                                                                                <td class="graybg" style="text-align: right">
                                                                                    <asp:Label ID="lbl_ttl_blnce_cash" Style="font-weight: 600" runat="server"></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>






                                                                    <div style="margin: 5px 0px 0px 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                        COLLECTION REPORT FOR OVER ALL CHEQUE 
                                                                    </div>
                                                                    <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                        <table class="table table-bordered">
                                                                            <tr>
                                                                                <th>SR.</th>
                                                                                <th>SESSION</th>
                                                                                <th>COLLECTION TYPE</th>
                                                                                <th style="text-align: center">AMOUNT IN (CHEQUE)</th>
                                                                            </tr>

                                                                            <asp:Repeater ID="rd_view_cheque" runat="server" OnItemDataBound="rd_view_cheque_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_parameter_New" Style="font-weight: 600" runat="server" Text='<%#Bind("parameter_New")%>'></asp:Label>
                                                                                            (<asp:Label ID="Label2" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>)
                                                                                        </td>
                                                                                        <td style="text-align: right;">
                                                                                            <asp:Label ID="lbl_sequence" Visible="false" runat="server" Text='<%#Bind("Sequence")%>'></asp:Label>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("AmountCash")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>

                                                                            <tr style="background: #ddd;">
                                                                                <td class="graybg" colspan="3" style="text-align: right"><b>TOTAL COLLECTION</b></td>
                                                                                <td class="graybg" style="text-align: right">
                                                                                    <asp:Label ID="lbl_ttl_cheque" Style="font-weight: 600" runat="server"></asp:Label></td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td colspan="3" style="text-align: right"><b>(-)TOTAL EXPENSES</b></td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lbl_exp_cheque" Style="font-weight: 600" runat="server"></asp:Label></td>
                                                                            </tr>

                                                                            <tr style="background: #ddd;">
                                                                                <td class="graybg" colspan="3" style="text-align: right"><b>DEPOSIT IN BANK</b></td>

                                                                                <td class="graybg" style="text-align: right">
                                                                                    <asp:Label ID="lbl_ttl_blnce_cheque" Style="font-weight: 600" runat="server"></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>





                                                                    <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                        COLLECTION REPORT FOR OVER ALL ONLINE 
                                                                    </div>
                                                                    <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                        <table class="table table-bordered">
                                                                            <tr>
                                                                                <th>SR.</th>
                                                                                <th>SESSION</th>
                                                                                <th>COLLECTION TYPE</th>
                                                                                <th style="text-align: center">AMOUNT IN (ONLINE)</th>
                                                                            </tr>

                                                                            <asp:Repeater ID="rd_overall_online" runat="server" OnItemDataBound="rd_overall_online_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_parameter_New" Style="font-weight: 600" runat="server" Text='<%#Bind("parameter_New")%>'></asp:Label>
                                                                                            (<asp:Label ID="Label2" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>)
                                                                                        </td>
                                                                                        <td style="text-align: right;">
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("AmountCash")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>

                                                                            <tr style="background: #ddd">
                                                                                <td class="graybg" colspan="3" style="text-align: right"><b>TOTAL COLLECTION</b></td>
                                                                                <td class="graybg" style="text-align: right">
                                                                                    <asp:Label ID="lbl_ttl_online" Style="font-weight: 600" runat="server"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3" style="text-align: right"><b>(-)TOTAL EXPENSES</b></td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lbl_exp_online" Style="font-weight: 600" runat="server"></asp:Label></td>
                                                                            </tr>

                                                                            <tr style="background: #ddd">
                                                                                <td class="graybg" colspan="3" style="text-align: right"><b>DEPOSIT IN BANK</b></td>

                                                                                <td class="graybg" style="text-align: right">
                                                                                    <asp:Label ID="lbl_ttl_blnce_onlne" Style="font-weight: 600" runat="server"></asp:Label></td>
                                                                            </tr>



                                                                            <tr style="background: #94f17d">
                                                                                <td class="greenbg" colspan="3" style="text-align: right"><b>BALANCE AMOUNT</b></td>

                                                                                <td class="greenbg" style="text-align: right">
                                                                                    <asp:Label ID="lbl_balance_amount" Style="font-weight: 600" runat="server"></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>


                                                                <%--ADMISSION--%>
                                                                <asp:Panel ID="pnl_admission_day_scholar" runat="server">

                                                                    <div style="margin: 5px 0px 5px 0px; padding: 5px 0px 5px 0px; float: left; height: 31px; width: 100%; font-family: Arial; font-size: 16px; text-align: center; background-color: #fff; font-weight: bold; color: #000;">
                                                                        NEW ADMISSION FEE (Day Scholar)
                                                                    </div>


                                                                    <asp:Panel ID="pnl_admission_day_scholar_cash" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            NEW ADMISSION FEE (CASH) 
                                                                        </div>
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">

                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>User</th>
                                                                                    <th runat="server" visible="false" class="noPrint" style="display: none">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_admission_day_boarding" runat="server" OnItemDataBound="rp_view_admission_day_boarding_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth" style="display: none">
                                                                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClick="lnkDel_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="10" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_ttl_adm_day_boarding_amt_cash" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_admission_day_scholar_online" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            NEW ADMISSION FEE (ONLINE)    
                                                                        </div>
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>User</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_admission_day_boarding_online" runat="server" OnItemDataBound="rp_view_admission_day_boarding_online_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEditOnline" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEditOnline_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDelOnline" runat="server" ToolTip="Delete" OnClick="lnkDelOnline_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_ttl_adm_day_boarding_amt_online" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_admission_day_scholar_cheque" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            NEW ADMISSION FEE (CHEQUE)    
                                                                        </div>
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>User</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_admission_day_boarding_cheque" runat="server" OnItemDataBound="rp_view_admission_day_boarding_cheque_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEditCheque" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEditCheque_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDelCheque" runat="server" ToolTip="Delete" OnClick="lnkDelCheque_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="10" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_ttl_adm_day_boarding_amt_cheque" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>

                                                                </asp:Panel>

                                                                <%--ANNUAL--%>
                                                                <asp:Panel ID="pnl_annual_day_scholar" runat="server" Visible="false">
                                                                    <div style="margin: 5px 0px 5px 0px; padding: 5px 0px 5px 0px; float: left; height: 31px; width: 100%; font-family: Arial; font-size: 16px; text-align: center; background-color: #fff; font-weight: bold; color: #000;">
                                                                        READMISSION FEE (Day Scholar)
                                                                    </div>


                                                                    <asp:Panel ID="pnl_annual_day_scholar_cash" runat="server">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            READMISSION FEE (CASH) 
                                                                        </div>

                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>User</th>
                                                                                    <th runat="server" visible="false" class="actionth displYnone">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_annual_day_boarding_cash" runat="server" OnItemDataBound="rp_view_annual_day_boarding_cash_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth displYnone">
                                                                                                <asp:LinkButton ID="lnkEdit001" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit001_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel001" runat="server" ToolTip="Delete" OnClick="lnkDel001_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="10" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_annual_day_boarding_cash" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_annual_day_scholar_online" runat="server">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            READMISSION FEE (ONLINE) 
                                                                        </div>

                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>User</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_annual_day_boarding_online" runat="server" OnItemDataBound="rp_view_annual_day_boarding_online_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label5" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit002" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit002_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel002" runat="server" ToolTip="Delete" OnClick="lnkDel002_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_annual_day_boarding_online" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grid_view_annual_day_boarding_online" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_annual_day_boarding_online_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" runat="server" Style="display: block; width: 70px;" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ADM. NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SECTION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE ">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>



                                                                    <asp:Panel ID="pnl_annual_day_scholar_cheque" runat="server">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            READMISSION FEE (CHEQUE) 
                                                                        </div>

                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">

                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>User</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_annual_day_boarding_cheque" runat="server" OnItemDataBound="rp_view_annual_day_boarding_cheque_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label7" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit003" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit003_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel003" runat="server" ToolTip="Delete" OnClick="lnkDel003_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_annual_day_boarding_cheque" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>


                                                                            <%--<asp:GridView ID="grid_view_annual_day_boarding_cheque" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_annual_day_boarding_cheque_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" runat="server" Style="display: block; width: 70px;" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ADM. NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SECTION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE ">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </asp:Panel>

                                                                <%--MONTHLY--%>
                                                                <asp:Panel ID="pnl_monthly_day_scholar" runat="server">
                                                                    <div style="margin: 5px 0px 5px 0px; padding: 5px 0px 5px 0px; float: left; height: 31px; width: 100%; font-family: Arial; font-size: 16px; text-align: center; background-color: #fff; font-weight: bold; color: #000;">
                                                                        TUITION FEE (Day Scholar)
                                                                    </div>

                                                                    <asp:Panel ID="pnl_monthly_day_scholar_cash" runat="server">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            TUITION FEE (CASH) 
                                                                        </div>

                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>SR.</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>User</th>
                                                                                    <th runat="server" visible="false" class="actionth displYnone">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_monthly_day_boarding_cash" runat="server" OnItemDataBound="rp_view_monthly_day_boarding_cash_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth displYnone">
                                                                                                <asp:LinkButton ID="lnkEdit004" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit004_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel004" runat="server" ToolTip="Delete" OnClick="lnkDel004_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="10" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_monthly_day_boarding_cash" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grid_view_monthly_day_boarding_cash" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_monthly_day_boarding_cash_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" runat="server" Style="display: block; width: 70px;" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ADM. NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SEC.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE ">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_monthly_day_scholar_online" runat="server">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            TUITION FEE (ONLINE) 
                                                                        </div>


                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>SR.</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>User</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_monthly_day_boarding_online" runat="server" OnItemDataBound="rp_view_monthly_day_boarding_online_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label8" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit005" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit005_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel005" runat="server" ToolTip="Delete" OnClick="lnkDel005_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_monthly_day_boarding_online" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grid_view_monthly_day_boarding_online" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_monthly_day_boarding_online_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" runat="server" Style="display: block; width: 70px;" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ADM. NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SEC.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_monthly_day_scholar_cheque" runat="server">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            TUITION FEE (CHEQUE) 
                                                                        </div>


                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>SR.</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>User</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_monthly_day_boarding_cheque" runat="server" OnItemDataBound="rp_view_monthly_day_boarding_cheque_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label9" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit006" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit006_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel006" runat="server" ToolTip="Delete" OnClick="lnkDel006_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_monthly_day_boarding_cheque" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grid_view_monthly_day_boarding_cheque" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_monthly_day_boarding_cheque_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" runat="server" Style="display: block; width: 70px;" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ADM. NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SEC.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </asp:Panel>




                                                                <asp:Panel ID="Panel1" runat="server" Visible="false">

                                                                    <asp:Panel ID="pnl_admission_hostel" runat="server" Visible="false">
                                                                        <div style="margin: 5px 0px 5px 0px; padding: 5px 0px 5px 0px; float: left; height: 31px; width: 100%; font-family: Arial; font-size: 16px; text-align: center; background-color: #fff; font-weight: bold; color: #000;">
                                                                            NEW ADMISSION (Hostel)
                                                                        </div>

                                                                        <asp:Panel ID="pnl_admission_hostel_cash" runat="server" Visible="false">
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                                NEW ADMISSION (CASH) 
                                                                            </div>

                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                                <table class="table table-striped table-bordered">
                                                                                    <tr>
                                                                                        <th>SR.</th>
                                                                                        <th>SESSION</th>
                                                                                        <th>DATE</th>
                                                                                        <th>ADM. NO.</th>
                                                                                        <th>STUDENT NAME</th>
                                                                                        <th>CLASS</th>
                                                                                        <th>SEC.</th>
                                                                                        <th>ROLL</th>
                                                                                        <th>RECIPT NO.</th>
                                                                                        <th>MODE</th>
                                                                                        <th>AMOUNT</th>
                                                                                        <th>User</th>
                                                                                        <th runat="server" visible="false" class="actionth">Action</th>
                                                                                    </tr>

                                                                                    <asp:Repeater ID="rp_view_admission_hostal" runat="server" OnItemDataBound="rp_view_admission_hostal_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td class="amountStyle">
                                                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td runat="server" visible="false" class="actionth">
                                                                                                    <asp:LinkButton ID="lnkEdit007" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit007_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkDel007" runat="server" ToolTip="Delete" OnClick="lnkDel007_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <tr class="trbg">
                                                                                        <td colspan="10" class="amountStyleTTL">TOTAL</td>
                                                                                        <td class="amountStyleAmt">
                                                                                            <asp:Label ID="lbl_admission_hostal_cash" runat="server"></asp:Label></td>
                                                                                        <td colspan="2"></td>
                                                                                    </tr>
                                                                                </table>

                                                                                <%--<asp:GridView ID="grid_view_admission_hostal" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_admission_hostal_RowDataBound" ShowFooter="True">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SR.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SESSION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="ADM. NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CLASS">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="SEC.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="ROLL">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="MODE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="User">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>--%>
                                                                            </div>
                                                                        </asp:Panel>


                                                                        <asp:Panel ID="pnl_admission_hostel_online" runat="server" Visible="false">
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                                NEW ADMISSION (ONLINE)  
                                                                            </div>


                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                                <table class="table table-striped table-bordered">
                                                                                    <tr>
                                                                                        <th>SR.</th>
                                                                                        <th>SESSION</th>
                                                                                        <th>DATE</th>
                                                                                        <th>ADM. NO.</th>
                                                                                        <th>STUDENT NAME</th>
                                                                                        <th>CLASS</th>
                                                                                        <th>SEC.</th>
                                                                                        <th>ROLL</th>
                                                                                        <th>RECIPT NO.</th>
                                                                                        <th>MODE</th>
                                                                                        <th>TRANSACTION ID</th>
                                                                                        <th>AMOUNT</th>
                                                                                        <th>User</th>
                                                                                        <th runat="server" visible="false" class="actionth">Action</th>
                                                                                    </tr>

                                                                                    <asp:Repeater ID="rp_view_admission_hostal_online" runat="server" OnItemDataBound="rp_view_admission_hostal_online_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label10" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td class="amountStyle">
                                                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td runat="server" visible="false" class="actionth">
                                                                                                    <asp:LinkButton ID="lnkEdit008" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit008_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkDel008" runat="server" ToolTip="Delete" OnClick="lnkDel008_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <tr class="trbg">
                                                                                        <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                        <td class="amountStyleAmt">
                                                                                            <asp:Label ID="lbl_admission_hostal_online" runat="server"></asp:Label></td>
                                                                                        <td colspan="2"></td>
                                                                                    </tr>
                                                                                </table>


                                                                                <%--<asp:GridView ID="grid_view_admission_hostal_online" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_admission_hostal_online_RowDataBound" ShowFooter="True">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SR.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SESSION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ADM. NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CLASS">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="SEC.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="ROLL">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="MODE ">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="User">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                    </Columns>
                                                                                </asp:GridView>--%>
                                                                            </div>
                                                                        </asp:Panel>


                                                                        <asp:Panel ID="pnl_admission_hostel_cheque" runat="server" Visible="false">
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                                NEW ADMISSION (CHEQUE)  
                                                                            </div>


                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                                <table class="table table-striped table-bordered">
                                                                                    <tr>
                                                                                        <th>SR.</th>
                                                                                        <th>SESSION</th>
                                                                                        <th>DATE</th>
                                                                                        <th>ADM. NO.</th>
                                                                                        <th>STUDENT NAME</th>
                                                                                        <th>CLASS</th>
                                                                                        <th>SEC.</th>
                                                                                        <th>ROLL</th>
                                                                                        <th>RECIPT NO.</th>
                                                                                        <th>MODE</th>
                                                                                        <th>TRANSACTION ID</th>
                                                                                        <th>AMOUNT</th>
                                                                                        <th>User</th>
                                                                                        <th runat="server" visible="false" class="actionth">Action</th>
                                                                                    </tr>

                                                                                    <asp:Repeater ID="rp_view_admission_hostal_cheque" runat="server" OnItemDataBound="rp_view_admission_hostal_cheque_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label11" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td class="amountStyle">
                                                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td runat="server" visible="false" class="actionth">
                                                                                                    <asp:LinkButton ID="lnkEdit009" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit009_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkDel009" runat="server" ToolTip="Delete" OnClick="lnkDel009_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <tr class="trbg">
                                                                                        <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                        <td class="amountStyleAmt">
                                                                                            <asp:Label ID="lbl_admission_hostal_cheque" runat="server"></asp:Label></td>
                                                                                        <td colspan="2"></td>
                                                                                    </tr>
                                                                                </table>


                                                                                <%--<asp:GridView ID="grid_view_admission_hostal_cheque" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_admission_hostal_cheque_RowDataBound" ShowFooter="True">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SR.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SESSION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ADM. NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CLASS">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="SEC.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="ROLL">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="MODE ">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="User">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                    </Columns>
                                                                                </asp:GridView>--%>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_annual_hostel" runat="server" Visible="false">
                                                                        <div style="margin: 5px 0px 5px 0px; padding: 5px 0px 5px 0px; float: left; height: 31px; width: 100%; font-family: Arial; font-size: 16px; text-align: center; background-color: #fff; font-weight: bold; color: #000;">
                                                                            READMISSION (Hostel)
                                                                        </div>

                                                                        <asp:Panel ID="pnl_annual_hostel_cash" runat="server" Visible="false">
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                                READMISSION FEE (CASH) 
                                                                            </div>

                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                                <table class="table table-striped table-bordered">
                                                                                    <tr>
                                                                                        <th>SR.</th>
                                                                                        <th>SESSION</th>
                                                                                        <th>DATE</th>
                                                                                        <th>ADM. NO.</th>
                                                                                        <th>STUDENT NAME</th>
                                                                                        <th>CLASS</th>
                                                                                        <th>SEC.</th>
                                                                                        <th>ROLL</th>
                                                                                        <th>RECIPT NO.</th>
                                                                                        <th>MODE</th>
                                                                                        <th>AMOUNT</th>
                                                                                        <th>User</th>
                                                                                        <th runat="server" visible="false" class="actionth">Action</th>
                                                                                    </tr>

                                                                                    <asp:Repeater ID="rp_view_annul_fee_hostal_cash" runat="server" OnItemDataBound="rp_view_annul_fee_hostal_cash_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td class="amountStyle">
                                                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td runat="server" visible="false" class="actionth">
                                                                                                    <asp:LinkButton ID="lnkEdit010" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit010_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkDel010" runat="server" ToolTip="Delete" OnClick="lnkDel010_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <tr class="trbg">
                                                                                        <td colspan="10" class="amountStyleTTL">TOTAL</td>
                                                                                        <td class="amountStyleAmt">
                                                                                            <asp:Label ID="lbl_annul_fee_hostal_cash" runat="server"></asp:Label></td>
                                                                                        <td colspan="2"></td>
                                                                                    </tr>
                                                                                </table>
                                                                                <%--<asp:GridView ID="grid_view_annul_fee_hostal_cash" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_annul_fee_hostal_cash_RowDataBound" ShowFooter="True">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SR.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SESSION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ADM. NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CLASS">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="SEC.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="ROLL">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="MODE ">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="User">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                    </Columns>
                                                                                </asp:GridView>--%>
                                                                            </div>
                                                                        </asp:Panel>


                                                                        <asp:Panel ID="pnl_annual_hostel_online" runat="server" Visible="false">
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                                READMISSION FEE  (ONLINE) 
                                                                            </div>


                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                                <table class="table table-striped table-bordered">
                                                                                    <tr>
                                                                                        <th>SR.</th>
                                                                                        <th>SESSION</th>
                                                                                        <th>DATE</th>
                                                                                        <th>ADM. NO.</th>
                                                                                        <th>STUDENT NAME</th>
                                                                                        <th>CLASS</th>
                                                                                        <th>SEC.</th>
                                                                                        <th>ROLL</th>
                                                                                        <th>RECIPT NO.</th>
                                                                                        <th>MODE</th>
                                                                                        <th>TRANSACTION ID</th>
                                                                                        <th>AMOUNT</th>
                                                                                        <th>User</th>
                                                                                        <th runat="server" visible="false" class="actionth">Action</th>
                                                                                    </tr>

                                                                                    <asp:Repeater ID="rp_view_annul_fee_hostal_online" runat="server" OnItemDataBound="rp_view_annul_fee_hostal_online_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label12" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td class="amountStyle">
                                                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td runat="server" visible="false" class="actionth">
                                                                                                    <asp:LinkButton ID="lnkEdit011" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit011_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkDel011" runat="server" ToolTip="Delete" OnClick="lnkDel011_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <tr class="trbg">
                                                                                        <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                        <td class="amountStyleAmt">
                                                                                            <asp:Label ID="lbl_annul_fee_hostal_online" runat="server"></asp:Label></td>
                                                                                        <td colspan="2"></td>
                                                                                    </tr>
                                                                                </table>
                                                                                <%--<asp:GridView ID="grid_view_annul_fee_hostal_online" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_annul_fee_hostal_online_RowDataBound" AllowSorting="True" ShowFooter="True">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SR.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SESSION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ADM. NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CLASS">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="SEC.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="ROLL">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="MODE ">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="User">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>--%>
                                                                            </div>
                                                                        </asp:Panel>


                                                                        <asp:Panel ID="pnl_annual_hostel_cheque" runat="server" Visible="false">
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                                READMISSION FEE  (CHEQUE) 
                                                                            </div>


                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                                <table class="table table-striped table-bordered">
                                                                                    <tr>
                                                                                        <th>SR.</th>
                                                                                        <th>SESSION</th>
                                                                                        <th>DATE</th>
                                                                                        <th>ADM. NO.</th>
                                                                                        <th>STUDENT NAME</th>
                                                                                        <th>CLASS</th>
                                                                                        <th>SEC.</th>
                                                                                        <th>ROLL</th>
                                                                                        <th>RECIPT NO.</th>
                                                                                        <th>MODE</th>
                                                                                        <th>TRANSACTION ID</th>
                                                                                        <th>AMOUNT</th>
                                                                                        <th>User</th>
                                                                                        <th runat="server" visible="false" class="actionth">Action</th>
                                                                                    </tr>

                                                                                    <asp:Repeater ID="rp_view_annul_fee_hostal_cheque" runat="server" OnItemDataBound="rp_view_annul_fee_hostal_cheque_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label13" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td class="amountStyle">
                                                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td runat="server" visible="false" class="actionth">
                                                                                                    <asp:LinkButton ID="lnkEdit012" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit012_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkDel012" runat="server" ToolTip="Delete" OnClick="lnkDel012_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <tr class="trbg">
                                                                                        <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                        <td class="amountStyleAmt">
                                                                                            <asp:Label ID="lbl_annul_fee_hostal_cheque" runat="server"></asp:Label></td>
                                                                                        <td colspan="2"></td>
                                                                                    </tr>
                                                                                </table>

                                                                                <%--<asp:GridView ID="grid_view_annul_fee_hostal_cheque" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_annul_fee_hostal_cheque_RowDataBound" AllowSorting="True" ShowFooter="True">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SR.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SESSION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ADM. NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CLASS">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="SEC.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="ROLL">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="MODE ">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="User">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>--%>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_monthly_hostel" runat="server" Visible="false">
                                                                        <div style="margin: 5px 0px 5px 0px; padding: 5px 0px 5px 0px; float: left; height: 31px; width: 100%; font-family: Arial; font-size: 16px; text-align: center; background-color: #fff; font-weight: bold; color: #000;">
                                                                            TUITION FEE (Hostel)
                                                                        </div>


                                                                        <asp:Panel ID="pnl_monthly_hostel_cash" runat="server" Visible="false">
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                                TUITION FEE (CASH) 
                                                                            </div>
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                                <table class="table table-striped table-bordered">
                                                                                    <tr>
                                                                                        <th>SR.</th>
                                                                                        <th>SESSION</th>
                                                                                        <th>DATE</th>
                                                                                        <th>ADM. NO.</th>
                                                                                        <th>STUDENT NAME</th>
                                                                                        <th>CLASS</th>
                                                                                        <th>SEC.</th>
                                                                                        <th>ROLL</th>
                                                                                        <th>RECIPT NO.</th>
                                                                                        <th>MODE</th>
                                                                                        <th>AMOUNT</th>
                                                                                        <th>User</th>
                                                                                        <th runat="server" visible="false" class="actionth">Action</th>
                                                                                    </tr>

                                                                                    <asp:Repeater ID="rp_view_monthley_fee_hostal_cash" runat="server" OnItemDataBound="rp_view_monthley_fee_hostal_cash_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td class="amountStyle">
                                                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td runat="server" visible="false" class="actionth">
                                                                                                    <asp:LinkButton ID="lnkEdit013" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit013_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkDel013" runat="server" ToolTip="Delete" OnClick="lnkDel013_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <tr class="trbg">
                                                                                        <td colspan="10" class="amountStyleTTL">TOTAL</td>
                                                                                        <td class="amountStyleAmt">
                                                                                            <asp:Label ID="lbl_monthley_fee_hostal_cash" runat="server"></asp:Label></td>
                                                                                        <td colspan="2"></td>
                                                                                    </tr>
                                                                                </table>
                                                                                <%--<asp:GridView ID="grid_view_monthley_fee_hostal_cash" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_monthley_fee_hostal_cash_RowDataBound" ShowFooter="True">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SR.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SESSION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ADM. NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CLASS">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="SEC.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="ROLL">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="MODE ">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="User">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>--%>
                                                                            </div>
                                                                        </asp:Panel>


                                                                        <asp:Panel ID="pnl_monthly_hostel_online" runat="server" Visible="false">
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                                TUITION FEE (ONLINE) 
                                                                            </div>
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                                <table class="table table-striped table-bordered">
                                                                                    <tr>
                                                                                        <th>SR.</th>
                                                                                        <th>SESSION</th>
                                                                                        <th>DATE</th>
                                                                                        <th>ADM. NO.</th>
                                                                                        <th>STUDENT NAME</th>
                                                                                        <th>CLASS</th>
                                                                                        <th>SEC.</th>
                                                                                        <th>ROLL</th>
                                                                                        <th>RECIPT NO.</th>
                                                                                        <th>MODE</th>
                                                                                        <th>TRANSACTION ID</th>
                                                                                        <th>AMOUNT</th>
                                                                                        <th>User</th>
                                                                                        <th runat="server" visible="false" class="actionth">Action</th>
                                                                                    </tr>

                                                                                    <asp:Repeater ID="rp_view_monthley_fee_hostal_online" runat="server" OnItemDataBound="rp_view_monthley_fee_hostal_online_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label14" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td class="amountStyle">
                                                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td runat="server" visible="false" class="actionth">
                                                                                                    <asp:LinkButton ID="lnkEdit014" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit014_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkDel014" runat="server" ToolTip="Delete" OnClick="lnkDel014_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <tr class="trbg">
                                                                                        <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                        <td class="amountStyleAmt">
                                                                                            <asp:Label ID="lbl_monthley_fee_hostal_online" runat="server"></asp:Label></td>
                                                                                        <td colspan="2"></td>
                                                                                    </tr>
                                                                                </table>


                                                                                <%--<asp:GridView ID="grid_view_monthley_fee_hostal_online" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_monthley_fee_hostal_online_RowDataBound" ShowFooter="True">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SR.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SESSION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ADM. NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CLASS">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="SEC.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="ROLL">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="MODE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="User">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>--%>
                                                                            </div>
                                                                        </asp:Panel>


                                                                        <asp:Panel ID="pnl_monthly_hostel_cheque" runat="server" Visible="false">
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                                TUITION FEE (CHEQUE) 
                                                                            </div>
                                                                            <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                                <table class="table table-striped table-bordered">
                                                                                    <tr>
                                                                                        <th>SR.</th>
                                                                                        <th>SESSION</th>
                                                                                        <th>DATE</th>
                                                                                        <th>ADM. NO.</th>
                                                                                        <th>STUDENT NAME</th>
                                                                                        <th>CLASS</th>
                                                                                        <th>SEC.</th>
                                                                                        <th>ROLL</th>
                                                                                        <th>RECIPT NO.</th>
                                                                                        <th>MODE</th>
                                                                                        <th>TRANSACTION ID</th>
                                                                                        <th>AMOUNT</th>
                                                                                        <th>User</th>
                                                                                        <th runat="server" visible="false" class="actionth">Action</th>
                                                                                    </tr>

                                                                                    <asp:Repeater ID="rp_view_monthley_fee_hostal_cheque" runat="server" OnItemDataBound="rp_view_monthley_fee_hostal_cheque_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label15" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td class="amountStyle">
                                                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td runat="server" visible="false" class="actionth">
                                                                                                    <asp:LinkButton ID="lnkEdit015" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit015_Click"> <i class="bx bxs-edit"> </i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkDel015" runat="server" ToolTip="Delete" OnClick="lnkDel015_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                                    <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>' Visible="false"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <tr class="trbg">
                                                                                        <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                        <td class="amountStyleAmt">
                                                                                            <asp:Label ID="lbl_monthley_fee_hostal_cheque" runat="server"></asp:Label></td>
                                                                                        <td colspan="2"></td>
                                                                                    </tr>
                                                                                </table>
                                                                                <%--<asp:GridView ID="grid_view_monthley_fee_hostal_cheque" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_monthley_fee_hostal_cheque_RowDataBound" ShowFooter="True">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SR.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SESSION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ADM. NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CLASS">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="SEC.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="ROLL">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="MODE">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="User">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>--%>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </asp:Panel>

                                                                </asp:Panel>




                                                                <asp:Panel ID="pnl_form_sale" runat="server" Visible="false">
                                                                    <div style="margin: 5px 0px 5px 0px; padding: 5px 0px 5px 0px; float: left; height: 31px; width: 100%; font-family: Arial; font-size: 16px; text-align: center; background-color: #fff; font-weight: bold; color: #000;">
                                                                        ADMISSION FORM SALE
                                                                    </div>
                                                                    <asp:Panel ID="pnl_form_sale_cash" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            ADMISSION FORM FEE (CASH) 
                                                                        </div>
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>FORM NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_form_Sale_cash" runat="server" OnItemDataBound="rp_view_form_Sale_cash_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("Form_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("student_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("roll_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("recpt_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_Mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit016" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit016_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel016" runat="server" ToolTip="Delete" OnClick="lnkDel016_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Transaction_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="10" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_total_form_sale_cash" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grid_view_form_Sale_cash" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_form_Sale_cash_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="FORM NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Form_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("student_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SEC.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("roll_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("recpt_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE ">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_Mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_form_sale_online" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            ADMISSION FORM FEE (ONLINE) 
                                                                        </div>
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>FORM NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_form_Sale_online" runat="server" OnItemDataBound="rp_view_form_Sale_online_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("Form_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("student_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("roll_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("recpt_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_Mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label16" runat="server" Text='<%#Bind("Transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit017" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit017_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel017" runat="server" ToolTip="Delete" OnClick="lnkDel017_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Transaction_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_total_form_sale_online" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grid_view_form_Sale_online" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_form_Sale_online_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="FORM NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Form_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("student_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SEC.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("roll_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("recpt_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE ">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_Mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_form_sale_cheque" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            ADMISSION FORM FEE (CHEQUE) 
                                                                        </div>
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>FORM NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_form_Sale_cheque" runat="server" OnItemDataBound="rp_view_form_Sale_cheque_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("Form_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("student_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("roll_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("recpt_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_Mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label16" runat="server" Text='<%#Bind("Transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit018" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit018_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel018" runat="server" ToolTip="Delete" OnClick="lnkDel018_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Transaction_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_total_form_sale_cheque" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grid_view_form_Sale_cheque" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_form_Sale_cheque_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="FORM NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Form_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("student_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SEC.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("roll_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("recpt_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE ">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_Mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </asp:Panel>


                                                                <asp:Panel ID="pnl_other_fees" runat="server" Visible="false">
                                                                    <div style="margin: 5px 0px 5px 0px; padding: 5px 0px 5px 0px; float: left; height: 31px; width: 100%; font-family: Arial; font-size: 16px; text-align: center; background-color: #fff; font-weight: bold; color: #000;">
                                                                        OTHER FEE
                                                                    </div>


                                                                    <asp:Panel ID="pnl_other_fees_cash" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            OTHER FEE (CASH) 
                                                                        </div>
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_other_fee" runat="server" OnItemDataBound="rp_view_other_fee_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Payment_date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slipid")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Content_Fee")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit019" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit019_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel019" runat="server" ToolTip="Delete" OnClick="lnkDel019_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Transaction_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="10" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_total_other_fee_cash" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grid_view_other_fee" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_other_fee_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Payment_date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ADM. NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SEC.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slipid")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE ">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Content_Fee")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>

                                                                    <asp:Panel ID="pnl_other_fees_online" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            OTHER FEE (ONLINE) 
                                                                        </div>

                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_other_fee_online" runat="server" OnItemDataBound="rp_view_other_fee_online_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Payment_date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slipid")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label16" runat="server" Text='<%#Bind("Transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Content_Fee")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit020" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit020_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel020" runat="server" ToolTip="Delete" OnClick="lnkDel020_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Transaction_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_total_other_fee_online" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>

                                                                            <%--<asp:GridView ID="grid_view_other_fee_online" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_other_fee_online_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Payment_date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ADM. NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SEC.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slipid")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE ">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Content_Fee")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>

                                                                    <asp:Panel ID="pnl_other_fees_cheque" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            OTHER FEE (CHEQUE) 
                                                                        </div>

                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>ADM. NO.</th>
                                                                                    <th>STUDENT NAME</th>
                                                                                    <th>CLASS</th>
                                                                                    <th>SEC.</th>
                                                                                    <th>ROLL</th>
                                                                                    <th>RECIPT NO.</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_view_other_fee_cheque" runat="server" OnItemDataBound="rp_view_other_fee_cheque_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Payment_date")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slipid")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label16" runat="server" Text='<%#Bind("Transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Content_Fee")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit021" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit021_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel021" runat="server" ToolTip="Delete" OnClick="lnkDel021_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Transaction_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="11" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_total_other_fee_cheque" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>

                                                                            <%--<asp:GridView ID="grid_view_other_fee_cheque" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_view_other_fee_cheque_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Payment_date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ADM. NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STUDENT NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CLASS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="SEC.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="ROLL">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="RECIPT NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slipid")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE ">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Content_Fee")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </asp:Panel>




                                                                <asp:Panel ID="pnl_general_expense" runat="server">
                                                                    <div style="margin: 5px 0px 5px 0px; padding: 5px 0px 5px 0px; float: left; height: 31px; width: 100%; font-family: Arial; font-size: 16px; text-align: center; background-color: #fff; font-weight: bold; color: #000;">
                                                                        GENERAL EXPENSES
                                                                    </div>


                                                                    <asp:Panel ID="pnl_general_expense_cash" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            GENERAL EXPENSES (CASH) 
                                                                        </div>
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>SLIP NO.</th>
                                                                                    <th>VENDOR NAME</th>
                                                                                    <th>MODE</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_general_exp_cash" runat="server" OnItemDataBound="rp_general_exp_cash_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_name" runat="server" Text='<%#Bind("Vendor_Name")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Payment_amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit022" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit022_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel022" runat="server" ToolTip="Delete" OnClick="lnkDel022_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Check_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="6" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_vendor_general_exp_cash" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grd_general_exp_cash" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grd_general_exp_cash_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SLIP NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>


                                                                                    <asp:TemplateField HeaderText="VENDOR NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Vendor_Name" runat="server" Text='<%#Bind("Vendor_Name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="MODE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_amount" runat="server" Text='<%#Bind("Payment_amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_general_expense_online" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            GENERAL EXPENSES   (NEFT) 
                                                                        </div>

                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>SLIP NO.</th>
                                                                                    <th>VENDOR NAME</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_general_exp_online" runat="server" OnItemDataBound="rp_general_exp_online_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("Vendor_Name")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label16" runat="server" Text='<%#Bind("Check_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Payment_amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit023" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit023_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel023" runat="server" ToolTip="Delete" OnClick="lnkDel023_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Check_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="7" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_vendor_general_exp_online" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grd_general_exp_online" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grd_general_exp_online_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SLIP NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>


                                                                                    <asp:TemplateField HeaderText="VENDOR NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Vendor_Name" runat="server" Text='<%#Bind("Vendor_Name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="MODE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_amount" runat="server" Text='<%#Bind("Payment_amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_general_expense_cheque" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            GENERAL EXPENSES   (CHEQUE) 
                                                                        </div>

                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">

                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>SLIP NO.</th>
                                                                                    <th>VENDOR NAME</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_general_exp_cheque" runat="server" OnItemDataBound="rp_general_exp_cheque_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("Vendor_Name")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label16" runat="server" Text='<%#Bind("Check_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Payment_amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit024" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit024_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel024" runat="server" ToolTip="Delete" OnClick="lnkDel024_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Check_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="7" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_vendor_general_exp_cheque" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grd_general_exp_cheque" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grd_general_exp_cheque_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SLIP NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>


                                                                                    <asp:TemplateField HeaderText="VENDOR NAME">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Vendor_Name" runat="server" Text='<%#Bind("Vendor_Name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="MODE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_amount" runat="server" Text='<%#Bind("Payment_amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </asp:Panel>


                                                                <asp:Panel ID="pnl_general_Exp_no_vendor" runat="server">
                                                                    <div style="margin: 5px 0px 5px 0px; padding: 5px 0px 5px 0px; float: left; height: 31px; width: 100%; font-family: Arial; font-size: 16px; text-align: center; background-color: #fff; font-weight: bold; color: #000;">
                                                                        GENERAL EXPENSES WITHOUT VENDOR
                                                                    </div>


                                                                    <asp:Panel ID="pnl_general_Exp_no_vendor_Cash" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            GENERAL EXPENSES (CASH) 
                                                                        </div>
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>SLIP NO.</th>
                                                                                    <th>REMARKS</th>
                                                                                    <th>MODE</th>

                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_general_Exp_no_vendor_Cash" runat="server" OnItemDataBound="rp_general_Exp_no_vendor_Cash_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit025" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit025_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel025" runat="server" ToolTip="Delete" OnClick="lnkDel025_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Description")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Transaction_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="6" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_total_general_exp_no_vendor_cash" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>


                                                                            <%--<asp:GridView ID="grd_general_Exp_no_vendor_Cash" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grd_general_Exp_no_vendor_Cash_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SLIP NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="REMARKS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label688" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>


                                                                                    <asp:TemplateField HeaderText="MODE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" runat="server" Style="font-weight: 600"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_general_Exp_no_vendor_online" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            GENERAL EXPENSES (ONLINE)
                                                                        </div>

                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>SLIP NO.</th>
                                                                                    <th>REMARKS</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_general_Exp_no_vendor_online" runat="server" OnItemDataBound="rp_general_Exp_no_vendor_online_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label16" runat="server" Text='<%#Bind("Transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit026" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit026_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel026" runat="server" ToolTip="Delete" OnClick="lnkDel026_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Description")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Transaction_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="7" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_total_general_exp_no_vendor_online" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grd_general_Exp_no_vendor_online" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grd_general_Exp_no_vendor_online_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SLIP NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="REMARKS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label666" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" runat="server" Style="font-weight: 600"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnl_general_Exp_no_vendor_cheque" runat="server" Visible="false">
                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: 25px; width: 100%; font-family: Arial; font-size: 13px; text-align: center; background-color: #3bd9f1; font-weight: bold; color: #000;">
                                                                            GENERAL EXPENSES (CHEQUE)
                                                                        </div>

                                                                        <div style="margin: 0px; padding: 5px 0px 5px 0px; float: left; height: auto; width: 100%; font-family: Arial; font-size: 12px;">
                                                                            <table class="table table-striped table-bordered">
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>DATE</th>
                                                                                    <th>SLIP NO.</th>
                                                                                    <th>REMARKS</th>
                                                                                    <th>MODE</th>
                                                                                    <th>TRANSACTION ID</th>
                                                                                    <th>AMOUNT</th>
                                                                                    <th>USER</th>
                                                                                    <th runat="server" visible="false" class="actionth">Action</th>
                                                                                </tr>

                                                                                <asp:Repeater ID="rp_general_Exp_no_vendor_cheque" runat="server" OnItemDataBound="rp_general_Exp_no_vendor_cheque_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_recipt_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label16" runat="server" Text='<%#Bind("Transaction_no")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="amountStyle">
                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td runat="server" visible="false" class="actionth">
                                                                                                <asp:LinkButton ID="lnkEdit027" runat="server" CausesValidation="false" ToolTip="Edit" class="lnkediT" OnClick="lnkEdit027_Click"> <i class="bx bxs-edit"></i></asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDel027" runat="server" ToolTip="Delete" OnClick="lnkDel027_Click" class="lnkdeletE" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                                <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Description")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_pay_mode_transaction_no" runat="server" Text='<%#Bind("Transaction_no")%>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="trbg">
                                                                                    <td colspan="7" class="amountStyleTTL">TOTAL</td>
                                                                                    <td class="amountStyleAmt">
                                                                                        <asp:Label ID="lbl_total_general_exp_no_vendor_cheque" runat="server"></asp:Label></td>
                                                                                    <td colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <%--<asp:GridView ID="grd_general_Exp_no_vendor_cheque" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grd_general_Exp_no_vendor_cheque_RowDataBound" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SR.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SESSION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_session" Style="display: block; width: 70px;" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SLIP NO.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="REMARKS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label666" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="MODE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate><b>TOTAL</b></FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_payment_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_total_cash_amount" Style="font-weight: 600" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="User">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>--%>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </asp:Panel>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="not-found-dv" runat="server" id="NotFoundS">
                                                        <p>There is no matching data related to your filters.</p>
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


    <div class="modal fade" id="myModal" role="dialog" style="top: 0px" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog md-width">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 18px;">Update Payment</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Receipt No.</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:Label ID="lbl_receipt_no_pop" Style="background-color: #f7f6f6;" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Payment Date</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tx_payment_date" runat="server" class="form-control"></asp:TextBox>

                                <script>
                                    $(function () {
                                        $("#<%=tx_payment_date.ClientID %>").datepicker({
                                            dateFormat: "dd/mm/yy",
                                            changeMonth: true,
                                            changeYear: true,
                                            yearRange: "2021:2024",
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
                                <asp:DropDownList ID="ddl_pay_mode" runat="server" class="form-select">
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
                                    <asp:ListItem>UPI</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row" id="transacctionDV">
                        <div class="row">
                            <div class="col-sm-4">
                                <asp:Label ID="lbl_transactions" runat="server" class="find-dv-lbl"></asp:Label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_transaction_no" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Remark</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_remarks" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>



                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_update" OnClick="btn_update_Click" runat="server" Text="Update" class="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="mdl_reasoN" role="dialog" style="top: 0px" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog md-width">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 18px;">Reason for Delete</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Receipt No.</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:Label ID="lbl_reason_bill_no" Style="background-color: #f7f6f6;" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Enter Reason</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_reason_for_delete" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>



                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_conf_delete" OnClick="btn_conf_delete_Click" runat="server" Text="Delete" class="btn btn-primary" />
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        ///===========ON STATE SELECTION
        $(document).ready(function () {
            on_pay_mode_selection();
            $("#<%=ddl_pay_mode.ClientID%>").on('change', function () {
                on_pay_mode_selection();
            })
        });

        function on_pay_mode_selection() {
            if ($('#<%= ddl_pay_mode.ClientID %> option:selected').val() == "Cash") {
                $("#transacctionDV").hide();
            }
            else if ($('#<%= ddl_pay_mode.ClientID %> option:selected').val() == "Cheque") {
                $("#transacctionDV").show();
                $("#<%=lbl_transactions.ClientID %>").text("Cheque No.");
                $("#<%=txt_transaction_no.ClientID %>").focus();
            }
            else if ($('#<%= ddl_pay_mode.ClientID %> option:selected').val() == "NEFT") {
                $("#transacctionDV").show();
                $("#<%=lbl_transactions.ClientID %>").text("UTR No.");
                $("#<%=txt_transaction_no.ClientID %>").focus();
            }
            else {
                $("#transacctionDV").show();
                $("#<%=lbl_transactions.ClientID %>").text("Transaction No.");
                $("#<%=txt_transaction_no.ClientID %>").focus();
            }
        }


        //document.addEventListener('contextmenu', event => event.preventDefault());
    </script>
</asp:Content>
