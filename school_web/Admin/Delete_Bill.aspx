<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Delete_Bill.aspx.cs" Inherits="school_web.Admin.Delete_Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Update/Delete Slip
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
    </style>
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
                            <li class="breadcrumb-item active" aria-current="page">Update/Delete Slip</li>
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
                                    <div class="col-xl-7">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr"> 
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-4">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Session</label>
                                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="row"> 
                                        <div style="margin: 0px; padding: 0px; height: 43px; width: 100%; border-bottom: 1px solid #e3d4d4; text-align: center; z-index: 1;">
                                            <asp:RadioButton ID="rd_addmission_fee" runat="server" Text="Admission Fee" GroupName="ab" AutoPostBack="true" OnCheckedChanged="rd_addmission_fee_CheckedChanged" />
                                            <asp:RadioButton ID="rd_annual_fee" runat="server" Text="Annual Fee" GroupName="ab" AutoPostBack="true" OnCheckedChanged="rd_annual_fee_CheckedChanged" />
                                            <asp:RadioButton ID="rd_monthleyfee" runat="server" Text="Monthly Fee" GroupName="ab" AutoPostBack="true" OnCheckedChanged="rd_monthleyfee_CheckedChanged" />
                                        </div>
                                        <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnl_payment_history" runat="server" visible="false">
                                            <table class="table">
                                                <tr>
                                                    <td colspan="8"><b>Payment History</b>

                                                    </td>
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
                                                                        <asp:Label ID="lbl_slipno" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_Addmission_no" runat="server" Text='<%#Bind("Addmission_no") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Branchid" runat="server" Text='<%#Bind("Branch") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payment Mode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_paymenetmode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Type") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>Total</FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="td2" Width="100px" />
                                                                    <HeaderStyle CssClass="td2" />
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl_totalamount" runat="server" Font-Bold="true"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Transection_in" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Transection_in")%>'> </asp:Label>
                                                                        <asp:Button ID="btn_delete_bill" Visible="false" runat="server" Text="Delete Bill" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_delete_bill_Click" OnClientClick="return confirm('Are you sure you want to delete this?');" />
                                                                        <asp:Button ID="btn_update_payment_date" runat="server" Text="Update Payment Date" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_update_payment_date_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView> 
                                                    </td>
                                                </tr> 
                                            </table> 
                                        </div>
                                        <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnldate" runat="server" visible="false"> 
                                            <table style="padding: 0px; height: 43px; width: 90%; text-align: center; margin: 0px auto; float: none;">
                                                <tr>
                                                    <td>Enter New Date
                                                    </td>
                                                    <td>
                                                        <div class="clndr-div">
                                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                            <asp:TextBox ID="txt_date_new" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td>Payment Mode
                                                    </td>
                                                    <td>
                                                        <div class="clndr-div">
                                                            <asp:DropDownList ID="ddl_paymentmode" runat="server"    class="form-select find-dv-txtbx">
                                                                <asp:ListItem>Cash</asp:ListItem>
                                                                <asp:ListItem>Pos</asp:ListItem>
                                                                <asp:ListItem>Netbanking</asp:ListItem>
                                                                <asp:ListItem>Deposited In Bank</asp:ListItem>
                                                                <asp:ListItem>Sbdebit</asp:ListItem>
                                                                <asp:ListItem>Cheque</asp:ListItem>
                                                                <asp:ListItem>NEFT</asp:ListItem>
                                                                <asp:ListItem>Debitcard</asp:ListItem>
                                                                <asp:ListItem>Creditcard</asp:ListItem>
                                                                <asp:ListItem>Otherdcard</asp:ListItem>
                                                                <asp:ListItem>UPI</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_update_final" runat="server" Text="Update Payment Date" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_update_final_Click" />
                                                    </td>
                                                </tr>
                                            </table> 
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
</asp:Content>
