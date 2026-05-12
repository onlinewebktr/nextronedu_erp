<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="transport-fee-master.aspx.cs" Inherits="school_web.Admin.transport_fee_master" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Transport Fees for Day Scholar
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        .Llabel {
            margin: 11px 0px 6px 0px;
        }

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
                <div class="breadcrumb-title pe-3">Fees Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Transport Fees for Day Scholar</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <li><a href="Admission_Fee_Master.aspx">Set Admission Fees</a></li>
                        <li><a href="annual-fee-master.aspx">Set Annual Fees</a></li>
                        <li><a href="monthly-fee-master.aspx">Set Monthly Fees</a></li>
                        <li><a href="transport-fee-master.aspx" class="sub-mnu-p-a-active">Set Transportation Fees</a></li>
                    </ul>
                </div>
                <div class="col-xl-12">
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Set Transportation Fees"></asp:Label>
                            <hr />
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-4">
                                        <label for="validationCustom01" class="form-label">Transportation Distance<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_transp_path" runat="server" class="form-select" AutoPostBack="false" OnSelectedIndexChanged="ddl_transp_path_SelectedIndexChanged"></asp:DropDownList>
                                    </div>


                                    <div class="col-md-12" runat="server" id="sdtList" visible="false">
                                        <label for="validationCustom01" class="form-label">Student List<sup>*</sup></label>
                                        <div class="grd-wpr">

                                            <table id="example22" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Admission No.</th>
                                                        <th>Admission Date</th>
                                                        <th>Class</th>
                                                        <th>Session</th>
                                                        <th>Section</th>
                                                        <th>Roll No.</th>
                                                        <th>Student Name</th>
                                                        <th>Father Name</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_std_list" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_dateofadmission" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:Label>
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
                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label3" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>




                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Choose Month<sup>*</sup></label>
                                        <span class="chkbx-all">
                                            <asp:CheckBox ID="chk_all_month" runat="server" Text="Select All" OnCheckedChanged="chk_all_month_CheckedChanged" AutoPostBack="true" /></span>
                                        <br />
                                        <asp:Repeater ID="rp_month" runat="server" OnItemDataBound="rp_month_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_month_name" class="chkstle" runat="server" Text='<%#Eval("Month") %>' />
                                                <asp:Label ID="lbl_value" runat="server" Visible="false" Text='<%#Bind("Value")%>'></asp:Label>
                                                <asp:Label ID="lbl_month_name" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                                <asp:Label ID="lbl_mnth_id" runat="server" Visible="false" Text='<%#Bind("Month_Id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Transportation Fees<sup>*</sup></label>
                                        <asp:TextBox ID="txt_fee" runat="server" class="form-control" OnRowDataBound="grd_fee_RowDataBound"></asp:TextBox>
                                    </div>

                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <h6 class="mb-0 text-uppercase">Added Transport Fees Month Wise</h6>
                            <hr />
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <label for="validationCustom01" class="find-dv-lbl">Transportation Distance</label>
                                                        <asp:DropDownList ID="ddl_srch_t_path" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_srch_t_path_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="validationCustom01" class="find-dv-lbl">Month</label>
                                                        <asp:DropDownList ID="ddl_srch_months" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_fnd_by_pth_mnth" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_by_pth_mnth_Click" />
                                                    </div>
                                                    <div class="col-sm-3">
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
                                                                    <span style="font-size: 14px; font-weight: bold;">Transport Fees
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                </div>
                                                            </div>


                                                        </div>

                                                        <asp:Panel ID="Panel1" runat="server">
                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Session</th>
                                                        <th>Transportation Route</th>
                                                        <th>Transport Distance</th>
                                                        <th>Month</th>
                                                        <th>Fees Amount</th>
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
                                                                    <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Pathname" runat="server" Text='<%#Bind("Pathname")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Transfer_Status" runat="server" Text='<%#Bind("Transfer_Status")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Month" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                </td>

                                                                <td style="text-align: left;">
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>

                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_transportation_path_id" runat="server" Text='<%#Bind("Transportation_path_id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_month_id" runat="server" Text='<%#Bind("month_id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("session_id")%>' Visible="false"></asp:Label>

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
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
