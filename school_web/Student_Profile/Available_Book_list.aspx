<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="Available_Book_list.aspx.cs" Inherits="school_web.Student_Profile.Available_Book_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Available Book
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
                    <h4 class="card-title">Available Book</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">


                            <div class="col-lg-2 col-width-50 pads-rght-5">
                                <label for="validationCustom01" class="lebelheadpp">Class</label>

                                <div class="clndr-dv-wpr" style="position: relative;">
                                    <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>


                           
                        </div>



                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

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
                                                                    <th>Book Catogery</th>
                                                                    <th>Book Name</th>
                                                                    <th>Author Name</th>
                                                                    <th>Publication</th>
                                                                    <th>Publication Year</th>
                                                                    <th>ISBN Num</th>
                                                                    <th>Book Status</th>

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
                                                                                <asp:Label ID="lbl_Book_Category" runat="server" Text='<%#Bind("Book_Category")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_NameOfBook" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_AuthorName" runat="server" Text='<%#Bind("AuthorName")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Publication" runat="server" Text='<%#Bind("Publication")%>'></asp:Label>
                                                                            </td>
                                                                              <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_PublicationYear" runat="server" Text='<%#Bind("PublicationYear")%>'></asp:Label>
                                                                                  </td>

                                                                           
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_ISBN_Num" runat="server" Text='<%#Bind("ISBN_Num")%>'></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_BookStatus" runat="server" Text='<%#Bind("Book_Status")%>'></asp:Label>
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
</asp:Content>
