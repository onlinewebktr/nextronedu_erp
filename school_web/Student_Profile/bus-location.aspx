<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="bus-location.aspx.cs" Inherits="school_web.Student_Profile.bus_location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Transport
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
            <div class="headingtablee">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="main-card mb-3 card">
                            <div class="card-header">
                                <h4 class="card-title">Transport</h4>
                            </div>
                            <div class="card-body">
                                <div class="grd-wpr">
                                    <div id="content">


                                        <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Vehicle Name</th>
                                                    <th>Van No.</th>
                                                    <th>Pick-Up Point</th>
                                                    <th>KM</th>
                                                    <th>Driver Name</th>
                                                    <th>Driver Mobile No.</th>
                                                    <th>Warden Name</th>
                                                    <th>Warden Mobile No.</th>

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
                                                                <asp:Label ID="lbl_transport_name" runat="server" Text='<%#Bind("transport_name")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Bus_no" runat="server" Text='<%#Bind("Bus_no")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Boarding_Point" runat="server" Text='<%#Bind("Boarding_Point")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_KM" runat="server" Text='<%#Bind("KM")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Bus_driver_name" runat="server" Text='<%#Bind("Bus_driver_name")%>'></asp:Label>
                                                            </td>


                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Bus_driver_mobileno" runat="server" Text='<%#Bind("Bus_driver_mobileno")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Warden_Name" runat="server" Text='<%#Bind("Warden_Name")%>'></asp:Label>
                                                            </td>
                                                             <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Warden_Mobile_No" runat="server" Text='<%#Bind("Warden_Mobile_No")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>




                                        <asp:Panel ID="pnl_location_view" runat="server" Visible="false">
                                            <div class="iframe-wpr">
                                                <iframe runat="server" id="iframES" style="width: 100%; height: 500px"></iframe>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnl_transportation_not_taken" runat="server" Visible="false" Style="text-align: center; min-height: 500px; padding: 50px 0px 0px 0px;">
                                            <i class="fa fa-smile-o" aria-hidden="true" style="font-size: 52px; color: #f900a1; margin: 80px 0px 5px 0px;"></i>
                                            <asp:Label ID="lbl_messages" runat="server" Text="Transportation has not taken." Style="text-align: center; float: left; width: 100%;"></asp:Label>
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
</asp:Content>
