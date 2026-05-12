<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="bulk-attendance.aspx.cs" Inherits="school_web.Admin.bulk_attendance" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Bulk Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-responsive {
            overflow-x: inherit;
        }

        .rdobtnS {
            margin: 0px 0px 0px 0px;
            font-size: 12px;
        }

            .rdobtnS tr {
                padding: 0px 2px;
                width: 33px;
                float: left;
            }

                .rdobtnS tr td {
                    padding: 0px;
                    width: 30px;
                    margin: 0px;
                    height: 30px;
                    float: left;
                    text-align: center;
                    font-weight: 800;
                    color: #000;
                    pointer-events: none;
                }

                    .rdobtnS tr td label {
                        position: relative;
                        top: -28px;
                    }


        .att-imgs {
            padding: 3px;
            width: 44px;
            height: 44px;
            border: 1px solid #a1a1a1;
            border-radius: 50%;
        }

        .att-name {
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 12px;
            font-weight: 500;
        }

        .att-adm_no {
            padding: 0px;
            width: 100%;
            float: left;
        }

        .att-roll {
            padding: 0px;
            width: 100%;
            float: left;
        }

        .img-dv {
            padding: 0px;
            width: 45px;
            float: left;
        }

        .contnt-dv {
            padding: 0px 0px 0px 10px;
            float: left;
            width: 52%;
            text-align: left;
        }

        .action-dv {
            padding: 8px 0px 0px 0px;
            float: right;
        }

        .ui-datepicker-trigger {
            display: none;
        }

        .container {
            padding-right: 10px;
            padding-left: 10px;
        }


        .rdobtnS tr:nth-child(1) {
        }

        .rdobtnS tr:nth-child(2) {
        }

        .rdobtnS tr:nth-child(3) {
        }


        .rdobtnS tr:nth-child(1) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #39c500;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(1) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(1) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #3ed700;
            }

                .rdobtnS tr:nth-child(1) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }

                .rdobtnS tr:nth-child(1) input[type="radio"]:checked label {
                    color: #fff;
                }

        /*========================SecondRoW============================*/
        .rdobtnS tr:nth-child(2) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #ff0000;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(2) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(2) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #ff0000;
            }

                .rdobtnS tr:nth-child(2) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }


        /*========================THIRDROWS============================*/
        .rdobtnS tr:nth-child(3) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #ffa500;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(3) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(3) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #ffa500;
            }

                .rdobtnS tr:nth-child(3) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }


        th {
            display: none;
        }
         .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 5px;
            left: -23px;
        }
    </style>
    
     <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
            }).attr("readonly", "true");
        });
    </script>
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Student Attndance</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Make Bulk Attendance</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="row">
                                                            <div class="col-sm-2" style="display: none">
                                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                                <asp:DropDownList ID="ddl_session" runat="server" class="form-control  find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Date</label>
                                                                <asp:TextBox ID="txt_date" runat="server"   class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Teacher</label>
                                                                <asp:DropDownList ID="ddl_teacher" runat="server" CssClass="form-control  find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_teacher_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                                <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control  find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                                <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control  find-dv-txtbx"></asp:DropDownList>
                                                            </div>


                                                            <div class="col-sm-2">
                                                                <asp:Button ID="btn_download_excel" runat="server" Text="Download Excel Format" class="btn btn-primary find-dv-btn" OnClick="btn_download_excel_Click" />
                                                            </div>
                                                        </div>

                                                        <div style="float: left; width: 100%">
                                                            <div class="row">
                                                                <div class="col-sm-3"></div>

                                                                <div class="col-sm-6">
                                                                    <div style="float: left; width: 100%; background: #f7f7f7; padding: 8px 10px 10px 10px; border-radius: 3px; margin: 14px 0px 0px 0px; border: 1px solid #dddddd;">
                                                                        <div class="row">
                                                                            <div class="col-sm-8">
                                                                                <label for="validationCustom01" class="find-dv-lbl">Choose Excel (.csv file)</label>
                                                                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control  find-dv-txtbx" />
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <asp:Button ID="btn_upload_attendance" runat="server" Text="Upload Excel" class="btn btn-primary find-dv-btn" OnClick="btn_upload_attendance_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="grd-wpr">
                                                <asp:GridView ID="grd_student_list" Visible="false" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" AllowPaging="false" Font-Bold="False" PageSize="50">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Admission No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Student Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Roll No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Section">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attendance">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_section" runat="server" Text="P"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>


                                                <asp:Panel ID="pnl_grid" runat="server" Visible="false">
                                                    <asp:Label ID="lbl_total" runat="server" Text="Label"></asp:Label>
                                                    <asp:GridView ID="grvExcelData" class="table table-striped table-bordered dataTable" runat="server" CssClass="table table-bordered" Width="100%">
                                                    </asp:GridView>
                                                    <div class="col-4">
                                                        <asp:Button ID="btn_final_submit" OnClick="btn_final_submit_Click" runat="server" Text="Final Submit" OnClientClick='return confirm("Are you sure want to save all ?")' CssClass="btn btn-primary" Style="margin: 0px 0px 0px 0px; padding: 6px 10px;" />
                                                    </div>
                                                </asp:Panel>



                                                <div class="texbox-border">
                                                    <div runat="server" visible="false" id="grid111">
                                                        <asp:GridView ID="GrdView" runat="server" class="table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="img-dv">
                                                                            <img src="<%#Eval("Student_img") %>" class="att-imgs" />
                                                                        </div>
                                                                        <div class="contnt-dv">
                                                                            <asp:Label ID="lbl_FullName" runat="server" class="att-name" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_reg_id" runat="server" class="att-adm_no" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_roll_no" runat="server" class="att-roll" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </div>
                                                                        <div class="action-dv">
                                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" class="rdobtnS" OnDataBound="RadioButtonList1_DataBound"></asp:RadioButtonList>
                                                                        </div>
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
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
