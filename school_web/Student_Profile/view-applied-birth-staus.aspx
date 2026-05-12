<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="view-applied-birth-staus.aspx.cs" Inherits="school_web.Student_Profile.view_applied_birth_staus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Applied Birth Certificate Status
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
                    <h4 class="card-title">View Birth Certificate</h4>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="grd-wpr">
                                        <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Adm. No.</th>
                                                    <th>Apply Date</th>
                                                    <th>Apply Remarks</th>
                                                    <th>Attachment File</th>
                                                    <th>Status</th>
                                                    <th>Reply Date</th>
                                                    <th>Reply Remarks</th>
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
                                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_applydate_time" runat="server" Text='<%#Bind("Apply_date_time1")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Apply_Amessage" runat="server" Text='<%#Bind("Apply_message")%>' Style="word-break: break-all"></asp:Label>
                                                            </td>
                                                             <td>
                                                                <asp:Label ID="lbl_Attachment" Visible="false" runat="server" Text='<%#Bind("Attachment")%>'></asp:Label>
                                                                <a id="a1" runat="server" href='<%#Eval("Attachment") %>' download target="_blank" style="display: block; padding: 0px 0px 0px 0px; font-size: 22px; color: #0066CC; text-decoration: none; float: left;"><i class="fa fa-download" aria-hidden="true"></i></a>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Status" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_replydate_time_show" runat="server"></asp:Label>

                                                                <asp:Label ID="lbl_Reply_datetime" runat="server" Text='<%#Bind("Reply_datetime")%>' Visible="false"></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Reply_remarks" runat="server" Text='<%#Bind("Reply_remarks")%>'></asp:Label>
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
</asp:Content>
