<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="View_Home_Work_Class_Wise.aspx.cs" Inherits="school_web.LMS_VC_Admin.View_Home_Work_Class_Wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Uploaded Homework Class Wise
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .gridcss th {
            font-size: 14px!important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 5px;
            left: -23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-notebook icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Uploaded Homework Class Wise</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-md-4">
                                <div class="position-relative form-group">
                                    <label>Class</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Section</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Start Date</label>

                                    <asp:TextBox ID="txt_date" runat="server" CssClass="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>End Date</label>

                                    <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn btn-primary" Text="Find" Style="margin-top: 30px;" />
                            </div>

                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl. No.</th>
                                    <th>Teacher Name</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Subject</th>
                                    <th>Topic</th>
                                    <th>Details</th>
                                    <th>Upload Date</th>
                                    <th>Completion Date</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_teachername" runat="server" Font-Names="Arial" Text='<%#Bind("teachername") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CategoryName" runat="server" Font-Names="Arial" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section_Name" runat="server" Font-Names="Arial" Text='<%#Bind("Section_Name") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CourseName" runat="server" Font-Names="Arial" Text='<%#Bind("CourseName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Topic" runat="server" Font-Names="Arial" Text='<%#Bind("Topic") %>'></asp:Label>
                                            </td>
                                             <td>
                                                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Text='<%#Bind("Description") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Upload_Date" runat="server" Font-Names="Arial" Text='<%#Bind("Upload_Date") %>'></asp:Label>
                                                   <asp:Label ID="lbl_Upload_Time" runat="server" Font-Names="Arial" Text='<%#Bind("Upload_Time") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CompletingDate" runat="server" Font-Names="Arial" Text='<%#Bind("CompletingDate") %>'></asp:Label>
                                            </td>
                                            <td>

                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">

                                                            <asp:LinkButton ID="lnk_view" runat="server" CssClass="dropdown-item" OnClick="lnk_view_Click"><i class="dropdown-icon lnr-inbox"></i><span>View Details</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click"><i class="dropdown-icon lnr lnr-pencil"></i><span>Edit</span></asp:LinkButton>

                                                            <asp:Label ID="lbl_Home_Work_id" runat="server" Font-Names="Arial" Text='<%#Bind("Home_Work_id") %>' Visible="false"></asp:Label>

                                                            <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Description" runat="server" Font-Names="Arial" Text='<%#Bind("Description") %>' Visible="false"></asp:Label>
                                                            <a id="A1" href='Homework_replay.aspx?homework_id=<%#Eval("Home_Work_id")%>' style="color: #000" target="_blank" class="dropdown-item"><i class="dropdown-icon lnr-inbox"></i><span>View Student Reply</span></a>
                                                        </div>
                                                    </div>
                                                </div>


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
    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 916px;
                margin: 1.75rem auto;
            }
        }
    </style>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">View uploaded list</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">

                    <table class="table table-bordered" style="width: 100%:">
                        <tr>
                            <td style="font-size: 16px; font-weight: bold;">Topic Name
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px;">
                                <asp:Literal ID="lbl_topicdetails" runat="server"></asp:Literal>
                            </td>

                        </tr>
                        <tr>
                            <td style="font-size: 16px; font-weight: bold;">Homework Details
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px!important;">
                                <div style="font-size: 14px!important; height: 100px; overflow-x: hidden; overflow-y: scroll; float: left; width: 100%;">
                                    <asp:Label ID="txt_info1" runat="server" Style="font-size: 14px!important"></asp:Label>
                                </div>

                            </td>

                        </tr>

                        <tr>
                            <td style="font-size: 16px; font-weight: bold;">Uploaded File List
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <div style="font-size: 14px!important; height: 200px; overflow-x: hidden; overflow-y: scroll; float: left; width: 100%;">
                                    <asp:GridView ID="GrdViewimg" runat="server" class="mb-0 table table-bordered" CssClass="table table-striped table-bordered gridcss" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Img" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_imp" runat="server" Text='<%#Bind("Images")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_Homework_Id" runat="server" Text='<%#Bind("Homework_Id")%>' Visible="false"></asp:Label>
                                                    <asp:Image ID="Image2" runat="server" ImageUrl='<%# Bind("Images") %>' Style="margin: 0px; height: 50px; width: 50px; border: 2px solid #f93; padding: 1px" />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Topic Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_topicname" runat="server" Text='<%#Bind("Topicname")%>'></asp:Label>

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uploading Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Upload_Date" runat="server" Text='<%#Bind("Upload_Date")%>'></asp:Label>

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Download">
                                                <ItemTemplate>
                                                    <a href='<%#Eval("Images") %>' download target="_blank" style="display: block; padding: 2px; font-family: ebrima; font-size: 31px; color: #0066CC; text-decoration: none;"><i class="fa fa-cloud-download" aria-hidden="true"></i></a>

                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText=" " Visible="false">
                                                <ItemTemplate>
                                                    <asp:Button ID="BtnDeleteimg" OnClientClick="return confirm('Are you sure want to delete ?')" runat="server" OnClick="BtnDeleteimg_Click" class="btn btn-sm btn-success" Text="Delete" />

                                                </ItemTemplate>

                                            </asp:TemplateField>



                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>





                </div>
            </div>
        </div>
    </div>
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <div class="input-group input-group-icon">
    </div>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
