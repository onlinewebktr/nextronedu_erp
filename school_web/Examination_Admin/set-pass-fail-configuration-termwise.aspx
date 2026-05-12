<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="set-pass-fail-configuration-termwise.aspx.cs" Inherits="school_web.Examination_Admin.set_pass_fail_configuration_termwise1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server"> Set Pass Fail Configuration Term Wise
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <style>
        .table-responsive {
            overflow-x: inherit;
        }

        .trm-sub-pass-f-dv {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .trm-sub-box-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            box-shadow: 0 1px 3px 0 rgb(0 0 0 / 20%), 0 1px 1px 0 rgb(0 0 0 / 14%), 0 2px 1px -1px rgb(0 0 0 / 12%);
        }

        .trm-sub-box-wpr-h {
            margin: 0px;
            padding: 10px 0px;
            width: 100%;
            float: left;
            text-align: center;
            font-size: 16px;
            background: rgb(250,250,250);
            font-weight: 600;
        }


        .trm-sub-box-wpr table {
            width: 100%;
        }

            .trm-sub-box-wpr table tr th {
                padding: 10px 20px;
                background: #efefef !important;
                border-bottom: 1px solid #e5e5e5;
                color: rgba(0,0,0,.87);
            }

            .trm-sub-box-wpr table tr td {
                padding: 10px 20px;
                border-bottom: 1px solid #e5e5e5;
                color: rgba(0,0,0,.87);
            }


        .trm-sub-box-txtbx {
            border: 0px;
            border-bottom: 1px solid #c9c9c9;
            color: rgba(0,0,0,0.87);
        }
    </style>
    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 916px;
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
                <div class="breadcrumb-title pe-3"><a href="exam-setup-home.aspx" runat="server" id="backbtns" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Exam Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Set Pass Fail Configuration Term Wise</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="find-dv">
                                        <div class="row">
                                            <div class="col-sm-3" id="storeDv" runat="server">
                                                <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Class Name</label>
                                                <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="trm-sub-pass-f-dv">
                                        <label class="switch" style="margin: 0px 0px 0px 0px;">
                                            <asp:CheckBox ID="chk_individual_sub" runat="server" AutoPostBack="true" OnCheckedChanged="chk_individual_sub_CheckedChanged" />
                                            <span class="slider round"></span>
                                        </label>
                                        Define annual and term-wise pass percentage for individual subjects.


                                        <asp:Panel ID="pnl_over_all" runat="server" Visible="false">
                                            <div class="trm-sub-box-wpr">
                                                <table>
                                                    <tr>
                                                        <th>Condition</th>
                                                        <th>Value(%)</th>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;">Term Level Subject Pass %</td>
                                                        <td style="text-align: left;">
                                                            <asp:TextBox ID="txt_percentage_o" runat="server" class="trm-sub-box-txtbx"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-12">
                                                <asp:Button ID="btn_save_overall" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btn_save_overall_Click" Style="margin: 10px 0px 0px 0px" />
                                                <asp:Button ID="btn_delete_overall" runat="server" Text="Delete" class="btn btn-dark" OnClientClick="return confirm('Are you sure you want to delete?');" CausesValidation="false" OnClick="btn_delete_overall_Click" Style="margin: 10px 0px 0px 0px" />
                                            </div>
                                        </asp:Panel>



                                        <asp:Panel ID="pnl_individuasal" runat="server" Visible="false">
                                            <div class="row">
                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="col-xl-6">
                                                            <div class="trm-sub-box-wpr">
                                                                <asp:Label ID="lbl_term_name" class="trm-sub-box-wpr-h" runat="server" Text='<%#Bind("Term_Name")%>'></asp:Label>
                                                                <asp:Label ID="lbl_term_id" runat="server" Visible="false" Text='<%#Bind("Exam_Term_Id")%>'></asp:Label>

                                                                <table>
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Subjects</th>
                                                                            <th>Percentage</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rp_subjects" runat="server" OnItemDataBound="rp_subjects_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_subject_Name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:TextBox ID="txt_percentage" runat="server" class="trm-sub-box-txtbx"></asp:TextBox>
                                                                                        <asp:Label ID="lbl_terms_id" runat="server" Visible="false" Text='<%#Bind("Term_id")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_subject_id" runat="server" Visible="false" Text='<%#Bind("Subject_id")%>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>


                                            <div class="col-12">
                                                <asp:Button ID="btn_save" runat="server" Text="Save" CssClass="btn btn-primary" Visible="false" OnClick="btn_save_Click" Style="margin: 10px 0px 0px 0px" />
                                                <asp:Button ID="btn_delete" runat="server" Text="Delete" class="btn btn-dark" Visible="false" OnClientClick="return confirm('Are you sure you want to delete?');" CausesValidation="false" OnClick="btn_delete_Click" Style="margin: 10px 0px 0px 0px" />
                                            </div>
                                        </asp:Panel>
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
