<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scholarship-guidelines.aspx.cs" Inherits="school_web.Scholarship.Scholarship_guidelines" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scholarship Guidelines</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Welcome To School ERP" />
    <meta name="keywords" content="Welcome To School ERP" />
    <link rel="icon" type="images/ico" sizes="48x48" href="../images/icon_school.ico" />




    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/index.css" rel="stylesheet" />
    <link href="../css/index_media.css" rel="stylesheet" />

    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <link href="../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />

    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    function onlyZeroandOne(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode < 48 || charCode > 49)
            return false;

        return true;
    }
    //-->
    </script>
    <style>
        body, #form1 {
            background: #69d368 !important;
        }

        .app-head-sec {
            background: rgb(12 12 12) !important;
            color: #fff !important;
        }

        body {
            font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
            font-size: 16px;
            line-height: 1.42857143;
            color: #333;
        }

        .navbar-header {
            height: auto;
            padding: 0;
            margin: 0;
            padding-left: 15px;
        }

        .heder2 {
            border-bottom: 2px solid #000;
            padding-bottom: 15px;
            margin: 0px;
            float: left;
            width: 100%;
        }

        .notificationpan {
            width: 314px !important;
            bottom: 135px !important;
        }

        .online_frm-h {
            margin: 15px 0px 5px 0px;
            padding: 10px 0px 0px 0px;
            font-size: 21px;
        }

        .form_control {
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 7px 15px 7px 15px;
            float: left;
            border: 1px solid rgb(234, 236, 239);
            font-size: 14px;
            pointer-events: none;
            color: #000;
            height: 36px;
        }

        .acc-dt-sub-btn {
            background-color: rgb(44 194 24 / 99%) !important;
            color: #fff !important;
        }

            .acc-dt-sub-btn:hover {
                box-shadow: none;
                background-image: none;
                background-color: rgb(44 194 24 / 99%) !important;
                color: #fff !important;
                opacity: 0.9;
            }

        button[disabled], html input[disabled] {
            cursor: default;
            background-color: Gray !important;
        }

            button[disabled], html input[disabled]:hover {
                cursor: default;
                background-color: Gray !important;
            }
    </style>
    <link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=IBM+Plex+Mono:ital@0;1&family=IBM+Plex+Sans+Condensed:ital@0;1&family=IBM+Plex+Sans:ital,wght@0,100;0,400;0,700;1,100;1,400;1,700&family=IBM+Plex+Serif:ital@0;1&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Open+Sans:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;1,300;1,400;1,500;1,600;1,700;1,800&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&family=Syne+Mono&display=swap" rel="stylesheet" />
    <link href="../css/registration.css" rel="stylesheet" />

    <style>
        /* SQUARED TWO */
        .squaredTwo {
            width: 28px;
            height: 28px;
            float: left;
            margin: 4px 13px 0px 0px;
            padding: 4px 0px 0px 6px;
            background: #fcfff4;
            background: -webkit-linear-gradient(top, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
            background: -moz-linear-gradient(top, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
            background: -o-linear-gradient(top, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
            background: -ms-linear-gradient(top, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
            background: linear-gradient(top, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#fcfff4', endColorstr='#b3bead',GradientType=0 );
            -webkit-box-shadow: inset 0px 1px 1px white, 0px 1px 3px rgba(0,0,0,0.5);
            -moz-box-shadow: inset 0px 1px 1px white, 0px 1px 3px rgba(0,0,0,0.5);
            box-shadow: inset 0px 1px 1px white, 0px 1px 3px rgba(0,0,0,0.5);
            position: relative;
        }

            .squaredTwo label {
                cursor: pointer;
                position: absolute;
                width: 21px;
                height: 20px;
                left: 4px;
                top: 4px;
                -webkit-box-shadow: inset 0px 1px 1px rgba(0,0,0,0.5), 0px 1px 0px rgba(255,255,255,1);
                -moz-box-shadow: inset 0px 1px 1px rgba(0,0,0,0.5), 0px 1px 0px rgba(255,255,255,1);
                box-shadow: inset 0px 1px 1px rgba(0,0,0,0.5), 0px 1px 0px rgba(255,255,255,1);
                background: -webkit-linear-gradient(top, #cfcfcf 0%, #5c5c5c 100%);
                background: -moz-linear-gradient(top, #222 0%, #45484d 100%);
                background: -o-linear-gradient(top, #222 0%, #45484d 100%);
                background: -ms-linear-gradient(top, #222 0%, #45484d 100%);
                background: linear-gradient(top, #222 0%, #45484d 100%);
                filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#222', endColorstr='#45484d',GradientType=0 );
            }

                .squaredTwo label:after {
                    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
                    filter: alpha(opacity=0);
                    opacity: 0;
                    content: '';
                    position: absolute;
                    width: 12px;
                    height: 7px;
                    background: transparent;
                    top: 5px;
                    left: 5px;
                    border: 3px solid #fcfff4;
                    border-top: none;
                    border-right: none;
                    -webkit-transform: rotate(-45deg);
                    -moz-transform: rotate(-45deg);
                    -o-transform: rotate(-45deg);
                    -ms-transform: rotate(-45deg);
                    transform: rotate(-45deg);
                }

                .squaredTwo label:hover::after {
                    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=30)";
                    filter: alpha(opacity=30);
                    opacity: 0.3;
                }

            .squaredTwo input[type=checkbox]:checked + label:after {
                -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=100)";
                filter: alpha(opacity=100);
                opacity: 1;
            }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 17px;
            height: 17px;
            position: relative;
            top: 1.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section>
            <div class="form-wprs">
                <div class="container">


                    <div class="row">
                        <div class="heder2">

                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">


                                <a href="../Default.aspx">
                                    <img src="images/slogo.png" runat="server" id="school_logo" class="img-responsive schoollogoimg" /></a>


                            </div>

                            <div class="col-1g-10 col-md-10 col-sm-12 col-xs-12">
                                <div class="textonlinehe">

                                    <h1 class="headingtextth1">
                                        <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                    </h1>

                                    <div class="onlineadtextp">
                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="lbl_address_2" runat="server"></asp:Label>


                                    </div>
                                    <div class="onlineadtextp">
                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                        &nbsp;&nbsp;

                           
                            <asp:Label ID="lbl_website" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div class="onlineadtextp">
                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                    </div>




                                </div>
                            </div>
                        </div>


                    </div>

                    <div id="notification">
                        <div id="pan" class="notificationpan">
                            <div style="float: left; height: auto;">
                                <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White" Style="font-size: 21px;"></asp:Label>
                            </div>
                        </div>
                    </div>



                    <div class="row rowtop">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="online_frm-bg">
                                    <div class="online_frm">
                                        <div class="aplication-sec" style="background: #fff;">
                                            <div class="app-head-sec">
                                                <h2 class="regi-title">Scholarship Guideline</h2>
                                            </div>
                                            <div class="reg-content-sec">


                                                <asp:Label ID="lbl_data" runat="server"></asp:Label>

                                                <ul class="terms-ul" id="a2" runat="server" visible="false">
                                                </ul>

                                                <asp:Panel ID="pnl_branch" runat="server" Visible="false">
                                                    <div class="check-btn-sec">
                                                        <div class="squaredTwo">
                                                            <asp:RadioButton ID="rd_sch_branch_1" runat="server" GroupName="AV" />  
                                                            <label for="rd_sch_branch_1"></label>
                                                        </div>
                                                        <span><b>Branch 1</b>
                                                            <br />
                                                            <asp:Label ID="lbl_branch_1" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <br />
                                                    <div class="check-btn-sec">
                                                        <div class="squaredTwo">
                                                            <asp:RadioButton ID="rd_sch_branch_2" runat="server" GroupName="AV" />
                                                            <label for="rd_sch_branch_2"></label>
                                                        </div>
                                                        <span><b>Branch 2</b>
                                                            <br />
                                                            <asp:Label ID="lbl_branch_2" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <br />
                                                </asp:Panel>

                                                <div class="check-btn-sec">
                                                    <div class="squaredTwo">
                                                        <input type="checkbox" value="None" id="squaredTwo" runat="server" name="check" />
                                                        <label for="squaredTwo"></label>
                                                    </div>
                                                    <span>I have read and accept the scholarship terms and conditions stated above.
                                                    <br />
                                                        (Click Check Box to proceed for apply.)</span>
                                                </div>




                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12"></div>
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <div class="txtbx-wprr">
                                                            <asp:Button ID="btn_terms" runat="server" class="acc-dt-sub-btn" Text="Proceed" OnClick="btn_terms_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

        </section>
    </form>
</body>
</html>
