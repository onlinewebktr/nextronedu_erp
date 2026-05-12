<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="add-subject.aspx.cs" Inherits="school_web.Admin.add_subject" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Subjects
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

        .modal.fade .modal-dialog {
            transform: translate(0, 0px);
        }

        .modal {
            background: rgb(0 0 0 / 75%);
        }

        .chkstle {
            display: block;
            position: relative;
            padding: 6px 5px 4px 5px;
            margin-bottom: 6px;
            cursor: pointer;
            font-size: 13px;
            float: left;
            border: 1px solid #ddd;
            background: #f5f5f5;
        }
    </style>
    <script type="text/javascript">
        function openModalPwd() {
            $('#myModal').modal('hide');
            $('#myModalPwd').modal('show');
        }

        function openModal() {
            $('#myModal').modal('show');
        }


        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);



        
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
                <div class="breadcrumb-title pe-3">Subject Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Subjects</li>
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
                                                        <asp:ListItem>ALL</asp:ListItem>
                                                        <asp:ListItem>Scholastic</asp:ListItem>
                                                        <asp:ListItem>Co-Scholastic</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>


                                                <div class="col-sm-6">
                                                    <div style="margin: 0px; padding: 0px; float: left; height: 50px; width: 100%;">
                                                        <a onclick="openModal()">
                                                            <img src="../assets/images/add_subject.png" style="height: 50px; width: 50px; float: right;" /></a>

                                                        <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 19px 0px 6px 0px" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn"><i class='bx bx-download'></i>Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 19px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                            ToolTip="Print">
                                                                <i class='bx bx-printer'></i>
                                                        </asp:LinkButton>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-12">
                                            <div id="tblPrintIQ" runat="server">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Class Name</th>
                                                                <th>Subject Id</th>
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
                                                                                <asp:Label ID="lbl_subject_ids" runat="server" Text='<%#Bind("Subject_id")%>'></asp:Label>
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
                                                                                <asp:Label ID="lbl_midetry" runat="server"></asp:Label>

                                                                                <asp:Label ID="lbl_Is_mandatory" runat="server" Text='<%#Bind("Is_mandatory")%>' Visible="false"></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"><i class="lni lni-pencil-alt"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"></i></asp:LinkButton>
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



    <div class="modal fade" id="myModalPwd" role="dialog">
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

    <!-------popupadd year----->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 5px 16px;">
                    <h5 class="modal-title">Add Subject</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation" novalidate="">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label Llabel">Class</label>
                                <span class="chkbx-all">
                                    <asp:CheckBox ID="chk_all" runat="server" Text="Select All" OnCheckedChanged="chk_all_CheckedChanged" AutoPostBack="true" /></span>
                                <br />
                                <asp:Repeater ID="rd_class" runat="server" OnItemDataBound="rd_class_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_class" class="chkstle" runat="server" Text='<%#Eval("Course_Name") %>' />
                                        <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("course_id")%>'></asp:Label>
                                        <asp:Label ID="lbl_course_name" runat="server" Visible="false" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:Repeater>
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
                            <div class="col-md-4">
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


    


    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_subjectid" runat="server" />
</asp:Content>
