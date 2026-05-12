<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="descriptive-indicator.aspx.cs" Inherits="school_web.Examination_Admin.descriptive_indicator1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server"> Descriptive Indicator
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

        .subs-box-wpr {
            margin: 10px 0px;
            padding: 0px 0px;
            width: 100%;
            float: left;
            text-align: center;
            box-shadow: 0 1px 3px 0 rgb(0 0 0 / 20%), 0 1px 1px 0 rgb(0 0 0 / 14%), 0 2px 1px -1px rgb(0 0 0 / 12%);
        }

        .subs-box-wpr-txt {
            margin: 0px;
            padding: 16px;
            width: 100%;
            float: left;
            background-color: rgb(201, 152, 241);
        }

        .subs-box-wpr-btn {
            margin: 0px;
            padding: 10px 0px;
            width: 100%;
            float: left;
            font-size: 16px;
        }

            .subs-box-wpr-btn a {
                font-size: 16px;
                color: #000;
            }

        .subs-box-sngl-wrd {
            padding: 0px 0px;
            font-size: 24px;
            font-weight: 400;
            color: #fff;
            border: 1px solid #e5e5e5;
            border-radius: 50%;
            height: 62px;
            line-height: 62px;
            margin: 10px auto;
            text-align: center;
            text-transform: uppercase;
            width: 62px;
            display: block;
        }

        .subs-box-sub-name {
            margin: 5px 0px 5px 0px;
            padding: 0px 0px;
            width: 100%;
            float: left;
            font-size: 15px;
            text-transform: uppercase;
            color: #fff;
        }

        .chosen-btns {
            margin: 0px 5px 10px 0px;
            padding: 3px 10px 4px;
            width: auto;
            float: left;
            font-weight: 600;
            border: 2px solid #b3b3b3;
            letter-spacing: 0.5px;
            font-size: 14px;
            border-radius: 2px;
            color: #464646;
        }

        .chosen-btns-active {
            color: #0d6efd;
            border: 2px solid #0d6efd;
        }
    </style>
    <style>
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


        /*===============================*/
        .switchs {
            position: relative;
            display: inline-block;
            width: 24px;
            height: 24px;
        }

            .switchs input {
                opacity: 0;
            }

        .sliders {
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

            .sliders:before {
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

        input:checked + .sliders {
            background-color: #2196F3;
        }

        input:focus + .sliders {
            box-shadow: 0 0 1px #2196F3;
        }

        /*input:checked + .sliders:before {
            -webkit-transform: translateX(4px);
            -ms-transform: translateX(4px);
            transform: translateX(4px);
        }*/

        /* Rounded sliders */
        .sliders.rounds {
            border-radius: 34px;
        }

            .sliders.rounds:before {
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
                                    <asp:Panel ID="pnl_choose_subjects" runat="server" Visible="true">
                                        <div class="col-sm-12">
                                            <asp:LinkButton ID="lnk_scholastic" runat="server" OnClick="lnk_scholastic_Click" class="chosen-btns">Scholastic</asp:LinkButton>
                                            <asp:LinkButton ID="lnk_co_scholastic" runat="server" OnClick="lnk_co_scholastic_Click" class="chosen-btns">Co-scholastic</asp:LinkButton>
                                        </div>

                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-3" id="storeDv" runat="server">
                                                    <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Class Name</label>
                                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Term</label>
                                                    <asp:DropDownList ID="ddl_term" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_term_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Exam Structure</label>
                                                    <asp:DropDownList ID="ddl_exam_structure" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_exam_structure_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2">
                                                    <label class="switch" style="margin: 23px 10px 0px 0px;">
                                                        <asp:CheckBox ID="chk_activity" AutoPostBack="true" OnCheckedChanged="chk_activity_CheckedChanged" runat="server" />
                                                        <span class="slider round"></span>
                                                    </label>
                                                    Activity
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Subject</label>
                                                    <asp:DropDownList ID="ddl_subject" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="trm-sub-pass-f-dv">
                                            <div class="row">
                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="col-xl-3">
                                                            <div class="subs-box-wpr">
                                                                <div class="subs-box-wpr-txt">
                                                                    <asp:Label ID="lbl_first_letter" class="subs-box-sngl-wrd" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_subject_name" runat="server" class="subs-box-sub-name" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_subject_id" runat="server" Visible="false" Text='<%#Bind("Subject_id")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_subject_sub_level_Id" runat="server" Visible="false" Text='<%#Bind("Subject_Sub_Level_Id")%>'></asp:Label>
                                                                </div>

                                                                <div class="subs-box-wpr-btn">
                                                                    <asp:LinkButton ID="lnk_edt_for_remarks" OnClick="lnk_edt_for_remarks_Click" runat="server"><i class="lni bx bxs-pencil"></i></asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </asp:Panel>


                                    <asp:Panel ID="pnl_remarks_panel" runat="server" Visible="false">
                                        <div class="trm-sub-pass-f-dv">
                                            <div class="back-dv">
                                                <asp:LinkButton ID="lnk_back_to_choose_sub" runat="server" OnClick="lnk_back_to_choose_sub_Click"><i class="bx bx-arrow-to-left"></i></asp:LinkButton><asp:Label ID="lbl_head_on_update" runat="server"></asp:Label>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-8">
                                                    <asp:Repeater ID="rp_grades" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_grades" runat="server" OnClick="lnk_grades_Click" Text='<%#Bind("Grade_name")%>' class="chosen-btns"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>

                                                <div class="col-sm-4">
                                                    <div style="margin: 0px; padding: 0px; float: left; height: 50px; width: 100%;">
                                                        <a onclick="openModal()">
                                                            <img src="../assets/images/add_subject.png" style="height: 50px; width: 50px; float: right;" /></a>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="trm-sub-box-wpr">
                                                <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 90px">Use Assign</th>
                                                            <th>Remarks</th>
                                                            <th style="width: 90px">Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rp_remarks" runat="server" OnItemDataBound="rp_remarks_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <label class="switchs" style="margin: 0px 10px 0px 0px;">
                                                                            <asp:RadioButton ID="rd_assign" runat="server" GroupName="A" AutoPostBack="true" OnCheckedChanged="rd_assign_CheckedChanged" />
                                                                            <%--<asp:CheckBox ID="chk_activity" runat="server" />--%>
                                                                            <span class="sliders rounds"></span>
                                                                        </label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_remark" runat="server" Text='<%#Bind("Remark")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
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
    <asp:HiddenField ID="hd_id" runat="server" />

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add Descriptive Indicator</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" Text="Close"  OnClick="btn_cancel_Click"  />
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation" novalidate="">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label">Remark</label>
                                <asp:TextBox ID="txt_remark" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <div class="col-12">
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" OnClick="btn_cancel_Click" />
                                <asp:Button ID="btn_save_remarks" OnClick="btn_save_remarks_Click" runat="server" Text="Save" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }

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

    <style>
        .modal-dialog {
            max-width: 600px;
        }

        .modal {
            background: rgb(0 0 0 / 34%);
        }

        .back-dv {
            margin: -8px 0px 15px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            border-bottom: 1px solid #ddd;
        }

            .back-dv a {
                float: left;
                margin: -2px 10px 0px 0px;
                padding: 0px;
                font-size: 25px;
            }

            .back-dv span {
                margin: -6px 0px 0px 0px;
                padding: 0px;
                font-size: 20px;
            }
    </style>
</asp:Content>
