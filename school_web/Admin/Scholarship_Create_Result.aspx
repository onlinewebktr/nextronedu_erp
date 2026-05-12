<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Scholarship_Create_Result.aspx.cs" Inherits="school_web.Admin.Scholarship_Create_Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Scholarship Create Result
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
        #notification {
            z-index: 9999;
        }

        tfoot, th, thead {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            background: #1c77fd !important;
            font-size: 12px !important;
            font-weight: bold;
        }

        td {
            font-size: 12px !important;
        }
    </style>
    <script src="../Grid_calender/Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Grid_calender/Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../Grid_calender/Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>
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
    <script>
        $(function () {
            $("#<%=txt_adm_date.ClientID %>").datepicker({
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
                <div class="breadcrumb-title pe-3">Scholarship</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Create Result</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">

                                <div class="row">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label" style="font-weight: 500; font-size: 1rem; background: #ffd700; padding: 5px 5px 5px 5px; width: 100%; text-align: center; color: #000;">
                                            Note:- If result has been published that you can't change result
                                        </label>
                                    </div>
                                </div>

                                <div class="row">



                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Scholarship <sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_Scholarship_name" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_Scholarship_name_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Scholarship For<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2" style="display:none">
                                                <label for="validationCustom01" class="form-label">Admission Date<sup>*</sup></label>
                                                <div class="clndr-div">
                                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                    <asp:TextBox ID="txt_adm_date" runat="server" class="form-control find-dv-txtbx" Style="width: 100%!important; height: 31px !important;"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2" style="display:none">
                                                <label for="validationCustom01" class="form-label">Full Marks<sup>*</sup></label>


                                                <asp:TextBox ID="txtfull_marks"  runat="server" class="form-control" onkeypress="return isNumberKey(event)">100</asp:TextBox>

                                            </div>



                                            <div class="col-1">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_find_Click" Style="margin: 24px 0px 0px 0px; padding: 3px 10px; width: 60px!important; height: 31px!important;" />
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>

                                        <div id="tblPrintIQ" runat="server">
                                            <div class="prnt-dv-wpr">


                                                <div class="table-responsive" style="margin-top: 10px;">
                                                    <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reg. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Registration_id" runat="server" Text='<%#Bind("Registration_id")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_testid" runat="server" Text='<%#Bind("Test_id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Scholarship for">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Class")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="DOB">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_DOB" runat="server" Text='<%#Bind("DOB")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Admission Date">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_admssion_Date" Style="width: 100px!important; height: 30px!important;"
                                                                        runat="server" class="Calender"></asp:TextBox>
                                                                    <img src="../Grid_calender/calender.png" style="float: right; margin: 6px 0px 0px -18px; position: absolute;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Time">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddl_hours" runat="server">
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>01</asp:ListItem>
                                                                        <asp:ListItem>02</asp:ListItem>
                                                                        <asp:ListItem>03</asp:ListItem>
                                                                        <asp:ListItem>04</asp:ListItem>
                                                                        <asp:ListItem>05</asp:ListItem>
                                                                        <asp:ListItem>06</asp:ListItem>
                                                                        <asp:ListItem>07</asp:ListItem>
                                                                        <asp:ListItem>08</asp:ListItem>
                                                                        <asp:ListItem>09</asp:ListItem>


                                                                    </asp:DropDownList>


                                                                    <asp:DropDownList ID="ddl_minutes" runat="server">
                                                                        <asp:ListItem>00</asp:ListItem>
                                                                        <asp:ListItem>01</asp:ListItem>
                                                                        <asp:ListItem>02</asp:ListItem>
                                                                        <asp:ListItem>03</asp:ListItem>
                                                                        <asp:ListItem>04</asp:ListItem>
                                                                        <asp:ListItem>05</asp:ListItem>
                                                                        <asp:ListItem>06</asp:ListItem>
                                                                        <asp:ListItem>07</asp:ListItem>
                                                                        <asp:ListItem>08</asp:ListItem>
                                                                        <asp:ListItem>09</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>

                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                        <asp:ListItem>25</asp:ListItem>
                                                                        <asp:ListItem>26</asp:ListItem>
                                                                        <asp:ListItem>27</asp:ListItem>
                                                                        <asp:ListItem>28</asp:ListItem>
                                                                        <asp:ListItem>29</asp:ListItem>
                                                                        <asp:ListItem>30</asp:ListItem>
                                                                        <asp:ListItem>31</asp:ListItem>
                                                                        <asp:ListItem>32</asp:ListItem>
                                                                        <asp:ListItem>33</asp:ListItem>
                                                                        <asp:ListItem>34</asp:ListItem>
                                                                        <asp:ListItem>35</asp:ListItem>
                                                                        <asp:ListItem>36</asp:ListItem>
                                                                        <asp:ListItem>37</asp:ListItem>
                                                                        <asp:ListItem>38</asp:ListItem>
                                                                        <asp:ListItem>39</asp:ListItem>
                                                                        <asp:ListItem>40</asp:ListItem>

                                                                        <asp:ListItem>41</asp:ListItem>
                                                                        <asp:ListItem>42</asp:ListItem>
                                                                        <asp:ListItem>43</asp:ListItem>
                                                                        <asp:ListItem>44</asp:ListItem>
                                                                        <asp:ListItem>45</asp:ListItem>
                                                                        <asp:ListItem>46</asp:ListItem>
                                                                        <asp:ListItem>47</asp:ListItem>
                                                                        <asp:ListItem>48</asp:ListItem>
                                                                        <asp:ListItem>49</asp:ListItem>
                                                                        <asp:ListItem>50</asp:ListItem>
                                                                        <asp:ListItem>51</asp:ListItem>
                                                                        <asp:ListItem>52</asp:ListItem>
                                                                        <asp:ListItem>53</asp:ListItem>
                                                                        <asp:ListItem>54</asp:ListItem>
                                                                        <asp:ListItem>55</asp:ListItem>
                                                                        <asp:ListItem>56</asp:ListItem>
                                                                        <asp:ListItem>57</asp:ListItem>
                                                                        <asp:ListItem>58</asp:ListItem>
                                                                        <asp:ListItem>59</asp:ListItem>
                                                                    </asp:DropDownList>


                                                                    <asp:DropDownList ID="ddl_am_pm" runat="server">
                                                                        <asp:ListItem>AM</asp:ListItem>
                                                                        <asp:ListItem>PM</asp:ListItem>

                                                                    </asp:DropDownList>

                                                                </ItemTemplate>
                                                                <ItemStyle Width="160px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Attendance Status">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddl_attendance_status" runat="server">
                                                                        <asp:ListItem>Present</asp:ListItem>
                                                                        <asp:ListItem>Absent</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Exam Result">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddl_exam_result" runat="server">
                                                                        <asp:ListItem>Qualified</asp:ListItem>
                                                                        <asp:ListItem>Disqualified</asp:ListItem>
                                                                        <asp:ListItem>Rejected</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Full Marks">
                                                                <ItemTemplate>

                                                                    <asp:TextBox ID="txt_full_marks" Style="width: 70px!important;" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Obtain Marks">
                                                                <ItemTemplate>

                                                                    <asp:TextBox ID="txt_Obtain_marks" Style="width: 70px!important;" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Obtain(%)">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lbl_Obtain_percentage" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rank">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lbl_Rank" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>

                                                    <asp:Button ID="btn_save" runat="server" Text="Save and Calculate Rank" Visible="false" CssClass="btn btn-success" CausesValidation="false" OnClick="btn_save_Click" Style="margin: 0px 4px 0px 0px; padding: 6px 10px; width: 60px!important; height: 37px!important; float: right;" />
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
        <!--end row-->
    </div>
</asp:Content>
