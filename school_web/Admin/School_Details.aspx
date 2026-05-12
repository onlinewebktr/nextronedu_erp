<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="School_Details.aspx.cs" Inherits="school_web.Admin.College_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    School Details 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn {
            padding: 5px 20px;
            font-size: 15px;
        }

        .form-control {
            display: block;
            width: 100%;
            padding: 5px 10px;
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
                <div class="breadcrumb-title pe-3">About School</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">School Details</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="School Details"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-lg-12" id="storeDiv" runat="server">
                                        <div class="position-relative form-group">
                                            <label>School Name</label>
                                            <asp:TextBox ID="txt_Colleg_name" runat="server" CssClass="form-control" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="position-relative form-group">
                                            <label>Address</label>
                                            <asp:TextBox ID="txt_address1" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-lg-2">
                                        <div class="position-relative form-group">
                                            <label>Is 2nd Address</label>
                                            <asp:DropDownList ID="ddl_is_2d_branch" runat="server" CssClass="form-select">
                                                <asp:ListItem>No</asp:ListItem>
                                                <asp:ListItem>Yes</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-lg-10 hidden" id="address2">
                                        <div class="position-relative form-group">
                                            <label>Address2</label>
                                            <asp:TextBox ID="txt_address2" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>



                                    <div class="col-lg-10">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <div class="position-relative form-group">
                                                    <label>Are You Affilliated</label>
                                                    <asp:DropDownList ID="ddl_affilliated" runat="server" CssClass="form-select">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                        <asp:ListItem>Proposed</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2" id="aff_noDV">
                                                <div class="position-relative form-group">
                                                    <label>Affiliation No.</label>
                                                    <asp:TextBox ID="txt_affliaction_no" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="col-lg-3">
                                                <div class="position-relative form-group">
                                                    <label>School Regd No.</label>
                                                    <asp:TextBox ID="txt_college_no" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="col-lg-2">
                                                <div class="position-relative form-group">
                                                    <label>Is UDISE No.?</label>
                                                    <asp:DropDownList ID="ddl_udise_no" runat="server" CssClass="form-select">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2" id="udise_noDV">
                                                <div class="position-relative form-group">
                                                    <label>UDISE No.</label>
                                                    <asp:TextBox ID="txt_udise_no" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="position-relative form-group">
                                                    <label>Contact No.</label>
                                                    <asp:TextBox ID="txt_contactno" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="position-relative form-group">
                                                    <label>Email Id</label>
                                                    <asp:TextBox ID="txt_emailid" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="row">
                                            <div class="col-xl-6">
                                                <div class="position-relative form-group">
                                                    <label>Webiste</label>
                                                    <asp:TextBox ID="txt_wbesite" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xl-6">
                                                <div class="position-relative form-group">
                                                    <label>GST No.</label>
                                                    <asp:TextBox ID="txt_gstno" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="row">
                                            <div class="col-xl-6">
                                                <div class="position-relative form-group">
                                                    <label>PAN No.</label>
                                                    <asp:TextBox ID="txt_pan_no" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xl-6">
                                                <div class="position-relative form-group">
                                                    <label>Tan. No.</label>
                                                    <asp:TextBox ID="txt_tan_no" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="row">
                                            <div class="col-xl-6">
                                                <div class="position-relative form-group">
                                                    <label>ESTD On (year)</label>
                                                    <asp:TextBox ID="txt_estad_no" runat="server" class="form-control" onkeypress="return isNumberKey(event)" MaxLength="4"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                    <div class="col-lg-12" id="Div1" runat="server">
                                        <div class="position-relative form-group">
                                            <label>Trust Name</label>
                                            <asp:TextBox ID="txt_trustname" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="row">
                                            <div class="col-xl-6">
                                                <div class="position-relative form-group">
                                                    <label>PAN No.</label>
                                                    <asp:TextBox ID="txt_trust_pan_no" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xl-6">
                                                <div class="position-relative form-group">
                                                    <label>Mobile No.</label>
                                                    <asp:TextBox ID="txt_mobile_no_trust" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-2">
                                        <div class="position-relative form-group">
                                            <label>State</label>
                                            <asp:DropDownList ID="ddl_state" runat="server" CssClass="form-select"></asp:DropDownList>
                                        </div>
                                    </div>







                                    <div class="col-lg-12">
                                        <div class="row">
                                            <div class="col-xl-3">
                                                <div class="position-relative form-group">
                                                    <label>Admission Slip Prefix</label>


                                                    <asp:TextBox ID="txt_prifix" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xl-3">
                                                <div class="position-relative form-group">
                                                    <label>Annual fee Slip Prefix</label>
                                                    <asp:TextBox ID="txt_readmission_fee_slip" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="col-xl-3">
                                                <div class="position-relative form-group">
                                                    <label>Monthly Fee Slip Prefix</label>
                                                    <asp:TextBox ID="txt_monthly_reciptslip" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xl-3" style="display: none">
                                                <div class="position-relative form-group">
                                                    <label>Online Reg. Prefix</label>
                                                    <asp:TextBox ID="txt_online_reg_prifix" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="row">
                                            <div class="col-xl-4">
                                                <div class="position-relative form-group">
                                                    <label>Character Certificate Prefix</label>
                                                    <asp:TextBox ID="txt_Character_certificate_prefix" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xl-4">
                                                <div class="position-relative form-group">
                                                    <label>Transfer Certificate Prefix</label>
                                                    <asp:TextBox ID="txt_Transfer_certificate_prefix" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xl-4">
                                                <div class="position-relative form-group">
                                                    <label>Leaving Certificate </label>
                                                    <asp:TextBox ID="txt_Leaving_certificate_prefix" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="row">
                                            <div class="col-xl-3">
                                                <div class="position-relative form-group">
                                                    <label>Logo(file size 1 mb)</label>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                                                    <asp:Label ID="lbl_img_path" runat="server" Visible="false" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xl-6">
                                                <div class="position-relative form-group">
                                                    <asp:Image ID="Image1" runat="server" Style="padding: 2px; height: 133px; width: 197px;" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btn_Submit_Click" Style="padding: 7px 40px;" />

                                    </div>
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
            on_aff_selection();
            $("#<%=ddl_affilliated.ClientID%>").on('change', function () {
                on_aff_selection();
            })
        });

        function on_aff_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_affilliated.ClientID %> option:selected').val() == "Yes") {
                $("#aff_noDV").show();
            }
            else {
                $("#aff_noDV").hide();
            }
        }


        //========================
        $(document).ready(function () {
            on_scnd_add_selection();
            $("#<%=ddl_is_2d_branch.ClientID%>").on('change', function () {
                on_scnd_add_selection();
            })
        });

        function on_scnd_add_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_is_2d_branch.ClientID %> option:selected').val() == "Yes") {
                $("#address2").show();
            }
            else {
                $("#address2").hide();
            }
        }


        //UdiseNo
        $(document).ready(function () {
            on_udise_selection();
            $("#<%=ddl_udise_no.ClientID%>").on('change', function () {
                on_udise_selection();
            })
        });

        function on_udise_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_udise_no.ClientID %> option:selected').val() == "Yes") {
                $("#udise_noDV").show();
            }
            else {
                $("#udise_noDV").hide();
            }
        }
    </script>
</asp:Content>
