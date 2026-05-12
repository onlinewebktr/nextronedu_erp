<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Transport_Taken_Students_List.aspx.cs" Inherits="school_web.Admin.Transport_Taken_Students_List" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Transport Mapped List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 24px;
            height: 26px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddlsession.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'fee-collection-monthly-wise.aspx/GetRooPathAdmNo',
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

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .modal {
            background: rgb(0 0 0 / 39%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translate(0, 0px);
            }

        .modal-header {
            padding: .5rem 1rem;
        }
    </style>

    <script type="text/javascript">
        function openModalRemove() {
            $('#myModal1').modal('show');
        }

        function openModalEdit() {
            $('#myModal').modal('show');
        }
    </script>
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
                <div class="breadcrumb-title pe-3">Transportation</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student Transport Mapped List</li>
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
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row g-3 needs-validation" novalidate="">
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                    <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="validationCustom01" class="find-dv-lbl">Boarding Point</label>
                                                    <asp:DropDownList ID="ddl_Transportation_Distance" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find_by_transportpath" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_transportpath_Click" />
                                                </div>
                                            </div>
                                            <div style="height: 2px; width: 100%; float: left"></div>
                                            <div class="row g-3 needs-validation" novalidate="">
                                                <div class="col-sm-3">
                                                    <label for="validationCustom01" class="find-dv-lbl">Vehicle</label>
                                                    <asp:DropDownList ID="ddl_vehicle" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find_by_vehicle" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_vehicle_Click" />
                                                </div>


                                                <div class="col-sm-3">
                                                    <label for="validationCustom01" class="find-dv-lbl">Admission No.</label>
                                                    <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Button ID="btn_find_by_adm_no" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_adm_no_Click" />
                                                </div>



                                                <div class="col-sm-3">
                                                    <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                </div>
                                            </div>



                                        </div>
                                        <div class="grd-wpr">
                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr printborder">

                                                    <div class="head-printdv" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">
                                                        <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                            <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                        </div>
                                                        <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                            <h1 style="margin: 10px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
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
                                                                <span style="font-size: 14px; font-weight: bold;">Transport Taken Student List<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:Panel ID="pnl_view" runat="server">
                                                        <table id="datatable" data-page-length='1500' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th class="hiddenOnPrint">Action</th>
                                                                    <th>Session</th>
                                                                    <th>Admission No.</th>
                                                                    <th>Class</th>
                                                                    <th>Section</th>
                                                                    <th>Roll No.</th>
                                                                    <th>Student Name</th>
                                                                    <th>Mobile No.</th>
                                                                    <th>Transport Fee</th>
                                                                    <th>From Month</th>
                                                                    <th>Vehicle Name</th>
                                                                    <th>Vehicle Route</th>
                                                                    <th>Seat Name</th>
                                                                    <th>Boarding Point</th>
                                                                    <th>Assign Date</th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_view" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td class="hiddenOnPrint">
                                                                                <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                    <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                        href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                        <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                            <i class="bx bx-grid-horizontal"></i>
                                                                                        </div>
                                                                                    </a>
                                                                                    <ul class="dropdown-menu dropdown-menu-end">
                                                                                        <li>
                                                                                            <asp:LinkButton ID="lnkEdit" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Remove"> <i class="lni lni-trash"></i><span>Remove</span></asp:LinkButton>
                                                                                        </li>


                                                                                        <li>
                                                                                            <asp:LinkButton ID="lnk_change" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnk_change_Click" ToolTip="Remove"> <i class="lni lni-pencil-alt"></i><span>Change</span></asp:LinkButton>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_month_id" runat="server" Text='<%#Bind("Month_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_transport_id" runat="server" Text='<%#Bind("transport_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Sheet_Id" runat="server" Text='<%#Bind("Sheet_Id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_TransportPath_id" runat="server" Text='<%#Bind("TransportPath_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_boarding_point_id" runat="server" Text='<%#Bind("Boarding_Point_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Transport_Assigned_Id" runat="server" Text='<%#Bind("Transport_Assigned_Id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Category_id" runat="server" Text='<%#Bind("Category_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_SubCategory_id" runat="server" Text='<%#Bind("SubCategory_id")%>' Visible="false"></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Admission_no" runat="server" Text='<%#Bind("Admission_no")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("mobilenumber")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("Transport_fee")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_monthname" runat="server" Text='<%#Bind("Month_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Bus_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Pathname" runat="server" Text='<%#Bind("Pathname")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_seatname" runat="server" Text='<%#Bind("seatname")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Boarding_Point" runat="server" Text='<%#Bind("Boarding_Point")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Created_date" runat="server" Text='<%#Bind("Created_date")%>'></asp:Label>
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
    <div id="myModal1" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Remove Remarks</h5>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
                <div class="modal-body">

                    <div class="p-4 border rounded">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label">Enter Reason<sup>*</sup></label>
                                <asp:TextBox ID="txt_reason" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>

                                <asp:Button ID="btn_conf_remove" Style="margin: 10px 10px 0px 0px;" OnClick="btn_conf_remove_Click" runat="server" Text="Submit" class="btn btn-danger find-dv-btn" />

                                <a href="#!" data-dismiss="modal" style="margin: 10px 10px 0px 0px;" class="btn btn-primary find-dv-btn">Close</a>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>
    <div id="fadeup1"></div>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 750px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Change Transport</h5>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation" novalidate="">
                            <div class="col-md-12" style="display: none">
                                <asp:RadioButton ID="rd_change_month_no" runat="server" Text="Change Month" GroupName="ab" Checked="true" />
                                <asp:RadioButton ID="rd_change_Changetransport" runat="server" Text="Change Transport" GroupName="ab" />
                                <asp:RadioButton ID="rd_change_both" runat="server" Text="Both" Checked="true" GroupName="ab" />
                            </div>
                            <div class="col-md-2">
                                <label for="validationCustom01" class="form-label">Month Name</label>
                                <asp:DropDownList ID="ddl_monthname" runat="server" class="form-select">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label for="validationCustom01" class="form-label">Vehicle Name</label>
                                <asp:DropDownList ID="ddl_bus_name" runat="server" class="form-select" OnSelectedIndexChanged="ddl_bus_name_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label for="validationCustom01" class="form-label">Transportation Route</label>
                                <asp:DropDownList ID="ddl_path_root" runat="server" class="form-select" OnSelectedIndexChanged="ddl_path_root_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label for="validationCustom01" class="form-label">Boarding Point</label>
                                <asp:DropDownList ID="ddl_boarding_point" runat="server" class="form-select">
                                </asp:DropDownList>
                            </div>
                            <div class="col-12">
                                <asp:Button ID="btn_Submit" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClientClick="return close()" class="btn btn-dark" Visible="false" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_Category_id" runat="server" />
    <asp:HiddenField ID="hd_SubCategory_id" runat="server" />
</asp:Content>
