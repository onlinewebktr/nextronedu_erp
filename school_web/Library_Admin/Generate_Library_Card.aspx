<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="Generate_Library_Card.aspx.cs" Inherits="school_web.Library_Admin.Generate_Library_Card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Generate Library Card
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">


    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }
    </style>


    <script type="text/javascript">

        function Confirm() {

            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to submit ?")) {
                confirm_value.value = "Yes";

            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }


        function save_data() {
            var valsubmit = $('#<%=Btn_Generate.ClientID %>').val();
            if (valsubmit == "Generate") {

                $('#<%=Btn_Generate.ClientID %>').val('Submitting.. Please Wait..');

                Confirm();
                document.getElementById("<%=Btn_Generate.ClientID %>").click();

            }
            else {


                alert("Already submitted ")
            }

        }
    </script>

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
                <div class="breadcrumb-title pe-3">Library Card</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">

                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Generate Library Card for Student</li>


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
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn" Visible="false">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="find-dv">
                                                        <div class="row" id="finalsubmitpnl" runat="server" visible="false">
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Issue Date</label>
                                                                <div class="clndr-div">
                                                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                                    <asp:TextBox ID="txt_issuedate" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Valid Upto</label>
                                                                <div class="clndr-div">
                                                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                                    <asp:TextBox ID="txt_valid_up_to" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-1">

                                                                <a onclick="save_data()" class="btn btn-primary" style="width: 155px; margin: 22px 0px 0px 1px;">Generate</a>

                                                                <div style="height: 1px; overflow: hidden">
                                                                    <asp:Button ID="Btn_Generate" Style="margin: 5px 0px 5px 0px;" runat="server" Text="Generate" class="btn btn-primary find-dv-btn" OnClick="Btn_Generate_Click" />
                                                                </div>

                                                            </div>

                                                            <script>
                                                                $(function () {
                                                                    $("#<%=txt_issuedate.ClientID %>").datepicker({
                                                                        dateFormat: "dd/mm/yy",
                                                                        changeMonth: true,
                                                                        changeYear: true,
                                                                        readOnly: true,
                                                                        yearRange: "1900:2100",
                                                                    }).attr("readonly", "true");
                                                                });
                                                                $(function () {
                                                                    $("#<%=txt_valid_up_to.ClientID %>").datepicker({
                                                                        dateFormat: "dd/mm/yy",
                                                                        changeMonth: true,
                                                                        changeYear: true,
                                                                        readOnly: true,
                                                                        yearRange: "1900:2100",
                                                                    }).attr("readonly", "true");
                                                                });


                                                            </script>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="grd-wpr">





                                                <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Session">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Admission No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Section">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Roll No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Student Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Library Card No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_lib_card_no" runat="server" Text='<%#Bind("lib_card_no")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bar Code">
                                                            <ItemTemplate>
                                                               
                                                                <asp:Label ID="barcodeImgDV" runat="server">
                                                                    <a href="print_student.aspx?Lib_Bar_code=<%#Eval("Lib_Bar_code") %>" target="_blank">
                                                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%#Bind("Lib_Bar_code_img") %>' Style="height: 40px" />
                                                                    </a>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Library Card  Issue Date ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_lib_card_Issuedate" runat="server" Text='<%#Bind("lib_card_Issuedate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Library Card  Issue Date ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_lib_card_Valid_up_to_Date" runat="server" Text='<%#Bind("lib_card_Valid_up_to_Date")%>'></asp:Label>
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
        <!--end row-->
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
