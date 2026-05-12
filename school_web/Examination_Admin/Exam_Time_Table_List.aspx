<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Exam_Time_Table_List.aspx.cs" Inherits="school_web.Examination_Admin.Exam_Time_Table_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Print Admit Card Bulk
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
                <div class="breadcrumb-title pe-3">Admit Card</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Print Admit Card Bulk</li>
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
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row g-3 needs-validation" novalidate="">
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Exam Term</label>
                                                    <asp:DropDownList ID="ddl_exam_term" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_exam_term_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Exam</label>
                                                    <asp:DropDownList ID="ddl_exam" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-3">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <label for="validationCustom01" class="find-dv-lbl">Section </label>
                                                            <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </div>

                                                        <div class="col-sm-6">
                                                            <label for="validationCustom01" class="find-dv-lbl">Page</label>
                                                            <asp:DropDownList ID="ddl_page" runat="server" class="form-select find-dv-txtbx">
                                                                <asp:ListItem Value="1">A4 X 1</asp:ListItem>
                                                                <asp:ListItem Value="2">A4 X 2</asp:ListItem>
                                                                <asp:ListItem Value="4">A4 X 4</asp:ListItem>
                                                                <asp:ListItem Value="5">A4 X 2 (portrait)</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>




                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" />
                                                </div>


                                                <div class="col-sm-2" style="display: none">
                                                    <label for="validationCustom01" class="find-dv-lbl">Enter Admission No</label>
                                                    <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1" style="display: none">
                                                    <asp:Button ID="btn_find_by_admission_no" runat="server" Text="Find" OnClick="btn_find_by_admission_no_Click" class="btn btn-primary find-dv-btn" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr">
                                            <asp:GridView ID="grid_grade" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable" OnRowDataBound="grid_grade_RowDataBound">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("classname") %>'></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Section">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Exam Term ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Term_Name" runat="server" Text='<%#Bind("examtermname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Exam">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_exam_name" runat="server" Text='<%#Bind("exam_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Maximum_Marks" runat="server" Text='<%#Bind("Created_datetime1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_exam_id" runat="server" Text='<%#Bind("Exam_id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Session_Id" runat="server" Text='<%#Bind("Session_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Branch_id" runat="server" Text='<%#Bind("Branch_id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_shift_type" runat="server" Text='<%#Bind("Shift_type") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Exam_Term_Id" runat="server" Text='<%#Bind("Exam_Term_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("admissionnumber") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"></i><span> </span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>




                                                            <a id="admitcardLink" runat="server" target="_blank"><i class='bx bx-printer'></i><span>Print</span></a>

                                                            <%--<a id="a1" runat="server" href="slip/Print_Exam_admit_card.aspx?session_Id=<%#Eval("Session_Id") %>&branch_id=<%#Eval("Branch_id") %>&classid=<%#Eval("Class_id") %>&section=<%#Eval("Section") %>&examterm=<%#Eval("Exam_Term_Id") %>&admin=<%#Eval("admissionnumber") %>" target="_blank"><i class='bx bx-printer'></i><span>Print</span></a>--%>
                                                        </ItemTemplate>


                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>

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
</asp:Content>
