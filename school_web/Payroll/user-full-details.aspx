<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="user-full-details.aspx.cs" Inherits="school_web.Payroll.user_full_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    User Full Details
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
                                        <h2 class="sfulldet44">User Full Details</h2>
                                        <h2 class="messbox-sec-h2" style="width: 100%;">Personal Information</h2>
                                        <div class="texbox-border">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: center"> 
                                                <asp:Image ID="img_studentimages" runat="server" Style="border: 1px solid #808080; height: 120px; width: 100px; padding: 2px;" />
                                            </div>
                                        </div>
                                        <div class="texbox-border">
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">User Type</p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_emp_type" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">User Name  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_emp_name" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>

                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Gender </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_gender" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Date of Birth </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_dob" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>


                                            <div class="clearfix"></div>
                                            <div class="row">

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Blood Group </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_blood_group" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Religion </p>
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
                                                    <p class="textcont1 ">Marital Status </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                         <asp:Label ID="lbl_merital_status" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Father's/Husband Name no. </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_father_name" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>



                                            <div class="clearfix"></div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">PAN  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_pan" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Address </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                         <asp:Label ID="lbl_address" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                            </div>


                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">City  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_city" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Pincode </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                         <asp:Label ID="lbl_pincode" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">State  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_state" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Email </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                         <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Mobile No.  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_mobile_no" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Bank Name </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                         <asp:Label ID="lbl_bank_name" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Branch  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_branch" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Account No. </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                         <asp:Label ID="lbl_account_no" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">IFSC  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_ifsc_code" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">MICR </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                         <asp:Label ID="lbl_micr" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>


                                        <h2 class="messbox-sec-h2" style="width: 100%;">Organization Details </h2>
                                        <div class="clearfix"></div>


                                        <div class="clearfix"></div>
                                        <div class="texbox-border">
                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Employee Code   </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                        <asp:Label ID="lbl_emp_code" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Punch Card No  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                       <asp:Label ID="lbl_punch_card" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="row">

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Official Email Id  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                    <asp:Label ID="lbl_official_email" runat="server" Text=""></asp:Label>

                                                    </p>
                                                </div>


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Qualification  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                                                   <asp:Label ID="lbl_qualification" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                            </div>

                                            <div class="clearfix"></div>
                                            <div class="row">


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Grade</p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                         <asp:Label ID="lbl_grade" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Department  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                     <asp:Label ID="lbl_department" runat="server" Text=""></asp:Label>

                                                    </p>
                                                </div>
                                            </div>


                                            <div class="clearfix"></div>
                                            <div class="row">


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Designation  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                         <asp:Label ID="lbl_designation" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">E.P.F. No.  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                     <asp:Label ID="lbl_epf_no" runat="server" Text=""></asp:Label>

                                                    </p>
                                                </div>
                                            </div>


                                            <div class="clearfix"></div>
                                            <div class="row">


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Join Date  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                         <asp:Label ID="lbl_join_date" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">PF leaving Date  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                     <asp:Label ID="lbl_pf_leaving_date" runat="server" Text=""></asp:Label>

                                                    </p>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>
                                            <div class="row">


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Reason </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                          <asp:Label ID="lbl_reson" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">ESIC No.  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                     <asp:Label ID="lbl_esic_no" runat="server" Text=""></asp:Label>

                                                    </p>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>
                                            <div class="row">


                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Join Date  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                          <asp:Label ID="lbl_join_date1" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">ESIC Leaving Date </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                     <asp:Label ID="lbl_esci_leaving_date" runat="server" Text=""></asp:Label>

                                                    </p>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Reason  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                          <asp:Label ID="lbl_reson1" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                                    <p class="textcont1 ">Date of Joining  </p>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <p class="textcont3">
                                                        :
                     <asp:Label ID="lbl_doj1" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>






                                        <asp:Panel ID="pnl_docs" runat="server" Visible="false">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Documents Details </h2>
                                            <div class="clearfix"></div>


                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-6 col-xs-6">

                                                    <div class="courses-sec">
                                                        <div class="table-responsive">
                                                            <table style="width: 100%;">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Document Name</th>
                                                                        <th>Document</th>
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
                                                                                    <asp:Label ID="lbl_Document_Name" runat="server" Text='<%#Bind("Document_Name")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <a href="<%#Eval("Document") %>">Download</a>
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
                                        </asp:Panel>
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
