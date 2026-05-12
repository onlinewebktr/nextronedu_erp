<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Update_Roll_No.aspx.cs" Inherits="school_web.Admin.Update_Roll_No" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Update Student Details
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


    <script src="../Grid_calender/Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Grid_calender/Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../Grid_calender/Styles/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $(".Calender").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                minDate: 0,
                startDate: new Date(),

                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>

    <style>
        table.dataTable td, table.dataTable th {
            position: relative;
        }

        .fxtblWpr {
            width: 100%;
            height: 410px;
            background-color: #ddd;
            overflow: auto;
            border: 1px solid #ccc;
        }

        table {
            width: 100% !important;
            overflow-x: scroll;
        }

        td, th {
            border: 1px solid #ccc;
        }

        th {
            font-weight: 600;
            text-align: left;
            background-color: #f1f4f7;
        }

        .fixed-td {
            position: sticky !important;
            z-index: 2;
            left: 0;
            color: #000;
            background-color: #efff00 !important;
        }

        .fixed-hd {
            position: sticky !important;
            top: 0;
            z-index: 1 !important;
        }

        .left-top-td {
            z-index: 3 !important;
        }

        /*.scrollable-td {
            width: 200px;
        }*/

        .noline-break {
            white-space: nowrap;
            word-break: keep-all;
        }

        .txtbxstyles {
            margin: 0px;
            padding: 3px 4px 2px;
            height: 27px;
            border: 1px solid #959595;
            font-size: 13px;
        }

        table tr th {
            padding: 4px 5px !important;
        }

        table tr td {
            padding: 4px 5px !important;
        }
    </style>

    <%--<script type="text/javascript"> 
        $(function () {
            var sessionid = $("#<%=ddlsessionad.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Update_Roll_No.aspx/GetRooPathAdmNo',
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
    </script>--%>
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
                            <li class="breadcrumb-item active" aria-current="page">Update Student Details</li>
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
                        <div class="card-body">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-8">
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                            <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                            <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>


                                                        <div class="col-sm-3">
                                                            <label for="validationCustom01" class="find-dv-lbl" style="width: auto;">Section</label>
                                                            <asp:DropDownList ID="ddl_secion" runat="server" class="form-control find-dv-txtbx">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Button ID="btn_fnd" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_Click" />
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="col-sm-4">
                                                    <div class="row">

                                                        <div class="col-sm-8">
                                                            <label for="validationCustom01" class="find-dv-lbl">Enter Admission No.</label>
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click" Style="margin-top: 20px;" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr" style="overflow: auto;">
                                            <div class="fxtblWpr">
                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th class="fixed-td fixed-hd left-top-td">#</th>
                                                            <th class="fixed-td fixed-hd left-top-td">Student</th>
                                                            <th class="scrollable-td fixed-hd">Student's Name</th>
                                                            <th class="scrollable-td fixed-hd">Section</th>
                                                            <th class="scrollable-td fixed-hd">Roll No.</th>
                                                            <th class="scrollable-td fixed-hd">Date of Birth</th>

                                                            <th class="scrollable-td fixed-hd">Father's Name</th>
                                                            <th class="scrollable-td fixed-hd">Mother's Name</th>

                                                            <th class="scrollable-td fixed-hd">Adm./Reg No.</th>
                                                            <th class="scrollable-td fixed-hd">Student PEN No.</th>
                                                            <th class="scrollable-td fixed-hd">Gender</th>
                                                            <th class="scrollable-td fixed-hd">Blood Group</th>
                                                            <th class="scrollable-td fixed-hd">Religion</th>
                                                            <th class="scrollable-td fixed-hd">Caste Category</th>
                                                            <th class="scrollable-td fixed-hd">Aadhar No</th>
                                                            <th class="scrollable-td fixed-hd">Date of Admission</th>
                                                            <th class="scrollable-td fixed-hd">Father's whatsApp No</th>
                                                            <th class="scrollable-td fixed-hd">Mother's whatsApp No</th>
                                                            <th class="scrollable-td fixed-hd">Father's Mobile No</th>
                                                            <th class="scrollable-td fixed-hd">Mother's Mobile No</th>
                                                            <th class="scrollable-td fixed-hd">Father's Email Id</th>

                                                            <th class="scrollable-td fixed-hd">Height</th>
                                                            <th class="scrollable-td fixed-hd">Weight</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="fixed-td">
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td class="fixed-td noline-break">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        (<asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>) 
                                                                        <asp:Label ID="lbl_Section" Visible="false" runat="server" Text='<%#Bind("Section")%>'></asp:Label>


                                                                        <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_student_name" runat="server" Text='<%#Bind("studentname")%>' Style="width: 170px;" class="form-control txtbxstyles"></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_section" runat="server" Text='<%#Bind("Section")%>' Style="width: 70px;" class="form-control txtbxstyles"></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                         <%--onkeypress="return isNumberKey(event)"--%>
                                                                        <asp:TextBox ID="txt_roll_no" runat="server" Style="width: 70px;" class="form-control txtbxstyles" Text='<%#Bind("rollnumber")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_date_of_birth" class="Calender form-control txtbxstyles" onkeyup="var v = this.value; if (v.match(/^\d{2}$/) !== null) {this.value = v + '/';} else if (v.match(/^\d{2}\/\d{2}$/) !== null) {this.value = v + '/';}" MaxLength="10" Style="width: 105px; pointer-events: none" runat="server" Text='<%#Bind("dob")%>'></asp:TextBox>
                                                                        <img src="../Grid_calender/calender.png" style="position: absolute; right: 10px; top: 36%;" />
                                                                    </td>


                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_father_name" runat="server" class="form-control txtbxstyles" Text='<%#Bind("fathername")%>' Style="width: 170px;"></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_mother_name" class="form-control txtbxstyles" runat="server" Text='<%#Bind("mothername")%>' Style="width: 170px;"></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_adm_no_reg_no" class="form-control txtbxstyles" runat="server" Text='<%#Bind("Admission_no_date")%>' Style="width: 170px;"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_pen_no" class="form-control txtbxstyles" Style="width: 120px;" runat="server" Text='<%#Bind("Student_pen_no")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:DropDownList ID="ddl_gender" runat="server" class="form-control txtbxstyles">
                                                                            <asp:ListItem>Select</asp:ListItem>
                                                                            <asp:ListItem>MALE</asp:ListItem>
                                                                            <asp:ListItem>FEMALE</asp:ListItem>
                                                                            <asp:ListItem>TRANSGENDER</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lbl_gender" runat="server" Visible="false" Text='<%#Bind("gender")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:DropDownList ID="ddl_blood_group" runat="server" class="form-control txtbxstyles">
                                                                            <asp:ListItem>NA</asp:ListItem>
                                                                            <asp:ListItem>A+</asp:ListItem>
                                                                            <asp:ListItem>A-</asp:ListItem>
                                                                            <asp:ListItem>B+</asp:ListItem>
                                                                            <asp:ListItem>B-</asp:ListItem>
                                                                            <asp:ListItem>O+</asp:ListItem>
                                                                            <asp:ListItem>O-</asp:ListItem>
                                                                            <asp:ListItem>AB+</asp:ListItem>
                                                                            <asp:ListItem>AB-</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lbl_blood_group" runat="server" class="form-control txtbxstyles" Visible="false" Text='<%#Bind("blood_group")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:DropDownList ID="ddl_religion" runat="server" class="form-control txtbxstyles">
                                                                            <asp:ListItem>Select</asp:ListItem>
                                                                            <asp:ListItem>HINDU</asp:ListItem>
                                                                            <asp:ListItem>ISLAM</asp:ListItem>
                                                                            <asp:ListItem>SIKH</asp:ListItem>
                                                                            <asp:ListItem>CHRISTIAN</asp:ListItem>
                                                                            <asp:ListItem>BUDDHISM</asp:ListItem>
                                                                            <asp:ListItem>JAIN</asp:ListItem>
                                                                            <asp:ListItem>N/A</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lbl_religion" class="form-control txtbxstyles" runat="server" Visible="false" Text='<%#Bind("religion")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:DropDownList ID="ddl_cast" runat="server" class="form-control txtbxstyles">
                                                                            <asp:ListItem>Select</asp:ListItem>
                                                                            <asp:ListItem>GENERAL</asp:ListItem>
                                                                            <asp:ListItem>OBC</asp:ListItem>
                                                                            <asp:ListItem>OBC-A</asp:ListItem>
                                                                            <asp:ListItem>OBC-B</asp:ListItem>
                                                                            <asp:ListItem>ST</asp:ListItem>
                                                                            <asp:ListItem>SC</asp:ListItem>
                                                                            <asp:ListItem>BC</asp:ListItem>
                                                                            <asp:ListItem>OTHERS</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lbl_caste_category" runat="server" class="form-control txtbxstyles" Visible="false" Text='<%#Bind("cast")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_aadharno" style="width:100px" MaxLength="16" onkeypress="return isNumberKey(event)" Text='<%#Bind("aadharno")%>' runat="server" class="form-control txtbxstyles"></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_date_of_admission" class="Calender form-control txtbxstyles" Style="width: 105px; pointer-events: none" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:TextBox>
                                                                        <img src="../Grid_calender/calender.png" style="position: absolute; right: 10px; top: 36%;" />
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_Father_whatsApp_no" Style="width: 110px;" class="form-control txtbxstyles" runat="server" onkeypress="return isNumberKey(event)" Text='<%#Bind("Father_whatsApp_no")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_Mother_whatsApp_no" Style="width: 110px;" class="form-control txtbxstyles" runat="server" onkeypress="return isNumberKey(event)" Text='<%#Bind("Mother_whatsApp_no")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_father_mobile" runat="server" Style="width: 110px;" class="form-control txtbxstyles" Text='<%#Bind("father_mob")%>' onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_mother_mobile_no" runat="server" Style="width: 110px;" class="form-control txtbxstyles" Text='<%#Bind("mother_mob")%>' onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_father_email_id" runat="server" Style="width: 110px;" class="form-control txtbxstyles" Text='<%#Bind("email_id")%>'></asp:TextBox>
                                                                    </td>


                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_height" runat="server" Style="width: 110px;" class="form-control txtbxstyles" Text='<%#Bind("Height")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td class="scrollable-td">
                                                                        <asp:TextBox ID="txt_weight" runat="server" Style="width: 110px;" class="form-control txtbxstyles" Text='<%#Bind("Weight")%>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>

                                            <asp:Button ID="btn_save" runat="server" Text="Save" class="btn btn-primary find-dv-btn" OnClick="btn_save_Click" Style="padding: 10px 14px 10px 14px; margin-bottom: 10px;" Visible="false" />



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
</asp:Content>
