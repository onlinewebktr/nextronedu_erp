<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_school_calendar.aspx.cs" Inherits="school_web.LMS_VC_Admin.print.Print_school_calendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Calendar</title>
    <style type="text/css">
        body, #form1 {
            padding: 0px;
            font-family: sans-serif,Arial;
            font-size: 14px;
            font-weight: normal;
        }

        .main {
            margin: 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
        }

        .main_auto {
            width: 980px;
            margin: 0px auto;
            height: auto;
            padding: 0px;
        }

        .main_const {
            width: 99.5%;
            height: auto;
            margin: 30px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            border: 1px solid black;
        }

            .main_const table {
                margin: 0px;
                padding: 0px;
                float: left;
                height: auto;
                width: 100%;
            }

        @media print {
            .noPrint {
                display: none;
            }

            .cnt_data {
                page-break-after: always;
                padding-bottom: 200px;
            }
        }

        th {
            text-align: center;
            padding: 5px 5px 5px 5px;
            border-collapse: collapse;
        }

        td {
            text-align: left;
            padding: 5px 5px 5px 5px;
            border-collapse: collapse;
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
            <div class="main_auto">
                <div class="main_const" style="position: relative;">
                    <div class="printTopDiv noPrint">

                        <a href="javascript:" onclick="printit()" style="margin-left: 12px; margin-top: 12px; float: left;">
                            <img src="../../images/printer.png" class="printBtn noPrint" style="height: 36px; width: 36px;" />
                        </a>
                        <asp:ImageButton ID="btn_back" runat="server" ImageUrl="~/images/Back.jpg" Height="36px" OnClick="btn_back_Click" Style="margin-left: 843px" />

                    </div>

                    <div class="heading" style="text-align: center; height: 150px;">
                        <div style="margin: 0px; padding: 0px; float: left; height: 150px; width: 150px; padding: 0px 0px 0px 10px;">
                            <img alt="0" runat="server" id="log1" style="border-style: none; border-color: inherit; border-width: medium; margin: 14px auto; padding: 1px 7px 0px 0px; height: 120px; width: 120px;" />
                        </div>
                        <div style="margin: 0px; padding: 0px; float: left; height: 150px;width: 755px;">
                            <div style="margin: 7px 0px 0px 33px; width: 96%; padding: 0px; float: left; font-size: 18px; text-align: right!important; height: auto;">
                                Affiliation No. :
                                <asp:Label ID="lbl_affilation_no" runat="server"></asp:Label>


                            </div>

                            <h1 style="margin: 0px 0px 0px 33px; width: 96%; padding: 0px; float: left; height: auto; text-align: left; vertical-align: middle; font-size: 42px; text-decoration: underline;">
                                <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                <br />

                            </h1>

                            <div style="margin: 7px 0px 0px 33px; padding: 0px; float: left; height: auto; text-align: left; vertical-align: middle; font-size: 17px; width: 678px;">
                                <asp:Label ID="lbl_address" runat="server"></asp:Label>


                            </div>
                            <div style="margin: 1px 0px 0px 33px; padding: 0px; float: left; height: auto; text-align: left; vertical-align: middle; font-size: 17px; text-decoration: underline; width: 678px;">
                                <asp:Label ID="lbl_emaiid" runat="server"></asp:Label>


                            </div>
                        </div>



                    </div>
                    <div class="row_line">
                        <asp:GridView ID="grdprint" runat="server" CssClass="grid1" AutoGenerateColumns="False" Font-Bold="False"
                            BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Style="border-left: 0px solid #000; border-right: 1px solid #000; text-align: center; width: 100%;">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Day">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Day")%>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_type" runat="server" Text='<%#Bind("Type")%>'></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle Width="95" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Details">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_type" runat="server" Style="word-break: break-all" Text='<%#Bind("Details")%>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>


                        </asp:GridView>



                    </div>
                    <div class="block" style="height: auto; border-bottom: 1px solid; border-top: 0px solid #000; margin: 30px 0px 0px 0px; float: left; width: 100%;">

                        <table cellpadding="0" cellspacing="0" style="float: right; width: 200px; margin: 0px; padding: 0px; height: 22px; padding: 6px 20px 6px 0px; font-size: 15px">
                            <tr>
                                <td style="text-align: right; font-weight: bold;">Principale
                                </td>

                            </tr>

                        </table>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
