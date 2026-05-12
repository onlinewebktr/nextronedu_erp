<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="grade-master.aspx.cs" Inherits="school_web.Admin.grade_master" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Grade Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
        .week-off-row {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .week-off-big-h {
            margin: 10px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            font-size: 20px;
            float: left;
            font-weight: 500;
            text-align: center;
        }

        .week-off-day-h {
            margin: 10px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            font-size: 17px;
            float: left;
            font-weight: 500;
            text-align: center;
        }

        .week-off-chkbx {
            width: auto;
            display: initial;
            margin: 0px 0px 0px 10px;
            font-weight: 500;
        }

        .modal-dialog {
            max-width: 300px;
        }
    </style>

    <style>
        /* SQUARED TWO */
        .squaredTwo {
            width: 28px;
            height: 28px;
            float: left;
            margin: 4px 13px 0px 0px;
            padding: 4px 0px 0px 6px;
            background: #fcfff4;
            background: -webkit-linear-gradient(top, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
            background: -moz-linear-gradient(top, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
            background: -o-linear-gradient(top, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
            background: -ms-linear-gradient(top, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
            background: linear-gradient(top, #fcfff4 0%, #dfe5d7 40%, #b3bead 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#fcfff4', endColorstr='#b3bead',GradientType=0 );
            -webkit-box-shadow: inset 0px 1px 1px white, 0px 1px 3px rgba(0,0,0,0.5);
            -moz-box-shadow: inset 0px 1px 1px white, 0px 1px 3px rgba(0,0,0,0.5);
            box-shadow: inset 0px 1px 1px white, 0px 1px 3px rgba(0,0,0,0.5);
            position: relative;
        }

            .squaredTwo label {
                cursor: pointer;
                position: absolute;
                width: 21px;
                height: 20px;
                left: 4px;
                top: 4px;
                -webkit-box-shadow: inset 0px 1px 1px rgb(0 0 0 / 50%), 0px 1px 0px rgb(255 255 255);
                -moz-box-shadow: inset 0px 1px 1px rgba(0,0,0,0.5), 0px 1px 0px rgba(255,255,255,1);
                box-shadow: inset 0px 1px 1px rgb(0 0 0 / 50%), 0px 1px 0px rgb(255 255 255);
                background: -webkit-linear-gradient(top, #cfcfcf 0%, #5c5c5c 100%);
                background: -moz-linear-gradient(top, #222 0%, #45484d 100%);
                background: -o-linear-gradient(top, #222 0%, #45484d 100%);
                background: -ms-linear-gradient(top, #222 0%, #45484d 100%);
                background: linear-gradient(top, #222 0%, #45484d 100%);
                filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#222', endColorstr='#45484d',GradientType=0 );
                background: #858585;
                color: #858585;
            }

                .squaredTwo label:after {
                    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
                    filter: alpha(opacity=0);
                    opacity: 0;
                    content: '';
                    position: absolute;
                    width: 12px;
                    height: 7px;
                    background: transparent;
                    top: 5px;
                    left: 5px;
                    border: 3px solid #fcfff4;
                    border-top: none;
                    border-right: none;
                    -webkit-transform: rotate(-45deg);
                    -moz-transform: rotate(-45deg);
                    -o-transform: rotate(-45deg);
                    -ms-transform: rotate(-45deg);
                    transform: rotate(-45deg);
                }

                .squaredTwo label:hover::after {
                    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=30)";
                    filter: alpha(opacity=30);
                    opacity: 0.3;
                }

            .squaredTwo input[type=checkbox]:checked + label:after {
                -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=100)";
                filter: alpha(opacity=100);
                opacity: 1;
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active">Grade Master</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Grade Master"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Grade Name<sup>*</sup></label>
                                        <asp:TextBox ID="txt_grade_name" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Nature of Work<sup>*</sup></label>
                                        <asp:TextBox ID="txt_nature_of_work" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Description<sup>*</sup></label>
                                        <asp:TextBox ID="txt_description" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label">Min Work. Hour<sup>*</sup></label>
                                                <asp:TextBox ID="txt_min_working_hour" MaxLength="2" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label">Max Work. Hour<sup>*</sup></label>
                                                <asp:TextBox ID="txt_max_working_hour" MaxLength="2" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Salary Calculation Based on<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_salary_calculation_method" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_salary_calculation_method_SelectedIndexChanged">
                                            <asp:ListItem>No of Working Days In Month</asp:ListItem>
                                            <asp:ListItem>No of Days In Month</asp:ListItem>
                                            <asp:ListItem>Custom</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" runat="server" id="scDPnl" visible="false">
                                        <label for="validationCustom01" class="form-label">Salary Calculation days<sup>*</sup></label>
                                        <asp:TextBox ID="txt_days" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label" style="width: 100%">Weekly off on<sup>*</sup></label>

                                        <asp:CheckBox ID="chk_mon" runat="server" Text="Mon" Style="margin: 0px 10px 0px 0px;" OnCheckedChanged="chk_mon_CheckedChanged" AutoPostBack="true" />
                                        <asp:CheckBox ID="chk_tue" runat="server" Text="Tue" Style="margin: 0px 10px 0px 0px;" OnCheckedChanged="chk_mon_CheckedChanged" AutoPostBack="true" />
                                        <asp:CheckBox ID="chk_wed" runat="server" Text="Wed" Style="margin: 0px 10px 0px 0px;" OnCheckedChanged="chk_mon_CheckedChanged" AutoPostBack="true" />
                                        <asp:CheckBox ID="chk_thu" runat="server" Text="Thu" Style="margin: 0px 10px 0px 0px;" OnCheckedChanged="chk_mon_CheckedChanged" AutoPostBack="true" />

                                        <asp:CheckBox ID="chk_fri" runat="server" Text="Fri" Style="margin: 0px 10px 0px 0px;" OnCheckedChanged="chk_mon_CheckedChanged" AutoPostBack="true" />
                                        <asp:CheckBox ID="chk_sat" runat="server" Text="Sat" Style="margin: 0px 10px 0px 0px;" OnCheckedChanged="chk_mon_CheckedChanged" AutoPostBack="true" />
                                        <asp:CheckBox ID="chk_sun" runat="server" Text="Sun" Style="margin: 0px 10px 0px 0px;" OnCheckedChanged="chk_mon_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Grade</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">


                                            <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 0px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
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
                                                                    <span style="font-size: 14px; font-weight: bold;">Grade Master List
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                </div>
                                                            </div>


                                                        </div>

                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Grade Name</th>
                                                                        <th>Nature of Work</th>
                                                                        <th>Description</th>
                                                                        <th>Max Work Day</th>
                                                                        <th>Min Work Day</th>
                                                                        <th>Salary Calcu.</th>
                                                                        <th>Action</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rd_view" runat="server">
                                                                        <ItemTemplate>
                                                                            <asp:Panel ID="Panel1" runat="server">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_grade_name" runat="server" Text='<%#Bind("grade_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_nature_of_work" runat="server" Text='<%#Bind("nature_of_work")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_description" runat="server" Text='<%#Bind("description")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_max_working_hour" runat="server" Text='<%#Bind("Max_working_hour")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_min_working_hour" runat="server" Text='<%#Bind("Min_working_hour")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_salary_calculation_method" runat="server" Text='<%#Bind("salary_calculation_method")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_grade_id" runat="server" Text='<%#Bind("grade_id")%>' Visible="false"></asp:Label>
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
        </div>
        <!--end row-->
    </div>

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title">Weekly off setting</h5>
                    <a href="#!" onclick="closeModal()" class="btn btn-secondary">Close</a>
                    <%--<asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />--%>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation" novalidate="">
                            <div class="col-12">
                                <h2 class="week-off-big-h">Select week for off</h2>
                                <asp:Label ID="lbl_week_of_day_h" class="week-off-day-h" runat="server" Text=""></asp:Label>
                                <div class="week-off-row">
                                    <asp:CheckBox ID="chk_all_week" runat="server" AutoPostBack="true" OnCheckedChanged="chk_all_week_CheckedChanged" />
                                    <p class="week-off-chkbx">All Weeks</p>
                                </div>

                                <div class="week-off-row">
                                    <asp:CheckBox ID="chk_first_week" runat="server" />
                                    <p class="week-off-chkbx">1st Weeks</p>
                                </div>
                                <div class="week-off-row">
                                    <asp:CheckBox ID="chk_scnd_week" runat="server" />
                                    <p class="week-off-chkbx">2nd Weeks</p>
                                </div>
                                <div class="week-off-row">
                                    <asp:CheckBox ID="chk_thrd_week" runat="server" />
                                    <p class="week-off-chkbx">3rd Weeks</p>
                                </div>
                                <div class="week-off-row">
                                    <asp:CheckBox ID="chk_frth_week" runat="server" />
                                    <p class="week-off-chkbx">4th Weeks</p>
                                </div>
                                <div class="week-off-row">
                                    <asp:CheckBox ID="chk_fth_week" runat="server" />
                                    <p class="week-off-chkbx">5th Weeks</p>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:Button ID="btn_save_week_off" OnClick="btn_save_week_off_Click" runat="server" Text="Save" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>


    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_temp_id" runat="server" />
    <script type="text/javascript">
        function openModal() {
            $("#myModal").show();
            $('#myModal').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function closeModal() {
            $("#myModal").hide();
            $('#myModal').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>
</asp:Content>
