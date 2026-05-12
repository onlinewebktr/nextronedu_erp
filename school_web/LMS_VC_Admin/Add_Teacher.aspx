<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Add_Teacher.aspx.cs" Inherits="school_web.LMS_VC_Admin.Add_Teacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Add Teacher
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function openModal() {

            $("#modalConfirm").modal('show');
        }
    </script>
    <style>
        .dt-button-collection {
            margin-top: -59.4px !important;
        }

        .dropdown-item {
            display: block;
            width: 100%;
            padding: 3px 0px 0px 7px!important;
            clear: both;
            font-weight: 400;
            color: #212529;
            text-align: inherit;
            white-space: nowrap;
            background-color: transparent;
            border: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server"> Teacher List</asp:Literal>

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
        <div class="main-card mb-3 card">
            <div class="card-body" style="display: none">
                <div class="col-md-12">
                    <div style="margin: 0px; float: left; height: auto; width: 100%">
                        <asp:DataList ID="datl_teacherlist" runat="server" RepeatColumns="14" Style="border-collapse: collapse; font-size: 9px; display: none" border="1">
                            <ItemTemplate>
                                <asp:Label ID="lbl_UserID" runat="server" Text='<%#Bind("UserID") %>' Visible="false"></asp:Label>
                                <asp:CheckBox ID="chk_subjectname" runat="server" Text='<%#Bind("Name")%>' CssClass="lbl_color" />
                            </ItemTemplate>
                        </asp:DataList>
                        <b>Search By Teacher : </b>
                        <asp:ListBox ID="lst_Teacher" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                        <asp:Button ID="btn_find_multipal" runat="server" CssClass="btn btn-primary" Text="Find" OnClick="btn_find_multipal_Click" />

                        </>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4" style="display: none">
                    <div class="main-card card">
                        <div class="card-body">
                            <h5 class="card-title"></h5>
                            <div class="form-row">
                                <div class="col-md-12">
                                    <h5 class="card-title"></h5>
                                    <div class="position-relative form-group">
                                        <label>Teacher Name<sup>*</sup></label>
                                        <div class="input-group input-group-icon">
                                            <asp:TextBox ID="txt_Name" runat="server" CssClass="form-control"> </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="position-relative form-group">
                                        <label>Email ID:</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_EmailID" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="position-relative form-group">
                                        <label>Phone No<sup>*</sup></label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_Phone" runat="server" CssClass="form-control" MaxLength="10" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="position-relative form-group">
                                        <label>User Id<sup>*</sup></label>
                                        <div class="input-group input-group-icon">
                                            <asp:TextBox ID="txt_UserName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="position-relative form-group">
                                        <label>Password<sup>*</sup></label>
                                        <div class="input-group input-group-icon">
                                            <asp:TextBox ID="txt_Pswd" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%-- <div class="position-relative form-group">
                        <label>Posted Office:</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fa fa-calendar"></i></span>
                            </div>
                            <asp:TextBox ID="txt_postedin" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>--%>
                                    <div class="row">
                                        <div class="card-footer col-md-12">
                                            <div class="col-md-8">
                                                <asp:Button ID="btn_cncel" runat="server" Text="Cancel" CssClass="btn btn-dark" OnClick="btn_cncel_Click" />

                                            </div>
                                            <div class="col-md-4">
                                                <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="main-card card">
                        <div class="card-body">
                            <%--<h5 class="card-title">All Added Teacher </h5>--%>
                            <asp:HiddenField ID="hdid" runat="server" />
                            <asp:HiddenField ID="hdpwd" runat="server" />
                            <asp:HiddenField ID="hd_UserID" runat="server" />

                            <table style="margin: 0px; padding: 0px; height: auto; width: 100%; display: none">
                                <tr>
                                    <td style="padding: 3px;">Search By  User id/Mobile No.</td>
                                    <td style="padding: 3px;">
                                        <asp:TextBox ID="txt_userid_mobile_no" runat="server" CssClass="form-control" Style="float: left;"></asp:TextBox>
                                    </td>
                                    <td style="padding: 3px;">
                                        <asp:Button ID="btn_find_dtudent_regid" runat="server" CssClass="btn btn-primary" Text="Find" OnClick="btn_find_dtudent_regid_Click" />
                                        <asp:Button ID="btn_reset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btn_reset_Click" />
                                    </td>
                                </tr>
                            </table>
                            <hr />
                            <asp:ImageButton ID="img_expor_excel" OnClick="img_expor_excel_Click" runat="server" ImageUrl="~/images/excel_con.png" Style="height: 20px; width: 20px; display: none" />
                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th>Sl No.</th>
                                        <th>Name</th>
                                        <th>User ID</th>
                                        <th>Password</th>
                                        <th>Phone No</th>
                                        <th>Template Name</th>
                                        <th>Status</th>





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
                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("name") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_UserID" runat="server" Text='<%#Bind("user_id") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdUserID" runat="server" Value='<%#Bind("user_id") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Password" runat="server" Text='<%#Bind("Pswd") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_PhoneNo" runat="server" Text='<%#Bind("mobile") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Template_Name" runat="server" Text='<%#Bind("Template_Name") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lblStatusD" runat="server" Text='<%#Bind("StatusD") %>'></asp:Label>
                                                    <asp:Label ID="lbl_tempateid" runat="server" Text='<%#Bind("templateid") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_roomid" runat="server" Text='<%#Bind("roomid") %>' Visible="false"></asp:Label>
                                                </td>
                                                
                                                <td>
                                                    <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                        <div class="btn-group dropdown">
                                                            <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                                <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                            </button>
                                                            <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                                <asp:LinkButton ID="lnk_edit" runat="server" Visible="false" CssClass="dropdown-item" OnClick="lnk_edit_Click"><span>Edit</span></asp:LinkButton>

                                                                <asp:LinkButton ID="lnk_Delete" Visible="false" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><span>Delete</span></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkActive" CssClass="dropdown-item" runat="server" OnClick="lnkActive_Click" />
                                                                <asp:HiddenField ID="hdfActive" runat="server" Value='<%# Bind("Istatus") %>' />
                                                                <asp:HiddenField ID="hdId" runat="server" Value='<%#Bind("Id") %>' />
                                                                <asp:LinkButton ID="lnkedit_zoom" Text="Update Live  Credination" CssClass="dropdown-item" Visible="true" runat="server" OnClick="lnkedit_zoom_Click" />
                                                                <asp:LinkButton ID="lnk_update_zoomuserid" Text="Update Meeting User Id" CssClass="dropdown-item" Visible="false" runat="server" OnClick="lnk_update_zoomuserid_Click" />


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
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content" style="position: relative; float: left;">
                    <div class="modal-header">
                        <h3 class="modal-title" style="font-size: 20px!important;">Update 100MS Online Meeting </h3>
                        <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                    </div>
                    <div class="modal-body" style="position: relative; padding: 0px 8px 8px 8px; float: left;">
                        <div role="form">
                            <div class="position-relative form-group">
                                <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                    <table border="1" style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; border-collapse: collapse;">
                                        <tr>
                                            <td style="padding: 3px">Teacher Name</td>
                                            <td style="padding: 3px">
                                                <asp:Label ID="lbl_teachername" runat="server" Text=""></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td style="padding: 3px">User ID</td>
                                            <td style="padding: 3px">
                                                <asp:Label ID="lbluserid" runat="server" Text=""></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td style="padding: 3px">Old Teamplate Name</td>
                                            <td style="padding: 3px">
                                                <asp:Label ID="lbl_old_temaplatename" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 3px">New Template Name</td>
                                            <td style="padding: 3px">

                                                <asp:DropDownList ID="ddl_new_Template" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 3px; text-align: center" colspan="2">
                                                <asp:Button ID="btn_update_zoomid" runat="server" CssClass="btn btn-success btn-sm" Text="Update" OnClick="btn_update_zoomid_Click" />
                                                <asp:HiddenField ID="hd_teacheruserid" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="fadeup"></div>


        <div class="modal fade" id="myModal1" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content" style="position: relative; float: left;">
                    <div class="modal-header">
                        <h3 class="modal-title" style="font-size: 20px!important;">Update Zoom User Id & Password</h3>
                        <asp:Button ID="Button1" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                    </div>
                    <div class="modal-body" style="position: relative; padding: 0px 8px 8px 8px; float: left;">
                        <div role="form">
                            <div class="position-relative form-group">
                                <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                    <table border="1" style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; border-collapse: collapse;">
                                        <tr>
                                            <td style="padding: 3px">Teacher Name</td>
                                            <td style="padding: 3px">
                                                <asp:Label ID="lbl_teachername1" runat="server" Text=""></asp:Label></td>

                                        </tr>

                                        <tr>
                                            <td style="padding: 3px">Enter Email id<sup>**</sup></td>
                                            <td style="padding: 3px">
                                                <asp:TextBox ID="txt_emailid1" runat="server" type="email"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="ab" runat="server" ErrorMessage="**" ControlToValidate="txt_emailid1" ForeColor="Red">**</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 3px">Password<sup>**</sup></td>
                                            <td style="padding: 3px">
                                                <asp:TextBox ID="txt_password" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="ab" runat="server" ErrorMessage="**" ControlToValidate="txt_password" ForeColor="Red">**</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="padding: 3px; text-align: center" colspan="2">
                                                <asp:Button ID="btn_update_emailidnad_password" runat="server" CssClass="btn btn-success btn-sm" ValidationGroup="ab" Text="Update" OnClick="btn_update_emailidnad_password_Click" />
                                                <asp:HiddenField ID="hd_teacherid" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 5px 0px 5px 0px">
                                                <asp:Label ID="lblmsg" runat="server" ForeColor="Maroon" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="fadeup1"></div>
        <script type="text/javascript">
            function openModal1() {
                $("#myModal1").show();
                $('#myModal1').addClass('show');
                $('#fadeup1').addClass('modal-backdrop fade show');
            }
            function close() {
                $("#myModal1").hide();
                $('#myModal1').removeClass('show');
                $('#fadeup1').removeClass('modal-backdrop fade show');
            }
        </script>



        <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
            rel="stylesheet" type="text/css" />
        <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
            type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=lst_Teacher]').multiselect({
                    includeSelectAllOption: true
                });
            });
        </script>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
