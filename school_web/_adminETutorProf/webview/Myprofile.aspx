<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Myprofile.aspx.cs" Inherits="school_web._adminETutorProf.webview.Myprofile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Profile</title>
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
            float:left;
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
    </style>
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


            <h2 class="messbox-sec-h2" style="width: 100%;">Personal Details</h2>
            <div class="texbox-border">

                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Emp. Name  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_employeename" runat="server" Text=""></asp:Label>
                    </p>
                </div>

            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Gender  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_gender" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Date of Birth  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_dob" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Blood Group  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_bloadgroup" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Religion  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_Religion" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Marital Status  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_Marital_status" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">

                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">

                    <p class="textcont1">Father's/Husband Name  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_fathername" runat="server" Text=""></asp:Label>
                    </p>

                </div>


            </div>
            <div class="texbox-border">



                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">PAN  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_pan_no" runat="server" Text=""></asp:Label>
                    </p>
                </div>

            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Address  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_address" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>


            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">City  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_city" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Pin code  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_Pincode" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">State  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_state" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>



            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Email  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Mobile No.  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_mobileno" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Bank Name  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_bank_name" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Branch  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_Branch" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Account No.  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_account_no" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">IFSC  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_ifsc" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">MICR  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_MICR" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

               <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Zoom User Id  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_zoomuserid" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>


               <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Password  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_zoompwd" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>


            <h2 class="messbox-sec-h2" style="width: 100%;">Organization Details</h2>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Employee Code  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_employeecode" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Punch Card No  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_punchcardno" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Official Email Id </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_officialemailid" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Qualification </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_qualification" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Grade </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_grade" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Department </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_department" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Designation </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_Designation" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">E.P.F. No. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_epfno" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Join Date </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_joindate" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">PF leaving Date </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_pfleavingdate" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Reason </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_Reason" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">ESIC No. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_ESIC_no" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">ESIC Join Date </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_esijoindate" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="texbox-border">

                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">ESIC Leaving Date </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_esic_leavingdate" runat="server" Text=""></asp:Label>
                    </p>
                </div>

            </div>

           <div class="texbox-border">
                     <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Reason </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_Reason_esileaving" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>


            <div class="texbox-border">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1">Date of Joining </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_Date_of_Joining" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>



        </div>
    </form>
</body>
</html>
