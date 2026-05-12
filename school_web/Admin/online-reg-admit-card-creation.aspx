<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="online-reg-admit-card-creation.aspx.cs" Inherits="school_web.Admin.online_reg_admit_card_creation" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Admit Card Creation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="lt_meata" runat="server"></asp:Literal>  
    <style>
        table tr th input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 25px;
            height: 25px;
            border: 0.15em solid currentColor;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }

        table tr th label {
            width: 100%;
        }

        table tr td input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 25px;
            height: 25px;
            border: 1px solid #646464;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }

        .form-control + .form-control {
            margin-top: 1em;
        }

        tfoot, th, thead {
            color: #fff;
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
                <div class="breadcrumb-title pe-3">Online Registration</div>
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
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=""></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label Llabel">Session</label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label Llabel">Class</label>
                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label Llabel">Test Name</label>
                                        <asp:DropDownList ID="ddl_test_name" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label Llabel">Room</label>
                                        <asp:DropDownList ID="ddl_room_no" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label Llabel">Exam Type</label>
                                        <asp:DropDownList ID="ddl_exam_type" runat="server" class="form-select">
                                            <asp:ListItem>Exam</asp:ListItem>
                                            <asp:ListItem>Viva</asp:ListItem>
                                            <asp:ListItem>Written</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <div style="background: #f6f7bf; padding: 0px 5px 5px 5px; border: 1px solid #d8e787; border-radius: 3px; float: left; width: 100%;">
                                            <label for="validationCustom01" class="form-label Llabel">Shift</label>
                                            <asp:DropDownList ID="ddl_shift" runat="server" class="form-select" OnSelectedIndexChanged="ddl_shift_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div style="background: #d6ffe1; padding: 0px 5px 5px 5px; border: 1px solid #bbefc9; border-radius: 3px; float: left; width: 100%;">
                                            <label for="validationCustom01" class="form-label Llabel">Date of Exam</label>
                                            <div class="clndr-div">
                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                <asp:TextBox ID="txt_exam_date" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div style="background: #dcf3ce; padding: 0px 5px 5px 5px; border: 1px solid #a1e595; border-radius: 3px;">
                                            <label for="validationCustom01" class="form-label Llabel">Exam Start Time</label>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddl_hours" runat="server" class="form-select">
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
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddl_minutes" runat="server" class="form-select">
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
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddl_am_pm" runat="server" class="form-select">
                                                        <asp:ListItem>AM</asp:ListItem>
                                                        <asp:ListItem>PM</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div style="background: #f7d9e4; padding: 0px 5px 5px 5px; border: 1px solid #f7c1d5; border-radius: 3px;">
                                            <label for="validationCustom01" class="form-label Llabel">Exam End Time</label>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddl_hours_e" runat="server" class="form-select">
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
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddl_minutes_e" runat="server" class="form-select">
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
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddl_am_pm_e" runat="server" class="form-select">
                                                        <asp:ListItem>AM</asp:ListItem>
                                                        <asp:ListItem>PM</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Notice Details<sup>*</sup></label>
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
                                        <textarea id="txt_info" runat="server" name="area" class="form-control" style="min-height: 500px; width: 100%"></textarea>
                                    </div>




                                    <div class="col-12" runat="server" id="grdDVS" visible="false">
                                        <label for="validationCustom01" class="form-label">Select Student For Room<sup>*</sup></label>
                                        <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="hdrChkBox" runat="server" onClick="checkAllRows(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="rowChkBox" runat="server" onClick="checkUncheckHeaderCheckBox(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Apply Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Registration_id" runat="server" Text='<%#Bind("Registration_id")%>'></asp:Label>
                                                        <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_addedStatusS" runat="server" Text='<%#Bind("AddedStatusS")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_lbl_student_name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of birth">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_lbl_dob" runat="server" Text='<%#Bind("DOB")%>'></asp:Label>
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

                                                            objRow.style.backgroundColor = "#aef5c1";
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
                                                    objRow.style.backgroundColor = "#aef5c1";
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
                                    <div class="col-12">
                                        <asp:Button ID="btn_add" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_add_Click" />
                                    </div> 
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

     <style>
        .tox-tinymce {
            height: 400px !important;
            width: 100% !important;
        }
    </style>
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
