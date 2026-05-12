<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Upload_bulk_discount_for_student_wise.aspx.cs" Inherits="school_web.Admin.Upload_bulk_discount_for_student_wise" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Upload Discount For Admission/Annual Fee
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Confirm() {

            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to submit final?")) {
                confirm_value.value = "Yes";
                if (!isSubmitted) {
                    $('#<%=btn_final_submit.ClientID %>').val('Submitting.. Please Wait..');
                    isSubmitted = true;

                }
                else {

                    alert("Please Wait.. due to process is running");
                    // return false;
                }
            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
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
                <div class="breadcrumb-title pe-3">Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Upload Discount For Admission/Annual/Monthly Fee</li>
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

                                    <div class="col-sm-2">
                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                    </div>

                                    <div class="col-sm-2" style="display: none">
                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                    </div>


                                    <div class="col-sm-2">
                                        <label for="validationCustom01" class="find-dv-lbl">School Type </label>
                                        <asp:DropDownList ID="ddl_schooltype" runat="server" class="form-control find-dv-txtbx">
                                            <asp:ListItem>Days</asp:ListItem>
                                            <asp:ListItem>Hosteler</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-2">
                                        <label for="validationCustom01" class="find-dv-lbl">Discount For </label>
                                        <asp:DropDownList ID="ddl_fnd_fee_group" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_fnd_fee_group_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-sm-2" id="monthname" runat="server" visible="false">
                                        <label for="validationCustom01" class="find-dv-lbl">Month Name</label>
                                        <asp:DropDownList ID="ddl_month_name" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                    </div>


                                    <div class="col-sm-2">
                                        <%--<label for="validationCustom01" class="find-dv-lbl">Download CSV Format </label>--%>
                                        <asp:Button ID="btn_getcsvexcel" runat="server" Text="Download CSV Format" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_getcsvexcel_Click" Style="margin: 21px 0px 0px 0px; padding: 6px 10px;" />
                                    </div>

                                    <div class="row" id="panlfileupload" runat="server" visible="true">
                                        <div class="col-md-3">
                                            <label for="validationCustom01" class="form-label">Browse Excel(.csv file)<sup>*</sup></label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />


                                        </div>

                                        <div class="col-4">
                                            <asp:Button ID="btn_Submit" runat="server" Text="Upload" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click"
                                                Style="margin: 21px 0px 0px 0px; padding: 6px 10px;" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnl_grid" runat="server" Visible="false">
                    <div class="col-xl-12">
                        <h6 class="mb-0 text-uppercase">Uploaded discount for :-
                        <asp:Label ID="lbl_type" runat="server"></asp:Label></h6>
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


                                                    <asp:Button ID="btn_final_submit" OnClientClick="Confirm()" runat="server" Text="Final Submit" CssClass="btn btn-primary" OnClick="btn_final_submit_Click" Style="margin: 29px 0px 0px 0px; padding: 6px 10px;" />
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
</asp:Content>
