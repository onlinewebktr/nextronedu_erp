<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="Edit_Book.aspx.cs" Inherits="school_web.Library_Admin.Edit_Book" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Edit Book
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%=txt_AuthorName.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Edit_Book.aspx/Getbookauthername',
                        data: "{ 'authername': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });

        $(function () {
            $("#<%=txt_itemcode.ClientID%>").autocomplete({
                  source: function (request, response) {
                      $.ajax({
                          url: 'Issue_Book.aspx/Getitem_code',
                          data: "{ 'BookId': '" + request.term + "'}",
                          dataType: "json",
                          type: "POST",
                          contentType: "application/json; charset=utf-8",
                          success: function (data) {
                              if (data.d.length > 0) {
                                  response($.map(data.d, function (item) {
                                      return {
                                          label: item
                                      };
                                  }))
                              } else {
                                  response([{ label: 'No results found.' }]);
                              }
                          }
                      });
                  },
                  select: function (e, u) {
                      if (u.item.val == -1) {
                          return false;
                      }
                  }
              });
          });
    </script>

      <script type="text/javascript">
          function PrintPanel() {
              var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
              var printWindow = window.open('', '', 'height=400,width=800');
              printWindow.document.write('<html><head>');
              printWindow.document.write('</head><body>');
              printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
              printWindow.document.write(panel.innerHTML);
              printWindow.document.write('</body ></html>');
              printWindow.document.close();
              setTimeout(function () {
                  printWindow.print();
              }, 500);
              return false;
          }
          jQuery(".sn-bill-head-text").fitText(0.38);
      </script>
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .home-grph-wpr {
            width: 114%;
        }
    </style>
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
                            <li class="breadcrumb-item"><a href="Lib_Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Edit Book’s List</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Select Class </label>
                                                        <asp:DropDownList ID="ddl_class_wise" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_wise_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Book Status </label>
                                                        <asp:DropDownList ID="ddl_book_status" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_book_status_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Location Wise </label>
                                                        <asp:DropDownList ID="ddl_lcation" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_lcation_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">OR</div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Author Name</label>
                                                        <asp:TextBox ID="txt_AuthorName" runat="server" class="form-control"></asp:TextBox>
                                                    </div>


                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find_auther_wise" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_auther_wise_Click" />
                                                    </div>
                                                  <div class="clearfix"></div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Item Code</label>
                                                        <asp:TextBox ID="txt_itemcode" runat="server" class="form-control"></asp:TextBox>
                                                    </div>


                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_itemcode" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_itemcode_Click" />
                                                    </div>
                                                    <div class="col-sm-3">


                                                        <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click"  Visible="false" class="btn btn-primary find-dv-btn" style="margin-left:10px;">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">

                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">


                                                        <div class="pgslry-head-div head">

                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                            </div>
                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                                <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                                                </h1>

                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                    &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                </div>
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <span style="font-size: 14px; font-weight: bold;">Book List<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                </div>
                                                            </div>


                                                        </div>

                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Item Code</th>
                                                                        <th>Book Status</th>
                                                                        <th>Name Of Book</th>
                                                                        <th>Book Type</th>
                                                                        <th>Class</th>
                                                                        <th>Subject</th>
                                                                        
                                                                        <th>Author Name</th>
                                                                        <th>Publication</th>
                                                                        <th>Volume Name</th>
                                                                        <th>Edition</th>
                                                                        <th>Location</th>
                                                                        <th>Sub-Location</th>
                                                                        <th>ISBN No.</th>
                                                                        <th>Invoice no.</th>
                                                                         <th>Qty.</th>
                                                                        <th>Price</th>
                                                                        <th>Action</th>

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
                                                                                          <asp:Label ID="lbl_BookId" runat="server" Text='<%#Bind("BookId")%>'  ></asp:Label>
                                                                                       
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_BookStatus" runat="server" Text='<%#Bind("Book_Status")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_TypeName" runat="server" Text='<%#Bind("booktype")%>'></asp:Label>
                                                                                    </td>
                                                                                     <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_classname" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Book_Category" runat="server" Text='<%#Bind("Book_Category")%>'></asp:Label>
                                                                                    </td>

                                                                                   

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label5" runat="server" Text='<%#Bind("AuthorName")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Publication" runat="server" Text='<%#Bind("Publication")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_EnterVolumePart" runat="server" Text='<%#Bind("EnterVolumePart")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Edition" runat="server" Text='<%#Bind("Edition")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Location" runat="server" Text='<%#Bind("location_new")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Sub_Location")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label14" runat="server" Text='<%#Bind("ISBN_Num")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label15" runat="server" Text='<%#Bind("InvoiceNo")%>'></asp:Label>
                                                                                    </td>
                                                                                     <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("EnterQuantity")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label16" runat="server" Text='<%#Bind("Price")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                            <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                                href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                                <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                                    <i class="bx bx-grid-horizontal"></i>
                                                                                                </div>
                                                                                            </a>
                                                                                            <ul class="dropdown-menu dropdown-menu-end">
                                                                                                <li>
                                                                                                    <a class="dropdown-item" href="New_Book_Entry_N.aspx?Book_Unique_Identifier=<%#Eval("Book_Unique_Identifier") %>&BookId=<%#Eval("BookId") %>" target="_blank"><i class='bx bx-happy-heart-eyes'></i><span>Edit</span> </a>
                                                                                                </li>
                                                                                                <li>
                                                                                                    <a class="dropdown-item" href="BookDetails.aspx?BookId=<%#Eval("BookId") %>" target="_blank"><i class='bx bx-printer'></i><span>View Details</span></a>
                                                                                                </li>
                                                                                              <li>
                                                                                                  <asp:LinkButton ID="lnkDel" class="dropdown-item" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i>Delete</asp:LinkButton>
                                                                                              </li>

                                                                                            </ul>
                                                                                        </div>


                                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                        
                                                                                        <asp:Label ID="lbl_Book_Unique_Identifier" runat="server" Text='<%#Bind("Book_Unique_Identifier")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_class_id" Visible="false" runat="server" Text='<%#Bind("SelectClass")%>'></asp:Label>

                                                                                    </td>

                                                                                </tr>
                                                                           
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
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
        </div>
        <!--end row-->
    </div>
</asp:Content>
