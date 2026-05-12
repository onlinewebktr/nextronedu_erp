<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Report_Full_Marks.aspx.cs" Inherits="school_web.Examination_Admin.Report_Full_Marks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Report Full Marks
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Reports</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Marks Entry View</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Visible="false" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Marks Entry"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-4">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_CourseCat" class="form-select find-dv-txtbx" runat="server" Style="width: 100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Section<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_section" class="form-select find-dv-txtbx" runat="server" Style="width: 98%"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Term<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_term" class="form-select find-dv-txtbx" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_term_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-8">
                                        <div class="row"> 
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Assessment<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_assesment" class="form-select find-dv-txtbx" runat="server" Style="width: 98%"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Subject<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_subject" class="form-select find-dv-txtbx" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Subject Activity<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_exam_level" class="form-select find-dv-txtbx" runat="server" Style="width: 98%"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3" style="padding-left: 0px">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" Style="margin: 22px 0px 0px 0px; padding: 4px 10px 4px;"
                                                    OnClick="btn_find_Click" />

                                                <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin: 21px 0px 1px 0px !important; margin-left: 10px; margin: 24px 0px 0px 0px; float: right; padding: 4px 5px;"
                                                    class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-xl-12">
                                        <hr />

                                        <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">
                                            <table style="width: 100%;" id="example1" data-page-length='100000' class="table table-hover table-striped table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Student</th>
                                                        <th>Roll No.</th>
                                                        <th>Assessment</th>
                                                        <th>Subject</th>
                                                        <th>Subject Activity</th>
                                                        <th>Full Marks</th>
                                                        <th>Obtained Marks</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPDetails" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_studentname" runat="server" Font-Names="Arial" Text='<%#Bind("studentname") %>'></asp:Label>
                                                                    (<asp:Label ID="lbl_adm_no" runat="server" Font-Names="Arial" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>)
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_assesment" runat="server" Font-Names="Arial" Text='<%#Bind("Assessment_Name") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_subject" runat="server" Font-Names="Arial" Text='<%#Bind("Subject_name") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_subject_activity" runat="server" Font-Names="Arial" Text='<%#Bind("Subject_Activity_Name") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_fulmarks" runat="server" Font-Names="Arial" Text='<%#Bind("Maximum_Marks") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_obtain_marks" runat="server" Font-Names="Arial" Text='<%#Bind("Marks") %>'></asp:Label>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </asp:Panel>

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
</asp:Content>
