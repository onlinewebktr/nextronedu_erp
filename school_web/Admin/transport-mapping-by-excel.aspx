<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="transport-mapping-by-excel.aspx.cs" Inherits="school_web.Admin.transport_mapping_by_excel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Transport Mapping By Excel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value"; 
            if (confirm("Do you want to submit final?"))
            {
                confirm_value.value = "Yes";
                if (!isSubmitted)
                {
                    $('#<%=btn_final_submit.ClientID %>').val('Submitting.. Please Wait..');
                    isSubmitted = true;
                }
                else
                {
                    alert("Please Wait.. due to process is running"); 
                }
            }
            else
            {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
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
                <div class="breadcrumb-title pe-3">Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Transport Mapping with Student</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-8">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-xl-2">
                                        <div class="frms-row-wpr">
                                            <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-xl-4">
                                        <div class="frms-row-wpr">
                                            <label for="validationCustom01" class="form-label">Vehicle Name<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_bus_name" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_bus_name_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-xl-3">
                                        <div class="frms-row-wpr">
                                            <label for="validationCustom01" class="form-label">Transport Route<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_path_root" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_path_root_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-xl-3">
                                        <div class="frms-row-wpr">
                                            <label for="validationCustom01" class="form-label">Boarding Point<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_boarding_point" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_boarding_point_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-xl-3" style="display:none">
                                        <div class="frms-row-wpr">
                                            <asp:CheckBox ID="chk_is_first_chr_remove" Checked="false" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-xl-12">
                                        <div class="frms-row-wpr">
                                            <label for="validationCustom01" class="form-label">Transport Fee Details<sup>*</sup></label>
                                            <table class="table-bordered table " style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; font-size: 13px;">
                                                <tr>
                                                    <th>Boarding Point</th>
                                                    <th>KM Coverd</th>
                                                    <th>Transport Fee</th>
                                                    <th>Vacant Seat</th>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 3px;">
                                                        <asp:Label ID="lbl_boardingpoint" runat="server" Font-Bold="true">0</asp:Label></td>
                                                    <td style="padding: 3px;">
                                                        <asp:Label ID="lbl_kmcoverdby" runat="server" Font-Bold="true">0</asp:Label></td>
                                                    <td style="padding: 3px;">
                                                        <asp:Label ID="lbl_trasportfee" runat="server" Font-Bold="true">0</asp:Label></td>
                                                    <td style="padding: 3px;">
                                                        <asp:Label ID="lbl_vacant_seat" runat="server" Font-Bold="true">0</asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <label for="validationCustom01" class="form-label">Browse Excel(.csv file)<sup>*</sup></label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />
                                    </div>
                                    <div class="col-4">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Upload" OnClick="btn_Submit_Click" CssClass="btn btn-primary" Style="margin: 24px 0px 0px 0px; padding: 4px 10px;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-4">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate=""  style="display: none">
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Download with Student</label>
                                        <asp:DropDownList ID="ddl_download_with_fee" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6" id="sessinDV">
                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                        <asp:DropDownList ID="ddl_excel_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6" id="classDV">
                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                        <asp:DropDownList ID="ddl_excel_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12" id="exclempty" style="display: none">
                                        <a href="doc/Format-for-transport-mapping.csv" download="" class="btnbtn btn-1 btn-sep icon-cart">Download Excel Format</a>
                                    </div>
                        
                                    <div class="col-md-12" id="exclWithData" style="display: none">
                                        <asp:LinkButton ID="lnk_download_excel" OnClick="lnk_download_excel_Click" class="btnbtn btn-1 btn-sep icon-cart" runat="server">Download Excel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <asp:Panel ID="pnl_grid" runat="server" Visible="false">
                    <div class="col-xl-12">
                        <h6 class="mb-0 text-uppercase">Uploaded Student List</h6>
                        <hr />
                        <div class="card">
                            <div class="card-body">
                                <div class="table-responsive">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                        <div class="row">
                                            <div class="col-sm-12"> 
                                                <asp:Label ID="lbl_total1" runat="server" Text="Label"></asp:Label>
                                                <asp:GridView ID="grvExcelData" class="table table-striped table-bordered dataTable" runat="server" CssClass="table table-bordered" Width="100%">
                                                </asp:GridView>
                                                <div class="col-4">
                                                    <asp:Button ID="btn_final_submit" OnClientClick="Confirm()" runat="server" Text="Final Submit" CssClass="btn btn-primary" OnClick="btn_final_submit_Click" Style="margin: 0px 0px 0px 0px; padding: 6px 10px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            on_selection();
            $("#<%=ddl_download_with_fee.ClientID%>").on('change', function () {
                on_selection();
            })
        });

        function on_selection() {
            if ($('#<%= ddl_download_with_fee.ClientID %> option:selected').val() == "Yes") {
                $("#sessinDV").show();
                $("#classDV").show();
                $("#exclempty").hide();
                $("#exclWithData").show();
            }
            else {
                $("#sessinDV").hide();
                $("#classDV").hide();
                $("#exclempty").show();
                $("#exclWithData").hide();
            }
        }
    </script>
</asp:Content>
