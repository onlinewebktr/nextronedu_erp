<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Hostel_discount_on_monthly_fee.aspx.cs" Inherits="school_web.Admin.Hsotel_discount_on_monthly_fee" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Set Discount on Monthly
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

        #pageFooter {
            display: none;
        }

        .home-grph-wpr {
            width: 114%;
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
                <div class="breadcrumb-title pe-3"><a href="Hostel_Master.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i>Hostel</a></div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Set Discount on Admission Fees</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul sub-pag-menu-ul-mrgn">
                        <li><a href="Hostel_Admission_Fee_or_Annual_Master_Discount.aspx" class="sub-mnu-p-a">Set Discount on Admission/Annual Fees</a></li>

                        <li><a href="Hostel_discount_on_monthly_fee.aspx"  class="sub-mnu-p-a-active" >Set Discount on Monthly Fees</a></li>

                        <li><a href="Hostel_student_wise_discount.aspx" >Student Wise Discount</a></li>
                    </ul>
                </div>
                <hr />
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Set Discount on Admission Fees"></asp:Label>
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
                                                <label for="validationCustom01" class="form-label Llabel">Select Category</label>
                                                <asp:DropDownList ID="ddl_category" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_category_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label Llabel">Select Sub-Category</label>
                                                <asp:DropDownList ID="ddl_sub_category" runat="server" class="form-select">
                                                </asp:DropDownList>
                                            </div>


                                                 <div class="col-sm-2" style="display:none">
                                                    <label for="validationCustom01" class="form-label Llabel">Class</label>
                                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                           

                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label Llabel">Select Hsotel</label>
                                                <asp:DropDownList ID="ddl_hostel" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_hostel_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>


                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label Llabel">Room Category</label>
                                                <asp:DropDownList ID="ddl_room_catogery" runat="server" class="form-select" AutoPostBack="true"  OnSelectedIndexChanged="ddl_room_catogery_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>



                                            <div class="col-md-12">
                                                <label for="validationCustom01" class="form-label" style="margin: 10px 0px 5px 0px;">Choose Month<sup>*</sup></label>
                                                <span class="chkbx-all">
                                                    <asp:CheckBox ID="chk_all_month" runat="server" Text="Select All" OnCheckedChanged="chk_all_month_CheckedChanged" AutoPostBack="true" />
                                                </span>
                                                <br />
                                                <asp:Repeater ID="rp_months" runat="server" OnItemDataBound="rp_month_ItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_month_name" class="chkstle" runat="server" Text='<%#Eval("Month") %>' />
                                                        <asp:Label ID="lbl_value" runat="server" Visible="false" Text='<%#Bind("Value")%>'></asp:Label>
                                                        <asp:Label ID="lbl_month_name" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                                        <asp:Label ID="lbl_month_id" runat="server" Visible="false" Text='<%#Bind("Month_Id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:Repeater>



                                            </div>

                                            <div class="col-md-6" style="margin: 0px 0px 10px 0px;">
                                                <asp:Panel ID="pnl_fee_grid" runat="server" Visible="false">
                                                    <label for="validationCustom01" class="form-label Llabel">Fees Detail</label>
                                                    <br />
                                                    <asp:GridView ID="grd_fee" runat="server" CssClass="table table-bordered table-striped" OnRowDataBound="grd_fee_RowDataBound" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center; margin: 0px 0px 0px 0px;" ShowFooter="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Fees Head">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("content_id") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Fees Amount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_fee" runat="server" Style="width: 80px;" Text='<%#Eval("amount") %>' onkeypress="return isNumberKey(event)" Enabled="false" OnTextChanged="txt_fee_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                                  <FooterTemplate>
                                                                    <asp:Label ID="lbl_full_amount" runat="server"  ></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Disc. Amount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_disc_fee" runat="server" Style="width: 80px;" AutoPostBack="true" OnTextChanged="txt_disc_fee_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lbl_full_discount" runat="server"  ></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                    </asp:GridView>
                                                    <table class="table-bordered"  style="width: 100%; display:none">
                                                        <tr>
                                                            <td style="padding: 5px 5px; width: 245px; text-align: right;">Total Discount</td>
                                                            <td style="padding: 5px 5px; width: 161px; text-align: right;">
                                                                <asp:Label ID="lbl_totalmrp" runat="server"></asp:Label></td>
                                                            <td style="padding: 5px 5px; text-align: right;">
                                                                <asp:Label ID="lbl_ttl_disc" runat="server"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-12">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" />
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Discount on Admission/Annual Fees</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">

                                                 



                                                <div class="col-sm-3">
                                                    <label for="validationCustom01" class="find-dv-lbl">Hostel</label>
                                                    <asp:DropDownList ID="ddl_hostel_search" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Room Category</label>
                                                    <asp:DropDownList ID="ddl_room_category_search" runat="server" class="form-select find-dv-txtbx">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Month Name</label>
                                                    <asp:DropDownList ID="ddl_month_srch" runat="server" class="form-control find-dv-txtbx" ></asp:DropDownList>
                                                </div>




                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_fnd_by_class" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_by_class_Click" />
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
                                                                <span style="font-size: 14px; font-weight: bold;">Set Discount Month Wise
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                            </div>
                                                        </div>


                                                    </div>

                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    
                                                                    <th>Category</th>
                                                                    <th>Sub-Category</th>
                                                                    <th>Hostel</th>
                                                                    <th>Room catogery</th>
                                                                  
                                                                     <th>Fees Head</th>
                                                                    <th>Month</th>
                                                                    
                                                                    <th>Fees Amount</th>
                                                                    <th>Disc. Amount</th>
                                                                    <th>After Disc.</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_viewaddedfee" runat="server">
                                                                    <ItemTemplate>
                                                                        <asp:Panel ID="Panel1" runat="server">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                </td>
                                                                              
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_category" runat="server" Text='<%#Bind("category")%>'></asp:Label>
                                                                                </td>


                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="Label5" runat="server" Text='<%#Bind("sub_category")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_hostel_name" runat="server" Text='<%#Bind("Hostel_name")%>'></asp:Label>
                                                                                    
                                                                                </td>


                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_roomcatogery" runat="server" Text='<%#Bind("room_name_catogery")%>'></asp:Label>
                                                                                     <asp:Label ID="Label4" runat="server" Text='<%#Bind("parameter")%>' Visible="false"></asp:Label>
                                                                                </td>
                                                                                 

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content")%>'></asp:Label>
                                                                                </td>

 <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("amount")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Bind("disc_amount")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%#Bind("after_disc")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                                                    <asp:Label ID="lbl_category_id" runat="server" Text='<%#Bind("category_id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_sub_category_id" runat="server" Text='<%#Bind("sub_category_id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_fee_head_id" runat="server" Text='<%#Bind("content_id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_parameter_id" runat="server" Text='<%#Bind("parameter_id")%>' Visible="false"></asp:Label>

                                                                                    <asp:Label ID="lbl_Hostel_id" runat="server" Text='<%#Bind("Hostel_id")%>' Visible="false"></asp:Label>
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
            <!--end row-->
        </div>
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
