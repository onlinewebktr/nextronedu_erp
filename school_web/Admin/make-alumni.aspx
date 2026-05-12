<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="make-alumni.aspx.cs" Inherits="school_web.Admin.make_alumni" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Make Alumni
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .hdr-bg {
            background: #39e4ff !important;
            font-size: 14px !important;
            font-weight: 500 !important;
            color: #000;
            padding: 5px 5px;
        }

        .modal {
            background: rgb(0 0 0 / 55%);
        }

        .okay-butn {
            margin: 8px 10px 10px 0px;
            padding: 4px 8px 4px 8px;
            background: #177fff;
            color: #fff;
            font-size: 13px;
            border-radius: 4px;
            font-weight: 500;
            line-height: 20px;
            float: right;
            border: 1px solid #d1d1d1;
            text-decoration: none;
        }

            .okay-butn:hover {
                background: #177fff;
                color: #fff;
            }

        .not-okay-butn {
            margin: 8px 0px 10px 0px;
            padding: 4px 10px 4px 10px;
            background: #fff;
            color: #177fff;
            font-size: 13px;
            border-radius: 4px;
            font-weight: 400;
            line-height: 20px;
            float: right;
            border: 1px solid #d1d1d1;
        }

            .not-okay-butn:hover {
                background: #fff;
                color: #177fff;
            }
    </style>

    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .home-grph-wpr {
            width: 114%;
        }
    </style>
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
                <div class="breadcrumb-title pe-3">Student Update</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Make Alumni</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <div class="col-sm-2" style="display: none">
                <label for="validationCustom01" class="find-dv-lbl">Admission Type</label>
                <asp:DropDownList ID="ddl_days_hostel" runat="server" class="form-control find-dv-txtbx">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem Value="1">Hostel</asp:ListItem>
                    <asp:ListItem Value="2">Day Scholar</asp:ListItem>
                    <asp:ListItem Value="3">Day Boarding with Lunch</asp:ListItem>
                    <asp:ListItem Value="4">Bus</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-1" style="display: none">
                <asp:Button ID="Button1" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_by_days_Click" />
            </div>

            <div class="row" data-ng-app="ComplainApp" data-ng-controller="ComplainAppCtrl">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Make Alumni</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
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
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Student Type</label>
                                                        <asp:DropDownList ID="ddl_studenttype" runat="server" class="form-control find-dv-txtbx">
                                                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                            <asp:ListItem Value="New">New Admission</asp:ListItem>
                                                            <asp:ListItem Value="NT">Old Admission</asp:ListItem>
                                                            <asp:ListItem Value="Transferred">Transferred to Next Session</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>



                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 20px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 20px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>







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
                                                                    <span style="font-size: 14px; font-weight: bold;">Make Alumni Student
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <table class="table-bordered" style="width: 100%">
                                                                        <tr>
                                                                            <th class="hdr-bg">Total Student :
                                        <asp:Label ID="lbl_total_student" runat="server"></asp:Label>
                                                                            </th>
                                                                            <th class="hdr-bg">Total Readmission : 
                                    <asp:Label ID="lbltotal_readmission" runat="server">0</asp:Label>
                                                                            </th>
                                                                            <th class="hdr-bg">Total New Admission : 
                                  <asp:Label ID="lbl_newadmission" runat="server">0</asp:Label>
                                                                            </th>
                                                                            <th class="hdr-bg">Total Transferred To Next Session : 
                                  <asp:Label ID="lbl_total_trasfer_tonextsession" runat="server">0</asp:Label>
                                                                            </th>
                                                                        </tr>
                                                                    </table>

                                                                    <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th>Adm. No.</th>
                                                                                <th>Student Name</th>
                                                                                <th>Adm. Date</th>
                                                                                <th>Current Session Date</th>
                                                                                <th>Admission Type</th>
                                                                                <th>Class</th>
                                                                                <th>Session</th>
                                                                                <th>Section</th>
                                                                                <th>Roll No.</th>
                                                                                <th>Father's Name</th>
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
                                                                                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_Old_Admission_Date" runat="server" Text='<%#Bind("Old_Admission_Date")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="Label4" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_admissiontype" runat="server" Text='<%#Bind("Admission_Type")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td style="text-align: left;">
                                                                                            <asp:LinkButton ID="lnk_make_alimni" OnClick="lnk_make_alimni_Click" Style="background-color: #e14eca; color: #fff; padding: 2px 5px 2px 5px; width: auto; border-radius: 2px; font-weight: 500; display: inherit;" runat="server">Proceed</asp:LinkButton>
                                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                            <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
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
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
    <!--end page wrapper -->


    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 11px 10px 9px 10px;">
                    <h5 class="modal-title" style="float: left; width: 100%;">Update with Alumni</h5>
                </div>
                <div class="modal-body" style="padding: 2px 10px; border-top: 1px solid #ddd;">
                    <p class="form-txtbx-p">Remark</p>
                    <asp:TextBox ID="txt_remark" runat="server" class="form-control" TextMode="MultiLine" Style="border: 1px solid rgba(29,37,59,.5); padding: 5px 5px; border-radius: 4px;"></asp:TextBox>


                    <asp:LinkButton ID="lbk_close" runat="server" class="not-okay-butn">Close</asp:LinkButton>
                    <asp:LinkButton ID="lnk_submit_alumni" runat="server" class="okay-butn" OnClick="lnk_submit_alumni_Click">Submit</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        //=========================================
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
</asp:Content>
