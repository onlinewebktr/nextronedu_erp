<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="my-leave.aspx.cs" Inherits="school_web.Student_Profile.my_leave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    My Leave
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container"> 
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>


            <div class="main-card mb-3 card">
                <div class="card-header">
                    <h4 class="card-title">My Leave</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                <label for="validationCustom01" class="lebelheadpp">Leave Status</label>
                                <asp:DropDownList ID="ddl_Status" runat="server" class="form-control form-control-custom">
                                    <asp:ListItem Value="0">ALL</asp:ListItem>
                                    <asp:ListItem Value="1">Pending</asp:ListItem>
                                    <asp:ListItem Value="2">Approved</asp:ListItem>
                                    <asp:ListItem Value="3">Rejected</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-2 col-width-50 pads-rght-5">
                                <label for="validationCustom01" class="lebelheadpp">Apply Date From</label>
                                <div class="clndr-dv-wpr" style="position: relative;">
                                    <asp:TextBox ID="txt_from_date" runat="server" class="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-lg-2 col-width-50 pads-lft-5">
                                <label for="validationCustom01" class="lebelheadpp">Apply Date To</label>
                                <div class="clndr-dv-wpr" style="position: relative;">
                                    <asp:TextBox ID="txt_to_date" runat="server" class="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary fnd-btnmrgn" OnClick="btn_find_Click"   />
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <table class="counts-tbls">
                                    <thead>
                                        <tr>
                                            <th style="width: auto;">Total Leave :
                                    <asp:Label ID="lbl_total_leave" runat="server"></asp:Label>
                                            </th>

                                            <th style="width: auto;">Pending Leave : 
                                    <asp:Label ID="lbltotal_pending" runat="server">0</asp:Label>
                                            </th>
                                        </tr>

                                        <tr>
                                            <th style="width: auto;">Approved Leave : 
                                    <asp:Label ID="lbltotal_approved" runat="server">0</asp:Label>
                                            </th>

                                            <th style="width: auto;">Rejected Leave : 
                                    <asp:Label ID="lbl_rejected_leave" runat="server">0</asp:Label>
                                            </th>
                                        </tr>
                                    </thead>
                                </table>
                                <div class="table-responsive">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div class="grd-wpr">
                                                    <div id="content">
                                                        <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Status</th>
                                                                    <th>Apply Date</th>
                                                                    <th>Leave From</th>
                                                                    <th>Leave To</th>
                                                                    <th>No of Days</th>
                                                                    <th>Subject</th>
                                                                    <th>Details</th>
                                                                    <th>Updated Remark</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("Apply_Date")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("Leave_From")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label7" runat="server" Text='<%#Bind("Leave_To")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Total_day_count" runat="server" Text='<%#Bind("Total_day_count")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Cause")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label3" runat="server" Style="text-align: left; width: 100%; line-height: 18px;"
                                                                                    Text='<%#Bind("Details")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label4" runat="server" Style="text-align: left; width: 100%; line-height: 18px;"
                                                                                    Text='<%#Bind("Updated_remark")%>'></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="Label5" runat="server" Style="text-align: left; width: 100%; font-weight: 600; line-height: 18px;"
                                                                                    Text='<%#Bind("Updated_date")%>'></asp:Label>
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
                </div>
            </div>
        </div>
        <!--end row-->
    </div>

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>

    <script>
        $(function () {
            $("#<%=txt_from_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_to_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <link href="assets/css/calender-modified.css" rel="stylesheet" />
</asp:Content>
