<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Student_Transaction_Summary.aspx.cs" Inherits="school_web.Admin.Student_Transaction_Summary" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Student Transaction Summary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
           tfoot, th, thead {
    border-color: inherit;
    border-style: solid;
    border-width: 0;
    vertical-align: middle;
    background: #ffffff !important;
    color: #0e0e0e!important;
}
        tbody, td, tfoot, th, thead, tr {
            font-size: 12px;
            text-transform: capitalize;
        }

        .chkstle {
            position: relative;
            padding: 6px 5px 1px 5px;
            margin: 0px 1px 5px 0px;
            cursor: pointer;
            font-size: 12px;
            font-weight: 600;
            float: left;
            border: 1px solid #bac419;
            background: #FBFFBB;
            border-radius: 2px;
        }

            .chkstle input {
                cursor: pointer;
                width: 21px;
            }

            .chkstle label {
                margin: -2px 0px 0px 0px;
                float: left;
            }

        .mode-ul {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            text-align: center;
        }

            .mode-ul li {
                margin: 0px 5px 0px 0px;
                padding: 2px 5px;
                list-style-type: none;
                display: inline;
                font-size: 13px;
                border: 1px solid #bac419;
                background: #FBFFBB;
                border-radius: 2px;
                font-weight: 600;
                color: #000;
            }

                .mode-ul li i {
                    margin: 0px;
                    padding: 0px;
                    font-style: normal;
                }

                .mode-ul li span {
                    margin: 0px;
                    padding: 0px;
                }

        .btn {
            padding: 5px 7px 4px;
            font-size: 13px;
        }

        .usewise-dv {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            position: relative;
        }

        .usewise-tbldv {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .usewise-title {
            margin: 5px 0px 5px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 17px;
        }

        .std-info-fnd {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            position: absolute;
            left: 0px;
            top: -22px;
            cursor: pointer;
            background: #000;
        }
    </style>
    <style>
        .sub-pag-menu-ul li a {
            color: #312F7F;
            border: 1px solid #312F7F;
        }

            .sub-pag-menu-ul li a:hover {
                background: #312F7F;
                color: #fff;
                border: 1px solid #312F7F;
            }

        .sub-mnu-p-a-active {
            background: #312F7F;
            color: #ffffff !important;
        }

        .dataTables_length {
            display: none;
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Today Collection Sheet</li>
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
                    <ul class="sub-pag-menu-ul" id="forMilliaKsba" runat="server" visible="false">
                        <li><a href="dcr-all-fee.aspx">Today Collection Sheet</a></li>
                        <li><a href="admission-fee-dcr.aspx" class="sub-mnu-p-a-active">Collection Sheet Admission/Annual</a></li>
                    </ul>

                    <ul class="sub-pag-menu-ul" id="forallSchool" runat="server">
                        <li><a href="collection-sheet-dcr.aspx">Today Collection Sheet</a></li>
                        <li><a href="collection-sheet-dcr-userwise.aspx">User-Wise Collection Sheet</a></li>
                        <li><a href="collection-sheet-dcr-with-remark.aspx">Collection Sheet With Remark</a></li>
                        <li><a href="admission-fee-dcr.aspx">Collection Sheet Admission/Annual</a></li>
                        <li><a href="Student_Transaction_Summary.aspx" class="sub-mnu-p-a-active">Student Transaction</a></li>
                    </ul>
                </div>


                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="grd-wpr">
                        <div class="card">
                            <div class="card-body">
                                <div class="find-dv">
                                    <div class="row  g-3 needs-validation">
                                        <div class="col-sm-3">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                    <asp:TextBox ID="txt_from_date" runat="server" class="form-control find-dv-txtbx datepicker txtbx-ddl-style"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-6">
                                                    <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                    <asp:TextBox ID="txt_to_date" runat="server" class="form-control find-dv-txtbx datepicker txtbx-ddl-style"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-4">
                                            <asp:Button ID="btn_find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" runat="server" Text="Find" />

                                             <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 0px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>

                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                ToolTip="Print">
                                                <i class='bx bx-printer'></i>Print</asp:LinkButton>
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
                                                        &nbsp;&nbsp;  website :
                                                                    <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                    </div>
                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                                    </div>
                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        <span style="font-size: 14px; font-weight: bold;">Student Transaction Summery 
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="prnt-dv-wpr printborder" id="grdsdatA">
                                                <div class="grd-wpr-new">
                                                     <asp:Panel ID="Panel1" runat="server">
                                                    <table class="table table-bordered" datatable="ng" dt-options="vm.dtOptions" style="border: 1px solid #c5c0c0;">
                                                        <thead>
                                                            <tr>
                                                                 <th colspan="3">
                                                                    
                                                                </th>
                                                                <th colspan="2" style="text-align: center;">
                                                                    Total CR
                                                                </th>
                                                                <th colspan="2" style="text-align: center;">
                                                                     Total DR
                                                                </th>
                                                            </tr>
                                                            <tr>

                                                                <th>#</th>
                                                                <th>Student Name</th>
                                                                <th>Account Id</th>
  
                                                                <th style="text-align: center;">Cash(CR)</th>
                                                                <th style="text-align: center;">Bank(CR)</th>
                                                                <th style="text-align: center;">Cash(DR)</th>
                                                                <th style="text-align: center;">Bank(DR)</th>
                                                                
                                                               
                                                                 
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server"  >
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("Alternet_Account")%>'></asp:Label>
                                                                </td>
                                                                 
                                                                 
                                                                 
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_cash_cr" runat="server" Text='<%#Bind("Credit_CASH")%>'></asp:Label>
                                                                </td>
                                                                 <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_bank_cr" runat="server" Text='<%#Bind("Credit_BANK")%>'></asp:Label>
                                                                </td>

                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_cash_dr" runat="server" Text='<%#Bind("Debit_CASH")%>'></asp:Label>
                                                                </td>
                                                                 <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_bank_dr" runat="server" Text='<%#Bind("Debit_BANK")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                        </tbody>
                                                        <tfoot>
                                                            <tr>
                                                                <td colspan="3" style="text-align: right;">
                                                                    Total
                                                                </td>
                                                                 <td  style="text-align: center;">
                                                                    <asp:Label ID="lbl_cash_cr_total" runat="server"  ></asp:Label>
                                                                </td>
                                                                 <td  style="text-align: center;">
                                                                       <asp:Label ID="lbl_bank_cr_total" runat="server"  ></asp:Label>
                                                                </td>
                                                                 <td  style="text-align: center;">
                                                                    <asp:Label ID="lbl_cash_dr_total" runat="server" ></asp:Label>
                                                                </td>
                                                                 <td  style="text-align: center;">
                                                                     <asp:Label ID="lbl_bank_dr_total" runat="server" ></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tfoot>
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
</asp:Content>
