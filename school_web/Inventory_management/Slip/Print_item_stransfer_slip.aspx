<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_item_stransfer_slip.aspx.cs" Inherits="school_web.Inventory_management.Slip.Print_item_stransfer_slip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../MasterAdmin/Slip/print.css" rel="stylesheet" />

    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>
    <style>
        span {
            text-transform: uppercase;
        }

        .containerNotFound {
            /* width: 25%; */
            margin-left: 2%;
            padding: 2%;
            text-align: center;
        }

        .branch {
            margin: 0px auto;
            /* float: left; */
            padding: 0;
            height: auto;
            width: 800px;
            position: relative;
        }

        .printSlip {
            width: 100%;
            margin: 0px auto;
        }

        th {
            font-weight: bold;
            font-size: 14px;
            padding: 5px;
        }

        td {
            padding: 5px;
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
    <style>
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
    </style>
    <style>
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
                    <img src="../../images/print-recipt.png" class="printBtn noPrint1" />
                </a>
                <asp:ImageButton ID="btn_back" runat="server" OnClientClick="down();" ImageUrl="../../images/backbtn.png" Height="36px" Style="margin-left: 245px" Width="37px" />

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
                                                        <asp:Image ID="img_header" runat="server" Style="margin: 0px auto; padding: 0px; width: 100%; border-bottom: 2px solid #bb5208;" />
                                                        <asp:Panel ID="headertext" runat="server" Visible="false">
                                                            <table style="margin-top: 5px;">
                                                                <tr>
                                                                    <td style="width: 100px; text-align: center; padding: 0px; border: 1px solid #a7a7a7;">
                                                                        <asp:Image ID="img_logo" runat="server" Style="width: 100px; margin: 0px 0px 0px 0px" />
                                                                    </td>
                                                                    <td style="padding: 0px;">

                                                                        <h2 style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 20px;">
                                                                            <asp:Label ID="lbl_hospital_name" runat="server" Style="font-size: 14px;"></asp:Label></h2>
                                                                        <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 20px;">
                                                                            <asp:Label ID="lbl_address1" runat="server" Style="font-size: 11px;"></asp:Label>
                                                                        </p>
                                                                        <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 20px;">
                                                                            <asp:Label ID="lbl_address2" runat="server" Style="font-size: 11px;"></asp:Label>
                                                                        </p>
                                                                        <h2 style="margin: 0px 0px 5px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 15px;">
                                                                            <asp:Label ID="lbl_email_mobile" runat="server" Style="font-size: 10px;"></asp:Label></h2>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="border-top" style="border: none;">
                                                    <div style="background-color: #ebf1cc; box-shadow: 5px 5px #526c74; width: 50%; margin: 0px auto; text-align: center; font-size: 17px;">Transfer Slip</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table style="text-align: left;">
                                                        <tr>
                                                            <td style="padding: 5px 0px 5px 0px;">Demand No / Transfer No.</td>
                                                            <td style="padding: 5px 0px 5px 0px; width: 400px">:-
                                                                                    <asp:Label ID="lbl_po_no" runat="server" Style="font-size: 11px;"></asp:Label>

                                                            </td>
                                                            <td style="padding: 5px 0px 5px 0px;">Date </td>
                                                            <td style="padding: 5px 0px 5px 0px;">:-
                                                                                    <asp:Label ID="lbl_date" runat="server" Style="font-size: 11px;"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px 0px 5px 0px;">Transfer From </td>
                                                            <td style="padding: 5px 0px 5px 0px;">:-
                                                                                    <asp:Label ID="lbl_transfer_by" runat="server" Text="Central Store" Style="font-size: 11px;"></asp:Label>

                                                            </td>
                                                            <td style="padding: 5px 0px 5px 0px;">Transfer To </td>
                                                            <td style="padding: 5px 0px 5px 0px;">:-
                                                                                    <asp:Label ID="lbl_transfer_to" runat="server" Style="font-size: 11px;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td style="padding: 5px 0px 5px 0px;">Transfer By </td>
                                                            <td style="padding: 5px 0px 5px 0px;">:-
                                                                                    <asp:Label ID="lbl_user_name" runat="server" Style="font-size: 11px;"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td colspan="4">
                                                    <div class="page">
                                                        <asp:GridView ID="GrdView_Generate_PO" runat="server" class="data-Table table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Quantity">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("unit")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Rate" runat="server" Text='<%#Bind("Purchase_Rate")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Total">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Total_rate" runat="server" Text='<%#Bind("Total_amount")%>'></asp:Label>
                                                                    </ItemTemplate>


                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="padding: 0px 30px 20px 0px; padding-left: 5px; text-align: right;">Grand Total  :-
                                                    <asp:Label ID="lbl_Total_rate" runat="server">
                                                       
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="padding: 30px 0px 0px 0px; padding-left: 5px;">
                                                    <p style="padding-top: 10px; float: left; padding-bottom: 5px; text-align: left; width: 100%">
                                                        <span>Receiver Name :- </span>
                                                        <asp:Label ID="lbl_receivername" runat="server"></asp:Label>

                                                    </p>
                                                    <p style="padding-top: 10px; float: left; padding-bottom: 5px; text-align: left; width: 100%">
                                                        <span>Remarks :- </span>
                                                        <asp:Label ID="lbl_remarks" runat="server"></asp:Label>

                                                    </p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 30px 0px 0px 0px; padding-left: 5px;">
                                                    <p style="padding-top: 20px; float: left; padding-bottom: 5px; text-align: center; width: 100%">

                                                        <asp:Label ID="Label2" runat="server" Text="Receiver Signature" Style="font-size: 13px; color: #ccc; float: left"></asp:Label>

                                                    </p>
                                                </td>
                                                <td colspan="2" style="padding: 30px 0px 0px 0px; padding-left: 5px;">
                                                    <p style="padding-top: 20px; float: left; padding-bottom: 5px; text-align: center; width: 100%">


                                                        <asp:Label ID="Label4" runat="server" Text="Authorised Signatory" Style="font-size: 13px; color: #ccc; float: right;"></asp:Label>
                                                    </p>
                                                </td>
                                            </tr>
                                        </tbody>


                                        <tfoot>
                                            <tr id="footer_sapace" runat="server">
                                                <td colspan="4" style="padding-top: 0px; padding-bottom: 0px; padding-left: 5px;">
                                                    <!--place holder for the fixed-position footer-->
                                                    <div id="page_footer_space" runat="server" class="page-footer-space">
                                                        <asp:Image ID="img_footer" runat="server" Style="margin: 0px auto; padding: 0px; width: 100%;" />
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
