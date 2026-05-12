<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Apply_Career.aspx.cs" Inherits="school_web.Payroll.Apply_Career" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Apply Career</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Apply Career" />
    <meta name="keywords" content="Apply Career" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/registration.css" rel="stylesheet" />

    <script src="assets/js/jquery-1.10.2.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <link href="font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />

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
    </style>
    <link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=IBM+Plex+Mono:ital@0;1&family=IBM+Plex+Sans+Condensed:ital@0;1&family=IBM+Plex+Sans:ital,wght@0,100;0,400;0,700;1,100;1,400;1,700&family=IBM+Plex+Serif:ital@0;1&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Open+Sans:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;1,300;1,400;1,500;1,600;1,700;1,800&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&family=Syne+Mono&display=swap" rel="stylesheet" />


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
                            <div class="col-sm-12 col-md-2" style="float: left">
                                <div le="margin: 0px; padding: 0px; height: 110px; width: 100%; float: left;">
                                    <a href="../Default.aspx">
                                        <img src="images/slogo.png" runat="server" id="school_logo" class="img-responsive schoollogoimg" style="width: 150px; height: 150px" /></a>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-9" style="float: left">
                                <div style="margin: 0px; padding: 0px; height: 145px; float: left;">
                                    <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 25px; text-decoration: underline;">
                                        <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                    </h1>

                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 15px; width: 100%;">
                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                    </div>
                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 15px; width: 100%;">
                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                        &nbsp;&nbsp;

                           
                            <asp:Label ID="lbl_website" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 15px; width: 100%;">
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



                    <div class="row" style="margin-top: 20px;">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="online_frm-bg">
                                    <div class="online_frm">
                                        <div class="aplication-sec">
                                            <div class="app-head-sec">
                                                <h2 class="regi-title" style="color: #fff;">Apply Career
                                                    <asp:Label ID="lbl_session" runat="server"></asp:Label></h2>
                                            </div>
                                            <div class="reg-content-sec">

                                                <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Session">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("SessionName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apply For">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Apply_for" runat="server" Text='<%#Bind("name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apply Fee">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Course_Fee" runat="server" Text='<%#Bind("ApplicationFee")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Start Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Start_Date" runat="server" Text='<%#Bind("StartDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="End Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_end_date" runat="server" Text='<%#Bind("EndDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Number of Seats" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_no_seat" runat="server" Text='<%#Bind("NoOfSeat")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lbl_no_seat_avl" runat="server" Visible="false"></asp:Label>
                                                                <asp:Button ID="btn_Submit" runat="server" Text="Apply" CssClass="acc-dt-sub-btn" ValidationGroup="a" OnClick="btn_Submit_Click" Style="margin: 0px 0px 0px; padding: 6px 15px;" />
                                                                <asp:Label ID="lbl_Isactive" Visible="false" runat="server" Text='<%#Bind("Isactive")%>'></asp:Label>

                                                                <asp:Label ID="lbl_start_Idate" Visible="false" runat="server" Text='<%#Bind("StartIdate")%>'></asp:Label>
                                                                <asp:Label ID="lbl_end_Idate" Visible="false" runat="server" Text='<%#Bind("EndIdate")%>'></asp:Label>

                                                                <asp:Label ID="lbl_Vacancy_Id" Visible="false" runat="server" Text='<%#Bind("Vacancy_Id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Designation" Visible="false" runat="server" Text='<%#Bind("HiringFor")%>'></asp:Label>
                                                                <asp:Label ID="lbl_HiringParameterId" Visible="false" runat="server" Text='<%#Bind("HiringParameterId")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Session_id" Visible="false" runat="server" Text='<%#Bind("SessionId")%>'></asp:Label>

                                                                <asp:Label ID="lbl_no_application" runat="server" Text='<%#Bind("NoOfSeat")%>' Visible="false"></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                    <HeaderStyle BackColor="#fcd535" />
                                                </asp:GridView>



                                                <table border="1" style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                                    <tr>
                                                        <td style="background: #90ee902e;">
                                                            <div style="margin: 5px; padding: 0px; float: left; height: 25px; width: 25px; background-color: LightGreen; border: 1px solid #000;">
                                                            </div>
                                                            <span style="float: left; margin: 7px 0px 0px 0px; font-weight: bold">Online apply career running</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background: #ff00008c">
                                                            <div style="margin: 5px; padding: 0px; float: left; height: 25px; width: 25px; background-color: red; border: 1px solid #000;">
                                                            </div>
                                                            <span style="float: left; margin: 7px 0px 0px 0px; font-weight: bold">Online apply career Closed</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background: #ffa5005e">
                                                            <div style="margin: 5px; padding: 0px; float: left; height: 25px; width: 25px; background-color: Orange; border: 1px solid #000;">
                                                            </div>
                                                            <span style="float: left; margin: 7px 0px 0px 0px; font-weight: bold">Online apply career upcoming</span>
                                                        </td>
                                                    </tr>
                                                </table>

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
