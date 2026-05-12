<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_slip.aspx.cs" Inherits="school_web.Admin.slip.payment_slip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>
    <style type="text/css">
        .tdstyle {
            padding: 2px 5px 0px 5px;
            font-weight: bold;
        }

        th {
            padding: 5px 0px 5px 0px;
        }

        .branch {
            margin: 0px auto;
            /* float: left; */
            padding: 0;
            height: auto;
            width: 400px;
            position: relative;
        }

        .printSlip {
            width: 100%;
            margin: 0px auto;
        }

        .containerNotFound {
            /* width: 25%; */
            margin-left: 2%;
            padding: 2%;
            text-align: center;
        }

        @media print {
            .noPrint {
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
        function changeHashOnLoad() {
            window.location.href += "#";
            setTimeout("changeHashAgain()", "50");
        }

        function changeHashAgain() {
            window.location.href += "1";
        }

        var storedHash = window.location.hash;
        window.setInterval(function () {
            if (window.location.hash != storedHash) {
                window.location.hash = storedHash;
            }
        }, 50);


    </script>
    <script language="javascript" type="text/javascript">
        function down() {
            window.top.close();
        }
    </script>
    <link href="account_print.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="divSlip">
            <asp:Label ID="lbl_session" runat="server" Visible="false"></asp:Label>
            <div class="printTopDiv noPrint">

                <a href="javascript:" onclick="printit()">
                    <img src="../../images/print-recipt.png" class="printBtn noPrint" />
                </a>
                <asp:ImageButton ID="btn_back" runat="server" OnClientClick="down();" ImageUrl="../../images/backbtn.png" Height="36px" Style="margin-left: 245px" Width="37px" />

            </div>
            <div class="container">
                <div class="row">
                    <div class="branch" id="print_page">


                        <div style="margin: 0px auto; margin: 0px; float: left; height: auto; width: 100%; padding: 0px 0px 0px 0px; border-collapse: collapse;">


                            <div class="row" style="width: 100%; float: left;">
                                <div class="printSlip">

                                    <table class="data-Table">
                                        <thead>
                                            <tr>
                                                <td colspan="4" class="border-top" style="border: none;">
                                                    <asp:Image ID="img_header" runat="server" Style="margin: 0px auto; padding: 0px; float: none; width: 100%;" />
                                                    <asp:Panel ID="headertext" runat="server" Visible="false">
                                                        <table>
                                                            <tr>
                                                                <td style="width: 70px; text-align: center; padding: 0px; border: 0px solid #a7a7a7;">
                                                                    <asp:Image ID="img_logo" runat="server" Style="width: 100%; margin: 0px 0px 0px 0px" />
                                                                </td>
                                                                <td style="padding: 0px;">

                                                                    <h1 style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 30px;">
                                                                        <asp:Label ID="lbl_hospital_name" runat="server" Style="font-size: 20px;"></asp:Label></h1>
                                                                    <p style="margin: 0px 0px 0px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 20px;">
                                                                        <asp:Label ID="lbl_address1" runat="server" Style="font-size: 12px;"></asp:Label>
                                                                    </p>
                                           
                                                                    <p style="margin: 0px 0px 5px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 20px;">
                                                                        <span style="font-size: 12px;">Email:-</span>
                                                                        <asp:Label ID="lbl_email" runat="server" Style="font-size: 12px;"></asp:Label>
                                                                        <span style="font-size: 12px;">Tel./Mob. No.</span>
                                                                        <asp:Label ID="lbl_mobile" runat="server" Style="font-size: 12px;"></asp:Label>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <tr>
                                                <td colspan="4" class="border-top border-bottom" style="text-align: center; padding: 5px 0px 5px 0px;">
                                                    <h3 style="padding: 2px; margin: 0px; font-size: 14px;">
                                                        <asp:Label ID="lbl_vouchertype" runat="server" Text="" Font-Bold="true" Style="font-size: 14px;"></asp:Label>
                                                        Voucher</h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 10px 0px 10px 0px;">
                                                    <p style="float: left; color: maroon">
                                                        Slip No :  
                                                    <asp:Label ID="lbl_receipt_no" runat="server" Text=""></asp:Label>
                                                    </p>

                                                </td>
                                                <td style="padding: 10px 0px 10px 0px;"></td>
                                                <td style="padding: 10px 0px 10px 0px;"></td>
                                                <td style="padding: 10px 0px 10px 0px; width: 220px;">
                                                    <p style="float: left; color: maroon">
                                                        Date  : 
                                                    <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </td>
                                            </tr>




                                            <tr>
                                                <td colspan="4" style="padding: 5px 0px 5px 0px;">
                                                    <asp:GridView ID="grd_view" runat="server" AutoGenerateColumns="false" Style="text-align: center;">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Particular">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Account_Name" runat="server" Text='<%#Bind("Account_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    Rs.<asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Credit")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>


                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="padding: 100px 0px 5px 0px;">
                                                    <table>
                                                        <tr>
                                                            <td class="tdstyle" style="text-align: left; padding: 5px 0px 5px 0px; width: 120px;">
                                                                <p style="float: left; color: maroon">Through</p>
                                                                <p style="float: right; color: maroon">
                                                                    :
                                                                </p>
                                                            </td>
                                                            <td style="text-align: left; padding: 5px 0px 5px 0px;">
                                                                <asp:Label ID="lbl_through" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdstyle" style="text-align: left; padding: 5px 0px 5px 0px; width: 120px;">
                                                                <p style="float: left; color: maroon">Remarks</p>
                                                                <p style="float: right; color: maroon">
                                                                    :
                                                                </p>
                                                            </td>
                                                            <td style="text-align: left; padding: 5px 0px 5px 0px;">
                                                                <asp:Label ID="lbl_remarks" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>

                                                    </table>


                                                    <asp:Label ID="lbl_msg" runat="server" Text="" Style="line-height: 20px; text-transform: initial;"></asp:Label>

                                                </td>
                                            </tr>
                                            <tr>

                                                <td class="border-top border-bottom" colspan="4" style="padding: 5px 10px 5px 10px; vertical-align: top; text-align: right;">&nbsp;
                                                <span>Total Amt.</span> =  Rs.<asp:Label ID="lbl_total_amount" runat="server"></asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="border-top border-bottom">Rupees In Word: 
                                                <asp:Label ID="lbl_inwords" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="lbl_companyname" runat="server" Text="" Style="float: right; font-weight: bold; margin-right: 10px;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="padding-right: 30px; padding-bottom: 0px; padding-left: 5px;">
                                                    <p style="padding-top: 50px; float: left; padding-bottom: 5px; text-align: center;">
                                                        <asp:Label ID="Label1" runat="server" Text="Receiver's Signature" Style="font-size: 15px; color: #000;"></asp:Label><br />
                                                        <br />
                                                    </p>
                                                    <p style="padding-top: 50px; float: right; padding-bottom: 5px; text-align: center;">
                                                        <asp:Label ID="lbl_company" runat="server" Text="Signature" Style="font-size: 15px; color: #000;"></asp:Label><br />
                                                        <br />
                                                    </p>
                                                </td>
                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr id="footer_sapace" runat="server">
                                                <td colspan="4" style="padding-top: 0px; padding-bottom: 0px; padding-left: 5px;">
                                                    <!--place holder for the fixed-position footer-->
                                                </td>
                                            </tr>
                                        </tfoot>

                                    </table>
                                </div>
                            </div>

                            <div class="row" style="width: 100%; float: left; margin: 10px 0px 10px 0px;">
                                <p style="text-align: center; margin: 10px auto; border-bottom: 1px dotted #000;">
                                </p>
                            </div>



                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
