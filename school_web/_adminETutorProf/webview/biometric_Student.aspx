<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="biometric_Student.aspx.cs" Inherits="school_web._adminETutorProf.webview.biometric_Student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
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
            color: #ff2956;
            position: absolute;
            top: 5px;
            left: -23px;
        }

        .texbox-border {
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 1px solid #00000038;
        }

        .btn {
            padding: 2px 17px 2px 17px!important;
            margin: 5px 0px 0px 0px;
        }
        /******************Notification**********************/
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

        table {
            /*box-shadow: 0 1px 1px 0 rgb(0 0 0 / 14%), 0 7px 10px -5px rgb(244 67 54 / 40%);*/
            /*background: linear-gradient( 60deg,#f7807e,#e53935);*/
            border-radius: 0px;
            /*border: 1px dashed #d1c5c5!important;*/
            background: #fff!important;
            border-bottom: 0px solid #c8c5c5;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 5px 5px;
            vertical-align: middle;
            border-color: #ddd0;
            font-size: 12px;
            color: #000;
            padding: 6px 0px 5px 7px!important;
            text-align: center;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #ddd;
            text-align: center;
            padding: 0px 2px 0px 2px;
            font-size: 11px;
        }

        .form-control {
            display: block;
            width: 100%;
            height: 25px!important;
            font-size: 14px!important;
            padding: 2px 10px;
        }

        .table {
            margin-bottom: 9px!important;
        }
    </style>
    <link href="../../Autocomplete/jquery-ui.css" rel="stylesheet" />


    <script src="../../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>

    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="fullinfo">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 100%; height: auto;">
                        <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                    <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                        class="closenotificationpan" alt="" />
                </div>
            </div>

            <%-- <h2 class="messbox-sec-h2">Attendance Report</h2>--%>

            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont1 ">Class   </p>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont3">
                    <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                </p>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont1 ">Section   </p>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont3">
                    <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control"></asp:DropDownList>
                </p>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont1 ">Start Date </p>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont3">
                    <asp:TextBox ID="txt_date" runat="server" CssClass="calender-icon"></asp:TextBox>
                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                </p>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont1 ">End Date </p>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
                <p class="textcont3">
                    <asp:TextBox ID="txt_enddate" runat="server" CssClass="calender-icon"></asp:TextBox>
                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                </p>
            </div>

            <div class="clearfix"></div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: center">
                <asp:Button ID="btn_submit" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_submit_Click" />


            </div>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <h5 class="card-title" style="text-align: center;">
                    <asp:Label ID="lbl_month_year" runat="server" Style="color: #f81b1b;"></asp:Label>&nbsp;
                    <br />
                    Total Student:-
                    <asp:Label ID="lbl_total" runat="server" Style="color: #f81b1b;">0</asp:Label>

                </h5>
                <asp:GridView ID="grd_attendance" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" AllowPaging="false" Font-Bold="False" Style="margin-top: 0; width: 100%; overflow: scroll" class="table table-striped table-bordered dataTable">
                    <RowStyle />
                    <Columns>
                           <asp:TemplateField HeaderText="S.l No.">
                            <ItemTemplate>
                                <asp:Label ID="lbl_lm" runat="server" Text='<%#Bind("sl") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name">
                            <ItemTemplate>
                                <asp:Label ID="lbl_emp_name" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Adm. No.">
                            <ItemTemplate>
                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Roll No.">
                            <ItemTemplate>
                                <asp:Label ID="lbl_rollno" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class">
                            <ItemTemplate>
                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Session">
                            <ItemTemplate>
                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Attendance Log Detail">
                            <ItemTemplate>
                                <asp:Label ID="lbl_attendance" runat="server" Text='<%#Bind("Attendance") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </form>
</body>
</html>
