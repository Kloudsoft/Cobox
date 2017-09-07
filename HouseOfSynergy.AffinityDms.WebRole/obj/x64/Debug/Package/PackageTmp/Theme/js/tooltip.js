// JavaScript Document

	
	$(document).ready(function () {
       $('[data-toggle="tooltip"]').tooltip({
            placement:"bottom",
            html:"true",
            trigger:"click",
            title:'<ul class="list-type"><li><a href="#"><i class="ace-icon fa fa-cog"></i>01</a></li><li><a href="profile.html"><i class="ace-icon fa fa-user"></i>02</a></li><li><a href="#"><i class="ace-icon fa fa-power-off"></i>03</a></li></ul>',
        }); 		 
        $('[data-toggle="tooltip"]').on('show.bs.tooltip', function () { 
            $('[data-toggle="tooltip"]').not(this).tooltip('hide');
        });

        $("#bjslide").click(function () {
            if ($("#bjslide-expand").is(":hidden")) {
                $("#bjslide-expand").slideDown();
            } else {
                $("#bjslide-expand").slideUp();
            }

        });
});
