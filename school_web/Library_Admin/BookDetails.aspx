<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="school_web.Library_Admin.BookDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Details</title>
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
        }

        .top {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .topcell_left {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 50%;
            float: left;
            text-align: left;
        }

        .topcell_right {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 50%;
            float: left;
            text-align: right;
        }

        .heading {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .leftlogoheading {
            margin: 0px;
            padding: 0px;
            height: 110px;
            width: 13%;
            float: left;
        }

        .righttextheading {
            margin: 0px;
            padding: 0px;
            height: 110px;
            width: 80%;
            float: left;
        }

        @media print {
            .noPrint {
                display: none;
            }
        }

        .print_form-img img {
            margin: 0px auto;
            float: left;
            padding: 0px 10px 0px 0px;
            border-right: 1px solid #ffffff;
        }

        .print_form-dtls {
            margin: 7px 0px 0px 0px;
            padding: 10px 0px 0px 0px;
            width: 100%;
            height: auto;
            float: left;
            border-top: 1px solid #131312;
        }
    </style>
    <script type="text/javascript">
        function PrintWindow() {
            window.print();
            // CheckWindowState();
        }

        function CheckWindowState() {
            if (document.readyState == "complete") {
                window.close();
            }
            else {
                setTimeout("CheckWindowState()", 2000)
            }
        }

        PrintWindow();
    </script>


    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <section class="section">
            <div class="print_form">
                <div class="prnt-btn-sec">
                    <div class="prnt-btn-sec-cntr">
                        <div class="print-btn-sec">
                            <div class="noPrint" style="float: left">
                                <asp:Button ID="btn_back" runat="server" ToolTip="Back" CssClass="back-btn noPrint" OnClick="btn_back_Click" />
                            </div>
                            <div class="noPrint" style="float: right">
                                <%--OnClientClick="return PrintPanel()"--%>
                                <asp:LinkButton ID="print1" OnClick="print1_Click" runat="server" ToolTip="Print" CssClass="print-btn noPrint"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="print-pg-sec" id="tblPrintIQ" runat="server">
                    <div class="print_form-inr">
                        <div class="print_form-img">

                            <div class="top" style="display:none">
                                <div class="topcell_left">
                                    Affiliation No :
                        <asp:Label ID="lbl_affiliation_no" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="topcell_right">
                                    School No. :
                        <asp:Label ID="lbl_schoolno" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="heading">
                                <div class="leftlogoheading">
                                    <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px" />
                                </div>
                                <div class="righttextheading">
                                    <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                        <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                    </h1>

                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                    </div>
                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                    </div>
                                    <div runat="server" id="contact_no" visible="false" style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                        Contact No. :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>




                        </div>
                        <div class="print_form-dtls">
                            <table class="print_table">
                                <tr>
                                    <td colspan="4">
                                        <h2 class="messbox-sec-h2" style="width: 100%;">Book Information</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Book Type : </td>
                                    <td>
                                        <asp:Label ID="lbl_booktype" runat="server" Font-Bold="true"></asp:Label>
                                    </td>


                                    <td>Book Catogery :</td>
                                    <td>
                                        <asp:Label ID="lbl_book_catogery" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Book Status :</td>
                                    <td>
                                        <asp:Label ID="lbl_bookstatus" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>Class :</td>
                                    <td>
                                        <asp:Label ID="lbl_class" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Name of the Book :</td>
                                    <td>
                                        <asp:Label ID="lbl_nameofthebook" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>Author Name :</td>
                                    <td>
                                        <asp:Label ID="lbl_AuthorName" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Publication :</td>
                                    <td>
                                        <asp:Label ID="lbl_Publication" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>Volume Name  :</td>
                                    <td>
                                        <asp:Label ID="lbl_EnterVolumePart" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>

                                <tr>

                                    <td>Edition :</td>
                                    <td>
                                        <asp:Label ID="lbl_Edition" runat="server" Font-Bold="true"></asp:Label>
                                    </td>

                                    <td>Publication Year :</td>
                                    <td>
                                        <asp:Label ID="lbl_PublicationYear" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>


                                    <td>No Of Pages : </td>
                                    <td>
                                        <asp:Label ID="lbl_NoOfPages" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>Location/Sub Location : </td>
                                    <td>
                                        <asp:Label ID="lbl_Location" runat="server" Font-Bold="true"></asp:Label>

                                        <asp:Label ID="lbl_sublocation" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>






                                <tr>

                                    <td>ISBN  :</td>
                                    <td>
                                        <asp:Label ID="lbl_ISBN_Num" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>Invoice No : </td>
                                    <td>
                                        <asp:Label ID="lbl_invoice" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>

                                    <td>Price :</td>
                                    <td>
                                        <asp:Label ID="lbl_price" runat="server" Font-Bold="true"></asp:Label>
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
