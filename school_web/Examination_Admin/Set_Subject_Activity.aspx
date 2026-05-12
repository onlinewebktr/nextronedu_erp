<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Set_Subject_Activity.aspx.cs" Inherits="school_web.Examination_Admin.Set_Subject_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Set  Subject Activity
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

        .form-label {
            margin-bottom: 4px !important;
            margin-top: 9px !important;
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
                            <li class="breadcrumb-item active" aria-current="page">Set Assessment</li>
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
                                                        <div class="step-name">Name</div>
                                                    </div>

                                                    <div class="stepper-item" id="a2" runat="server">
                                                        <div class="step-counter">2</div>
                                                        <div class="step-name">Define Logic</div>
                                                    </div>
                                                    <div class="stepper-item" id="a3" runat="server">
                                                        <div class="step-counter">3</div>
                                                        <div class="step-name">Calculation</div>
                                                    </div>
                                                </div>



                                            </div>


                                            <div class="col-sm-1">


                                                <a class="btn btn-success find-dv-btn" href="Subject_Activity.aspx" style="float: right;"><i class="bx bx-arrow-back"></i></a>
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

                                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">

                                        <div class="column-left">
                                            <ul>
                                                <!-- ngIf: examStructureParentMap[parentNode.id] -->
                                                <!-- ngIf: parentNode -->
                                                <li class="ng-scope" style="">
                                                    <div class="chart active ng-isolate-scope">
                                                        <div class="text-wrap_main">
                                                            <div class="text-wrap">
                                                                <span class="ng-binding" id="year" runat="server"></span>
                                                            </div>
                                                            <canvas height="137" width="137" style="height: 110px; width: 110px;"></canvas>
                                                        </div>

                                                    </div>
                                                </li>
                                                <!-- end ngIf: parentNode -->
                                                <li class="l2">
                                                    <div class="chart active ng-isolate-scope">
                                                        <div class="text-wrap_main">
                                                            <div class="text-wrap">
                                                                <span class="ng-binding" id="examname" runat="server"></span>
                                                            </div>
                                                            <canvas height="137" width="137" style="height: 110px; width: 110px;"></canvas>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>


                                    </div>

                                    <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
                                        <div id="pnl_Basic" runat="server" visible="false" class="diccontent">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold;display:none" >Mandatory to pass</label>
                                                    <label class="switch" style="margin: 0px 0px 0px 0px;display:none">
                                                        <asp:CheckBox ID="chk_mandatory" runat="server" Checked="true"  />
                                                        <span class="slider round"></span>
                                                    </label>
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Is Active  </label>
                                                    <label class="switch" style="margin: 0px 0px 0px 0px;">
                                                        <asp:CheckBox ID="chk_isactive" runat="server" Checked="false" />
                                                        <span class="slider round"></span>
                                                    </label>
                                                </div>

                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Class </label>
                                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Exam Term </label>
                                                    <asp:DropDownList ID="ddl_examterm" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_examterm_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Assessments </label>
                                                    <asp:DropDownList ID="ddl_assessment" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_assessment_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Subject</label>
                                                    <asp:DropDownList ID="ddl_subject" runat="server" class="form-select">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>




                                            <div class="row">
                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Name"></asp:RequiredFieldValidator></sup></label>
                                                    <asp:TextBox ID="txt_Name" runat="server" class="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Short Name<sup> </sup></label>
                                                    <asp:TextBox ID="txt_Short_Name" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" style="display:none">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Sequence No.</label>
                                                    <asp:TextBox ID="txt_Sequence_No" runat="server" class="form-control" onkeypress="return isNumberKey(event)" MaxLength="1"></asp:TextBox>
                                                </div>



                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4 col-md-2 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Marks Entry Date From<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_start_date"></asp:RequiredFieldValidator></sup></label>
                                                    <asp:TextBox ID="txt_start_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                </div>

                                                <div class="col-lg-4 col-md-2 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Marks Entry Date End<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_enddate"></asp:RequiredFieldValidator></sup></label>
                                                    <asp:TextBox ID="txt_enddate" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                </div>

                                            </div>


                                            <div class="row" style="display: none">
                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    <label for="validationCustom01" class="form-label" style="font-weight: bold">Description.<sup></sup></label>
                                                    <asp:TextBox ID="txt_Description" runat="server" class="form-control" TextMode="MultiLine" Style="height: 150px"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-12" style="margin-top: 20px;">

                                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" CausesValidation="false" OnClick="btn_cancel_Click" />
                                                <asp:Button ID="btn_next_1_2" runat="server" Text="Next" CssClass="btn btn-success" ValidationGroup="a" OnClick="btn_next_1_2_Click" />




                                            </div>
                                        </div>




                                        <div id="pnl_Define_Logic" runat="server" visible="false" class="diccontent">
                                            <div class="row">

                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label for="validationCustom01" class="form-label" style="font-weight: bold">Grade System<sup>*</sup></label>
                                                        <asp:DropDownList ID="ddl_gradesystem" runat="server" class="form-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="row">

                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label for="validationCustom01" class="form-label" style="font-weight: bold">Maximum Marks<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_maximummarks"></asp:RequiredFieldValidator></sup></label>
                                                        <asp:TextBox ID="txt_maximummarks" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label for="validationCustom01" class="form-label" style="font-weight: bold">Pass Marks<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_cutoff"></asp:RequiredFieldValidator></sup></label>
                                                        <asp:TextBox ID="txt_cutoff" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                    </div>
                                                </div>







                                                <div class="row" style="display: none;">
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label for="validationCustom01" class="form-label" style="font-weight: bold">Add Distinction  </label>
                                                        <label class="switch" style="margin: 0px 0px 0px 0px;">
                                                            <asp:CheckBox ID="chk_add_distinction" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chk_add_distinction_CheckedChanged" />
                                                            <span class="slider round"></span>
                                                        </label>
                                                    </div>
                                                </div>

                                                <div class="row" id="distinction" runat="server" visible="false">
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label for="validationCustom01" class="form-label" style="font-weight: bold">Distinction Marks<sup>  </sup></label>
                                                        <asp:TextBox ID="txt_distinctionmarks" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                    </div>
                                                </div>





                                                <div class="col-md-12" style="margin-top: 20px;">





                                                    <asp:Button ID="btn_back_2_1" runat="server" Text="Previous" class="btn btn-dark" CausesValidation="false" OnClick="btn_back_2_1_Click" />
                                                    <asp:Button ID="btn_Next_2_3" runat="server" Text="Next" CssClass="btn btn-success" OnClick="btn_Next_2_3_Click" />


                                                </div>

                                            </div>



                                        </div>







                                        <div id="pn_Calculation" runat="server" visible="false" class="diccontent">



                                            <div class="row">
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label for="validationCustom01" class="form-label" style="font-weight: bold">Calculation Logic<sup>*</sup></label>
                                                        <asp:DropDownList ID="ddl_calculation_logic" runat="server" class="form-select">
                                                            <asp:ListItem>Sum</asp:ListItem>
                                                            <asp:ListItem>Average</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 20px;">
                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

                                                        <asp:CheckBox ID="chk_per" runat="server" Text="Advanced Settings" AutoPostBack="true" OnCheckedChanged="chk_per_CheckedChanged" />



                                                        <div class="row" id="advancedSetting" runat="server" visible="false">




                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="margin-top: 13px;">
                                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Consider best</label>


                                                                <asp:DropDownList ID="ddl_consider_best" runat="server" class="form-select">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                <label for="validationCustom01" class="form-label" style="margin: 24px 0px 0px 0px;">
                                                                    of 'number of sub levels'   <a href="#" data-toggle="tooltip" title="Out of all the sub levels present, performance of the best 'n' sub levels would be considered"><i class="bx bxs-info-circle" style="font-size: 26px;"></i></a>
                                                                </label>
                                                            </div>

                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="margin-top: 13px;">
                                                                <label for="validationCustom01" class="form-label" style="font-weight: bold">Pass criteria </label>
                                                                <asp:DropDownList ID="ddl_pass_criteria" runat="server" class="form-select">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <script>

                                                                $(document).ready(function () {
                                                                    $('[data-toggle="tooltip"]').tooltip();
                                                                });
                                                            </script>

                                                        </div>







                                                    </div>
                                                </div>
                                                <div class="row" id="sublevel" runat="server" visible="false">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                                        <a href="#" data-toggle="tooltip" title="Please select one of the nodes(level/sub level) which must be considered for calculation"><i class="bx bxs-info-circle" style="font-size: 26px;"></i></a>
                                                        <asp:GridView ID="grid_subject_assesment" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" Style="margin-top: 20px;" OnRowDataBound="grid_subject_assesment_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select ">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk_per" runat="server" AutoPostBack="true" OnCheckedChanged="chk_per_CheckedChanged1" />

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name Of Sub Level">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Subject_name" runat="server" Text='<%#Bind("Subject_Activity_Name")%>'></asp:Label>
                                                                        <asp:Label ID="lbl_Subject_id" runat="server" Text='<%#Bind("Subject_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Select_Data" runat="server" Text='<%#Bind("Select_Data")%>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Mandatory to Pass">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Is_Mandatory_to_pass" runat="server" Text='<%#Bind("Is_Mandatory_to_pass")%>' Visible="false"></asp:Label>
                                                                        <asp:CheckBox ID="chk_per1" runat="server" Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>

                                                    </div>
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
        </div>
    </div>
</asp:Content>
