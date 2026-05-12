<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="subject-setup.aspx.cs" Inherits="school_web.Examination_Admin.subject_setup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Subject Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
       <style>
        #notification {
            margin: 0px;
            padding: 0px;
            position: relative;
            z-index: 99999;
            /* float: left; */
            width: 100%;
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
                <div class="breadcrumb-title pe-3">Exam Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Subject Setup</li>
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
                                    <div class="row">


                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-3" id="storeDv" runat="server">
                                                    <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Class Name</label>
                                                    <asp:DropDownList ID="ddl_course_search" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_course_search_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                                 <div class="col-sm-3" id="Div1" runat="server">
                                                    <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Subject Type</label>
                                                    <asp:DropDownList ID="ddl_course_type" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_course_type_SelectedIndexChanged">
                                                        <asp:ListItem>All</asp:ListItem>
                                                         <asp:ListItem>Scholastic</asp:ListItem>
                                                         <asp:ListItem>Co-Scholastic</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>


                                                <div class="col-sm-6">
                                                    <div style="margin: 0px; padding: 0px; float: left; height: 50px; width: 100%;">
                                                        <a onclick="openModal()">
                                                            <img src="../assets/images/add_subject.png" style="height: 50px; width: 50px; float: right;" /></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-12">
                                            <table id="example2" data-page-length='100000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>

                                                    <tr>
                                                        <th>#</th>
                                                        <th>Class Name</th>
                                                        <th>Subject Name</th>
                                                        <th>Short Name</th>
                                                        <th>Subject Code</th>
                                                        <th>Subject Type</th>
                                                        <th>Mandatory</th>

                                                        <th>Action</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                                                    </td>
                                                                    

                                                                     <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Subject_Short_Name" runat="server" Text='<%#Bind("Subject_Short_Name")%>'></asp:Label>
                                                                    </td>

                                                                     <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Subject_Code" runat="server" Text='<%#Bind("Subject_Code")%>'></asp:Label>
                                                                    </td>



                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Subject_type" runat="server" Text='<%#Bind("Subject_Type_Scholastic_Co_Scholastic")%>'></asp:Label>
                                                                    </td>

                                                                     <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_midetry" runat="server"  ></asp:Label>
                                                                          <asp:Label ID="lbl_midetry2" runat="server" Text='<%#Bind("Is_mandatory")%>' Visible="false"></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                        <asp:Label ID="lbl_Subject_id" runat="server" Text='<%#Bind("Subject_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("course_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Subject_position" runat="server" Text='<%#Bind("Subject_position")%>' Visible="false"></asp:Label>
                                                                         <asp:Label ID="lbl_Subject_Type_Scholastic_Co_Scholastic" runat="server" Text='<%#Bind("Subject_Type_Scholastic_Co_Scholastic")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </asp:Panel>
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

    <%--------------------------------------------------------------%>

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
    <!-------popupadd year----->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Subject Details</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">

                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation" novalidate="">
                            <div class="col-md-4">
                                <label for="validationCustom01" class="form-label">Class Name <sup>* </sup></label>

                                <asp:DropDownList ID="ddl_course" runat="server"  class="form-select">
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-4">
                                <label for="validationCustom01" class="form-label">Subject Name <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_subject"></asp:RequiredFieldValidator></sup></label>
                                <asp:TextBox ID="txt_subject" runat="server" class="form-control"></asp:TextBox>

                            </div>
                            <div class="col-md-4">
                                <label for="validationCustom01" class="form-label">Subject Code  </label>
                                <asp:TextBox ID="txt_sujectcode" runat="server" class="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label for="validationCustom01" class="form-label">Subject Short Name  </label>
                                <asp:TextBox ID="txt_sujectshortname" runat="server" class="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label for="validationCustom01" class="form-label">Subject Type </label>
                                <asp:DropDownList ID="ddl_subjecttype" runat="server" class="form-select">
                                    <asp:ListItem>Scholastic</asp:ListItem>
                                    <asp:ListItem>Co-Scholastic</asp:ListItem>

                                </asp:DropDownList>
                            </div>



                            <div class="col-md-4"   >
                                <label for="validationCustom01" class="form-label">Is mandatory</label>
                                <label class="switch" style="margin: 31px 0px 0px 0px;">
                                    <asp:CheckBox ID="chk_mandatory" runat="server" Checked="false" />
                                    <span class="slider round"></span>
                                </label>
                            </div>






                            <div class="col-md-6" style="display: none">
                                <label for="validationCustom01" class="form-label">Subject Position(1,2,3)<sup>* </sup></label>
                                <asp:TextBox ID="txt_subjectposition" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>






                            <div class="col-12">
                                <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClientClick="return close()" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
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
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_subjectid" runat="server" />
</asp:Content>
