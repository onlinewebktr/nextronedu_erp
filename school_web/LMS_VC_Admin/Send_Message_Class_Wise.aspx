<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Send_Message_Class_Wise.aspx.cs" Inherits="school_web.LMS_VC_Admin.Send_Message_Class_Wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Send Message to Student
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function openModal() {

            $("#modalConfirm").modal('show');
        }
    </script>
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
        }

        .clndr-icon {
            font-size: 16px !important;
            color: #ff2956;
            position: absolute;
            top: 33px;
            right: 10px;
        }

        .notificationpan {
            display: none;
            top: 66px!important;
            right: 10px!important;
            padding: 10px 10px;
            width: 337px;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgb(154 154 154 / 80%);
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="fa fa-envelope icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server"> Send Message to Student</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Send Message</h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">

                                    <label>Send Type<sup>*</sup></label>
                                    <div class="input-group input-group-icon">
                                        <asp:RadioButton ID="rd_Class_wise" runat="server" Text="Class Wise" Checked="true" GroupName="ak" AutoPostBack="true" OnCheckedChanged="rd_Class_wise_CheckedChanged" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                          <asp:RadioButton ID="rd_individual" runat="server" Text="Individual" AutoPostBack="true" GroupName="ak" OnCheckedChanged="rd_individual_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="position-relative form-group" id="class1" runat="server" visible="true">

                                    <label>Class<sup>*</sup></label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_CourseCat" class="form-control" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="position-relative form-group" id="class2" runat="server" visible="true">

                                    <label>Section<sup>*</sup></label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_section" class="form-control" runat="server" Style="width: 98%"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="position-relative form-group" id="admissionno" runat="server" visible="false">

                                    <label>Admission No.<sup>*</sup></label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_admission_no" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>




                                <div class="position-relative form-group">

                                    <label>Subject<sup>*</sup></label>
                                    <div class="input-group input-group-icon">
                                        <asp:TextBox ID="txt_notice_subject" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Details<sup>*</sup></label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_notice_details" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150px"></asp:TextBox>


                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group">
                                    <label>Choose File (<sup> jpg,png,pdf fil size 500kb </sup>)</label>
                                    <div class="input-group input-group-icon">
                                        <asp:FileUpload ID="fl_Photo" runat="server" />
                                        <asp:HiddenField ID="Hd_Photo" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                            runat="server" ControlToValidate="fl_Photo"
                                            ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                            ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG|.pdf|.PDF)$"
                                            ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="card-footer pull-right">
                                    <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8" style="display: none">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Top 10  Events</h5>

                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Notice Type</th>
                                    <th>Admission No.</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Subject</th>
                                    <th>Details</th>
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
                                                <asp:Label ID="lbl_noticetype" runat="server" Text='<%#Bind("Send_Type") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("Admission_no") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_class" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Section_Id") %>'></asp:Label>
                                                <asp:Label ID="lbl_section" runat="server" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Heading" runat="server" Text='<%#Bind("Subject") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Details" runat="server" Text='<%#Bind("Details") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <a runat="server" id="a1" href='<%#Eval("Attachments") %>' download style="display: block; padding: 5px 0px 7px 30px; font-family: ebrima; font-size: 31px; color: #0066CC; text-decoration: none;"><i class="fa fa-download" aria-hidden="true"></i></a>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                            </td>

                                            <td>

                                                <asp:Label ID="lbl_Attachments" runat="server" Text='<%#Bind("Attachments") %>' Visible="false"></asp:Label>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="dropdown-item" OnClick="lnkEdit_Click"><i class="dropdown-icon lnr-inbox"></i><span>Edit</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Classid" runat="server" Text='<%#Bind("Class_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Section_Id" runat="server" Text='<%#Bind("Section_Id") %>' Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <a href="View_All_Message_Sent_to_Student.aspx">
                            <button type="button" class="btn-shadow btn btn-info" style="margin: 10px 0px 0px 0px; float: right;">
                                <span class="btn-icon-wrapper pr-2 opacity-7">
                                    <i class="pe-7s-plus fa-w-20"></i>
                                </span>
                                View All Send Message
                            </button>
                        </a>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
