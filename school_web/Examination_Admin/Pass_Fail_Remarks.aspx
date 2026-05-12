<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Pass_Fail_Remarks.aspx.cs" Inherits="school_web.Examination_Admin.Pass_Fail_Remarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Pass/Fail Remarks
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type=checkbox], input[type=radio] {
            border-style: none;
            width: 25px;
            height: 25px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 8px;
            z-index: 9999;
            background-color: green;
        }
    </style>

    <script type="text/javascript">


        function CheckLimit() {
            var textField =
                document.getElementById("<%=txt_pass.ClientID %>");
            var labelCount =
                document.getElementById("<%=lblCountLimit.ClientID %>");

            if (textField.value.length > 50) {
                textField.value = textField.value.substring(0, 50);
            } else {
                labelCount.innerHTML = 50 - textField.value.length;
            }
        }
        function CheckLimit1() {
            var textField =
                document.getElementById("<%=txt_failed.ClientID %>");
            var labelCount =
                document.getElementById("<%=lblCountLimit1.ClientID %>");

            if (textField.value.length > 50) {
                textField.value = textField.value.substring(0, 50);
            } else {
                labelCount.innerHTML = 50 - textField.value.length;
            }
        }
        function CheckLimit2() {
            var textField =
                document.getElementById("<%=txt_grace_remarks.ClientID %>");
            var labelCount =
                document.getElementById("<%=lblCountLimit2.ClientID %>");

            if (textField.value.length > 50) {
                textField.value = textField.value.substring(0, 50);
            } else {
                labelCount.innerHTML = 50 - textField.value.length;
            }
        }

        function CheckLimit3() {
            var textField =
                document.getElementById("<%=txt_grace_marks_indicator.ClientID %>");
            var labelCount =
                document.getElementById("<%=lblCountLimit3.ClientID %>");

            if (textField.value.length > 5) {
                textField.value = textField.value.substring(0, 5);
            } else {
                labelCount.innerHTML = 5 - textField.value.length;
            }
        }

        function CheckLimit4() {
            var textField =
                document.getElementById("<%=txt_remarks.ClientID %>");
            var labelCount =
                document.getElementById("<%=lblCountLimit4.ClientID %>");

            if (textField.value.length > 50) {
                textField.value = textField.value.substring(0, 50);
            } else {
                labelCount.innerHTML = 50 - textField.value.length;
            }
        }
    </script>

    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Pass/Fail Remarks</li>
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
                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-md-3">
                                            <label for="validationCustom01" class="form-label" style="font-weight: bold">Select Class<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_class" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>




                                    <div class="row" style="margin-top: 20px;">

                                        <div class="col-md-7">

                                            <label for="validationCustom01" class="form-label" style="font-weight: bold">Pass(Remarks)<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_pass"></asp:RequiredFieldValidator></sup>   <a href="#" data-toggle="tooltip" title="Remarks can be set for students scoring between a defined percentage value,You can also define general remarks which would be used if student does not fall under the conditions mentioned above"><i class="bx bxs-info-circle" style="font-size: 26px;"></i></a></label>
                                            <asp:TextBox ID="txt_pass" runat="server" class="form-control" MaxLength="50" onKeyUp="CheckLimit();"></asp:TextBox>



                                        </div>
                                        <div class="col-md-4">

                                            <label for="validationCustom01" class="form-label" style="font-weight: bold; margin-top: 45px;">You have</label>
                                            <asp:Label ID="lblCountLimit" runat="server" ForeColor="Red" Text="50" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="/50" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-md-7">

                                            <label for="validationCustom01" class="form-label" style="font-weight: bold">Failed(Remarks)<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_failed"></asp:RequiredFieldValidator></sup>   <a href="#" data-toggle="tooltip" title="Remarks can be set for students scoring between a defined percentage value,You can also define general remarks which would be used if student does not fall under the conditions mentioned above"><i class="bx bxs-info-circle" style="font-size: 26px;"></i></a></label>
                                            <asp:TextBox ID="txt_failed" runat="server" class="form-control" MaxLength="50" onKeyUp="CheckLimit1();"></asp:TextBox>


                                            <asp:CheckBox ID="chk_remarks_failed" runat="server" Text="Show Subject names in the remarks (Example format: Failed in Subject1, Subject2)" />


                                        </div>

                                        <div class="col-md-4">

                                            <label for="validationCustom01" class="form-label" style="font-weight: bold; margin-top: 45px;">You have</label>
                                            <asp:Label ID="lblCountLimit1" runat="server" ForeColor="Red" Text="50" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="/50" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-md-7">

                                            <label for="validationCustom01" class="form-label" style="font-weight: bold">Grace marks(Remarks)    <a href="#" data-toggle="tooltip" title="Remarks can be set for students securing between a defined percentage values. You can also define general which would be used if student doesn't fall under the conditions mentioned above"><i class="bx bxs-info-circle" style="font-size: 26px;"></i></a></label>
                                            <asp:TextBox ID="txt_grace_remarks" runat="server" class="form-control" MaxLength="50" onKeyUp="CheckLimit2();"></asp:TextBox>


                                            <asp:CheckBox ID="chk_graceremarks" runat="server" Text="Show Subject names in the remarks (Example format: Failed in Subject1, Subject2)" />


                                        </div>

                                        <div class="col-md-4">

                                            <label for="validationCustom01" class="form-label" style="font-weight: bold; margin-top: 45px;">You have</label>
                                            <asp:Label ID="lblCountLimit2" runat="server" ForeColor="Red" Text="50" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="/50" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-md-5">

                                            <label for="validationCustom01" class="form-label" style="font-weight: bold">Grace Marks Indicator   <a href="#" data-toggle="tooltip" title="Remarks can be set for students securing between a defined percentage values. You can also define general which would be used if student doesn't fall under the conditions mentioned above"><i class="bx bxs-info-circle" style="font-size: 26px;"></i></a></label>
                                            <asp:TextBox ID="txt_grace_marks_indicator" onKeyUp="CheckLimit3();" runat="server" class="form-control" MaxLength="5"></asp:TextBox>





                                        </div>
                                        <div class="col-md-4">

                                            <label for="validationCustom01" class="form-label" style="font-weight: bold; margin-top: 45px;">You have</label>
                                            <asp:Label ID="lblCountLimit3" runat="server" ForeColor="Red" Text="5" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="/5" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-12">
                                            <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation">

                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <label for="validationCustom01" class="form-label" style="font-weight: bold">Division<sup>*</sup>   <a href="#" data-toggle="tooltip" data-placement="top" title="Remarks can be set for students scoring between a defined percentage value,You can also define general remarks which would be used if student does not fall under the conditions mentioned above"><i class="bx bxs-info-circle" style="font-size: 26px;"></i></a></label>


                                        </div>

                                        <div class="row">

                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Lower Range(%)<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="aa" ControlToValidate="txt_lowerrange"></asp:RequiredFieldValidator></sup></label>
                                                <asp:TextBox ID="txt_lowerrange" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Upper Range(%)<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="aa" ControlToValidate="txt_upper_range"></asp:RequiredFieldValidator></sup></label>
                                                <asp:TextBox ID="txt_upper_range" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                            </div>

                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Remarks<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="aa" ControlToValidate="txt_remarks"></asp:RequiredFieldValidator></sup></label>
                                                <asp:TextBox ID="txt_remarks" onKeyUp="CheckLimit4();" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                            </div>

                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <label for="validationCustom01" class="form-label" style="font-weight: bold; margin-top: 25px;">You have</label>
                                                <asp:Label ID="lblCountLimit4" runat="server" ForeColor="Red" Text="50" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="/50" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                <asp:Button ID="btn_save_range" runat="server" Text="Add" CssClass="btn btn-success" ValidationGroup="aa" Style="margin-top: 24px;" OnClick="btn_save_range_Click" />

                                            </div>

                                        </div>



                                        <asp:GridView ID="grid_range" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" Style="margin-top: 20px;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                        <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"></i><span></span></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i><span></span></asp:LinkButton>
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
        <!--end row-->
    </div>

    <!--end page wrapper -->
    <script>

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

        });
    </script>
    <div class="tooltip bs-tooltip-top" role="tooltip">
        <div class="arrow"></div>
        <div class="tooltip-inner">
            Some tooltip text!
        </div>
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    




</asp:Content>
