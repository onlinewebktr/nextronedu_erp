<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="grade-system.aspx.cs" Inherits="school_web.Examination_Admin.grade_system" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Grade System
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript"> 
        $(function () {
            var sessionid = $("#<%=ddlsession.ClientID%>").val();
            $("#<%=txt_searchby.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'grade-system.aspx/GetRooPathname',
                        data: "{ 'PathRooT': '" + request.term + "', Session_id:'" + sessionid + "'}",
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
    <style>
        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 0px 0px 0px 0px;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .mdl-close-btn {
            margin: 0px;
            padding: 0px 5px 0px 5px;
            border: 0px;
            background: #ed0000;
            font-size: 18px;
            color: #fff;
            line-height: 25px;
            border-radius: 2px;
        }

        .modal-header {
            padding: 7px 15px;
        }

        .modal.fade .modal-dialog {
            transform: translate(0, 0px);
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



            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Exam Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Grade System</li>
                        </ol>
                    </nav>
                </div>
                <a href="#" data-toggle="modal" data-target="#myModalCopY" style="float: right; position: absolute; right: 0px; font-size: 23px; top: 2px;"><i class="bx bx-cog"></i></a>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-3">
                                                    <label for="validationCustom01" class="find-dv-lbl">Grade Name</label>
                                                    <asp:TextBox ID="txt_searchby" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <a class="btn btn-success find-dv-btn" href="Add-Grade-System.aspx" style="margin: 14px 7px 1px 0px !important; float: right; padding: 3px 6px 6px 11px; font-size: 14px;"><i class="bx bx-plus-medical"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr">
                                            <asp:GridView ID="grid_grade" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grade Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_gradename" runat="server" Text='<%#Bind("Grade_Name") %>'></asp:Label>
                                                            <asp:Label ID="lbl_Grade_System_Id" runat="server" Text='<%#Bind("Grade_System_Id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"></i><span> </span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>


                                                            <asp:LinkButton ID="lnk_view" runat="server" ToolTip="View" CausesValidation="false" OnClick="lnk_view_Click"><i class="lni lni-eye"> </i></asp:LinkButton>
                                                        </ItemTemplate>


                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>

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



    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 1100px;
                margin: 1.75rem auto;
            }
        }
    </style>
    <!-------popupadd year----->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">View Grade System</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">

                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation">

                            <div class="row">
                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">

                                    <asp:GridView ID="grid_range" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" Style="margin-top: 20px;">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Lower Range">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Lower_Range" runat="server" Text='<%#Bind("Lower_Range")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upper Range">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Upper_Range" runat="server" Text='<%#Bind("Upper_Range")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Grade" runat="server" Text='<%#Bind("Grade")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">

                                    <table class="table table-bordered" role="grid" aria-describedby="example2_info" style="margin-top: 20px;">
                                        <thead>
                                            <tr>


                                                <th>Rounding Off</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txt_val1" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txt_va2" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>




                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                    <asp:GridView ID="grid_classmaped" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" Style="margin-top: 20px;">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Course_Name" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>
    <div id="fadeup"></div>

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script type="text/javascript">
        function openModal() {
            $("#myModal").show();
            $('#myModal').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#myModal").hide();
            $('#myModal').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }


        function openModalGradE() {
            $('#myModalCopY').modal('show');

        }
    </script>



    <div class="modal fade" id="myModalCopY" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 500px;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Copy Grade Setting For Next Session</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Copy From Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_current_session" runat="server" class="form-select" OnSelectedIndexChanged="ddl_current_session_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Copy to Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_copy_to_session" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>


                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_copy_setting" OnClick="btn_copy_setting_Click" runat="server" Text="Submit" class="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
