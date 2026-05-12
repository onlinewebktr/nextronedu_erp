<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierWisePurchaseItemDetails.aspx.cs" Inherits="school_web.Inventory_management.SupplierWisePurchaseItemDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    
    <title>Supplier Wise Purchase Report</title>

    <link href="../MasterAdmin/css/Line-Icons-Pro.css" rel="stylesheet" />
    <link href="../MasterAdmin/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../MasterAdmin/sMenu/style.css" rel="stylesheet" />
    <link href="../MasterAdmin/sMenu/style-responsive.css" rel="stylesheet" />
    <link href="../MasterAdmin/sMenu/menu.css" rel="stylesheet" />
    <script src="../MasterAdmin/js/jquery-1.10.2.min.js"></script>
    <script src="../MasterAdmin/js/bootstrap.min.js"></script>
    <link href="../MasterAdmin/menu/admin.css" rel="stylesheet" />
    <link href="../MasterAdmin/menu/them1.css" rel="stylesheet" />
    <link href="../MasterAdmin/css/admin.css" rel="stylesheet" />
    <link href="../Auto_complete/jquery-ui.css" rel="stylesheet" />
    <script src="../Auto_complete/jquery-ui.js"></script>
    <script src="../MasterAdmin/js/Toggle_custom.js"></script>


    <script src="../dropdownsearch/select2.min.js"></script>
    <link href="../dropdownsearch/select2.min.css" rel="stylesheet" />

    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    function onlyZeroandOne(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode < 48 || charCode > 49)
            return false;

        return true;
    }
    //-->
    </script>
    <style>
        .maste-p {
            width: 90%;
            float: left;
            padding: 0px 0px 0px 8px;
            margin: 0px;
            font-size: 14px;
            line-height: 20px;
        }

        .chck-box {
            float: left;
        }

        .all-check {
            padding: 0px 0px 0px 10px;
        }

        .form-group {
            margin-bottom: 0rem;
            /*margin: 5px 0px 5px 0px;*/
        }

        .mandatory_data {
            background-color: #f7e9b3!important;
            color: #000!important;
            font-weight: bold;
        }

        input[type=text]::-webkit-input-placeholder { /* Chrome/Opera/Safari */
            color: #000!important;
            font-weight: bold;
        }

        input[type=text]::-moz-placeholder { /* Firefox 19+ */
            color: #000!important;
            font-weight: bold;
        }

        input[type=text]:-ms-input-placeholder { /* IE 10+ */
            color: #000!important;
            font-weight: bold;
        }

        input[type=text]:-moz-placeholder { /* Firefox 18- */
            color: #000!important;
            font-weight: bold;
        }

        legend .divbg {
            background-color: #bd5300;
            margin: 0px 10px 0px 0px;
            padding: 0px;
            width: auto;
            float: left;
        }

        legend i {
            font-size: 22px;
            padding: 4px 6px;
        }

        .tdleft {
            padding-left: 20px !important;
        }

        .error {
            color: red;
            display: inline;
            background-color: #ccc;
            padding: 5px;
            position: absolute;
            z-index: 9999;
            width: auto;
            line-height: 20px;
            font-size: 13px;
            min-width: 300px;
            top: 20px;
        }

        .maste-p {
            width: 88%;
            float: left;
            padding: 0px 0px 0px 8px;
            margin: 0px;
            font-size: 14px;
            line-height: 20px;
        }

        .chck-box {
            float: left;
        }

        .all-check {
            padding: 0px 0px 0px 10px;
            width: 50% !important;
            float: left;
        }

        /*.form-group {
            margin-bottom: 0rem;
        }*/

        .new-patirnt-form-p {
            margin: 1px 0px 1px 0px;
            padding: 0px 0px 0px 0px;
            width: 100%;
            height: auto;
            float: left;
            font-size: 13px;
            color: #000;
            font-weight: bold;
        }

        .card-header, .card-title {
            text-transform: uppercase;
            color: #000;
            font-weight: bold;
            font-size: 12px;
            border-bottom: 1px solid #70a9a9;
            padding: 5px 0px 5px 5px;
            background-color: #d1eaea;
        }

        sup {
            color: #f00;
            top: -0.5em;
        }

        ::placeholder {
            color: red;
            opacity: 1; /* Firefox */
        }

        :-ms-input-placeholder { /* Internet Explorer 10-11 */
            color: red;
        }

        ::-ms-input-placeholder { /* Microsoft Edge */
            color: red;
        }

        .form-control:disabled, .form-control[readonly] {
            background-color: unset;
        }

        .age-main {
            width: 68%;
            float: left;
        }

        .age-main2 {
            width: 50px;
            float: left;
            margin: 0px 0px 0px 10px;
        }

        .socur-main2 {
            width: 65px;
            float: left;
            margin: 0px 10px 0px 0px;
        }

        .aadhar-logo {
            width: 100%;
            height: auto;
            margin: 0px;
            padding: 0px 0px 21px 41px;
            float: left;
        }

        .aadhar-img {
            width: 70%;
            height: auto;
            margin: 0px;
            padding: 0px 0px 0px 0px;
            float: left;
        }

        th {
            background-color: #f9f9f9;
        }

        td {
            vertical-align: top;
        }

        .cancel_button {
            padding: 3px 8px!important;
            margin: 8px 0px!important;
        }

        .fieldset_style {
            padding: 10px;
            border: 1px solid #e6cb0a;
            background-color: #fff9d0;
            width: 40%;
            margin-top: 10px;
            float: left;
        }

        .ipd_dash_box {
            padding: 5px 15px;
            background-color: #ddd;
            border-right: 1px solid #000;
        }

            .ipd_dash_box img {
                float: right;
                width: 10%;
            }
    </style>



    <link href="../DataTable/datatables.min.css" rel="stylesheet" />
    <script src="../DataTable/datatables.min.js"></script>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $('#example').dataTable({
                "sPaginationType": "full_numbers"
            });
            $('#example1').dataTable({
                "sPaginationType": "full_numbers"
            });
        });
    </script>
    <style type="text/css">
        table.dataTable tbody th, table.dataTable tbody td {
            padding: 2px 2px!important;
        }

        .dataTables_wrapper .dataTables_paginate .paginate_button {
            padding: 0em;
        }

        .pagination li a {
            padding: .2rem .70rem!important;
        }

        #menu_box {
            position: relative;
        }

        .menu-group-ul {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            width: 95%;
            height: auto;
            float: left;
            display: inherit;
        }

            .menu-group-ul li {
                margin: 0px;
                padding: 0px;
                list-style-type: none;
                display: inline;
                float: left;
                border-left: 1px solid #011e31;
            }

                .menu-group-ul li a {
                    margin: 0px 2px;
                    padding: 5px 12px;
                    text-decoration: none;
                    color: #fff;
                    font-weight: 500;
                    font-size: 14px;
                    -webkit-transition: all .3s ease;
                    transition: all .3s ease;
                    display: block;
                }

                    .menu-group-ul li a:hover {
                        background-color: #f5f6fc;
                        color: #000;
                    }

        #footer {
            height: auto;
        }

        .menu-group-ul li a {
            font-family: 'Poppins', sans-serif;
            font-weight: 400;
        }

        .active {
            background: #f3a800;
        }

        .fixed {
            position: fixed;
            top: 0px;
            left: 0;
            width: 100%;
            background-color: #008fbe;
            box-shadow: -3px 13px 24px -1px rgba(0,0,0,0.3);
            z-index: 99999;
            -webkit-transition: all .3s ease-out;
            -moz-transition: all .3s ease-out;
            -ms-transition: all .3s ease-out;
            -o-transition: all .3s ease-out;
            transition: all .3s ease-out;
        }

        .sticky {
            -webkit-transition: all .3s ease-out;
            -moz-transition: all .3s ease-out;
            -ms-transition: all .3s ease-out;
            -o-transition: all .3s ease-out;
            transition: all .3s ease-out;
        }

        /*.fixed .menu-group-ul li a {
            color: #000;
        }*/

        .fixed ul.sidebar-menu {
            padding-top: 44px;
        }

        .logo-sec {
            height: auto;
            width: 66%;
            margin: 0px 0px 0px 0px;
            float: left;
        }

        .marquee-scrl-sec {
            padding: 0px 0px 0px 0px;
            margin: 0px 0px 0px 0px;
            width: 100%;
            height: auto;
            float: left;
            text-align: center;
            color: #000;
        }

        .profile_sec {
            height: auto;
            width: 34%;
            margin: 0px 0px 0px 0px;
            float: left;
            background-color: #5e72e4;
        }

        .para_sty_heading {
            color: #fff;
            font-size: 20px;
            width: 100%;
            text-align: center;
            font-weight: bold;
        }

        .padding_zero {
            padding: 0px;
        }

        @media only screen and (max-width:800px) {
            ul.sidebar-menu {
                padding-top: 110px;
            }

            .ipd_dash_box {
                padding: 5px 15px;
                background-color: #ddd;
                border: 1px solid #000;
                margin: 2px;
            }

                .ipd_dash_box img {
                    float: right;
                    width: 10%;
                }

            .fieldset_style {
                padding: 10px;
                border: 1px solid #e6cb0a;
                background-color: #fff9d0;
                width: 100%;
                margin-top: 10px;
                float: left;
            }

            .age-main {
                width: 72%;
            }

            .padding_zero {
                padding-right: 15px;
                padding-left: 15px;
            }

            .para_sty_heading {
                color: #fff;
                font-size: 18px;
                width: 100%;
                text-align: center;
                font-weight: bold;
            }

            .find-ddl {
                display: none;
            }

            .fixed {
                position: fixed;
            }


                .fixed .logof {
                    margin: 0px 0px 0px 0px!important;
                }

                .fixed.logo-sec-two {
                    padding: 0px 0px 0px 0px;
                }

            .menu-group-ul {
                display: none;
            }

            .logo-sec {
                height: auto;
                width: 100%;
                text-align: center;
                padding: 10px 0px 0px 0px;
            }

            .marquee-scrl-sec {
                padding: 0px 0px 0px 0px;
                margin: 0px 0px 0px 0px;
                width: 100%;
                /*position: absolute;
                bottom: -4px;*/
                text-align: center;
            }

                .marquee-scrl-sec span {
                    margin: 0px 0px 0px 0px;
                    padding: 0px 0px 0px 0px;
                    text-align: center;
                }

                .marquee-scrl-sec marquee {
                    padding: 5px 0px 5px 0px;
                    width: 100%;
                    float: right;
                }

            .profile_sec {
                height: auto;
                width: 100%;
                margin: 0px 0px 0px 0px;
                float: left;
                background-color: #5e72e4;
                padding: 0px 10px;
            }

            #header_box {
                position: relative;
            }
        }
    </style>

    <script>
        $(window).scroll(function () {
            var sticky = $('.sticky'),
                scroll = $(window).scrollTop();

            if (scroll >= 50) sticky.addClass('fixed');
            else sticky.removeClass('fixed');
        });
    </script>
    <script language="javascript" type="text/javascript">
        var submit = 0;
        function CheckIsRepeat() {
            if (++submit > 1) {
                alert('Please wait... your 1st attempt is in process.');
                return false;
            }
        }
        function clear_repeat() {
            submit = 0;
        }
    </script>
</head>
<body>
   <form id="form1" runat="server">
          <div class="page-cont-wpr">
        <div class="app-main__inner">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 100%; height: auto;">
                        <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="float: none; margin: 0px auto;">
                    <div class="main-card mb-3 card">
                        <fieldset>
                            <legend>
                                <p class="divbg"><i class="pe-7s-config icon-gradient bg-mean-fruit"></i></p>
                                <span style="line-height: 32px;">Supplier Wise Purchase Report</span>

                                <%--    <asp:LinkButton ID="lnk_excel_download" runat="server"  class="btn-print noPrint" Style="float: right"><i class="fa fa-file-excel"> </i></asp:LinkButton>
                                <asp:LinkButton ID="print1" runat="server" ToolTip="Print" class="btn-print noPrint" Style="float: right"><i class="fa fa-print"></i></asp:LinkButton>--%>

                            </legend>
                            <div id="tblitems" class="card-body" style="overflow: auto;">
                                <asp:Panel ID="pnl_grid" runat="server">
                                    <table id="example2" border="1" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                        <thead>

                                            <tr>
                                                <th colspan="21" style="margin: 0px 0px 5px 0px; padding: 0px; text-align: center; line-height: 16px; border-bottom: 1px solid #000">

                                                    <asp:Label ID="lbl_hospital_name" runat="server" Font-Bold="true" Style="font-size: 17px;"></asp:Label>

                                                    <asp:Label ID="lbl_address1" runat="server" Font-Bold="true" Style="margin: 0px 0px 5px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 13px;"></asp:Label>


                                                    <asp:Label ID="lbl_address2" runat="server" Font-Bold="true" Style="margin: 0px 0px 5px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 15px;"></asp:Label>

                                                    <p style="font-size: 15px; text-align: center;">
                                                        <asp:Label ID="lbl_duration" runat="server" Font-Bold="true"></asp:Label>
                                                    </p>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>#</th>
                                                <th>Purchase Date</th>
                                                <th>Invoice No</th>
                                                <th>Session</th>
                                                <th>Supplier Name</th>
                                                <th>Mobile</th>
                                                <th>GSTIN</th>
                                                <th>Total</th>
                                                <th>Discount</th>
                                                <th>Taxable</th>
                                                <th>Tax</th>
                                                 <th>Extra Charge</th>
                                                <th>Grand total</th>
                                                <th>Round off</th>
                                                <th>Final Value</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                                <itemtemplate>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_purchase_date" runat="server" Text='<%#Bind("pur_date")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("invoice_no")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_party_name" runat="server" Text='<%#Bind("party_name")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_mobile" runat="server" Text='<%#Bind("mobile")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_gstin" runat="server" Text='<%#Bind("gstin")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_total" runat="server" Text='<%#Bind("Total_Purchase_rate")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_discount" runat="server" Text='<%#Bind("discount_amount")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_taxable" runat="server" Text='<%#Bind("Total_taxable")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_tax" runat="server" Text='<%#Bind("Total_Gst_value")%>'></asp:Label>
                                                        </td>

                                                         <td>

                                                            <asp:Label ID="lbl_txt_freight" runat="server" Text='<%#Bind("txt_freight")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lbl_grand_total" runat="server" Text='<%#Bind("Total_netamount")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_flat_disc" runat="server" Text='<%#Bind("roundoff")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_final_value" runat="server" Text='<%#Bind("Grand_total")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </itemtemplate>
                                                <footertemplate>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                      <td></td>
                                                    <td style="border-top: 2px solid #000!important; font-size: 12px;">
                                                        <asp:Label ID="lbl_total_amount" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </footertemplate>
                                            </asp:Repeater>

                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>


    </div>
    </form>
</body>
</html>
