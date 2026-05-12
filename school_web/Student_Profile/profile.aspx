<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="school_web.Student_Profile.profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .messbox-sec-h2 {
            width: 100%;
            margin: 0px 0px 5px 0px;
            padding: 2px 0px 2px 7px;
            float: left;
            font-size: 18px;
            line-height: 25px;
            font-weight: 500;
            color: #fff !important;
            background-color: #109be1;
            text-align: left;
            border-radius: 2px;
        }

        .texbox-border {
            margin: 0px;
            padding: 3px 5px 3px 5px;
            width: 50%;
            border-bottom: 1px solid rgb(0 0 0 / 22%);
            float: left;
        }

        .textcont1 {
            height: auto;
            width: 160px;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 13px;
            line-height: 20px;
            color: #000;
            text-align: left;
        }

        .textcont3 {
            width: 69%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 13px;
            line-height: 20px;
            color: #000;
            text-align: left;
            font-weight: bold;
        }

        .admission_info {
            width: 100%;
            margin: 10px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            border: 1px solid #ddd;
            border-radius: 3px;
        }

        @media only screen and (max-width:900px) {
            .texbox-border {
                width: 100%;
            }

            .textcont1 {
                width: 134px;
            }

            .textcont3 {
                width: 54%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div style="float: left; width: 235px; height: auto; background: #f00; padding: 4px; border-radius: 4px; color: #fff; position: absolute; right: 0px;">
                        <asp:Label ID="lblmessage" runat="server" class="notif-txt"></asp:Label>
                    </div>

                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>



            <div class="headingtablee">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="main-card mb-3 card">
                            <div class="card-header">
                                <h4 class="card-title">Profile</h4>
                            </div>
                            <div class="card-body" style="padding-top: 0px;">

                                <div class="admission_info">
                                    <h2 class="messbox-sec-h2">Admission Information
                <asp:LinkButton ID="lnk_edit" Style="color: #fff; float: right" runat="server" OnClick="lnk_edit_Click">Edit&nbsp;&nbsp;<i class="fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton></h2>



                                    <div class="texbox-border">
                                        <p class="textcont1 ">Admission Date  :</p>
                                        <asp:Label ID="lbladmissiondate" class="textcont3" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="texbox-border">
                                        <p class="textcont1 ">Session  :</p>
                                        <asp:Label ID="lbl_session" class="textcont3" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="texbox-border">
                                        <p class="textcont1 ">Admission In : </p>
                                        <asp:Label ID="lbl_admissionin" class="textcont3" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="texbox-border">
                                        <p class="textcont1 ">Class : </p>
                                        <asp:Label ID="lbl_class" runat="server" Text="" class="textcont3"></asp:Label>
                                    </div>



                                    <div class="texbox-border">
                                        <p class="textcont1 ">Section : </p>
                                        <asp:Label ID="lbl_Section" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Admission No. :</p>
                                        <asp:Label ID="lbl_admission_no" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Roll No. :</p>
                                        <asp:Label ID="lbl_rollno" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">House :</p>
                                        <asp:Label ID="lbl_house" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border" style="border-bottom: 0px">
                                        <p class="textcont1 ">Transportation : </p>
                                        <asp:Label ID="lbl_transprtaion" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="admission_info">
                                    <h2 class="messbox-sec-h2">Student's Information</h2>
                                    <div class="texbox-border" style="width: 100%; text-align: center">
                                        <asp:Image ID="Image1" runat="server" Style="border: 1px solid #808080; height: 120px; width: 100px; padding: 2px;" />
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Student Name :</p>
                                        <asp:Label ID="lbl_student_name" runat="server" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Date of Birth :</p>
                                        <asp:Label ID="lbl_dateofbirth" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Place of Birth : </p>
                                        <asp:Label ID="lbl_palceof_birth" class="textcont3" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Certificate No. :</p>
                                        <asp:Label ID="lbl_CertificateNo" runat="server" Text=""></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Blood Group :</p>
                                        <asp:Label ID="lbl_bloodgroup" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Aadhar No. : </p>
                                        <asp:Label ID="lbl_aadharno" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Gender :</p>
                                        <asp:Label ID="lblgender" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Religion :</p>
                                        <asp:Label ID="lbl_religion" class="textcont3" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Ration Type  : </p>
                                        <asp:Label ID="lbl_ration_type" class="textcont3" runat="server" Text=""></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Category :</p>
                                        <asp:Label ID="lbl_catogery" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Certificate No. :</p>
                                        <asp:Label ID="lbl_certificate" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Mother Tongue : </p>
                                        <asp:Label ID="lbl_mother_tongue" class="textcont3" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Is any Illness : </p>
                                        <asp:Label ID="lbl_anyillness" class="textcont3" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Prev. School : </p>
                                        <asp:Label ID="lbl_prevschool" class="textcont3" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border" style="width: 100%; border-bottom: 0px">
                                        <p class="textcont1 " style="width: 162px">Cast :</p>
                                        <asp:Label ID="lbl_cast" runat="server" Text="" class="textcont3"></asp:Label>
                                    </div>
                                </div>


                                <div class="admission_info">
                                    <h2 class="messbox-sec-h2">Father's Information</h2>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Father's Name :</p>
                                        <asp:Label ID="lblfathername" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Occupation :</p>
                                        <asp:Label ID="lbl_occupation" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Qualification :</p>
                                        <asp:Label ID="lbl_qulification" runat="server" Text="" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Nationality :</p>
                                        <asp:Label ID="lbl_Nationality" runat="server" Text="" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Marital Status : </p>
                                        <asp:Label ID="lbl_martialstatus" runat="server" class="textcont3" Text=""></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Mobile No. :</p>
                                        <asp:Label ID="lbl_mobile_no" runat="server" Text="" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Email Id :</p>
                                        <asp:Label ID="lbl_emailid" runat="server" Text="" class="textcont3"></asp:Label>
                                    </div>



                                    <div class="texbox-border">
                                        <p class="textcont1 ">Guardian's Name :</p>
                                        <asp:Label ID="lbl_guardianname" runat="server" Tclass="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border" style="border-bottom: 0px">
                                        <p class="textcont1 ">Parent Income</p>
                                        <asp:Label ID="lbl_parent_income" runat="server" class="textcont3"></asp:Label>
                                    </div>
                                </div>



                                <div class="admission_info">
                                    <h2 class="messbox-sec-h2">Mother's Information</h2>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Mother's Name :</p>
                                        <asp:Label ID="lbl_mother" runat="server" class="textcont3"></asp:Label>
                                    </div>
                                    <div class="texbox-border">
                                        <p class="textcont1 ">Occupation :</p>
                                        <asp:Label ID="lbl_occupation_mother" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Qualification  :</p>
                                        <asp:Label ID="lbl_motherqulification" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Mobile No. :</p>
                                        <asp:Label ID="lbl_mobileno_mother" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border" style="border-bottom: 0px">
                                        <p class="textcont1 ">Email Id : </p>
                                        <asp:Label ID="lbl_emailcode" runat="server" class="textcont3"></asp:Label>
                                    </div>
                                </div>



                                <div class="admission_info">
                                    <h2 class="messbox-sec-h2">Current Address </h2>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">C/o </p>
                                        <asp:Label ID="lbl_current" runat="server" class="textcont3"></asp:Label>
                                    </div>
                                    <div class="texbox-border">
                                        <p class="textcont1 ">Mobile No. :</p>
                                        <asp:Label ID="lbl_mobile_no_current" runat="server" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">City/Village :</p>
                                        <asp:Label ID="lbl_cityvillage_current" runat="server" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">District :</p>
                                        <asp:Label ID="lbl_distict_current" runat="server" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">P. O. :</p>
                                        <asp:Label ID="lbl_po_current" runat="server" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">P. S. :</p>
                                        <asp:Label ID="lbl_ps_current" runat="server" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">state :</p>
                                        <asp:Label ID="lbl_state_current" runat="server" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Pin Code :</p>
                                        <asp:Label ID="lbl_pincode" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>



                                <div class="admission_info">
                                    <h2 class="messbox-sec-h2">Permanent Address :</h2>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">C/o </p>
                                        <asp:Label ID="lbl_permanent_address" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Mobile No. :</p>
                                        <asp:Label ID="lbl_mobile_no_permanent" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">City/Village :</p>
                                        <asp:Label ID="lbl_cityvillage_permanent" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">District :</p>
                                        <asp:Label ID="lbl_distict_permanent" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">P. O. :</p>
                                        <asp:Label ID="lbl_po_permanent" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">P. S. : </p>
                                        <asp:Label ID="lbl_ps_permanent" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">state :</p>
                                        <asp:Label ID="lbl_state_permanent" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Pin Code : </p>
                                        <asp:Label ID="lbl_pincode_permanent" runat="server" class="textcont3"></asp:Label>
                                    </div>
                                </div>



                                <div class="admission_info">
                                    <h2 class="messbox-sec-h2">Bank Details </h2>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">Account Holder Name :</p>
                                        <asp:Label ID="lbl_account_holder_name" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Account No. :</p>
                                        <asp:Label ID="lbl_account_name" runat="server" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border">
                                        <p class="textcont1 ">IFSC Code : </p>
                                        <asp:Label ID="lbl_IFSCCode" runat="server" class="textcont3"></asp:Label>
                                    </div>


                                    <div class="texbox-border">
                                        <p class="textcont1 ">Bank Name :</p>
                                        <asp:Label ID="lbl_bankname" runat="server" class="textcont3"></asp:Label>
                                    </div>

                                    <div class="texbox-border" style="border-bottom: 0px">
                                        <p class="textcont1 ">Branch Name : </p>
                                        <asp:Label ID="lbl_branch_name" runat="server" class="textcont3"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
