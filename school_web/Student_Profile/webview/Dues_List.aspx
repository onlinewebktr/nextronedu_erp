<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dues_List.aspx.cs" Inherits="school_web.Student_Profile.webview.Dues_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dues List</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>

    <link href="../../assets/css/app.css" rel="stylesheet" />
    <link href="../../css/bootstrap.css" rel="stylesheet" />

    <style>
        #form1, body {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            font-family: sans-serif;
        }

        .main {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }

        .notificationpan {
            display: none;
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 133px!important;
            right: 10px;
            padding: 10px 10px;
            width: 290px;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgba(154, 154, 154, 0.8);
        }


        .closenotificationpan {
            position: absolute;
            margin: 0px 0px 0px 0px;
            top: 6px;
            right: 6px;
            cursor: pointer;
        }

        #notification {
            margin: 0px;
            padding: 0px;
            position: relative;
            z-index: 999;
        }

        .fnd-box-row-wpr-h {
            margin: 0px;
            padding: 5px 5px 4px 5px!important;
            width: 100%;
            float: left;
            font-size: 20px;
            border-bottom: 1px solid #ddd;
        }

        input[type="radio"], input[type="checkbox"] {
            margin: 4px 0 0;
            line-height: normal;
            height: 21px!important;
            width: 21px!important;
        }

        .form-control {
            display: block;
            width: 100%;
            height: 28px;
            padding: 6px 12px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
        }

        .table {
            width: 100%;
            max-width: 100%;
            margin-bottom: 0px;
        }

            .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
                padding: 2px 3px 3px 4px;
                line-height: 1.42857143;
                vertical-align: top;
                border-top: 1px solid #ddd;
            }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <div class="fnd-box-wpr" style="margin: 5px 0px 0px 0px;">
                <div id="notification">
                    <div id="pan" class="notificationpan">
                        <div style="float: left; width: 100%; height: auto;">
                            <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                        </div>

                    </div>
                </div>
                
               
                <div class="fnd-box-wpr-inr" style="display:none">

                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Month">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Month" runat="server" Text='<%#Bind("Month") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <div style="display: block">

                    <div class="fnd-box-wpr-inr">
                        <asp:Panel ID="pnl_month_wise_fee_details" runat="server" Visible="false">
                            <h2 class="fnd-box-row-wpr-h">Month Wise Dues List</h2>
                           
                                <table style="width: 100%;" class="table table-hover table-bordered">

                                    <tr>
                                        <th>Month</th>
                                        <th>Fees Head</th>
                                        <th>Fees Amt.</th>
                                        <th>Dis.</th>
                                        <th>Paid Prev.</th>
                                        <th>Payable</th>
                                    </tr>


                                    <asp:Repeater ID="rp_fee_details" runat="server" OnItemDataBound="rp_fee_details_ItemDataBound">
                                        <ItemTemplate>
                                            <tr id="row" runat="server">
                                                <td>
                                                    <asp:Label ID="lbl_mnth" runat="server" Text='<%#Bind("months") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcontent" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("disc_amount") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_pre_paid" runat="server" Text='<%#Bind("previously_paid") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_tot_pble" runat="server" Text='<%#Bind("total_payable") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <tr>
                                        <th colspan="2">Total :
                                        </th>
                                        <th>
                                            <asp:Label ID="lbl_fee_amount" runat="server" Text=""></asp:Label></th>
                                        <th>
                                            <asp:Label ID="lbl_discount" runat="server" Text=""></asp:Label></th>
                                        <th>
                                            <asp:Label ID="lbl_paid_prev" runat="server" Text=""></asp:Label></th>
                                        <th>
                                            <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label></th>

                                    </tr>
                                </table>
                            
                        </asp:Panel>
                    </div>
                </div>
               


            </div>

        </div>
    </form>
</body>
</html>
