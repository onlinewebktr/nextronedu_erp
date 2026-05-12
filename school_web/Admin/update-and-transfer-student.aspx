<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="update-and-transfer-student.aspx.cs" Inherits="school_web.Admin.update_and_transfer_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Update & Transfer Student
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-ttleS {
            margin: 0px 0px 0px 0px;
            padding: 8px 10px 5px 10px;
            width: 100%;
            float: left;
            font-size: 18px;
            color: #0296bd;
            border-bottom: 1px solid #ddd;
        }

        .form-label {
            margin-bottom: 2px;
        }

        th {
            font-weight: 500;
        }

        .form-control:disabled, .form-control[readonly] {
            background-color: #ffffff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
    <div class="page-wrapper">
        <div class="page-content">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-white">Success Alerts</h6>
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-dark">Warning Alerts</h6>
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                </div>
            </div>

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Admission</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student Registration</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Student Registration"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Admission Information</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">Student Type</label>
                                        <asp:DropDownList ID="ddl_student_type" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>New</asp:ListItem>
                                            <asp:ListItem>Old</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Categories<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_category" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_category_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Sub Categories<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_subcategory" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Form Sl.No.</label>
                                        <asp:TextBox ID="txt_form_slno" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Admission date<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_admission_date"></asp:RequiredFieldValidator></sup></label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_admission_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">UID No.</label>
                                        <div class="clndr-div">

                                            <asp:TextBox ID="txt_uid" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>





                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Session <sup>*</sup></label>
                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">Admission in</label>
                                        <asp:RadioButton ID="rdb_hostel" runat="server" Text="Hostel" Style="margin: 0px 10px 0px 0px;" GroupName="a2" />
                                        <asp:RadioButton ID="rdb_dayscholar" runat="server" Text="Day Scholar" GroupName="a2" />
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Boarding Type</label>
                                        <asp:DropDownList ID="ddl_day_boarding" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem Value="0">Day Scholar</asp:ListItem>
                                            <asp:ListItem Value="1">Day Boarding</asp:ListItem>
                                            <asp:ListItem Value="2">Day Boarding with Lunch</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Standard</label>
                                        <asp:DropDownList ID="ddlclass" class="form-select find-dv-txtbx" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Section *</label>

                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Admission no.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Roll No.  <sup>*</sup></label>
                                        <asp:TextBox ID="txt_rollnumber" runat="server" class="form-control find-dv-txtbx" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3" style="display: none">
                                        <label for="validationCustom01" class="form-label">House</label>
                                        <asp:DropDownList ID="ddl_house" class="form-select find-dv-txtbx" runat="server"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3" style="display: none">
                                        <label for="validationCustom01" class="form-label">Academic</label>
                                        <asp:Label ID="lbl_actype" runat="server" Text="Year"></asp:Label>
                                        <asp:DropDownList ID="ddl_academic_ses_type" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%">Transportation</label>
                                        <asp:RadioButton ID="rdotransyes" runat="server" Text="Yes" OnCheckedChanged="rdotransyes_CheckedChanged" AutoPostBack="true" GroupName="a3" Style="margin: 0px 10px 0px 0px;" />
                                        <asp:RadioButton ID="rdotransno" runat="server" Text="No" OnCheckedChanged="rdotransno_CheckedChanged" AutoPostBack="true" GroupName="a3" />
                                    </div>
                                    <div class="col-md-3" id="transportpath" runat="server" visible="false" style="position: relative">
                                        <label for="validationCustom01" class="form-label" style="width: 100%">Transportation Path</label>
                                        <asp:DropDownList ID="ddl_path_root" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>




                            <%-- =============== --%>
                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 5px 0px 0px 0px;">
                                <h2 class="form-ttleS">Student's Information</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">


                                    <asp:TextBox ID="txt_student_name" runat="server" class="form-control  find-dv-txtbx" Visible="false"></asp:TextBox>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">First Name</label>
                                        <asp:TextBox ID="txt_firstname" runat="server" class="form-control  find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Middle Name </label>
                                        <asp:TextBox ID="txt_middlename" runat="server" class="form-control  find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Last Name</label>
                                        <asp:TextBox ID="txt_lastname" runat="server" class="form-control  find-dv-txtbx"></asp:TextBox>
                                    </div>


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Date of Birth</label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_dob" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Birth Certificate no.</label>
                                        <asp:TextBox ID="txt_birth_certificate_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Place of Birth</label>
                                        <asp:TextBox ID="txt_place_of_birth" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Blood Group</label>
                                        <asp:DropDownList ID="ddl_blood_group" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>A+</asp:ListItem>
                                            <asp:ListItem>A-</asp:ListItem>
                                            <asp:ListItem>B+</asp:ListItem>
                                            <asp:ListItem>B-</asp:ListItem>
                                            <asp:ListItem>O+</asp:ListItem>
                                            <asp:ListItem>O-</asp:ListItem>
                                            <asp:ListItem>AB+</asp:ListItem>
                                            <asp:ListItem>AB-</asp:ListItem>
                                            <asp:ListItem>N/A</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>




                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Aadhar No.</label>
                                        <asp:TextBox ID="txt_aadharno_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Gender</label>
                                        <asp:DropDownList ID="ddl_gender" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                            <asp:ListItem>Transgender</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Religion</label>
                                        <asp:DropDownList ID="ddl_religion" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Hindu</asp:ListItem>
                                            <asp:ListItem>Islam</asp:ListItem>
                                            <asp:ListItem>Sikh</asp:ListItem>
                                            <asp:ListItem>Christian</asp:ListItem>
                                            <asp:ListItem>Buddhism</asp:ListItem>
                                            <asp:ListItem>Jain</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Ration Type</label>
                                        <asp:DropDownList ID="ddl_ration_cards_types" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>APL</asp:ListItem>
                                            <asp:ListItem>BPL</asp:ListItem>
                                            <asp:ListItem>N/A</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Caste</label>
                                        <asp:DropDownList ID="ddl_cast" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>General</asp:ListItem>
                                            <asp:ListItem>OBC</asp:ListItem>
                                            <asp:ListItem>OBC-A</asp:ListItem>
                                            <asp:ListItem>OBC-B</asp:ListItem>
                                            <asp:ListItem>ST</asp:ListItem>
                                            <asp:ListItem>SC</asp:ListItem>
                                            <asp:ListItem>EBC</asp:ListItem>
                                            <asp:ListItem>Others</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Caste Certificate No.</label>
                                        <asp:TextBox ID="txt_cast_certificate_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mother Tongue</label>
                                        <asp:DropDownList ID="ddl_student_mother_tongue" runat="server" class="form-select find-dv-txtbx">
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
                                    </div>


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">If any illness</label>
                                        <asp:DropDownList ID="ddl_illness" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_illness_SelectedIndexChanged">
                                            <asp:ListItem>NO</asp:ListItem>
                                            <asp:ListItem>YES</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Illness Remark</label>
                                        <asp:TextBox ID="txt_illness_remark" runat="server" class="form-control find-dv-txtbx" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Previous School</label>
                                        <asp:TextBox ID="txt_school_current" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Caste</label>
                                        <asp:TextBox ID="txt_jati" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">RTE Student</label>
                                        <asp:DropDownList ID="ddl_rte" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Staff Ward</label>
                                        <asp:DropDownList ID="ddl_staff_ward" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Birth mark</label>
                                        <asp:TextBox ID="txt_persnal_identfication_marks" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <%-- =============== --%>
                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 5px 0px 0px 0px; display: none">
                                <h2 class="form-ttleS">Academic Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-12">
                                        <table class="table-bordered">
                                            <tr>
                                                <th>Academic Record</th>
                                                <th>University/Board</th>
                                                <th>Year Of Passing</th>
                                                <th>Total Marks Obtained</th>
                                                <th>Percentage Of Marks</th>
                                                <th>Division</th>
                                                <th>Subjects</th>
                                                <th>Document</th>
                                            </tr>
                                            <tr>
                                                <td>10th</td>
                                                <td>
                                                    <asp:TextBox ID="txt_tenth_board" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_tenth_passing_year" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_tenth_total_obtai_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_tenth_percentage_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_tenth_devision" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_tenth_subject" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:FileUpload ID="tenthDoc" runat="server" class="form-control find-dv-txtbx" /></td>
                                            </tr>
                                            <tr>
                                                <td>10+2</td>
                                                <td>
                                                    <asp:TextBox ID="txt_twelve_board" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_twelve_passing_year" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_twelve_total_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_twelve_percentage_of_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_twelve_division" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_twelve_subjects" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:FileUpload ID="ten_plus_doc" runat="server" class="form-control find-dv-txtbx" /></td>
                                            </tr>
                                            <tr>
                                                <td>Graduation</td>
                                                <td>
                                                    <asp:TextBox ID="txt_graduation_university" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_graduation_year_of_passing" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_graduation_total_mark_obtains" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_graduation_percentage_of_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_graduation_division" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_graduation_subject" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:FileUpload ID="graduation_doc" runat="server" class="form-control find-dv-txtbx" /></td>
                                            </tr>
                                            <tr>
                                                <td>Post Graduation</td>
                                                <td>
                                                    <asp:TextBox ID="txt_post_graduation_university" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_post_graduation_year_of_passing" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_post_graduation_total_mark_obtain" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_post_graduation_percentage_of_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_post_graduation_division" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_post_graduation_subject" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:FileUpload ID="post_graduation_doc" runat="server" class="form-control find-dv-txtbx" /></td>
                                            </tr>
                                            <tr>
                                                <td>Other</td>
                                                <td>
                                                    <asp:TextBox ID="txt_other_board" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_other_passing_year" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_other_mark_obtain" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_other_percentage_of_mark" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_other_division" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_other_subjects" runat="server" class="form-control find-dv-txtbx"></asp:TextBox></td>
                                                <td>
                                                    <asp:FileUpload ID="other_doc" runat="server" class="form-control find-dv-txtbx" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <%-- =============== --%>
                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 5px 0px 0px 0px;">
                                <h2 class="form-ttleS">Father's Information</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">First  Name</label>
                                        <asp:TextBox ID="txt_father_name" runat="server" class="form-control find-dv-txtbx" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txt_father_firstname" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Middle Name</label>
                                        <asp:TextBox ID="txt_father_middle_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Last Name</label>
                                        <asp:TextBox ID="txt_father_lastname" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Occupation</label>
                                        <asp:TextBox ID="txt_occupation_father"  runat="server" CssClass="form-control find-dv-txtbx"></asp:TextBox>

                                         
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Father’s Qualification</label>
                                        <asp:TextBox ID="txt_father_qualification" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">Nationality</label>
                                        <asp:TextBox ID="txt_f_nationality" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Maritial Status</label>
                                        <asp:DropDownList ID="ddl_maritial_status" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Married</asp:ListItem>
                                            <asp:ListItem>Unmarried</asp:ListItem>
                                            <asp:ListItem>Divorce</asp:ListItem>
                                            <asp:ListItem>Single Parent</asp:ListItem>
                                            <asp:ListItem>N/A</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mobile No.(Country Code)</label>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddl_cunterycode1" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txt_guardian_mob" runat="server" class="form-control find-dv-txtbx" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Father’s mail id</label>
                                        <asp:TextBox ID="txt_guardian_email" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Guardian's Name</label>
                                        <asp:TextBox ID="txt_guardian_name" runat="server" class="form-control  find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Parents Income</label>
                                        <asp:TextBox ID="txt_parent_income" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                </div>
                            </div>

                            <%-- =============== --%>
                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 5px 0px 0px 0px;">
                                <h2 class="form-ttleS">Mother's Information</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">First Name</label>
                                        <asp:TextBox ID="txt_mother_name" runat="server" class="form-control find-dv-txtbx" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txt_first_name_mother" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Middle Name</label>
                                        <asp:TextBox ID="txt_middle_name_mother" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Last Name</label>
                                        <asp:TextBox ID="txt_last_name_mother" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Occupation</label>

                                         <asp:TextBox ID="txt_m_occupation"  runat="server" CssClass="form-control find-dv-txtbx"></asp:TextBox>


                                       
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mother’s Qualification</label>
                                        <asp:TextBox ID="txt_mother_qulalifiaction" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">Nationality</label>
                                        <asp:TextBox ID="txt_m_nationality" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Maritial Status</label>
                                        <asp:DropDownList ID="ddl_m_maritial_status" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Married</asp:ListItem>
                                            <asp:ListItem>Unmarried</asp:ListItem>
                                            <asp:ListItem>Divorce</asp:ListItem>
                                            <asp:ListItem>Single Parent</asp:ListItem>
                                            <asp:ListItem>N/A</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mobile No(Country Code)</label>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddl_cunterycode2" runat="server" class="form-select find-dv-txtbx">
                                                    <asp:ListItem>+91</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txt_mother_mob" runat="server" class="form-control find-dv-txtbx" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mother’s mail id</label>
                                        <asp:TextBox ID="txt_mother_email" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <%-- =============== --%>
                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 5px 0px 0px 0px;">
                                <h2 class="form-ttleS">Current Address</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">C/o</label>
                                        <asp:TextBox ID="txt_temp_address" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mobile No(Country Code)</label>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddl_cunterycode3" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_cunterycode3_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txt_temp_mobileno" runat="server" class="form-control find-dv-txtbx" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">City/Village</label>
                                        <asp:TextBox ID="txt_temp_city" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">District</label>
                                        <asp:TextBox ID="txt_temp_district" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">P.O.</label>
                                        <asp:TextBox ID="txt_temp_po" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">P.S</label>
                                        <asp:TextBox ID="txt_temp_ps" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">State</label>
                                        <asp:TextBox ID="txt_c_state" runat="server" class="form-control find-dv-txtbx" ></asp:TextBox>
                                        <asp:DropDownList ID="ddl_temp_state" runat="server" class="form-select find-dv-txtbx" Visible="false"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Pin  Code</label>
                                        <asp:TextBox ID="txt_temp_pin" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Country</label>
                                        <asp:DropDownList ID="ddl_country_c" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <%--=================--%>
                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 5px 0px 0px 0px;">
                                <h2 class="form-ttleS">Permanent Address
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="As Same Current Address" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" /></h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">C/o</label>
                                        <asp:TextBox ID="txt_par_address" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mobile No(Country Code)</label>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddl_cunterycode4" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_cunterycode4_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txt_per_mobileno" runat="server" class="form-control find-dv-txtbx" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">City/Village</label>
                                        <asp:TextBox ID="txt_par_city" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">District</label>
                                        <asp:TextBox ID="txt_par_district" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">P.O.</label>
                                        <asp:TextBox ID="txt_par_po" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">P.S</label>
                                        <asp:TextBox ID="txt_par_ps" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">State</label>
                                        <asp:TextBox ID="txt_p_state" runat="server" class="form-control find-dv-txtbx" ></asp:TextBox>
                                        <asp:DropDownList ID="ddl_par_state" runat="server" class="form-select find-dv-txtbx" Visible="false"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Pin Code</label>
                                        <asp:TextBox ID="txt_par_pin" runat="server" class="form-control find-dv-txtbx" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Country</label>
                                        <asp:DropDownList ID="ddl_country_p" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <%-- =============== --%>
                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 5px 0px 0px 0px;">
                                <h2 class="form-ttleS">Bank Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Account No.</label>
                                        <asp:TextBox ID="txt_account_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Account Holder Name</label>
                                        <asp:TextBox ID="txt_account_holder_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Bank Name</label>
                                        <asp:TextBox ID="txt_bank_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">IFSC Code</label>
                                        <asp:TextBox ID="txt_ifsc_code" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">Branch Name</label>
                                        <asp:TextBox ID="txt_branch_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                </div>
                            </div>


                            <%-- =============== --%>
                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 5px 0px 0px 0px;">
                                
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                  


                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_transportation_id" runat="server" />

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


    <script>
        $(function () {
            $("#<%=txt_admission_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_dob.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

    </script>
</asp:Content>
