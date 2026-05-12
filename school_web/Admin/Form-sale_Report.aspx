<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Form-sale_Report.aspx.cs" Inherits="school_web.Admin.Form_sale_Report" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Form Sale Collection List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
         .head {
            display: none;
        }
          tbody, td, tfoot, th, thead, tr {
            border-color: inherit;
            border-style: solid;
            border-width: 1px;
            text-align: left;
            color: #000;
        }

        th {
            border-color: inherit;
            border-style: solid;
            border-width: 1px;
            background-color: #0d6efd;
        }

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
                <div class="breadcrumb-title pe-3"><a href="daily-closing-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Form Sale Collection List</li>
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
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">


                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                        <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                        <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                     <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                       <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx"  ></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                    <div class="col-sm-2"  >
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin-left: 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">

                                                <div class="col-sm-12">
                                                    <div id="tblPrintIQ" runat="server">
                                                        <div class="prnt-dv-wpr printborder">


                                                            <div class="pgslry-head-div head">

                                                                <div style="margin: 0px; padding: 0px; height: 131px; width: 20%; float: left;">
                                                                    <asp:Image ID="imglogo" runat="server" Style="height: 81px; width: 81px" />
                                                                </div>
                                                                <div style="margin: 0px; padding: 0px; height: 131px; width: 80%; float: left;">
                                                                    <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                                        <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                                                    </h1>

                                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                                                    </div>
                                                                    <div  style="margin: 7px 0px 0px 0px; display:none; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div style="margin: 7px 0px 0px 0px; display:none; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                    </div>


                                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        <span style="font-size: 14px; font-weight: bold;">Form Sale Report</span>


                                                                    </div>
                                                                </div>


                                                            </div>
                                                            <asp:GridView ID="grd_fee" runat="server" AutoGenerateColumns="False" Width="100%" Style="text-align: center;" OnRowDataBound="grd_fee_RowDataBound" ShowFooter="True">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Form No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Form_no" runat="server" Text='<%#Bind("Form_no") %>'></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Student Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_student_name" runat="server" Text='<%#Bind("student_name") %>'></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Father Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_fathers_name" runat="server" Text='<%#Bind("fathers_name") %>'></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Mobile No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("mobile") %>'></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Category">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_course" runat="server" Text='<%#Bind("cast") %>'></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Gender">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_gender" runat="server" Text='<%#Bind("gender") %>'></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("date") %>'></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Session">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_paymenetmode" runat="server" Text='<%#Bind("session") %>'></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("class") %>'></asp:Label>

                                                                        </ItemTemplate>
                                                                        <FooterTemplate>Total</FooterTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="td2" Width="100px" />
                                                                        <HeaderStyle CssClass="td2" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totalamount" runat="server" Font-Bold="true"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>

                                                            </asp:GridView>

                                                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%" class="head">
                                                                <div style="margin: 57px 0px 0px 0px; padding: 0px; float: left; width: 50%; color: black; font-size: 15px; font-weight: bold;">
                                                                    <span>Print Date Time :
                                    <asp:Label ID="lbl_datetime" runat="server" ForeColor="Black"></asp:Label></span>
                                                                </div>

                                                                <div style="margin: 57px 0px 0px 0px; padding: 0px; float: left; text-align: right; width: 40%; color: black; font-size: 15px; font-weight: bold;">
                                                                    <div style="margin: 0px; padding: 0px; float: left; text-align: center; width: 100%">
                                                                        <span>
                                                                            <asp:Label ID="lbl_useridandname" runat="server"></asp:Label></span>
                                                                    </div>
                                                                    <div style="margin: 0px; padding: 0px; float: left; text-align: center; font-weight: normal; width: 100%">

                                                                        <span>Signature</span>
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
</asp:Content>
