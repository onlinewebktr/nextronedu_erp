<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inventory.aspx.cs" Inherits="school_web.Admin.slip.inventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Print Inventory</title>
    <style>
        body, #form1 {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            position: absolute;
            font-family: sans-serif;
            font-size: 13px;
        }

        .main {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .mainautot {
            margin: 0px auto;
            padding: 0px;
            height: auto;
            width: 97%;
        }

        .mainwith {
            margin: 0px;
            padding: 5px;
            height: auto;
            width: 100%;
            float: left;
            border: 1px solid #000;
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
            width: 20%;
            float: left;
        }

        .righttextheading {
            margin: 0px;
            padding: 0px;
            height: 110px;
            width: 80%;
            float: left;
        }

        .slipno {
            margin: 0px;
            padding: 0px 0px 4px 0px;
            height: auto;
            width: 100%;
            float: left;
            border-bottom: 2px solid #000;
        }

        .slipno_left {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 25%;
            float: left;
            text-align: left;
        }

        .slipno_middle {
            margin: 0px;
            padding: 0px 0px 3px 0px;
            height: auto;
            width: 100%;
            float: left;
            text-align: center;
            font-size: 20px;
        }

        .slipno_right {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 20%;
            float: left;
            text-align: right;
        }

        .studentdetails {
            margin: 4px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }


        .student_left {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 40%;
            float: left;
            text-align: left;
        }

        .student_middle {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 30%;
            float: left;
            text-align: center;
        }

        .student_right {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 30%;
            float: left;
            text-align: right;
        }

        .pay_particular {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .td2 {
            text-align: right;
            padding: 0px 3px 0px 0px;
        }

        .td3 {
            text-align: left;
            padding: 0px 0px 0px 5px;
        }

        .footer {
            margin: 10px 0px 0px 0px;
            border-top: 2px solid #000;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .footer_left {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 50%;
            float: left;
            text-align: left;
        }

        .footer_middle {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 20%;
            float: left;
            text-align: right;
        }

        .footer_right {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 30%;
            float: left;
            text-align: right;
        }

        .footer_auth_sig {
            margin: 115px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
            text-align: left;
        }

        .student_left-p-info {
            margin: 3px 0px 3px 0px;
            padding: 0px;
            height: auto;
            width: 33.3%;
            float: left;
        }

            .student_left-p-info p {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                height: auto;
                width: 31%;
                float: left;
                font-weight: 500;
            }

            .student_left-p-info i {
                width: 10px;
                float: left;
                font-weight: 600;
                font-style: inherit;
            }

            .student_left-p-info span {
                margin: 0px 0px 0px 0px;
                padding: 0px;
                height: auto;
                width: 64%;
                float: left;
                font-weight: 600;
            }

        table {
            border-spacing: 0;
            border-collapse: collapse;
        }


        .table {
            border-collapse: collapse;
        }

            .table td, .table th {
                background-color: #fff;
            }

        * {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }


        :after, :before {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        table {
            width: 100%;
        }

            table tr th {
                padding: 5px 5px;
                text-align: left;
                border: 1px solid #ddd;
            }

            table tr td {
                padding: 5px 5px;
                text-align: left;
                border: 1px solid #ddd;
            }


        @media print {
            .noPrint {
                display: none;
            }
        }
    </style>
    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>

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
</head>
<body>
    <form id="form1" runat="server">

        <div class="main">

            <div class="mainautot">
                <div style="padding: 0px 0px 0px 0px; margin: 0px; height: 29px; width: 870px; float: left;">
                    <asp:Button ID="Button1" runat="server" Text="Back" class="noPrint" OnClick="btn_back_Click"
                        Style="float: left;" />
                    <asp:Button ID="btn_print" runat="server" Text="Printit" class="noPrint" OnClick="btn_print_Click"
                        Style="float: right;" />
                </div>
            </div>
            <div class="mainautot">


                <div class="mainwith">
                    <div class="top">
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
                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%; display: none">
                                Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="slipno">
                        <div class="slipno_middle">
                            <b style="font-weight: 500;">Inventory Transfer</b>
                        </div>
                        <div class="slipno_right" style="display: none">
                            Date :
                        <asp:Label ID="lbl_date" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>



                    <div class="pay_particular">
                        <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Date</th>
                                    <th>Floor</th>
                                    <th>Room</th>
                                    <th>Section</th>
                                    <th>Item Code</th>
                                    <th>Item Name</th>
                                    <th>Brand</th>
                                    <th>Model No.</th>
                                    <th>Sr. No.</th>
                                    <th>Unique Number</th>
                                    <th>Working Status</th>
                                    <th>Warranty</th>
                                    <th>Warranty Last Date</th>
                                    <th>Value</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rd_view" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_transfer_date" runat="server" Text='<%#Bind("Transfer_date")%>'></asp:Label>
                                            </td>

                                            <td style="text-align: left;">
                                                <asp:Label ID="Label5" runat="server" Text='<%#Bind("Floor")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("Room_name")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label7" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Item_id")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_item_name" runat="server" Text='<%#Bind("Item_name")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label8" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label9" runat="server" Text='<%#Bind("Modal_no")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label10" runat="server" Text='<%#Bind("Serial_no")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("Unique_key")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label11" runat="server" Text='<%#Bind("Working_status")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label12" runat="server" Text='<%#Bind("Is_warranty")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label13" runat="server" Text='<%#Bind("Expire_date")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label14" runat="server" Text='<%#Bind("Value")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
