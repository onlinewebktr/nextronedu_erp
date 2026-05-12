<%@ Page Title="" Language="C#" MasterPageFile="~/Parents_Profile/Parents.Master" AutoEventWireup="true" CodeBehind="Hostel_Out_Pass_Request.aspx.cs" Inherits="school_web.Parents_Profile.Hostel_Out_Pass_Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .clndr-dv-wpr {
            margin: 0px;
            padding: 0px;
            float: left;
            width: 100%;
            position: relative;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .gridcss th {
            font-size: 14px !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 12px !important;
            color: #e14eca;
            position: absolute;
            top: 8px;
            right: 6px;
            left: auto;
        }

        .modal.show .modal-dialog {
            transform: translateY(0%);
        }

        .modal {
            background: rgb(0 0 0 / 48%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translateY(0px);
            }

        .modal-header {
            display: flex;
            align-items: flex-start;
            justify-content: space-between;
            padding: 12px 9px 10px 5px !important;
            border-bottom: 1px solid #e9ecef;
            border-top-left-radius: .2857rem;
            border-top-right-radius: .2857rem;
        }

        .taskside {
            padding: 1px 0px 0px 0px !important;
        }

            .taskside h4 {
                font-size: 18px;
                margin: 0px 0px 0px 0px !important;
            }
            .notificationpan {
    
    width: 100%!important;
    background-color: rgb(231 231 231 / 0%);
    position: fixed;
    top: 133px !important;
      right: 1px;
    padding: 10px 10px;
   
    height: auto;
    border: 0px solid rgb(162, 162, 162);
    box-shadow:none;
}
            .alert {
    position: relative;
    padding: .9rem 1.25rem;
    margin-bottom: 1rem;
    border: .0625rem solid transparent;
    border-radius: .2857rem;
    width: 100% !important;
}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pagemainhh">
        <div class="container">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">

                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>

                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">

                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>

                    </div>
                </div>
            </div>


            <asp:HiddenField ID="HdID" runat="server" />



            <div class="headingtablee card" style="margin: 14px 0px 0px 0px;">
                <div class="row">
                    <div class="boxmargmm">


                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6 paddrght">
                            <label for="validationCustom01" class="lebelheadpp">Date From</label>
                            <div class="clndr-dv-wpr">
                                <asp:TextBox ID="txt_date" runat="server" CssClass="form-control"></asp:TextBox>
                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6 paddlft">
                            <label for="validationCustom01" class="lebelheadpp">Date To</label>
                            <div class="clndr-dv-wpr">
                                <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control calender-icon"></asp:TextBox>
                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <asp:Button ID="btn_find_by_date" OnClick="btn_find_by_date_Click" runat="server" Text="Find" class="btnfindaa btnfindaa2a" />
                        </div>




                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                        <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="grd-wpr">
                                        <div id="content">
                                            <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>SL.No. </th>

                                                        <th>Request ID </th>
                                                        <th>Admission No.</th>
                                                        <th>Student Name</th>
                                                        
                                                        <th>Request Date</th>
                                                        <th>Out Pass Date</th>
                                                        <th>Returen Date</th>
                                                        <th>No. of Day </th>
                                                        <th>Status</th>
                                                        <th>Last Remarks</th>
                                                        <th>Last Reply Date</th>

                                                        <th class="hiddenOnPrint">Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPDetails" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Request_id" runat="server" Text='<%#Bind("Request_id")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Adm_No" runat="server" Text='<%#Bind("Adm_No")%>'></asp:Label>
                                                                </td>


                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>

                                                                </td>
                                                                 

                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_request_date" runat="server" Text='<%#Bind("applydate")%>'></asp:Label>
                                                                </td>


                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_outpass_date" runat="server" Text='<%#Bind("Start_datetime")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Return_Pass_Date_time" runat="server" Text='<%#Bind("End_Datetime")%>'></asp:Label>
                                                                </td>


                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_no_of_date" runat="server" Text='<%#Bind("No_of_day")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Last_status")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_remarks" Style="word-break: break-all;" runat="server" Text='<%#Bind("Last_remarks")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Last_reply_date_time" Style="word-break: break-all;" runat="server" Text='<%#Bind("Last_replydatetime")%>'></asp:Label>
                                                                </td>



                                                                <td>
                                                                    <asp:LinkButton ID="lnk_view" runat="server" Style="background-color: #e14eca; color: #fff; padding: 2px 5px 2px 5px; width: auto; border-radius: 2px; font-weight: 500;" OnClick="lnk_view_Click"><i class="dropdown-icon lnr-inbox"></i><span>Process</span></asp:LinkButton>
                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                                      <asp:Label ID="lbl_course"  Visible="false" runat="server" Text='<%#Bind("classname")%>'></asp:Label>
                                                                     <asp:Label ID="Label1" Visible="false" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
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
        <!--end row-->
    </div>

    <script type="text/javascript">

        function openModal2() {

            $('#myModal').modal('show');
        }
    </script>
    <style>
        .mdl-frm-row {
            margin: 2px 0px 6px 0px !important;
        }

        .col-eq {
            flex: 1;
            background: #f4f4f4;
        }

        .taskside {
            padding: 10px;
        }

            .taskside h4 {
                margin-bottom: 0;
                font-size: 18px;
            }

        .taskseparator {
            border-top: 1px solid #e6e6e6;
            -webkit-box-shadow: 0 1px 6px 1px rgba(255,255,255,.5);
            box-shadow: 0 1px 6px 1px rgba(255,255,255,.5);
        }

        .taskside hr {
            margin-top: 9px;
            margin-bottom: 9px;
            border: 0;
            border-top: 1px solid #655f5f !important;
        }

        .task-info {
            float: left;
            width: 100%;
        }

            .task-info h5 {
                font-size: 13px;
            }

        .text-dark {
            font-weight: bold;
        }

        .fa.pull-left {
            margin-right: 0.3em;
        }

        .task-info h5 span {
        }
    </style>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="float: left; width: 100%;">Hostel Out Pass Process</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" Style="margin: -5px 0px 0px 0px; padding: 4px 10px 4px 7px; font-size: 12px; background: #f00; font-weight: 600; border-radius: 3px;" />
                </div>
                <div class="modal-body md-bdy" style="overflow: auto; padding: 0px 1rem; background: #f4f4f4;">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="mdl-frm-row">

                                    <div class="row" runat="server" visible="false" style="margin: 0px; padding: 0px;" id="row1">
                                        <div class="row" style="margin-top: 10px">
                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">Status<sup>*</sup></label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddl_status" runat="server" class="form-select find-dv-txtbx" Style="width: 100%; height: 24px;">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 10px">

                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">
                                                    Remarks<sup> *<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Required Field."
                                                        ControlToValidate="txt_remarks_floup" ValidationGroup="b">
                                                    </asp:RequiredFieldValidator></sup>
                                                </label>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txt_remarks_floup" runat="server" class="form-control" TextMode="MultiLine" Style="height: 150px !important; border: 1px solid #000; background: #fff;"></asp:TextBox>
                                            </div>
                                        </div>






                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: center;">
                                                <asp:Button ID="btn_save" OnClick="btn_save_Click" runat="server" Style="float: none; margin-bottom: 10px;"
                                                    Text="Save" ValidationGroup="b" class="btn btn-success" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GrdView_Follow_Up" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Next_Follow_Up_Date" runat="server" Text='<%#Bind("Date_timechat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Replay_By" runat="server" Text='<%#Bind("Reply_By")%>'></asp:Label>{
                                                            <asp:Label ID="lbl_Reply_By" runat="server" Text='<%#Bind("Reply_By_User")%>'></asp:Label>
                                                            }
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>



                            </div>
                            <div class="col-sm-4 col-eq">
                                <div class="taskside">
                                    <h4>Summary               
                                       
                                    </h4>
                                    <!-- /.box-tools -->

                                    <hr class="taskseparator" />

                                    <div style="text-align: center; padding: 0px; float: left; height: auto; width: 100%; margin: 0px 0px 8px 0px;">

                                        <asp:Image ID="Image1" runat="server" Style="height: 120px; width: 120px; padding: 2px; border: 1px solid #000;" />

                                    </div>


                                    <div class="task-info task-single-inline-wrap task-info-start-date">
                                        <h5><i class="fapull-left fa fa-calendar-o"></i>
                                            <span class="text-dark">Request Date:</span>
                                            <asp:Label ID="lbl_Enquirydate" runat="server" Style="margin-left: 17px;"></asp:Label>
                                        </h5>
                                    </div>
                                    <div class="task-info task-single-inline-wrap task-info-start-date">
                                        <h5><i class="fapull-left fa fa-calendar-o"></i>
                                            <span class="text-dark">Last Reply Date:</span>
                                            <asp:Label ID="lbl_lastfloupdate" runat="server" Style="margin-left: 4px;"></asp:Label>
                                        </h5>
                                    </div>


                                    <div class="task-info task-single-inline-wrap ptt10">
                                        <h5><span class="text-dark">Name:</span>
                                            <asp:Label ID="lbl_name" runat="server"></asp:Label>
                                        </h5>
                                        <h5><span class="text-dark">Admission No.:</span>
                                            <asp:Label ID="lbl_admi_no" runat="server"></asp:Label>
                                        </h5>
                                        <h5><span class="text-dark">Phone:</span><asp:Label ID="lbl_mobile_no" runat="server"></asp:Label></h5>


                                        <h5><span class="text-dark">Email:</span><asp:Label ID="lbl_email" runat="server"></asp:Label>
                                        </h5>

                                        <h5><span class="text-dark">Class:</span>
                                            <asp:Label ID="lbl_class" runat="server"></asp:Label></h5>
                                        <h5><span class="text-dark">Roll No.:</span>
                                            <asp:Label ID="lbl_roll_no" runat="server"></asp:Label></h5>
                                        <h5><span class="text-dark">Hostel Room No.:</span>
                                                <asp:Label ID="lbl_hostel_romm_no" runat="server"></asp:Label>
                                            </h5>
                                             <h5><span class="text-dark">Hostel Bed No.:</span>
                                                <asp:Label ID="lbl_hostel_bed_no" runat="server"></asp:Label>
                                            </h5>
                                    </div>
                                </div>
                            </div>

                        </div>



                    </div>




                </div>
            </div>
        </div>
    </div>

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <div class="input-group input-group-icon">
    </div>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });

    </script>
    <link href="../Studentprofile/assets/css/calender-modified.css" rel="stylesheet" />

</asp:Content>
