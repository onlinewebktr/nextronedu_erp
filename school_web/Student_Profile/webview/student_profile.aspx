<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student_profile.aspx.cs" Inherits="school_web.Student_Profile.webview.student_profile" %>

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
    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <%--<script src="../../assets/js/bootstrap.min.js"></script>--%>
  <%--  <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />--%>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <link href="../../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
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
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="admission_info">
            <h2 class="messbox-sec-h2" style="width: 100%;">Admission Information
                <asp:LinkButton ID="lnk_edit" Style="color: #fff; float: right" runat="server" OnClick="lnk_edit_Click">Edit&nbsp;&nbsp;<i class="fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton></h2>


            <%-- <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Form Sl. No.  </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_formslno" runat="server" Text=""></asp:Label>
                    </p>
                </div>

            </div>--%>
            <%--<div class="clearfix"></div>--%>
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
            <div class="texbox-border">
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
                        <asp:Label ID="lbl_student_name" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_dateofbirth" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_palceof_birth" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_CertificateNo" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_bloodgroup" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_aadharno" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lblgender" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_religion" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_ration_type" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_catogery" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_certificate" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_mother_tongue" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_anyillness" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_prevschool" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_cast" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lblfathername" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_occupation" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_qulification" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_Nationality" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_martialstatus" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_mobile_no" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_emailid" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_guardianname" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_parent_income" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_mother" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_occupation_mother" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_motherqulification" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_mobileno_mother" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_emailcode" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_current" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_mobile_no_current" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_cityvillage_current" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_distict_current" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_po_current" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_ps_current" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_state_current" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_pincode" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_permanent_address" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_mobile_no_permanent" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_cityvillage_permanent" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_distict_permanent" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_po_permanent" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_ps_permanent" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_state_permanent" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_pincode_permanent" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>








        </div>



        <div class="Current">
            <h2 class="messbox-sec-h2">Bank Details </h2>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Account Holder Name </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_account_holder_name" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="texbox-border">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                    <p class="textcont1 ">Account No. </p>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                    <p class="textcont3">
                        :
                        <asp:Label ID="lbl_account_name" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_IFSCCode" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_bankname" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="lbl_branch_name" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>



        </div>





    </form>
</body>
</html>
