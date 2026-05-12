<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="hostel_student_assigned_report.aspx.cs" Inherits="school_web.Admin.hostel_student_assigned_report" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Hostel Assigned Student List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/Angular/angular.min.js"></script>
    <style>
        .modal-dialog {
            max-width: 700px;
            top: 30px;
        }

        .modal {
            background: rgb(0 0 0 / 31%);
        }

        .GreenBtnS {
            background: #20cd00;
            color: #fff;
            padding: 3px 5px 2px 5px;
            border-radius: 3px;
        }

        .RedsBtnS {
            background: #f30000;
            color: #fff;
            padding: 3px 5px 2px 5px;
            border-radius: 3px;
        }

        #notification {
            z-index: 999999;
        }

        .head {
            display: none;
        }
    </style>

    <script type="text/javascript">
        function openModalCause() {
            $('#mdl_confirm').modal('show');
        }
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
                <div class="breadcrumb-title pe-3">Hostel</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Hostel Hostel Assigned Student List</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row" data-ng-app="HostelApp" data-ng-controller="HostelHistoryAppCtrl">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Hostel</label>
                                                        <asp:DropDownList ID="ddl_hostel" runat="server" class="form-control form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_hostel_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Room Category</label>
                                                        <asp:DropDownList ID="ddl_room_cat" runat="server" class="form-select"></asp:DropDownList>
                                                    </div>



                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>


                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>

                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 19px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>

                                                    </div>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hd_id" runat="server" />
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
                                                                        <span style="font-size: 14px; font-weight: bold;">Hostel Assigned List
                                                                            <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                    </div>
                                                                </div>


                                                            </div>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info" data-page-length='1500'>
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Action</th>
                                                                            <th>#</th>
                                                                            <th>Assign Date</th>
                                                                            <th>Hostel Name</th>
                                                                            <th>Admission No.</th>
                                                                            <th>Class</th>
                                                                            <th>Section</th>
                                                                            <th>Session</th>
                                                                            <th>Roll No.</th>
                                                                            <th>Student Name</th> 
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <tr>
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
                                                                                                    <a data-ng-click="btnFndByOrderNo('<%# Eval("Hostel_assign_id") %>')" href="#!" data-toggle="modal" data-target="#myModal" class="dropdown-item"><i class="bx bx-building-house"></i><span>View Hostel Details</span></a>
                                                                                                </li>
                                                                                                <li>
                                                                                                    <asp:LinkButton ID="lnkEdit" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"></i><span>Edit Hostel Details</span></asp:LinkButton>
                                                                                                </li>
                                                                                                <li>
                                                                                                    <asp:LinkButton ID="lnk_delete" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnk_delete_Click" ToolTip="Delete"> <i class="lni lni-trash"></i><span>Remove</span></asp:LinkButton>
                                                                                                </li>
                                                                                            </ul>
                                                                                        </div>
                                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                                          <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:LinkButton ID="lnk_status" Visible="false" runat="server" OnClick="lnk_status_Click"></asp:LinkButton>
                                                                                        <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("HStatus")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_hostel_assign_id" runat="server" Text='<%#Bind("Hostel_assign_id")%>' Visible="false"></asp:Label>

                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_dateofadmission" runat="server" Text='<%#Bind("Created_date1")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("Hostel_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_admissionserialnumber1" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                    </td>


                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
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




                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="padding: 3px 17px;">
                                    <h5 class="modal-title">Hostel Details</h5>
                                    <a href="#!" data-dismiss="modal" class="btn btn-secondary">Close</a>
                                </div>
                                <div class="modal-body">
                                    <div class="p-4 border rounded" data-ng-repeat="x in hstlHistry">
                                        <div class="row g-3 needs-validation">
                                            <table id="Table1" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <tr>
                                                    <td>From Month : </td>
                                                    <td>{{x.From_month_name}}</td>
                                                </tr>
                                                <tr>
                                                    <td>Hostel Name : </td>
                                                    <td>{{x.Hostel_name}}</td>
                                                </tr>
                                                <tr>
                                                    <td>Roon Category : </td>
                                                    <td>{{x.Room_category_name}}</td>
                                                </tr>
                                                <tr>
                                                    <td>Roon No. : </td>
                                                    <td>{{x.Room_name}}</td>
                                                </tr>
                                                <tr>
                                                    <td>Bed No. : </td>
                                                    <td>{{x.Bed_name}}</td>
                                                </tr>
                                                <tr>
                                                    <td>Student Name : </td>
                                                    <td>{{x.Studentname}}</td>
                                                </tr>
                                                <tr>
                                                    <td>Admission No. : </td>
                                                    <td>{{x.Admission_no}}</td>
                                                </tr>
                                                <tr>
                                                    <td>Session : </td>
                                                    <td>{{x.Session}}</td>
                                                </tr>
                                                <tr>
                                                    <td>Class : </td>
                                                    <td>{{x.Class_name}}</td>
                                                </tr>
                                                <tr>
                                                    <td>Section : </td>
                                                    <td>{{x.Section}}</td>
                                                </tr>
                                                <tr>
                                                    <td>Roll No. : </td>
                                                    <td>{{x.rollnumber}}</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>





                    <div id="mdl_confirm" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <%--<div class="modal-header" style="padding: 3px 17px;">
                                    <h5 class="modal-title">Hostel Details</h5>
                                    <a href="#!" data-dismiss="modal" class="btn btn-secondary">Close</a>
                                </div>--%>
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

                </div>
            </div>
        </div>
        <!--end row-->
    </div>

    <script type="text/javascript">
        var app = angular.module('HostelApp', []);
        app.controller('HostelHistoryAppCtrl', function ($scope, $http) {

            //FindByOrderId 
            $scope.btnFndByOrderNo = function (AssignNo) {
                var paramiter1 = AssignNo;
                $http.get("graph.asmx/fetch_std_hostel_details", { params: { "Paramiter1": paramiter1 } }).then(function (response) {
                    $scope.hstlHistry = response.data;
                    //if ($scope.odrHistry == "") {
                    //    $("#orderHistoryTable").addClass("hidden");
                    //    $("#not_found").removeClass("hidden");
                    //}
                    //else
                    //{
                    //    $("#orderHistoryTable").removeClass("hidden");
                    //    $("#not_found").addClass("hidden");
                    //}
                })
            }
        });
    </script>

</asp:Content>
