<%@ Page Title="" Language="C#" MasterPageFile="~/Online_Test_admin/Admin.Master" AutoEventWireup="true" CodeBehind="Question_Details.aspx.cs" Inherits="school_web.Online_Test_admin.Question_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Question  Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" async src="https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.1/MathJax.js?config=MML_HTMLorMML-full">
    </script>
    <style>
        .table td {
            padding: 1px 0px 1px 6px !important;
            vertical-align: top;
            border-top: 1px solid #e9ecef00;
        }
    </style>
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
                            <li class="breadcrumb-item active" aria-current="page">Question Details</li>
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
                            <div style="width: 100%; float: right; padding: 4px 2px 3px 5px; border: 1px dashed #dcd6d6; margin: 0px 0px 8px 0px;">
                                <table style="width: 100%; float: right; margin: 0px; padding: 0px;" class="table">
                                    <tr>
                                        <td>Exam Name :</td>
                                        <td>
                                            <asp:Label ID="lbl_testname1" runat="server" Font-Bold="true"></asp:Label>

                                        </td>
                                        <td>Class :</td>
                                        <td>
                                            <asp:Label ID="lbl_classname" runat="server" Font-Bold="true"></asp:Label>

                                        </td>
                                        <td style="display:none">Section Name :</td>
                                        <td style="display:none">
                                            <asp:Label ID="lbl_section" runat="server" Font-Bold="true"></asp:Label>

                                        </td>
                                        <td>Subject Name :</td>
                                        <td>
                                            <asp:Label ID="lbl_subjectname_view" runat="server" Font-Bold="true"></asp:Label>

                                        </td>

                                        <td style="display:none">Default Language:</td>
                                        <td style="display:none">
                                            <asp:Label ID="lbl_defult_Language" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td style="display:none">Regional Language:</td>
                                        <td style="display:none">
                                            <asp:Label ID="lbl_secondaryLanguage" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td>No Of Question:</td>
                                        <td>
                                            <asp:Label ID="lbl_no_ofquestion" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>

                                        <td>Upload By</td>
                                        <td>
                                            <asp:Label ID="lbl_upload_by" runat="server" Font-Bold="true"></asp:Label></td>
                                        <td>Upload Date</td>
                                        <td>
                                            <asp:Label ID="lbl_upload_date" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                              
                    <asp:GridView ID="grid_view" runat="server" AutoGenerateColumns="False"
                        Width="100%" Style="float: left; text-align: left; margin: 0px; padding: 0px;" ShowHeader="False" OnRowDataBound="grid_view_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table border="0" style="width: 100%; margin: 0px; padding: 0px; float: left;" class="table">


                                        <tr>
                                            <td class="lefttd" style="width: 100%">
                                                <asp:Label ID="lbl_direction_en" runat="server" Text='<%#Bind("Direction") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lbl_direction_hn" runat="server" Text='<%#Bind("Direction_HN") %>'></asp:Label>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td class="lefttd" style="width: 100%">
                                                <asp:Label ID="lbl_phrase_en" runat="server" Text='<%#Bind("DI") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lbl_phrase_hn" runat="server" Text='<%#Bind("DI_HN") %>'></asp:Label>
                                            </td>

                                        </tr>


                                        <tr>
                                            <td class="lefttd" style="width: 100%">
                                                <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>" Style="color: #d81515; font-weight: bold;"></asp:Label>)&nbsp; 
                                            <asp:Label ID="lbl_question_en" runat="server" Text='<%#Bind("Question_name") %>' Style="font-weight: bold;"></asp:Label>
                                                <br />
                                                <asp:Label ID="lbl_question_hn" runat="server" Text='<%#Bind("Question_name_HN") %>'></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>

                                            <td style="padding: 5px 0px 0px 38px;">
                                                <asp:Label ID="lbl_option_en" runat="server" Style="color: #ce5b5b;"></asp:Label>
                                                <br />
                                                <asp:Label ID="lbl_option_hn" runat="server" Style="color: #ce5b5b;" Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_questionid" runat="server" Text='<%#Bind("questionid") %>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td style="padding: 5px 0px 0px 38px; color: #1989ea;">
                                                <asp:Label ID="lbl_opteionans" runat="server" Text='<%#Bind("Answer") %>'></asp:Label>&nbsp;
                                                <asp:Label ID="lbl_ans" runat="server" Text='<%#Bind("ans") %>'></asp:Label>&nbsp;&nbsp;<asp:Label ID="lbl_ans_HN" runat="server" Text='<%#Bind("ans_HN") %>'></asp:Label>

                                            </td>
                                        </tr>

                                        <tr>

                                            <td style="padding: 5px 0px 0px 38px; color: #0c0998">Marks :-
                                                        <asp:Label ID="lbl_marks" runat="server" Text='<%#Bind("marks") %>'></asp:Label>
                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>

                                            <td style="padding: 5px 0px 0px 38px;">
                                                <asp:Label ID="lbl_explanation" runat="server" Style="color: green"></asp:Label>

                                            </td>
                                        </tr>


                                    </table>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>





                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
     <asp:HiddenField ID="hd_test_id" runat="server" />
    <asp:HiddenField ID="hd_exam_type" runat="server" />
    <asp:HiddenField ID="hd_section_id" runat="server" />
    <asp:HiddenField ID="hd_user" runat="server" />
    <asp:HiddenField ID="hd_actiontype" runat="server" />
    <asp:HiddenField ID="hd_Objective_Entry_id" runat="server" />
</asp:Content>
