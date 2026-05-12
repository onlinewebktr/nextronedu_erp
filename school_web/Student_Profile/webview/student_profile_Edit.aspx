<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student_profile_Edit.aspx.cs" Inherits="school_web.Student_Profile.webview.student_profile_Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Student Profile</title>
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
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
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

        .texbox-border {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 1px solid #00000038;
        }

        .textcont1 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 11px;
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

        .admission_info {
            margin: 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
        }

        .sudent_info {
            margin: 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
        }

        .father_info {
            margin: 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
        }

        .mother_info {
            margin: 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
        }

        .Current {
            margin: 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
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
            $("#<%=lbl_dateofbirth1.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>
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
</head>
<body>
    <form id="form1" runat="server">
   <div class="admission_info">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 100%; height: auto;">
                        <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                    <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                        class="closenotificationpan" alt="" />
                </div>
            </div>
            <h2 class="messbox-sec-h2">Admission Information</h2>
            <h2 class="h2_right"></h2>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Admission Date  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbladmissiondate" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Session  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_session" runat="server" Text=""></asp:Label>
                    </p>
                </div>

            </div>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Admission In </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_admissionin" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>



            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Class </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_class" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Section </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_Section" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Admission No. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_admission_no" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Roll No. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_rollno" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">House </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_house" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Transportation </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_transprtaion" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>

        </div>
        <div class="sudent_info">
            <h2 class="messbox-sec-h2">Student's Information</h2>

            <div class="clearfix"></div>
            <div class="texbox-border" style="display: none">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: center">
                    <asp:Image ID="Image1" runat="server" Style="border: 1px solid #808080; height: 120px; width: 100px; padding: 2px;" />



                </div>

            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Student Name </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_student_name" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Date of Birth </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         
                         <asp:TextBox ID="lbl_dateofbirth1" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Place of Birth </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                       
                        
                         <asp:TextBox ID="lbl_palceof_birth" runat="server" Style="width: 96%;"></asp:TextBox>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Certificate No. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                      
                         <asp:TextBox ID="lbl_CertificateNo" runat="server" Style="width: 96%;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </p>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Blood Group </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_bloodgroup" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Aadhar No. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                          <asp:TextBox ID="lbl_aadharno" runat="server" Style="width: 96%;" MaxLength="12" onkeypress="return isNumberKey(event)"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Gender </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        

                        <asp:DropDownList ID="ddl_gender" runat="server" Style="width: 96%; height: 25px;">
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Religion </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_religion" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Ration Type                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:DropDownList ID="ddl_ration_type" runat="server" Style="width: 96%; height: 25px;">
                             <asp:ListItem>APL</asp:ListItem>
                             <asp:ListItem>BPL</asp:ListItem>
                             <asp:ListItem>N/A</asp:ListItem>
                         </asp:DropDownList>


                    </p>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Category </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:DropDownList ID="ddl_Category" runat="server" Style="width: 96%; height: 25px;">
                             <asp:ListItem>General </asp:ListItem>
                             <asp:ListItem>OBC</asp:ListItem>
                             <asp:ListItem>ST</asp:ListItem>
                             <asp:ListItem>SC</asp:ListItem>
                             <asp:ListItem>EBC</asp:ListItem>
                             <asp:ListItem>Others</asp:ListItem>
                         </asp:DropDownList>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Certificate No.</p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                     
                          <asp:TextBox ID="lbl_certificate" runat="server" Style="width: 96%;"></asp:TextBox>
                    </p>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Mother Tongue</p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                     
                        <asp:DropDownList ID="ddl_mother_tongue" runat="server" Style="width: 96%; height: 25px;">
                            <asp:ListItem>Hindi</asp:ListItem>
                            <asp:ListItem>English</asp:ListItem>
                            <asp:ListItem>Bengali</asp:ListItem>
                            <asp:ListItem>Marathi</asp:ListItem>
                            <asp:ListItem>Telugu</asp:ListItem>
                            <asp:ListItem>Tamil</asp:ListItem>
                            <asp:ListItem>Gujarati</asp:ListItem>
                            <asp:ListItem>Urdu</asp:ListItem>
                            <asp:ListItem>Kannada</asp:ListItem>
                            <asp:ListItem>Odia</asp:ListItem>
                            <asp:ListItem>Malayalam</asp:ListItem>
                            <asp:ListItem>Punjabi</asp:ListItem>
                            <asp:ListItem>Assamese</asp:ListItem>
                            <asp:ListItem>Maithili</asp:ListItem>
                            <asp:ListItem>Sanskrit</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Is any Illness </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        

                          <asp:DropDownList ID="ddl_anyillness" runat="server" Style="width: 96%; height: 25px;">
                              <asp:ListItem>NO</asp:ListItem>
                              <asp:ListItem>YES</asp:ListItem>
                          </asp:DropDownList>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Prev. School </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        
                          <asp:TextBox ID="lbl_prevschool" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Cast </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_cast" runat="server" Style="width: 96%;"></asp:TextBox>



                    </p>
                </div>
            </div>

        </div>


        <div class="father_info">
            <h2 class="messbox-sec-h2">Father's Information</h2>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Father's Name </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lblfathername" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Occupation </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         
                          <asp:DropDownList ID="ddl_ocupation" runat="server" Style="width: 96%; height: 25px;">
                              <asp:ListItem>Others</asp:ListItem>
                              <asp:ListItem>State Govt. Job</asp:ListItem>
                              <asp:ListItem>Central Govt. Job</asp:ListItem>
                              <asp:ListItem>Private Job</asp:ListItem>
                              <asp:ListItem>Business</asp:ListItem>
                              <asp:ListItem>Farmer</asp:ListItem>
                          </asp:DropDownList>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Qualification </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_qulification" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Nationality </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_Nationality" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Marital Status </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:DropDownList ID="ddl_martialstatus" runat="server" Style="width: 96%; height: 25px;">
                             <asp:ListItem>Married</asp:ListItem>
                             <asp:ListItem>Unmarried</asp:ListItem>
                             <asp:ListItem>Divorce</asp:ListItem>
                             <asp:ListItem>Single Parent</asp:ListItem>
                             <asp:ListItem>N/A</asp:ListItem>
                         </asp:DropDownList>

                    </p>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Mobile No.</p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_mobile_no" runat="server" Style="width: 96%;" onkeypress="return isNumberKey(event)"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Email Id</p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        
                         <asp:TextBox ID="lbl_emailid" runat="server" Style="width: 96%;" type="email"></asp:TextBox>
                    </p>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Guardian's Name</p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         
                        <asp:TextBox ID="lbl_guardianname" runat="server" Style="width: 96%;"></asp:TextBox>
                    </p>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Parent Income</p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        
                         <asp:TextBox ID="lbl_parent_income" runat="server" Style="width: 96%;"></asp:TextBox>
                    </p>
                </div>
            </div>


        </div>

        <div class="mother_info">
            <h2 class="messbox-sec-h2">Mother's Information</h2>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Mother's Name </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_mother" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Occupation </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                            <asp:DropDownList ID="ddl_occupation_mother" runat="server" Style="width: 96%; height: 25px;">
                                <asp:ListItem>House Wife</asp:ListItem>

                                <asp:ListItem>State Govt. Job</asp:ListItem>
                                <asp:ListItem>Central Govt. Job</asp:ListItem>
                                <asp:ListItem>Private Job</asp:ListItem>
                                <asp:ListItem>Business</asp:ListItem>
                                <asp:ListItem>Farmer</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>


                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Mother. Qualification </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_motherqulification" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Mobile No. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_mobileno_mother" runat="server" Style="width: 96%;" onkeypress="return isNumberKey(event)"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Email Id </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_emailcode" runat="server" Style="width: 96%;" type="email"></asp:TextBox>

                    </p>
                </div>
            </div>
        </div>

        <div class="Current">
            <h2 class="messbox-sec-h2">Current Address </h2>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Address </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_current" runat="server" Style="width: 96%;" TextMode="MultiLine"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Mobile No. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_mobile_no_current" runat="server" Style="width: 96%;" onkeypress="return isNumberKey(event)"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">City/Village </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_cityvillage_current" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">District </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_distict_current" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">P. O. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_po_current" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">P. S. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_ps_current" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">state </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                      
                        <asp:DropDownList ID="ddl_state_current" runat="server" Style="width: 96%; height: 25px;">
                        </asp:DropDownList>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Pin Code </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_pincode" runat="server" Style="width: 96%;" MaxLength="6" onkeypress="return isNumberKey(event)"></asp:TextBox>

                    </p>
                </div>
            </div>

        </div>


        <div class="Current">
            <h2 class="messbox-sec-h2">Permanent Address </h2>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Address </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_permanent_address" runat="server" Style="width: 96%;" TextMode="MultiLine"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Mobile No. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_mobile_no_permanent" runat="server" Style="width: 96%;" onkeypress="return isNumberKey(event)"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">City/Village </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        
                        <asp:TextBox ID="lbl_cityvillage_permanent" runat="server" Style="width: 96%;"></asp:TextBox>
                    </p>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">District </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        
                         <asp:TextBox ID="lbl_distict_permanent" runat="server" Style="width: 96%;"></asp:TextBox>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">P. O. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        
                          <asp:TextBox ID="lbl_po_permanent" runat="server" Style="width: 96%;"></asp:TextBox>
                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">P. S. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                       
                         <asp:TextBox ID="lbl_ps_permanent" runat="server" Style="width: 96%;"></asp:TextBox>
                    </p>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">state </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        
                        <asp:DropDownList ID="ddl_state_permanent" runat="server" Style="width: 96%; height: 25px;">
                        </asp:DropDownList>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Pin Code </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         <asp:TextBox ID="lbl_pincode_permanent" runat="server" Style="width: 96%;" MaxLength="6" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </p>
                </div>
            </div>








        </div>



        <div class="Current">
            <h2 class="messbox-sec-h2">Bank Details </h2>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Account Holder </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_account_holder_name" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>

              <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Account No </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="txt_account_no" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">IFSC Code </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_IFSCCode" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Bank Name </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_bankname" runat="server" Style="width: 96%;"></asp:TextBox>

                    </p>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Branch Name </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         
                        <asp:TextBox ID="lbl_branch_name" runat="server" Style="width: 96%;"></asp:TextBox>
                    </p>
                </div>
            </div>
            <div class="clearfix"></div>







        </div>
        <div class="Current">
            <h2 class="messbox-sec-h2">Password </h2>

            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Your Password </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         
                        <asp:TextBox ID="txt_password" runat="server" Style="width: 96%;"></asp:TextBox>
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


    </form>
</body>
</html>
