<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Hostel_Damage_Item_Mapping_With_Student.aspx.cs" Inherits="school_web.Admin.Hostel_Damage_Item_Mapping_With_Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3"><a href="Hostel_Master.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i>Hostel</a></div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Damage Item Mapping</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">

                <div class="col-xl-12">

                    <ul class="sub-pag-menu-ul">
                        <li><a href="Hostel_Damage_master.aspx">Item Master</a></li>

                        <li><a href="Hostel_Damage_Item_Mapping_With_Student.aspx" class="sub-mnu-p-a-active">Damage Item Mapping</a></li>

                        <li><a href="Hostel_Damage_Item_View_Mapped_With_Student.aspx">View Item Mapped </a></li>
                    </ul>
                </div>



                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Damage Item Mapping"></asp:Label>
                    <hr />
                     <div class="col-xl-4"></div>
                    <div class="col-xl-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="p-4 border rounded">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">Item Name<sup> </sup></label>
                                            <asp:DropDownList ID="ddl_item_name" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddl_item_name_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>


                                        </div>
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">Price<sup>*</sup></label>
                                            <asp:TextBox ID="txt_amount" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">Payable By<sup>*</sup></label>
                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">Remarks<sup>*</sup></label>
                                            <asp:TextBox ID="txt_remarks" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>


                                        <div class="col-12">
                                            <div style="overflow: hidden; height: 1px;">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Send to Month Bill" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                            </div>
                                            <a onclick="save_data()" class="btn btn-primary" style="width: 155px; width: 155px; margin: -6px 0px 0px -1px;">Send to Month Bill</a>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                     <div class="col-xl-4"></div>
                </div>



            </div>
        </div>
        <!--end row-->
    </div>
    <script type="text/javascript">
        function Confirm() {

            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to submit?")) {
                confirm_value.value = "Yes";

            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }


        function save_data() {
            var valsubmit = $('#<%=btn_Submit.ClientID %>').val();
              if (valsubmit == "Send to Month Bill") {

                  $('#<%=btn_Submit.ClientID %>').val('Submitting.. Please Wait..');
               <%--   $('#<%=btn_Submit.ClientID %>').click(); --%>
                  Confirm();
                  document.getElementById("<%=btn_Submit.ClientID %>").click();

            }
            else {
                alert("Already submitted")
            }

        }
    </script>
</asp:Content>
