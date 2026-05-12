<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="exam-hierarchy-validator.aspx.cs" Inherits="school_web.Examination_Admin.exam_hierarchy_validator1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server"> Exam Hierarchy Validator
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Category</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Category</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">


                                    <div class="find-dv">
                                        <div class="row">
                                            <div class="col-sm-3" id="storeDv" runat="server">
                                                <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Class Name</label>
                                                <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="trm-sub-pass-f-dv">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Repeater ID="rp_terms" runat="server">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_term_id" runat="server" Visible="false" Text='<%#Bind("Exam_Term_Id")%>'></asp:Label>
                                                        <asp:LinkButton ID="lnk_terms" runat="server" OnClick="lnk_terms_Click" Text='<%#Bind("Term_Name")%>' class="chosen-btns"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="trm-sub-box-wpr">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table>
                                                    <tr>
                                                        <th>
                                                            <asp:Label ID="lbl_trm_heading" runat="server" Text=""></asp:Label>
                                                        </th>
                                                    </tr>

                                                    <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_assessment_name" runat="server" Text='<%#Bind("Assessment_Name")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_assessment_id" Visible="false" runat="server" Text='<%#Bind("Assessment_Id")%>'></asp:Label>
                                                                </td>
                                                            </tr>


                                                            <asp:Label ID="lbl_subjectDV" runat="server">
                                                                <tr id="subjectDV">
                                                                    <td>
                                                                        <table>
                                                                            <asp:Repeater ID="rd_subjects" runat="server" OnItemDataBound="rd_subjects_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                                                                            <asp:Label ID="lbl_subject_id" Visible="false" runat="server" Text='<%#Bind("Subject_id")%>'></asp:Label>
                                                                                            <asp:Label ID="lbl_assessment_id" Visible="false" runat="server" Text='<%#Bind("Assessment_id")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <asp:Label ID="lbl_activityDV" runat="server">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table>
                                                                                                    <asp:Repeater ID="rd_activity" runat="server">
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td style="text-align: left;">
                                                                                                                    <asp:Label ID="lbl_subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                                                                                                    <asp:Label ID="lbl_subject_id" Visible="false" runat="server" Text='<%#Bind("Subject_id")%>'></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:Repeater>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
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

</asp:Content>
