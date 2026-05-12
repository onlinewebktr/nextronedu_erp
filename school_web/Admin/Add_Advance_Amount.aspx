<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Add_Advance_Amount.aspx.cs" Inherits="school_web.Admin.Add_Advance_Amount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Add Advance Amount
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <style>
        label {
            display: inline-block;
            font-size: 19px;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 30px;
            height: 30px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .modal {
            background: rgb(0 0 0 / 39%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translate(0, 0px);
            }

        .modal-header {
            padding: .5rem 1rem;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session_name.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Add_Advance_Amount.asp/GetRooPath',
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

        $(function () {
            var sessionid = $("#<%=ddl_session.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Add_Advance_Amount.asp/GetRooPathAdmNo',
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
    </script>
    <script>
        $(function () {
            $("#<%=txt_date_new.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_bank_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
                maxDate: 0   // आज से आगे की date disable
            }).attr("readonly", "true");
        });
        
        function openModalDeleteBill() {
            $('#myModal1').modal('show');
        }
        function openModalEditBill() {
            $('#myModalEdit').modal('show');
        }
        function studentInfo() {
            $('#myModalStudentInfo').modal('show');
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
                <div class="breadcrumb-title pe-3">Student Update</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Advance Amount</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row">
                                    <div class="col-xl-6">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-4">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Session</label>
                                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-xl-5">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Admission No.</label>
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-xl-3">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" Style="margin: 22px 0px 0px 0px;" OnClick="btn_find_admission_no_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-6">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Student Name</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-4">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Session</label>
                                                            <asp:DropDownList ID="ddl_session_name" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-xl-5">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Student Name</label>
                                                            <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-xl-3">
                                                            <asp:Button ID="btn_find_by_name" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" Style="margin: 22px 0px 0px 0px;" OnClick="btn_find_by_name_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="std_basic_infoS" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-xl-12">
                                            <div class="fnd-box-wpr">
                                                <h2 class="fnd-box-row-wpr-h">Student Basic Information</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_name" runat="server" class="stdnt-info-fnds" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_father_name" runat="server" class="stdnt-info-fnds " Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lblclass" runat="server" class="stdnt-info-fnds" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Section : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="txtsection" runat="server" class="stdnt-info-fnds" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Roll No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_old_roll_no" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Admission No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_admission_no" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Hostel : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lblhostel" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Transportation : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbltransporttion" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Contact no. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_phone" runat="server" Text="7250408680" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                         

                                                              
                                                                <div class="row" style="text-align:center">
                                                                    
                                                                            <asp:LinkButton ID="lnk_edit_bill" OnClick="lnk_edit_bill_Click" runat="server" class="button-61 nowordbreak collect-feesss" Style="width: 24%;"> Add Advance Amount</asp:LinkButton>
                                                                     
                                                                 
                                                                </div>
                                                          
                                                               

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="row">
                                        <div class="col-xl-12">
                                            <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnl_payment_history" runat="server" visible="false">
                                                <table class="table">
                                                    <tr>
                                                        <td colspan="8" style="padding: 0px 0px 0px 5px;"><b>Advance Payment History</b></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                                                            <asp:GridView ID="grd_fee" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable" OnRowDataBound="grd_fee_RowDataBound" ShowFooter="True">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Slip No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_slipno" runat="server" Text='<%#Bind("slipno") %>'></asp:Label>
                                                                           
                                                                            
                                                                             
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date_of_entry") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Payment Mode">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_paymenetmode" runat="server" Text='<%#Bind("Mode") %>'></asp:Label>
                                                                           <asp:Label ID="lbl_transaction_no" Visible="false" runat="server" Text='<%#Bind("Pay_mode_transaction_no")%>'> </asp:Label>
                                                                            <asp:Label ID="lbl_unique_entry_id" Visible="false" runat="server" Text='<%#Bind("unique_entry_id") %>'></asp:Label>
                                                                            
                                                                        
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="Remakes">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Remakes" runat="server" Text='<%#Bind("Remakes") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    

                                                                    <asp:TemplateField HeaderText="Type" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Add_type") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>Total</FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Wallet_input_amount","{0:n}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="td2" Width="100px" />
                                                                        <HeaderStyle CssClass="td2" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totalamount" runat="server" Font-Bold="true"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                     
                                                                            <asp:Label ID="lbl_bank_name" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Bank_name")%>'> </asp:Label>
                                                                            <asp:Label ID="lbl_bank_date" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Bank_date")%>'> </asp:Label>

                                                                            <asp:Label ID="lbl_id" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Id")%>'> </asp:Label>
                                                                         
                                                                            <asp:LinkButton ID="lnk_delete_bill" OnClick="lnk_delete_bill_Click" CausesValidation="false" runat="server" Style="background-color: #f7f100; min-width: 30px; color: #000;" class="button-61 nowordbreak collect-feesss"><span class="material-symbols-outlined">delete</span></asp:LinkButton>
                                                                        
 
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

        <div id="myModalStudentInfo" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 820px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 5px 10px;">
                    <h5 class="modal-title">Student Details</h5>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%;">
                        <table style="width: 100%;" id="Table1" class="table table-hover table-bordered ">
                            <tr>
                                <th>Student Name</th>
                                <th>Admission No</th>
                                <th>Class</th>
                                <th>Section</th>
                                <th>Roll</th>
                                <th>Father's Name</th>
                                <th>Action</th>
                            </tr>
                            <asp:Repeater ID="rp_std" runat="server">
                                <ItemTemplate>
                                    <tr id="row" runat="server">
                                        <td>
                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbladmissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername") %>'></asp:Label>
                                            <asp:Label ID="Label1" Visible="false" runat="server" Text='<%#Bind("session") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnk_select" runat="server" OnClick="lnk_select_Click">Select</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>


    
    <div id="myModal1" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Remarks</h5>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
                <div class="modal-body">

                    <div class="p-4 border rounded">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px;">Enter Remark</label>
                                <asp:TextBox ID="txt_remark" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox> 
                                <asp:Button ID="btn_conf_delete" Style="margin: 10px 10px 0px 0px;" OnClick="btn_conf_delete_Click" runat="server" Text="Submit" class="btn btn-danger find-dv-btn" />
                                <a href="#!" data-dismiss="modal" style="margin: 10px 10px 0px 0px;" class="btn btn-primary find-dv-btn">Close</a>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>


     <div id="myModalEdit" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add Advance Amount</h5>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 0px 0px 3px 0px;">Date<sup>*</sup></label>
                                <div class="clndr-div">
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    <asp:TextBox ID="txt_date_new"  runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                         <div class="row">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 0px 0px 3px 0px;">Advance Amount<sup>*</sup></label>
                                <div class="clndr-div">
                                   
                                    <asp:TextBox ID="txt_amount"  runat="server" class="form-control find-dv-txtbx" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                          <div class="row">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 0px 0px 3px 0px;">Remarks<sup>*</sup></label>
                                <div class="clndr-div">
                                 
                                    <asp:TextBox ID="txt_inputremarks"  runat="server" class="form-control find-dv-txtbx" TextMode="MultiLine"  ></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 8px 0px 3px 0px;">Payement Mode<sup>*</sup></label>
                                <asp:DropDownList ID="ddl_paymentmode" runat="server" class="form-select find-dv-txtbx">
                                    <asp:ListItem>Cash</asp:ListItem>
                                    <asp:ListItem>Deposited In Bank</asp:ListItem>
                                    <asp:ListItem>UPI</asp:ListItem>
                                    <asp:ListItem>UPI_Cash</asp:ListItem>
                                    <asp:ListItem>Pos</asp:ListItem>
                                    <asp:ListItem>Pos_Cash</asp:ListItem>
                                    <asp:ListItem>Netbanking</asp:ListItem>
                                    <asp:ListItem>Sbdebit</asp:ListItem>
                                    <asp:ListItem>Cheque</asp:ListItem>
                                    <asp:ListItem>NEFT</asp:ListItem>
                                    <asp:ListItem>Debitcard</asp:ListItem>
                                    <asp:ListItem>Creditcard</asp:ListItem>
                                    <asp:ListItem>Otherdcard</asp:ListItem>
                                    <asp:ListItem>Demand Draft(DD)</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row" id="bank_dt">
                            <div class="col-md-6">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 8px 0px 3px 0px;">Bank Name<sup>*</sup></label>
                                <asp:DropDownList ID="ddl_bank" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 8px 0px 3px 0px;">Bank Date<sup>*</sup></label>
                                <div class="clndr-div">
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    <asp:TextBox ID="txt_bank_date" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="pnl_mode_t_nS">
                            <div class="col-md-12">
                                <asp:Label ID="lbl_mode_trns_no" runat="server" class="form-label" Style="font-size: 14px; margin: 8px 0px 3px 0px; float: left; width: 100%;" Text="Transaction No."></asp:Label>
                                <asp:TextBox ID="txt_transaction_no" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>

                       

                        <div class="row">
                            <div class="col-md-12">
                                <asp:Button ID="btn_update_final" Style="margin: 10px 10px 0px 0px; float: left;"
                                    runat="server" Text="Add" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_update_final_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            on_payment_mode_selection();
            $("#<%=ddl_paymentmode.ClientID%>").on('change', function () {
                on_payment_mode_selection();
            })
        });

        function on_payment_mode_selection() {
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Cash") {
                $("#pnl_mode_t_nS").hide();
                $("#bank_dt").hide();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Paid to NBCS") {
                $("#pnl_mode_t_nS").hide();
                $("#bank_dt").hide();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Netbanking") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Deposited In Bank") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Sbdebit") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Cheque") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Cheque No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "NEFT") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("UTR No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Debitcard") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Creditcard") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Otherdcard") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "UPI") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Demand Draft(DD)") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Pos") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Other APP") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "UPI_Cash" || $('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Pos_Cash") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
        }
    </script>
</asp:Content>
