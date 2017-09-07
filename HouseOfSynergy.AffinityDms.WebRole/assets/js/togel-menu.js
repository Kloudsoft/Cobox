// JavaScript Document
 $(document).ready(function(){
                 // Moving Logo from Logo-Bar to Navbar-header on Tab size of 768px or Minimum
                 $(window).bind("load resize orientationchange",function(e){
                     $('#menu-toggler-box').css('width',240);
                     var width  = $( window ).width();
                     var wheight  = $( window ).height();
                     var fcoloumn = 240;
                     var total = width - fcoloumn;
                     var totalheight = wheight - 102;
                     $("#side-content").css("width", total);
                     $("#side-content,#divid2,#divid3,#divid5,#divid6,#dashboard-content").css("height", totalheight);
                 });

                 $(window).load("load resize orientationchange",function(e){
                     $('#menu-toggler-box').css('width',240);
                     var width  = $( window ).width();
                     var wheight  = $( window ).height();
                     var fcoloumn = 240;
                     var total = width - fcoloumn;
                     var totalheight = wheight - 102;
                     $("#side-content").css("width", total);
                     $("#side-content").css("height", totalheight);
                     $("#divid2,#divid3,#divid5,#divid6,#dashboard-content").css("height", totalheight-25);

                 });



             });
