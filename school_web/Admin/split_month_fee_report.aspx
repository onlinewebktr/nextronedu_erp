<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="split_month_fee_report.aspx.cs" Inherits="school_web.Admin.split_month_fee_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Split Month Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .head {
            display: none;
        }
    </style>
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i>Report</a></div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Split Month Report</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper1" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-md-2">
                                                        <label for="validationCustom01" class="form-label Llabel">Session</label>
                                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label for="validationCustom01" class="form-label Llabel">Class</label>
                                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label for="validationCustom01" class="form-label Llabel">Section</label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-select"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label for="validationCustom01" class="form-label Llabel"> OR</label>
                                                   
                                                        </div>

                                                     <div class="col-md-2">
                                                        <label for="validationCustom01" class="form-label Llabel">Admission No</label>
                                                        
<asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>

                                                    </div>
                                                
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label" style="margin: 10px 0px 5px 0px;">Choose Month<sup>*</sup></label>
                                            <span class="chkbx-all">
                                                <asp:CheckBox ID="chk_all_month" runat="server" Text="Select All" OnCheckedChanged="chk_all_month_CheckedChanged" AutoPostBack="true" /></span>
                                            <br />
                                            <asp:Repeater ID="rp_month" runat="server">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_month_name" class="chkstle" runat="server" Text='<%#Eval("Month") %>' />
                                                    <asp:Label ID="lbl_value" runat="server" Visible="false" Text='<%#Bind("Value")%>'></asp:Label>
                                                    <asp:Label ID="lbl_month_name" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                                    <asp:Label ID="lbl_month_id" runat="server" Visible="false" Text='<%#Bind("Month_Id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                                  <div class="col-12">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary find-dv-btn" ValidationGroup="a" OnClick="btn_find_Click1" />


                                                    <asp:LinkButton ID="btn_excels" Visible="false" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" Style="margin-left: 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>


                                                </div>
                                        <div class="col-xl-12">
                                            <h6 class="mb-0 text-uppercase" style="margin: 10px 0px 0px 0px;">Monthly Split List</h6>
                                            <hr />
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="table-responsive">
                                                        <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                                            <div class="row">





                                                                <div class="col-sm-12">
                                                                    <div id="tblPrintIQ" runat="server">
                                                                        <div class="prnt-dv-wpr printborder">
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
                                                                                            <span style="font-size: 18px; font-weight: bold;">Split Month Report
                                                                        <asp:Label ID="lbl_date" runat="server"></asp:Label></span>


                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                                <div class="grd-wpr">
                                                                                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped" Style="width: 100%" AutoGenerateColumns="true" OnRowCreated="GridView2_RowCreated" OnRowDataBound="GridView2_RowDataBound" ShowFooter="true">
                                                                                    </asp:GridView>

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
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
