<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Offline_Registration_form_Print.aspx.cs" Inherits="school_web.Admin.slip.Offline_Registration_form_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Ofline_Reg_Css.css" rel="stylesheet" />
    <style>
        @media print {
            .noPrint {
                display: none;
            }
        }
    </style>
    <script type="text/javascript">
        function printit() {
            if (window.print) {
                window.print();
            }
        }
    </script>
    <script type="text/javascript">
        function PrintWindow() {
            window.print();
            // CheckWindowState();
        }

        function CheckWindowState() {
            if (document.readyState == "complete") {
                window.close();
            }
            else {
                setTimeout("CheckWindowState()", 2000)
            }
        }

        PrintWindow();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <div class="main_auto">
                <asp:Button ID="btn_back" runat="server" Text="Back" class="noPrint" OnClick="btn_back_Click"
                    Style="float: left;" />
                <asp:Button ID="btn_print" runat="server" Text="Printit" class="noPrint" OnClick="btn_print_Click1"
                    Style="float: right;" />
                <div class="main_auto_main">

                    <div style="margin: 0px; padding: 0px; float: left; height: 1500px;">


                        <div style="padding: 0px 0px 0px 0px; margin: 0px; height: 150px; width: 998px; float: left;">


                            <div class="top">
                                <div class="topcell_left">
                                    Affiliation No :
                        <asp:Label ID="lbl_affiliation_no" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="topcell_right">
                                    School No. :
                        <asp:Label ID="lbl_schoolno" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="heading">
                                <div class="leftlogoheading">
                                    <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px" />
                                </div>
                                <div class="righttextheading">
                                    <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 25px; text-decoration: underline;">
                                        <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                    </h1>

                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 16px; width: 100%;">
                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                    </div>
                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 15px; width: 100%;">
                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                    </div>
                                    <div runat="server" id="contact_no" visible="false" style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 15px; width: 100%;">
                                        Contact No. :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="slipno">
                                <div class="slipno_left" style="font-size: 20px;">
                                    <b>Form No :
                        <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true"></asp:Label></b>
                                </div>
                                <div class="slipno_middle" style="font-size: 20px;">
                                    <b> </b>
                                </div>
                                <div class="slipno_right" style="font-size: 20px;">
                                    <b>Date :
                        <asp:Label ID="lbl_date" runat="server" Font-Bold="true"></asp:Label></b>
                                </div>
                            </div>

                        </div>

                        <div class="studentdetails">
                             <div style="margin: 0px; padding: 0px; float: left;text-align:center; height: auto; width: 100%; margin: 7px 0px 0px 0px;">
                                <h2 class="online_frm-h" style="text-align:center">Student Registration Form-                        <asp:Label ID="lbl_session" runat="server" Font-Bold="true"></asp:Label> </h2>
                            </div>
                              <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Index No.</h2>
                                <asp:TextBox ID="txt_indexno" runat="server" class="form_control" style="height: 30px;" ReadOnly="true" Enabled="false"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">First Name</h2>
                                <asp:TextBox ID="txt_student_first_name" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Middle Name</h2>
                                <asp:TextBox ID="txt_student_middle_name" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Last Name</h2>
                                <asp:TextBox ID="txt_student_last_name" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>

                        <div class="studentdetails">
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Date of Birth</h2>
                                <asp:TextBox ID="TextBox1" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Gender</h2>
                                <asp:TextBox ID="TextBox2" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Nationality</h2>
                                <asp:TextBox ID="TextBox3" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>

                        <div class="studentdetails">
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Blood Group</h2>
                                <asp:TextBox ID="TextBox4" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Religion</h2>
                                <asp:TextBox ID="TextBox5" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Caste</h2>
                                <asp:TextBox ID="TextBox6" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>

                        <div class="studentdetails">
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Aadhar card no.</h2>
                                <asp:TextBox ID="TextBox7" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Identification marks</h2>
                                <asp:TextBox ID="TextBox8" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Class</h2>
                                <asp:TextBox ID="TextBox9" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>

                        <div class="studentdetails" style="display: none">
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Registration Fees</h2>
                                <asp:TextBox ID="TextBox10" runat="server" class="form_control"></asp:TextBox>

                            </div>


                        </div>


                        <div class="studentdetails">
                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; margin: 7px 0px 0px 0px;">
                                <h2 class="online_frm-h">Previous School Details</h2>
                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Name of last school attended</h2>
                                <asp:TextBox ID="TextBox11" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Board</h2>
                                <asp:TextBox ID="TextBox12" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Passout class</h2>
                                <asp:TextBox ID="TextBox13" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="studentdetails">
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Percentage %</h2>
                                <asp:TextBox ID="TextBox14" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Reason for shift</h2>
                                <asp:TextBox ID="TextBox15" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Year</h2>
                                <asp:TextBox ID="TextBox16" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="studentdetails">
                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; margin: 7px 0px 0px 0px;">
                                <h2 class="online_frm-h">Father’s Contact Details</h2>
                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">First Name</h2>
                                <asp:TextBox ID="TextBox17" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Middle Name</h2>
                                <asp:TextBox ID="TextBox18" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Last Name</h2>
                                <asp:TextBox ID="TextBox19" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>

                        <div class="studentdetails">
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Occupation</h2>
                                <asp:TextBox ID="TextBox20" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Qualification</h2>
                                <asp:TextBox ID="TextBox21" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Designation</h2>
                                <asp:TextBox ID="TextBox22" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>

                        <div class="studentdetails">
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Mobile No.</h2>
                                <asp:TextBox ID="TextBox23" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Email</h2>
                                <asp:TextBox ID="TextBox24" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Annual Income</h2>
                                <asp:TextBox ID="TextBox25" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>


                        <div class="studentdetails">
                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; margin: 7px 0px 0px 0px;">
                                <h2 class="online_frm-h">Mother’s Contact Details</h2>
                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">First Name</h2>
                                <asp:TextBox ID="TextBox26" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Middle Name</h2>
                                <asp:TextBox ID="TextBox27" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Last Name</h2>
                                <asp:TextBox ID="TextBox28" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>

                        <div class="studentdetails">
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Occupation</h2>
                                <asp:TextBox ID="TextBox29" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Qualification</h2>
                                <asp:TextBox ID="TextBox30" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Designation</h2>
                                <asp:TextBox ID="TextBox31" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>

                        <div class="studentdetails">
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Mobile No.</h2>
                                <asp:TextBox ID="TextBox32" runat="server" class="form_control"></asp:TextBox>

                            </div>

                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Email</h2>
                                <asp:TextBox ID="TextBox33" runat="server" class="form_control"></asp:TextBox>

                            </div>
                            <div class="online_frm_grp">

                                <h2 class="online_frm-grp-h">Annual Income</h2>
                                <asp:TextBox ID="TextBox34" runat="server" class="form_control"></asp:TextBox>

                            </div>
                        </div>
                    </div>







                    <div class="studentdetails">
                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; margin: 7px 0px 0px 0px;">
                            <h2 class="online_frm-h">Present Address Details</h2>
                        </div>
                        <div class="online_frm_grp" style="width: 100%;">

                            <h2 class="online_frm-grp-h">Present Address</h2>
                            <asp:TextBox ID="TextBox35" runat="server" class="form_control"></asp:TextBox>

                        </div>


                    </div>

                    <div class="studentdetails">
                        <div class="online_frm_grp">

                            <h2 class="online_frm-grp-h">Present P.O.</h2>
                            <asp:TextBox ID="TextBox36" runat="server" class="form_control"></asp:TextBox>

                        </div>

                        <div class="online_frm_grp">

                            <h2 class="online_frm-grp-h">Present District</h2>
                            <asp:TextBox ID="TextBox37" runat="server" class="form_control"></asp:TextBox>

                        </div>
                        <div class="online_frm_grp">

                            <h2 class="online_frm-grp-h">Present City</h2>
                            <asp:TextBox ID="TextBox38" runat="server" class="form_control"></asp:TextBox>

                        </div>
                    </div>


                    <div class="studentdetails">
                        <div class="online_frm_grp">

                            <h2 class="online_frm-grp-h">Present State</h2>
                            <asp:TextBox ID="TextBox39" runat="server" class="form_control"></asp:TextBox>

                        </div>

                        <div class="online_frm_grp">

                            <h2 class="online_frm-grp-h">Present Pin Code</h2>
                            <asp:TextBox ID="TextBox40" runat="server" class="form_control"></asp:TextBox>

                        </div>

                    </div>



                    <div class="studentdetails">
                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; margin: 7px 0px 0px 0px;">
                            <h2 class="online_frm-h">Permanent  Address Details</h2>
                        </div>
                        <div class="online_frm_grp" style="width: 100%;">

                            <h2 class="online_frm-grp-h">Permanent  Address</h2>
                            <asp:TextBox ID="TextBox41" runat="server" class="form_control"></asp:TextBox>

                        </div>


                    </div>

                    <div class="studentdetails">
                        <div class="online_frm_grp">

                            <h2 class="online_frm-grp-h">Permanent P.O.</h2>
                            <asp:TextBox ID="TextBox42" runat="server" class="form_control"></asp:TextBox>

                        </div>

                        <div class="online_frm_grp">

                            <h2 class="online_frm-grp-h">Permanent District</h2>
                            <asp:TextBox ID="TextBox43" runat="server" class="form_control"></asp:TextBox>

                        </div>
                        <div class="online_frm_grp">

                            <h2 class="online_frm-grp-h">Permanent City</h2>
                            <asp:TextBox ID="TextBox44" runat="server" class="form_control"></asp:TextBox>

                        </div>
                    </div>


                    <div class="studentdetails">
                        <div class="online_frm_grp">

                            <h2 class="online_frm-grp-h">Permanent State</h2>
                            <asp:TextBox ID="TextBox45" runat="server" class="form_control"></asp:TextBox>

                        </div>

                        <div class="online_frm_grp">

                            <h2 class="online_frm-grp-h">Permanent Pin Code</h2>
                            <asp:TextBox ID="TextBox46" runat="server" class="form_control"></asp:TextBox>

                        </div>

                    </div>


                    <div class="studentdetails">
                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; margin: 7px 0px 0px 0px;">
                            <h2 class="online_frm-h">Misc Details</h2>
                        </div>

                        <div class="online_frm_grp" style="width: 30%;">

                            <h2 class="online_frm-grp-h">Is the student handicapped(Yes/No)</h2>
                            <asp:TextBox ID="tx12" runat="server" class="form_control"></asp:TextBox>

                        </div>



                        <div class="online_frm_grp" style="width: 60%;">

                            <h2 class="online_frm-grp-h">Medical Remarks</h2>
                            <asp:TextBox ID="TextBox47" runat="server" class="form_control"></asp:TextBox>

                        </div>


                    </div>
                    <div class="studentdetails">

                        <div class="online_frm_grp" style="width: 100%;">

                            <h2 class="online_frm-grp-h">From where did you hear about the school?</h2>
                            <asp:TextBox ID="TextBox49" runat="server" class="form_control"></asp:TextBox>

                        </div>

                    </div>

                    <div class="studentdetails" style="margin: 28px 0px 0px 0px;">
                        <div class="online_frm_grp" style="text-align: center;">
                            <h2 class="online_frm-grp-h" style="text-align: center; margin: 2px 0px 0px 0px;">Student Photo</h2>
                            <asp:Image ID="img_student" runat="server" Style="border: 1px solid #000; height: 120px; width: 100px" />
                            <h2 class="online_frm-grp-h" style="text-align: center; margin: 42px 0px 0px 0px;">Student Signature</h2>


                        </div>
                        <div class="online_frm_grp" style="text-align: center;">
                            <h2 class="online_frm-grp-h" style="text-align: center; margin: 2px 0px 0px 0px;">Father's Photo</h2>
                            <asp:Image ID="Image1" runat="server" Style="border: 1px solid #000; height: 120px; width: 100px" />
                            <h2 class="online_frm-grp-h" style="text-align: center; margin: 42px 0px 0px 0px;">Father's Signature</h2>

                        </div>
                        <div class="online_frm_grp" style="text-align: center;">
                            <h2 class="online_frm-grp-h" style="text-align: center; margin: 2px 0px 0px 0px;">Mother's Photo</h2>
                            <asp:Image ID="Image2" runat="server" Style="border: 1px solid #000; height: 120px; width: 100px" />
                            <h2 class="online_frm-grp-h" style="text-align: center; margin: 42px 0px 0px 0px;">Mother's Signature</h2>

                        </div>
                    </div>
                    <div class="studentdetails">
                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; margin: 7px 0px 0px 0px;">
                            <h2 class="online_frm-h">Submit Document</h2>
                        </div>

                        <div class="online_frm_grp" style="width: 100%">
                            <h2 class="online_frm-grp-h" style="margin: 3px 0px 0px 7px;">(i)   Family photo(Father,Mother,Ward)</h2>
                            <h2 class="online_frm-grp-h" style="margin: 3px 0px 0px 7px;">(ii)  Birth Certificate of the child</h2>
                            <h2 class="online_frm-grp-h" style="margin: 3px 0px 0px 7px;">(iii) Residential Certificate</h2>
                        </div>





                        <div class="online_frm_grp" style="width: 50%">

                            <div class="online_frm_grp" style="text-align: center;">

                                <h2 class="online_frm-grp-h" style="text-align: center; margin:90px 0px 0px 0px;border-top: 2px solid;">Accountant Signature  </h2>

                            </div>
                        </div>
                        <div class="online_frm_grp" style="width: 40%">

                            <div class="online_frm_grp" style="text-align: center;">

                                <h2 class="online_frm-grp-h" style="text-align: center; margin: 90px 0px 0px 0px;border-top: 2px solid;">Seal</h2>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
