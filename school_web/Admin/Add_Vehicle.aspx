<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Add_Vehicle.aspx.cs" Inherits="school_web.Admin.Add_Vehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Vehicle 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>


    <script>
        $(function () {
            $("#<%=txt_Vehicle_Registration_date.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");

        });

        $(function () {

            $("#<%=txt_vechile_insurance_expirydate.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_Pollutionexpirydate.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_body_Fitness_Expiry.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_driver_licence_expiry.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

    </script>

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



    function sync(textbox) {
        document.getElementById('txt_Vehicle_Registration').value = textbox.value;
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
                <div class="breadcrumb-title pe-3">Transportation</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Vehicle</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" "></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">

                                    <div class="row">
                                        <div class="col-lg-4">
                                            <ul class="sub-pag-menu-ul sub-pag-menu-ul-mrgn">

                                                <li><a id="a1" href="Transport_List.aspx" runat="server" class="sub-mnu-p-a-active btn-success" style="background: #ff0000; border: 1px solid #001017;">Back</a></li>

                                            </ul>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label" style="font-weight: 500; font-size: 1rem; background: #ffd700; padding: 5px 5px 5px 5px; width: 100%; text-align: center; color: #000;">
                                                Vehicle Information
                                           
                                            </label>
                                        </div>
                                    </div>


                                    <div class="col-lg-3">
                                        <div class="position-relative form-group">
                                            <label>Vehicle Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_Vehicle_name" Display="Dynamic" runat="server" ErrorMessage="Enter Vehicle Name" ValidationGroup="a"></asp:RequiredFieldValidator></sup></label>
                                            <asp:TextBox ID="txt_Vehicle_name" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="position-relative form-group">
                                            <label>Registration No.<sup><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_bus_no_vachileno" Display="Dynamic" runat="server" ErrorMessage="Enter Registration No. " ValidationGroup="a"></asp:RequiredFieldValidator></sup></label>
                                            <asp:TextBox ID="txt_bus_no_vachileno" runat="server" CssClass="form-control" Style="text-transform: uppercase;"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-xl-3">
                                        <div class="position-relative form-group">
                                            <label>Vehicle Registration Date<sub>  </sub></label>
                                            <asp:TextBox ID="txt_Vehicle_Registration_date" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-xl-3">
                                        <div class="position-relative form-group">
                                            <label>Vehicle Registration Expiry Date<sup></sup> </label>
                                            <asp:TextBox ID="txt_Registration_Expiry_date" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                    <div class="row">
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Vehicle Insurance Expiry Date<sup></sup> </label>
                                                <asp:TextBox ID="txt_vechile_insurance_expirydate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Pollution Expiry Date<sub></sub> </label>
                                                <asp:TextBox ID="txt_Pollutionexpirydate" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Body Fitness Expiry Date<sup>  </sup></label>
                                                <asp:TextBox ID="txt_body_Fitness_Expiry" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Vehicle Type </label>
                                                <asp:DropDownList ID="ddl_Vehicle_type" runat="server" class="form-select find-dv-txtbx">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>AC</asp:ListItem>
                                                    <asp:ListItem>Non/AC</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>No. of Seat<sub>*<asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txt_no_seat" Display="Dynamic" runat="server" ErrorMessage="No. of Seat" ValidationGroup="a"></asp:RequiredFieldValidator></sub> </label>
                                                <asp:TextBox ID="txt_no_seat" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>



                                    <div class="col-lg-4" style="display: none">
                                        <div class="position-relative form-group">
                                            <label>Vehicle Route<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_vehicle_rute" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator></sup></label>
                                            <asp:TextBox ID="txt_vehicle_rute" runat="server" CssClass="form-control">A1</asp:TextBox>
                                        </div>
                                    </div>




                                    <div class="row" style="margin-top: 0px;">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label" style="font-weight: 500; font-size: 1rem; background: #ffd700; padding: 5px 5px 5px 5px; width: 100%; text-align: center; color: #000;">
                                                Driver & Warden  Information
                                           
                                            </label>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                    <div class="row">
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Transport Owner Type </label>
                                                <asp:DropDownList ID="ddl_Transport" runat="server" class="form-select find-dv-txtbx">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Own Transport</asp:ListItem>
                                                    <asp:ListItem>Third Party</asp:ListItem>

                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Vehicle Owner Name<sup></sup> </label>
                                                <asp:TextBox ID="txt_Vehicle_Owner_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>
                                                    Vehicle Owner Mobile No.<sub> 
                                                    </sub>
                                                </label>
                                                <asp:TextBox ID="txt_Vehicle_Owner_mobile_no" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Vehicle Driver Name<sup></sup> </label>
                                                <asp:TextBox ID="txt_Vehicle_drivername" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                    <div class="row">

                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Driver Mobile No.<sub> </sub></label>
                                                <asp:TextBox ID="txt_driver_mobile_no" runat="server" MaxLength="10" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Driver Licence No.<sup> </sup></label>
                                                <asp:TextBox ID="txt_driver_licence" runat="server" CssClass="form-control" Style="text-transform: uppercase;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Driver licence expiry Date <sup></sup></label>
                                                <asp:TextBox ID="txt_driver_licence_expiry" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Vehicle Warden<sub>*</sub></label>
                                                <asp:DropDownList ID="ddl_Vehicle_Warden" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_Vehicle_Warden_SelectedIndexChanged">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>






                                    <div class="row" id="Warden1" runat="server" visible="false">
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Vehicle Warden Name<sup> </sup></label>
                                                <asp:TextBox ID="txt_Vehicle_Warden_name" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Warden Mobile No.<sub> </sub></label>
                                                <asp:TextBox ID="txt_Warden_mobile_no" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Aadhar No. </label>
                                                <asp:TextBox ID="txt_Warden_aadhar_no" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Warden Address <sup></sup></label>
                                                <asp:TextBox ID="txt_Warden_address" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>





                                    <div class="row" style="margin-top: 5px;">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label" style="font-weight: 500; font-size: 1rem; background: #ffd700; padding: 5px 5px 5px 5px; width: 100%; text-align: center; color: #000;">
                                                Upload Document File Type:jpg,word,pdf(300kb)
                                           
                                            </label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">


                                            <asp:GridView ID="grd_doc" runat="server"
                                                Style="text-align: left; font-family: arial; font-size: 12px; height: auto; margin-top: 4px; margin-bottom: 16px; margin-right: 0px;" CssClass="table table-bordered"
                                                Width="100%"
                                                AutoGenerateColumns="False" OnRowDataBound="grd_doc_RowDataBound">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.L. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_document_name" runat="server" Font-Names="Arial" Text='<%#Bind("Doc_name") %>'></asp:Label>
                                                            <asp:Label ID="lbl_Description_ID_No" runat="server" Font-Names="Arial" Text='<%#Bind("Doc_id") %>' Visible="false"></asp:Label>

                                                            <asp:Label ID="lbl_mandatory" runat="server" Font-Names="Arial" Text='*' ForeColor="Red" Visible="false" Style="display: none"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:FileUpload runat="server" ID="fu_file1" Style="float: left;" />
                                                            <asp:ImageButton runat="server" ID="btn_upload" ToolTip="Upload Certifiacte" rel="tooltip" ImageUrl="~/images/uacc.png" Height="32px" Width="116px" Style="float: left;" OnClick="btn_upload_Click1"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="461px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <a id="a1" runat="server" style="padding: 5px 11px 5px 11px; background-color: #03ce2f; font-family: ebrima; font-size: 12px; color: Black; text-decoration: none;"
                                                                target="_blank">Download</a>


                                                            <asp:Button ID="btn_delete" runat="server" Text="Delete" OnClick="btn_delete_Click" Style="padding: 3px 11px 3px 11px; background-color: #ff0000; font-family: ebrima; font-size: 12px; color: Black; text-decoration: none;" OnClientClick="return confirm('Are you sure you want to delete this?');" />




                                                        </ItemTemplate>




                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>


                                        </div>
                                    </div>





                                    <div class="col-md-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Save" ValidationGroup="a" CssClass="btn btn-primary" OnClick="btn_Submit_Click" Style="padding: 7px 40px;" />


                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" Style="padding: 7px 15px 7px 15px;" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
