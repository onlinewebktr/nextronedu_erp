<%@ Page Title="" Language="C#" MasterPageFile="~/Online_Test_admin/Admin.Master" AutoEventWireup="true" CodeBehind="Upload_Bulk_Questions.aspx.cs" Inherits="school_web.Online_Test_admin.Upload_Bulk_Questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Upload Question
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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



            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Exam Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Upload Bulk Question</li>
                        </ol>
                    </nav>
                </div>


            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <table border="0" style="width: 100%; margin: 0px auto; padding: 0px; float: none" class="table table-bordered">


                                            <tr>
                                                <td class="lefttd" style="width: 185px;">Exam Name<sup> </sup>
                                                </td>
                                                <td class="righttd">
                                                    <asp:Label ID="lbl_exmaname" runat="server"></asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lefttd" style="width: 185px;">Class <sup></sup>
                                                </td>
                                                <td class="righttd">
                                                    <asp:Label ID="lbl_classname" runat="server"></asp:Label>


                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="lefttd" style="width: 15px; width: 185px;">Subject <sup></sup>
                                                </td>
                                                <td class="righttd">
                                                    <asp:Label ID="lbl_subject" runat="server"></asp:Label>


                                                </td>
                                            </tr>

                                            <tr style="display: none">
                                                <td class="lefttd" style="width: 15px; width: 185px;">Default Language 
                                                </td>
                                                <td class="righttd">
                                                    <asp:Label ID="lbl_primerylanguage" runat="server">English</asp:Label>

                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="lefttd" style="width: 15px; width: 185px;">Section </td>
                                                <td class="righttd">
                                                    
                                                     <asp:DropDownList ID="ddl_section" runat="server" class="form-select">
                                </asp:DropDownList>

                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="lefttd" style="width: 15px; width: 185px;">Question Document <sup>*</sup>
                                                </td>
                                                <td class="righttd">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" Style="margin: 0px; float: left; float: left" />
                                                </td>
                                            </tr>


                                            <tr>
                                                <td colspan="2" style="padding: 10px 0px 10px 0px; text-align: center;">

                                                    <asp:Button ID="btn_s_add" runat="server"
                                                        Height="31px" Text="Upload" Width="80px" ValidationGroup="a"
                                                        OnClick="btn_s_add_Click" CssClass="mt-2 btn btn-primary" />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 10px 0px 10px 0px; text-align: center;">

                                                    <asp:Label ID="lblmsg" runat="server" Style="color: #d81515;"></asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center">
                                                    <a target="_blank" id="viequestion" height="31px" text="View Question" width="130px" runat="server" visible="false" class="mt-2 btn btn-primary">View Question</a>

                                                    <asp:Button ID="btn_final_submit" runat="server"
                                                        Height="31px" Text="Final Update" Width="130px" CssClass="mt-2 btn btn-primary" CausesValidation="false" OnClick="btn_final_submit_Click" OnClientClick="return confirm('Are you sure want to final update?')" Visible="false" />

                                                    <asp:Button ID="btn_delete_question" runat="server"
                                                        Height="31px" Text="Delete Question" Width="130px" CssClass="mt-2 btn btn-primary" CausesValidation="false" OnClick="btn_delete_question_Click" OnClientClick="return confirm('Are you sure want to delete?')" Visible="false" />
                                                </td>
                                            </tr>


                                        </table>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="card">
                                            <div class="card-body">
                                                <div class="p-4 border rounded">
                                                    <div class="row g-3 needs-validation" novalidate="">
                                                        <div class="col-md-12">
                                                            <a href="Doc/Question_sample.docx" style="font-size: 12px;" download="" class="btnbtn btn-1 btn-sep icon-cart">Download Sample Question </a>

                                                        </div>
                                                        <div class="col-md-12">
                                                            <a href="Doc/Question Coding.pdf" style="font-size: 12px;" download="" class="btnbtn btn-1 btn-sep icon-cart">Download Question Coding </a>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <p>Note: MS Office should be version 10 or higher.</p>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



        </div>
        <!--end row-->
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_userid" runat="server" />
    <asp:HiddenField ID="hd_type" runat="server" />
    <asp:HiddenField ID="hd_test_id" runat="server" />
    <asp:HiddenField ID="hd_question_id" runat="server" />
    <asp:HiddenField ID="hd_class_id" runat="server" />
    <asp:HiddenField ID="hd_exam_type" runat="server" />

    <asp:HiddenField ID="hd_marks" runat="server" />
    <asp:HiddenField ID="hd_sessionid" runat="server" />
    <asp:HiddenField ID="hd_section_id" runat="server" />

    <asp:HiddenField ID="hd_Objective_Entry_id" runat="server" />
</asp:Content>
