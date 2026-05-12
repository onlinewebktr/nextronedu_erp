<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="create-internal-admit-card.aspx.cs" Inherits="school_web.Examination_Admin.create_internal_admit_card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Create Internal Exam Admit Card
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Internal Exam</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Create Admit Card</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Create Admit Card"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-sm-6">
                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="validationCustom01" class="find-dv-lbl">Exam Name</label>
                                        <asp:DropDownList ID="ddl_exam" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label Llabel">Class</label>
                                        <span class="chkbx-all">
                                            <asp:CheckBox ID="chk_all" runat="server" Text="Select All" OnCheckedChanged="chk_all_CheckedChanged" AutoPostBack="true" /></span>
                                        <br />
                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_class" class="chkstle" runat="server" Text='<%#Eval("Course_Name") %>' />
                                                <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("course_id")%>'></asp:Label>
                                                <asp:Label ID="lbl_course_name" runat="server" Visible="false" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label for="validationCustom01" class="find-dv-lbl">Exam Date</label>
                                                <asp:TextBox ID="txt_exam_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6">
                                                <label for="validationCustom01" class="find-dv-lbl">Is Arrival Time</label>
                                                <asp:DropDownList ID="ddl_is_arrival_time" runat="server" class="form-select find-dv-txtbx">
                                                    <asp:ListItem>No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-8">
                                        <div class="row">
                                            <div class="col-sm-3" id="arr_time_dv">
                                                <label for="validationCustom01" class="find-dv-lbl">Arrival Time</label>
                                                <asp:DropDownList ID="ddl_arr_h" runat="server" class="form-select find-dv-txtbx" Style="width: 30%;">
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddl_arr_m" runat="server" class="form-select find-dv-txtbx" Style="width: 30%; margin: 0px 0px 0px 5px;">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddl_arr_ampm" runat="server" class="form-select find-dv-txtbx" Style="width: 30%; margin: 0px 0px 0px 5px;">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4" id="starttimeDv">
                                                <label for="validationCustom01" class="find-dv-lbl">Exam Start Time</label>
                                                <asp:DropDownList ID="ddl_s_hour" runat="server" class="form-select find-dv-txtbx" Style="width: 30%;">
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddl_s_minut" runat="server" class="form-select find-dv-txtbx" Style="width: 30%; margin: 0px 0px 0px 5px;">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddl_s_ampm" runat="server" class="form-select find-dv-txtbx" Style="width: 30%; margin: 0px 0px 0px 5px;">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4" id="endtimeDv">
                                                <label for="validationCustom01" class="find-dv-lbl">Exam End Time</label>
                                                <asp:DropDownList ID="ddl_e_hour" runat="server" class="form-select find-dv-txtbx" Style="width: 30%;">
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddl_e_minut" runat="server" class="form-select find-dv-txtbx" Style="width: 30%; margin: 0px 0px 0px 5px;">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddl_e_ampm" runat="server" class="form-select find-dv-txtbx" Style="width: 30%; margin: 0px 0px 0px 5px;">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_Submit" Style="margin: 0px 7px 1px 0px !important;" OnClick="btn_Submit_Click" runat="server" Text="Submit" class="btn btn-primary find-dv-btn" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hd_id" runat="server" />
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Created Admit Card</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr">
                                                    <div id="content">
                                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Session</th>
                                                                    <th>Class Name</th>
                                                                    <th>Exam Name</th>
                                                                    <th>Exam Date</th>
                                                                    <th>Arrival Time</th>
                                                                    <th>Exam Start Time</th>
                                                                    <th>Exam End Time</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_views" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_class_name" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_exam_name" runat="server" Text='<%#Bind("Exam_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("Exam_date")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%#Bind("Exam_arrival_time")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("Exam_start_time")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("Exam_end_time")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <%--<asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>--%>
                                                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_exam_id" runat="server" Text='<%#Bind("Exam_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
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


    <script type="text/javascript">
        $(document).ready(function () {
            on_is_arr_time_selection();
            $("#<%=ddl_is_arrival_time.ClientID%>").on('change', function () {
                on_is_arr_time_selection();
            })
        });

        function on_is_arr_time_selection() {
            if ($('#<%= ddl_is_arrival_time.ClientID %> option:selected').val() == "Yes") {
                $("#arr_time_dv").show();

                $("#arr_time_dv").removeClass("col-sm-6");
                $("#starttimeDv").removeClass("col-sm-6");
                $("#endtimeDv").removeClass("col-sm-6");


                $("#arr_time_dv").addClass("col-sm-4");
                $("#starttimeDv").addClass("col-sm-4");
                $("#endtimeDv").addClass("col-sm-4");
            }
            else {
                $("#arr_time_dv").hide();

                $("#arr_time_dv").removeClass("col-sm-4");
                $("#starttimeDv").removeClass("col-sm-4");
                $("#endtimeDv").removeClass("col-sm-4");

                $("#arr_time_dv").addClass("col-sm-6");
                $("#starttimeDv").addClass("col-sm-6");
                $("#endtimeDv").addClass("col-sm-6");
            }
        }
    </script>
</asp:Content>
