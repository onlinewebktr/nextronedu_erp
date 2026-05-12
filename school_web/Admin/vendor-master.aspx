<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="vendor-master.aspx.cs" Inherits="school_web.Admin.vendor_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Vendor Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">General Expense</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active">Vendor Master</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Vendor Master"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation">
                                    <div class="col-md-3">
                                        <label class="form-label">Company Name<sup>*</sup></label>
                                        <asp:TextBox ID="txt_company_name" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Contact Person Name<sup>*</sup></label>
                                        <asp:TextBox ID="txt_contact_person_name" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Mobile No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_mobile" runat="server" class="form-control" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Address<sup>*</sup></label>
                                        <asp:TextBox ID="txt_address" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Pincode<sup>*</sup></label>
                                        <asp:TextBox ID="txt_pincode" runat="server" class="form-control" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">District<sup>*</sup></label>
                                        <asp:TextBox ID="txt_district" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">State<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_state" runat="server" class="form-select"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label class="form-label">Vendor type<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_vendor_Type" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Is GST No.<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_is_gst" runat="server" class="form-select">
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" id="gstnosdV">
                                        <label class="form-label">GST No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_gst_no" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3" id="">
                                        <label class="form-label">Is PAN No.<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_is_pan_no" runat="server" class="form-select">
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3"  id="pannodV">
                                        <label class="form-label">PAN No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_pan_no" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
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
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        //============================
        $(document).ready(function () {
            on_is_gst_selection();
            $("#<%=ddl_is_gst.ClientID%>").on('change', function () {
                on_is_gst_selection();
            })
        });

        function on_is_gst_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_is_gst.ClientID %> option:selected').val() == "Yes") {
                $("#gstnosdV").show();
            }
            else {
                $("#gstnosdV").hide();
            }
        }


        //============================
        $(document).ready(function () {
            on_is_pan_selection();
            $("#<%=ddl_is_pan_no.ClientID%>").on('change', function () {
                on_is_pan_selection();
            })
        });

        function on_is_pan_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_is_pan_no.ClientID %> option:selected').val() == "Yes") {
                $("#pannodV").show();
            }
            else {
                $("#pannodV").hide();
            }
        }
    </script>
</asp:Content>
