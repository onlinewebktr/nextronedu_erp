<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scholarship_preview.aspx.cs" Inherits="school_web.Scholarship.Scholarship_preview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scholarship Preview</title>


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
    <link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=IBM+Plex+Mono:ital@0;1&family=IBM+Plex+Sans+Condensed:ital@0;1&family=IBM+Plex+Sans:ital,wght@0,100;0,400;0,700;1,100;1,400;1,700&family=IBM+Plex+Serif:ital@0;1&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Open+Sans:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;1,300;1,400;1,500;1,600;1,700;1,800&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&family=Syne+Mono&display=swap" rel="stylesheet" />

    <link href="../css/registration.css" rel="stylesheet" />
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
            background: url(assets/images/bg-pattern.jpg) #f7f7ff;
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
        }

        .notificationpan {
            width: 314px !important;
            bottom: 135px !important;
        }

        .online_frm-h {
            margin: 15px 0px 5px 0px;
            padding: 5px 0px 5px 0px;
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

        .online_frm-h {
            border-bottom: 2px solid #000 !important;
            text-align: center !important;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 20px;
            height: 20px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }

        label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 700;
            font-size: 13px !important;
        }

        .bgscolors {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            background: rgb(255 255 255 / 71%);
        }

        .form_control {
            border: 1px solid rgb(34, 34, 34) !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="bgscolors">
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
                                <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                    <div class="online_frm-bg">
                                        <div class="online_frm">
                                            <asp:Panel ID="pnl_student_details" runat="server">
                                                <h2 class="online_frm-hss">Check your details filled by you, once submit will not modify.</h2>
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Student Details</h2>
                                                </div>

                                                <div class="online_frm-inr">

                                                    <asp:Panel ID="pnl_payment_dv" runat="server">
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Payment Mode <sup>*</sup></h2>

                                                                    <asp:RadioButton ID="rd_payment_mode_online" runat="server" Text="Online" class="chkstle" Style="margin: 3px 5px 0px 0px;" Enabled="false" />

                                                                    <asp:RadioButton ID="rd_payment_mode_ofline" runat="server" Text="Offline" class="chkstle" Style="margin: 3px 5px 0px 0px;" Enabled="false" />

                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row" id="paymentid1" runat="server" visible="false">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Payment Slip(jpg file-500kb)<sup> </sup></h2>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <asp:Image ID="img_payment_slip" runat="server" Visible="false" Style="height: 230px; width: 230px; padding: 2px; border: 2px solid #000;" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="paymentid2" runat="server" visible="false">
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Transaction No. <sup>*</sup></h2>
                                                                    <asp:TextBox ID="txt_transaction_no" runat="server" CssClass="form_control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                                <div class="online_frm-grp">
                                                                    <h2 class="online_frm-grp-h">Type of Transaction<sup></sup></h2>
                                                                    <asp:TextBox ID="lbl_type_transaction" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>

                                                    <div class="row" runat="server" id="schBranchDv">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h" style="color: #c70000">Selected Branch : 
                                                                        <asp:Label ID="lbl_selected_branch" runat="server"></asp:Label></h2>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">First Name  </h2>
                                                                <asp:TextBox ID="txt_first_name" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Middle Name  </h2>
                                                                <asp:TextBox ID="txt_middle_name" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Last Name <sup></sup></h2>
                                                                <asp:TextBox ID="txt_last_name" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Date of birth<sup></sup></h2>
                                                                <asp:TextBox ID="txt_dob" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Gender <sup></sup></h2>

                                                                <asp:Label ID="lbl_gender" runat="server" Text="" CssClass="form_control"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Nationality<sup></sup></h2>
                                                                <asp:TextBox ID="txt_nationality" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Blood Group</h2>
                                                                <asp:TextBox ID="txt_blood_group" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Religion<sup></sup></h2>
                                                                <asp:Label ID="lbl_religion" runat="server" Text="" CssClass="form_control"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Caste<sup></sup></h2>
                                                                <asp:Label ID="lbl_category" runat="server" Text="" CssClass="form_control"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Height<sup></sup></h2>
                                                                <asp:Label ID="lbl_height" runat="server" Text="" CssClass="form_control"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Weight<sup></sup></h2>
                                                                <asp:Label ID="lbl_weight" runat="server" Text="" CssClass="form_control"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Aadhar card no</h2>
                                                                <asp:TextBox ID="txt_aadharcarno" MaxLength="16" onkeypress="return isNumberKey(event)" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Identification marks<sup></sup></h2>
                                                                <asp:TextBox ID="txt_identification_marks" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Type of Admission<sup></sup></h2>
                                                                <asp:Label ID="lbl_adm_type" runat="server" CssClass="form_control"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Class<sup></sup></h2>
                                                                <asp:Label ID="lbl_class" runat="server" CssClass="form_control"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Registration Fees<sup></sup></h2>
                                                                <asp:TextBox ID="txt_reg_fees" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp" style="background: #ffffffd4; padding: 6px;">
                                                                <h2 class="online_frm-grp-h">Sibling Details</h2>
                                                                <table class="table-bordered" style="width: 100%">
                                                                    <tr>
                                                                        <th style="padding: 7px 0px 7px 0px; font-size: 13px; text-align: center">S. No.</th>
                                                                        <th style="padding: 7px 0px 7px 0px; font-size: 13px; text-align: center">Name of Sibling</th>
                                                                        <th style="padding: 7px 0px 7px 0px; font-size: 13px; text-align: center">Age</th>
                                                                        <th style="padding: 7px 0px 7px 0px; font-size: 13px; text-align: center">School/College</th>
                                                                        <th style="padding: 7px 0px 7px 0px; font-size: 13px; text-align: center">Class</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>1.</td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_sb_name1" runat="server" CssClass="form_control"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_sb_age1" runat="server" CssClass="form_control"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_sb_school1" runat="server" CssClass="form_control"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_sb_class1" runat="server" CssClass="form_control"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>2.</td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_sb_name2" runat="server" CssClass="form_control"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_sb_age2" runat="server" CssClass="form_control"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_sb_school2" runat="server" CssClass="form_control"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_sb_class2" runat="server" CssClass="form_control"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>


                                            <asp:Panel ID="pn_prev_info" runat="server">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Previous School Details</h2>
                                                </div>

                                                <div class="online_frm-inr">
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Name of last school attended<sup></sup></h2>
                                                                <asp:TextBox ID="txt_lastschool" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Board<sup></sup></h2>
                                                                <asp:TextBox ID="txt_board" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Passout class</h2>
                                                                <asp:TextBox ID="txt_passout_classs" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Percentage %</h2>
                                                                <asp:TextBox ID="txt_percentage" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Reason for shift</h2>
                                                                <asp:TextBox ID="txt_reason_for_shift" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Year</h2>
                                                                <asp:TextBox ID="txt_year" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnl_father_dt" runat="server">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Father’s Contact Details</h2>
                                                </div>

                                                <div class="online_frm-inr">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">First Name <sup></sup></h2>
                                                                <asp:TextBox ID="txt_father_first_name" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Middle Name <sup></sup></h2>
                                                                <asp:TextBox ID="txt_father_middle_name" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Last Name <sup></sup></h2>
                                                                <asp:TextBox ID="txt_father_last_name" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Occupation</h2>
                                                                <asp:TextBox ID="txt_occupation" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Qualification</h2>
                                                                <asp:TextBox ID="txt_qualitication" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Designation</h2>
                                                                <asp:TextBox ID="txt_designation" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Mobile No.<sup></sup></h2>
                                                                <asp:Label ID="lbl_father_mob" runat="server" Text="Label" CssClass="form_control"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Email</h2>
                                                                <asp:TextBox ID="txt_email" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Annual Income<sup></sup></h2>
                                                                <asp:TextBox ID="txt_annual_income" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnl_mther_dt" runat="server">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Mother’s Contact Details</h2>
                                                </div>

                                                <div class="online_frm-inr">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">First Name <sup></sup></h2>
                                                                <asp:TextBox ID="txt_mother_first_name" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Middle Name </h2>
                                                                <asp:TextBox ID="txt_mother_middle_name" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Last Name <sup></sup></h2>
                                                                <asp:TextBox ID="txt_mother_last_name" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Occupation</h2>
                                                                <asp:TextBox ID="txt_Mother_Occupation" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Qualification</h2>
                                                                <asp:TextBox ID="txt_mother_Qualification" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Designation</h2>
                                                                <asp:TextBox ID="txt_mother_Designation" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Mobile No.</h2>
                                                                <asp:Label ID="lbl_mther_mob" runat="server" CssClass="form_control"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Email</h2>
                                                                <asp:TextBox ID="txt_mother_emailid" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Annual Income</h2>
                                                                <asp:TextBox ID="txt_mother_income" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </asp:Panel>


                                            <asp:Panel ID="pnl_address_dt" runat="server">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Address details</h2>
                                                </div>

                                                <div class="online_frm-inr">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Present Address <sup></sup></h2>
                                                                <asp:TextBox ID="txt_adress" runat="server" CssClass="form_control" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Present P.O. <sup></sup></h2>
                                                                <asp:TextBox ID="txt_present_po" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Present District <sup></sup></h2>
                                                                <asp:TextBox ID="txt_present_district" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Present City <sup></sup></h2>
                                                                <asp:TextBox ID="txt_city" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Present State <sup></sup></h2>
                                                                <asp:TextBox ID="txt_State" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Present Pin Code <sup></sup></h2>
                                                                <asp:TextBox ID="txt_pincode" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Permanent Address <sup></sup></h2>
                                                                <asp:TextBox ID="txt_pAddress" runat="server" CssClass="form_control" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Permanent P.O. <sup></sup></h2>
                                                                <asp:TextBox ID="txt_perma_po" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Permanent District <sup></sup></h2>
                                                                <asp:TextBox ID="txt_perma_disctrict" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Permanent City <sup></sup></h2>
                                                                <asp:TextBox ID="txt_pcity" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Permanent State <sup></sup></h2>
                                                                <asp:TextBox ID="txt_Pstate" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Permanent Pin Code<sup></sup></h2>
                                                                <asp:TextBox ID="txt_Ppincod" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>



                                            <asp:Panel ID="pnl_misc_dt" runat="server">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Misc Details</h2>
                                                </div>

                                                <div class="online_frm-inr">

                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Is the student handicapped</h2>
                                                                <asp:Label ID="lbl_is_handicap" runat="server" CssClass="form_control"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Medical Remarks</h2>
                                                                <asp:TextBox ID="txt_medicalremarks" runat="server" CssClass="form_control" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">From where did you hear about the school?</h2>
                                                                <asp:TextBox ID="txt_about_theschool" runat="server" CssClass="form_control" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnl_docs" runat="server">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Uploaded Image</h2>
                                                </div>

                                                <div class="online_frm-inr">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Student Image<sup></sup></h2>
                                                                <asp:Image ID="Image1" runat="server" Style="max-width: 170px; padding: 2px; border: 2px solid #000;" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="display: none">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Father's Signature<sup></sup></h2>
                                                                <asp:Image ID="img_father_sig" runat="server" Style="height: 50px; width: 150px; padding: 2px; border: 2px solid #000;" />
                                                            </div>
                                                        </div>
                                                        <div style="display: none" class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Mother's Signature<sup></sup></h2>
                                                                <asp:Image ID="img_mother_signature" runat="server" Style="height: 50px; width: 150px; padding: 2px; border: 2px solid #000;" />
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="row" style="display: none">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Mother's Image<sup></sup></h2>
                                                                <asp:Image ID="img_mother_photo" runat="server" Style="max-width: 170px; padding: 2px; border: 2px solid #000;" />
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Father's Image<sup></sup></h2>
                                                                <asp:Image ID="img_fathers_image" runat="server" Style="max-width: 170px; padding: 2px; border: 2px solid #000;" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Family Images<sup></sup></h2>
                                                                <asp:Image ID="img_family_image" runat="server" Style="max-width: 170px; padding: 2px; border: 2px solid #000;" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="display: none">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Birth Certificate Image<sup></sup></h2>
                                                                <asp:Image ID="img_birthcertificate" runat="server" Style="max-width: 170px; padding: 2px; border: 2px solid #000;" />
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Residential Certificates Image<sup></sup></h2>
                                                                <asp:Image ID="img_residental" runat="server" Style="max-width: 170px; padding: 2px; border: 2px solid #000;" />
                                                            </div>
                                                        </div>

                                                    </div>




                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <a class="textcheckbbbb">
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                    <b style="color: #f50000">Declaration:-</b> &nbsp; &nbsp;   I/We 
                                                                <asp:Label ID="lbl_mom_name" runat="server" Text=""></asp:Label>, Mother of
                                                                <asp:Label ID="lbl_term_c_std_name" runat="server" Text=""></asp:Label>
                                                                    solemnly declare that I/We will abide by the rules and regulations of 
                                                                <asp:Label ID="lbl_schoolname" runat="server" Font-Bold="true"></asp:Label>, the information provided by me/us in the Scholarship registration form is correct and
we undertstand that if any of the information is found to be incorrect or false, my/our ward shall be automatically debarred
from selection/Scholarship applcation process without any correspondence in this regard. I/We accept the process of admission
undertaken by the school and understand that applicant  does not guarantee scholarship.I/We will abide by all
the rules and regulations of school and will be liable for any violations as deemed fit by the school authorities.

                                                                </a>
                                                            </div>
                                                        </div>

                                                    </div>




                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_modify" runat="server" Text="Modify Form" class="acc-dt-sub-back-btn" OnClick="btn_modify_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_final_submit" runat="server" Text="Final Submit" class="acc-dt-sub-btn" OnClick="btn_final_submit_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <div class="steps-dv">
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number stps-success-num" runat="server" id="pronumS1">1</p>
                                            <p class="steps-bx-txt-p stps-success-name" runat="server" id="prontxt1">Account Details</p>
                                        </div>
                                        <div class="steps-root steps-root-done" runat="server" id="pro1"></div>

                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number stps-success-num" runat="server" id="pronumS2">2</p>
                                            <p class="steps-bx-txt-p stps-success-name" runat="server" id="prontxt2">Student Details</p>
                                        </div>
                                        <div class="steps-root steps-root-done" runat="server" id="pro2"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number stps-success-num" runat="server" id="pronumS3">3</p>
                                            <p class="steps-bx-txt-p stps-success-name" runat="server" id="prontxt3">Previous School Details</p>
                                        </div>
                                        <div class="steps-root steps-root-done" runat="server" id="pro3"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number stps-success-num" runat="server" id="pronumS4">4</p>
                                            <p class="steps-bx-txt-p stps-success-name" runat="server" id="prontxt4">Father’s Contact Details</p>
                                        </div>
                                        <div class="steps-root steps-root-done" runat="server" id="pro4"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number stps-success-num" runat="server" id="pronumS5">5</p>
                                            <p class="steps-bx-txt-p stps-success-name" runat="server" id="prontxt5">Mother’s Contact Details</p>
                                        </div>
                                        <div class="steps-root steps-root-done" runat="server" id="pro5"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number stps-success-num" runat="server" id="pronumS6">6</p>
                                            <p class="steps-bx-txt-p stps-success-name" runat="server" id="prontxt6">Addresss Details</p>
                                        </div>
                                        <div class="steps-root steps-root-done" runat="server" id="pro6"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number stps-success-num" runat="server" id="pronumS7">7</p>
                                            <p class="steps-bx-txt-p stps-success-name" runat="server" id="prontxt7">Misc Details</p>
                                        </div>
                                        <div class="steps-root steps-root-done" runat="server" id="pro7"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number stps-success-num" runat="server" id="pronumS8">8</p>
                                            <p class="steps-bx-txt-p stps-success-name" runat="server" id="prontxt8">Upload Document</p>
                                        </div>
                                        <div class="steps-root steps-root-done" runat="server" id="Div1"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number" runat="server" id="p1">9</p>
                                            <p class="steps-bx-txt-p" runat="server" id="p2">Preview & Final Submit</p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </section>
        </div>

    </form>
</body>
</html>
