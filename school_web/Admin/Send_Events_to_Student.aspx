<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Send_Events_to_Student.aspx.cs" Inherits="school_web.Admin.Send_Events_to_Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Send Events to Student
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
        }

        .clndr-icon {
            font-size: 16px !important;
            color: #ff2956;
            position: absolute;
            top: 179px;
            right: 40px;
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
                <div class="breadcrumb-title pe-3"><a href="Event.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Event's</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Send Event's</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Send Event's"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Class<sup></sup></label>
                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Section<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-12">
                                        <asp:TextBox ID="txt_date" runat="server" CssClass="form-control calender-icon"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />

                                        <script src="../Autocomplete/jquery-ui.js"></script>
                                        <script>
                                            $(function () {
                                                $("#<%=txt_date.ClientID %>").datepicker({
                                                    dateFormat: "dd/mm/yy",
                                                    changeMonth: true,
                                                    changeYear: true,
                                                    yearRange: "1900:2100",
                                                    minDate: 0

                                                }).attr("readonly", "true");
                                            });
                                        </script>
                                    </div>

                                    <div class="col-md-12">
                                        <label>Subject<sup>*</sup></label>
                                        <asp:TextBox ID="txt_notice_subject" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-12">
                                        <label>Link</label>
                                        <asp:TextBox ID="txt_link" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Details<sup>*</sup></label>
                                        <asp:TextBox ID="txt_notice_details" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150px"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Choose File (<sup> jpg,png,pdf </sup>)</label> 
                                        <asp:FileUpload ID="fl_Photo" runat="server" />
                                        <asp:HiddenField ID="Hd_Photo" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                            runat="server" ControlToValidate="fl_Photo"
                                            ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                            ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG|.pdf|.PDF )$"
                                            ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                    </div>



                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">Top 10 Event's</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>Sl No.</th>
                                                        <th>Class</th>
                                                        <th>Section</th>
                                                        <th>Subject</th>
                                                        <th>Details</th>
                                                        <th>Link</th>
                                                        <th>File</th>
                                                        <th>Date</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lbl_class" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Heading" runat="server" Text='<%#Bind("Heading") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Details" runat="server" Text='<%#Bind("Details") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_links" Visible="false" runat="server" Text='<%#Bind("Link") %>'></asp:Label>
                                                                    <a runat="server" target="_blank" id="a2" href='<%#Eval("Link") %>' style="display: block; padding: 0px 0px 0px 0px; font-family: ebrima; font-size: 16px; color: #0066CC; text-decoration: none;"><i class="fa fa-external-link" aria-hidden="true"></i></a>
                                                                </td>
                                                                <td>
                                                                    <a runat="server" id="a1" href='<%#Eval("Attachments") %>' download style="display: block; padding: 0px 0px 0px 0px; font-family: ebrima; font-size: 16px; color: #0066CC; text-decoration: none;"><i class="fa fa-download" aria-hidden="true"></i></a>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Posted_Date") %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbl_Attachments" runat="server" Text='<%#Bind("Attachments") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click"><i class="lni lni-pencil-alt"></i><span>Edit</span></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnk_Delete" runat="server" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="lni lni-trash"></i><span>Delete</span></asp:LinkButton>
                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Classid" runat="server" Text='<%#Bind("Class") %>' Visible="false"></asp:Label>
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
</asp:Content>
