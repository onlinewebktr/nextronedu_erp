<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="students_subject_mapping_New.aspx.cs" Inherits="school_web.Admin.students_subject_mapping_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    students-subject-mapping 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .find-dv-lbl {
            margin: 0px;
            padding: 12px 0px 0px 0px;
            width: 100%;
            float: left;
        } 
        table tr th input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 1.15em;
            height: 1.15em;
            border: 0.15em solid currentColor;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        } 
        table tr th label {
            width: 100%;
        } 
        table tr td input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 20px;
            height: 20px;
            border: 1px solid #646464;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }

        .form-control + .form-control {
            margin-top: 1em;
        }

        tfoot, th, thead {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            background: #0c6eff !important;
            text-align: center;
            font-size: 13px;
            color: #fff;
        }

        tbody, td, tfoot, th, thead, tr {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            text-align: center;
            font-size: 13px;
        }

        .wrapper.toggled:not(.sidebar-hovered) .sidebar-wrapper {
            width: 70px;
        }

        .modal {
            background: rgb(0 0 0 / 75%);
        }

        tbody, td, tfoot, th, thead, tr {
            font-size: 12px;
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Subject Allocation</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Subject Allocation"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-sm-2">
                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1">
                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1">
                                        <label for="validationCustom01" class="find-dv-lbl">Religion</label>
                                        <asp:DropDownList ID="ddl_religion" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" Style="margin: 34px 0px 0px 0px!important;" />
                                    </div> 
                                    <div class="col-sm-1"> 
                                        <label for="validationCustom01" class="find-dv-lbl">OR</label>
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="validationCustom01" class="find-dv-lbl">Admission No.</label>
                                        <asp:TextBox ID="txt_admission_no" runat="server" CssClass="form-control find-dv-txtbx"></asp:TextBox> 
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Button ID="btn_find_by_admission" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_admission_Click" Style="margin: 34px 0px 0px 0px!important;" />
                                        <div style="display: none;">
                                            <asp:Button ID="btn_empty" runat="server" Text="Find" OnClick="btn_empty_Click" />
                                        </div>
                                        <script>
                                            function click_empty() {
                                                console.log("before clicked");
                                                document.getElementById('<%=btn_empty.ClientID%>').click();
                                            }
                                        </script>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btn_chek_all" runat="server" Text="Check All" Visible="false" class="btn btn-primary find-dv-btn" OnClick="btn_chek_all_Click" Style="margin: 34px 0px 0px 0px;" />
                                        <asp:Button ID="btn_uncheck_all" runat="server" Text="Uncheck All" Visible="false" class="btn btn-primary find-dv-btn" OnClick="btn_uncheck_all_Click" Style="margin: 34px 0px 0px 18px;" />
                                    </div> 
                                </div>

                                <div class="row">
                                    <div class="col-xl-12">
                                        <hr />
                                        <div style="overflow-y: auto;">
                                            <div style="margin: 0px; padding: 0px; float: left; height: auto; margin: 0px; width: 100%; float: left;"> 
                                                <div style="margin: 0px; padding: 0px; float: left; height: auto; margin: 0px; width: 100%; float: left;">
                                                    <asp:GridView ID="GrdView_data" runat="server" class="table table-bordered" AutoGenerateColumns="true" Width="100%" OnRowCreated="GrdView_data_RowCreated">
                                                    </asp:GridView>
                                                </div>
                                                <script>
                                                    function subSelect(v, p) {
                                                        var chk = $('#ContentPlaceHolder1_GrdView_data_' + p);
                                                        if (chk.prop('checked')) {
                                                            $('.' + v).prop('checked', true);
                                                        }
                                                        else {
                                                            $('.' + v).prop('checked', false);
                                                        }
                                                    }
                                                </script> 
                                            </div>
                                        </div>
                                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                            <asp:Button ID="btn_map" runat="server" Visible="false" Text="Map Subject" class="btn btn-primary find-dv-btn" OnClick="btn_map_Click" />
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



    <div class="modal fade" id="myModalPwd" role="dialog" style="top: 0px;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog md-width" style="max-width: 300px; transform: translate(0, 50px);">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-body md-bdy">
                    <ul class="srch-popup-close-btn-ul">
                        <li>
                            <a href="javascript:" data-dismiss="modal"><i class="bx bx-x" aria-hidden="true"></i></a>
                        </li>
                    </ul>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="pwdtxtbxWpR">
                                    <asp:TextBox ID="txt_pwd_code" runat="server" class="form-control" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                                    <asp:Button ID="btn_password" runat="server" Text="Submit" class="btn btn-primary pwdtxtbxWpR-Button" OnClick="btn_password_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript"> 
        function openModalPwd() {
            $('#myModalPwd').modal('show'); 
        }
    </script>


    
</asp:Content>
