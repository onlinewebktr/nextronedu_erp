<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student_profile_Edit.aspx.cs" Inherits="school_web._adminETutorProf.webview.student_profile_Edit" %>

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
            z-index: 99999 !important;
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
        .notificationpan {
            display: none;
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
           top: 169px !important;
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
            top: 12px;
            right: 6px;
            cursor: pointer;
            left: auto;
            width: 16px;
        }

        #notification {
            margin: 0px;
            padding: 0px;
            position: relative;
            z-index: 999;
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
                    <img src="../../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                        class="closenotificationpan" alt="" />
                </div>
            </div>







        </div>
        <div class="sudent_info">
            <h2 class="messbox-sec-h2">Student's Information</h2>




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
                    <p class="textcont1 ">Section </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:DropDownList ID="ddl_section" runat="server" Style="width: 96%; height: 25px"></asp:DropDownList>


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
                        <asp:TextBox ID="txt_roll_no" runat="server" onkeypress="return isNumberKey(event)" Style="width: 96%;"></asp:TextBox>

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
                        <asp:DropDownList ID="ddl_house" runat="server" Style="width: 96%; height: 25px"></asp:DropDownList>


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
                    <p class="textcont1 ">Blood Group </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                         
                        <asp:DropDownList ID="ddl_blood_group" runat="server" Style="width: 96%; height: 25px;">
                            <asp:ListItem>NA</asp:ListItem>
                            <asp:ListItem>A+</asp:ListItem>
                            <asp:ListItem>A-</asp:ListItem>
                            <asp:ListItem>B+</asp:ListItem>
                            <asp:ListItem>B-</asp:ListItem>
                            <asp:ListItem>O+</asp:ListItem>
                            <asp:ListItem>O-</asp:ListItem>
                            <asp:ListItem>AB+</asp:ListItem>
                            <asp:ListItem>AB-</asp:ListItem>
                        </asp:DropDownList>
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
                            <asp:ListItem>MALE</asp:ListItem>
                            <asp:ListItem>FEMALE</asp:ListItem>
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
                        <asp:DropDownList ID="ddl_religion" runat="server" Style="width: 96%; height: 25px;">
                            <asp:ListItem>HINDU</asp:ListItem>
                            <asp:ListItem>ISLAM</asp:ListItem>
                            <asp:ListItem>SIKH</asp:ListItem>
                            <asp:ListItem>CHRISTIAN</asp:ListItem>
                            <asp:ListItem>BUDDHISM</asp:ListItem>
                            <asp:ListItem>JAIN</asp:ListItem>
                            <asp:ListItem>N/A</asp:ListItem>
                        </asp:DropDownList>

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
                         <asp:DropDownList ID="ddl_cast_category" runat="server" Style="width: 96%; height: 25px;">
                         </asp:DropDownList>



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
                       <asp:DropDownList ID="ddl_student_mother_tongue" runat="server" Style="width: 96%; height: 25px;"></asp:DropDownList>

                    </p>
                </div>
            </div>




            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Jati </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="lbl_castjati" runat="server" Style="width: 96%;"></asp:TextBox>



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
                              <asp:ListItem>NA</asp:ListItem>
                              <asp:ListItem>OTHERS</asp:ListItem>
                              <asp:ListItem>STATE GOVT. JOB</asp:ListItem>
                              <asp:ListItem>CENTRAL GOVT. JOB</asp:ListItem>
                              <asp:ListItem>PRIVATE JOB</asp:ListItem>
                              <asp:ListItem>BUSINESS</asp:ListItem>
                              <asp:ListItem>FARMER</asp:ListItem>
                              <asp:ListItem>PUBLIC SECTOR EMPLOYEE</asp:ListItem>
                              <asp:ListItem>ACCOUNTANT</asp:ListItem>
                              <asp:ListItem>ADVOCATE</asp:ListItem>
                              <asp:ListItem>AIR CRAFT ENG</asp:ListItem>
                              <asp:ListItem>ARMY</asp:ListItem>
                              <asp:ListItem>ASSISTAND PROPESSOR</asp:ListItem>
                              <asp:ListItem>ASSISTANT TEACHER</asp:ListItem>
                              <asp:ListItem>BANKING SERVICE</asp:ListItem>
                              <asp:ListItem>CENTRAL GOVT</asp:ListItem>
                              <asp:ListItem>DOCTOR</asp:ListItem>
                              <asp:ListItem>ENGINEER</asp:ListItem>
                              <asp:ListItem>EXPIRED</asp:ListItem>
                              <asp:ListItem>GOVT JOB</asp:ListItem>
                              <asp:ListItem>GOVT RETIRED</asp:ListItem>
                              <asp:ListItem>IT PROFESSIONAL</asp:ListItem>
                              <asp:ListItem>JOURNALIST</asp:ListItem>
                              <asp:ListItem>LAWYER</asp:ListItem>
                              <asp:ListItem>MANAGER</asp:ListItem>
                              <asp:ListItem>PRIVATE JOB</asp:ListItem>
                              <asp:ListItem>RETIRED</asp:ListItem>
                              <asp:ListItem>SENIOR AME</asp:ListItem>
                              <asp:ListItem>SERVICE</asp:ListItem>
                              <asp:ListItem>OTHER</asp:ListItem>
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
                          
                        <asp:DropDownList ID="ddl_father_qualification" runat="server" Style="width: 96%; height: 25px;"></asp:DropDownList>
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
                    <p class="textcont1 ">WhatsApp No.</p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:TextBox ID="txt_father_whatsapp_no" runat="server" Style="width: 96%;" onkeypress="return isNumberKey(event)"></asp:TextBox>

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

                                <asp:ListItem>OTHERS</asp:ListItem>
                                <asp:ListItem>STATE GOVT. JOB</asp:ListItem>
                                <asp:ListItem>CENTRAL GOVT. JOB</asp:ListItem>
                                <asp:ListItem>PRIVATE JOB</asp:ListItem>
                                <asp:ListItem>BUSINESS</asp:ListItem>
                                <asp:ListItem>FARMER</asp:ListItem>
                                <asp:ListItem>HOUSE WIFE</asp:ListItem>
                                <asp:ListItem>PUBLIC SECTOR EMPLOYEE</asp:ListItem>
                                <asp:ListItem>ADVOCATE</asp:ListItem>
                                <asp:ListItem>ASSISTANT PROFESSOR</asp:ListItem>
                                <asp:ListItem>ASST TEACHER</asp:ListItem>
                                <asp:ListItem>GOVT JOB</asp:ListItem>
                                <asp:ListItem>JOURNALIST</asp:ListItem>
                                <asp:ListItem>PENSION</asp:ListItem>
                                <asp:ListItem>SERVICE</asp:ListItem>

                                <asp:ListItem>HOUSE WIFE</asp:ListItem>
                                <asp:ListItem>OTHERS</asp:ListItem>
                                <asp:ListItem>NA</asp:ListItem>
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
                         <asp:DropDownList ID="ddl_mother_qualification" runat="server" Style="width: 96%; height: 25px;">
                         </asp:DropDownList>


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
                    <p class="textcont1 ">C/o </p>
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
                    <p class="textcont1 ">C/o </p>
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
