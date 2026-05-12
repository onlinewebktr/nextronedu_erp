<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/webview/Site1.Master" AutoEventWireup="true" CodeBehind="coupon-list.aspx.cs" Inherits="school_web.Student_Profile.webview.coupon_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .scratchCrd {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .scratch-card {
            position: relative;
            width: 300px;
            /*height: 150px;*/
            margin: 0 auto;
        }

        canvas {
            border: 2px solid #ccc;
            border-radius: 8px;
        }

        .coupon-code {
            display: none;
            font-size: 21px;
            font-weight: bold;
            color: #333;
            margin-top: 10px;
            position: absolute;
            top: 40%;
            width: 100%;
            z-index: -1;
            text-align: center;
        }

        h1 {
            margin-bottom: 20px;
        }

        .couponList {
            margin: 0px;
            padding: 10px 10px;
            width: 100%;
            float: left;
        }

        .couponList-inr {
            margin: 0px;
            padding: 27px 0px 0px 0px;
            width: 100%;
            height: 150px;
            float: left;
            text-align: center;
            background: #f3ef74;
            border-radius: 10px;
            border: 1px solid #d9d201;
        }

        .couponList-inr-couponcd-p {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 15px;
            color: #5e5e5e;
        }

        .couponList-inr-txt-p {
            margin: 10px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 17px;
            color: #000000;
        }

        .couponList-inr-date-p {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 14px;
            color: #303030;
        }

        .couponList-inr-txt-h2 {
            margin: 10px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            text-align: center;
            font-size: 25px;
        }

        .prev-couponList-inr-txt-h2 {
            margin: 20px 0px 0px 0px;
            padding: 0px 10px;
            width: 100%;
            float: left;
            text-align: center;
            font-size: 23px;
        }

        .prev-couponLno-coupon {
            margin: 20px 0px 0px 0px;
            padding: 15px 10px;
            width: 100%;
            float: left;
            text-align: center;
            font-size: 23px;
            background: #e1ef12;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnl_new_coupon" runat="server" Visible="false">
        <div class="scratchCrd">
            <h1 class="couponList-inr-txt-h2">Scratch to Reveal Your Coupon</h1>
            <asp:Repeater ID="RPDetails" runat="server">
                <ItemTemplate>
                    <div class="scratch-card">
                        <canvas id="scratchCanvas" width="300" height="150"></canvas>
                        <div class="coupon-code" id="coupon-code">
                            <p>Your Coupon Discount Amount is : <span><%#Eval("Amount") %></span></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>


    <asp:Panel ID="pnl_prev_coupont_list" runat="server" Visible="false">
        <h2 class="prev-couponList-inr-txt-h2">Coupon List</h2>
        <asp:Repeater ID="rp_prev_coupon_list" runat="server">
            <ItemTemplate>
                <div class="couponList">
                    <div class="couponList-inr">
                        <p class="couponList-inr-couponcd-p">Coupon Code : <%#Eval("Coupon_id") %></p>
                        <p class="couponList-inr-txt-p">Your Coupon Discount Amount is : <%#Eval("Amount") %></p>
                        <p class="couponList-inr-date-p">Coupon Date : <%#Eval("Updated_date") %></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>


    <asp:Panel ID="pnl_no_data" runat="server" Visible="false" Style="padding: 15px 10px; float: left; width: 100%;">
        <h2 class="prev-couponLno-coupon">There are no coupons available at the moment.</h2>
    </asp:Panel>


    <script>
        const canvas = document.getElementById("scratchCanvas");
        const ctx = canvas.getContext("2d");
        const couponCode = document.getElementById("coupon-code");

        let isDrawing = false;
        let scratchArea = 0;

        ctx.fillStyle = "#af2224";
        ctx.fillRect(0, 0, canvas.width, canvas.height);

        ctx.fillStyle = "#f0f0f0";
        ctx.font = "24px Arial";
        ctx.textAlign = "center";
        ctx.textBaseline = "middle";
        ctx.fillText("Scratch to reveal", canvas.width / 2, canvas.height / 2);

        function getPosition(e) {
            const rect = canvas.getBoundingClientRect();
            let x, y;
            if (e.touches && e.touches.length > 0) {
                x = e.touches[0].clientX - rect.left;
                y = e.touches[0].clientY - rect.top;
            } else {
                x = e.clientX - rect.left;
                y = e.clientY - rect.top;
            }
            return { x, y };
        }

        function scratch(x, y) {
            ctx.globalCompositeOperation = "destination-out";
            ctx.beginPath();
            ctx.arc(x, y, 15, 0, Math.PI * 2, false);
            ctx.fill();

            scratchArea += Math.PI * 15 * 15;
            if (scratchArea > 0.3 * canvas.width * canvas.height) {
                revealCoupon();
            }
        }

        function revealCoupon() {
            couponCode.style.display = "block";
        }

        // Mouse events
        canvas.addEventListener("mousedown", (e) => {
            isDrawing = true;
            const { x, y } = getPosition(e);
            scratch(x, y);
        });
        canvas.addEventListener("mouseup", () => isDrawing = false);
        canvas.addEventListener("mousemove", (e) => {
            if (isDrawing) {
                const { x, y } = getPosition(e);
                scratch(x, y);
            }
        });

        // Touch events
        canvas.addEventListener("touchstart", (e) => {
            e.preventDefault();
            isDrawing = true;
            const { x, y } = getPosition(e);
            scratch(x, y);
        });
        canvas.addEventListener("touchend", () => isDrawing = false);
        canvas.addEventListener("touchmove", (e) => {
            e.preventDefault();
            if (isDrawing) {
                const { x, y } = getPosition(e);
                scratch(x, y);
            }
        });
    </script>

    <%--<script type="text/javascript">
        const canvas = document.getElementById("scratchCanvas");
        const ctx = canvas.getContext("2d");
        const couponCode = document.getElementById("coupon-code");
        let isMouseDown = false;
        let scratchArea = 0;

        // Draw the scratch-off layer (gray area)
        ctx.fillStyle = "#af2224"; // Gray color
        ctx.fillRect(0, 0, canvas.width, canvas.height);

        // Draw the text underneath the scratch-off layer
        ctx.fillStyle = "#f0f0f0"; // Light text color
        ctx.font = "24px Arial";
        ctx.textAlign = "center";
        ctx.textBaseline = "middle";
        ctx.fillText("Scratch to reveal", canvas.width / 2, canvas.height / 2);

        // Function to handle scratch effect
        const scratchEffect = (e) => {
            if (!isMouseDown) return;

            const rect = canvas.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            ctx.globalCompositeOperation = "destination-out"; // Erase part of the layer
            ctx.beginPath();
            ctx.arc(x, y, 15, 0, Math.PI * 2, false); // Draw a circle to simulate scratching
            ctx.fill();

            // Update the scratch area
            scratchArea += Math.PI * 15 * 15; // Area of the scratched circle (rough estimate)

            // Reveal the coupon if enough area is scratched
            if (scratchArea > 0.3 * canvas.width * canvas.height) {
                revealCoupon();
            }
        };

        // Reveal the coupon code
        const revealCoupon = () => {
            couponCode.style.display = "block"; // Show the coupon code
        };

        // Mouse events to detect when to start/stop scratching
        canvas.addEventListener("mousedown", () => {
            isMouseDown = true;
        });
        canvas.addEventListener("mouseup", () => {
            isMouseDown = false;
        });
        canvas.addEventListener("mousemove", scratchEffect);
    </script>--%>
</asp:Content>
