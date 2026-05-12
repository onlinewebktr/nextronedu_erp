<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="Print_Created_Bard_Code_For_Book.aspx.cs" Inherits="school_web.Library_Admin.Print_Created_Bard_Code_For_Book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Print Created Barcode
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="css/barcode-print.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("#<%=txt_AuthorName.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Print_Created_Bard_Code_For_Book.aspx/Getbookname',
                        data: "{ 'bookname': '" + request.term + "'}",
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
                            <li class="breadcrumb-item active" aria-current="page">Print Created Barcode</li>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Class </label>
                                                        <asp:DropDownList ID="ddl_class_wise" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_wise_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-2" style="display:none">
                                                        <label for="validationCustom01" class="find-dv-lbl">Book Status </label>
                                                        <asp:DropDownList ID="ddl_book_status" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_book_status_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Location Wise </label>
                                                        <asp:DropDownList ID="ddl_lcation" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_lcation_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">OR</div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Book Name</label>
                                                        <asp:TextBox ID="txt_AuthorName" runat="server" class="form-control"></asp:TextBox>
                                                    </div>


                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find_auther_wise" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_auther_wise_Click" />
                                                    </div>

                                                    <div class="col-sm-3">


                                                        <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Visible="false" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                   
                                                           <asp:LinkButton ID="btn_print_barcodes" runat="server" class="btn btn-primary find-dv-btn" OnClick="btn_print_barcodes_Click"  ToolTip="Print Barcode"  Style="margin: 21px 0px 0px 5px;"><i class="fa fa-barcode" aria-hidden="true"></i><i class='bx bx-printer'></i></asp:LinkButton>
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

                                                        <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="hdrChkBox" runat="server" onClick="checkAllRows(this);" Text="All" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="rowChkBox" runat="server" onClick="checkUncheckHeaderCheckBox(this);" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Name Of Book">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_NameOfBook" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Book Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_TypeName" runat="server" Text='<%#Bind("booktype")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Book Category">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Book_Category" runat="server" Text='<%#Bind("Book_Category")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Class">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_classname" runat="server"></asp:Label>
                                                                        <asp:Label ID="lbl_class_id" Visible="false" runat="server" Text='<%#Bind("SelectClass")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Author Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_AuthorName" runat="server" Text='<%#Bind("AuthorName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ISBN No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label14" runat="server" Text='<%#Bind("ISBN_Num")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="BarCode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_barcode" runat="server" Text='<%#Bind("Bar_code")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="BarCode">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lbl_BookId" Visible="false" runat="server" Text='<%#Bind("BookId")%>'></asp:Label>
                                                                        <asp:Label ID="barcodeImgDV" runat="server">
                                                                            <a href="print-barcode.aspx?bookid=<%#Eval("BookId") %>" target="_blank">
                                                                                <asp:Image ID="Image2" runat="server" ImageUrl='<%#Bind("Barcode_img") %>' Style="height: 40px" />
                                                                            </a>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                            </Columns>
                                                        </asp:GridView>

                                                        <script type="text/javascript">
                                                            function checkAllRows(obj) {

                                                                var objGridview = obj.parentNode.parentNode.parentNode;
                                                                var list = objGridview.getElementsByTagName("input");

                                                                for (var i = 0; i < list.length; i++) {
                                                                    var objRow = list[i].parentNode.parentNode;
                                                                    if (list[i].type == "checkbox" && obj != list[i]) {
                                                                        if (obj.checked) {

                                                                            //If the header checkbox is checked then check all 
                                                                            //checkboxes and highlight all rows.

                                                                            objRow.style.backgroundColor = "#0baf36";
                                                                            objRow.style.Color = "#fff";
                                                                            list[i].checked = true;
                                                                        }
                                                                        else {
                                                                            objRow.style.backgroundColor = "#FFFFFF";
                                                                            list[i].checked = false;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            function checkUncheckHeaderCheckBox(obj) {
                                                                var objRow = obj.parentNode.parentNode;

                                                                if (obj.checked) {
                                                                    objRow.style.backgroundColor = "#0baf36";
                                                                    objRow.style.Color = "#fff";
                                                                }
                                                                else {
                                                                    objRow.style.backgroundColor = "#FFFFFF";
                                                                }
                                                                var objGridView = objRow.parentNode;

                                                                //Get all input elements in Gridview
                                                                var list = objGridView.getElementsByTagName("input");
                                                                for (var i = 0; i < list.length; i++) {
                                                                    var objHeaderChkBox = list[0];

                                                                    //Based on all or none checkboxes are checked check/uncheck Header Checkbox
                                                                    var checked = true;

                                                                    if (list[i].type == "checkbox" && list[i] != objHeaderChkBox) {
                                                                        if (!list[i].checked) {
                                                                            checked = false;
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                objHeaderChkBox.checked = checked;
                                                            }
                                                        </script>





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
