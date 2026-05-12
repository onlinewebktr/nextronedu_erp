<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Hostel_student_wise_discount.aspx.cs" Inherits="school_web.Admin.Hostel_student_wise_discount" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Services Charge Discount Student Wise 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .fnd-box-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            border: 1px solid #ddd;
            border-radius: 2px;
        }

        .fnd-box-wpr-inr {
            margin: 0px;
            padding: 5px 5px;
            width: 100%;
            float: left;
        }

        .fnd-box-row-wpr-h {
            margin: 0px;
            padding: 5px 5px 4px 5px;
            width: 100%;
            float: left;
            font-size: 16px;
            border-bottom: 1px solid #ddd;
        }

        .fnd-box-row-wpr {
            margin: 3px 0px 3px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .form-label-fnds {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .padd-rght-5 {
            padding-right: 5px;
        }

        .padd-lft-5 {
            padding-left: 5px;
        }

        .padd-lft0 {
            padding-left: 0px;
        }

        .pdd-both0 {
            padding-left: 0px !important;
            padding-right: 0px !important;
        }

        .form-fnd-btns {
            margin: 0px;
            padding: 3px 10px;
        }

        .stdnt-info-fnds {
            margin: 3px 0px 3px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 13px;
        }

        .chkbx-all {
            margin: 0px 0px 0px 10px;
            padding: 0px 0px 0px 0px;
            font-weight: 500;
        }

        .hdrmodify {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .hdrmodify table tr th {
                background: #6ea9ff;
            }

        .pntsHeadS {
            margin: 0px 0px 2px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 17px;
            font-weight: 500;
            text-align: center;
            display: none;
        }

        .pntsDatesS {
            margin: 0px 0px 4px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            display: none;
        }

        .frms-row-wpr {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session_student.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'fee-collection-monthly-wise.aspx/GetRooPath',
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
            var sessionid = $("#<%=ddlsessionad.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({



                source: function (request, response) {
                    $.ajax({
                        url: 'fee-collection-monthly-wise.aspx/GetRooPathAdmNo',
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
                <div class="breadcrumb-title pe-3"><a href="Hostel_Master.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i>Hostel</a></div>

                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Set Discount Student Wise(Services Charge)</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <div class="row">
                <div class="col-xl-12">
                    <div class="col-xl-12">
                        <ul class="sub-pag-menu-ul sub-pag-menu-ul-mrgn">
                            <li style="display:none"><a href="Hostel_Admission_Fee_or_Annual_Master_Discount.aspx" class="sub-mnu-p-a">Set Discount on Admission/Annual Fees</a></li>

                            <li style="display:none"><a href="Hostel_discount_on_monthly_fee.aspx">Set Discount on Monthly Fees</a></li>
                             
                            <li><a href="Hostel_student_wise_discount.aspx" class="sub-mnu-p-a-active">Services Charge Discount Discount</a></li>
                        </ul>
                    </div>
                </div>
                <hr />
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row">
                                    <div class="col-xl-3">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:DropDownList ID="ddlsessionad" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Admission No.</label>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-xl-4">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Student Name</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:DropDownList ID="ddl_session_student" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Student Name</label>
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:Button ID="btn_find_name" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_name_Click" />
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
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_name" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_father_name" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lblclass" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Roll No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_roll_no" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Admission No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_admission_no" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Section : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_section" runat="server" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-4 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Type : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lbl_student_type" runat="server" class="stdnt-info-fnds"></asp:Label>
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
                                        <div class="col-xl-12">
                                            <div class="fnd-box-wpr" style="margin: 8px 0px 0px 0px;">
                                                <h2 class="fnd-box-row-wpr-h">Set Discount</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row" style="border-bottom: 0px solid #000;">
                                                            <div class="col-md-3">
                                                                <div class="frms-row-wpr">
                                                                    <label for="validationCustom01" class="form-label"><b>Discount On</b><sup> </sup></label>
                                                                    <asp:DropDownList ID="ddl_discount_on" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_discount_on_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <asp:Panel ID="pnl_discount_annul_or_admission_fee" runat="server" Visible="false">
                                                                <div class="col-xl-12">

                                                                    <div class="row">

                                                                        <div class="col-md-3">
                                                                            <label for="validationCustom01" class="form-label Llabel"><b>Select Hsotel</b></label>
                                                                            <asp:DropDownList ID="ddl_hostel_ad" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_hostel_ad_SelectedIndexChanged" class="form-select">
                                                                            </asp:DropDownList>
                                                                        </div>


                                                                        <div class="col-md-6" style="margin: 0px 0px 10px 0px;">
                                                                            <asp:Panel ID="pnl_fee_grid" runat="server" Visible="false">
                                                                                <label for="validationCustom01" class="form-label Llabel"><b>Fees Detail</b></label>
                                                                                <br />
                                                                                <asp:GridView ID="grd_fee" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center; margin: 0px 0px 0px 0px;" ShowFooter="True">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Fees Head">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                                                <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("content_id") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Fees Amount">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txt_fee" runat="server" Style="width: 80px;" Text='<%#Eval("amount") %>' onkeypress="return isNumberKey(event)" Enabled="false"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_full_amount" runat="server"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Disc. Amount">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txt_disc_fee" runat="server" Style="width: 80px;" AutoPostBack="true" OnTextChanged="txt_disc_fee_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbl_full_discount" runat="server"></asp:Label>
                                                                                            </FooterTemplate>

                                                                                        </asp:TemplateField>

                                                                                    </Columns>
                                                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                                    <RowStyle ForeColor="#000066" />
                                                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                                                </asp:GridView>
                                                                                <table class="table-bordered" style="width: 100%; display: none">
                                                                                    <tr>
                                                                                        <td style="padding: 5px 5px; width: 245px; text-align: right;">Total Discount</td>
                                                                                        <td style="padding: 5px 5px; width: 161px; text-align: right;">
                                                                                            <asp:Label ID="lbl_totalmrp" runat="server"></asp:Label></td>
                                                                                        <td style="padding: 5px 5px; text-align: right;">
                                                                                            <asp:Label ID="lbl_ttl_disc" runat="server"></asp:Label></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </div>
                                                                        <div class="col-12" style="text-align: center">
                                                                            <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" Style="width: 200px;" />

                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </asp:Panel>

                                                            <asp:Panel ID="pnl_discount_month" runat="server" Visible="false">


                                                                <div class="row" style="margin-top: 9px;">
                                                                    <div class="col-md-3">
                                                                        <label for="validationCustom01" class="form-label Llabel">Select Hsotel</label>
                                                                        <asp:DropDownList ID="ddl_hostel_month" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_hostel_month_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>


                                                                    <div class="col-md-3">
                                                                        <label for="validationCustom01" class="form-label Llabel">Room Category</label>
                                                                        <asp:DropDownList ID="ddl_room_catogery_month" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_room_catogery_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="row">



                                                                    <div class="col-md-12">
                                                                        <label for="validationCustom01" class="form-label" style="margin: 10px 0px 5px 0px;">Choose Month<sup>*</sup></label>
                                                                        <span class="chkbx-all">
                                                                            <asp:CheckBox ID="chk_all_month" runat="server" Text="Select All" OnCheckedChanged="chk_all_month_CheckedChanged" AutoPostBack="true" />
                                                                        </span>
                                                                        <br />
                                                                        <asp:Repeater ID="rp_months" runat="server">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chk_month_name" class="chkstle" runat="server" Text='<%#Eval("Month") %>' />
                                                                                <asp:Label ID="lbl_value" runat="server" Visible="false" Text='<%#Bind("Value")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_month_name" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_month_id" runat="server" Visible="false" Text='<%#Bind("Month_Id")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>



                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6" style="margin: 0px 0px 10px 0px;">
                                                                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                                            <label for="validationCustom01" class="form-label Llabel">Fees Detail</label>
                                                                            <br />
                                                                            <asp:GridView ID="grd_fee_month" runat="server" CssClass="table table-bordered table-striped" OnRowDataBound="grd_fee_month_RowDataBound" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center; margin: 0px 0px 0px 0px;" ShowFooter="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Fees Head">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                                                            <asp:Label ID="lbl_content_id" runat="server" Text='<%#Bind("content_id") %>' Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Fees Amount">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txt_fee" runat="server" Style="width: 80px;" Text='<%#Eval("amount") %>' onkeypress="return isNumberKey(event)" Enabled="false"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_full_amount" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Disc. Amount">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txt_disc_fee_month" runat="server" Style="width: 80px;" AutoPostBack="true" OnTextChanged="txt_disc_fee_month_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lbl_full_discount" runat="server"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                                <RowStyle ForeColor="#000066" />
                                                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                                <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                                            </asp:GridView>
                                                                            <table class="table-bordered" style="width: 100%; display: none">
                                                                                <tr>
                                                                                    <td style="padding: 5px 5px; width: 245px; text-align: right;">Total Discount</td>
                                                                                    <td style="padding: 5px 5px; width: 161px; text-align: right;">
                                                                                        <asp:Label ID="lbl_totalmrp_month" runat="server"></asp:Label></td>
                                                                                    <td style="padding: 5px 5px; text-align: right;">
                                                                                        <asp:Label ID="lbl_ttl_disc_month" runat="server"></asp:Label></td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-12" style="text-align: center">
                                                                    <asp:Button ID="btn_submit_data_month" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_submit_data_month_Click" Visible="false" />

                                                                </div>


                                                            </asp:Panel>


                                                            <asp:Panel ID="pnl_servicesmatser" runat="server" Visible="false">
                                                                <div class="col-md-12">
                                                                    <label for="validationCustom01" class="form-label" style="margin: 10px 0px 5px 0px;">Choose Month<sup>*</sup></label>
                                                                    <span class="chkbx-all">
                                                                        <asp:CheckBox ID="chk_all_month_servicesmaster" runat="server" Text="Select All" OnCheckedChanged="chk_all_month_servicesmaster_CheckedChanged" AutoPostBack="true" /></span>
                                                                    <br />
                                                                    <asp:Repeater ID="rp_month" runat="server">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk_month_name" class="chkstle" runat="server" Text='<%#Eval("Month") %>' />
                                                                            <asp:Label ID="lbl_value" runat="server" Visible="false" Text='<%#Bind("Value")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_month_name" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_month_id" runat="server" Visible="false" Text='<%#Bind("Month_Id")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="grd-wpr">
                                                                        <div class="row">
                                                                            <div class="col-md-8">

                                                                                <label for="validationCustom01" class="form-label Llabel">Fees Detail</label>
                                                                                <br />
                                                                                <table class="table table-striped table-bordered dataTable" style="border: 1px solid #ddd;">
                                                                                    <tr>
                                                                                        <th>#</th>
                                                                                        <th>Fees Head</th>
                                                                                        <th>Fees Amount</th>
                                                                                        <th>Disc. Amount</th>
                                                                                    </tr>

                                                                                    <asp:Repeater ID="rd_view" runat="server">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                </td>
                                                                                                <td style="text-align: left;">
                                                                                                    <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("Content_name")%>'></asp:Label>
                                                                                                    <asp:Label ID="lbl_content_id" Visible="false" runat="server" Text='<%#Bind("Content_id")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td style="text-align: left;">
                                                                                                    <asp:TextBox ID="txt_fee" runat="server" Style="width: 80px; pointer-events: none;" Text='<%#Eval("Fees_amount") %>'></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="text-align: left;">
                                                                                                    <asp:TextBox ID="txt_disc_fee_other_services" runat="server" Style="width: 80px;" AutoPostBack="true" OnTextChanged="txt_disc_fee_other_services_TextChanged"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>


                                                                                    <tr>
                                                                                        <td style="font-weight: 600; text-align: right" colspan="2">Total Discount</td>
                                                                                        <td style="font-weight: 600">
                                                                                            <asp:Label ID="lbl_totalmrp_other" runat="server"></asp:Label></td>
                                                                                        <td style="font-weight: 600">
                                                                                            <asp:Label ID="lbl_ttl_disc_other" runat="server"></asp:Label></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-12">
                                                                    <asp:Button ID="btn_submit_services" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_submit_services_Click" Style="float: left;" />
                                                                </div>
                                                            </asp:Panel>



                                                        </div>







                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Student Wise Services Charge Discount </h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Discount On</label>
                                                        <asp:DropDownList ID="ddl_discount_on_srch" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>


                                                    <div class="col-sm-2">
                                                        <asp:Button ID="btn_fnd_by_cat" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_by_cat_Click" />

                                                    </div>

                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 20px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 20px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>


                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr">
                                                    <div id="content">

                                                        <div class="pgslry-head-div head">

                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                            </div>
                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                                <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                                                </h1>

                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                    &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                </div>
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <span style="font-size: 14px; font-weight: bold;">Discount Student Wise
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                </div>
                                                            </div>


                                                        </div>

                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Student Name</th>
                                                                        <th>Adm No.</th>

                                                                        <th>Session</th>
                                                                        <th>Hostel Name</th>
                                                                        <th>Discount on</th>


                                                                        <th>Month</th>
                                                                        <th>Fees Head</th>
                                                                        <th>Fees Amount</th>
                                                                        <th>Disc. Amount</th>
                                                                        <th>After Disc.</th>
                                                                        <th>Action</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rd_viewaddedfee" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admission_no")%>'></asp:Label>
                                                                                </td>


                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Hostel_name" runat="server" Text='<%#Bind("Hostel_name")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_discount_on" runat="server" Text='<%#Bind("Discount_on")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("content")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_disc_amount" runat="server" Text='<%#Bind("disc_amount")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_after_disc" runat="server" Text='<%#Bind("after_disc")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">

                                                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                                                    <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_fee_head_id" runat="server" Text='<%#Bind("content_id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("session_id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_parameter_id" runat="server" Text='<%#Bind("parameter_id")%>' Visible="false"></asp:Label>

                                                                                    <asp:Label ID="lbl_Hostel_id" runat="server" Text='<%#Bind("Hostel_id")%>' Visible="false"></asp:Label>
                                                                                     <asp:Label ID="lbl_parameter" runat="server" Text='<%#Bind("parameter")%>' Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
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
            </div>
        </div>
    </div>


    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 750px;">
            <div class="popupTablWpR">
                <div class="row">
                    <div class="col-md-6">
                        <h2 class="popup-dt-h">Student Details</h2>
                    </div>
                    <div class="col-md-6">
                        <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Close</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>

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
                                    <asp:Label ID="lbl_session" Visible="false" runat="server" Text='<%#Bind("session") %>'></asp:Label>
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
    <style>
        .conf-btn-ul li a {
            margin: 0px 5px;
            padding: 0px 0px 1px;
            text-decoration: none;
            background: #ff0000;
            color: #fff;
            width: 50px;
            float: right;
            text-align: center;
            border-radius: 3px;
            font-size: 13px;
            line-height: 25px;
            font-weight: 500;
        }

        table tr th {
            padding: 10px 5px !important;
        }

        table tr td {
            padding: 10px 5px !important;
        }
    </style>

</asp:Content>
