<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="dues-list.aspx.cs" Inherits="school_web.Admin.dues_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Transport Dues List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Reports</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Transport Monthly Dues</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <%--<li><a href="admission-fee-dues-report.aspx">Admission Fees Dues List</a></li>
                        <li><a href="annual-fee-dues-report.aspx">Annual Fees Dues List</a></li>--%>
                        <li><a href="monthly-dues.aspx">Monthly Dues List</a></li> 
                        <li><a href="dues-list.aspx" class="sub-mnu-p-a-active">Transport Dues</a></li>
                        <li><a href="overall-dues.aspx">All Dues List</a></li>
                    </ul>
                </div>
                <div class="col-xl-12">
                    <hr />
                    <div class="col-xl-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="p-4 border rounded">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-xl-12">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label for="validationCustom01" class="form-label Llabel">Session</label>
                                                    <asp:DropDownList ID="ddl_session" runat="server" class="form-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <label for="validationCustom01" class="form-label Llabel">Class</label>
                                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-select"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label for="validationCustom01" class="form-label Llabel">Section</label>
                                                    <asp:DropDownList ID="ddl_section" runat="server" class="form-select"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2" style="display: none">
                                                    <label for="validationCustom01" class="form-label Llabel">Student Type</label>
                                                    <asp:DropDownList ID="ddl_fee_type" runat="server" class="form-select">
                                                        <asp:ListItem>ALL</asp:ListItem>
                                                        <asp:ListItem Value="4">Bus</asp:ListItem>
                                                        <asp:ListItem Value="2">With School Fee</asp:ListItem>
                                                    </asp:DropDownList>
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
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary find-dv-btn" ValidationGroup="a" OnClick="btn_find_Click" />
                                                    <asp:LinkButton ID="btn_excels" Visible="false" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" Style="margin-left: 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-xl-12">
                        <h6 class="mb-0 text-uppercase">Monthly Dues List</h6>
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
                                                                        <span style="font-size: 18px; font-weight: bold;">Dues List
                                                                        <asp:Label ID="lbl_date" runat="server"></asp:Label></span>


                                                                    </div>
                                                                </div>


                                                            </div>
                                                            <div class="grd-wpr">
                                                                <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped" Style="width: 100%" AutoGenerateColumns="true" OnRowCreated="GridView2_RowCreated" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound="GridView2_RowDataBound2" ShowFooter="true"></asp:GridView>

                                                                <div style="display: none;">
                                                                    <asp:Button ID="btn_empty" runat="server" Text="Find" OnClick="btn_empty_Click" />
                                                                </div>


                                                                <script>
                                                                    function click_empty() {
                                                                        console.log("before clicked");
                                                                        document.getElementById('<%=btn_empty.ClientID%>').click();
                                                                    }
                                                                </script>

                                                            </div>
                                                                <div class="grd-wpr" id="sendsms" runat="server" visible="false" style="display: block">

                                                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                                                    <tr>
                                                                        <td style="padding: 5px; font-weight: bold;">Send Dues Message
                                                                            
                                                                            <asp:RadioButton ID="rd_whatassp" runat="server" GroupName="ak" Text="Whatsapp" />
                                                                            <asp:RadioButton ID="rd_sms" Checked="true" runat="server" GroupName="ak" Text="SMS" />
                                                                        </td>


                                                                    </tr>
                                                                    
                                                                    <tr>
                                                                        <td style="padding: 5px;">
                                                                            <div class="col-md-12">
                                                                                <asp:Label ID="lbl_msg" runat="server"></asp:Label>
                                                                            </div>
                                                                            <div class="row" style="display: none">
                                                                                <asp:Panel runat="server" Visible="false" ID="pnl_0" CssClass="col-lg-6">
                                                                                    <asp:Label runat="server" ID="lbl_0" class="form-label">Message</asp:Label>
                                                                                    <asp:TextBox ID="txt_0" runat="server" class="form-control"></asp:TextBox>
                                                                                </asp:Panel>
                                                                                <asp:Panel runat="server" Visible="false" ID="pnl_1" CssClass="col-lg-6">
                                                                                    <asp:Label runat="server" ID="lbl_1" class="form-label">Message</asp:Label>
                                                                                    <asp:TextBox ID="txt_1" runat="server" class="form-control"></asp:TextBox>
                                                                                </asp:Panel>
                                                                            </div>

                                                                            <div class="row" style="display: none">
                                                                                <asp:Panel runat="server" Visible="false" ID="pnl_2" CssClass="col-lg-6">
                                                                                    <asp:Label runat="server" ID="lbl_2" class="form-label">Message</asp:Label>
                                                                                    <asp:TextBox ID="txt_2" runat="server" class="form-control"></asp:TextBox>
                                                                                </asp:Panel>

                                                                                <asp:Panel runat="server" Visible="false" ID="pnl_3" CssClass="col-lg-6">
                                                                                    <asp:Label runat="server" ID="lbl_3" class="form-label">Message</asp:Label>
                                                                                    <asp:TextBox ID="txt_3" runat="server" class="form-control"></asp:TextBox>
                                                                                </asp:Panel>
                                                                            </div>
                                                                            <div class="row" style="display: none">
                                                                                <asp:Panel runat="server" Visible="false" ID="pnl_4" CssClass="col-lg-6" Style="display: block">
                                                                                    <asp:Label runat="server" ID="lbl_4" class="form-label">Message</asp:Label>
                                                                                    <asp:TextBox ID="txt_4" runat="server" class="form-control"></asp:TextBox>
                                                                                </asp:Panel>

                                                                                <asp:Panel runat="server" Visible="false" ID="pnl_5" CssClass="col-lg-6">
                                                                                    <asp:Label runat="server" ID="lbl_5" class="form-label">Message</asp:Label>
                                                                                    <asp:TextBox ID="txt_5" runat="server" class="form-control"></asp:TextBox>
                                                                                </asp:Panel>

                                                                            </div>

                                                                            <div class="row" style="display: block">
                                                                                <asp:Panel runat="server" Style="display: none" Visible="false" ID="pnl_6" CssClass="col-lg-6">
                                                                                    <asp:Label runat="server" ID="lbl_6" class="form-label">Message</asp:Label>
                                                                                    <asp:TextBox ID="txt_6" runat="server" class="form-control"></asp:TextBox>
                                                                                </asp:Panel>

                                                                                <asp:Panel runat="server" Visible="false" ID="pnl_7" Style="display: none" CssClass="col-lg-6">
                                                                                    <asp:Label runat="server" ID="lbl_7" class="form-label">Message</asp:Label>
                                                                                    <asp:TextBox ID="txt_7" runat="server" class="form-control"></asp:TextBox>

                                                                                </asp:Panel>

                                                                            </div>
                                                                            <div class="row">
                                                                                <asp:Panel runat="server" Visible="false" ID="pnl_8" CssClass="col-lg-6">
                                                                                    <asp:Label runat="server" ID="lbl_8" class="form-label">Message</asp:Label>
                                                                                    <asp:TextBox ID="txt_8" runat="server" class="form-control"></asp:TextBox>

                                                                                </asp:Panel>
                                                                            </div>




                                                                            <div class="row">
                                                                                <asp:Panel runat="server" Visible="false" ID="pnl_msg" class="col-lg-12">
                                                                                    <label for="validationCustom01" class="form-label">Message</label>
                                                                                    <asp:TextBox ID="txt_message" ReadOnly="true" TextMode="MultiLine" runat="server" style="height:100px" class="form-control"></asp:TextBox>
                                                                                </asp:Panel>
                                                                            </div>

                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="padding: 5px;">
                                                                            <asp:Button ID="btn_msgPreview" runat="server" Text="SMS Preview" class="btn btn-danger find-dv-btn" OnClick="btn_msgPreview_Click" />

                                                                            <div style="overflow: hidden; height: 1px">


                                                                                <asp:Button ID="btn_Submit" runat="server" Text="Send" class="btn btn-success find-dv-btn" Style="margin-left: 20px;" OnClick="btn_Submit_Click" />
                                                                            </div>
                                                                            <a onclick="save_data()" class="btn btn-primary" style="  width: 146px; margin: 21px 0px 0px 9px;">Final Send SMS</a>


                                                                        </td>
                                                                    </tr>
                                                                </table>

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

    <script type="text/javascript">
        function Confirm() {
            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to submit?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }


        function save_data() {
            var valsubmit = $('#<%=btn_Submit.ClientID %>').val();
            if (valsubmit == "Send") {
                $('#<%=btn_Submit.ClientID %>').val('Submitting.. Please Wait..');
                Confirm();
                document.getElementById("<%=btn_Submit.ClientID %>").click();
            }
            else {
                alert("Already submitted")
            }
        }
    </script>
</asp:Content>
