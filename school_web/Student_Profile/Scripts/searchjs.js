
$(function () {
    $("#ContentPlaceHolder1_txt_school").autocomplete({
        source: function (request, response) {
            //var firstname = $("#ContentPlaceHolder1_hideemail%>").val();
            //var std_country = $("#ContentPlaceHolder1_txt_std_country").text();
            //var std_state = $("ContentPlaceHolder1_ddl_std_state").text();
            //var std_district = $("#ContentPlaceHolder1_txt_std_district").text();
            alert(request.term);

            $.ajax({
                url: "Edit_profile.aspx/search_school_college",
                data: "{'sertext':'" + request.term + "'}",
              //  data: "{'sertext':'" + request.term + "','username':'" + firstname + "','country':'" + std_country + "','state':'" + std_state + "','district':'" + std_district + "'}",
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























//document.writeln('<script src="http://code.jquery.com/jquery-1.9.1.js"></script>');
//document.writeln('<script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>');
$(function () {
    $("#ContentPlaceHolder1_TextBox1").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "ssssssssssssssss.aspx/search_school_college",
                //url: "/Myaccount/searching.asmx/search_school_college",
                data: "{'sertext':'" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = response.d;

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








