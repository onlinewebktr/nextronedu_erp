<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Make_Attendance.aspx.cs" Inherits="school_web._adminETutorProf.webview.Make_Attendance_Student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Make Attendance for Student</title>
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
            border: 1px solid #e7e7e7;
            text-align: center;
            padding: 3px 4px 3px 5px;
            font-size: 11px;
        }

        .form-control {
            display: block;
            width: 100%;
            height: 25px!important;
            font-size: 14px!important;
            padding: 2px 5px;
        }

        .table {
            margin-bottom: 9px!important;
        }

        label {
            display: inline!important;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: bold;
        }










        .rdobtnS {
            margin: 0px 0px 0px 0px;
            font-size: 12px;
        }

            .rdobtnS tr {
                padding: 0px 2px;
                width: 33px;
                float: left;
            }

                .rdobtnS tr td {
                    padding: 0px;
                    width: 30px;
                    margin: 0px;
                    height: 30px;
                    float: left;
                }

                    .rdobtnS tr td label {
                        position: relative;
                        top: -28px;
                    }


        .att-imgs {
            padding: 3px;
            width: 44px;
            height: 44px;
            border: 1px solid #a1a1a1;
            border-radius: 50%;
        }

        .att-name {
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 12px;
            font-weight: 500;
        }

        .att-adm_no {
            padding: 0px;
            width: 100%;
            float: left;
        }

        .att-roll {
            padding: 0px;
            width: 100%;
            float: left;
        }

        .img-dv {
            padding: 0px;
            width: 45px;
            float: left;
        }

        .contnt-dv {
            padding: 0px 0px 0px 10px;
            float: left;
            width: 52%;
            text-align: left;
        }

        .action-dv {
            padding: 8px 0px 0px 0px;
            float: right;
        }

        .ui-datepicker-trigger {
            display: none;
        }

        .container {
            padding-right: 10px;
            padding-left: 10px;
        }


        .rdobtnS tr:nth-child(1) {
        }

        .rdobtnS tr:nth-child(2) {
        }

        .rdobtnS tr:nth-child(3) {
        }


        .rdobtnS tr:nth-child(1) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #39c500;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(1) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(1) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #3ed700;
            }

                .rdobtnS tr:nth-child(1) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }

                .rdobtnS tr:nth-child(1) input[type="radio"]:checked label {
                    color: #fff;
                }

        /*========================SecondRoW============================*/
        .rdobtnS tr:nth-child(2) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #ff0000;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(2) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(2) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #ff0000;
            }

                .rdobtnS tr:nth-child(2) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }


        /*========================THIRDROWS============================*/
        .rdobtnS tr:nth-child(3) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #ffa500;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(3) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(3) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #ffa500;
            }

                .rdobtnS tr:nth-child(3) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }


        th {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
        <div class="fullinfo">
            <div style="margin: 0px; padding: 0px 5px; float: left; height: auto; width: 100%; position: relative">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
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

                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6" style="padding-left: 5px;">
                                    <p class="textcont1 ">Date</p>
                                    <asp:TextBox ID="txt_date" runat="server" CssClass="classTarget form-control" Style="width: 100%"  Enabled="false"></asp:TextBox>
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
                                    <p class="textcont1 ">Subject</p>
                                    <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6" style="padding-left: 5px;">
                                    <p class="textcont1 ">Period</p>
                                    <asp:DropDownList ID="ddl_period" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary" OnClick="btn_find_Click" Style="float: left; margin: 10px 0px 0px 0px;" />
                                </div>
                            </div>
                        </div>


                        <div class="clearfix"></div>
                        <div class="texbox-border">
                            <div runat="server" visible="false" id="grid111">
                                <asp:GridView ID="GrdView" runat="server" class="table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="img-dv">
                                                    <img src="<%#Eval("Student_img") %>" class="att-imgs" />
                                                </div>
                                                <div class="contnt-dv">
                                                    <asp:Label ID="lbl_FullName" runat="server" class="att-name" Text='<%#Bind("studentname")%>'></asp:Label>
                                                    <asp:Label ID="lbl_reg_id" runat="server" class="att-adm_no" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                    <asp:Label ID="lbl_roll_no" runat="server" class="att-roll" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                </div>
                                                <div class="action-dv">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" class="rdobtnS" OnDataBound="RadioButtonList1_DataBound"></asp:RadioButtonList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="btn_save_all" runat="server" Style="width: 100px; height: 37px; float: right" CssClass="mt-2 btn btn-primary" Text="Save All" OnClick="btn_save_all_Click" OnClientClick='return confirm("Are you sure want to save all ?")' />
                            </div>
                        </div>
                        <asp:HiddenField ID="hd_id" runat="server" />
                    </ContentTemplate>

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
                </asp:UpdateProgress>
            </div>
        </div>
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
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




        </script>
    </form>
</body>
</html>
