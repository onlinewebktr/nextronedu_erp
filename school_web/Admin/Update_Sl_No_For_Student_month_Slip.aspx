<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Update_Sl_No_For_Student_month_Slip.aspx.cs" Inherits="school_web.Admin.Update_Sl_No_For_Student_month_Slip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Update Bill No.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Update Slip No.</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">


                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">Update Slip No</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">



                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>

                                                        <th>Running Slip No.</th>

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
                                                                    <asp:Label ID="lbl_new_slip_no" runat="server" Text='<%#Bind("slip_no")%>'></asp:Label>
                                                                </td>


                                                                <td style="text-align: left;">
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                                </td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
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
        <!--end row-->
    </div>





    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />

    <div id="myModal1" class="modal fade" role="dialog" style="z-index: 9999;background: #0000005e;">
        <div class="modal-dialog" style="max-width: 865px; margin-top: 76px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="width: 100%;">Update Slip no.</h5>


                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-secondary noPrint">Close</a>
                </div>
                <div class="modal-body" style="max-height: 400px; overflow: auto;">
                    <div class="row" id="tdprint1">
                        <table style="width: 100%;" class=" table">
                            <tr>
                                <td colspan="5">
                                    <table style="width: 50%;margin: 0px auto;
    border: 1px solid #000;" class=" table">
                                        <tr>
                                            <td>Enter New Slip No <sup>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txt_newslipno" ValidationGroup="bb">
                                                </asp:RequiredFieldValidator></sup>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_newslipno" onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server"></asp:TextBox>
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
                                            </td>




                                        </tr>
                                        <tr>
                                            <td>Enter Remarks <sup>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txt_remarks" ValidationGroup="bb">
                                                </asp:RequiredFieldValidator></sup>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_remarks" CssClass="form-control" TextMode="MultiLine" runat="server" Style="height: 120px;"></asp:TextBox>




                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <asp:Button ID="btn_attribute_name" runat="server" Text="Add" OnClick="btn_attribute_name_Click" CssClass="btn btn-primary" ValidationGroup="bb" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="5">

                                    <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info" data-page-length='1500'>
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Old Bill No.</th>
                                                <th>Updated Bill No.</th>
                                                <th>Updated Date</th>
                                                <th>Updated By</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rd_view_history" runat="server">
                                                <ItemTemplate>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>


                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_old_slip_no" runat="server" Text='<%#Bind("Old_Slip_no")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_new_slip_no" runat="server" Text='<%#Bind("New_slip_no")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("date_time1")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_updated_by" runat="server" Text='<%#Bind("update_by")%>'></asp:Label>
                                                        </td>


                                                    </tr>

                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </td>

                            </tr>

                        </table>

                    </div>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function openModal1() {
            $('#myModal1').modal('show');

        }


    </script>
</asp:Content>
