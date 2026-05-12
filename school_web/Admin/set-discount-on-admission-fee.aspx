<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="set-discount-on-admission-fee.aspx.cs" Inherits="school_web.Admin.set_discount_on_admission_fee_aspx" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Set Discount on Admission Fees
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

    <style>
        .Llabel {
            margin: 11px 0px 6px 0px;
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
    <style>
        .head {
            display: none;
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
                <div class="breadcrumb-title pe-3">Fees Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Discount Setup</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul sub-pag-menu-ul-mrgn">
                        <li><a href="set-discount-on-admission-fee.aspx" class="sub-mnu-p-a-active">Admission Fees</a></li>
                        <li><a href="set-discount-on-annual-fee.aspx">Annual Fees</a></li>
                        <li><a href="set-discount-on-monthly-fee.aspx">Monthly Fees</a></li>
                        <li><a href="set-discount-on-bus-fees.aspx">Bus Fees</a></li>
                        <li><a href="Set_Student_Wise_Discount_type_head.aspx">Discount Mode</a></li>
                        <li><a href="set-student-wise-discount.aspx">Student Wise</a></li>
                        <li><a href="set-student-wise-discount-s-month.aspx">Month Wise</a></li>
                    </ul>
                </div>
                <hr />
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Set Discount for Admission Fees"></asp:Label>
                            <hr />
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-xl-12">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label Llabel">Session</label>
                                                <asp:DropDownList ID="ddl_session" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label Llabel">Discount For</label>
                                                <asp:DropDownList ID="ddl_parameter" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_parameter_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2" id="hostel_name" runat="server" visible="false">
                                                <label for="validationCustom01" class="form-label Llabel">Hostel</label>
                                                <asp:DropDownList ID="ddl_hostel_name" runat="server" class="form-select">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label Llabel">Group</label>
                                                <asp:DropDownList ID="ddl_category" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_category_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label Llabel">Sub-Group</label>
                                                <asp:DropDownList ID="ddl_sub_category" runat="server" class="form-select">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label Llabel">Class</label>
                                                <asp:DropDownList ID="ddl_class" runat="server" class="form-select" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6" style="margin: 0px 0px 10px 0px;">
                                                <asp:Panel ID="pnl_fee_grid" runat="server" Visible="false">
                                                    <label for="validationCustom01" class="form-label Llabel">Fees Detail</label>
                                                    <br />
                                                    <table class="table table-striped table-bordered dataTable" style="border-bottom: 1px solid #adadad; border-left: 1px solid #adadad; border-top: 1px solid #adadad; margin-bottom: 0px !important;">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Fees Head</th>
                                                                <th>Fees Amount</th>
                                                                <th>Disc. Amount</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rp_fee_details" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("content_id") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td class="noclick">
                                                                            <asp:TextBox ID="txt_fee" class="noclick" runat="server" Style="width: 80px;" Text='<%#Eval("amount") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_disc_fee" Text="0" runat="server" Style="width: 80px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <tr>
                                                                <td></td>
                                                                <td style="text-align: right; font-weight: 700;">TOTAL</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_total_head_fee_for_head" Style="font-weight: bold;" runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lbl_total_disc" runat="server" Style="font-weight: bold;"></asp:Label></td>
                                                            </tr>
                                                        </tbody>
                                                    </table> 
                                                </asp:Panel>
                                            </div>
                                            <div class="col-12" style="text-align: left">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" Visible="false" />
                                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" />
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <script type="text/javascript">
                    $(document).ready(function () {

                        CalculateTotalFee();
                        $("[Id*=txt_fee]").on("keyup", function () {
                            CalculateTotalFee();
                        });

                        CalculateTotal();
                        $("[Id*=txt_disc_fee]").on("keyup", function () {
                            CalculateTotal();
                        });
                    });


                    function CalculateTotalFee() {
                        var total = 0;
                        $("[Id*=txt_fee").each(function () {
                            total += parseFloat($(this).val());
                        });
                        $("#<%=lbl_total_head_fee_for_head.ClientID%>").text(total);
                    }




                    function CalculateTotal() {
                        var total = 0; var totalFee = 0;
                        $("[Id*=txt_disc_fee").each(function () {
                            total += parseFloat($(this).val());
                            $("[Id*=txt_disc_fee").focus(function () { $(this).select(); });
                        });
                        $("[Id*=txt_fee").each(function () {
                            totalFee = parseFloat($(this).val());
                        });
                        $("#<%=lbl_total_disc.ClientID%>").text(total);


                        var ttlfeeheads = parseFloat($("#<%=lbl_total_head_fee_for_head.ClientID%>").text());
                        var ttlfeedisc = parseFloat($("#<%=lbl_total_disc.ClientID%>").text());
                        if (ttlfeedisc > ttlfeeheads) {
                            $("#<%=lbl_total_disc.ClientID%>").text(ttlfeeheads);
                        }
                    }
                </script>
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Discount for Admission Fees</h6>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Discount For</label>
                                                        <asp:DropDownList ID="ddl_parameter_search" runat="server" class="form-select find-dv-txtbx">
                                                        </asp:DropDownList>
                                                    </div>



                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>


                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_fnd_by_class" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_by_class_Click" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 20px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn"><i class='bx bx-download'></i>Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 20px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                            ToolTip="Print">
                                                                <i class='bx bx-printer'></i>
                                                        </asp:LinkButton>
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
                                                                    <span style="font-size: 14px; font-weight: bold;">Discount Admission Fee
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label>
                                                                    </span> 
                                                                </div>
                                                            </div> 
                                                        </div>

                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Class</th>
                                                                        <th>Group</th>
                                                                        <th>Sub-Group</th>
                                                                        <th>Fees Head</th>
                                                                        <th>Fees Amount</th>
                                                                        <th>Disc. Amount</th>
                                                                        <th>After Disc.</th>
                                                                        <th>Action</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rd_viewaddedfee" runat="server" OnItemDataBound="rd_viewaddedfee_ItemDataBound">
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
                                                                                        <asp:Label ID="lbl_category" runat="server" Text='<%#Bind("category")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_sub_category" runat="server" Text='<%#Bind("sub_category")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_disc_amount" runat="server" Text='<%#Bind("disc_amount")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_after_disc" runat="server" Text='<%#Bind("after_disc")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                                                        <asp:Label ID="lbl_category_id" runat="server" Text='<%#Bind("category_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_sub_category_id" runat="server" Text='<%#Bind("sub_category_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_fee_head_id" runat="server" Text='<%#Bind("fee_head_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_parameter_id" runat="server" Text='<%#Bind("parameter_id")%>' Visible="false"></asp:Label>
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

    <!--end page wrapper -->
    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 916px;
                margin: 1.75rem auto;
            }
        }
    </style>
</asp:Content>
