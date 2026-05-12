<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="event-details.aspx.cs" Inherits="school_web.Student_Profile.webview.event_details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="favicon/ms-icon-144x144.png" />
    <meta name="theme-color" content="#ffffff" />

    <script src="../../js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <link href="../../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    <style>
        .messbox-sec-h2 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-size: 18px;
            line-height: 25px;
            font-weight: 500;
            text-align: center;
            color: #fff;
            background-color: #109be1;
        }

        .fullinfo {
            margin: 0px 0px 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
        }

        .textcont1 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 13px;
            line-height: 20px;
            color: #000;
            text-align: left;
        }

        .textcont3 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 12px;
            line-height: 20px;
            color: #000;
            text-align: left;
            font-weight: bold;
            position: relative;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #fdb351;
            position: absolute;
            top: 12px;
            right: 4px;
        }

        .texbox-border {
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 0px solid #00000038;
        }

        .btn {
            padding: 2px 17px 2px 17px !important;
            margin: 5px 0px 0px 0px;
        }
        /******************Notification**********************/
        .notificationpan {
            display: none;
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 133px !important;
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

        table {
            border-radius: 0px;
            background: #fff !important;
            border-bottom: 0px solid #c8c5c5;
            border-radius: 5px;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 5px 5px;
            vertical-align: middle;
            border-color: #ddd0;
            font-size: 12px;
            color: #000;
            padding: 6px 0px 5px 7px !important;
        }

        .my-btn {
            color: #fff;
            background-color: #fdb351 !important;
            border-color: #fdb351 !important;
            font-size: 15px !important;
            line-height: 23px !important;
            font-weight: 400 !important;
            /*-webkit-transition: all 0.9s;
            -o-transition: all 0.9s;
            -moz-transition: all 0.9s;
            transition: all 0.9s;*/
        }

            .my-btn:hover {
                color: #fff;
                background-color: #fdb351 !important;
            }
    </style>
</head>
<body style="background-color: #fffdc0;">
    <form id="form1" runat="server">
        <div class="fullinfo">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 100%; height: auto;">
                        <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">

                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12" style="margin-top: 4px; padding-right: 10px; padding-left: 10px; border-bottom: 0px dashed #beb7b7;">
                            <table class="table" style="width: 100%;">
                                <tr>
                                    <td style="padding: 2px 0px 2px 0px; position: relative; border-bottom: 1px solid #beb7b7; font-weight: bold"><i class="fa fa-calendar" aria-hidden="true" style="color: #000"></i>&nbsp;<asp:Label ID="lbl_Posted_Date" Font-Bold="true" runat="server" Text='<%#Bind("Date1") %>'></asp:Label>
                                        <asp:Panel ID="pnl_link" runat="server">
                                            <asp:Label ID="lbl_link" runat="server" Text='<%#Bind("Link") %>' Visible="false"></asp:Label>
                                            <a href="<%#Eval("Link") %>" target="_blank" style="position: absolute; right: 5px; top: 5px; font-size: 15px;"><i class="fa fa-external-link" aria-hidden="true"></i></a>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 2px 0px 2px 0px; ">
                                        <asp:Label ID="Label1"  Style="font-size: 13px; text-transform: capitalize;" runat="server" Text='<%#Bind("Heading") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 2px 0px 2px 0px;">
                                        <asp:Label ID="lbl_Details" runat="server" Text='<%#Bind("Details") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 2px 0px 2px 0px;">
                                        <asp:Label ID="lbl_Attachments" runat="server" Text='<%#Bind("Attachments") %>' Visible="false"></asp:Label>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <a href='<%#Eval("Attachments") %>' download target="_blank" style="display: block; padding: 0px 0px 0px 0px; font-size: 22px; color: #0066CC; text-decoration: none; float: left;"><i class="fa fa-download" aria-hidden="true"></i></a>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 2px 0px 2px 0px; text-align: left;">
                                        <asp:Button ID="btn_submit" runat="server" Text="Back" CssClass="btn btn-primary my-btn" OnClick="btn_submit_Click" />
                                    </td>
                                </tr>
                            </table>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
