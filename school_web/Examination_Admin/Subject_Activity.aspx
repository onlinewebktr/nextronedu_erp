<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Subject_Activity.aspx.cs" Inherits="school_web.Examination_Admin.Subject_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Subject Activity
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        tbody, td, tfoot, th, thead, tr {
            font-size: 13px;
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
                            <li class="breadcrumb-item active" aria-current="page">Subject Activity</li>
                        </ol>
                    </nav>
                </div>
                <a href="#" title="Change Date" data-toggle="modal" data-target="#myModalDate" style="float: right; position: absolute; right: 30px; font-size: 23px; top: 2px;"><i class="bx bx-calendar"></i></a><a href="#" data-toggle="modal" data-target="#myModalSetting" style="float: right; position: absolute; right: 0px; font-size: 23px; top: 2px;"><i class="bx bx-cog"></i></a>
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
                                            <div class="row">
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
                                                    <asp:DropDownList ID="ddl_examterm" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_examterm_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Assessments </label>
                                                    <asp:DropDownList ID="ddl_assessment" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_assessment_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Subject </label>
                                                    <asp:DropDownList ID="ddl_subject" runat="server" class="form-select find-dv-txtbx">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_fina_data_search" runat="server" Text="Find" OnClick="btn_fina_data_search_Click" class="btn btn-primary find-dv-btn" />
                                                </div>
                                                <div class="col-sm-1">
                                                    <a class="btn btn-success find-dv-btn" href="Set_Subject_Activity.aspx" style="margin: 20px 7px 1px 0px !important; float: right; padding: 3px 6px 6px 11px; font-size: 14px;" title="Add Subject Activity"><i class="bx bx-plus-medical"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr" style="overflow: auto;">
                                            <asp:GridView ID="grid_grade" runat="server" OnRowDataBound="grid_grade_RowDataBound" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Course_Name") %>'></asp:Label>
                                                            <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Exam Term">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_examterm" runat="server" Text='<%#Bind("Term_Name") %>'></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Assessments Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Assessments" runat="server" Text='<%#Bind("Assessment_Name") %>'></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_subjectname" runat="server" Text='<%#Bind("Subject_name") %>'></asp:Label>
                                                            (<asp:Label ID="lbl_subjids" runat="server" Text='<%#Bind("Subject_id") %>'></asp:Label>)
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Subject Activity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_subjectactivity" runat="server" Text='<%#Bind("Subject_Activity_Name") %>'></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Grade Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Grade_Name" runat="server" Text='<%#Bind("Grade_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Maximum Marks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Maximum_Marks" runat="server" Text='<%#Bind("Maximum_Marks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cut Off">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Cut_Off_Percentage" runat="server" Text='<%#Bind("Cut_Off_Percentage") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Calulation Type" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_calulattiontype" runat="server" Text='<%#Bind("Calculation_Type") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Marks Entry Deadline">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Marks_Entry_Deadline_Date1" runat="server" Text='<%#Bind("Start_Date_Marks") %>'></asp:Label>

                                                            <asp:Label ID="lbl_Marks_Entry_Deadline_Date2" runat="server" Text='<%#Bind("End_Date_Marks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl_Istatus" runat="server" Text='<%#Bind("Istatus") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Subject Type">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lbl_Subject_Type_Scholastic_Co_Scholastic" runat="server" Text='<%#Bind("Subject_Type_Scholastic_Co_Scholastic") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Subject_Sub_Level_Id" runat="server" Text='<%#Bind("Subject_Sub_Level_Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"><i class="lni lni-pencil-alt"></i><span></span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="lnk_allow_permission_fill_permission" runat="server" ToolTip="Allow Permission"
                                                                CausesValidation="false" OnClick="lnk_allow_permission_fill_permission_Click">
                                                                    Allow Permission</asp:LinkButton>
                                                            <asp:Label ID="lbl_Is_save_marks" runat="server" Text='<%#Bind("Is_save_marks") %>' Visible="false"></asp:Label>

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
    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 500px;
                margin: 1.75rem auto;
            }
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 24px;
        }

            .switch input {
                opacity: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 16px;
                width: 16px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>

    <div id="myModalDate" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 700px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Change Mark Entry Date</h5>
                    <asp:Button ID="Button1" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation" novalidate="">
                            <div class="col-md-6">
                                <label for="validationCustom01" class="form-label">
                                    Session <sup>*</sup>
                                </label>
                                <asp:DropDownList ID="ddl_change_date_session" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_change_date_session_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="validationCustom01" class="form-label">
                                    Choose Class <sup>*</sup>
                                </label>
                                <asp:DropDownList ID="ddl_exam_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_exam_class_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="validationCustom01" class="form-label">
                                    Choose Exam <sup>*</sup>
                                </label>
                                <asp:DropDownList ID="ddl_exam_name" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="validationCustom01" class="form-label">Marks Entry End Date<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="aa" ControlToValidate="txt_end_date"></asp:RequiredFieldValidator></sup></label>
                                <asp:TextBox ID="txt_end_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                            </div>
                            <div class="col-12">
                                <asp:Button ID="btn_change_date" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="aa" OnClick="btn_change_date_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Allow Permission</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation" novalidate="">
                            <div class="col-md-4">
                                <label for="validationCustom01" class="form-label">
                                    Marks Entry Date From <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_start_date"></asp:RequiredFieldValidator>
                                    </sup>
                                </label>
                                <asp:TextBox ID="txt_start_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label for="validationCustom01" class="form-label">Marks Entry Date End<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_enddate"></asp:RequiredFieldValidator></sup></label>
                                <asp:TextBox ID="txt_enddate" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label for="validationCustom01" class="form-label">Allow permission for marks entry</label>
                                <label class="switch" style="margin: 21px 0px 0px 0px;">
                                    <asp:CheckBox ID="chk_allow" runat="server" Checked="false" />
                                    <span class="slider round"></span>
                                </label>
                            </div>
                            <div class="col-12">
                                <asp:Button ID="btn_Submit" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClientClick="return close()" class="btn btn-dark" Visible="false" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="fadeup"></div>

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script type="text/javascript">
        function openModal() {
            $("#myModal").show();
            $('#myModal').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#myModal").hide();
            $('#myModal').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>



    <script type="text/javascript">
        function openModalSetting() {
            $('#myModalSetting').modal('show');
        }
        function openModalDateSetting() {
            $('#myModalDate').modal('show');
        }
    </script>


    <style type="text/css">
        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .mdl-close-btn {
            margin: 0px;
            padding: 0px 5px 0px 5px;
            border: 0px;
            background: #ed0000;
            font-size: 18px;
            color: #fff;
            line-height: 25px;
            border-radius: 2px;
        }

        .modal-header {
            padding: 7px 15px;
        }
    </style>


    <div class="modal fade" id="myModalSetting" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 640px;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 18px;">Copy Assessment Subject Setting For Next Term/Session</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Copy From Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_current_session" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_current_session_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Copy For Next</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_copy_to" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_copy_to_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Term</asp:ListItem>
                                    <asp:ListItem>Session</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div id="copy_to_SessionDV" runat="server" visible="false" style="width: 100%; float: left">
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Choose Session</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_copy_to_session" runat="server" class="form-select"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="copy_to_termDV" runat="server" visible="false" style="width: 100%; float: left">
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Copy From Term</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_copy_from_term" runat="server" class="form-select" OnSelectedIndexChanged="ddl_copy_from_term_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Copy to Term</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_copy_to_term_for_term" runat="server" class="form-select"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_copy_setting" runat="server" Text="Submit" class="btn btn-primary" OnClick="btn_copy_setting_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
