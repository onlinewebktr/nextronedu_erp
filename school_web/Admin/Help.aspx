<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="school_web.Admin.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Update Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 3px;
            left: 427px;
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


            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Help</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Update Details</li>
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
                            <div class="table-responsive">

                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.66&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 19/09/2024</h3>
                                <p style="margin-bottom: 5px;">1. Push notification upgrade. </p>


                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.65&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 13/08/2024</h3>
                                <p style="margin-bottom: 5px;">1. Student profile in web </p>
                                <p style="margin-bottom: 5px;">2. Teacher Profile in web</p>
                                <p style="margin-bottom: 5px;">3. Fee collection optimization </p>
                                <p style="margin-bottom: 5px;">4. Security update</p>
                                <p style="margin-bottom: 5px;">5. Daily Collection Sheet</p>
                                <p style="margin-bottom: 5px;">6. Logout issue resolved</p>
                                <p style="margin-bottom: 5px;">7. Student Attendance delete option</p>

                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.64&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 29/06/2024</h3>
                                <p style="margin-bottom: 5px;">1. Print report card exam-wise</p>
                                <p style="margin-bottom: 5px;">2. Principale app.</p>
                                <p style="margin-bottom: 5px;">3. Report card & admit card show in teacher profile.</p>
                                <p style="margin-bottom: 5px;">4. Admit card-A4,A5</p>
                                <p style="margin-bottom: 5px;">5. Group-wise menu permission.</p>



                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.63&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 08/06/2024</h3>
                                <p style="margin-bottom: 5px;">1. Add Fount Office Enquiry and Flow up module.</p>
                                <p style="margin-bottom: 5px;">2. Transport assigned at the time admission.</p>
                                <p style="margin-bottom: 5px;">3. Student parent mappint.</p>
                                <p style="margin-bottom: 5px;">4. WhatsApp & message module.</p>
                                <p style="margin-bottom: 5px;">5. Separate dues report for transport.</p>
                                <p style="margin-bottom: 5px;">5. Separate dues report for transport.</p>
                                <p style="margin-bottom: 5px;">6. Separate edit option for student.</p>
                                <p style="margin-bottom: 5px;">7. Demand bill.</p>
                                <p style="margin-bottom: 5px;">8. Parent APP.</p>
                                <p style="margin-bottom: 5px;">9. Hostel out-pass.</p>
                                <p style="margin-bottom: 5px;">10. Student list page modification.</p>
                                <p style="margin-bottom: 5px;">11. Student-wise discount page modification.</p>
                                <p style="margin-bottom: 5px;">12. Student-wise discount page modification.</p>
                                <p style="margin-bottom: 5px;">13. Dashboard modification.</p>
                                <p style="margin-bottom: 5px;">14. Visitor APP. Visitor App Login By default User id-visitor and pwd-visitor@123</p>


                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.62&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 16/05/2024</h3>
                                <p style="margin-bottom: 5px;">1.Sale return</p>
                                <p style="margin-bottom: 5px;">2. Sale return report user wise </p>
                                <p style="margin-bottom: 5px;">3. Sale return report return  </p>
                                <p style="margin-bottom: 5px;">4. Sale transfer to main stock  </p>
                                <p style="margin-bottom: 5px;">5. Payment mode change option after billing   </p>
                                <p style="margin-bottom: 5px;">6. Student wallet module    </p>
                                <p style="margin-bottom: 5px;">7. Item wise sale price update option    </p>
                                <p style="margin-bottom: 5px;">8. Boarding point wise student list in the transport    </p>
                                <p style="margin-bottom: 5px;">9. Student Id card    </p>
                                <p style="margin-bottom: 5px;">10. Employee id card single print front side and Back side   </p>
                                <p style="margin-bottom: 5px;">11. Employee Id card collection form and permission </p>
                                <p style="margin-bottom: 5px;">12. Student Id card data collection and permission</p>
                                <p style="margin-bottom: 5px;">13. Message Send Panel </p>
                                <p style="margin-bottom: 5px;">14. Month dues message and over all dues message send via Whatsapp api</p>
                                <p style="margin-bottom: 5px;">15. Subject assigned and subject mapping Password required</p>
                                <p style="margin-bottom: 5px;">16. Examination setting copy Option</p>
                                <p style="margin-bottom: 5px;">17. Fee Master Copy Option </p>
                                <p style="margin-bottom: 5px;">18. Setup Option – Bulk Hostel assigned, Bulk Transport Assigned, Monthly Fee update</p>
                                <p style="margin-bottom: 5px;">19. Student list page modify</p>
                                <p style="margin-bottom: 5px;">20. Add Transport, Add Hostel, Add discount and add Extra Fee Head at fee collection page </p>
                                <p style="margin-bottom: 5px;">21. Transport Dues list separate </p>
                                <p style="margin-bottom: 5px;">22. Individual Student Edit Option </p>
                                <p style="margin-bottom: 5px;">23. Hostel assigned at the time admission</p>
                                <p style="margin-bottom: 5px;">24. Bug Fixed</p>

                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.29&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 28/10/2023</h3>
                                <p style="margin-bottom: 5px;">1. Hostel Module</p>
                                <p style="margin-bottom: 5px;">2. Sale & Purchase Module</p>
                                <p style="margin-bottom: 5px;">3. Payroll Module</p>
                                <p style="margin-bottom: 5px;">4.Custom Month Selection List </p>
                                <p style="margin-bottom: 5px;">5.Fee Collection Modify </p>
                                <p style="margin-bottom: 5px;">6.Admission Form Change</p>
                                <p style="margin-bottom: 5px;">7.Custom Report For Student</p>
                                <p style="margin-bottom: 5px;">8.Custom Report For Student</p>
                                <p style="margin-bottom: 5px;">9.Bulk image and indivisual image update</p>
                                <p style="margin-bottom: 5px;">10.Watter mark slip </p>
                                <p style="margin-bottom: 5px;">11.Student Attendance Report  </p>
                                <p style="margin-bottom: 5px;">12.Student ledger </p>
                                <p style="margin-bottom: 5px;">14.Online admission transfer </p>
                                <p style="margin-bottom: 5px;">15.Student transfer - old class to new class </p>
                                <p style="margin-bottom: 5px;">16.Split Month </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.28&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 22/02/2024</h3>




                                <p style="margin-bottom: 5px;">1. Admission no. update on form sale.</p>
                                <p style="margin-bottom: 5px;">2. Assigned subject summary report. show no of student assigned in which subject.</p>
                                <p style="margin-bottom: 5px;">3. Revise last bill of monthly payment of a student.</p>
                                <p style="margin-bottom: 5px;">4. Edit admission fee after fee has been taken.</p>
                                <p style="margin-bottom: 5px;">5. Now you can send message, events & notice with link.</p>
                                <p style="margin-bottom: 5px;">6. Auto push sending in all types of fee collection.</p>
                                <p style="margin-bottom: 5px;">7. Update complain modules</p>
                                <p style="margin-bottom: 5px;">8. Library Module</p>
                                <p style="margin-bottom: 5px;">9. Transport Module</p>




                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.27&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 27/06/2023</h3>
                                <p style="margin-bottom: 5px;">1. Updated page permissions security</p>
                                <p style="margin-bottom: 5px;">2. Updated updated syllabus</p>
                                <p style="margin-bottom: 5px;">3. New report added of discount.</p>
                                <p style="margin-bottom: 5px;">4. Only admin can collect fee in back date, User can collect fee by current date or today date in all types of fee collection.</p>
                                <p style="margin-bottom: 5px;">5. You can also edit/delete bill by Day End Summary report.</p>
                                <p style="margin-bottom: 5px;">6. Now you can see monthly/overall dues with late fine.</p>
                                <p style="margin-bottom: 5px;">7. Now you can delete bill of General Expense.</p>
                                <p style="margin-bottom: 5px;">8. Other fee head delete issue resolved.</p>
                                <p style="margin-bottom: 5px;">9. Now you can print Monthly fee slip by choosing what copy to print,(Both copy, Office copy or Student copy).</p>
                                <p style="margin-bottom: 5px;">10. Activity module updated.</p>
                                <p style="margin-bottom: 5px;">11. Added multiple attachment option in leave request by student & teacher.</p>
                                <p style="margin-bottom: 5px;">12. All Discount Showing Student Wise</p>
                                <p style="margin-bottom: 5px;">13. Calender update showing details.</p>



                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.26&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 25/05/2023</h3>
                                <p style="margin-bottom: 5px;">1. Update Day End Report</p>
                                <p style="margin-bottom: 5px;">2. Update Session Management   </p>
                                <p style="margin-bottom: 5px;">3. Add Inbox(Events,Message,Notice)   </p>
                                <p style="margin-bottom: 5px;">4. Class Teacher Mapping   </p>
                                <p style="margin-bottom: 5px;">5. Student Attndance View Subject Wise  </p>
                                <p style="margin-bottom: 5px;">6. Student Complaint List  </p>
                                <p style="margin-bottom: 5px;">7. Bulk Student Month Skip  </p>


                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.25&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 12/05/2023</h3>
                                <p style="margin-bottom: 5px;">1. Routine multiple subject mapping</p>
                                <p style="margin-bottom: 5px;">2. Dues report update   </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.24&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 11/05/2023</h3>
                                <p style="margin-bottom: 5px;">1. SMS Module   </p>
                                <p style="margin-bottom: 5px;">2. Graph Update Home Pgae   </p>
                                <p style="margin-bottom: 5px;">3.  General Expense Update   </p>
                                <p style="margin-bottom: 5px;">4.  Day End Summery Update   </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.23&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 10/04/2023</h3>
                                <p style="margin-bottom: 5px;">1. All month option in monthly discount   </p>
                                <p style="margin-bottom: 5px;">2. Skip Month Option  </p>
                                <p style="margin-bottom: 5px;">3. Monthly Dues student send Push </p>
                                <p style="margin-bottom: 5px;">4. Class Activity  </p>
                                <p style="margin-bottom: 5px;">6. Employee hiring Module  </p>
                                <p style="margin-bottom: 5px;">7. Offline Admission Form generate and Print  </p>
                                <p style="margin-bottom: 5px;">8. Privious year dues add in monthly fee & Excel import   </p>
                                <p style="margin-bottom: 5px;">10. Previous year dues calculation </p>
                                <p style="margin-bottom: 5px;">11. Monthly Dues student send Push </p>
                                <p style="margin-bottom: 5px;">12. Student Month Skip   </p>
                                <p style="margin-bottom: 5px;">13. update hoste fee master   </p>
                                <p style="margin-bottom: 5px;">14. New Subject Mapping With Student </p>
                                <p style="margin-bottom: 5px;">15. Complain Module Both School </p>
                                <p style="margin-bottom: 5px;">16. School Tool Development </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.22&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 25/02/2023</h3>
                                <p style="margin-bottom: 5px;">1. Birth Certificate  </p>
                                <p style="margin-bottom: 5px;">2. Income Certificate  </p>
                                <p style="margin-bottom: 5px;">3. Create Admit Card Online Asmission  </p>
                                <p style="margin-bottom: 5px;">4. Print Admit Card  Online Asmission  </p>
                                <p style="margin-bottom: 5px;">5. Print Result Online Asmission  </p>
                                <%--  <p style="margin-bottom: 5px;">6. Student Prmote  </p>--%>


                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.21&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 17/12/2022</h3>
                                <p style="margin-bottom: 5px;">1. Online Registration Module  </p>
                                <p style="margin-bottom: 5px;">2. Add on the website Notice  </p>
                                <p style="margin-bottom: 5px;">3. General Expense  </p>
                                <p style="margin-bottom: 5px;">4. Student Enquiry  </p>
                                <p style="margin-bottom: 5px;">5. Bulk Student Monthly Payment Using CSV  </p>
                                <p style="margin-bottom: 5px;">6. Student Id Card  </p>
                                <p style="margin-bottom: 5px;">7. Financial Year Changing Option  </p>

                                <p style="margin-bottom: 5px;">9. Offline Form Generate Print </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.20&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 03/12/2022</h3>
                                <p style="margin-bottom: 5px;">1. Online Registration Module  </p>
                                <p style="margin-bottom: 5px;">2. Add on the website Notice  </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.19&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 11/10/2022</h3>
                                <p style="margin-bottom: 5px;">1. Set deadline date for marks entry for teachers  </p>
                                <p style="margin-bottom: 5px;">2. App link with google play store in login page </p>
                                <p style="margin-bottom: 5px;">3. Give permission only to admin or exam cell for changing the marks after submission  </p>
                                <p style="margin-bottom: 5px;">4. After submission of online registration and offline form kindly set a view option to preview the full form . </p>
                                <p style="margin-bottom: 5px;">5. Make the term and condition dynamic in online. School will set this according to them</p>
                                <p style="margin-bottom: 5px;">6. Manual attendance for teachers like student </p>
                                <p style="margin-bottom: 5px;">7. Name of father and mother in online registration form with signature upload </p>
                                <p style="margin-bottom: 5px;">8. Integration of online payment in online registration form.  </p>
                                <p style="margin-bottom: 5px;">9. Set Report for online payment  </p>


                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.18&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 02/09/2022</h3>
                                <p style="margin-bottom: 5px;">1. Student Profile </p>
                                <p style="margin-bottom: 5px;">2. Student Payment History </p>
                                <p style="margin-bottom: 5px;">3. student payment-summary </p>
                                <p style="margin-bottom: 5px;">4. Student Class Wise attendance </p>
                                <p style="margin-bottom: 5px;">5. Student class wise bulk attendance</p>



                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.17&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 02/09/2022</h3>
                                <p style="margin-bottom: 5px;">1. Admit Card Create & Print </p>
                                <p style="margin-bottom: 5px;">2. Progress Report Bulk and Single Print </p>
                                <p style="margin-bottom: 5px;">3. Result publishing on APP</p>
                                <p style="margin-bottom: 5px;">4. Class Wise School Calendar </p>
                                <p style="margin-bottom: 5px;">5. Upload Employee Signature </p>
                                <p style="margin-bottom: 5px;">6. Certificate Request Process </p>
                                <p style="margin-bottom: 5px;">7. Student & Teacher Leave Accept & Reject Process </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.16&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 09/08/2022</h3>
                                <p style="margin-bottom: 5px;">1. Bus Allocation </p>

                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.15&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 20/07/2022</h3>
                                <p style="margin-bottom: 5px;">1. Examination Module </p>

                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.14&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 30/06/2022</h3>

                                <p style="margin-bottom: 5px;">1. New Fine System(Date Range Wise) </p>
                                <p style="margin-bottom: 5px;">2. Update Report </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.13&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 14/06/2022</h3>

                                <p style="margin-bottom: 5px;">1. Slip Note </p>

                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.12&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 14/06/2022</h3>

                                <p style="margin-bottom: 5px;">1. Report Update </p>
                                <p style="margin-bottom: 5px;">2. Online Registration </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.11&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 06/06/2022</h3>

                                <p style="margin-bottom: 5px;">1. Report Update </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.10&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 14/05/2022</h3>

                                <p style="margin-bottom: 5px;">1. Updated Report and Add New Report in report Section </p>



                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.10&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 14/05/2022</h3>

                                <p style="margin-bottom: 5px;">1. Updated Report and Add New Report in report Section </p>

                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.9&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 13/05/2022</h3>

                                <p style="margin-bottom: 5px;">1. Delete Student Payment History </p>
                                <p style="margin-bottom: 5px;">2. Deleted Payment Re-Entery  </p>


                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative">School Update Notification Version:-1.0.0.8&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 10/05/2022</h3>

                                <p style="margin-bottom: 5px;">1. Student Payment History(Student List) </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative;">School Update Notification Version:-1.0.0.7&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 07/05/2022</h3>

                                <p style="margin-bottom: 5px;">1. Custom Student List </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative;">School Update Notification Version:-1.0.0.6&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 06/05/2022</h3>

                                <p style="margin-bottom: 5px;">1. Today Birthday Student </p>
                                <p style="margin-bottom: 5px;">2. Birthday Set Message Template </p>
                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative;">School Update Notification Version:-1.0.0.5&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 06/05/2022</h3>
                                <p style="margin-bottom: 5px;">1. Active/Inactive Student</p>

                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative;">School Update Notification Version:-1.0.0.4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 01/05/2022</h3>
                                <p style="margin-bottom: 5px;">1. School Inventory</p>

                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative;">School Update Notification Version:-1.0.0.3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 30/04/2022</h3>
                                <p style="margin-bottom: 5px;">1. Update:- Change Admission No & Class Change</p>


                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative;">School Update Notification Version:-1.0.0.2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 24/04/2022</h3>
                                <p style="margin-bottom: 5px;">1. Monthly Dues List</p>

                                <p style="margin-bottom: 5px;">3. Day Boarding with Lunch</p>

                                <h3 style="font-size: 17px; border-bottom: 1px solid; position: relative;">School Update Notification Version:-1.0.0.1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;Date :- 11/04/2022</h3>
                                <p style="margin-bottom: 5px;">1. Student Routine(Time Table)</p>
                                <p style="margin-bottom: 5px;">2. Teacher Routine(Time Table)</p>



                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>

</asp:Content>
