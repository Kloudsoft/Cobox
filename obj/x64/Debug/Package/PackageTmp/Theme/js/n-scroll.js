// JavaScript Document

	 $(document).ready(function(){

               $('#divid2,#divid3,#divid5,#divid6,#dashboard-content').niceScroll({touchbehavior:false,cursorwidth:7,cursorcolor:"#003044",cursorborder:"0px solid #666666",cursorborderradius:"0px",autohidemode:false,boxzoom:false,right:"0px",zindex :502});
		 

     $("#side-content,#divid2,#divid3,#divid5,#divid6,#dashboard-content").scroll(function(){

                     $("#side-content,#divid2,#divid3,#divid5,#divid6,#dashboard-content").getNiceScroll().resize();
                 });
             });