<%@ Page Title="" Language="C#" MasterPageFile="~/_adminETutorProf/webview/Site1.Master" AutoEventWireup="true" CodeBehind="student-image-update.aspx.cs" Inherits="school_web._adminETutorProf.webview.student_image_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Update Image
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link href="../../assets/css/responsiveDatatable.css" rel="stylesheet" />
    <style>
        .stdSlctbtn {
            margin: 0px;
            padding: 5px 2px 6px;
            width: 54px;
            float: left;
            background: #f3d500;
            color: #000000;
            line-height: 15px;
            border-radius: 3px;
            text-align: center;
            font-weight: 600;
            font-size: 13px;
            border: 1px solid #726f6f;
            box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            text-align: left;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 5px 5px 5px 5px !important;
        }

        .notificationpan {
            bottom: 5px !important;
            top: auto !important;
        }

        .save-images {
            margin: 0px;
            width: auto;
            float: left;
            font-size: 14px !important;
            letter-spacing: 0px !important;
        }


        .button-61 {
            align-items: center;
            appearance: none;
            border-radius: 4px;
            border-style: none;
            box-shadow: rgba(0, 0, 0, .2) 0 3px 1px -2px,rgba(0, 0, 0, .14) 0 2px 2px 0,rgba(0, 0, 0, .12) 0 1px 5px 0;
            box-sizing: border-box;
            color: #fff;
            cursor: pointer;
            display: inline-flex;
            font-weight: 500;
            height: 36px;
            justify-content: center;
            letter-spacing: .0892857em;
            line-height: normal;
            min-width: 64px;
            outline: none;
            overflow: visible;
            padding: 0 16px;
            position: relative;
            text-align: center;
            text-decoration: none;
            text-transform: uppercase;
            transition: box-shadow 280ms cubic-bezier(.4, 0, .2, 1);
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
            vertical-align: middle;
            will-change: transform,opacity;
        }

            .button-61:hover {
                box-shadow: rgba(0, 0, 0, .2) 0 2px 4px -1px, rgba(0, 0, 0, .14) 0 4px 5px 0, rgba(0, 0, 0, .12) 0 1px 10px 0;
            }

            .button-61:disabled {
                background-color: rgba(0, 0, 0, .12);
                box-shadow: rgba(0, 0, 0, .2) 0 0 0 0, rgba(0, 0, 0, .14) 0 0 0 0, rgba(0, 0, 0, .12) 0 0 0 0;
                color: rgba(0, 0, 0, .37);
                cursor: default;
                pointer-events: none;
            }

            .button-61:not(:disabled) {
                background-color: #6200ee;
            }

            .button-61:focus {
                box-shadow: rgba(0, 0, 0, .2) 0 2px 4px -1px, rgba(0, 0, 0, .14) 0 4px 5px 0, rgba(0, 0, 0, .12) 0 1px 10px 0;
            }

            .button-61:active {
                box-shadow: rgba(0, 0, 0, .2) 0 5px 5px -3px, rgba(0, 0, 0, .14) 0 8px 10px 1px, rgba(0, 0, 0, .12) 0 3px 14px 2px;
                background: #A46BF5;
            }
    </style>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/cropperjs/1.5.13/cropper.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function openStdImgs() {
            $('#mdlStdImg').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="fullinfo">
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Class</p>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </p>
        </div>
        <div class="clearfix"></div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Section</p>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </p>
        </div>
        <div class="clearfix"></div>
        <div class="clearfix"></div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;"></div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
            <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_find_Click" />
        </div>

        <div class="clearfix"></div>
        <div class="texbox-border" style="padding: 0px 5px; overflow: auto">
            <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Action</th>
                        <th>Adm. No.</th>
                        <th>Roll No.</th>
                        <th>Student's Name</th>
                        <th>Father's Name</th>
                        <th>Image</th>
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
                                    <asp:LinkButton ID="lnkEdit" class="stdSlctbtn" runat="server" CausesValidation="false" OnClick="lnkEdit_Click"><span>Update</span></asp:LinkButton>
                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_session" runat="server" Visible="false" Text='<%#Bind("session")%>'></asp:Label>
                                    <asp:Label ID="lbl_img_url" runat="server" Visible="false" Text='<%#Bind("studentimagepath")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_adm_no" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_roll" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_names" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                </td>
                                <td>

                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Bind("studentimagepath") %>' Style="height: 60px; width: 60px; margin: 0px; border: 2px solid #f93; padding: 2px" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>


    <div id="mdlStdImg" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 10px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px; width: 75%; float: left;">Update Image</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202; float: right"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body" style="padding: 5px 5px;">
                    <div class="p-4 border rounded" style="width: 100%; padding: 5px 5px !important;">
                        <div class="disc-tbl-wprs">
                            <div style="width: 100%; overflow: auto;">

                                <table class="table table-striped table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Adm. No.</th>
                                            <th>Roll No.</th>
                                            <th>Student's Name</th>
                                            <th>Image</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_adm_no_p" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_roll_p" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_lame_p" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Image ID="img_std_img_p" runat="server" Style="height: 60px; width: 60px; margin: 0px; border: 2px solid #f93; padding: 2px" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>


                                <p style="font-size: 15px; font-weight: 500; margin: 0px 0px 3px 0px;">
                                    Choose Image
                                </p>
                                <input type="file" id="imageInput" accept="image/*" class="form-control" />
                                <br />
                                <div style="width: 70%; margin: 0px auto; position: relative">
                                    <img id="imagePreview" style="max-width: 100%; display: none;" />
                                </div>
                                <br />
                                <button type="button" id="cropButton" class="save-images button-61">Crop & Upload</button>

                                <asp:Label ID="lblResult" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hd_teacher_id" runat="server" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/cropperjs/1.5.13/cropper.min.js"></script>
    <%--<script>
        let cropper;

        document.getElementById('imageInput').addEventListener('change', function (e) {
            const file = e.target.files[0];
            if (file) {
                const img = document.getElementById('imagePreview');
                img.style.display = 'block';
                img.src = URL.createObjectURL(file);

                img.onload = () => {
                    if (cropper) cropper.destroy();
                    cropper = new Cropper(img, {
                        aspectRatio: 1,
                        viewMode: 1
                    });
                };
            }
        });

        document.getElementById('cropButton').addEventListener('click', function () {
            const canvas = cropper.getCroppedCanvas();

            canvas.toBlob(function (blob) {
                const formData = new FormData();
                formData.append('croppedImage', blob, 'cropped.jpg');

                fetch('student-image-update.aspx', {
                    method: 'POST',
                    body: formData
                })
                    .then(res => res.text())
                    .then(msg => alert(msg))
                    .catch(err => console.error('Upload failed', err));
            });
        });
    </script>--%>

    <script>
        let cropper;
        document.getElementById('imageInput').addEventListener('change', function (e) {
            const file = e.target.files[0];
            if (file) {
                const img = document.getElementById('imagePreview');
                img.style.display = 'block';
                img.src = URL.createObjectURL(file);

                img.onload = () => {
                    if (cropper) cropper.destroy();
                    cropper = new Cropper(img, {
                        aspectRatio: 3 / 3.6, // Portrait shape (taller than wide)
                        viewMode: 1,
                        autoCropArea: 1,
                        movable: true,
                        zoomable: true,
                        scalable: true,
                        cropBoxResizable: true,
                    });
                };
            }
        });

        document.getElementById('cropButton').addEventListener('click', function () {
            const MAX_WIDTH = 500;
            const MAX_HEIGHT = 500;

            const originalCanvas = cropper.getCroppedCanvas();

            // Resize while keeping quality
            const tmpCanvas = document.createElement('canvas');
            let width = originalCanvas.width;
            let height = originalCanvas.height;

            if (width > MAX_WIDTH || height > MAX_HEIGHT) {
                if (width > height) {
                    height *= MAX_WIDTH / width;
                    width = MAX_WIDTH;
                } else {
                    width *= MAX_HEIGHT / height;
                    height = MAX_HEIGHT;
                }
            }

            tmpCanvas.width = width;
            tmpCanvas.height = height;

            const ctx = tmpCanvas.getContext('2d');
            ctx.imageSmoothingEnabled = true;
            ctx.imageSmoothingQuality = "high";
            ctx.drawImage(originalCanvas, 0, 0, width, height);

            // Upload at full quality (no compression loss)
            tmpCanvas.toBlob(function (blob) {
                const formData = new FormData();
                formData.append('croppedImage', blob, 'resized.jpg');

                fetch('student-image-update.aspx', {
                    method: 'POST',
                    body: formData
                })
                    .then(res => res.text())
                    .then(msg => {
                        console.log(msg);
                        //const regId = document.getElementById('hd_teacher_id').value;
                        const regId = $("#<%=hd_teacher_id.ClientID%>").val();
                        window.location.href = "student-image-update.aspx?regid=" + encodeURIComponent(regId);
                    })
                    .catch(err => console.error('Upload failed', err));
            }, 'image/jpeg', 1.0); // 100% quality
        });
    </script>

</asp:Content>
