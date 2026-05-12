<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Application_List_For_Career.aspx.cs" Inherits="school_web.Admin.Application_List_For_Career" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Application List For Career
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

        #pageFooter {
            display: none;
        }

        .dropdown-item {
            display: block;
            width: 100%;
            padding: 0.25rem 1rem;
            clear: both;
            font-weight: 400;
            color: #212529;
            text-align: inherit;
            text-decoration: none;
            white-space: nowrap;
            background-color: transparent;
            border: 0;
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
                <div class="breadcrumb-title pe-3">Employees Hiring </div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Application List For Career</li>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Hiring By</label>
                                                        <asp:DropDownList ID="ddl_hearing_by" runat="server" class="form-select find-dv-txtbx" OnSelectedIndexChanged="ddl_hearing_by_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Post Applied For</label>
                                                        <asp:DropDownList ID="ddl_post_applied_for" runat="server" class="form-select find-dv-txtbx" OnSelectedIndexChanged="ddl_post_applied_for_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>

                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 22px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 22px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>


                                                </div>
                                            </div>
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
                                                                        <span style="font-size: 14px; font-weight: bold;">Application List for Career
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                    </div>
                                                                </div>


                                                            </div>
                                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>

                                                                        <th>Session</th>
                                                                        <th>Application ID</th>
                                                                        <th>Date</th>
                                                                        <th>Post Applied For</th>
                                                                        <th>Applicant Name</th>
                                                                        <th>Subject Name</th>
                                                                        <th>Email Id</th>
                                                                        <th>Mobile No.</th>
                                                                        <th>Transaction Id</th>
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
                                                                                    <asp:Label ID="lbl_session_name" runat="server" Text='<%#Bind("session_name")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Apply_id" runat="server" Text='<%#Bind("Apply_id")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Apply_for" runat="server" Text='<%#Bind("Apply_for")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_First_Name" runat="server" Text='<%#Bind("First_Name")%>'></asp:Label>
                                                                                    <asp:Label ID="Label4" runat="server" Text='<%#Bind("Middle_Name")%>'></asp:Label>
                                                                                    <asp:Label ID="Label5" runat="server" Text='<%#Bind("Last_Name")%>'></asp:Label>
                                                                                </td>



                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_subject_name" runat="server" Text='<%#Bind("subject_name")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Emailid" runat="server" Text='<%#Bind("Emailid")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_mobile_no_CA" runat="server" Text='<%#Bind("mobile_no_CA")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_razorpay_payment_id" runat="server" Text='<%#Bind("razorpay_payment_id")%>'></asp:Label>
                                                                                </td>
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
                                                                                                <a class="dropdown-item" href="slip/online_career_form.aspx?regiDs=<%#Eval("Apply_id") %>" target="_blank"><span>View Details</span> </a>
                                                                                            </li>


                                                                                        </ul>
                                                                                    </div>



                                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_Registration_id" runat="server" Text='<%#Bind("Apply_id")%>' Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>



                                                    <div style="height: 2px; overflow: hidden">
                                                        <asp:GridView ID="grid_data" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Session">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_session_name" runat="server" Text='<%#Bind("session_name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Application ID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Apply_id" runat="server" Text='<%#Bind("Apply_id")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Post Applied For">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Apply_for" runat="server" Text='<%#Bind("Apply_for")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Applicant's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_applicantname" runat="server" Text='<%#Bind("Salutation")%>'></asp:Label>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("First_Name")%>'></asp:Label>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%#Bind("Middle_Name")%>'></asp:Label>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%#Bind("Last_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_applydate" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Subject Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_subject_name" runat="server" Text='<%#Bind("subject_name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Email id">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Emailid" runat="server" Text='<%#Bind("Emailid")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DOB*">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Date_birthday" runat="server" Text='<%#Bind("Date_birthday")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Gender">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Gender" runat="server" Text='<%#Bind("Gender")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Place Of Birth">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Place_Of_Birth" runat="server" Text='<%#Bind("Place_Of_Birth")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Birth State">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Birth_State" runat="server" Text='<%#Bind("Birth_State")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Religion">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Religion" runat="server" Text='<%#Bind("Religion")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Nationality">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Nationality" runat="server">INDIAN</asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Marital Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Marital_Status" runat="server" Text='<%#Bind("Marital_Status")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Address 1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Address_CA" runat="server" Text='<%#Bind("Address_CA")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="City 1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_City_CA" runat="server" Text='<%#Bind("City_CA")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="State 1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_State_CA" runat="server" Text='<%#Bind("State_CA")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pin Code 1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPincode_CA" runat="server" Text='<%#Bind("Pincode_CA")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Mobile No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblmobile_no_CA" runat="server" Text='<%#Bind("mobile_no_CA")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Residence Telephone no">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblResidence_telephone_no_CA" runat="server" Text='<%#Bind("Residence_telephone_no_CA")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Address 2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_address_pa" runat="server" Text='<%#Bind("address_pa")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="City 2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_city_pa" runat="server" Text='<%#Bind("city_pa")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="State 2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_state_pa" runat="server" Text='<%#Bind("state_pa")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Pin Code 2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_pin_pa" runat="server" Text='<%#Bind("pin_pa")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Child Name 1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_chiled_name1" runat="server" Text='<%#Bind("chiled_name1")%>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Child Gender 1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_chiled_gender1" runat="server" Text='<%#Bind("chiled_gender1")%>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Child Age 2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_chiled_age2" runat="server" Text='<%#Bind("chiled_age2")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Child Name 1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_chiled_name2" runat="server" Text='<%#Bind("chiled_name2")%>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Child Gender 2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_chiled_gender2" runat="server" Text='<%#Bind("chiled_gender2")%>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Child Age 2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_chiled_age2" runat="server" Text='<%#Bind("chiled_age2")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Child Name 3">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_chiled_name3" runat="server" Text='<%#Bind("chiled_name3")%>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Child Gender 3">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_chiled_gender3" runat="server" Text='<%#Bind("chiled_gender3")%>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Child Age 3">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_chiled_age3" runat="server" Text='<%#Bind("chiled_age3")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Father's Occupation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_father_occupation" runat="server" Text='<%#Bind("father_occupation")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Mother's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_mother_name" runat="server" Text='<%#Bind("mother_name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Mother's Occupation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_mother_occupation" runat="server" Text='<%#Bind("mother_occupation")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Spouse Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Spouse_name" runat="server" Text='<%#Bind("Spouse_name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Spouse Job is Transferable">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Spouses_job_is_transferable" runat="server" Text='<%#Bind("Spouses_job_is_transferable")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Spouse's Qualification">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_spouse_qualification" runat="server" Text='<%#Bind("spouse_qualification")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Spouse's Profession">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_spouse_profession" runat="server" Text='<%#Bind("spouse_profession")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Spouse's Profession">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_spouse_profession" runat="server" Text='<%#Bind("spouse_profession")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Spouse's Profession">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_spouse_designation" runat="server" Text='<%#Bind("spouse_designation")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Completed years">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_completed_years" runat="server" Text='<%#Bind("completed_years")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Teaching (years)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_teaching_years" runat="server" Text='<%#Bind("teaching_years")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Administration (Years)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_AdministrationYear" runat="server" Text='<%#Bind("Administration_year")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Any other">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_any_other" runat="server" Text='<%#Bind("any_other")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Name Of Institution">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Current_name_of_instituation" runat="server" Text='<%#Bind("Current_name_of_instituation")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Institution Address">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_instituation_address" runat="server" Text='<%#Bind("instituation_address")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Contact No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Contact_Numbe_instituation" runat="server" Text='<%#Bind("Contact_Numbe_instituation")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Designation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Designation_work" runat="server" Text='<%#Bind("Designation_work")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Joining Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_joining_date" runat="server" Text='<%#Bind("joining_date")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Place of Posting">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_place_of_posting" runat="server" Text='<%#Bind("place_of_posting")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Total Present Salary">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Present_Salary" runat="server" Text='<%#Bind("Present_Salary")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Basic Salary">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Basic_Salary_Present" runat="server" Text='<%#Bind("Basic_Salary_Present")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Allowance (Present)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Allowance_Present" runat="server" Text='<%#Bind("Allowance_Present")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Other Benefits Present">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Other_Benefits_Present" runat="server" Text='<%#Bind("Other_Benefits_Present")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Under Service Bond">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Other_Benefits_Present" runat="server" Text='<%#Bind("Under_Service_Bond")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Years service bond">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_years_service_bond" runat="server" Text='<%#Bind("years_service_bond")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Expected Salary">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Expected_Salary" runat="server" Text='<%#Bind("Expected_Salary")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="English Read">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_English_read" runat="server" Text='<%#Bind("English_read")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="English Write">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_English_write" runat="server" Text='<%#Bind("English_write")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="English Speak">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_English_Speak" runat="server" Text='<%#Bind("English_Speak")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Hindi Read">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Hindi_read" runat="server" Text='<%#Bind("Hindi_read")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hindi Write">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Hindi_write" runat="server" Text='<%#Bind("Hindi_write")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hindi Speak">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Hindi_Speak" runat="server" Text='<%#Bind("Hindi_speak")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Bengali Read">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Bangla_read" runat="server" Text='<%#Bind("Bangla_read")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bengali Write">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Bengali_write" runat="server" Text='<%#Bind("Hindi_write")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bengali Speak">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Bengali_Speak" runat="server" Text='<%#Bind("Hindi_speak")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Other Language">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Other_Language" runat="server" Text='<%#Bind("Other_Language")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Proficiency In Computer">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Proficiency_In_Computer" runat="server" Text='<%#Bind("Proficiency_In_Computer")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Transaction Id">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_razorpay_payment_id" runat="server" Text='<%#Bind("razorpay_payment_id")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Payable_amount")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
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
            <!--end row-->
        </div>
        <asp:HiddenField ID="hd_id" runat="server" />
        <!--end page wrapper -->
</asp:Content>
