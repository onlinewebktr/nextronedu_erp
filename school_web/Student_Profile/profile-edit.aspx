<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="profile-edit.aspx.cs" Inherits="school_web.Student_Profile.profile_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Edit Profile
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
            width: 100%;
            border-bottom: 1px solid rgb(0 0 0 / 22%);
            float: left;
        }

        .textcont1 {
            height: auto;
            width: 180px;
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


            <div class="main-card mb-3 card">
                <div class="card-header">
                    <h4 class="card-title">Edit Profile</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">


                    <div class="admission_info">
                        <h2 class="messbox-sec-h2">Admission Information</h2>
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

                    <div class="admission_info">
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


                    <div class="admission_info">
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

                    <div class="admission_info">
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

                    <div class="admission_info">
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


                    <div class="admission_info">
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



                    <div class="admission_info">
                        <h2 class="messbox-sec-h2">Bank Details </h2>
                        <div class="clearfix"></div>
                        <div class="texbox-border">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                <p class="textcont1 ">Account Holder Name </p>
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

                    <div class="admission_info">
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
                </div>
            </div>
        </div>
        <!--end row-->
    </div>

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>

    <script>
        $(function () {
            $("#<%=lbl_dateofbirth1.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>

    <link href="assets/css/calender-modified.css" rel="stylesheet" />
</asp:Content>
