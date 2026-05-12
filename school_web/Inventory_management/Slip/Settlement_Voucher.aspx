<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settlement_Voucher.aspx.cs" Inherits="school_web.Inventory_management.Slip.Settlement_Voucher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Settlement Voucher</title>
    <link href="Print.css" rel="stylesheet" />


    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>

    <script type="text/javascript">
        function pop(div) {
            document.getElementById(div).style.display = 'block';
        }
        function hide(div) {
            document.getElementById(div).style.display = 'none';
        }
        //To detect escape button
        document.onkeydown = function (evt) {
            evt = evt || window.event;
            if (evt.keyCode == 27) {
                hide('popDiv');
            }
        };
    </script>

    <style>
        .branch {
            margin: 0px auto;
            /* float: left; */
            padding: 0;
            height: auto;
            width: 800px;
            position: relative;
        }



        .containerNotFound {
            /* width: 25%; */
            margin-left: 2%;
            padding: 2%;
            text-align: center;
        }



        .printSlip {
            width: 100%;
            margin: 0px auto;
        }

        th {
            font-weight: bold;
            font-size: 11px;
            padding: 3px;
        }

        .th1 {
            background: #c3bebe !important
        }

        td {
            padding: 3px;
            text-align: right;
        }

        @media print {
            .noPrint1 {
                display: none;
            }

            .cnt_data {
                page-break-after: always;
                padding-bottom: 200px;
            }

            #Header, #Footer {
                display: none !important;
            }
        }

        .ontop {
            z-index: 999;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            display: none;
            position: absolute;
            background-color: #cccccc;
            color: #aaaaaa;
            opacity: .4;
            filter: alpha(opacity = 50);
        }

        #popup {
            width: 300px;
            height: 200px;
            position: absolute;
            color: #000000;
            background-color: #ffffff;
            /* To align popup window at the center of screen*/
            top: 50%;
            left: 50%;
            margin-top: -100px;
            margin-left: -150px;
        }

        /* Styles go here */
        /*.data-Table {
                margin-top: 100px;
        }*/
        /*.page-header, .page-header-space {
            height: 128px;
        }*/

        .page-header, .page-footer img {
            display: none;
        }

        .page-footer, .page-footer-space {
            height: 70px;
        }

        .page-footer {
            position: fixed;
            bottom: 20px;
            width: 800px;
        }

        .page-header {
            position: fixed;
            /*top: 0mm;*/
            /*width: 100%;*/
            width: 800px;
        }

        .noborder {
            border: 1px solid #fffefe;
        }

        .disply1 {
            display: none;
        }

        @page {
            margin: 10mm;
        }

        @media print {
            thead {
                display: table-header-group;
            }

            tfoot {
                display: table-footer-group;
            }

            button {
                display: none;
            }

            body {
                margin: 0;
            }

            .page-header, .page-footer img {
                display: block;
            }

            /* .page-header, .page-header-space {
                height: 128px;
            }*/
        }
    </style>

    <script language="javascript" type="text/javascript">
        function down() {
            window.top.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="divSlip">
            <asp:Label ID="lbl_session" runat="server" Visible="false"></asp:Label>
            <div class="printTopDiv noPrint1">

                <a href="javascript:" onclick="printit()">
                    <img src="../../images/print-recipt.png" class="printBtn noPrint1" style="height: 30px; width: 29px;" />
                </a>
                <asp:ImageButton ID="btn_back" runat="server" OnClick="btn_back_Click" ImageUrl="~/Inventory_management/Slip/close_icon.png" Height="36px" Style="height: 36px; width: 37px; margin-left: 234px; float: right;"
                    Width="37px" />

            </div>
            <div class="container">
                <div class="row">
                    <div class="branch" id="print_page">
                        <asp:HiddenField ID="hd_entry_id" runat="server" />
                        <div style="margin: 0px auto; margin: 0px; float: left; height: auto; width: 100%; padding: 0px 0px 0px 0px; border-collapse: collapse;">
                            <div class="row" style="width: 100%; float: left;">
                                <div class="printSlip">

                                    <table class="data-Table">
                                        <thead>
                                            <tr>
                                                <td colspan="4" class="border-top" style="border: none;">
                                                    <div id="page_header_space" runat="server" class="page-header-space" style="text-align: center;">

                                                        <asp:Panel ID="headertext" runat="server" Visible="true">
                                                            <table style="margin-top: 5px;">
                                                                <tr>
                                                                    <td style="width: 100px; text-align: center; padding: 0px; border: 1px solid #fff;">
                                                                        <asp:Image ID="img_logo" runat="server" Style="width: 100px; margin: 0px 0px 0px 0px; border: 1px solid #a7a7a7; border: 1px solid #fff;" />
                                                                    </td>
                                                                    <td style="padding: 0px; border: 1px solid #fff;">

                                                                        <h2 style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 20px;">
                                                                            <asp:Label ID="lbl_hospital_name" runat="server" Style="font-size: 20px;"></asp:Label></h2>
                                                                        <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 18px;">
                                                                            <asp:Label ID="lbl_address1" runat="server" Style="font-size: 12px;"></asp:Label>
                                                                        </p>
                                                                        <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 18px;">
                                                                            <asp:Label ID="lbl_address2" runat="server" Style="font-size: 12px;"></asp:Label>
                                                                        </p>
                                                                        <h2 style="margin: 0px 0px 5px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 18px;">
                                                                            <asp:Label ID="lbl_email_mobile" runat="server" Style="font-size: 12px;"></asp:Label></h2>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="border-top" style="border: none;">
                                                    <div style="background-color: #ebf1cc; box-shadow: 5px 5px #daddca; width: 100%; margin: 0px auto; text-align: center; font-size: 17px;">Dues Received Voucher</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                    <td colspan="2" style="width: 40%; border: 1px solid #fff;">
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_admission_no" runat="server" Style="font-size: 13px; float: left; text-transform: uppercase">Admission No.</asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_partyname" runat="server" Style="font-size: 13px; float: left"></asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_mobile_no" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_address" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                    </p>

                                                </td>
                                                <td style="border: none; width: 25%;">
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_admission_nodd" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_class_name" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_section" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_roll_no" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                    </p>
                                                </td>
                                                <td style="border: none; width: 25%;">
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_date" runat="server" Style="font-size: 13px; float: right"></asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_po_no" runat="server" Style="font-size: 13px; float: right;"></asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_payment_mode" runat="server" Style="font-size: 13px; float: right;"></asp:Label>
                                                    </p>
                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                        <asp:Label ID="lbl_user" runat="server" Style="font-size: 13px; float: right;"></asp:Label>
                                                    </p> 
                                                </td>
                                            </tr>

                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td colspan="4" style="border: none;">
                                                    <div class="page">


                                                        <table border="1" style="width: 100%; border: 1px solid #000;" class=" table table-hover table-striped table-bordered">
                                                            <thead>
                                                                <tr>
                                                                    <th class="th1">#</th>
                                                                    <th class="th1">Bill No</th>
                                                                    <th class="th1">Vochar Id</th>
                                                                    <th class="th1">Payment Mode</th>
                                                                    <th class="th1">Received from Cash</th>
                                                                    <th class="th1">Received from Bank</th>
                                                                    <th class="th1">Total Paid Amount</th>
                                                                    <th class="th1" style="display:none">Total Amount</th>
                                                                    
                                                                     
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="GrdView_Generate_PO" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Bill_No" runat="server" Text='<%#Bind("Bill_No")%>'></asp:Label>
                                                                            </td>
                                                                            

                                                                            <td>
                                                                                <asp:Label ID="lbl_Payment_Vochar_id" runat="server" Text='<%#Bind("Payment_Vochar_id")%>'></asp:Label>
                                                                            </td>
                                                                               <td>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("Bank_Payment_Mode")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Received_from_Cash" runat="server" Text='<%#Bind("Received_from_Cash")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Received_from_Bank" runat="server" Text='<%#Bind("Received_from_Bank")%>'></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: right;">
                                                                                <asp:Label ID="lbl_Total_Paid_Amount" runat="server" Text='<%#Bind("Total_Paid_Amount")%>'></asp:Label>
                                                                            </td>

                                                                            <td  style="display:none">
                                                                                <asp:Label ID="lbl_Duse_Amount" runat="server" Text='<%#Bind("Duse_Amount")%>'></asp:Label>
                                                                            </td>
                                                                             
                                                                           
                                                                           
                                                                           
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="padding: 0px 0px 10px 0px; padding-left: 5px; text-align: left; border: 1px solid #fff; vertical-align: top">

                                                    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 70%">
                                                        <table style="width: 98%; float: left; margin: 0px 0px 0px 5px;">
                                                            <tr>
                                                                <td>
                                                                    <p style="padding: 0px; margin: 0px; text-align: left;">
                                                                        Remarks :
                                                                    <asp:Label ID="lbl_remarks" runat="server"></asp:Label>

                                                                    </p>
                                                                </td>
                                                            </tr>



                                                        </table>
                                                    </div>
                                                   


                                                </td>

                                            </tr>

                                        </tbody>


                                        <tfoot>
                                            <tr id="footer_sapace" runat="server">
                                                <td colspan="4" style="padding-top: 0px; padding-bottom: 0px; padding-left: 5px; border: none;">
                                                    <!--place holder for the fixed-position footer-->
                                                    <div id="page_footer_space" runat="server" class="page-footer-space">
                                                        <p style="padding-top: 20px; float: right; padding-bottom: 5px; text-align: center;">
                                                            <asp:Label ID="Label1" runat="server" Text="Authorised Signatory" Style="font-size: 15px; color: #070505; font-weight: bold;"></asp:Label><br />
                                                            <br />
                                                        </p>

                                                    </div>
                                                </td>
                                            </tr>
                                        </tfoot>

                                    </table>
                                </div>
                            </div>



                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
