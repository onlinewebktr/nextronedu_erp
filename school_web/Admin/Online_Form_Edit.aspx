<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Online_Form_Edit.aspx.cs" Inherits="school_web.Admin.Online_Form_Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Online Registration</title>

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
            background: #06b5035e !important;
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

        .fade {
            opacity: 1;
        }

        .online_frm-h {
            border-bottom: 2px solid #000 !important;
            text-align: center !important;
        }

        .modal {
            position: fixed;
            top: 222px;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 1050;
            display: none;
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
        }

        .modal-content {
            position: relative;
            background-color: #fff;
            -webkit-background-clip: padding-box;
            background-clip: padding-box;
            border: 1px solid #999;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 6px;
            outline: 0;
            -webkit-box-shadow: 0 3px 9px rgb(0 0 0 / 50%);
            box-shadow: 0 3px 9px rgb(0 0 0 / 50%);
            float: left;
            width: 100%;
        }

        .modal-header {
            display: flex;
            flex-shrink: 0;
            align-items: center;
            justify-content: space-between;
            padding: 1rem 1rem;
            border-bottom: 1px solid #dee2e6;
            border-top-left-radius: calc(0.3rem - 1px);
            border-top-right-radius: calc(0.3rem - 1px);
        }

        .modal-title {
            margin-bottom: 0;
            line-height: 1.5;
            font-size: 15px;
            text-align: left;
            width: 100%;
        }

        .btn-secondary {
            color: #fff;
            background-color: #6c757d;
            border-color: #6c757d;
        }

        .btn {
            display: inline-block;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            text-align: center;
            letter-spacing: .5px;
            text-decoration: none;
            vertical-align: middle;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            user-select: none;
            background-color: transparent;
            border: 1px solid transparent;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            border-radius: 0.25rem;
            transition: color .15s ease-in-out, background-color .15s ease-in-out, border-color .15s ease-in-out, box-shadow .15s ease-in-out;
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
</head>
<body>
    <form id="form1" runat="server">



        <section>
            <div class="form-wprs">
                <div class="container">


                    <div class="row">
                        <div class="heder2">

                            <div class="col-sm-12 col-md-3">


                                <div style="margin: 0px; padding: 0px; height: 110px; width: 100%; float: left;">
                                    <a href="online-registration.aspx">
                                        <img src="images/slogo.png" runat="server" id="school_logo" class="img-responsive schoollogoimg" style="width: 132px; height: 132px;" /></a>
                                </div>

                            </div>
                            <div class="col-sm-12 col-md-9">
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



                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 25px; width: 100%;">
                                        <span style="font-size: 25px; font-weight: bold;">Online Registration Form </span>


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
                        <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 col-sm-12 col-xs-12">
                            <div class="row">
                                <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                    <div class="online_frm-bg">
                                        <div class="online_frm">
                                            <asp:Panel ID="pnl_account_details" runat="server">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Enter Account Details</h2>
                                                    <p class="online_frm-p">Enter your mobile  & email details to process your registration.</p>
                                                </div>

                                                <div class="online_frm-inr">
                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Mobile No. <sup>*</sup></h2>
                                                                <asp:TextBox ID="txt_std_mob" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Email Id <sup>*</sup></h2>
                                                                <asp:TextBox ID="txt_std_email_id" runat="server" onblur="validateEmail(this);" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_acc_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_acc_dt_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnl_student_details" runat="server" Visible="false">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Enter Student Details</h2>
                                                    <p class="online_frm-p">Enter student  details to process your registration.</p>
                                                </div>

                                                <div class="online_frm-inr">

                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Payment Mode <sup>*</sup></h2>

                                                                <asp:RadioButton ID="rd_payment_mode_online" runat="server" Text="Online" OnCheckedChanged="rd_payment_mode_online_CheckedChanged" Checked="true" GroupName="ak1" AutoPostBack="true" class="chkstle" Style="margin: 3px 5px 0px 0px;" />

                                                                <asp:RadioButton ID="rd_payment_mode_ofline" runat="server" OnCheckedChanged="rd_payment_mode_ofline_CheckedChanged" Text="Offline" GroupName="ak1" AutoPostBack="true" class="chkstle" Style="margin: 3px 5px 0px 0px;" />

                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row" id="paymentid1" runat="server" visible="false">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Payment Slip(jpg file-500kb)<sup> </sup></h2>
                                                                <asp:FileUpload ID="FileUpload4" runat="server" CssClass="form_control" />
                                                                <br />
                                                                <asp:Button ID="btn_payment_slip" runat="server" OnClick="btn_payment_slip_Click" Text="Upload Image" Style="height: 29px; font-size: 12px; font-weight: bold;" />
                                                                <asp:Label ID="lbl_payment_slip" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <a id="a1" runat="server" target="_blank">


                                                                    <asp:Image ID="img_payment_slip" runat="server" Visible="false" Style="height: 230px; width: 230px; padding: 2px; border: 2px solid #000;" />
                                                                </a>
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
                                                                <asp:DropDownList ID="ddl_transactiontype" runat="server" CssClass="form_control">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                    <asp:ListItem>NEFT</asp:ListItem>
                                                                    <asp:ListItem>IMPS</asp:ListItem>
                                                                    <asp:ListItem>RTGS</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                    </div>


                                                    <div class="row" style="display: none">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Session <sup>*</sup></h2>
                                                                <asp:DropDownList ID="ddl_session" Enabled="false" runat="server" CssClass="form_control"></asp:DropDownList>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">First Name <sup>*</sup></h2>
                                                                <asp:TextBox ID="txt_first_name" runat="server" CssClass="form_control" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Middle Name <sup></sup></h2>
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
                                                                <h2 class="online_frm-grp-h">Date of birth<sup>*</sup></h2>
                                                                <asp:TextBox ID="txt_dob" runat="server" CssClass="form_control" placeholder="dd/mm/yyyy" onkeyup="var v = this.value; if (v.match(/^\d{2}$/) !== null) {this.value = v + '/';} else if (v.match(/^\d{2}\/\d{2}$/) !== null) {this.value = v + '/';}" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Gender <sup>*</sup></h2>
                                                                <asp:DropDownList ID="ddl_gender" runat="server" CssClass="form_control">
                                                                    <asp:ListItem>Male</asp:ListItem>
                                                                    <asp:ListItem>Female</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Nationality<sup>*</sup></h2>
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
                                                                <h2 class="online_frm-grp-h">Religion<sup>*</sup></h2>
                                                                <asp:DropDownList ID="ddl_religion" runat="server" CssClass="form_control">
                                                                    <asp:ListItem>Hindu</asp:ListItem>
                                                                    <asp:ListItem>Islam</asp:ListItem>
                                                                    <asp:ListItem>Sikh</asp:ListItem>
                                                                    <asp:ListItem>Christian</asp:ListItem>
                                                                    <asp:ListItem>Buddhism</asp:ListItem>
                                                                    <asp:ListItem>Jain</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Caste<sup>*</sup></h2>
                                                                <asp:DropDownList ID="ddl_Category" runat="server" class="form_control"></asp:DropDownList>
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
                                                                <h2 class="online_frm-grp-h">Identification marks</h2>
                                                                <asp:TextBox ID="txt_identification_marks" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="display: none">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Type of Admission<sup> </sup></h2>
                                                                <asp:RadioButton ID="rd_day" runat="server" Text="Day Scholar" Checked="true" GroupName="ak" AutoPostBack="false" class="chkstle" Style="margin: 3px 5px 0px 0px;" />

                                                                <asp:RadioButton Visible="false" ID="rd_dayboarding" runat="server" Text="Day Boarding" GroupName="ak" AutoPostBack="true" class="chkstle" Style="margin: 3px 5px 0px 0px;" />

                                                                <asp:RadioButton ID="rd_hostel" runat="server" Text="Hosteler" GroupName="ak" AutoPostBack="false" Visible="false" class="chkstle" Style="margin: 3px 5px 0px 0px;" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Class<sup>*</sup></h2>
                                                                <asp:DropDownList ID="ddl_class" runat="server" CssClass="form_control" AutoPostBack="true"   ></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Registration Fees<sup>*</sup></h2>
                                                                <asp:TextBox ID="txt_reg_fees" Enabled="false" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_cack_std" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_cack_std_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_std_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_std_dt_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>


                                            <asp:Panel ID="pn_prev_info" runat="server" Visible="false">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Previous School Details</h2>
                                                    <p class="online_frm-p">Enter your previous school details to process your registration.</p>
                                                </div>

                                                <div class="online_frm-inr">
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Name of last school attended<sup> </sup></h2>
                                                                <asp:TextBox ID="txt_lastschool" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Board<sup> </sup></h2>
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

                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_back_pre_school" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_pre_school_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_prev_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_prev_dt_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnl_father_dt" runat="server" Visible="false">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Father’s Contact Details</h2>
                                                    <p class="online_frm-p">Enter your father’s contact details to process your registration.</p>
                                                </div>

                                                <div class="online_frm-inr">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">First Name <sup>*</sup></h2>
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
                                                                <h2 class="online_frm-grp-h">Mobile No.<sup>*</sup></h2>
                                                                <div class="row">
                                                                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px">
                                                                        <asp:DropDownList ID="ddl_cunterycode1" runat="server" class="form_control" Style="border-radius: 4px 0px 0px 4px;"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-left: 0px">
                                                                        <asp:TextBox ID="txt_father_mobile" runat="server" CssClass="form_control" Style="border-radius: 0px 4px 4px 0px;" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                    </div>
                                                                </div>
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
                                                                <h2 class="online_frm-grp-h">Annual Income<sup>*</sup></h2>
                                                                <asp:TextBox ID="txt_annual_income" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_back_father" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_father_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_fther_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_fther_dt_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnl_mther_dt" runat="server" Visible="false">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Mother’s Contact Details</h2>
                                                    <p class="online_frm-p">Enter your mother’s contact details to process your registration.</p>
                                                </div>

                                                <div class="online_frm-inr">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">First Name <sup>*</sup></h2>
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
                                                                <div class="row">
                                                                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4" style="padding-right: 0px">
                                                                        <asp:DropDownList ID="ddl_cunterycode2" runat="server" class="form_control" Style="border-radius: 4px 0px 0px 4px;"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-lg-9 col-md-9 col-sm-8 col-xs-8" style="padding-left: 0px">
                                                                        <asp:TextBox ID="txt_mother_mobile_no" runat="server" CssClass="form_control" Style="border-radius: 0px 4px 4px 0px;" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                    </div>
                                                                </div>
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
                                                                <asp:TextBox ID="txt_mother_income" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_back_mother" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_mother_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_mther_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_mther_dt_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>


                                            <asp:Panel ID="pnl_address_dt" runat="server" Visible="false">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Address details</h2>
                                                    <p class="online_frm-p">Enter your address  details to process your registration.</p>
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
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Present P.O. <sup></sup></h2>
                                                                <asp:TextBox ID="txt_present_po" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
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
                                                                <asp:TextBox ID="txt_pincode" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:CheckBox ID="chkCopyHomeAddress" runat="server" Text=" Same as Present Address" />
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Permanent Address <sup></sup></h2>
                                                                <asp:TextBox ID="txt_pAddress" runat="server" CssClass="form_control" TextMode="MultiLine" Style="height: 70px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Permanent P.O. <sup></sup></h2>
                                                                <asp:TextBox ID="txt_perma_po" runat="server" CssClass="form_control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
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
                                                                <h2 class="online_frm-grp-h">Permanent Pin Code<sup> </sup></h2>
                                                                <asp:TextBox ID="txt_Ppincod" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_back_add" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_add_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_add_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_add_dt_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>



                                            <asp:Panel ID="pnl_misc_dt" runat="server" Visible="false">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Misc Details</h2>
                                                    <p class="online_frm-p">Enter misc. contact details to process your registration.</p>
                                                </div>

                                                <div class="online_frm-inr">

                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Is the student handicapped</h2>
                                                                <asp:RadioButton ID="rd_handicp_yes" class="chkstle" runat="server" GroupName="ak" Text="Yes" />
                                                                <asp:RadioButton ID="rd_handicp_no" runat="server" Checked="false" GroupName="ak" class="chkstle" Text="No" />
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

                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_back_misc" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_misc_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_misc_dt" runat="server" Text="Next" class="acc-dt-sub-btn" OnClick="btn_misc_dt_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnl_docs" runat="server" Visible="false">
                                                <div class="online_frm-hdg">
                                                    <h2 class="online_frm-h">Upload Document<sup></sup></h2>
                                                    <p class="online_frm-p">Upload document to process your registration.(only jpg,png 200kb max size)</p>
                                                </div>

                                                <div class="online_frm-inr">
                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Student Recent passport size photograph<sup> </sup></h2>
                                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form_control" />
                                                                <br />
                                                                <asp:Button ID="btn_upload_student_image" runat="server" OnClick="btn_upload_student_image_Click" Text="Upload Student Image" Style="height: 29px; font-size: 12px; font-weight: bold;" />



                                                                <asp:Label ID="lbl_std_img" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Image ID="img_student_image" runat="server" Visible="false" Style="height: 150px; width: 100px; padding: 2px; border: 2px solid #000;" />

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Father's Signature<sup> </sup></h2>
                                                                <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form_control" />
                                                                <br />
                                                                <asp:Button ID="btn_upload_father_sig" runat="server" OnClick="btn_upload_father_sig_Click" Text="Upload Father's Signature" Style="height: 29px; font-size: 12px; font-weight: bold;" />
                                                                <asp:Label ID="lbl_father_signature" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Image ID="img_father_sig" runat="server" Visible="false" Style="height: 50px; width: 150px; padding: 2px; border: 2px solid #000;" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Mother's Signature<sup> </sup></h2>
                                                                <asp:FileUpload ID="FileUpload3" runat="server" CssClass="form_control" />
                                                                <br />
                                                                <asp:Button ID="btn_mother_signature" runat="server" OnClick="btn_mother_signature_Click" Text="Upload Mother's Signature" Style="height: 29px; font-size: 12px; font-weight: bold;" />
                                                                <asp:Label ID="lbl_mother_signature" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Image ID="img_mother_signature" Visible="false" runat="server" Style="height: 50px; width: 150px; padding: 2px; border: 2px solid #000;" />

                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Passport size photo of Mother.<sup> </sup></h2>
                                                                <asp:FileUpload ID="file_mother_photo" runat="server" CssClass="form_control" />
                                                                <br />
                                                                <asp:Button ID="btn_mothers_photo" runat="server" OnClick="btn_mothers_photo_Click" Text="Upload Mother's Photo" Style="height: 29px; font-size: 12px; font-weight: bold;" />
                                                                <asp:Label ID="lbl_mother_photo" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Image ID="img_mother_photo" Visible="false" runat="server" Style="height: 150px; width: 150px; padding: 2px; border: 2px solid #000;" />

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Passport size photo of Father.<sup> </sup></h2>
                                                                <asp:FileUpload ID="file_father_photo" runat="server" CssClass="form_control" />
                                                                <br />
                                                                <asp:Button ID="btn_upload_father_photo" runat="server" OnClick="btn_upload_father_photo_Click" Text="Upload Father's Photo" Style="height: 29px; font-size: 12px; font-weight: bold;" />
                                                                <asp:Label ID="lbl_father_photo" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Image ID="img_father_photo" runat="server" Visible="false" Style="height: 150px; width: 150px; padding: 2px; border: 2px solid #000;" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Family photo(Father,Mother,Ward)<sup> </sup></h2>
                                                                <asp:FileUpload ID="file_upload_Family_photo" runat="server" CssClass="form_control" />
                                                                <br />
                                                                <asp:Button ID="btn_upload_Family_photo" runat="server" OnClick="btn_upload_Family_photo_Click" Text="Upload Family Photo" Style="height: 29px; font-size: 12px; font-weight: bold;" />

                                                                <asp:Label ID="lbl_Family_photo" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Image ID="img_Family_photo" runat="server" Visible="false" Style="height: 150px; width: 150px; padding: 2px; border: 2px solid #000;" />

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Birth Certificate of the child<sup> </sup></h2>
                                                                <asp:FileUpload ID="file_birth_Certificate" runat="server" CssClass="form_control" />
                                                                <br />
                                                                <asp:Button ID="btn_upload_birth_certificate" runat="server" OnClick="btn_upload_birth_certificate_Click" Text="Upload Birth Certificate" Style="height: 29px; font-size: 12px; font-weight: bold;" />

                                                                <asp:Label ID="lbl_Birth_Certificate" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Image ID="img_Birth_Certificate" runat="server" Visible="false" Style="height: 150px; width: 150px; padding: 2px; border: 2px solid #000;" />

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <h2 class="online_frm-grp-h">Residential Certificate<sup> </sup></h2>
                                                                <asp:FileUpload ID="file_Residential_Certificate" runat="server" CssClass="form_control" />
                                                                <br />
                                                                <asp:Button ID="btn_upload_Residential_Certificate" runat="server" OnClick="btn_upload_Residential_Certificate_Click" Text="Upload Residential Certificate" Style="height: 29px; font-size: 12px; font-weight: bold;" />

                                                                <asp:Label ID="lbl_Residential_Certificate" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Image ID="img_Residential_Certificate" runat="server" Visible="false" Style="height: 150px; width: 150px; padding: 2px; border: 2px solid #000;" />

                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_back_doc" runat="server" Text="Back" class="acc-dt-sub-back-btn" OnClick="btn_back_doc_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <div class="online_frm-grp">
                                                                <asp:Button ID="btn_final_submit" runat="server" Text="Submit" class="acc-dt-sub-btn" OnClick="btn_final_submit_Click" />
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
                                            <a class="btn" style="background-color: #00cc0c; padding: 4px 7px 3px 11px; font-size: 16px; margin: 0px 0px 9px 0px; color: #fff;"
                                                href="online-registration.aspx">Back</a>
                                        </div>

                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number" runat="server" id="pronumS1">1</p>
                                            <p class="steps-bx-txt-p" runat="server" id="prontxt1">Account Details</p>
                                        </div>
                                        <div class="steps-root" runat="server" id="pro1"></div>

                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number" runat="server" id="pronumS2">2</p>
                                            <p class="steps-bx-txt-p" runat="server" id="prontxt2">Student Details</p>
                                        </div>
                                        <div class="steps-root" runat="server" id="pro2"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number" runat="server" id="pronumS3">3</p>
                                            <p class="steps-bx-txt-p" runat="server" id="prontxt3">Previous School Details</p>
                                        </div>
                                        <div class="steps-root" runat="server" id="pro3"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number" runat="server" id="pronumS4">4</p>
                                            <p class="steps-bx-txt-p" runat="server" id="prontxt4">Father’s Contact Details</p>
                                        </div>
                                        <div class="steps-root" runat="server" id="pro4"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number" runat="server" id="pronumS5">5</p>
                                            <p class="steps-bx-txt-p" runat="server" id="prontxt5">Mother’s Contact Details</p>
                                        </div>
                                        <div class="steps-root" runat="server" id="pro5"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number" runat="server" id="pronumS6">6</p>
                                            <p class="steps-bx-txt-p" runat="server" id="prontxt6">Addresss Details</p>
                                        </div>
                                        <div class="steps-root" runat="server" id="pro6"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number" runat="server" id="pronumS7">7</p>
                                            <p class="steps-bx-txt-p" runat="server" id="prontxt7">Misc Details</p>
                                        </div>
                                        <div class="steps-root" runat="server" id="pro7"></div>
                                        <div class="steps-bx-dv">
                                            <p class="steps-bx-number" runat="server" id="pronumS8">8</p>
                                            <p class="steps-bx-txt-p" runat="server" id="prontxt8">Upload Document</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>


        <script>
            function IsCharacter(e) {
                var charCode = (e.which) ? e.which : e.keyCode;
                if (!(charCode >= 65 && charCode <= 90) && !(charCode >= 97 && charCode <= 122) && (charCode != 32 && charCode != 8) && !(charCode == 9)) {
                    return false;
                }
                return true;
            }

            //===============
            function validateEmail(emailField) {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                if (reg.test(emailField.value) == false) {

                    $("#<%=txt_std_email_id.ClientID %>").addClass("txtbxError");

                    $("#<%=txt_std_email_id.ClientID %>").val('');
                    messge("Please enter valid email id.");
                    return false;
                }
                $("#<%=txt_std_email_id.ClientID %>").removeClass("txtbxError");
                return true;
            }

            //==============
            $(document).ready(function () {
                $("#<%=txt_std_mob.ClientID %>").on("blur", function () {
                    var mobNum = $(this).val();
                    var filter = /^\d*(?:\.\d{1,2})?$/;

                    if (filter.test(mobNum)) {
                        if (mobNum.length == 10) {
                            $("#<%=txt_std_mob.ClientID %>").removeClass("txtbxError");
                        }
                        else {
                            $("#<%=txt_std_mob.ClientID %>").addClass("txtbxError");
                            var mobNum = $(this).val('');
                            return false;
                        }
                    }
                    else {
                        $("#<%=txt_std_mob.ClientID %>").addClass("txtbxError");
                        var mobNum = $(this).val('');
                        return false;
                    }
                });
            });

            //================

            $(document).ready(function () {

                //Function to copy from txtShiptoAddress to txtShiptoAddress when checkbox is checked
                $('#<%=chkCopyHomeAddress.ClientID%>').on("click", function () {
                    if ($(this).is(":checked")) {
                        $('#<%=txt_pAddress.ClientID %>').val($('#<%=txt_adress.ClientID %>').val());
                    }
                    if ($(this).is(":checked")) {
                        $('#<%=txt_perma_po.ClientID %>').val($('#<%=txt_present_po.ClientID %>').val());
                    }
                    if ($(this).is(":checked")) {
                        $('#<%=txt_perma_disctrict.ClientID %>').val($('#<%=txt_present_district.ClientID %>').val());
                    }
                    if ($(this).is(":checked")) {
                        $('#<%=txt_pcity.ClientID %>').val($('#<%=txt_city.ClientID %>').val());
                    }
                    if ($(this).is(":checked")) {
                        $('#<%=txt_Pstate.ClientID %>').val($('#<%=txt_State.ClientID %>').val());
                    }
                    if ($(this).is(":checked")) {
                        $('#<%=txt_Ppincod.ClientID %>').val($('#<%=txt_pincode.ClientID %>').val());
                    }

                });

                //Function to copy from txtShiptoAddress to txtShiptoAddress when checkbox is checked and user modifies it

                if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                    $('#<%=txt_pAddress.ClientID %>').val($('#<%=txt_adress.ClientID %>').val());
                }
                if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                    $('#<%=txt_perma_po.ClientID %>').val($('#<%=txt_present_po.ClientID %>').val());
                }
                if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                    $('#<%=txt_perma_disctrict.ClientID %>').val($('#<%=txt_present_district.ClientID %>').val());
                }
                if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                    $('#<%=txt_pcity.ClientID %>').val($('#<%=txt_city.ClientID %>').val());
                }
                if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                    $('#<%=txt_Pstate.ClientID %>').val($('#<%=txt_State.ClientID %>').val());
                }
                if ($('#<%=chkCopyHomeAddress.ClientID%>').is(":checked")) {
                    $('#<%=txt_Ppincod.ClientID %>').val($('#<%=txt_pincode.ClientID %>').val());
                }

            });
        </script>

        <%---------------------------------------------------%>

        <style>
            @media (min-width: 576px) {
                .modal-dialog {
                    max-width: 916px;
                    margin: 1.75rem auto;
                }
            }

            .switch {
                position: relative;
                display: inline-block;
                width: 50px;
                height: 24px;
            }

                .switch input {
                    opacity: 0;
                }

            .slider {
                position: absolute;
                cursor: pointer;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: #ccc;
                -webkit-transition: .4s;
                transition: .4s;
            }

                .slider:before {
                    position: absolute;
                    content: "";
                    height: 16px;
                    width: 16px;
                    left: 4px;
                    bottom: 4px;
                    background-color: white;
                    -webkit-transition: .4s;
                    transition: .4s;
                }

            input:checked + .slider {
                background-color: #2196F3;
            }

            input:focus + .slider {
                box-shadow: 0 0 1px #2196F3;
            }

            input:checked + .slider:before {
                -webkit-transform: translateX(26px);
                -ms-transform: translateX(26px);
                transform: translateX(26px);
            }

            /* Rounded sliders */
            .slider.round {
                border-radius: 34px;
            }

                .slider.round:before {
                    border-radius: 50%;
                }
        </style>
        <!-------popupadd year----->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Fill By Form Summery</h5>
                        <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                    </div>
                    <div class="modal-body">

                        <div class="p-4 border rounded">
                            <div class="row g-3 needs-validation" novalidate="">

                                <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Student Name</th>
                                            <th>Class Name</th>
                                            <th>Father Name</th>
                                            <th>Session</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rd_view" runat="server">
                                            <ItemTemplate>

                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Class")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("Father_name")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                    </td>

                                                    <td style="text-align: left;">
                                                        <asp:Label ID="lbl_session_id" runat="server" Visible="false" Text='<%#Bind("Session_id")%>'></asp:Label>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Select"> Select </asp:LinkButton>
                                                        <asp:Label ID="lbl_Registration_id" runat="server" Text='<%#Bind("Registration_id")%>' Visible="false"></asp:Label>




                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>


                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
        <div id="fadeup"></div>
        <link href="Autocomplete/jquery-ui.css" rel="stylesheet" />
        <script src="Autocomplete/jquery-ui.js"></script>
        <script type="text/javascript">
            function openModal() {
                $("#myModal").show();
                $('#myModal').addClass('show');
                $('#fadeup').addClass('modal-backdrop fade show');
            }
            function close() {
                $("#myModal").hide();
                $('#myModal').removeClass('show');
                $('#fadeup').removeClass('modal-backdrop fade show');
            }
        </script>
        <asp:HiddenField ID="hd_applicationid" runat="server" />



    </form>
</body>
</html>
