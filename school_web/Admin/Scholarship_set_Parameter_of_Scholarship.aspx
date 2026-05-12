<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Scholarship_set_Parameter_of_Scholarship.aspx.cs" Inherits="school_web.Admin.Scholarship_set_Parameter_of_Scholarship" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Set Parameter of Scholarship Program

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
    <script type="text/javascript">
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
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .modal {
            background: rgb(0 0 0 / 52%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translate(0, 0px);
            }

        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 600px;
            }
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
                <div class="breadcrumb-title pe-3">Scholarship</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Parameter of Scholarship Program </li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">



                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase" style="position: relative">Parameter of Scholarship Program List 
                        <a href="Scholarship_Add_Program_Parameter.aspx" class="add-forpopup-btn"><i class="bx bx-upload"></i>Add Scholarship Program Parameter</a></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-md-3">
                                            <label for="validationCustom01" class="find-dv-lbl">Scholarship Program Name</label>
                                            <asp:DropDownList ID="ddl_test_name" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_test_name_SelectedIndexChanged"></asp:DropDownList>
                                        </div>


                                        <div class="col-md-7" style="text-align: right">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:LinkButton ID="btn_excels" Visible="false" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                            <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>

                                        </div>


                                        <div class="col-sm-12">

                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr">


                                                    <div class="pgslry-head-div head" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">

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

                                                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                <span style="font-size: 14px; font-weight: bold;">Scholarship Program List -<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                            </div>
                                                        </div>


                                                    </div>
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Session</th>
                                                                    <th>Scholarship Name</th>
                                                                    <th>Scholarship For</th>
                                                                    <th>Fee</th>
                                                                    <th>Start Date</th>
                                                                    <th>End Date</th>

                                                                    <th>Maximu Application Form Allowed</th>
                                                                    <th>Payment Mode</th>
                                                                    <th>Is Published</th>
                                                                    <th></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Test_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_course_Name" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Course_Fee" runat="server" Text='<%#Bind("Fees")%>'></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Start_Date" runat="server" Text='<%#Bind("start_date")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_end_date" runat="server" Text='<%#Bind("end_date")%>'></asp:Label>
                                                                            </td>



                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_no_application" runat="server" Text='<%#Bind("no_application")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_isactivenew" runat="server" Text='<%#Bind("activestatus")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">

                                                                                <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                    <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                        href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                        <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                            <i class="bx bx-grid-horizontal"></i>
                                                                                        </div>
                                                                                    </a>
                                                                                    <ul class="dropdown-menu dropdown-menu-end">
                                                                                        <li>
                                                                                            <asp:LinkButton ID="lnkActive" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnkActive_Click"> <i class="bx bxs-key"></i><span>Active</span></asp:LinkButton>




                                                                                        </li>
                                                                                        <li>
                                                                                            <asp:LinkButton ID="lnkEdit" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"></i><span>Edit</span></asp:LinkButton>
                                                                                        </li>


                                                                                        <li>
                                                                                            <asp:LinkButton ID="lnk_delete" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnk_delete_Click" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');"> <i class="lni lni-trash"></i><span>Delete</span></asp:LinkButton>
                                                                                        </li>

                                                                                        <li>
                                                                                            <asp:LinkButton ID="lnk_view_Scholorship_Guidelines" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnk_view_Scholorship_Guidelines_Click" ToolTip="Details"> <i class="bx bxs-detail"></i><span>Scholorship Guidelines</span></asp:LinkButton>
                                                                                            <asp:Label ID="lbl_Scholorship_Guidelines" runat="server" Text='<%#Bind("Scholorship_Guidelines")%>' Visible="false"></asp:Label>
                                                                                        </li>

                                                                                        <li>
                                                                                            <asp:LinkButton ID="lnk_Scholorship_Benefit" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnk_Scholorship_Benefit_Click" ToolTip="Details"> <i class="bx bxs-detail"></i><span>Scholorship Benefit</span></asp:LinkButton>
                                                                                            <asp:Label ID="lbl_Scholorship_Benefit" runat="server" Text='<%#Bind("Scholorship_Benefit")%>' Visible="false"></asp:Label>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>


                                                                                <asp:Label ID="lbl_courseid" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Isactive" runat="server" Text='<%#Bind("Isactive")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_test_id" runat="server" Text='<%#Bind("Test_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_sessions_ids" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>' Visible="false"></asp:Label>
                                                                            </td>



                                                                        </tr>
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
        <!--end row-->
    </div>

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />

    <%--------------------------------------------------------------%>

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');

        }
    </script>
    <div class="modal fade" id="myModal" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 600px;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">
                        <asp:Label ID="lbl_data_heading" runat="server">

                        </asp:Label></h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="row g-3 needs-validation" novalidate="">
                        <div class="col-md-12">

                            <asp:Label ID="lbl_data" runat="server">

                            </asp:Label>
                        </div>

                        <div class="col-12">

                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClientClick="return close()" class="btn btn-dark" Visible="false" CausesValidation="false" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
