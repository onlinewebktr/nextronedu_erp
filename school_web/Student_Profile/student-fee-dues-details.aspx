<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="student-fee-dues-details.aspx.cs" Inherits="school_web.Student_Profile.student_fee_dues_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Student Dues Fee Details
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
                    <h4 class="card-title">Monthly Dues List</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                               
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div class="grd-wpr">
                                                    <div id="content">
                                                        <div class="fnd-box-wpr-inr" style="display: none">

                                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Month">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Month" runat="server" Text='<%#Bind("Month") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>


                                                        <asp:Panel ID="pnl_month_wise_fee_details" runat="server" Visible="false">
                                                      
                                                            <table style="width: 100%;" class="table table-hover table-bordered">

                                                                <tr>
                                                                    <th>Month</th>
                                                                    <th>Fees Head</th>
                                                                    <th>Fees Amt.</th>
                                                                    <th>Dis.</th>
                                                                    <th>Paid Prev.</th>
                                                                    <th>Payable</th>
                                                                </tr>


                                                                <asp:Repeater ID="rp_fee_details" runat="server" OnItemDataBound="rp_fee_details_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr id="row" runat="server">
                                                                            <td>
                                                                                <asp:Label ID="lbl_mnth" runat="server" Text='<%#Bind("months") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblcontent" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("disc_amount") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_pre_paid" runat="server" Text='<%#Bind("previously_paid") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_tot_pble" runat="server" Text='<%#Bind("total_payable") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>

                                                                <tr>
                                                                    <th colspan="2">Total :
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lbl_fee_amount" runat="server" Text=""></asp:Label></th>
                                                                    <th>
                                                                        <asp:Label ID="lbl_discount" runat="server" Text=""></asp:Label></th>
                                                                    <th>
                                                                        <asp:Label ID="lbl_paid_prev" runat="server" Text=""></asp:Label></th>
                                                                    <th>
                                                                        <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label></th>

                                                                </tr>
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
        </div>
        <!--end row-->
    </div>

</asp:Content>
