<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="send-sms.aspx.cs" Inherits="school_web.Admin.send_sms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    send SMS 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-label {
            margin-bottom: 3px;
            font-weight: 500;
            font-weight: bold;
            margin: 6px 0px 5px 0px;
        }
        tbody, td, tfoot, th, thead, tr {
    border-color: inherit;
    border-style: solid;
    border-width: 0;
    vertical-align: middle;
    font-size: 11px!important;
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
                <div class="breadcrumb-title pe-3">SMS</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Send SMS/WhatsApp </li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-6">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase">Send SMS/WhatsApp </asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 22px!important;">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label for="validationCustom01" class="form-label">Send To<sup></sup></label>
                                            <asp:DropDownList ID="ddl_type_by" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_type_by_SelectedIndexChanged">
                                                <asp:ListItem>Student</asp:ListItem>
                                                <asp:ListItem>User</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row" id="section1" runat="server" visible="false">
                                        <div class="col-md-4">
                                            <label for="validationCustom01" class="form-label">Session</label>
                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-5">
                                            <label for="validationCustom01" class="form-label">Class</label>
                                            <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-3">
                                            <label for="validationCustom01" class="form-label">Section</label>
                                            <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row" id="section2" runat="server" visible="true">
                                        <div style="display:flex; overflow-x:auto; gap:10px; padding:10px; border:1px solid #ccc;">
                                        <table id="example22" class="table table-bordered table-striped" style="margin: 13px 0px 0px 0px;">
                                            <thead>
                                                <tr>
                                                    <th>#Sl.No.</th>
                                                    <th>Name</th>
                                                    <th>Adm. No.</th>
                                                    <th>Class</th>
                                                    <th>Roll No.</th>
                                                    <th>
                                                        <asp:CheckBox runat="server" AutoPostBack="true" ID="chk_mobile" OnCheckedChanged="chk_mobile_CheckedChanged" Text="Father Mob. No" /></th>
                                                    <th style="display: none">
                                                        <asp:CheckBox runat="server" AutoPostBack="true" ID="chk_alt_mobile" OnCheckedChanged="chk_mobile_CheckedChanged" Text="Mother Mob. No" /></th>

                                                    <th>
                                                        <asp:CheckBox runat="server" AutoPostBack="true" ID="chk_mobilefatherwhatsaap" OnCheckedChanged="chk_mobilefatherwhatsaap_CheckedChanged" Text="Father Whataasp No." /></th>
                                                    <th style="display: none">
                                                        <asp:CheckBox runat="server" AutoPostBack="true" ID="chk_mobilemotherwhatsaap" OnCheckedChanged="chk_mobilefatherwhatsaap_CheckedChanged" Text="Mother Whataasp No" /></th>



                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_studentname" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">


                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:LinkButton CssClass="text-black" OnClick="lbl_mobile_no_Click" ID="lbl_mobile_no" runat="server" Text='<%#Bind("father_mob")%>'></asp:LinkButton>
                                                            </td>
                                                            <td style="text-align: left;display: none;>
                                                                <asp:LinkButton CssClass="text-black" OnClick="lbl_mobile_no_Click"
                                                                    ID="lbl_AltMobileNo" runat="server" Text='<%#Bind("mother_mob")%>'></asp:LinkButton>
                                                            </td>


                                                            <td style="text-align: left;">
                                                                <asp:LinkButton CssClass="text-black" OnClick="lnk_father_whatsApp_Click" ID="lnk_father_whatsApp" runat="server" Text='<%#Bind("Father_whatsApp_no")%>'></asp:LinkButton>
                                                            </td>
                                                            <td style="text-align: left; display: none">
                                                                <asp:LinkButton CssClass="text-black" OnClick="lnk_father_whatsApp_Click"
                                                                    ID="lnk_mother_whatsApp" runat="server" Text='<%#Bind("Mother_whatsApp_no")%>'></asp:LinkButton>
                                                            </td>




                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>







                                            </tbody>
                                        </table>
                                            </div>



                                    </div>

                                    <div class="row" id="section2_user" runat="server" visible="false">
                                        <table id="example23" class="table table-bordered table-striped" style="margin: 13px 0px 0px 0px;">
                                            <thead>
                                                <tr>
                                                    <th>#Sl.No.</th>
                                                    <th>Name</th>
                                                    <th>Employee Code.</th>
                                                    <th>
                                                        <asp:CheckBox runat="server" AutoPostBack="true" ID="CheckBox1_user" OnCheckedChanged="CheckBox1_user_CheckedChanged" Text="Mobile No" /></th>


                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">


                                                                <asp:Label ID="lbl_Employee_Name" runat="server" Text='<%#Bind("Employee_Name")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_Emp_Code" runat="server" Text='<%#Bind("Emp_Code")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:LinkButton CssClass="text-black" OnClick="lbl_mobile_no_user_Click" ID="lbl_mobile_no_user" runat="server" Text='<%#Bind("Mobile")%>'></asp:LinkButton>
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


                <div class="col-xl-6">
                    <h6 class="mb-0 text-uppercase">Send Whatsapp/SMS</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 22px!important;">
                                <div class="table-responsive">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-sm-12">

                                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                                    <tr>
                                                        <td style="padding: 5px; font-weight: bold;" colspan="2">Send To</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px;" colspan="2">
                                                            <asp:TextBox ID="txt_sent" runat="server" Style="height: 120px; width: 100%;" TextMode="MultiLine"></asp:TextBox>

                                                            <asp:Label ID="lbl_admission_nooutput" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px; font-weight: bold;" colspan="2">

                                                            <asp:RadioButton ID="rd_whatassp" AutoPostBack="true" runat="server" Text="WhatsApp" Checked="true" OnCheckedChanged="rd_whatassp_CheckedChanged" GroupName="ab" />
                                                            <asp:RadioButton ID="rd_sms" runat="server" AutoPostBack="true" Text="SMS" OnCheckedChanged="rd_sms_CheckedChanged" GroupName="ab" Visible="false" />
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td style="padding: 5px; font-weight: bold;">SMS Template</td>
                                                    
                                                    
                                                        <td style="padding: 5px;">
                                                            <asp:DropDownList ID="ddl_template" OnSelectedIndexChanged="ddl_template_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </td>


                                                    </tr>

                                                    <tr>
                                                        <td id="sms" runat="server" visible="false" colspan="2">
                                                            <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">



                                                                <tr>
                                                                    <td style="padding: 5px;">
                                                                        <div class="col-md-12">
                                                                            <asp:Label ID="lbl_msg" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="row">
                                                                            <asp:Panel runat="server" Visible="false" ID="pnl_0" CssClass="col-lg-6">
                                                                                <asp:Label runat="server" ID="lbl_0" class="form-label">Message</asp:Label>
                                                                                <asp:TextBox ID="txt_0" runat="server" class="form-control"></asp:TextBox>
                                                                            </asp:Panel>
                                                                            <asp:Panel runat="server" Visible="false" ID="pnl_1" CssClass="col-lg-6">
                                                                                <asp:Label runat="server" ID="lbl_1" class="form-label">Message</asp:Label>
                                                                                <asp:TextBox ID="txt_1" runat="server" class="form-control"></asp:TextBox>
                                                                            </asp:Panel>
                                                                        </div>

                                                                        <div class="row">
                                                                            <asp:Panel runat="server" Visible="false" ID="pnl_2" CssClass="col-lg-6">
                                                                                <asp:Label runat="server" ID="lbl_2" class="form-label">Message</asp:Label>
                                                                                <asp:TextBox ID="txt_2" runat="server" class="form-control"></asp:TextBox>
                                                                            </asp:Panel>

                                                                            <asp:Panel runat="server" Visible="false" ID="pnl_3" CssClass="col-lg-6">
                                                                                <asp:Label runat="server" ID="lbl_3" class="form-label">Message</asp:Label>
                                                                                <asp:TextBox ID="txt_3" runat="server" class="form-control"></asp:TextBox>
                                                                            </asp:Panel>
                                                                        </div>
                                                                        <div class="row">
                                                                            <asp:Panel runat="server" Visible="false" ID="pnl_4" CssClass="col-lg-6">
                                                                                <asp:Label runat="server" ID="lbl_4" class="form-label">Message</asp:Label>
                                                                                <asp:TextBox ID="txt_4" runat="server" class="form-control"></asp:TextBox>
                                                                            </asp:Panel>

                                                                            <asp:Panel runat="server" Visible="false" ID="pnl_5" CssClass="col-lg-6">
                                                                                <asp:Label runat="server" ID="lbl_5" class="form-label">Message</asp:Label>
                                                                                <asp:TextBox ID="txt_5" runat="server" class="form-control"></asp:TextBox>
                                                                            </asp:Panel>

                                                                        </div>

                                                                        <div class="row">
                                                                            <asp:Panel runat="server" Visible="false" ID="pnl_6" CssClass="col-lg-6">
                                                                                <asp:Label runat="server" ID="lbl_6" class="form-label">Message</asp:Label>
                                                                                <asp:TextBox ID="txt_6" runat="server" class="form-control"></asp:TextBox>
                                                                            </asp:Panel>

                                                                            <asp:Panel runat="server" Visible="false" ID="pnl_7" CssClass="col-lg-6">
                                                                                <asp:Label runat="server" ID="lbl_7" class="form-label">Message</asp:Label>
                                                                                <asp:TextBox ID="txt_7" runat="server" class="form-control"></asp:TextBox>

                                                                            </asp:Panel>

                                                                        </div>
                                                                        <div class="row">
                                                                            <asp:Panel runat="server" Visible="false" ID="pnl_8" CssClass="col-lg-6">
                                                                                <asp:Label runat="server" ID="lbl_8" class="form-label">Message</asp:Label>
                                                                                <asp:TextBox ID="txt_8" runat="server" class="form-control"></asp:TextBox>

                                                                            </asp:Panel>
                                                                        </div>




                                                                        <div class="row">
                                                                            <asp:Panel runat="server" Visible="false" ID="pnl_msg" class="col-lg-12">
                                                                                <label for="validationCustom01" class="form-label">Message</label>
                                                                                <asp:TextBox ID="txt_message" ReadOnly="true" TextMode="MultiLine" style="height:120px;" runat="server" class="form-control"></asp:TextBox>
                                                                            </asp:Panel>
                                                                        </div>

                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td style="padding: 5px;">
                                                                        <asp:Button ID="btn_msgPreview" runat="server" Text="SMS Preview" class="btn btn-danger find-dv-btn" OnClick="btn_msgPreview_Click" />
                                                                        <asp:Button ID="btn_Submit" runat="server" Text="Send" class="btn btn-success find-dv-btn" Style="margin-left: 20px;" OnClick="btn_Submit_Click" OnClientClick="return confirm('Are you sure want to send sms?');" />



                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="whatassp" runat="server" visible="true" colspan="2">
                                                            <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                                                <tr>
                                                                    <td style="padding: 5px; font-weight: bold;">Write Message</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 5px;">
                                                                        <asp:TextBox ID="txt_WhatsApp_message" runat="server" Style="height: 120px; width: 100%;" TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 5px;">
                                                                        <asp:Button ID="btn_send_WhatsApp" runat="server" Text="Send" class="btn btn-success find-dv-btn" Style="margin-left: 20px;" OnClick="btn_send_WhatsApp_Click" OnClientClick="return confirm('Are you sure want to send whatsapp messge?');" />
                                                                    </td>
                                                                </tr>



                                                            </table>



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
                </div>
            </div>
        </div>
        <!--end row-->
    </div>


</asp:Content>
