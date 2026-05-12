<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salary-slip.aspx.cs" Inherits="cms_web.Payroll.print.salary_slip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salary Slip</title>

    <link href="../../assets/css/Print.css" rel="stylesheet" />
    <script src="../js/jquery-1.10.2.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../../assets/css/Print.css" rel="stylesheet" type="text/css" />');
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
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hd_order_id" runat="server" />
        <asp:HiddenField ID="hd_customer_id" runat="server" />
        <div class="invoice-sec">
            <div class="prnt-btn-sec">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec" style="text-align: center">
                        <div class="noPrint">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" Style="background-color: #009f25; border: 5px solid #009f25; cursor: pointer; float: none; display: inline-block; width: 40px; height: 40px;"
                                ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tblPrintIQ" runat="server">
                <div class="invoice-inr-sec1">
                    <div class="slry-invoice-wpr">

                        <div class="slry-head-div">
                            <div class="slry-head-logo-div">
                                <asp:Image ID="Image1" runat="server" />
                            </div>
                            <div class="slry-head-txt-div">
                                <asp:Label ID="lbl_firm_name" class="slry-head-firm-name-h" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl_frm_location" class="slry-head-firm-loction-h" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl_salary_date" class="slry-head-month-name-p" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="emp-info-dv">
                            <div class="emp-info-dv1">
                                <asp:Label ID="lbl_name" runat="server" class="emp-info-lbl-p"></asp:Label>
                                <asp:Label ID="lbl_desig" runat="server" class="emp-info-lbl-p"></asp:Label>
                                <asp:Label ID="lbl_pf_ac" runat="server" class="emp-info-lbl-p"></asp:Label>
                            </div>
                            <div class="emp-info-dv2">
                                <asp:Label ID="lbl_e_branch" runat="server" class="emp-info-lbl-p"></asp:Label>
                                <asp:Label ID="lbl_dep" runat="server" class="emp-info-lbl-p"></asp:Label>
                                <asp:Label ID="lbl_esic_no" runat="server" class="emp-info-lbl-p"></asp:Label>
                            </div>
                            <div class="emp-info-dv3">
                                <asp:Label ID="lbl_emp_id" runat="server" class="emp-info-lbl-p"></asp:Label>
                                <asp:Label ID="Label5" runat="server" class="emp-info-lbl-p" Text="UAN : "></asp:Label>
                                <asp:Label ID="lbl_pan" runat="server" class="emp-info-lbl-p"></asp:Label>
                            </div>
                        </div>
                        <div class="emp-info-dv p-bdrbtm-dhed">
                            <div class="emp-info-dv1">
                                <asp:Label ID="lbl_days_in_month" runat="server" class="emp-info-lbl-p"></asp:Label>
                                <asp:Label ID="lbl_wisking_days" runat="server" class="emp-info-lbl-p"></asp:Label>
                                <asp:Label ID="lbl_paid_days" runat="server" class="emp-info-lbl-p"></asp:Label>
                            </div>
                            <div class="emp-info-dv2">
                                <asp:Label ID="Label4" runat="server" class="emp-info-lbl-p" Text="CL Availed : 0.00"></asp:Label>
                                <asp:Label ID="Label6" runat="server" class="emp-info-lbl-p" Text="PL Availed : 0.00"></asp:Label>
                                <asp:Label ID="Label7" runat="server" class="emp-info-lbl-p" Text="Sl Availed : 0.00"></asp:Label>
                            </div>
                            <div class="emp-info-dv3">
                                <asp:Label ID="Label8" runat="server" class="emp-info-lbl-p" Text="Balance CL : 0.00"></asp:Label>
                                <asp:Label ID="Label9" runat="server" class="emp-info-lbl-p" Text="Balance PL : 0.00"></asp:Label>
                                <asp:Label ID="Label10" runat="server" class="emp-info-lbl-p" Text="Balance SL : 0.00"></asp:Label>
                            </div>
                        </div>

                        <div class="slry-emp-dt-tbl-div">


                            <div class="print-pg-grid1-dv">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="rp-head-td">Earnings</th>
                                            <th class="rp-amt-td">Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rp_income" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="rp-head-td">
                                                        <asp:Label ID="lbl_Income_head" runat="server" Text='<%#Bind("Income_head")%>'></asp:Label>
                                                    </td>
                                                    <td class="rp-amt-td">
                                                        <asp:Label ID="lbl_Income_Value" runat="server" Text='<%#Bind("Income_Value")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <div class="print-pg-grid2-dv">
                                <table>
                                    <thead>
                                        <tr>
                                            <th class="rp-head-td">Deduction</th>
                                            <th class="rp-amt-td">Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rp_deduction" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="rp-head-td">
                                                        <asp:Label ID="lbl_Deduction_head" runat="server" Text='<%#Bind("Deduction_head")%>'></asp:Label>
                                                    </td>
                                                    <td class="rp-amt-td">
                                                        <asp:Label ID="lbl_Deduction_value" runat="server" Text='<%#Bind("Deduction_value")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <table>
                                    <tr id="pnl_advane" runat="server" visible="false">
                                        <td class="rp-head-td">Advance Adjust</td>
                                        <td class="rp-amt-td">
                                            <asp:Label ID="txt_advance_adjust" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>

                            <div class="net-ttl-dv-prnt">
                                <div class="print-pg-grid1-net-dv">
                                    <table>
                                        <tr class="bdr-tp-btmboth">
                                            <td class="rp-head-td">Total Earnings</td>
                                            <td class="rp-amt-td">
                                                <asp:Label ID="txt_total_earning" runat="server" Text="" class="fnt-bld"></asp:Label></td>
                                        </tr>
                                        <tr class="bdr-tp-btmboth" style="height: 32px;">
                                            <td class="rp-head-td"></td>
                                            <td class="rp-amt-td">
                                                <asp:Label ID="Label1" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="print-pg-grid2-net-dv">
                                    <table>
                                        <tr class="bdr-tp-btmboth">
                                            <td class="rp-head-td">Total Deduction</td>
                                            <td class="rp-amt-td">
                                                <asp:Label ID="txt_total_deduction" runat="server" Text="Label" class="fnt-bld"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr class="p-bdrbtm-dhed">
                                            <td class="rp-head-td">Net Salary</td>
                                            <td class="rp-amt-td">
                                                <asp:Label ID="txt_net_salary" runat="server" class="fnt-bld"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>


                            <div class="in-wrd-rupee">
                                <asp:Label ID="lbl_rupees_in_words" runat="server" class="in-wrd-rupee-p"></asp:Label>
                            </div>

                            <div class="auth-div">
                                <p class="auth-div-p1">
                                    <asp:Label ID="lbl_firm_name1" runat="server" Text="Label"></asp:Label>
                                </p>

                                <p class="auth-div-p1-sig">Authorised Signatory</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
