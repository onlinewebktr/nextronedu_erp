<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="New_Book_Entry_Using_Excel.aspx.cs" Inherits="school_web.Library_Admin.New_Book_Entry_Using_Excel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Upload Book Using Excel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Confirm() {

            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to submit ?")) {
                confirm_value.value = "Yes";

            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }


        function save_data() {
            var valsubmit = $('#<%=btn_Submit_final.ClientID %>').val();
            if (valsubmit == "Final Submit") {

                $('#<%=btn_Submit_final.ClientID %>').val('Submitting.. Please Wait..');

                Confirm();
                document.getElementById("<%=btn_Submit_final.ClientID %>").click();

            }



            else {


                alert("Already submitted ")
            }

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
                <div class="breadcrumb-title pe-3">Book Management</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Upload Book Using Excel</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=""></asp:Label>
                    <hr />
  
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-3">

                                        <label for="validationCustom01" class="form-label">Browse Excel(.csv file)<sup>*</sup></label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />
                                        <a href="../Admin/doc/book_upload.csv" download="" style="margin: 5px 0px 0px 0px; float: left; font-weight: 500;">Download Excel Format</a>
                                        <%--                                        <a href="doc/student_upload_excel_manual.xlsx" download="" style="margin: 5px 0px 0px 0px; float: left; font-weight: 500;">Download User Manual</a>--%>
                                    </div>

                                    <div class="col-4">
                                        <asp:CheckBox ID="chk_book_id_manually" runat="server" Text="Is Book Id Manually in CSV Format." />

                                        <asp:Button ID="btn_upload" runat="server" Text="Upload" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_upload_Click" Style="margin: 23px 0px 0px 0px; padding: 4px 10px;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Book Details</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Panel ID="pnl_grid" runat="server" Visible="false">
                                                <asp:Label ID="lbl_total" runat="server" Text="Label"></asp:Label>
                                                <asp:GridView ID="grvExcelData" class="table table-striped table-bordered dataTable" runat="server" CssClass="table table-bordered" Width="100%">
                                                </asp:GridView>
                                                <asp:GridView ID="GridView2" class="table table-striped table-bordered dataTable" OnRowDataBound="GridView2_RowDataBound" runat="server" CssClass="table table-bordered" Width="100%">
                                                </asp:GridView>

                                                <div class="col-4" id="finalsubmitpnl" runat="server" visible="false">

                                                    <a onclick="save_data()" class="btn btn-primary" style="width: 155px; margin: 0px 0px 0px 2px;">Submit</a>

                                                    <asp:Button ID="btn_Submit_final" runat="server" Text="Final Submit" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_final_Click1" Style="display: none" />

                                                </div>
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
        <!--end row-->
    </div>
</asp:Content>
