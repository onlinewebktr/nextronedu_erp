<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="Book_TakenHistory.aspx.cs" Inherits="school_web.Student_Profile.Book_TakenHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Book Taken History
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
                    <h4 class="card-title">Book Taken History</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                           

                            <div class="col-lg-2 col-width-50 pads-rght-5">
                                <label for="validationCustom01" class="lebelheadpp">Date From</label>
                                <div class="clndr-dv-wpr" style="position: relative;">
                                    <asp:TextBox ID="txt_from_date" runat="server" class="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-lg-2 col-width-50 pads-lft-5">
                                <label for="validationCustom01" class="lebelheadpp">Date To</label>
                                <div class="clndr-dv-wpr" style="position: relative;">
                                    <asp:TextBox ID="txt_to_date" runat="server" class="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary fnd-btnmrgn" OnClick="btn_find_Click" />
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
                                                                    <th>Book Name</th>
                                                                    <th>Book Issue No.</th>
                                                                    <th>Issue Date</th>
                                                                    <th>Due Date</th>
                                                                    <th>Book Return No.</th>
                                                                    <th>Return Date</th>
                                                                    <th>Book Status</th>
                                                                     
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_view" runat="server"  >
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                             <asp:Label ID="lbl_NameOfBook" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                               <asp:Label ID="lbl_transaction_no" runat="server" Text='<%#Bind("transaction_no")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                              <asp:Label ID="lbl_book_issue_date" runat="server" Text='<%#Bind("issue_date") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                   <asp:Label ID="lbl_book_due_date" runat="server" Text='<%#Bind("due_date") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                  <asp:Label ID="lbl_Book_reurn_slip_id" runat="server" Text='<%#Bind("Book_reurn_slip_id")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                               <asp:Label ID="lbl_returned_date" runat="server" Text='<%#Bind("returned_date")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                               <asp:Label ID="lbl_Book_status" runat="server" Text='<%#Bind("Book_status")%>'></asp:Label>
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
