<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Apply_Career_Application.aspx.cs" Inherits="school_web.Payroll.Apply_Career_Application" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Apply Career Application</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="keywords" content="" /> 
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/js/jquery-1.10.2.min.js"></script> 
    <script src="assets/js/bootstrap.min.js"></script>
    <link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=IBM+Plex+Mono:ital@0;1&family=IBM+Plex+Sans+Condensed:ital@0;1&family=IBM+Plex+Sans:ital,wght@0,100;0,400;0,700;1,100;1,400;1,700&family=IBM+Plex+Serif:ital@0;1&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Open+Sans:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;1,300;1,400;1,500;1,600;1,700;1,800&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&family=Syne+Mono&display=swap" rel="stylesheet" />
     
    <link href="css/registration.css" rel="stylesheet" /> 
    <link href="../Content/theme/plugins/fontawesome-free/css/fontawesome.min.css" rel="stylesheet" />
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
            font-size: 18px;
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

        th {
            text-align: center !important;
            font-size: 15px !important;
            padding: 6px 5px 6px 0px !important;
            text-align: center !important;
        }

        td {
            text-align: left !important;
            font-size: 15px !important;
            padding: 2px 0px 0px 5px;
        }

        .online_frm-p {
            height: auto;
            width: 100%;
            margin: 0px 0px 10px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-size: 14px;
            line-height: 30px;
            color: #000;
            font-weight: bold;
            font-family: 'IBM Plex Sans', sans-serif;
        }

        .textcheckbbbb {
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            font-size: 16px;
            line-height: 23px;
            font-weight: 500;
            color: #333;
            text-align: justify;
            float: left;
            font-family: 'IBM Plex Sans', sans-serif;
            font-weight: bold;
        }

        input[type=checkbox], input[type=radio] {
            border: 1px solid #b5b5b5;
            border-radius: 2px;
            cursor: pointer;
            height: 20px;
            margin: 0 4px 0 0;
            transition: border-color .2s linear,background-color .2s linear,box-shadow .2s linear;
            vertical-align: text-bottom;
            width: 20px;
        }

        label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 700;
            font-size: 17px !important;
        }

        sup {
            color: red;
            font-size: 16px !important;
            font-weight: bold !important;
        }

        .table {
            width: 100%;
            max-width: 100%;
            margin-bottom: 20px;
            background: #f4f1f1 !important;
        }

        .table-bordered {
            border: 1px solid #ddd;
            background: #6de581 !important;
        }

        .form_control {
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 15px 3px 9px;
            float: left;
            border: 1px solid rgb(18 18 19);
            font-size: 14px;
            color: #000;
            height: 34px;
            text-align: left;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%);
            box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            font-family: 'IBM Plex Sans', sans-serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section>
            <div class="form-wprs">
                <div class="container"> 
                    <div class="row">
                        <div class="heder2" style="width: 100%;"> 
                             <div class="col-sm-12 col-md-2" style="float: left"> 
                                <div style="margin: 0px; padding: 0px; height: 110px; width: 100%; float: left;">
                                    <a href="../Default.aspx">
                                        <img src="images/slogo.png" runat="server" id="school_logo" class="img-responsive schoollogoimg" style="width: 150px; height: 150px;" /></a>
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



                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 25px; width: 100%;">
                                        <span style="font-size: 25px; font-weight: bold;">STAFF RECRUITMENT FORM   </span>


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
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="online_frm-hdg">

                                <h2 class="online_frm-h" style="display: none">The following personnel are required for the Academic Session
                                    <asp:Label ID="lbl_session" runat="server" Text="2023-2024"></asp:Label></h2>
                                <p class="online_frm-p">Note: All <sup style="top: -2px;">'*'</sup> marked fields are mandatory. Please mention'NA' if not applicable.</p>
                                <p class="online_frm-p" style="display:none">

                                    <asp:Label ID="lbl_session1" runat="server" Text="2023-2024"></asp:Label>.
                                </p>
                                <p class="online_frm-p">
                                </p>
                            </div>

                            <div class="online_frm-hdg">

                                <h2 class="online_frm-h">Job Information
                                </h2>
                                <div class="online_frm-inr">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Post Applied For: <sup>*</sup></h2>
                                                <asp:TextBox ID="txt_position_job_for" runat="server" CssClass="form_control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                       <%-- <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" id="subjecttype1" runat="server" visible="false">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Type of the Post: <sup>*</sup></h2>
                                                <asp:DropDownList ID="ddl_subject_type" runat="server" CssClass="form_control" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_type_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" id="subject_type2" runat="server" visible="false">
                                            <div class="online_frm-grp">

                                                <h2 class="online_frm-grp-h">Subject: <sup>*</sup></h2>
                                                <asp:DropDownList ID="ddl_subject_name" runat="server" CssClass="form_control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                        </div>
                                    </div>
                                </div>

                                <h2 class="online_frm-h">Personal Information
                                </h2>
                                <div class="online_frm-inr">
                                    <div class="row">
                                        <div class="col-lg-2">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Salutation : <sup></sup></h2>
                                                <asp:DropDownList ID="ddl_salution_name" runat="server" CssClass="form_control"> 
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">First Name <sup>*</sup></h2>
                                                <asp:TextBox ID="txt_first_name" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Middle Name <sup></sup></h2>
                                                <asp:TextBox ID="txt_Middle_Name" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Last Name <sup></sup></h2>
                                                <asp:TextBox ID="txt_last_Name" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                       

                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Email <sup>*</sup></h2>
                                                <asp:TextBox ID="txt_emailid" runat="server" CssClass="form_control" type="email" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Date of Birth <sup>*</sup></h2>
                                                <asp:TextBox ID="txt_date_birthday" TextMode="Date" runat="server" CssClass="form_control"></asp:TextBox>
                                            </div>
                                        </div>
                                    
                                       
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Place Of Birth <sup></sup></h2>
                                                <asp:TextBox ID="txt_plac_of_birth" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Birth State <sup></sup></h2>
                                                <asp:TextBox ID="txt_birthstate" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Gender <sup></sup></h2>
                                                <asp:DropDownList ID="ddl_gender" runat="server" CssClass="form_control">
                                                    <asp:ListItem>MALE</asp:ListItem>
                                                    <asp:ListItem>FEMALE</asp:ListItem>
                                                    <asp:ListItem>THIRD GENDER</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Religion <sup>*</sup></h2>
                                                <asp:DropDownList ID="ddl_religion" runat="server" CssClass="form_control">
                                                    <asp:ListItem>SELECT RELIGION</asp:ListItem>
                                                    <asp:ListItem>BUDDHIST</asp:ListItem>
                                                    <asp:ListItem>CHRISTIAN</asp:ListItem>
                                                    <asp:ListItem>HINDU</asp:ListItem>
                                                    <asp:ListItem>JAINISM</asp:ListItem>
                                                    <asp:ListItem>MUSLIM</asp:ListItem>
                                                    <asp:ListItem>OTHERS</asp:ListItem>
                                                    <asp:ListItem>SIKH</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Nationality<sup>*</sup></h2>
                                                <asp:TextBox ID="txt_Nationality" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)">INDIAN</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Marital Status: <sup></sup></h2>
                                                <asp:DropDownList ID="ddl_marital_status" runat="server" CssClass="form_control">
                                                    <asp:ListItem>SELECT MARITAL STATUS</asp:ListItem>
                                                    <asp:ListItem>MARRIED</asp:ListItem>
                                                    <asp:ListItem>UNMARRIED</asp:ListItem>
                                                    <asp:ListItem>DIVORCED</asp:ListItem>
                                                    <asp:ListItem>WIDOWED</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <h2 class="online_frm-h">Communication Address
                                </h2>
                                <div class="online_frm-inr">

                                    <div class="row">


                                        <div class="col-lg-6">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Address  </h2>
                                                <asp:TextBox ID="txt_address_ca" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">City <sup></sup></h2>
                                                <asp:TextBox ID="txt_city_ca" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                     
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">State <sup></sup></h2>
                                                <asp:TextBox ID="txt_state_ca" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Pin Code <sup></sup></h2>
                                                <asp:TextBox ID="txt_pincode_CA" runat="server" onkeypress="return isNumberKey(event)" CssClass="form_control" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Mobile No. <sup>*</sup></h2>
                                                <asp:TextBox ID="txt_mobile_no_CA" runat="server" onkeypress="return isNumberKey(event)" CssClass="form_control" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Residence Telephone No: <sup></sup></h2>
                                                <asp:TextBox ID="txt_residence_telephone_no" runat="server" onkeypress="return isNumberKey(event)" CssClass="form_control" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                </div>


                                <h2 class="online_frm-h">Permanent Address (Check this box if Communication Address & Permanent Address are the same.)<asp:CheckBox ID="chk_same_check_box" runat="server" OnCheckedChanged="chk_same_check_box_CheckedChanged" AutoPostBack="true" />
                                </h2>
                                <div class="online_frm-inr">
                                    <div class="row">


                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Address  </h2>
                                                <asp:TextBox ID="txt_address_pa" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">City <sup></sup></h2>
                                                <asp:TextBox ID="txt_city_pa" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">State <sup></sup></h2>
                                                <asp:TextBox ID="txt_state_pa" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Pin Code <sup></sup></h2>
                                                <asp:TextBox ID="txt_pin_pa" runat="server" onkeypress="return isNumberKey(event)" CssClass="form_control" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                        </div>
                                    </div>
                                </div>

                                <h2 class="online_frm-h">Family Information (Children Information)
                                </h2>
                                <div class="online_frm-inr">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Name:</h2>
                                                <asp:TextBox ID="txt_chiled_name1" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Gender <sup></sup></h2>
                                                <asp:DropDownList ID="ddl_chiled_gender1" runat="server" CssClass="form_control">
                                                    <asp:ListItem>MALE</asp:ListItem>
                                                    <asp:ListItem>FEMALE</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Age:</h2>
                                                <asp:TextBox ID="txt_chiled_age1" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" MaxLength="2" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Name:</h2>
                                                <asp:TextBox ID="txt_chiled_name2" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Gender <sup></sup></h2>
                                                <asp:DropDownList ID="ddl_chiled_gender2" runat="server" CssClass="form_control">
                                                    <asp:ListItem>MALE</asp:ListItem>
                                                    <asp:ListItem>FEMALE</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Age:</h2>
                                                <asp:TextBox ID="txt_chiled_age2" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" MaxLength="2" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Name:</h2>
                                                <asp:TextBox ID="txt_chiled_name3" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Gender <sup></sup></h2>
                                                <asp:DropDownList ID="ddl_chiled_gender3" runat="server" CssClass="form_control">
                                                    <asp:ListItem>MALE</asp:ListItem>
                                                    <asp:ListItem>FEMALE</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Age:</h2>
                                                <asp:TextBox ID="txt_chiled_age3" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" MaxLength="2" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Father's Name</h2>
                                                <asp:TextBox ID="txt_fathername" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Father's Occupation<sup></sup></h2>
                                                <asp:TextBox ID="txt_father_occupation" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>


 
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Mother's Name</h2>
                                                <asp:TextBox ID="txt_mother_name" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Mother's Occupation<sup></sup></h2>
                                                <asp:TextBox ID="txt_mother_occupation" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Spouse Name</h2>
                                                <asp:TextBox ID="txt_Spouse_name" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div> 
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Spouse's Qualification</h2>
                                                <asp:TextBox ID="txt_spouse_qualification" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div> 
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Spouse's Profession<sup></sup></h2>
                                                <asp:TextBox ID="txt_spouse_Profession" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Spouse's job is transferable<sup></sup></h2>
                                                <asp:DropDownList ID="ddl_spouse_transferableye_no" runat="server" CssClass="form_control">
                                                    <asp:ListItem>NO</asp:ListItem>
                                                    <asp:ListItem>YES</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                     
                                       
                                       
                                    
                                        <div class="col-lg-6">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Spouse's Organization</h2>
                                                <asp:TextBox ID="txt_spouse_organization" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Spouse's Designation<sup></sup></h2>
                                                <asp:TextBox ID="txt_spouse_designation" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>




                                </div>



                                <h2 class="online_frm-h">Academic/Professional Qualification (Please mention 'NA' if not applicable)
                                </h2>

                                <div class="online_frm-inr">

                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                            <table class="table-bordered">
                                                <tr>
                                                    <th>Qualification</th>
                                                    <th>Main Subjects</th>
                                                    <th>School/College</th>
                                                    <th>Board/University</th>
                                                    <th>Year of Passing</th>
                                                    <th>Percentage of Marks</th>
                                                    <th>Division</th>

                                                </tr>
                                                <tr>
                                                    <td>High School/10th<sup> </sup></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_tenth_main_subject" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_tenth_school_collage_name" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_tenth_board_university" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_tenth_passing_year" runat="server" class="form-control find-dv-txtbx" MaxLength="4" onkeypress="return isNumberKey(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_tenth_percentage_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_tenth_division" runat="server" CssClass="form_control" Style="padding: 2px 5px 1px 4px!important; height: 34px!important;">
                                                            <asp:ListItem>SELECT</asp:ListItem>
                                                            <asp:ListItem>DISTINCTION</asp:ListItem>
                                                            <asp:ListItem>FIRST</asp:ListItem>
                                                            <asp:ListItem>SECOND</asp:ListItem>
                                                            <asp:ListItem>THIRD</asp:ListItem>
                                                            <asp:ListItem>NA</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>Inter/+2<sup> </sup></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_11th_main_subject" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_11th_school_collage_name" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_11th_board_university" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_11th_passing_year" runat="server" class="form-control find-dv-txtbx" MaxLength="4" onkeypress="return isNumberKey(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_11th_percentage_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_11th_division" runat="server" CssClass="form_control" Style="padding: 2px 5px 1px 4px!important; height: 34px!important;">
                                                            <asp:ListItem>SELECT</asp:ListItem>
                                                            <asp:ListItem>DISTINCTION</asp:ListItem>
                                                            <asp:ListItem>FIRST</asp:ListItem>
                                                            <asp:ListItem>SECOND</asp:ListItem>
                                                            <asp:ListItem>THIRD</asp:ListItem>
                                                            <asp:ListItem>NA</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>Graduation<sup> </sup></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_Graduation_main_subject" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_Graduation_school_collage_name" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_Graduation_board_university" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_Graduation_passing_year" runat="server" class="form-control find-dv-txtbx" MaxLength="4" onkeypress="return isNumberKey(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_Graduation_percentage_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_Graduation_division" runat="server" CssClass="form_control" Style="padding: 2px 5px 1px 4px!important; height: 34px!important;">
                                                            <asp:ListItem>SELECT</asp:ListItem>
                                                            <asp:ListItem>DISTINCTION</asp:ListItem>
                                                            <asp:ListItem>FIRST</asp:ListItem>
                                                            <asp:ListItem>SECOND</asp:ListItem>
                                                            <asp:ListItem>THIRD</asp:ListItem>
                                                            <asp:ListItem>NA</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>Post Graduation<sup> </sup></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_PostGraduation_main_subject" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_PostGraduation_school_collage_name" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_PostGraduation_board_university" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_PostGraduation_passing_year" runat="server" class="form-control find-dv-txtbx" MaxLength="4" onkeypress="return isNumberKey(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_PostGraduation_percentage_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_PostGraduation_division" runat="server" CssClass="form_control" Style="padding: 2px 5px 1px 4px!important; height: 34px!important;">
                                                            <asp:ListItem>SELECT</asp:ListItem>
                                                            <asp:ListItem>DISTINCTION</asp:ListItem>
                                                            <asp:ListItem>FIRST</asp:ListItem>
                                                            <asp:ListItem>SECOND</asp:ListItem>
                                                            <asp:ListItem>THIRD</asp:ListItem>
                                                            <asp:ListItem>NA</asp:ListItem>
                                                        </asp:DropDownList>


                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>M Phil.<sup> </sup></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_MPhil_main_subject" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_MPhil_school_collage_name" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_MPhil_board_university" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_MPhil_passing_year" runat="server" class="form-control find-dv-txtbx" MaxLength="4" onkeypress="return isNumberKey(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_MPhil_percentage_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_MPhil_division" runat="server" CssClass="form_control" Style="padding: 2px 5px 1px 4px!important; height: 34px!important;">
                                                            <asp:ListItem>SELECT</asp:ListItem>
                                                            <asp:ListItem>DISTINCTION</asp:ListItem>
                                                            <asp:ListItem>FIRST</asp:ListItem>
                                                            <asp:ListItem>SECOND</asp:ListItem>
                                                            <asp:ListItem>THIRD</asp:ListItem>
                                                            <asp:ListItem>NA</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>B.Ed.<sup></sup></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_bed_main_subject" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_bed_school_collage_name" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_bed_board_university" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_bed_passing_year" runat="server" class="form-control find-dv-txtbx" MaxLength="4" onkeypress="return isNumberKey(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_bed_percentage_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_PHD_division" runat="server" CssClass="form_control" Style="padding: 2px 5px 1px 4px!important; height: 34px!important;">
                                                            <asp:ListItem>SELECT</asp:ListItem>
                                                            <asp:ListItem>DISTINCTION</asp:ListItem>
                                                            <asp:ListItem>FIRST</asp:ListItem>
                                                            <asp:ListItem>SECOND</asp:ListItem>
                                                            <asp:ListItem>THIRD</asp:ListItem>
                                                            <asp:ListItem>NA</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>

                                                </tr>



                                                <tr>
                                                    <td>CTET<sup></sup></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_bed_CTET_subject" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_CTET_school_collage_name" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_CTET_board_university" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_CTET_passing_year" runat="server" class="form-control find-dv-txtbx" MaxLength="4" onkeypress="return isNumberKey(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_CTET_percentage_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_CTET_division" runat="server" CssClass="form_control" Style="padding: 2px 5px 1px 4px!important; height: 34px!important;">
                                                            <asp:ListItem>SELECT</asp:ListItem>
                                                            <asp:ListItem>DISTINCTION</asp:ListItem>
                                                            <asp:ListItem>FIRST</asp:ListItem>
                                                            <asp:ListItem>SECOND</asp:ListItem>
                                                            <asp:ListItem>THIRD</asp:ListItem>
                                                            <asp:ListItem>NA</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>Any Other<sup></sup></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_anyother_subject" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_anyother_school_collage_name" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_anyother_board_university" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_anyother_passing_year" runat="server" class="form-control find-dv-txtbx" MaxLength="4" onkeypress="return isNumberKey(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_anyother_percentage_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_anyother_division" runat="server" CssClass="form_control" Style="padding: 2px 5px 1px 4px!important; height: 34px!important;">
                                                            <asp:ListItem>SELECT</asp:ListItem>
                                                            <asp:ListItem>DISTINCTION</asp:ListItem>
                                                            <asp:ListItem>FIRST</asp:ListItem>
                                                            <asp:ListItem>SECOND</asp:ListItem>
                                                            <asp:ListItem>THIRD</asp:ListItem>
                                                            <asp:ListItem>NA</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>

                                                </tr>




                                            </table>


                                        </div>
                                    </div>

                                </div>

                                <h2 class="online_frm-h">Work Experience (From Latest To Oldest)
                                    <asp:CheckBox ID="chk_iam_fresher" runat="server" Text="I 'm Fresher (PUT CHECK BOX)" />
                                </h2>
                                <div class="online_frm-inr">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <table class="table-bordered">
                                                <tr>
                                                    <th>Name of Organization<sup> </sup></th>
                                                    <th>From<sup> </sup></th>
                                                    <th>To<sup> </sup></th>
                                                    <th>Specifications<sup> </sup></th>
                                                    <%--<th>Other(if any)<sup> </sup></th>--%>
                                                    <th>Other Responsibilties<sup> </sup></th>
                                                    <th>Action</th>
                                                </tr>
                                                <tr>

                                                    <td>
                                                        <asp:TextBox ID="txt_organization" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_from" TextMode="Date" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_to" TextMode="Date" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>

                                                    <td>
                                                        <asp:TextBox ID="txt_subject" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>
                                                   <%-- <td>
                                                        <asp:TextBox ID="txt_class_teacher" runat="server" class="form-control find-dv-txtbx" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox></td>--%>
                                                    <td>
                                                        <asp:TextBox ID="txt_other_responsible" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_add_work_experiance" Style="padding: 5px 0px 4px 0px; margin: 0px 0px 0px 0px;"
                                                            runat="server" Text="Add" class="acc-dt-sub-btn" OnClick="btn_add_work_experiance_Click" />
                                                    </td>




                                                </tr>
                                                <tr>
                                                    <td colspan="7">
                                                        <asp:GridView ID="grid_work_experiance" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grid_work_experiance_RowDataBound" ShowFooter="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Organization">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Organization" Style="word-break: break-all" runat="server" Text='<%#Bind("Organization")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="From Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_From_Date" runat="server" Text='<%#Bind("From_Date")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="To Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_To_Date" runat="server" Text='<%#Bind("To_Date")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Days">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Total_Days" runat="server" Text='<%#Bind("Total_Days")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl_Total_Days_row" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Specifications">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Specifications" runat="server" Text='<%#Bind("Specifications")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Other(if any)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Cass_teacher" runat="server" Text='<%#Bind("Cass_teacher")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Other Responsible">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_other_responsible" runat="server" Text='<%#Bind("Other_responsible")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click1"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                        <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>




                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <h2 class="online_frm-h">Total Experience
                                 
                                </h2>
                                <div class="online_frm-inr">
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">In Completed Years</h2>
                                                <asp:TextBox ID="txt_in_completed_years" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Teaching  Experience </h2>
                                                <asp:TextBox ID="txt_teaching_years" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Administration  Experience </h2>
                                                <asp:TextBox ID="txt_Administration_year" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Any Other (Please Specify)</h2>
                                                <asp:TextBox ID="txt_any_other" runat="server" CssClass="form_control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <h2 class="online_frm-h">Current Job Information
                                 
                                </h2>
                                <div class="online_frm-inr">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Name Of Institution<sup> </sup></h2>
                                                <asp:TextBox ID="txt_name_of_instituation" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Address<sup> </sup></h2>
                                                <asp:TextBox ID="txt_instituation_address" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Institution Contact No<sup> </sup></h2>
                                                <asp:TextBox ID="txt_Contact_Numbe_instituation" runat="server" CssClass="form_control" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Work Designation<sup> </sup></h2>
                                                <asp:TextBox ID="txt_Designation_work" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Date of Joining<sup> </sup></h2>
                                                <asp:TextBox ID="txt_joining_date" TextMode="Date" runat="server" CssClass="form_control"></asp:TextBox>
                                            </div>
                                        </div>
                                    
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Place of Posting<sup> </sup></h2>
                                                <asp:TextBox ID="txt_place_of_posting" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Total Present Salary<sup> </sup></h2>
                                                <asp:TextBox ID="txt_Present_Salary" onkeypress="return isNumberKey(event)" runat="server" CssClass="form_control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Basic Salary<sup></sup></h2>
                                                <asp:TextBox ID="txt_basic_Salary_Present" runat="server" onkeypress="return isNumberKey(event)" CssClass="form_control"></asp:TextBox>
                                            </div>
                                        </div>


                                     
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Allowance<sup></sup></h2>
                                                <asp:TextBox ID="txt_Allowance_Present" runat="server" onkeypress="return isNumberKey(event)" CssClass="form_control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Other Benefits<sup></sup></h2>
                                                <asp:TextBox ID="txt_Other_Benefits_Present" runat="server" onkeypress="return isNumberKey(event)" CssClass="form_control"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Are You Under Service Bond?<asp:CheckBox ID="chk_service_bond" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chk_service_bond_CheckedChanged" /></h2>

                                            </div>
                                        </div>

                                        <div class="col-lg-3" id="no_boundpnl" runat="server" visible="false">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">No.of Years Service Bond<sup></sup></h2>
                                                <asp:TextBox ID="txt_no_of_years_service_bond" runat="server" onkeypress="return isNumberKey(event)" CssClass="form_control"></asp:TextBox>


                                            </div>
                                        </div>

                                    
                                        <div class="col-lg-3">
                                            <div class="online_frm-grp">
                                                <h2 class="online_frm-grp-h">Expected Salary<sup> </sup></h2>
                                                <asp:TextBox ID="txt_Expected_Salary" runat="server" onkeypress="return isNumberKey(event)" CssClass="form_control"></asp:TextBox>


                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <h2 class="online_frm-h">Proficiency In Languages
                                 
                                </h2>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <table class="table-bordered" style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 17px!important; padding: 6px 10px 6px 7px; font-weight: bold;">English<sup></sup></td>
                                                <td>
                                                    <asp:CheckBox ID="chk_english_read" runat="server" Text="Read" />
                                                </td>
                                                <td>

                                                    <asp:CheckBox ID="chk_english_write" runat="server" Text="Write" />

                                                </td>
                                                <td>

                                                    <asp:CheckBox ID="chk_english_Speak" runat="server" Text="Speak" />

                                                </td>



                                            </tr>
                                            <tr>
                                                <td style="font-size: 17px!important; padding: 6px 10px 6px 7px; font-weight: bold;">Hindi<sup></sup></td>
                                                <td>
                                                    <asp:CheckBox ID="chk_hindi_read" runat="server" Text="Read" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_hindi_write" runat="server" Text="Write" />

                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_hindi_speak" runat="server" Text="Speak" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 17px!important; padding: 6px 10px 6px 7px; font-weight: bold;">Bengali<sup></sup></td>
                                                <td>
                                                    <asp:CheckBox ID="chk_Bangla_read" runat="server" Text="Read" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_Bangla_write" runat="server" Text="Write" />

                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_Bangla_speak" runat="server" Text="Speak" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>


                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="online_frm-grp">
                                            <h2 class="online_frm-grp-h">Any Other Language Know<sup></sup></h2>
                                            <asp:TextBox ID="txt_any_other_language" runat="server" CssClass="form_control" oninput="this.value = this.value.toUpperCase()" onkeypress="return IsCharacter(event)"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="online_frm-grp">
                                            <h2 class="online_frm-grp-h">Proficiency In Computer<sup></sup></h2>
                                            <asp:DropDownList ID="ddl_computer_yesno" runat="server" CssClass="form_control">
                                                <asp:ListItem>NA</asp:ListItem>
                                                <asp:ListItem>NO</asp:ListItem>
                                                <asp:ListItem>YES</asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                    </div>




                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="online_frm-grp">
                                            <h2 class="online_frm-grp-h">Upload passport size (JPG/PNG)<sup> </sup></h2>
                                            <br />
                                            <p>(file size not more than 500 KB)</p>
                                            <asp:FileUpload ID="file_passport_photo" runat="server" CssClass="form_control" />
                                            <br />
                                            <asp:Button ID="btn_passport_photo" runat="server" OnClick="btn_passport_photo_Click" Text="Upload Passport Photo" Style="height: 29px; font-size: 12px; font-weight: bold; display:none" />

                                            <script>
                                                $('#<%=file_passport_photo.ClientID%>').on('change', function () { 
                                                    $('#<%=btn_passport_photo.ClientID%>').click();
                                                })
                                            </script>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="online_frm-grp">
                                            <asp:Image ID="img_passport_photo" Visible="false" runat="server" Style="height: 150px; width: 150px; padding: 2px; border: 2px solid #000;" />

                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="online_frm-grp">
                                            <h2 class="online_frm-grp-h">Upload Digital Signature<sup> </sup></h2>
                                            <br />
                                            <p>(file size not more than 200 KB)</p>
                                            <asp:FileUpload ID="file_upoad_Signature" runat="server" CssClass="form_control" />
                                            <br />
                                            <asp:Button ID="btn_upload_Signature" runat="server" OnClick="btn_upload_Signature_Click" Text="Upload Digital Signature" Style="height: 29px; font-size: 12px; font-weight: bold; display:none" />
                                              <script>
                                                  $('#<%=file_upoad_Signature.ClientID%>').on('change', function () { 
                                                    $('#<%=btn_upload_Signature.ClientID%>').click();
                                                })
                                              </script>

                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="online_frm-grp">
                                            <asp:Image ID="img_Signature" Visible="false" runat="server" Style="height: 50px; width: 150px; padding: 2px; border: 2px solid #000; margin: 44px 0px 0px 0px;" />

                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="online_frm-grp">
                                            <a class="textcheckbbbb">
                                                <asp:CheckBox ID="chk_declaration" runat="server" />

                                                I, the applicant, state that all information given above is true and correct. I understand that submission of the Application Form is a preliminary step in the selection ofstaff  and does not guarantee a job. I agree to abide by all decisions taken by the management.

                                            </a>
                                        </div>
                                    </div>



                                </div>

                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                        <div class="online_frm-grp">
                                            <asp:Button ID="btn_final_submit" runat="server" Text="NEXT" class="acc-dt-sub-btn" OnClick="btn_final_submit_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>








                    </div>

                </div>
            </div>
        </section>

        <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
        </script>
        <link href="assets/plugins/datetimepicker/css/classic.date.css" rel="stylesheet" /> 
        <script src="assets/plugins/datetimepicker/js/picker.date.js"></script>
       
        <script>
            $(function () {
                $("#<%=txt_from.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    readOnly: true,
                    maxDate: '0',
                    yearRange: "1900:2023",
                }).attr("readonly", "true");
            });
            $(function () {
                $("#<%=txt_to.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    readOnly: true,
                    maxDate: '0',
                    yearRange: "1900:2023",
                }).attr("readonly", "true");
            });
            $(function () {
                $("#<%=txt_date_birthday.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    readOnly: true,
                    maxDate: '0',
                    yearRange: "1900:2007",
                }).attr("readonly", "true");
            });
            $(function () {
                $("#<%=txt_joining_date.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    readOnly: true,
                    maxDate: '0',
                    yearRange: "1900:2023",
                }).attr("readonly", "true");
            });



        </script>



    </form>
</body>
</html>
