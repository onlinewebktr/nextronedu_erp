<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="coupon-status.aspx.cs" Inherits="school_web.Admin.coupon_status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Coupon List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
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
            background: rgb(0 0 0 / 39%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translate(0, 0px);
            }

        .statusPending {
            margin: 0px;
            padding: 0px 5px;
            background: #f5e200;
            color: #000000;
            border: 1px solid #d5c400;
            border-radius: 3px;
        }

        .statusApproved {
            margin: 0px;
            padding: 0px 5px;
            background: #04f700;
            color: #000000;
            border: 1px solid #16d300;
            border-radius: 3px;
        }

        .statusReject {
            margin: 0px;
            padding: 0px 5px;
            background: #f70000;
            color: #ffffff;
            border: 1px solid #bf0000;
            border-radius: 3px;
        }
    </style>

    <script type="text/javascript">
        function openModalC() {
            $('#MdlCoupon').modal('show');
        }
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
                <div class="breadcrumb-title pe-3">Reprint Slip</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a></li>
                            <li class="breadcrumb-item active" aria-current="page">Pending Coupon List</li>
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
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="find-dv">
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl" style="font-weight: bold">Session</label>
                                                <asp:DropDownList ID="ddl_session_serach" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                        <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                        <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                            </div>

                                            <div class="col-sm-2">
                                                <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 20px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 20px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server" ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
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

                                                            &nbsp;&nbsp; website :
                                                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                        </div>
                                                        <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                            Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                                        </div>
                                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                            <span style="font-size: 14px; font-weight: bold;">
                                                                <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <table id="datatable" data-page-length='1500' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <%--<th class="hiddenOnPrint"></th>--%>
                                                                <th>Coupon Code</th>
                                                                <th>Status</th>
                                                                <th>Session</th>
                                                                <th>Admission No.</th>
                                                                <th>Class</th>
                                                                <th>Section</th>
                                                                <th>Roll No.</th>
                                                                <th>Student Name</th>
                                                                <th>Father Name</th>
                                                                <th>Mobile No.</th>

                                                                <th>Total Amount</th>
                                                                <th>Disc. Amount</th>
                                                                <th>Disc. Percent</th>
                                                                <th>Created By</th>
                                                                <th>Created Date</th>


                                                                <th>Updated By</th>
                                                                <th>Updated Date</th>
                                                                <th>Remark</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <%--<td class="hiddenOnPrint">
                                                                            <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                    href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                    <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                        <i class="bx bx-grid-horizontal"></i>
                                                                                    </div>
                                                                                </a>
                                                                                <ul class="dropdown-menu dropdown-menu-end">
                                                                                    <li>
                                                                                        <a class="dropdown-item" target="_blank" href="slip/monthly-slip.aspx?admissionno=<%#Eval("Addmission_no") %>&sessionid=<%#Eval("Session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>">
                                                                                            <i class="bx bx-printer"></i><span>Print A4</span>
                                                                                        </a>
                                                                                    </li>
                                                                                    <asp:Label ID="Label4" runat="server">
                                                                                    <li>
                                                                                        <a class="dropdown-item" target="_blank" href="slip/monthly-slip-a5.aspx?admissionno=<%#Eval("Addmission_no") %>&sessionid=<%#Eval("Session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>">
                                                                                            <i class="bx bx-printer"></i><span>Print A5</span>
                                                                                        </a>
                                                                                    </li>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="Label3" runat="server">
                                                                                    <li> 
                                                                                        <a class="dropdown-item" target="_blank" href="slip/bill-with-scholarship.aspx?admissionno=<%#Eval("Addmission_no") %>&sessionid=<%#Eval("Session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>">
                                                                                            <i class="bx bx-printer"></i><span>Print A5</span>
                                                                                        </a> 
                                                                                    </li>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="Label5" runat="server">
                                                                                    <li> 
                                                                                        <a class="dropdown-item" target="_blank" href="slip/bill-installment.aspx?admissionno=<%#Eval("Addmission_no") %>&sessionid=<%#Eval("Session_id") %>&classid=<%#Eval("Class_id") %>&Slip_no=<%#Eval("Slip_no") %>">
                                                                                            <i class="bx bx-printer"></i><span>Print A5</span>
                                                                                        </a> 
                                                                                    </li>
                                                                                    </asp:Label>
                                                                                </ul>
                                                                            </div>

                                                                            <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_sessionid" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                        </td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lbl_coupon_id" runat="server" Text='<%#Bind("Coupon_id")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnk_status" Visible="false" runat="server" OnClick="lnk_status_Click">Pending</asp:LinkButton>
                                                                            <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%#Bind("Id")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("Admission_no")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_std_name" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_mobile_no" runat="server" Text='<%#Bind("Father_whatsApp_no")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Total_amount")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_disc_perc" runat="server" Text='<%#Bind("Discount_persent")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Created_date")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_created_by" runat="server" Text='<%#Bind("Created_by")%>'></asp:Label>
                                                                        </td>


                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%#Bind("Updated_By")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label8" runat="server" Text='<%#Bind("Updated_date")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label7" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
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

    <div id="MdlCoupon" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 800px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 10px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Coupon Discount Details</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body" style="padding: 5px 5px;">
                    <div class="p-4 border rounded" style="float: left; width: 100%; padding: 5px 5px !important;">
                        <div class="disc-pop-dv-wpr">
                            <div class="fnd-box-wpr-inr" style="padding: 0px 0px;">
                                <div id="monthFeeDiscHead">
                                    <div class="disc-pop-tbl-wpr">
                                        <table class="table table-striped table-bordered dataTable" style="border-bottom: 1px solid #adadad; border-left: 1px solid #adadad; border-top: 1px solid #adadad; margin-bottom: 0px !important;">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Fees Head</th>
                                                    <th>Month</th>
                                                    <th>Fees Amount</th>
                                                    <th>Disc. Amount</th>
                                                    <th>Disc. %</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_coupon_head" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("Fee_head_name") %>'></asp:Label>
                                                                <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("fee_head_id") %>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text='<%#Bind("month") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label11" runat="server" Text='<%#Bind("Fee_amount") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" Text='<%#Bind("disc_amount") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" Text='<%#Bind("Discount_per") %>'></asp:Label>
                                                            </td>
                                                            <%-- <td class="noclick">
                                                                <asp:TextBox ID="txt_head_fee" class="noclick" runat="server" Style="width: 80px;" Text='<%#Eval("amount") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_disc_fee" runat="server" Text='<%#Eval("discount") %>' Style="width: 80px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_disc_perc" runat="server" Text='0' Style="width: 80px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            </td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        <div class="disc-pop-txtbx-sec">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <p>Status</p>
                                                    <asp:DropDownList ID="ddl_status" runat="server" class="form-select disc-pop-txtbx">
                                                        <asp:ListItem>Approved</asp:ListItem>
                                                        <asp:ListItem>Reject</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-7">
                                                    <p>Remark</p>
                                                    <asp:TextBox ID="txt_Remarks" runat="server" class="form-control disc-pop-txtbx"></asp:TextBox>
                                                </div>

                                                <div class="col-md-2">
                                                    <asp:Button ID="btn_update_status" runat="server" class="button-6161 disc-pop-save_disc" Text="Save" OnClick="btn_update_status_Click" />
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

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
</asp:Content>
