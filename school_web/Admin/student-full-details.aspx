<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="student-full-details.aspx.cs" Inherits="school_web.Admin.student_full_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Full Details
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
            margin: 4px 0px 4px 0px;
            padding: 3px 5px 3px 5px;
            float: left;
            font-size: 14px;
            line-height: 20px;
            color: #000;
            text-align: left;
        }

        .textcont3 {
            height: auto;
            width: 100%;
            margin: 4px 0px 4px 0px;
            padding: 3px 5px 3px 5px;
            float: left;
            font-size: 14px;
            line-height: 20px;
            color: #000;
            text-align: left;
            font-weight: 400;
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

        .sfulldet44 {
            height: auto;
            width: 100%;
            margin: 0px 0px 15px 0px;
            padding: 2px 0px 0px 0px;
            float: left;
            font-size: 18px;
            line-height: 25px;
            font-weight: 500;
            text-align: center;
            color: #222;
            text-align: left;
        }

        .courses-sec {
            height: auto;
            width: 100%;
            margin: 15px 0px 15px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
        }

        table, th, td {
            border: 1px solid #cacaca;
            border-collapse: collapse;
        }

        table, th {
            height: auto;
            margin: 0px 0px 0px 0px;
            padding: 5px 10px 5px 10px !important;
            font-size: 15px;
            line-height: 23px;
            color: #401f1f;
            text-align: left!important;
            font-weight: 400;
        }

        table, tr, td {
            height: auto;
            margin: 0px 0px 0px 0px;
            padding: 3px 10px 3px 10px!important;
            font-size: 15px;
            line-height: 23px;
            color: #333;
            text-align: left;
        }

            tr:hover {
                background-color: #f5f5f5;
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

        <div class="page-wrapper">
            <div class="page-content">

                <section id="main-content">
                    <div class="wrapper">

                        <div class="row">
                            <div class="col-lg-12 colmd-12 col-sm-12 col-xs-12">

                                <div class="main-card mb-3 card">
                                    <div class="card-body">
                                        <h2 class="sfulldet44">Student Full Details</h2>
                                        <h2 class="messbox-sec-h2" style="width: 100%;">Admission Information  </h2>
                                        <div class="texbox-border">
                                            <div class="row">


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Form Sl.No.</p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_formno" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Admission date  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_admissiondate" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>

                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Session </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_session" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Admission in </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_admissionin" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>


                                            <div class="clearfix"></div>
                                            <div class="row">

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Boarding Type </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_boarding_type" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Standard </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_course" runat="server" Text=""></asp:Label>
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
                         <asp:Label ID="lbl_section" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Admission no. </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_admissionno" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>



                                            <div class="clearfix"></div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Roll No.  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_rollno" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Transportation </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                         <asp:Label ID="lbl_transportation" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Categories  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_cat" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Sub Categories </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                         <asp:Label ID="lbl_sub_cat" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                            </div>

                                            <div class="clearfix"></div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">  </p>
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

                                                <asp:Image ID="img_studentimages" runat="server" Style="border: 1px solid #808080; height: 120px; width: 100px; padding: 2px;" />
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
                                                    <p class="textcont1 ">Birth Certificate no.  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                    <asp:Label ID="lbl_CertificateNostud" runat="server" Text=""></asp:Label>

                                                    </p>
                                                </div>


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Place of Birth  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_palceof_birth" runat="server" Text=""></asp:Label>
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
                         <asp:Label ID="lbl_gender" runat="server" Text=""></asp:Label>
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
                                                    <p class="textcont1 ">Caste  </p>
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
                                                    <p class="textcont1 ">Caste Certificate No.  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                          <asp:Label ID="lbl_certificateno" runat="server" Text=""></asp:Label>
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
                                                    <p class="textcont1 ">If any illness  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                          <asp:Label ID="lbl_anyillness" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">illness Remark </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                     <asp:Label ID="lbl_Illness_Remark" runat="server" Text=""></asp:Label>

                                                    </p>
                                                </div>
                                            </div>



                                            <div class="clearfix"></div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Previous School  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_prev_school" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Caste  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                 <asp:Label ID="lbl_cast" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>


                                            <div class="clearfix"></div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">RTE Student</p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_rte_student" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Staff Ward</p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_staff_ward" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Birth mark</p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_identy" runat="server" Text=""></asp:Label>
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
                           <asp:Label ID="lbl_fathername" runat="server" Text=""></asp:Label>
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
                                                <asp:Label ID="lbl_cunterycode1" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lbl_mobile_no" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>
                                            <div class="row ">

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Mail Id  </p>
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
                                                    <p class="textcont1 ">Parent’s Income  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                           <asp:Label ID="lbl_parent_income" runat="server" Text=""></asp:Label>
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
                                               <asp:Label ID="lbl_mother_name" runat="server" Text=""></asp:Label>
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
                                                    <p class="textcont1 ">Qualification</p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                     <asp:Label ID="lbl_motherqulification" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Nationality</p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                <asp:Label ID="lbl_mother_nationalty" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Marital Status </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                           <asp:Label ID="lbl_marital_status" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Mobile No.</p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                          <asp:Label ID="lbl_cunterycode2" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lbl_mobile_mother" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Mail Id</p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                             <asp:Label ID="lbl_emailid_mother" runat="server" Text=""></asp:Label>
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
                                                <asp:Label ID="lbl_cunterycode3" runat="server" Text=""></asp:Label> <asp:Label ID="lbl_mobile_no_current" runat="server" Text=""></asp:Label>
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
                                                <asp:Label ID="lbl_cunterycode4" runat="server" Text="+91"></asp:Label><asp:Label ID="lbl_mobile_no_permanent" runat="server" Text=""></asp:Label>
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
                                                    <p class="textcont1 ">Bank Name  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                           <asp:Label ID="lbl_bankname" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>
                                                <div class="row">

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Account No. </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                <asp:Label ID="lbl_accountno" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                       
                                             
                                                    </p>
                                                </div>

                                            </div>
                                             <div class="clearfix"></div>
                                            <div class="row">

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">IFSC Code </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                <asp:Label ID="lbl_IFSCCode" runat="server" Text=""></asp:Label>
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


                                        <h2 class="messbox-sec-h2" style="width: 100%;">Upload Documents Details </h2>
                                        <div class="clearfix"></div>

                                        <div class="clearfix"></div>
                                        <div class="row">
                                            <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                <p class="textcont1 ">DOB Approval</p>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                <p class="textcont3">
                                                    :
                                                 <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                </p>
                                            </div>
                                        </div>


                                        <%-- <div class="texbox-border"></div>--%>
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-6 col-xs-6">

                                                <div class="courses-sec">
                                                    <div class="table-responsive">
                                                        <table style="width: 100%;">
                                                            <tr style="background-color: #edeef1">
                                                                <th>Student's Image</th>
                                                                <th>Sign. of Parent</th>
                                                                 <th>Sign. of mother</th>
                                                                <th>Transfer slc</th>
                                                                <th>DOB Proof</th>
                                                                <th>Admission Form</th>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <a href="#!" runat="server" id="doc_studentimg"><i class="bx bxs-download"></i></a>
                                                                </td>
                                                                <td>
                                                                    <a href="#!" runat="server" id="doc_signaturee"><i class="bx bxs-download"></i></a>
                                                                </td>
                                                                <td>
                                                                    <a href="#!" runat="server" id="doc_mother"><i class="bx bxs-download"></i></a>
                                                                </td>
                                                                <td>
                                                                    <a href="#!" runat="server" id="doc_transfer"><i class="bx bxs-download"></i></a>
                                                                </td>
                                                                <td>
                                                                    <a href="#!" runat="server" id="doc_dobcert"><i class="bx bxs-download"></i></a>
                                                                </td>
                                                                <td>
                                                                    <a href="#!" runat="server" id="doc_admission"><i class="bx bxs-download"></i></a>
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
            </div>
        </div>
    </div>
</asp:Content>
