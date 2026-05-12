<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Parents_Student_mapping.aspx.cs" Inherits="school_web.Admin.Parents_Student_mapping" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Parents Mapping with Student
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        .buttons-print {
            display: none;
        }

        #notification {
            z-index: 99999999999 !important;
        }

        .find-dv-lbl {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 12px;
            color: #000;
        }

        .logo-icon {
            width: 54px;
            height: 55px;
        }

        img {
            height: 13px;
            width: 13px;
            margin-right: 12px;
        }

        .btn-group, .btn-group-vertical {
            position: relative;
            display: inline-flex;
            vertical-align: middle;
            display: none;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=hd_session_id.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'bill-delete.aspx/GetRooPath',
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
            var sessionid = $("#<%=hd_session_id.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'bill-delete.aspx/GetRooPathAdmNo',
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
    </script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
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
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .modal-open .modal {
            background: rgb(0 0 0 / 48%);
        }

        @media print {
            .noPrint {
                display: none;
            }
        }
    </style>

    <script>
        $(document).ready(function () {
            /* $("#datatable").dataTable().fnDestroy();*/
            $('#datatable').dataTable({
                //stateSave: true,
                //bDestroy: true,
                destroy: true,
            });
        }
          //$(document).ready(function () {
          //    // var table = new DataTable('#example2');

          //    //$('#example2').on('click', function () {
          //    //    table.destroy();
          //    //});

          //    // $('#"bDestroy": true').DataTable().destroy();
          //    if ($.fn.DataTable.isDataTable('#example2')) {
          //        $('#example2').DataTable().clear().destroy();
          //    }

          //    var table = $('#example2').DataTable({
          //        retrieve: true,
          //        lengthChange: false,
          //        paging: true,
          //        buttons: ['copy', 'excel', 'pdf', 'print'],
          //         bDestroy: true,
          //    });

          //    table.buttons().container()
          //        .appendTo('#example2_wrapper .col-md-6:eq(0)');
          //});
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_session_id" runat="server" />
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
                <div class="breadcrumb-title pe-3">User Permission</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Parents Mapping with Student</li>
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

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="find-dv">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <label for="validationCustom01" class="find-dv-lbl" style="width: auto;">Status</label>
                                            <asp:DropDownList ID="ddl_status" runat="server" class="form-control find-dv-txtbx">
                                                <asp:ListItem>ALL</asp:ListItem>
                                                <asp:ListItem>Active</asp:ListItem>
                                                <asp:ListItem>Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>



                                        <div class="col-sm-5">
                                            <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                        </div>


                                        <div class="col-sm-3">
                                            <asp:LinkButton ID="btn_excels" Visible="false" runat="server" Style="margin: 20px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                            <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" Style="margin: 20px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                            <%--<a href="#" class="btn btn-success find-dv-btn" style="margin: 20px 7px 1px 0px !important; float: right; padding: 3px 6px 6px 11px; font-size: 14px;" title="Add Enquiry" data-toggle="modal" data-target="#myModal1"><i class="bx bx-plus-medical"></i> Add Parent</a>--%>
                                            <a href="#!" data-toggle="modal" data-target="#myModal3" class="button-29" style="float: right; position: absolute; right: 15px;"><span class="material-symbols-outlined" style="margin: 0px 5px 0px 0px; font-size: 18px;">escalator_warning</span>Add Parent</a>

                                            <asp:LinkButton ID="lnk_open_parents_mapping" runat="server" Style="float: right; position: absolute; right: 15px;" OnClick="lnk_open_parents_mapping_Click" class="button-29"> <span class="material-symbols-outlined" style="margin: 0px 5px 0px 0px; font-size: 18px;">escalator_warning</span>Add Parent  </asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="grd-wpr">
                                <div id="tblPrintIQ" runat="server">
                                    <div class="prnt-dv-wpr">
                                        <div id="content">
                                            <div class="pgslry-head-div head">
                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                    <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                </div>
                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                    <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                        <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                                    </h1>

                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                                    </div>
                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                    </div>
                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                    </div>
                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        <span style="font-size: 14px; font-weight: bold;">Student List-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                    </div>
                                                </div>


                                            </div>

                                            <asp:Panel ID="Panel1" runat="server">
                                                <table id="datatable" data-page-length='50' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Parents Name</th>
                                                            <th>Mobile No.</th>
                                                            <th>Student Name</th>
                                                            <th>Student Adm. No.</th>
                                                            <th>Parents User Id</th>
                                                            <th>Password</th>
                                                            <th>Status</th>
                                                            <%if (this.IsChecked)
                                                                { %>
                                                            <th class="noPrint"></th>
                                                            <%}%>
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
                                                                        <td>
                                                                            <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lbl_Phone" runat="server" Text='<%#Bind("Mobile")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_student_admission_no" runat="server" Text='<%#Bind("Student_id")%>'></asp:Label>
                                                                        </td>


                                                                        <td>
                                                                            <asp:Label ID="lbl_userid" Style="word-break: break-all" runat="server" Text='<%#Bind("User_id")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lbl_Password" runat="server" Text='<%#Bind("Password")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_status" Style="padding: 4px;" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                                        </td>
                                                                        <%if (this.IsChecked)
                                                                            { %>
                                                                        <td class="noPrint">

                                                                            <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                    href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                    <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                        <i class="bx bx-grid-horizontal"></i>
                                                                                    </div>
                                                                                </a>
                                                                                <ul class="dropdown-menu dropdown-menu-end">


                                                                                    <li>
                                                                                        <asp:LinkButton ID="lnkEdit" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"><i class="lni lni-pencil-alt"></i><span>Edit</span></asp:LinkButton>
                                                                                    </li>
                                                                                    <li>
                                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click" class="dropdown-item"><i class="lni lni-trash"></i>Delete</asp:LinkButton>
                                                                                    </li>


                                                                                    <li>
                                                                                        <asp:LinkButton ID="lnk_student_view" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnk_student_view_Click" ToolTip="View"><i class="lni lni-eye"></i><span>View or Map More Student</span></asp:LinkButton>
                                                                                    </li>
                                                                                    <li>

                                                                                        <asp:LinkButton ID="lnk_active_inactive" runat="server" ToolTip="Active/Inactive" CausesValidation="false" OnClick="lnk_active_inactive_Click" class="dropdown-item">
                                                                                                 
                                                                                                <i class="lni lni-eye"></i><span>Active</span></asp:LinkButton>
                                                                                    </li>
                                                                                    <asp:Label ID="lbl_User_id" runat="server" Text='<%#Bind("User_id")%>' Visible="false"></asp:Label>

                                                                                </ul>
                                                                            </div>
                                                                        </td>
                                                                        <%} %>
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




    <script language="Javascript">
       
    function isNumberKey(evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode != 46 && charCode > 31
                    && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }

    </script>


    <script language="Javascript">

        //function isvaliduserid(evt) {
        //    alert(evt);
        //    var emailInput = evt;
        //    var emailPattern = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
        //    if (emailPattern.test(emailInput)) {
        //        alert('Please enter a valid user id ');
        //        return false;
        //    }
        //    return true;

        //}

    </script>

    <style>
        .mdl-frm-row {
            margin: 2px 0px 6px 0px !important;
        }

        .col-eq {
            flex: 1;
            background: #f4f4f4;
        }

        .taskside {
            padding: 10px;
        }

            .taskside h4 {
                margin-bottom: 0;
                font-size: 18px;
            }

        .taskseparator {
            border-top: 1px solid #e6e6e6;
            -webkit-box-shadow: 0 1px 6px 1px rgba(255,255,255,.5);
            box-shadow: 0 1px 6px 1px rgba(255,255,255,.5);
        }

        .taskside hr {
            margin-top: 9px;
            margin-bottom: 9px;
            border: 0;
            border-top: 1px solid #655f5f !important;
        }

        .task-info {
            float: left;
            width: 100%;
        }

            .task-info h5 {
                font-size: 13px;
            }

        .text-dark {
            font-weight: bold;
        }

        .fa.pull-left {
            margin-right: 0.3em;
        }

        .task-info h5 span {
        }
    </style>
    <link href="../Autocomplete/amsify.suggestags.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery.amsify.suggestags.js"></script>



    <div class="modal fade" id="myModal2" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 1050px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Student Details</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="cardsp">
                            <h2>Parent Information</h2>
                            <div class="row">
                                <div class="col-sm-4">
                                    <p>
                                        <strong>Parent Name : </strong>
                                        <asp:Label ID="lbl_parntname" runat="server"></asp:Label>
                                    </p>
                                </div>
                                <div class="col-sm-3">
                                    <p>
                                        <strong>Mobile No : </strong>
                                        <asp:Label ID="lbl_parent_mb_no" runat="server"></asp:Label>
                                    </p>
                                </div>
                                <div class="col-sm-3">
                                    <p>
                                        <strong>User ID : </strong>
                                        <asp:Label ID="lbl_user_id_parent" runat="server"></asp:Label>
                                    </p>
                                </div>
                                <div class="col-sm-2">
                                    <p>
                                        <strong>Password : </strong>
                                        <asp:Label ID="lbl_password_parent" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>


                        <div class="cardsp" style="background-color: #e7ffdd; border: 1px solid #c1e1b3;">
                            <h2>Student Information</h2>
                            <div style="width: 100%; float: left; overflow: auto">
                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Student Name</th>
                                            <th>Admission No.</th>
                                            <th>Course</th>
                                            <th>Section</th>
                                            <th>Roll No.</th>
                                            <th>Session</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_course" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_rollno" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>



                        <div class="cardsp" style="background-color: #c4ffef; border: 1px solid #7ce5c9;">
                            <h2 style="border-bottom: 1px solid #7ce5c9;">Map More Students to this Parent</h2>
                            <div style="width: 100%; float: left; overflow: auto">
                                <div class="row">
                                    <div class="col-xl-6">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h" style="padding: 4px 5px 3px 5px; font-size: 15px;">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-7">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Admission No.</label>
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-xl-3">
                                                            <asp:Button ID="btn_find_admission_no" OnClick="btn_find_admission_no_Click" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" Style="margin: 22px 0px 0px 0px;" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-6">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h" style="padding: 4px 5px 3px 5px; font-size: 15px;">Find by Student Name</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-7">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Student Name</label>
                                                            <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-xl-3">
                                                            <asp:Button ID="btn_find_by_name" OnClick="btn_find_by_name_Click" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" Style="margin: 22px 0px 0px 0px;" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div style="width: 100%; float: left; overflow: auto">
                                    <table id="example211d" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Student Name</th>
                                                <th>Father Name</th>
                                                <th>Mobile No.</th>
                                                <th>Admission No.</th>
                                                <th>Class</th>
                                                <th>Section</th>
                                                <th>Roll No.</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rp_add_more_std" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lbl_mobile_no" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblfather_name" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_course" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_rollno" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <asp:CheckBox ID="chkRowData" runat="server" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>

                                <div class="col-sm-12" style="text-align: right;">
                                    <asp:Button ID="btn_map_more_std" Visible="false" OnClick="btn_map_more_std_Click" runat="server" Style="float: right; margin-bottom: 10px;"
                                        Text="Save" class="btn btn-success" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        .cardsp {
            width: 100%;
            padding: 10px;
            margin: 0px 0px 10px 0px;
            float: left;
            background-color: #f7ffd6;
            border: 1px solid #d8ef7b;
            border-radius: 3px;
            box-shadow: 0 4px 12px rgb(0 0 0 / 3%);
        }

            .cardsp h2 {
                margin: 0px 0px 5px 0px;
                color: #333;
                padding: 0px 0px 3px 0px;
                font-size: 17px;
                border-bottom: 1px solid #d8ef7b;
            }

            .cardsp p {
                margin: 8px 0;
                color: #555;
                font-size: 13px;
            }

        .modal-body {
            padding: 10px 10px;
        }

        .modal-header {
            padding: 5px 10px;
        }

        .ui-widget-content {
            z-index: 9999999 !important;
        }
    </style>


    <div class="modal fade" id="myModal3" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 1050px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">
                        <asp:Label ID="lbl_heading_name" runat="server" Text='Mapping Parents With Student'></asp:Label></h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-2">
                                <label for="validationCustom01" class="find-dv-lbl">
                                    Find Mobile No.<sup>  </sup>
                                </label>
                            </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txt_find_mobile_no" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btn_find_mobile_no" OnClick="btn_find_mobile_no_Click" runat="server" Style="float: left; margin-bottom: 10px;"
                                    Text="Find" class="btn btn-success" />
                            </div>
                        </div>

                        <div class="row" id="studentdetails" runat="server" visible="false">
                            <div class="col-sm-12">
                                <table id="example211" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Parents Name</th>
                                            <th>Parents Mobile No.</th>
                                            <th>Student Name</th>
                                            <th>Admission No.</th>
                                            <th>Class</th>
                                            <th>Section</th>
                                            <th>Roll No.</th>
                                            <th style="padding: 7px 0px 0px 0px; text-align: center;">
                                                <asp:CheckBox ID="chkAll" runat="server" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater2_parents" runat="server" OnItemDataBound="Repeater2_parents_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblfather_name" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_mobile_no" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_course" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_rollno" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                    </td>

                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="chkRowData" runat="server" /></td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <script type="text/javascript">
                                                    $(document).ready(function () {
                                                        // CHECK-UNCHECK ALL CHECKBOXES IN THE REPEATER 
                                                        // WHEN USER CLICKS THE HEADER CHECKBOX.
                                                        $('table [id*=chkAll]').click(function () {
                                                            if ($(this).is(':checked'))
                                                                $('table [id*=chkRowData]').prop('checked', true)
                                                            else
                                                                $('table [id*=chkRowData]').prop('checked', false)
                                                        });

                                                        // NOW CHECK THE HEADER CHECKBOX, IF ALL THE ROW CHECKBOXES ARE CHECKED.
                                                        $('table [id*=chkRowData]').click(function () {

                                                            var total_rows = $('table [id*=chkRowData]').length;
                                                            var checked_Rows = $('table [id*=chkRowData]:checked').length;

                                                            if (checked_Rows == total_rows)
                                                                $('table [id*=chkAll]').prop('checked', true);
                                                            else
                                                                $('table [id*=chkAll]').prop('checked', false);
                                                        });
                                                    });
                            </script>
                            <div class="col-sm-12" style="text-align: right;">
                                <asp:Button ID="btn_map_new" OnClick="btn_map_new_Click" runat="server" Style="float: right; margin-bottom: 10px;"
                                    Text="Save" class="btn btn-success" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:HiddenField ID="hd_userid" runat="server" />
    <asp:HiddenField ID="hdKeyID" runat="server" />
    <asp:HiddenField ID="HdID" runat="server" />
    <script type="text/javascript">
        function openModal2() {
            $('#myModal2').modal('show');
        }
        function openModal3() {
            $('#myModal3').modal('show');
        }
    </script>
</asp:Content>
