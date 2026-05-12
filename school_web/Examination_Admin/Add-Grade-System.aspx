<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Add-Grade-System.aspx.cs" Inherits="school_web.Examination_Admin.Add_Grade_System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
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

            input[type=checkbox], input[type=radio]:checked {
                background-color: #17a00e;
            }

        input[type="checkbox"]#der1_chk_per:checked + span {
            border-color: red;
            background-color: red;
        }

        tbody, td, tfoot, thead, tr {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            padding: 2px 0px 3px 6px;
        }

        .tbe1 {
            background: var(--secondary-bg-color) none repeat scroll 0 0;
            border: 1px solid #e1e1e1;
            padding: 15px 20px;
            margin-bottom: 0;
            width: 100%;
            min-width: 150px;
        }
    </style>
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

    <script language="Javascript">
        function validate() {
            var txtbxValue = parseInt($("#<%=txt_with_decimal.ClientID %>").val());
            var FieldVal = txtbxValue;
            if (FieldVal < 6) {

            } else {
                $("#<%=txt_with_decimal.ClientID %>").val('');
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
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body" style="padding: 8px 7px 6px 1px;">


                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="find-dv" style="margin: 0px 0px -1px 0px;">
                                        <div class="row">
                                            <div class="col-sm-11">
                                                <div class="stepper-wrapper">
                                                    <div class="stepper-item" id="a1" runat="server">
                                                        <div class="step-counter">1</div>
                                                        <div class="step-name">Basic</div>
                                                    </div>

                                                    <div class="stepper-item" id="a2" runat="server">
                                                        <div class="step-counter">2</div>
                                                        <div class="step-name">Define Logic</div>
                                                    </div>
                                                    <div class="stepper-item" id="a3" runat="server">
                                                        <div class="step-counter">3</div>
                                                        <div class="step-name">Map Class</div>
                                                    </div>
                                                </div>



                                            </div>


                                            <div class="col-sm-1">


                                                <a class="btn btn-success find-dv-btn" href="grade-system.aspx" style="float: right;"><i class="bx bx-arrow-back"></i></a>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>


                        </div>
                    </div>
                </div>

                <div class="col-xl-12">


                    <div class="card">
                        <div class="card-body">




                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation">

                                    <div id="pnl_Basic" runat="server" visible="false" class="diccontent">


                                        <div class="row">

                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Name"></asp:RequiredFieldValidator></sup></label>
                                                <asp:TextBox ID="txt_Name" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Input Type<sup>* </sup></label>
                                                <asp:DropDownList ID="ddl_input" runat="server" class="form-select">
                                                    <asp:ListItem>Marks</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Output Type<sup>* </sup></label>
                                                <asp:DropDownList ID="ddl_output" runat="server" class="form-select">
                                                    <asp:ListItem>Marks</asp:ListItem>
                                                    <asp:ListItem>Grade</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>


                                        <div class="col-md-12" style="margin-top: 20px;">

                                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" CausesValidation="false" OnClick="btn_cancel_Click" />
                                            <asp:Button ID="btn_next_1_2" runat="server" Text="Next" CssClass="btn btn-success" ValidationGroup="a" OnClick="btn_next_1_2_Click" />




                                        </div>
                                    </div>




                                    <div id="pnl_Define_Logic" runat="server" visible="false" class="diccontent">
                                        <div class="row">

                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Round Off </label>

                                            </div>
                                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">

                                                <asp:RadioButton ID="rd_With_Decimal" Checked="true" runat="server" Text="With Decimal" GroupName="ab" AutoPostBack="true" OnCheckedChanged="rd_With_Decimal_CheckedChanged" />
                                                <asp:RadioButton ID="rd_Without_Decimal" runat="server" Text="Without Decimal" GroupName="ab" AutoPostBack="true" OnCheckedChanged="rd_Without_Decimal_CheckedChanged" />
                                            </div>

                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" id="withdecimal" runat="server" visible="false">

                                                <label for="validationCustom01" class="form-label" style="font-weight: bold; font-size: 13px;">Maximum number of digits allowed after decimal point is 5<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Name"></asp:RequiredFieldValidator></sup></label>
                                                <asp:TextBox ID="txt_with_decimal" runat="server" class="form-control" MaxLength="1" onkeypress="return isNumberKey_point(event)">0</asp:TextBox>


                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 20px;">
                                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">

                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                        <asp:RadioButton ID="rd_Round_up" runat="server" Text="Round-up" GroupName="ab1" AutoPostBack="true" OnCheckedChanged="rd_Round_up_CheckedChanged" />
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                        <asp:RadioButton ID="rd_Round_down" runat="server" Text="Round-down" GroupName="ab1" AutoPostBack="true" OnCheckedChanged="rd_Round_down_CheckedChanged" />
                                                    </div>

                                                </div>
                                                <div class="row" style="display: none">
                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                        <asp:RadioButton ID="rd_Half_Round_Up" runat="server" Text="Half Round Up" GroupName="ab1" AutoPostBack="true" OnCheckedChanged="rd_Half_Round_Up_CheckedChanged" />
                                                    </div>

                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                        <asp:RadioButton ID="rd_Half_Round_Down" runat="server" Text="Half Round Down" GroupName="ab1" AutoPostBack="true" OnCheckedChanged="rd_Half_Round_Down_CheckedChanged" />
                                                    </div>
                                                </div>





                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">

                                                <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="width_decimal_Round_up" runat="server">
                                                    <tr>
                                                        <td>With Decimal and Round-up</td>

                                                    </tr>
                                                    <tr>
                                                        <td>Example: 22.34 --> 22.4, 22.37 --> 22.4</td>
                                                    </tr>
                                                </table>

                                                <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="width_decimal_Round_down" runat="server" visible="false">
                                                    <tr>
                                                        <td>With Decimal and Round-down</td>

                                                    </tr>
                                                    <tr>
                                                        <td>Example: 22.34 --> 22.3, 22.37 --> 22.3</td>
                                                    </tr>
                                                </table>

                                                <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="width_decimal_Half_Round_Up" runat="server" visible="false">
                                                    <tr>
                                                        <td>With Decimal and Half round up</td>

                                                    </tr>
                                                    <tr>
                                                        <td>Example: 22.34 --> 22.4, 22.37 --> 22.4 , 22.35 --> 22.4</td>
                                                    </tr>
                                                </table>

                                                <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="width_decimal_Half_Round_down" runat="server" visible="false">
                                                    <tr>
                                                        <td>With Decimal and Half round down</td>

                                                    </tr>
                                                    <tr>
                                                        <td>Example: 22.34 --> 22.4, 22.37 --> 22.4 , 22.35 --> 22.3</td>
                                                    </tr>
                                                </table>




                                                <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="Without_decimal_Round_up" runat="server" visible="false">
                                                    <tr>
                                                        <td>Without Decimal and Round-up</td>

                                                    </tr>
                                                    <tr>
                                                        <td>Example: 22.2 --> 23, 22.6 --> 23</td>
                                                    </tr>
                                                </table>

                                                <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="Without_decimal_Round_down" runat="server" visible="false">
                                                    <tr>
                                                        <td>Without Decimal and Round-down</td>

                                                    </tr>
                                                    <tr>
                                                        <td>Example: 22.2 --> 22, 22.6 --> 22</td>
                                                    </tr>
                                                </table>


                                                <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="Without_decimal_half_round_up" runat="server" visible="false">
                                                    <tr>
                                                        <td>Without Decimal and Half round up</td>

                                                    </tr>
                                                    <tr>
                                                        <td>Example: 22.2 --> 22 , 22.6 --> 23, 22.5 --> 23</td>
                                                    </tr>
                                                </table>
                                                <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="Without_decimal_half_round_down" runat="server" visible="false">
                                                    <tr>
                                                        <td>Without Decimal and Half round down</td>

                                                    </tr>
                                                    <tr>
                                                        <td>Example: 22.2 --> 22 , 22.6 --> 23, 22.5 --> 22</td>
                                                    </tr>
                                                </table>

                                            </div>

                                        </div>


                                        <div class="row" style="margin-top: 20px;">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Round Off Percentage </label>

                                                <asp:CheckBox ID="chk_per" runat="server" Text="Same as above" Checked="true" AutoPostBack="true" OnCheckedChanged="chk_per_CheckedChanged" />



                                            </div>


                                            <div id="pnl_percentage" runat="server" visible="false" class="diccontent_per">
                                                <div class="row">


                                                    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">

                                                        <asp:RadioButton ID="rd_With_DecimalPer" runat="server" Text="With Decimal" GroupName="ab" AutoPostBack="true" OnCheckedChanged="rd_With_DecimalPer_CheckedChanged" />
                                                        <asp:RadioButton ID="rd_Without_DecimalPer" runat="server" Text="Without Decimal" GroupName="ab" AutoPostBack="true" OnCheckedChanged="rd_Without_DecimalPer_CheckedChanged" />
                                                    </div>

                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" id="withdecimal_per" runat="server" visible="false">

                                                        <label for="validationCustom01" class="form-label" style="font-weight: bold; font-size: 13px;">Maximum number of digits allowed after decimal point is 5<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Name"></asp:RequiredFieldValidator></sup></label>
                                                        <asp:TextBox ID="txt_with_decimalPer" runat="server" class="form-control" MaxLength="1" onkeypress="return isNumberKey(event)">0</asp:TextBox>


                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 20px;">
                                                    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">

                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <asp:RadioButton ID="rd_Round_upPer" runat="server" Text="Round-up" GroupName="ab1" AutoPostBack="true" OnCheckedChanged="rd_Round_upPer_CheckedChanged" />
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <asp:RadioButton ID="rd_Round_downPer" runat="server" Text="Round-down" GroupName="ab1" AutoPostBack="true" OnCheckedChanged="rd_Round_downPer_CheckedChanged" />
                                                            </div>

                                                        </div>
                                                        <div class="row" style="display: none">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <asp:RadioButton ID="rd_Half_Round_UpPer" runat="server" Text="Half Round Up" GroupName="ab1" AutoPostBack="true" OnCheckedChanged="rd_Half_Round_UpPer_CheckedChanged" />
                                                            </div>

                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <asp:RadioButton ID="rd_Half_Round_DownPer" runat="server" Text="Half Round Down" GroupName="ab1" AutoPostBack="true" OnCheckedChanged="rd_Half_Round_DownPer_CheckedChanged" />
                                                            </div>
                                                        </div>





                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">

                                                        <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="width_decimal_Round_upPer" runat="server">
                                                            <tr>
                                                                <td>With Decimal and Round-up</td>

                                                            </tr>
                                                            <tr>
                                                                <td>Example: 22.34 --> 22.4, 22.37 --> 22.4</td>
                                                            </tr>
                                                        </table>

                                                        <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="width_decimal_Round_downPer" runat="server" visible="false">
                                                            <tr>
                                                                <td>With Decimal and Round-down</td>

                                                            </tr>
                                                            <tr>
                                                                <td>Example: 22.34 --> 22.3, 22.37 --> 22.3</td>
                                                            </tr>
                                                        </table>

                                                        <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="width_decimal_Half_Round_UpPer" runat="server" visible="false">
                                                            <tr>
                                                                <td>With Decimal and Half round up</td>

                                                            </tr>
                                                            <tr>
                                                                <td>Example: 22.34 --> 22.4, 22.37 --> 22.4 , 22.35 --> 22.4</td>
                                                            </tr>
                                                        </table>

                                                        <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="width_decimal_Half_Round_downPer" runat="server" visible="false">
                                                            <tr>
                                                                <td>With Decimal and Half round down</td>

                                                            </tr>
                                                            <tr>
                                                                <td>Example: 22.34 --> 22.4, 22.37 --> 22.4 , 22.35 --> 22.3</td>
                                                            </tr>
                                                        </table>




                                                        <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="Without_decimal_Round_upPer" runat="server" visible="false">
                                                            <tr>
                                                                <td>Without Decimal and Round-up</td>

                                                            </tr>
                                                            <tr>
                                                                <td>Example: 22.2 --> 23, 22.6 --> 23</td>
                                                            </tr>
                                                        </table>

                                                        <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="Without_decimal_Round_downPer" runat="server" visible="false">
                                                            <tr>
                                                                <td>Without Decimal and Round-down</td>

                                                            </tr>
                                                            <tr>
                                                                <td>Example: 22.2 --> 22, 22.6 --> 22</td>
                                                            </tr>
                                                        </table>


                                                        <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="Without_decimal_half_round_upPer" runat="server" visible="false">
                                                            <tr>
                                                                <td>Without Decimal and Half round up</td>

                                                            </tr>
                                                            <tr>
                                                                <td>Example: 22.2 --> 22 , 22.6 --> 23, 22.5 --> 23</td>
                                                            </tr>
                                                        </table>
                                                        <table style="margin: 0px; padding: 0px; height: auto; width: 100%; border: 1px solid #c8bcbc;" id="Without_decimal_half_round_downPer" runat="server" visible="false">
                                                            <tr>
                                                                <td>Without Decimal and Half round down</td>

                                                            </tr>
                                                            <tr>
                                                                <td>Example: 22.2 --> 22 , 22.6 --> 23, 22.5 --> 22</td>
                                                            </tr>
                                                        </table>

                                                    </div>








                                                </div>








                                            </div>









                                        </div>

                                        <div class="row" style="margin-top: 20px;">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Grade Range </label>
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Lower Range<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="aa" ControlToValidate="txt_lowerrange"></asp:RequiredFieldValidator></sup></label>
                                                    <asp:TextBox ID="txt_lowerrange" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Upper Range<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="aa" ControlToValidate="txt_upper_range"></asp:RequiredFieldValidator></sup></label>
                                                    <asp:TextBox ID="txt_upper_range" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Grade<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="aa" ControlToValidate="txt_grade"></asp:RequiredFieldValidator></sup></label>
                                                    <asp:TextBox ID="txt_grade" runat="server" class="form-control"></asp:TextBox>
                                                </div> 
                                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Grade Color</label>
                                                    <div style="position: relative;">
                                                        <asp:TextBox ID="txtCCode" runat="server" class="form-control txtbxedt"></asp:TextBox>
                                                        <input type="color" id="favcolor" name="favcolor" value="#ffffff" onchange="getInputValue(this.value);" style="float: left; width: 25%; height: 27px; position: absolute; right: 2px; top: 2px;" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                    <asp:Button ID="btn_save_range" runat="server" Text="Add" CssClass="btn btn-success" ValidationGroup="aa" Style="margin-top: 24px;" OnClick="btn_save_range_Click" />

                                                </div>
                                            </div>
                                            <script type="text/javascript">
                                                function getInputValue(color) {
                                                    var c = document.getElementById("favcolor");
                                                    var box1 = document.getElementById('<%= txtCCode.ClientID %>');
                                                    box1.value = color;
                                                }
                                                function getValue() {
                                                    var color = document.getElementById("favcolor");
                                                    var box1 = document.getElementById('<%= txtCCode.ClientID %>');
                                                    color.value = box1.value;
                                                }
                                            </script>


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
                                                            <asp:Label ID="lbl_Grade" runat="server" Text='<%#Bind("Grade")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Color">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_background_color" runat="server" Text='<%#Bind("Background_color")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"></i><span>Edit</span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i><span>Delete</span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>





                                        </div>




                                        <div class="col-md-12" style="margin-top: 20px;">





                                            <asp:Button ID="btn_back_2_1" runat="server" Text="Previous" class="btn btn-dark" CausesValidation="false" OnClick="btn_back_2_1_Click" />
                                            <asp:Button ID="btn_Next_2_3" runat="server" Text="Next" CssClass="btn btn-success" OnClick="btn_Next_2_3_Click" />


                                        </div>

                                    </div>











                                    <div id="pn_MapClass" runat="server" visible="false" class="diccontent">
                                        <div class="col-md-12">
                                            <asp:DataList ID="datalist_classgrid" runat="server" RepeatDirection="Horizontal"
                                                RepeatColumns="5" OnItemDataBound="datalist_classgrid_ItemDataBound">

                                                <ItemTemplate>
                                                    <div class="tbe1">
                                                        <asp:CheckBox ID="chk_per" runat="server" Text='<%# Eval("Course_Name") %>' />

                                                        <asp:Label ID="lbl_classid" runat="server" Text='<%# Eval("course_id") %>' Visible="false">      
                                                        </asp:Label>
                                                    </div>


                                                </ItemTemplate>
                                            </asp:DataList>

                                        </div>




                                        <div class="col-md-12" style="margin-top: 20px; margin-bottom: 20px;">
                                            <asp:Button ID="btn_back_3_2" runat="server" Text="Previous" class="btn btn-dark" CausesValidation="false" OnClick="btn_back_3_2_Click" />
                                            <asp:Button ID="btn_final_submit" runat="server" Text="Submit" CssClass="btn btn-success" CausesValidation="false" OnClick="btn_final_submit_Click" />


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

    <asp:HiddenField ID="hd_rang_id" runat="server" />

</asp:Content>
