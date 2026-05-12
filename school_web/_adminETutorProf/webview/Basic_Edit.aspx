<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Basic_Edit.aspx.cs" Inherits="school_web._adminETutorProf.webview.Basic_Edit" %>

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
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>

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
            font-size: 14px!important;
            padding: 2px 10px;
        }

        .table {
            margin-bottom: 9px!important;
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
    </style>
    <link href="../../Autocomplete/jquery-ui.css" rel="stylesheet" />


    <script src="../../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_dob.ClientID %>").datepicker({
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
        <div>
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

                <h2 class="messbox-sec-h2" style="width: 100%;">Personal Details</h2>

                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Employee Name </p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_emp_name" runat="server" Style="width: 96%;"></asp:TextBox>

                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Gender </p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:DropDownList ID="ddl_gender" runat="server" Style="width: 96%;height: 26px;">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>

                        </p>
                    </div>
                </div>

                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Date of Birth </p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_dob" runat="server" Style="width: 96%;"></asp:TextBox>
                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>

                        </p>
                    </div>
                </div>

                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Blood Group </p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:DropDownList ID="ddl_blood_group" runat="server" Style="width: 96%;height: 26px;">
                                <asp:ListItem>A+</asp:ListItem>
                                <asp:ListItem>A-</asp:ListItem>
                                <asp:ListItem>B+</asp:ListItem>
                                <asp:ListItem>B-</asp:ListItem>
                                <asp:ListItem>O+</asp:ListItem>
                                <asp:ListItem>O-</asp:ListItem>
                                <asp:ListItem>AB+</asp:ListItem>
                                <asp:ListItem>AB-</asp:ListItem>
                                <asp:ListItem>N/A</asp:ListItem>
                            </asp:DropDownList>

                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Religion </p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:DropDownList ID="ddl_religion" runat="server" Style="width: 96%;height: 26px;">
                                <asp:ListItem>Hindu</asp:ListItem>
                                <asp:ListItem>Islam</asp:ListItem>
                                <asp:ListItem>Sikh</asp:ListItem>
                                <asp:ListItem>Christian</asp:ListItem>
                                <asp:ListItem>Buddhism</asp:ListItem>
                                <asp:ListItem>Jain</asp:ListItem>
                            </asp:DropDownList>

                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Marital Status </p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:DropDownList ID="ddl_marital_status" runat="server" Style="width: 96%;height: 26px;">
                                <asp:ListItem>Unmarried</asp:ListItem>
                                <asp:ListItem>Married</asp:ListItem>
                            </asp:DropDownList>

                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Father's/Husband Name</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_father_name" runat="server" Style="width: 96%;"></asp:TextBox>


                        </p>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">PAN</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_pan" runat="server" Style="width: 96%;"></asp:TextBox>


                        </p>
                    </div>
                </div>


                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Address</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_address" runat="server" Style="width: 96%;" TextMode="MultiLine" Height="50"></asp:TextBox>


                        </p>
                    </div>
                </div>

                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">City</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_city" runat="server" Style="width: 96%;"></asp:TextBox>


                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Pincode</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_pin" runat="server" Style="width: 96%;"></asp:TextBox>


                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">State</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:DropDownList ID="ddl_state" runat="server" Style="width: 96%;height: 26px;"></asp:DropDownList>


                        </p>
                    </div>
                </div>
                 <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Email Id</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_emailid" runat="server"     Style="width: 96%;"></asp:TextBox>



                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Mobile No.</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_mobile" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)" Style="width: 96%;"></asp:TextBox>



                        </p>
                    </div>
                </div>


                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Bank Name</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_bank" runat="server" Style="width: 96%;"></asp:TextBox>



                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Branch Name</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_branch" runat="server" Style="width: 96%;"></asp:TextBox>



                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Account No.</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_ac_no" runat="server" Style="width: 96%;"></asp:TextBox>



                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">IFSC</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_ifsc" runat="server" Style="width: 96%;"></asp:TextBox>



                        </p>
                    </div>
                </div>
                <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">MICR</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_micr" runat="server" Style="width: 96%;"></asp:TextBox>



                        </p>
                    </div>
                </div>
                 <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Zoom User Id</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_zoomuserid" runat="server" Style="width: 96%;"></asp:TextBox>



                        </p>
                    </div>
                </div>
                 <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <p class="textcont1">Zoom Password</p>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <p class="textcont3">

                            <asp:TextBox ID="txt_zoompassword" runat="server" Style="width: 96%;"></asp:TextBox>



                        </p>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="texbox-border">

                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <p class="textcont3" style="text-align: center">

                            <asp:Button ID="btn_submit" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btn_submit_Click" />


                        </p>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
