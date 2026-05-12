<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Student_Full_Details.aspx.cs" Inherits="school_web.LMS_VC_Admin.Student_Full_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Student Full Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .messbox-sec-h2 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 2px 0px 2px 7px;
            float: left;
            font-size: 18px;
            line-height: 25px;
            font-weight: 500;
            text-align: center;
            color: #fff;
            background-color: #109be1;
            text-align: left;
        }

        .messbox-sec-h2_right {
            height: auto;
            width: 40%;
            margin: 0px 0px 0px 0px;
            padding: 2px 7px 2px 0px;
            float: left;
            font-size: 18px;
            line-height: 25px;
            font-weight: 500;
            text-align: center;
            color: #fff;
            background-color: #109be1;
            text-align: right;
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
            font-size: 16px;
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
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="row">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 235px; height: auto;">
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </div>
                    <asp:ImageButton ID="ImageButton1" ImageUrl="images/close.png" runat="server" OnClientClick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                        class="closenotificationpan" Style="background: none" />
                </div>
            </div>
        </div>
        <!--main content start-->
        <section id="main-content">
            <div class="wrapper">
                <div class="row">
                    <div class="col-lg-12 colmd-12 col-sm-12 col-xs-12">
                        <div class="main-card mb-3 card">
                            <div class="card-body">
                                <h2 class="messbox-sec-h2" style="width: 100%;">Admission Information  </h2>
                                <div class="texbox-border">
                                    <div class="row">


                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Admission Date  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                        <asp:Label ID="lbladmissiondate" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Session  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                        <asp:Label ID="lbl_session" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>

                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Admission In </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                        <asp:Label ID="lbl_admissionin" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Class </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                        <asp:Label ID="lbl_class" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>


                                    <div class="clearfix"></div>
                                    <div class="row">

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Section </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                        <asp:Label ID="lbl_Section" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Admission No. </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                        <asp:Label ID="lbl_admission_no" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>


                                    <div class="row">

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Roll No. </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                         <asp:Label ID="lbl_rollno" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">House </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                        <asp:Label ID="lbl_house" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>



                                    <div class="clearfix"></div>


                                    <div class="row">

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Transportation </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                         <asp:Label ID="lbl_transprtaion" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 "></p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                            </p>
                                        </div>
                                    </div>





                                </div>


                                <h2 class="messbox-sec-h2" style="width: 100%;">Student's Information  </h2>
                                <div class="clearfix"></div>
                                <div class="texbox-border">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: center">
                                        <asp:Image ID="Image1" runat="server" Style="border: 1px solid #808080; height: 120px; width: 100px; padding: 2px;" />



                                    </div>

                                </div>
                                <div class="clearfix"></div>
                                <div class="texbox-border">
                                    <div class="row">


                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Student Name  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                        <asp:Label ID="lbl_student_name" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Date of Birth  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                       <asp:Label ID="lbl_dateofbirth" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">


                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Place of Birth  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                        <asp:Label ID="lbl_palceof_birth" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Certificate No.  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                     <asp:Label ID="lbl_CertificateNo" runat="server" Text=""></asp:Label>

                                            </p>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="row">


                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Blood Group  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                         <asp:Label ID="lbl_bloodgroup" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Aadhar No.  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                     <asp:Label ID="lbl_aadharno" runat="server" Text=""></asp:Label>

                                            </p>
                                        </div>
                                    </div>


                                    <div class="clearfix"></div>
                                    <div class="row">


                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Gender  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                         <asp:Label ID="lblgender" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Religion  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                     <asp:Label ID="lbl_religion" runat="server" Text=""></asp:Label>

                                            </p>
                                        </div>
                                    </div>


                                    <div class="clearfix"></div>
                                    <div class="row">


                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Ration Type  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                         <asp:Label ID="lbl_ration_type" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Category  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                     <asp:Label ID="lbl_catogery" runat="server" Text=""></asp:Label>

                                            </p>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="row">


                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Certificate No.  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                          <asp:Label ID="lbl_certificate" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Mother Tongue  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                     <asp:Label ID="lbl_mother_tongue" runat="server" Text=""></asp:Label>

                                            </p>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="row">


                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Is any Illness  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                          <asp:Label ID="lbl_anyillness" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Prev. School  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                     <asp:Label ID="lbl_prevschool" runat="server" Text=""></asp:Label>

                                            </p>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">


                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Cast  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_cast" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 "></p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <h2 class="messbox-sec-h2" style="width: 100%;">Father's Information </h2>
                                <div class="clearfix"></div>

                                <div class="texbox-border">
                                    <div class="row">

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Father's Name  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lblfathername" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Occupation</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_occupation" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Qualification  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_qulification" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Nationality</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_Nationality" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="row ">

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Marital Status  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_martialstatus" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Mobile No.</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_mobile_no" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="row ">

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Email Id  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                            <asp:Label ID="lbl_emailid" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Guardian's Name</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                              <asp:Label ID="lbl_guardianname" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="row ">

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Parent Income  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_parent_income" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Guardian's Name</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                              <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <h2 class="messbox-sec-h2" style="width: 100%;">Mother's Information </h2>
                                <div class="clearfix"></div>
                                <div class="texbox-border">
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Mother's Name  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_mother" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Occupation</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_occupation_mother" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Qualification  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_motherqulification" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Mobile No.</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_mobileno_mother" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Email Id  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_emailcode" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                            </p>
                                        </div>

                                    </div>
                                </div>

                                <h2 class="messbox-sec-h2" style="width: 100%;">Current Address </h2>
                                <div class="clearfix"></div>
                                <div class="texbox-border">
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">C/o   </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_current" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Mobile No.</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_mobile_no_current" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">City/Village   </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_cityvillage_current" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">District</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_distict_current" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">P. O.   </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_po_current" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">P. S.</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_ps_current" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">State   </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_state_current" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Pin Code</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_pincode" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>






                                </div>
                                <h2 class="messbox-sec-h2" style="width: 100%;">Permanent Address </h2>
                                <div class="clearfix"></div>
                                <div class="texbox-border">
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">C/o   </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_permanent_address" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Mobile No.</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_mobile_no_permanent" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">City/Village   </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_cityvillage_permanent" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">District</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_distict_permanent" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>


                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">P. O.  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_po_permanent" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">P. S.</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_ps_permanent" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">State  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_state_permanent" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Pin Code</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_pincode_permanent" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>


                                </div>
                                <h2 class="messbox-sec-h2" style="width: 100%;">Bank Details </h2>
                                <div class="clearfix"></div>
                                <div class="texbox-border">
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Account Holder Name  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_account_holder_name" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">IFSC Code </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_IFSCCode" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                    </div>


                                    <div class="clearfix"></div>

                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Bank Name  </p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                           <asp:Label ID="lbl_bankname" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                            <p class="textcont1 ">Branch Name</p>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                            <p class="textcont3">
                                                :
                                                <asp:Label ID="lbl_branch_name" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
