<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Assessments.aspx.cs" Inherits="school_web.Examination_Admin.Assessments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Assessments
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddlsession.ClientID%>").val();
            $("#<%=txt_searchby.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Assessments.aspx/GetRooPathname',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
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

        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
    <style>
        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
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
                            <li class="breadcrumb-item active" aria-current="page">Assessments</li>
                        </ol>
                    </nav>
                </div>

                <a href="#" data-toggle="modal" data-target="#myModal" style="float: right; position: absolute; right: 0px; font-size: 23px; top: 2px;"><i class="bx bx-cog"></i></a>
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
                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx" ></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Exam Term</label>
                                                    <asp:DropDownList ID="ddl_examterm" runat="server" class="form-select find-dv-txtbx">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_fina_class_wise" runat="server" Text="Find" OnClick="btn_fina_class_wise_Click1" class="btn btn-primary find-dv-btn" />
                                                </div>


                                                <div class="col-sm-3">
                                                    <label for="validationCustom01" class="find-dv-lbl">Search By Assessments</label>
                                                    <asp:TextBox ID="txt_searchby" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" />
                                                </div>
                                                <div class="col-sm-1"> 
                                                    <a class="btn btn-success find-dv-btn" href="Set_Assessment.aspx"  style="margin: 20px 7px 1px 0px !important; float: right; padding: 3px 6px 6px 11px; font-size: 14px;" title="Add Assessments"><i class="bx bx-plus-medical"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr">
                                            <asp:GridView ID="grid_grade" runat="server" OnRowDataBound="grid_grade_RowDataBound" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Course_Name") %>'></asp:Label>
                                                            <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Exam Term">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_examterm" runat="server" Text='<%#Bind("Term_Name") %>'></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Assessments Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Assessments" runat="server" Text='<%#Bind("Assessment_Name") %>'></asp:Label>
                                                            <asp:Label ID="lbl_Assessments_Id" runat="server" Text='<%#Bind("Assessment_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Branch_Id" runat="server" Text='<%#Bind("Branch_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Exam_Term_Id" runat="server" Text='<%#Bind("Exam_Term_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Session_Id" runat="server" Text='<%#Bind("Session_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Grade_System_Id" runat="server" Text='<%#Bind("Grade_System_Id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grade Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Grade_Name" runat="server" Text='<%#Bind("Grade_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Maximum Marks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Maximum_Marks" runat="server" Text='<%#Bind("Maximum_Marks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pass Marks ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Cut_Off_Percentage" runat="server" Text='<%#Bind("Cut_Off_Percentage") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Calculation Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_calulattiontype" runat="server" Text='<%#Bind("Calculation_Type") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl_Istatus" runat="server" Text='<%#Bind("Istatus") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Subject Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Scholastic_Co_scholastic" runat="server" Text='<%#Bind("Scholastic_Co_scholastic")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Import Subject">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btn_importsubject" runat="server" Text="Import Subject" OnClick="btn_importsubject_Click" OnClientClick="return confirm('Are you sure you want to import subject ?');" class="btn btn-success" Style="padding: 3px 10px; font-size: 10px;" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"></i><span> </span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
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


    <div class="modal fade" id="myModal" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 550px;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 18px;">Copy Assessment Setting For Next Term/Session</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Copy From Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_current_session" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_current_session_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Copy For Next</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_copy_to" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_copy_to_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Term</asp:ListItem>
                                    <asp:ListItem>Session</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div id="copy_to_SessionDV" runat="server" visible="false" style="width: 100%; float: left">
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Choose Session</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_copy_to_session" runat="server" class="form-select"></asp:DropDownList>
                                </div>
                            </div>
                        </div> 
                    </div>

                    <div id="copy_to_termDV" runat="server" visible="false" style="width: 100%; float: left">
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Copy From Term</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_copy_from_term" runat="server" class="form-select" OnSelectedIndexChanged="ddl_copy_from_term_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div> 
                        <div class="mdl-frm-row">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="validationCustom01" class="find-dv-lbl">Copy to Term</label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_copy_to_term_for_term" runat="server" class="form-select"></asp:DropDownList>
                                </div>
                            </div>
                        </div> 
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_copy_setting"  runat="server" Text="Submit" class="btn btn-primary" OnClick="btn_copy_setting_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
