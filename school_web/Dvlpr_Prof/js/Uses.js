function changeHashOnLoad() {
    window.location.href += "#";
    setTimeout("changeHashAgain()", "50");
}

function changeHashAgain() {
    window.location.href += "1";
}

var storedHash = window.location.hash;
window.setInterval(function () {
    if (window.location.hash != storedHash) {
        window.location.hash = storedHash;
    }
}, 50);

(function ($) {

    "use strict";



    $(document).ready(function () {
        setNavigation();
        $('#mob').click(function () {
            $('.app-container').addClass('closed-sidebar-mobile');
            if ($('.app-container').hasClass('sidebar-mobile-open')) {
                $('.app-container').removeClass('sidebar-mobile-open');
                $('#mob').removeClass('is-active');
            } else {
                $('.app-container').addClass('sidebar-mobile-open');
                $('#mob').addClass('is-active');
            }
        });



        $('#example').DataTable({
            dom: 'lBfrtip',
            buttons: [
           {
               extend: 'print',
               text: '<i class="lnr-printer"></i>',
               titleAttr: 'print',
               exportOptions: {
                   columns: ':visible'
               }
           },
           
           {
               extend: 'excelHtml5',
               text: '<i class="lnr-file-add"></i>',
               titleAttr: 'Excel Export',
               exportOptions: {
                   columns: ':visible'
               }
           },

                {
                    extend: 'colvis',
                    text: '<i class="lnr-eye"> </i>',
                    titleAttr: 'colvis'
                }
            ],
           
        });
    });

    function setNavigation() {
        var path = window.location.pathname;
        path = path.replace(/\/$/, "");
        path = decodeURIComponent(path);
        
        $("#MainMenu li a").each(function () {
            var current = location.pathname.split("/Developer_Profile/")[1];
            var href = $(this).attr('href');
            if (current === href) {
                $('.metismenu li.mm-active').removeClass('mm-active');
                $('.metismenu li.mm-active ul').removeClass('mm-show');
                $(this).closest('.metismenu li.mm-active a').attr("aria-expanded", "false");
                $(this).closest('li').addClass('mm-active');
                $(this).closest('.metismenu li ul').addClass('mm-show');
                $(this).closest('.metismenu li a').attr("aria-expanded", "true");
            }

        });
    }


})(jQuery);