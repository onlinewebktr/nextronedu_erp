<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="create-dob-certificate.aspx.cs" Inherits="school_web.Admin.create_dob_certificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Create DOB Certificate
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'create-dob-certificate.aspx/GetRooPath',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });

        $(function () {
            var sessionid = $("#<%=ddl_session.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'create-dob-certificate.aspx/GetRooPathAdmNo',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });


        function openModalSTD() {
            $('#myModalStd').modal('show');
        } 

        function openModal() {
            $('#myModal').modal('show');
        }
    </script>




    <style>
        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
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
                <div class="breadcrumb-title pe-3"><a href="certificate-creation.aspx" runat="server" id="backbtns" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Certificate</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active">Create Birth Certificate</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Create Birth Certificate"></asp:Label>
                            <hr />
                            <div class="p-4 border rounded">
                                <div class="row" style="border-bottom: 1px solid #ddd; padding: 5px 0px 5px 0px;">
                                    <div class="col-xl-4">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Session</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-4 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-4 padd-lft-5">
                                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-xl-4 padd-lft-5">
                                                            <asp:Button ID="btn_find_session" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_session_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-xl-8" id="pendingforgeneratedata" runat="server" visible="false">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Pending Birth Certificate </h2>
                                            <div class="fnd-box-wpr-inr">

                                                <table id="example211" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Adm. No.</th>
                                                            <th>Student Name</th>
                                                            <th>Class</th>
                                                            <th>Section</th>
                                                            <th>Roll No.</th>

                                                            <th>Reply Date</th>
                                                            <th>Reply Remarks</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="Repeater2" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                    </td>


                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Reply_datetime1")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("Reply_remarks")%>' Style="word-break: break-all"></asp:Label>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">

                                                                        <asp:LinkButton ID="lnk_process" runat="server" Style="background-color: #e14eca; color: #fff; padding: 2px 5px 2px 5px; width: auto; border-radius: 2px; font-weight: 500; display: inherit;" OnClick="lnk_process_Click" CssClass="btnfindaa btnfindaa2a">Select </asp:LinkButton>




                                                                    </td>


                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>

                                            </div>
                                        </div>
                                    </div>



                                </div>
                                <div class="row" style="border-bottom: 1px solid #ddd; padding: 5px 0px 5px 0px;">
                                    <div class="col-xl-3">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Admission No.</label>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-xl-5">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Roll No.</h2>
                                            <div class="fnd-box-wpr-inr" style="padding: 5px 5px 0px 5px;">
                                                <div class="row">
                                                    <div class="col-xl-6 padd-rght-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Class</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                                    <asp:TextBox ID="txt_admission_no_student_wise" runat="server" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Roll No.</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:TextBox ID="txtrollnumber" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                            <div class="fnd-box-row-wpr">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <div class="fnd-box-row-wpr" style="margin: 2px 0px 3px 0px;">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Button ID="btnfind" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btnfind_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-4">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Student Name</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Student Name</label>
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:Button ID="btn_find_name" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_name_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>





                                <div class="table-responsive" style="margin: 10px 0px 0px 0px">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Student Name</th>
                                                            <th>Father's Name</th>
                                                            <th>Adm No.</th>
                                                            <th>Class</th>
                                                            <th>Section</th>
                                                            
                                                            <th>Roll No.</th>
                                                            <th>Session</th>
                                                            <th>Date of Admission</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_dateofadmission" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:LinkButton ID="lnkSelectStd" runat="server" CausesValidation="false" OnClick="lnkSelectStd_Click" Style="background: #f00; padding: 2px 5px 1px 5px; display: inline-flex; color: #fff; border-radius: 2px;"
                                                                            ToolTip="Create DOB Certificate"><i class="bx bx-certification"></i> Create</asp:LinkButton>

                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_adm_no" runat="server" Text='<%#Bind("admissionserialnumber")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_dob" runat="server" Text='<%#Bind("dob")%>' Visible="false"></asp:Label>

                                                                        <asp:Label ID="lbl_gcmid" runat="server" Text='<%#Bind("gcm_id")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
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
    <asp:HiddenField ID="hd_id" runat="server" />
    <!--end page wrapper -->



    <div class="modal fade" id="myModal" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Create DOB Certificate</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Admission No.</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:Label ID="lbl_admisn_no" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Student Name</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:Label ID="lbl_std_name" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Class </label>
                            </div>
                            <div class="col-sm-8">
                                <asp:Label ID="lbl_classss" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Date of Birth</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_dob" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Issue Date</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_issue_date" runat="server" class="form-control datepicker"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_create" runat="server" Text="Submit" OnClick="btn_create_Click" class="btn btn-primary" Style="width: auto !important;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <link href="../assets/css/modal-custom.css" rel="stylesheet" /> 
    <div class="modal fade" id="myModalStd" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 792px;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header" style="padding: 5px 15px;">
                    <h3 class="modal-title" style="font-size: 20px;">Student Details</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div style="float: left; width: 100%; overflow: auto">
                        <table style="width: 100%;" id="Table1" class="table table-hover table-bordered ">
                            <tr>
                                <th>Student Name</th>
                                <th>Admission No</th>
                                <th>Class</th>
                                <th>Section</th>
                                <th>Roll</th>
                                <th>Session</th>
                                <th>Father's Name</th>
                                <th>Action</th>
                            </tr>
                            <asp:Repeater ID="rp_std" runat="server">
                                <ItemTemplate>
                                    <tr id="row" runat="server">
                                        <td>
                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbladmissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername") %>'></asp:Label>
                                            <asp:Label ID="lbl_session" Visible="false" runat="server" Text='<%#Bind("session") %>'></asp:Label>
                                            <asp:Label ID="lbl_session_id" Visible="false" runat="server" Text='<%#Bind("Session_id") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnk_select" runat="server" OnClick="lnk_select_Click">Select</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>






    <style>
        .conf-btn-ul li a {
            margin: 0px 5px;
            padding: 0px 0px 1px;
            text-decoration: none;
            background: #ff0000;
            color: #fff;
            width: 50px;
            float: right;
            text-align: center;
            border-radius: 3px;
            font-size: 13px;
            line-height: 25px;
            font-weight: 500;
        }
    </style>


</asp:Content>
