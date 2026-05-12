<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vehicle_Details.aspx.cs" Inherits="school_web.Admin.Vehicle_Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vehicle Details</title>
    <link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&display=swap" rel="stylesheet" />
    <link href="../assets/css/Print.css" rel="stylesheet" />
    <script src="../assets/js/jquery-1.10.2.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&display=swap" rel="stylesheet"/>');
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
        table tr td {
            padding: 7px 10px;
            border: 1px solid #ddd;
            font-size: 15px;
            font-weight: bold;
        }
        span {
            font-weight:bold;
        }
        th
        {
            padding: 6px 0px 7px 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="section">
            <div class="print_form">
                <div class="prnt-btn-sec">
                    <div class="prnt-btn-sec-cntr">
                        <div class="print-btn-sec">
                            <div class="noPrint" style="float: left">
                                <asp:Button ID="btn_back" runat="server" ToolTip="Back" CssClass="back-btn" OnClick="btn_back_Click" />
                            </div>
                            <div class="noPrint" style="float: right">
                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" CssClass="print-btn"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="print-pg-sec" id="tblPrintIQ" runat="server">
                    <div class="print_form-inr">
                        <div class="print_form-img">
                            <%--<img src="digital-marketing/img/nayab-fashion-logo.png" />--%>
                            <h2 class="success-htitle-h">Vehicle Details</h2>
                        </div>
                        <div class="print_form-dtls">
                            <table class="print_table">
                                <tr>
                                    <td colspan="4">
                                        <h2 class="messbox-sec-h2" style="width: 100%;">Vehicle Information</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Vehicle Name : </td>
                                    <td>
                                        <asp:Label ID="lbl_vehiclename" runat="server" Text=""></asp:Label>
                                    </td>


                                    <td>Vehicle Registration No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_Vehicle_no" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                  <%--  <td>Vehicle Route :</td>
                                    <td>
                                        <asp:Label ID="lbl_vehicle_rute" runat="server" Text=""></asp:Label>
                                    </td>--%>
                                    <td>Transport Type :</td>
                                    <td>
                                        <asp:Label ID="lbl_Transport_own" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>  Vehicle Registration Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_Vehicle_Registration_date" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td>Vehicle Registration Expiry Date</td>
                                    <td>
                                        <asp:Label ID="lbl_Vehicle_Registration_expiry" runat="server" Text=""></asp:Label>
                                    </td>
                                     <td>Vehicle Insurance Expiry Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_vechile_insurance_expirydate" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td>Pollution Expiry Date  :</td>
                                    <td>
                                        <asp:Label ID="lbl_Pollutionexpirydate" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Body Fitness Expiry Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_body_Fitness_Expiry" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    
                                    <td>Vehicle Type :</td>
                                    <td>
                                        <asp:Label ID="lbl_Vehicle_type" runat="server" Text=""></asp:Label>
                                    </td>
                                     <td>No. of Seat :</td>
                                    <td colspan="3">
                                        <asp:Label ID="lbl_no_seat" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                
                                <%-- ======================= --%>
                                <tr>
                                    <td colspan="4">
                                        <h2 class="messbox-sec-h2" style="width: 100%;">Vehicle Owner/Driver Information</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Vehicle Owner Name : </td>
                                    <td>
                                        <asp:Label ID="lbl_Vehicle_Owner_Name" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Vehicle Owner Mobile No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_Vehicle_Owner_mobile_no" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>



                                <tr>
                                    <td>Vehicle Driver Name : </td>
                                    <td>
                                        <asp:Label ID="lbl_Vehicle_drivername" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Driver Mobile No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_driver_mobile_no" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Driver Licence No. : </td>
                                    <td>
                                        <asp:Label ID="lbl_driver_licence" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Driver licence expiry Date :</td>
                                    <td>
                                        <asp:Label ID="lbl_driver_licence_expiry" runat="server" Text=""></asp:Label>

                                    </td>
                                </tr>



                            </table>
                            <table class="print_table">
                                <tr>
                                    <td colspan="4">
                                        <h2 class="messbox-sec-h2" style="width: 100%;">Vehicle Warden Information</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Vehicle Warden  : </td>
                                    <td>
                                        <asp:Label ID="lbl_Vehicle_Warden" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Vehicle Warden Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_Vehicle_Warden_name" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td>Warden Mobile No.  : </td>
                                    <td>
                                        <asp:Label ID="lbl_Warden_mobile_no" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>Addhar No. :</td>
                                    <td>
                                        <asp:Label ID="lbl_Warden_aadhar_no" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Warden Address   : </td>
                                    <td>
                                        <asp:Label ID="lbl_Warden_address" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>

                                <%-- ======================= --%>
                               
                                <tr>
                                    <td colspan="4">

                                        <asp:GridView ID="grd_doc" runat="server"
                                            Style="text-align: left; font-family: arial; font-size: 12px; height: auto; margin-top: 4px; margin-bottom: 16px; margin-right: 0px;" CssClass="table table-bordered"
                                            Width="100%"
                                            AutoGenerateColumns="False" OnRowDataBound="grd_doc_RowDataBound">

                                            <Columns>
                                                <asp:TemplateField HeaderText="S.L. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="70px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Document Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_document_name" runat="server" Font-Names="Arial" Text='<%#Bind("Doc_name") %>'></asp:Label>
                                                        <asp:Label ID="lbl_Description_ID_No" runat="server" Font-Names="Arial" Text='<%#Bind("Doc_id") %>' Visible="false"></asp:Label>

                                                        <asp:Label ID="lbl_mandatory" runat="server" Font-Names="Arial" Text='*' ForeColor="Red" Visible="false" Style="display: none"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <a id="a1" runat="server" style="padding: 5px 11px 5px 11px; background-color: #03ce2f; font-family: ebrima; font-size: 12px; color: Black; text-decoration: none;"
                                                            target="_blank">Download</a>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </td>

                                </tr>
















                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
