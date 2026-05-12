<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Set_Boarding_Point_Wise_Fee.aspx.cs" Inherits="school_web.Admin.Set_Boarding_Point_Wise_Fee" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Set  Fee
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
    <style>
        .Llabel {
            margin: 11px 0px 6px 0px;
        }

        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            z-index: 1050;
            display: none;
            width: 100%;
            height: 100%;
            overflow: hidden;
            outline: 0;
        }

        .modal-header {
            padding: 7px 15px;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Transportation</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Set Fee</li>
                        </ol>
                    </nav>
                </div>

                <a href="#" data-toggle="modal" data-target="#myModal1" style="float: right; position: absolute; right: 0px; font-size: 17px; top: 2px; background: #0df514; border: 1px solid #fff; padding: 2px 3px 2px 3px; color: #000;"><i class="bx bx-cog"></i>Copy Fee</a>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Set Fees"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">

                                    <div class="col-lg-2">
                                        <div class="position-relative form-group">
                                            <label>Session<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="col-lg-4">
                                        <div class="position-relative form-group">
                                            <label>Vehicle Name<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_vechile_name" AutoPostBack="true" OnSelectedIndexChanged="ddl_vechile_name_SelectedIndexChanged" runat="server" class="form-select find-dv-txtbx">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="position-relative form-group">
                                            <label>Vehicle Route<sup>*</sup></label>
                                            <asp:DropDownList ID="ddl_Vehicle_Raute" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_Vehicle_Raute_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="position-relative form-group">
                                            <label>Vehicle No/Van No.<sup>*</sup></label>
                                            <asp:TextBox ID="txt_vehicle_no" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="clearfix"></div>


                                    <div class="col-xl-3">
                                        <div class="position-relative form-group">
                                            <label>Boarding Point<sup> *<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_boardingpoint"></asp:RequiredFieldValidator></sup></label>
                                            <asp:TextBox ID="txt_boardingpoint" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-xl-3">
                                        <div class="position-relative form-group">
                                            <label>
                                                Distance KM<sub>*
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_distance_km"></asp:RequiredFieldValidator></sub>
                                            </label>
                                            <asp:DropDownList ID="ddl_distance_km" runat="server" class="form-select find-dv-txtbx">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-xl-3">
                                        <div class="position-relative form-group">
                                            <label>Transport Fee <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_transportfee"></asp:RequiredFieldValidator></sup></label>
                                            <asp:TextBox ID="txt_transportfee" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-xl-3">
                                        <div class="position-relative form-group">

                                            <asp:Button ID="btn_add_boarding_point" runat="server" ValidationGroup="a" Text="Add" CausesValidation="false" CssClass="btn btn-primary" OnClick="btn_Submit_Click" Style="padding: 4px 40px; margin-top: 20px;" />

                                            <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 0px 0px 6px 0px; float: right" OnClick="btn_excels_Click" Visible="false" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                        </div>
                                    </div>





                                    <asp:GridView ID="GrdView" runat="server" class="mb-0 table table-bordered" AutoGenerateColumns="False" Width="100%" Style="margin-top: 10px;">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Boarding Point">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Boarding_Point" runat="server" Text='<%#Bind("Boarding_Point")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="KM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_km" runat="server" Text='<%#Bind("KM")%>'></asp:Label>
                                                    <asp:Label ID="lbl_km_id_Distance_id" runat="server" Text='<%#Bind("Distance_id")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Trasport Fee">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_transportfee" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                        <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                            href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                            <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                <i class="bx bx-grid-horizontal"></i>
                                                            </div>
                                                        </a>
                                                        <ul class="dropdown-menu dropdown-menu-end">
                                                            <li>
                                                                <asp:LinkButton ID="lnkEdit" class="dropdown-item" runat="server" OnClick="lnkEdit_Click" CausesValidation="false"> <i class="lni lni-pencil-alt"> </i><span>Edit</span> </asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton ID="lnk_change_vehicle" class="dropdown-item" runat="server" OnClick="lnk_change_vehicle_Click" CausesValidation="false"> <i class="lni lni-pencil-alt"> </i><span>Change Vehicle</span></asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton ID="lnkDel" class="dropdown-item" runat="server" OnClick="lnkDel_Click" OnClientClick="return confirm('Are you sure want to delete?');" CausesValidation="false"><i class="lni lni-trash"> </i><span>Delete</span></asp:LinkButton></li>

                                                            <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_TransportationPath_id" runat="server" Text='<%#Bind("TransportationPath_id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Transportation_Id" runat="server" Text='<%#Bind("Transportation_Id")%>' Visible="false"></asp:Label>

                                                            <asp:Label ID="lbl_Boarding_Point_id" runat="server" Text='<%#Bind("Boarding_Point_id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Session_Id" runat="server" Text='<%#Bind("Session_Id")%>' Visible="false"></asp:Label>
                                                        </ul>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>




                                    <div style="height: 1px; overflow: hidden">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Style="margin-top: 10px;">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Boarding Point">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Boarding_Point" runat="server" Text='<%#Bind("Boarding_Point")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_km" runat="server" Text='<%#Bind("KM")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Trasport Fee">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_transportfee" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
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
    </div>






    <div class="modal fade" id="myModal1" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 500px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Copy Bording Point fee for Next Session</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">From Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_current_session" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">To Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_copy_to_session" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>



                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_copy_fee_for_boarding" OnClick="btn_copy_fee_for_boarding_Click1" runat="server" Text="Copy" class="btn btn-success" OnClientClick="return confirm('Are you sure you want to boarding fee fee?');" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="myMdlUpdateBus" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 500px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Change Vehicle</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row" style="background: #ebe666; padding: 5px 5px 5px 5px; border-radius: 4px;">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Boarding Point</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_boarding_point_update" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <%-- ============================================ --%>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Choose Vehicle</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_vehicle_update" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_vehicle_update_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Route</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_route_update" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Vehicle No.</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_vehicle_no_update" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Distance</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_distance_km_update" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_change_vehicle" OnClick="btn_change_vehicle_Click" runat="server" Text="Update" class="btn btn-success" OnClientClick="return confirm('Are you sure you want to boarding fee fee?');" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openModal1() {
            $('#myModal1').modal('show');
        }

        function openModalVehicle() {
            $('#myMdlUpdateBus').modal('show');
        }
    </script>
</asp:Content>
