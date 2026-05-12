<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="fee-collect-type.aspx.cs" Inherits="school_web.Admin.fee_collect_type" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Fee Collection Method
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/js/jquery-1.10.2.min.js"></script>
    <style>
        /* Hide default radio button */
        .hidden-radio input[type="radio"] {
            display: none;
        }

        /* Container to align radios */
        .radio-container {
            display: flex;
            gap: 20px;
            margin-top: 10px;
        }

        /* Custom radio label */
        .custom-radio {
            display: flex;
            align-items: center;
            cursor: pointer;
            font-size: 16px;
            font-weight: 500;
            color: #333;
            transition: all 0.3s ease;
        }

        /* Custom radio button style */
        .radio-custom {
            width: 18px;
            height: 18px;
            border: 2px solid #007bff;
            border-radius: 50%;
            display: inline-block;
            position: relative;
            margin-right: 8px;
            transition: all 0.3s ease;
        }

            /* Inner dot (hidden by default) */
            .radio-custom::after {
                content: "";
                width: 10px;
                height: 10px;
                background-color: white;
                border-radius: 50%;
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                display: none;
            }

        /* When radio is checked, apply styles */
        .hidden-radio input[type="radio"]:checked + .radio-custom {
            background-color: #007bff;
            border-color: #0056b3;
        }

            .hidden-radio input[type="radio"]:checked + .radio-custom::after {
                display: block;
            }
    </style>
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
                <div class="breadcrumb-title pe-3">Fee Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Set Fee Collection Method</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-5">
                    <div class="card">
                        <div class="card-body">
                            <h6 class="mb-0 text-uppercase">Set Fee Collection Method</h6>

                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-12">
                                    <div class="radio-container">
                                        <label class="custom-radio">
                                            <asp:RadioButton ID="rd_monthwise" runat="server" GroupName="A" CssClass="hidden-radio" />
                                            <span class="radio-custom"></span>Month Wise
                                        </label>

                                        <label class="custom-radio">
                                            <asp:RadioButton ID="rd_inst_wise" runat="server" GroupName="A" CssClass="hidden-radio" />
                                            <span class="radio-custom"></span>Installment Wise
                                        </label>
                                    </div>


                                </div>


                                <div class="col-12">
                                    <asp:Button ID="btn_Submit" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-7">
                    <div class="card">
                        <div class="card-body">
                            <h6 class="mb-0 text-uppercase">Fee Collection Method Updated History</h6>
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Method</th>
                                                        <th>Updated By</th>
                                                        <th>Updated Date</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_mode_type" runat="server" Text='<%#Bind("Mode_type")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_updated_by_name" runat="server" Text='<%#Bind("Updated_by_name")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_created_date" runat="server" Text='<%#Bind("Created_date")%>'></asp:Label>
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
    </div>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const customRadios = document.querySelectorAll(".custom-radio");

            function updateRadioStyles() {
                customRadios.forEach(label => {
                    const radioInput = label.querySelector("input[type='radio']");
                    const radioCustom = label.querySelector(".radio-custom");

                    if (radioInput.checked) {
                        radioCustom.style.backgroundColor = "#007bff";
                        radioCustom.style.borderColor = "#0056b3";
                        radioCustom.style.display = "inline-block";
                    } else {
                        radioCustom.style.backgroundColor = "transparent";
                        radioCustom.style.borderColor = "#007bff";
                    }
                });
            }

            // Restore selection on page load
            updateRadioStyles();

            customRadios.forEach(label => {
                label.addEventListener("click", function () {
                    const radioInput = this.querySelector("input[type='radio']");
                    if (radioInput) {
                        radioInput.checked = true;

                        // Update styling for all radios in the same group
                        updateRadioStyles();
                    }
                });
            });
        });

    </script>
</asp:Content>
