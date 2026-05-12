<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Scholarship_Create_Admit_Card.aspx.cs" Inherits="school_web.Admin.Scholarship_Create_Admit_Card" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Create Scholarship Admit Card
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-rowdv {
            margin: 3px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }
    </style>
    <script src="../Grid_calender/Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Grid_calender/Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../Grid_calender/Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <asp:Literal ID="lt_meata" runat="server"></asp:Literal> 
  <%--  <script src="https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>--%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
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
                <div class="breadcrumb-title pe-3">Scholarship</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active">Create Scholarship Admit Card</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">

                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" "></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="float: left;">
                                <div class="form-rowdv">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label" style="font-weight: 500; font-size: 1rem; background: #ffd700; padding: 5px 5px 5px 5px; width: 100%; text-align: center; color: #000;">
                                                Note:-Scholarship Status must be stopped before creation of admit card
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-rowdv">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="form-label">Exam Date<sup>*</sup></label>
                                        </div>

                                        <div class="col-md-2">
                                            <asp:TextBox ID="txt_exam_date" runat="server" class="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="form-label">Scholorship Name<sup>*</sup></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddl_Scholorship" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_Scholorship_SelectedIndexChanged"></asp:DropDownList>
                                        </div>


                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="form-label">Scholorship For<sup>*</sup></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddl_class" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-rowdv">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="form-label">Total Student<sup>*</sup></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txt_total_student" runat="server" class="form-control" Style="pointer-events: none"></asp:TextBox>
                                        </div>


                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="form-label">Exam Centre<sup>*</sup></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddl_center_name" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_center_name_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>




                                    </div>
                                </div>

                                <div class="form-rowdv">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label" style="font-weight: 500; font-size: 1rem; background: #ffd700; padding: 5px 5px 5px 5px; width: 100%; text-align: center; color: #000;">
                                               Room Number and Roll Number Setting
                                            </label>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-rowdv">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label for="validationCustom01" class="form-label">Room No.<sup>*</sup></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddl_room" runat="server" class="form-select">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-2">
                                                    <label for="validationCustom01" class="form-label">
                                                        Start Roll No. <sup>*<br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="Enter Start Roll No." ValidationGroup="ab" ControlToValidate="txt_start_from_roll_no"></asp:RequiredFieldValidator>
                                                        </sup>
                                                    </label>
                                                </div>
                                                <div class="col-md-2">

                                                    <asp:TextBox ID="txt_start_from_roll_no"   runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                </div>


                                                <div class="col-md-2">
                                                    <label for="validationCustom01" class="form-label">
                                                        End Roll No. <sup>*<br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="Enter End Roll No." ValidationGroup="ab" ControlToValidate="txt_endroll_no"></asp:RequiredFieldValidator>
                                                        </sup>
                                                    </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txt_endroll_no"  runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="row">
                                                <asp:Button ID="btn_Add_roll_no" style="width: 100px;" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_Add_roll_no_Click" ValidationGroup="ab" />
                                            </div>
                                        </div>


                                        <div class="col-md-12"  style="margin-top:10px;">
                                            <asp:GridView ID="GrdView_roll" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_roll_RowDataBound" ShowFooter="True" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Room No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Room_no" runat="server" Text='<%#Bind("Room_no")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Roll No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_start_roll_from" runat="server" Text='<%#Bind("Roll_Start_From")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Roll No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Roll_End_from" runat="server" Text='<%#Bind("Roll_End_from")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>Total Admit Card</b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Admit Card Will Issue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Admitcard_created" runat="server" Text='<%#Bind("Admitcard_created")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_total_admitcard" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Id" Visible="false" runat="server" Text='<%#Bind("Id")%>'></asp:Label>
                                                            <asp:LinkButton style="padding: 3px 2px;" ID="lnkEdit" runat="server" CssClass="btn" OnClick="lnkEdit_Click1" CausesValidation="false"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>

                                                            <asp:LinkButton style="padding: 3px 2px;" ID="lnk_delete" runat="server" CssClass="btn" OnClick="lnk_delete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this?');"><i class="lni lni-trash"> </i></asp:LinkButton>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hd_id_roll" runat="server" />

                                        </div>






                                    </div>
                                </div>

                                <div class="form-rowdv">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label" style="font-weight: 500; font-size: 1rem; background: #ffd700; padding: 5px 5px 5px 5px; width: 100%; text-align: center; color: #000;">
                                               Examination Time & Examination Guidelines
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-rowdv">
                                    <div class="row">

                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="form-label" style="margin-left: 10px;">Reporting Time <sup>*</sup></label>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddl_rp_hour" runat="server" class="form-select">
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
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddl_rp_minut" runat="server" class="form-select">
                                                        <asp:ListItem>00</asp:ListItem>
                                                        <asp:ListItem>05</asp:ListItem>
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>40</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddl_rp_am_pm" runat="server" class="form-select">
                                                        <asp:ListItem>AM</asp:ListItem>
                                                        <asp:ListItem>PM</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="form-label">Start Time <sup>*</sup></label>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="row">
                                               <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddl_hour" runat="server" class="form-select">
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
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddl_minut" runat="server" class="form-select">
                                                        <asp:ListItem>00</asp:ListItem>
                                                        <asp:ListItem>05</asp:ListItem>
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>40</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddl_am_pm" runat="server" class="form-select">
                                                        <asp:ListItem>AM</asp:ListItem>
                                                        <asp:ListItem>PM</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>



                                <div class="form-rowdv">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="form-label" style="padding-left: 10px;">End Time<sup>*</sup></label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="row">
                                                 <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddl_exam_end_hour" runat="server" class="form-select">
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
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddl_exam_end_minut" runat="server" class="form-select">
                                                        <asp:ListItem>00</asp:ListItem>
                                                        <asp:ListItem>05</asp:ListItem>
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>40</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddl_exam_end_ampm" runat="server" class="form-select">
                                                        <asp:ListItem>AM</asp:ListItem>
                                                        <asp:ListItem>PM</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-rowdv">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label" style="padding-left: 10px;">Examinations Guidelines <sup>*</sup></label>
                                        </div>
                                        <div class="col-md-12">
                                            <script>
                                                $(function () {
                                                    tinymce.init({
                                                        selector: $('#<%=txt_info.ClientID%>').selector,
                                                    width: 600,
                                                    height: 300,
                                                    plugins: [
                                                        'advlist', 'autolink', 'link', 'image', 'lists', 'charmap', 'preview', 'anchor', 'pagebreak',
                                                        'searchreplace', 'wordcount', 'visualblocks', 'code', 'fullscreen', 'insertdatetime', 'media',
                                                        'table', 'emoticons', 'help'
                                                    ],
                                                    toolbar: 'undo redo | styles | bold italic | alignleft aligncenter alignright alignjustify | ' +
                                                        'bullist numlist outdent indent | link image | print preview media fullscreen | ' +
                                                        'forecolor backcolor emoticons | help',
                                                    menu: {
                                                        favs: { title: 'My Favorites', items: 'code visualaid | searchreplace | emoticons' }
                                                    },
                                                    menubar: 'favs file edit view insert format tools table help',
                                                    content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }'
                                                });
                                            });
                                            </script>
                                            <textarea id="txt_info" runat="server" name="area" class="form-control" style="min-height: 400px; width: 100%"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-10">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:Panel ID="pnl_created_admit" runat="server" Visible="false">
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="Label1" runat="server" Style="font-weight: 500; font-size: 1rem;"
                        class="mb-0 text-uppercase" Text=" "></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="float: left; width: 100%;">
                                <div class="grd-wpr">
                                    <asp:Button ID="btn_final_submit" runat="server" Text="Publish Admit Card" CssClass="btn btn-primary" Style="float: right; margin: 0px 0px 4px 0px; background: #f00; border: 1px solid #a70000;" OnClick="btn_final_submit_Click" />
                                    <asp:GridView ID="grid_grade" runat="server" class="table table-bordered" AutoGenerateColumns="False" Style="width: 100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Registration No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Registration_id" runat="server" Text='<%#Bind("Registration_id")%>'></asp:Label>

                                                    <asp:Label ID="lbl_Test_id" Visible="false" runat="server" Text='<%#Bind("Test_id")%>'></asp:Label>

                                                    <asp:Label ID="lbl_session_id"  runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Student Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_lbl_student_name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Roll No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("Roll_no")%>'></asp:Label>
                                                      <asp:Label ID="lblExam_Shift" runat="server" Text='<%#Bind("Exam_Shift")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Room No. ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_roomno" runat="server" Text='<%#Bind("Room_no")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="Reporting Time ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Reporting_Time" runat="server" Text='<%#Bind("Reporting_time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Examination Date Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Created_datetime1" runat="server" Text='<%#Bind("Created_datetime1")%>'></asp:Label>
                                                    <asp:Label ID="lbl_Exam_Time" runat="server" Text='<%#Bind("Exam_Time")%>'></asp:Label>
                                                    -
                                                    <asp:Label ID="lbl_Exam_end_time" runat="server" Text='<%#Bind("Exam_end_time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reporting Time ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Reporting_Time" runat="server" Text='<%#Bind("Reporting_time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>


                                                    <%--    <a href="slip/Print_admit_card_Scholarship_Reg.aspx?session_Id=<%#Eval("Session_id") %>&classid=<%#Eval("Class_id") %>&admin=<%#Eval("Registration_id") %>&type=in_s" target="_blank"><i class='bx bx-printer'></i><span>Print</span></a>--%>

                                                    <asp:Label ID="lbl_Exam_Centre_Id" runat="server" Text='<%#Bind("Exam_Centre_Id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_Test_id" runat="server" Text='<%#Bind("Test_id")%>' Visible="false"></asp:Label>

                                                    <a href="slip/Print_admit_card_Scholarship_Reg.aspx?session_Id=<%#Eval("Session_id") %>&Scholorshipid=<%#Eval("Test_id") %>&classid=<%#Eval("Class_id") %>&Centreid=<%#Eval("Exam_Centre_Id") %>&admin=<%#Eval("Registration_id") %>&type=in_s" target="_blank"><i class='bx bx-printer'></i><span>Print</span></a>

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
        </asp:Panel>
    </div>
    

     <asp:DropDownList ID="ddl_exam_Shift" runat="server" class="form-select" Visible="false">

         <asp:ListItem>First Shift</asp:ListItem>
         <asp:ListItem>Second Shift</asp:ListItem>
     </asp:DropDownList>
    <script>
        $(function () {
            $("#<%=txt_exam_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
    </script>
</asp:Content>
