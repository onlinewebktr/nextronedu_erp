<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Sale_slip.aspx.cs" Inherits="school_web.Inventory_management.Slip.Print_Sale_slip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bill</title>

    <script src="../../js/jquery-1.10.2.min.js"></script>
    <link href="Print.css" rel="stylesheet" />


    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>
    <style>
        .chckbx-sec {
            margin: 0px auto;
            padding: 0px;
            width: 80%;
            text-align: center;
        }

        .chckbx-sec-inr {
            margin: 12px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .chckbx--span {
            margin: 0px 15px;
            width: auto;
            display: inline;
        }

            .chckbx--span label {
                margin: 0px 0px 0px 5px;
            }

        .chckbx-sec-inr input[type="radio"] {
            -webkit-appearance: none;
            appearance: none;
            background-color: var(--form-background);
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 1.15em;
            height: 1.15em;
            border: 0.15em solid currentColor;
            border-radius: 50%;
            transform: translateY(-0.075em);
            display: inline-grid;
            place-content: center;
        }

            .chckbx-sec-inr input[type="radio"]::before {
                content: "";
                width: 0.65em;
                height: 0.65em;
                border-radius: 50%;
                transform: scale(0);
                transition: 120ms transform ease-in-out;
                box-shadow: inset 1em 1em var(--form-control-color);
                /* Windows High Contrast Mode */
                background-color: CanvasText;
            }

            .chckbx-sec-inr input[type="radio"]:checked::before {
                transform: scale(1);
            }

            .chckbx-sec-inr input[type="radio"]:focus {
                outline: max(2px, 0.15em) solid currentColor;
                outline-offset: max(2px, 0.15em);
            }
    </style>

    <style>
        @media print {
            .noPrint1 {
                display: none;
            }
        }
    </style>
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


    <script language="javascript" type="text/javascript">
        function down() {
            window.top.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="divSlip">
            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                <div style="margin: 0px auto; padding: 0px; width: 1000px">
                    <div style="margin: 0px; padding: 0px; width: 100%; float: left;">

                        <div class="printTopDiv noPrint1">

                            <a href="javascript:" onclick="printit()" style="float: left;">
                                <img src="../../images/print-recipt.png" class="printBtn noPrint1" style="height: 30px; width: 29px;" />
                            </a>
                            <div class="chckbx-sec">
                                <div class="chckbx-sec-inr">
                                    <div class="chckbx--span">
                                        <asp:RadioButton ID="rdo_both" onclick="myFunction('1')" runat="server" GroupName="aA" Text="Both" />
                                    </div>
                                    <div class="chckbx--span">
                                        <asp:RadioButton ID="rdo_office_copy" onclick="myFunction('2')" runat="server" GroupName="aA" Text="Office Copy" />
                                    </div>
                                    <div class="chckbx--span">
                                        <asp:RadioButton ID="rdo_student_copy" onclick="myFunction('3')" runat="server" GroupName="aA" Text="Student Copy" />
                                    </div>
                                </div>
                            </div>

                            <asp:ImageButton ID="btn_back" runat="server" OnClick="btn_back_Click" ImageUrl="~/Inventory_management/Slip/close_icon.png" Height="36px" Style="height: 36px; width: 37px; float: right;"
                                Width="37px" />

                        </div>
                    </div>

                </div>
            </div>


            <div class="container">


                <div class="row">
                    <div class="branch" id="print_page">

                        <div style="margin: 0px auto; height: auto; width: 800px; padding: 0px 0px 0px 0px; border-collapse: collapse;">
                            <div style="margin: 0px; float: left; height: auto; width: 100%; padding: 0px 0px 0px 0px; border-collapse: collapse;">

                                <div class="row" style="width: 100%; float: left;">
                                    <div class="printSlip" id="officecopY">

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
                                                        <div style="background-color: #ebf1cc; width: 100%; margin: 0px auto; text-align: center; font-size: 17px;">Sale Bill(Office Copy)</div>
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

                                                            <asp:Panel ID="pnltab_1_item" runat="server" Visible="false">
                                                                <table border="1" style="width: 100%; border: 1px solid #000;" class=" table table-hover table-striped table-bordered">
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="th1">#</th>
                                                                            <th class="th1">Item Name</th>
                                                                            <th class="th1" style="display: none">Brand</th>
                                                                            <%--<th>HSN No.</th>--%>
                                                                            <%-- <th>Batch No</th>
                                                                    <th>Expiry Date</th>--%>
                                                                            <th class="th1">Delivery Date</th>
                                                                            <th class="th1">Qty</th>
                                                                            <th class="th1">Unit</th>
                                                                            <th class="th1">Rate</th>
                                                                            <th class="th1">Total</th>
                                                                            <th class="th1" style="display: none">Disc(%)</th>
                                                                            <th class="th1" style="display: none">Taxable</th>
                                                                            <th class="th1" style="display: none">GST(%)</th>
                                                                            <th class="th1" style="display: none">Net Total</th>
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
                                                                                        <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_name")%>'></asp:Label>
                                                                                    </td>


                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Deliverydate" runat="server" Text='<%#Bind("Delivery_datetime")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_total_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("Unit_name")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: right;">
                                                                                        <asp:Label ID="lbl_Total" runat="server" Text='<%#Bind("Total")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="display: none">
                                                                                        <asp:Label ID="Label9" runat="server" Text='<%#Bind("Discount")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Discount_Percent")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="display: none">
                                                                                        <asp:Label ID="Label10" runat="server" Text='<%#Bind("Taxable")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="display: none">
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Total_GST")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("GST_Percent")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="display: none">
                                                                                        <asp:Label ID="Label8" runat="server" Text='<%#Bind("NetTotal")%>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnltab_1_package" runat="server" Visible="false">
                                                                <table border="1" style="width: 100%; border: 1px solid #000;" class=" table table-hover table-striped table-bordered">
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="th1">#</th>
                                                                            <th class="th1">Item Name</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:Repeater ID="Repeater_package_1" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: center;">
                                                                                        <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Package_Name")%>'></asp:Label>
                                                                                    </td>


                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="padding: 0px 0px 10px 0px; padding-left: 5px; text-align: left; border: 1px solid #fff; vertical-align: top">

                                                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 75%">
                                                            <table style="width: 98%; float: left; margin: 0px 0px 0px 5px;">
                                                                <tr>
                                                                    <td>
                                                                        <p style="padding: 0px; margin: 0px; text-align: left;">
                                                                            Remarks :
                                                                    <asp:Label ID="lbl_remarks" runat="server"></asp:Label>

                                                                        </p>

                                                                        <p style="padding: 0px; margin: 0px; text-align: left;font-size: 12px;">
                                                                            Payment Remarks :
                                                                    <asp:Label ID="lbl_payment_remarks" runat="server"></asp:Label>

                                                                        </p>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <asp:Panel ID="Panel1" runat="server">
                                                                        <td>
                                                                            <table border="1" style="width: 100%; border: 1px solid #000;" class=" table table-hover table-striped table-bordered">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th class="th1">#</th>
                                                                                        <th class="th1">Item Name</th>
                                                                                        <th class="th1">Brand</th>

                                                                                        <th class="th1">Qty</th>
                                                                                        <th class="th1">Unit</th>
                                                                                        <th class="th1">Rate</th>
                                                                                        <th class="th1">Total</th>

                                                                                        <th class="th1">Status</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td style="text-align: left;">
                                                                                                    <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_name")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Brand_name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                                                                                </td>

                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_total_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("Unit_name")%>'></asp:Label>
                                                                                                </td>

                                                                                                <td>
                                                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                                                                                </td>

                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Total" runat="server" Text='<%#Bind("Total")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_status" runat="server" Text="Not Delivered"></asp:Label>
                                                                                                </td>



                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </tbody>
                                                                            </table>

                                                                            <p style="padding: 7px 0px 0px 0px; margin: 0px; text-align: center;">
                                                                                Once the stock arrives, we will notify you, and it will come with a slip.
                                                                            </p>
                                                                        </td>
                                                                    </asp:Panel>

                                                                </tr>


                                                            </table>

                                                            <table style="width: 98%; float: left; margin: 107px 0px 0px 5px;">
                                                                <tr>
                                                                    <td style="text-align: center; border: 0px solid #fff">
                                                                        <asp:Label ID="Label4" runat="server" Text="Authorised Signatory" Style="font-size: 15px; color: #070505; font-weight: bold;"></asp:Label><br />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 25%">
                                                            <table style="width: 96%; float: left; margin: 0px; padding: 0px;">
                                                                <tr>
                                                                    <td class="noborder">Total Amount :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_Total_rate" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr class="disply1">
                                                                    <td class="noborder">Discount :(-)</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_discount" runat="server"></asp:Label></td>
                                                                </tr>

                                                                <tr style="display: none">
                                                                    <td class="noborder">Total Taxbale :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_taxbale" runat="server"></asp:Label></td>
                                                                </tr>


                                                                <tr style="display: none;">
                                                                    <td>GST :</td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_GST" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr class="disply1">
                                                                    <td class="noborder">SGST :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_sgst" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr class="disply1">
                                                                    <td class="noborder">CGST :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_cgst" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr class="disply1">
                                                                    <td class="noborder">Trans. Charge(+) :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_trancharge" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td class="noborder">Expense(+) :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_expense" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="noborder">Flat Dis.(-) :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_flat_dis" runat="server"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="noborder">Grand Total :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_grandtotal" runat="server"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="noborder">Round of :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_round_of" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="noborder">Net Total :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_net_total" runat="server"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="noborder">Paid Amount :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_paid_amount" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="noborder">Dues Amount :</td>
                                                                    <td class="noborder">
                                                                        <asp:Label ID="lbl_duesamount" runat="server"></asp:Label></td>
                                                                </tr>

                                                            </table>
                                                        </div>


                                                    </td>

                                                </tr>

                                            </tbody>




                                        </table>

                                    </div>

                                    <div class="printSlip" id="studentcopY">
                                        <div id="studentcopYInr">

                                            <table class="data-Table">
                                                <thead>
                                                    <tr>
                                                        <td colspan="4" class="border-top" style="border: none;">
                                                            <div id="Div1" runat="server" class="page-header-space" style="text-align: center;">

                                                                <asp:Panel ID="Panel2" runat="server" Visible="true">
                                                                    <table style="margin-top: 20px;">
                                                                        <tr>
                                                                            <td style="width: 100px; text-align: center; padding: 0px; border: 1px solid #fff;">
                                                                                <asp:Image ID="img_logo1" runat="server" Style="width: 100px; margin: 0px 0px 0px 0px; border: 1px solid #a7a7a7; border: 1px solid #fff;" />
                                                                            </td>
                                                                            <td style="padding: 0px; border: 1px solid #fff;">

                                                                                <h2 style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 20px;">
                                                                                    <asp:Label ID="lbl_hospital_name1" runat="server" Style="font-size: 20px;"></asp:Label></h2>
                                                                                <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 18px;">
                                                                                    <asp:Label ID="lbl_address11" runat="server" Style="font-size: 12px;"></asp:Label>
                                                                                </p>
                                                                                <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 18px;">
                                                                                    <asp:Label ID="lbl_address21" runat="server" Style="font-size: 12px;"></asp:Label>
                                                                                </p>
                                                                                <h2 style="margin: 0px 0px 5px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 18px;">
                                                                                    <asp:Label ID="lbl_email_mobile1" runat="server" Style="font-size: 12px;"></asp:Label></h2>
                                                                            </td>
                                                                        </tr>

                                                                    </table>
                                                                </asp:Panel>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" class="border-top" style="border: none;">
                                                            <div style="background-color: #ebf1cc; width: 100%; margin: 0px auto; text-align: center; font-size: 17px;">Sale Bill(Student Copy)</div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="width: 40%; border: 1px solid #fff;">
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_admission_no1" runat="server" Style="font-size: 13px; float: left; text-transform: uppercase">Admission No.</asp:Label>
                                                            </p>
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_partyname1" runat="server" Style="font-size: 13px; float: left"></asp:Label>
                                                            </p>
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_mobile_no1" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                            </p>
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_address22" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                            </p>

                                                        </td>
                                                        <td style="border: none; width: 25%;">
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_admission_nodd1" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                            </p>
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_class_name1" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                            </p>
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_section1" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                            </p>
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_roll_no1" runat="server" Style="font-size: 13px; float: left;"></asp:Label>
                                                            </p>
                                                        </td>
                                                        <td style="border: none; width: 25%;">
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_date1" runat="server" Style="font-size: 13px; float: right"></asp:Label>
                                                            </p>
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_po_no1" runat="server" Style="font-size: 13px; float: right;"></asp:Label>
                                                            </p>
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_payment_mode1" runat="server" Style="font-size: 13px; float: right;"></asp:Label>
                                                            </p>
                                                            <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: left; line-height: 20px; font-weight: bold;">
                                                                <asp:Label ID="lbl_user1" runat="server" Style="font-size: 13px; float: right;"></asp:Label>
                                                            </p>
                                                        </td>
                                                    </tr>

                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="4" style="border: none;">
                                                            <div class="page">
                                                                <asp:Panel ID="pnltab_2_item" runat="server" Visible="false">

                                                                    <table border="1" style="width: 100%; border: 1px solid #000;" class=" table table-hover table-striped table-bordered">
                                                                        <thead>
                                                                            <tr>
                                                                                <th class="th1">#</th>
                                                                                <th class="th1">Item Name</th>
                                                                                <th class="th1" style="display: none">Brand</th>
                                                                                <%--<th>HSN No.</th>--%>
                                                                                <%-- <th>Batch No</th>
                                                                    <th>Expiry Date</th>--%>
                                                                                <th class="th1">Delivery Date</th>
                                                                                <th class="th1">Qty</th>
                                                                                <th class="th1">Unit</th>
                                                                                <th class="th1">Rate</th>
                                                                                <th class="th1">Total</th>
                                                                                <th class="th1" style="display: none">Disc(%)</th>
                                                                                <th class="th1" style="display: none">Taxable</th>
                                                                                <th class="th1" style="display: none">GST(%)</th>
                                                                                <th class="th1" style="display: none">Net Total</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="GrdView_Generate_PO1" runat="server">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_name")%>'></asp:Label>
                                                                                        </td>


                                                                                        <td>
                                                                                            <asp:Label ID="lbl_Deliverydate" runat="server" Text='<%#Bind("Delivery_datetime")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_total_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("Unit_name")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td>
                                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td style="text-align: right;">
                                                                                            <asp:Label ID="lbl_Total" runat="server" Text='<%#Bind("Total")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="display: none">
                                                                                            <asp:Label ID="Label9" runat="server" Text='<%#Bind("Discount")%>' Visible="false"></asp:Label>
                                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("Discount_Percent")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="display: none">
                                                                                            <asp:Label ID="Label10" runat="server" Text='<%#Bind("Taxable")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="display: none">
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("Total_GST")%>' Visible="false"></asp:Label>
                                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("GST_Percent")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="display: none">
                                                                                            <asp:Label ID="Label8" runat="server" Text='<%#Bind("NetTotal")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                    </table>
                                                                </asp:Panel>

                                                                <asp:Panel ID="pnltab_2_package" runat="server" Visible="false">
                                                                    <table border="1" style="width: 100%; border: 1px solid #000;" class=" table table-hover table-striped table-bordered">
                                                                        <thead>
                                                                            <tr>
                                                                                <th class="th1">#</th>
                                                                                <th class="th1">Item Name</th>

                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="Repeater_package_2" runat="server">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: center;">
                                                                                            <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Package_Name")%>'></asp:Label>
                                                                                        </td>


                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                    </table>
                                                                </asp:Panel>
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
                                                                    <asp:Label ID="lbl_remarks1" runat="server"></asp:Label>

                                                                            </p>

                                                                            <p style="padding: 0px; margin: 0px; text-align: left;">
                                                                                Payment Remarks :
                                                                    <asp:Label ID="lbl_payment_remarks1" runat="server"></asp:Label>

                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>

                                                                        <asp:Panel ID="Panel11" runat="server">
                                                                            <td>
                                                                                <table border="1" style="width: 100%; border: 1px solid #000;" class=" table table-hover table-striped table-bordered">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th class="th1">#</th>
                                                                                            <th class="th1">Item Name</th>
                                                                                            <th class="th1">Brand</th>

                                                                                            <th class="th1">Qty</th>
                                                                                            <th class="th1">Unit</th>
                                                                                            <th class="th1">Rate</th>
                                                                                            <th class="th1">Total</th>

                                                                                            <th class="th1">Status</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:Repeater ID="Repeater11" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: left;">
                                                                                                        <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_name")%>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbl_Brand_name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                                                                                    </td>

                                                                                                    <td>
                                                                                                        <asp:Label ID="lbl_total_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("Unit_name")%>'></asp:Label>
                                                                                                    </td>

                                                                                                    <td>
                                                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                                                                                    </td>

                                                                                                    <td>
                                                                                                        <asp:Label ID="lbl_Total" runat="server" Text='<%#Bind("Total")%>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbl_status" runat="server" Text="Not Delivered"></asp:Label>
                                                                                                    </td>



                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </tbody>
                                                                                </table>

                                                                                <p style="padding: 7px 0px 0px 0px; margin: 0px; text-align: center;">
                                                                                    Once the stock arrives, we will notify you, and it will come with a slip.
                                                                                </p>
                                                                            </td>
                                                                        </asp:Panel>

                                                                    </tr>


                                                                </table>

                                                                <table style="width: 98%; float: left; margin: 107px 0px 0px 5px;">
                                                                    <tr>
                                                                        <td style="text-align: center; border: 0px solid #fff">
                                                                            <asp:Label ID="Label7" runat="server" Text="Authorised Signatory" Style="font-size: 15px; color: #070505; font-weight: bold;"></asp:Label><br />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 30%">
                                                                <table style="width: 96%; float: left; margin: 0px; padding: 0px;">
                                                                    <tr>
                                                                        <td class="noborder">Total Amount :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_Total_rate1" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr class="disply1">
                                                                        <td class="noborder">Discount :(-)</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_discount1" runat="server"></asp:Label></td>
                                                                    </tr>

                                                                    <tr style="display: none">
                                                                        <td class="noborder">Total Taxbale :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_taxbale1" runat="server"></asp:Label></td>
                                                                    </tr>


                                                                    <tr style="display: none;">
                                                                        <td>GST :</td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_GST1" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr class="disply1">
                                                                        <td class="noborder">SGST :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_sgst1" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr class="disply1">
                                                                        <td class="noborder">CGST :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_cgst1" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr class="disply1">
                                                                        <td class="noborder">Trans. Charge(+) :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_trancharge1" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr style="display: none">
                                                                        <td class="noborder">Expense(+) :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_expense1" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="noborder">Flat Dis.(-) :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_flat_dis1" runat="server"></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="noborder">Grand Total :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_grandtotal1" runat="server"></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="noborder">Round of :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_round_of1" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="noborder">Net Total :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_net_total1" runat="server"></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="noborder">Paid Amount :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_paid_amount1" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="noborder">Dues Amount :</td>
                                                                        <td class="noborder">
                                                                            <asp:Label ID="lbl_duesamount1" runat="server"></asp:Label></td>
                                                                    </tr>

                                                                </table>
                                                            </div>


                                                        </td>

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
        </div>



        <asp:HiddenField ID="hd_print_type" runat="server" />




        <script type="text/javascript">
            $(document).ready(function () {
                var PrintType = $('#<%= hd_print_type.ClientID %>').val();
                if (PrintType == "1") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";
                }
                else if (PrintType == "2") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " hidden";
                }
                else {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " hidden";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";
                }
            });





            function myFunction(PrintType) {

                if (PrintType == "1") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";

                    var StudentcopYInr = document.getElementById("studentcopYInr");
                    OfficecopY.classList.remove("hidden");
                    StudentcopY.classList.remove("hidden");
                    StudentcopYInr.classList.remove("extrClass");
                }
                else if (PrintType == "2") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " hidden";

                    var studentcopYInr = document.getElementById("studentcopYInr");
                    OfficecopY.classList.remove("hidden");
                    StudentcopY.classList.remove("showd");
                    studentcopYInr.classList.remove("extrClass");
                }
                else {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " hidden";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";

                    var studentcopYInr = document.getElementById("studentcopYInr");
                    studentcopYInr.className += " extrClass";
                    OfficecopY.classList.remove("showd");
                    StudentcopY.classList.remove("hidden");
                }
            }
        </script>
    </form>
</body>
</html>
