<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Change_Password_User.aspx.cs" Inherits="school_web.Payroll.Change_Password_User" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--favicon-->
    <script src="../assets/js/jquery-1.10.2.min.js"></script>

    <script src="../assets/js/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <link href="css/sale_entry.css" rel="stylesheet" />

    <link href="https://fonts.googleapis.com/css2?family=Lato:ital,wght@0,100;0,300;0,400;1,100&display=swap" rel="stylesheet" />

    <link href="../assets/css/icons.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
    <style>
        body, #form1 {
            font-size: 13px;
            color: #fff;
            letter-spacing: .5px;
            /* background: #f7f7ff; */
            background: #9fa19f;
            overflow-x: hidden;
            font-family: 'Lato', sans-serif;
        }

        sup {
            top: -.5em;
            color: #f00;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="page-wrapper">
                <div class="page-content">
                    <div id="notification">
                        <div id="pan" class="notificationpan">

                            <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                                <div class="d-flex align-items-center">

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
                    <div class="page-breadcrumb d-flex align-items-center mb-2 eeewe">


                        <div class="col-xl-11">
                            <div class="ps-3 d-none d-sm-flex">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb mb-0 p-0">
                                        <li class="breadcrumb-item">
                                            <a style="color: #fff;" id="a1" runat="server"><i class="bx bx-home-alt" style="color: #fff;"></i></a>
                                        </li>
                                        <li class="breadcrumb-item active" aria-current="page" style="color: #fff;">Change Password</li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                        <div class="col-xl-1">
                            <nav aria-label="breadcrumb">
                                <ol class="breadcrumb mb-0 p-0">
                                    <li class="breadcrumb-item">
                                        <a id="a2" runat="server" style="background: #e80c0c; color: #fff; padding: 5px 5px 5px 5px; border-radius: 5px; margin: 0px 0px 0px 55px; text-decoration: none;"><i class="bx bx-arrow-back"></i>Back</a>
                                    </li>

                                </ol>
                            </nav>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-xl-3"></div>
                        <div class="col-xl-6">
                            <div class="card" style="margin-top: 50px;">
                                <div class="card-body">
                                    <div class="p-4 border rounded" style="background: #690259; color: #fff;">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="validationCustom01" class="form-label">Enter Employee ID<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_employeecode"></asp:RequiredFieldValidator></sup></label>
                                                <asp:TextBox ID="txt_employeecode" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12">
                                                <label for="validationCustom01" class="form-label">New Password<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_newpaswword"></asp:RequiredFieldValidator></sup></label>
                                                <asp:TextBox ID="txt_newpaswword" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                                            </div>




                                            <div class="col-12">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Change Password" TextMode="Password" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" Style="margin: 30px 0px 0px 0px;" />

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-3"></div>


                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
