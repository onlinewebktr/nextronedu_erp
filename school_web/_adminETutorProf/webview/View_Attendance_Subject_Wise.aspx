<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Attendance_Subject_Wise.aspx.cs" Inherits="school_web._adminETutorProf.webview.View_Attendance_Subject_Wise" %>

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
    <script src="../../assets/js/bootstrap.min.js"></script>

    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>

    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Raleway:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap" rel="stylesheet" />
    <link href="../../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />

    <style>
        body {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-family: 'Poppins', sans-serif;
        }

        h1, h2, h3, h4, h5, h6 {
            font-family: 'Poppins', sans-serif;
        }

        p {
            font-family: 'Poppins', sans-serif;
        }

        a {
            font-family: 'Poppins', sans-serif;
        }

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

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > .table-bordered > tbody > tr > .table-bordered > tfoot > tr > th {
            background: #e1dddd!important;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #a29b9b;
            text-align: center;
            padding: 3px 4px 3px 5px;
            font-size: 11px;
        }

        .form-control {
            display: block;
            width: 100%;
            height: 25px!important;
            font-size: 13px!important;
            padding: 1px 5px;
        }

        .table {
            margin-bottom: 9px!important;
        }

        .ui-datepicker-trigger {
            display: none;
        }

        .container {
            padding-right: 10px;
            padding-left: 10px;
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

            }).attr("readonly", "true");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
        <div class="fullinfo">
            <div style="margin: 0px; padding: 0px 5px; float: left; height: auto; width: 100%; position: relative">
           <%--     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>
                        <div id="notification">
                            <div id="pan" class="notificationpan">
                                <div style="float: left; width: 100%; height: auto;">
                                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                                </div>
                                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                                    class="closenotificationpan" alt="" />
                            </div>
                        </div>
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6" style="padding-right: 5px;">
                                    <p class="textcont1 ">Session  </p>
                                    <asp:DropDownList ID="ddl_session" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>


                                <div class="col-lg-9 col-md-9 col-sm-6 col-xs-6" style="padding-left: 5px;">
                                    <p class="textcont1 ">Date</p>
                                    <asp:TextBox ID="txt_date" runat="server" CssClass="classTarget form-control" Style="width: 100%"  ></asp:TextBox>
                                </div>



                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6" style="padding-right: 5px;">
                                    <p class="textcont1 ">Class</p>
                                    <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6" style="padding-left: 5px;">
                                    <p class="textcont1 ">Section</p>
                                    <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                </div>


                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6" style="padding-right: 5px;">
                                    <p class="textcont1 ">Period</p>
                                    <asp:DropDownList ID="ddl_period" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_period_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6" style="padding-left: 5px;">
                                    <p class="textcont1 ">Subject</p>
                                    <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary" OnClick="btn_find_Click" Style="float: left; margin: 10px 0px 0px 0px;" />
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="texbox-border">
                            <div runat="server" visible="false" id="grid111">
                                <table style="margin: 0px; padding: 0px; width: 100%" border="1">
                                    <tr>
                                        <td style="padding: 5px;">Total Students
                                        </td>

                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbltotal_student" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>
                                        <td style="padding: 5px;">Total Present Students
                                        </td>

                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_persenstudent" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>

                                        <td style="padding: 5px;">Total Absent Students
                                        </td>

                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_totalabsentstudent" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>
                                        <td style="padding: 5px;">Total Leave Students
                                        </td>

                                        <td style="padding: 5px;">
                                            <asp:Label ID="lbl_leave_student" Font-Bold="true" runat="server">0</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="GrdView" runat="server" class="table-bordered" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Student Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FullName" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Admission No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_reg_id" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Roll No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Attendance_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_day" runat="server" Text='<%#Bind("Day")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Attendance Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Attendance_Status" runat="server" Text='<%#Bind("Attendance_Status")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                        <asp:HiddenField ID="hd_id" runat="server" />
                  <%--  </ContentTemplate>

                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress2"
                    runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                    DynamicLayout="False">
                    <ProgressTemplate>
                        <p class="waiting">
                            &nbsp;&nbsp;&nbsp; 
                                            <img src="../../images/Processing.gif" />

                        </p>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
            </div>
        </div>
     <%--   <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
        <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
            rel="Stylesheet" type="text/css" />
        <script type="text/javascript">
            //On Page Load.
            $(function () {
                SetDatePicker();

            });

            //On UpdatePanel Refresh.
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        SetDatePicker();


                    }
                });
            };
            function SetDatePicker() {
                $("[id$=txt_date]").datepicker({
                    showOn: 'button',
                    buttonImageOnly: true,
                    buttonImage: 'calendar.png',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1900:2100",
                    dateFormat: "dd/mm/yy",
                    // minDate: 0
                });
            }




        </script>--%>
    </form>
</body>
</html>
