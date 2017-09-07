// JavaScript Document
$(document).ready(function () {
	
	
	
    $('#task-cross').click(function (e) {
        $('#task-container').addClass('hide');
    });
    $('#task-minus').click(function (e) {
        $('#task-contents').slideToggle();

    });
    $('#report-cross').click(function (e) {
        $('#report-container').addClass('hide');
    });
    $('#report-minus').click(function (e) {
        $('#report-contents').slideToggle();

    });
    $('#workflow-cross').click(function (e) {
        $('#workflow-container').addClass('hide');
    });
    $('#workflow-minus').click(function (e) {
        $('#workflow-contents').slideToggle();

    });
    $('#message-cross').click(function (e) {
        $('#message-container').addClass('hide');
    });
    $('#message-minus').click(function (e) {
        $('#message-contents').slideToggle();

    });
    $('#s-doc-cross').click(function (e) {
        $('#s-doc-container').addClass('hide');
    });
    $('#s-doc-minus').click(function (e) {
        $('#s-doc-contents').slideToggle();

    });
    $('#frequent-cross').click(function (e) {
        $('#frequent-container').addClass('hide');
    });
    $('#frequent-minus').click(function (e) {
        $('#frequent-contents').slideToggle();

    });
	
	$('#capturing').click(function (e) {
        $('#capturing').removeClass('hide');
        $('#capturing').addClass('show');

        $('#divid2').removeClass('show');
        $('#divid3').removeClass('show');
        $('#divid4').removeClass('show');
        $('#divid5').removeClass('show');
        $('#divid6').removeClass('show');

        $('#divid2').addClass('hide');
        $('#divid3').addClass('hide');
        $('#divid4').addClass('hide');
        $('#divid5').addClass('hide');
        $('#divid6').addClass('hide');

    });

    $('#home').click(function (e) {
        $('#dashboard').removeClass('hide');
        $('#dashboard').addClass('show');

        $('#divid2').removeClass('show');
        $('#divid3').removeClass('show');
        $('#divid4').removeClass('show');
        $('#divid5').removeClass('show');
        $('#divid6').removeClass('show');

        $('#divid2').addClass('hide');
        $('#divid3').addClass('hide');
        $('#divid4').addClass('hide');
        $('#divid5').addClass('hide');
        $('#divid6').addClass('hide');

    });
    $('#divid1').click(function (e) { //alert(1);
        $('#dashboard').removeClass('show');
        $('#dashboard').addClass('hide');
        $('#divid2').removeClass('hide');
        $('#divid2').addClass('show');
        $('#divid4').addClass('class1');
        $('#divid4').removeClass('class0');
        //$('#sidebar').addClass('menu-min');
        // $('#sidebar').removeClass('sidebar-scroll');

    });
    $('#divid2').click(function (e) {
        $('#divid3').removeClass('hide');
        $('#divid3').addClass('show');
        $('#divid3-menu').removeClass('hide');
        $('#divid3-menu').addClass('show');
        $('#divid4').addClass('class2');
        $('#divid4').removeClass('class1');
    });
    $('.showcontent').click(function (e) {
        var display_id = $(this).attr('data-display-divid');
        display_id = parseInt(display_id);
        for (var i = 5; i < 10; i++) {
            if (i == display_id) {
                $('#divid' + i).removeClass('hide');
                $('#divid' + i).addClass('show');
            } else {
                $('#divid' + i).removeClass('show');
                $('#divid' + i).addClass('hide');
            }
        }

        $('#side-content').animate({scrollLeft: $('#side-content').offset().left}, 800);
    });


    $('.fa-chevron-up1').click(function (e) {
        $('#divid6').removeClass('show col-xs-6');
        $('#divid5').css("width", 630);
        $('#divid6').addClass('hide');
        $('#divid5').removeClass('col-xs-6');
        $('#divid5').addClass('col-sm-12');
    });

    $('#menu-toggler01').click(function (e) {


        if ($('#menu-toggler-box').hasClass('active01') && $('#navbar-logo').hasClass('navbar-logo-in')) {
            $('#menu-toggler-box').removeClass('active01');
            $('#menu-toggler-box').css('width', 240);
            $('#navbar-logo').removeClass('navbar-logo-in');

            var width = $(window).width();
            var fcoloumn = 240;

        } else {
            $('#menu-toggler-box').addClass('active01');
            $('#menu-toggler-box').css('width', 60);
            $('#navbar-logo').addClass('navbar-logo-in');

            var width = $(window).width();
            var fcoloumn = 60;
        }

        var total = width - fcoloumn;
        $("#side-content").css("width", total);

    });

    $('#cross').click(function (e) {

        $('#divid5').removeClass('col-xs-6');
        $('#divid4').removeClass('class1');
        $('#divid6').removeClass('show col-xs-6');

        $('#divid2').removeClass('hide');
        $('#divid2').addClass('show');
        $('#divid3').removeClass('show');
        $('#divid3').addClass('hide');
        for (var i = 5; i < 10; i++) {
            $('#divid' + i).removeClass('show');
            $('#divid' + i).addClass('hide');
        }




    });

    function singleClick(e) {
        // do something, "this" will be the DOM element
        $('#divid5').addClass('col-xs-6');
        $('#divid5').css("width", 520);
        $('#divid6').css("left", 985);
        $('#divid4').addClass('class1');
        $('#divid4').removeClass('class2');
        $('#divid6').addClass('show col-xs-6');
        $('#side-content').scrollLeft(1500);
    }

    function doubleClick(e) {
        // do something, "this" will be the DOM element
        window.location = 'Document.html';
    }

    $('.btn-click').click(function (e) {
        var that = this;
        setTimeout(function () {
            var dblclick = parseInt($(that).data('double'), 10);
            if (dblclick > 0) {
                $(that).data('double', dblclick - 1);
            } else {
                singleClick.call(that, e);
            }
        }, 300);
    }).dblclick(function (e) {
        $(this).data('double', 2);
        doubleClick.call(this, e);
    });

    $('.menu1 li').click(function () {
        $(".menu1 li.active").removeClass("active");
        $(this).addClass('active');
    });
    $('#divid2 li').click(function () {
        $("#divid2 li.active").removeClass("active");
        $(this).addClass('active');
    });
    $('#divid3 li').click(function () {
        $("#divid3 li.active").removeClass("active");
        $(this).addClass('active');
    });
    
    //capturing Page
    function checkBoxCapturing(){
        var numberOfChecked  = $('.capturingChk:checked').length;
        if(numberOfChecked >= 2){
               $('#mearge-batch').removeClass('hide');//.addClass('show');
        }else{
            $('#mearge-batch').addClass('hide');  //removeClass('show').
        }
    }
    $('.capturingChk').click(function(e){
        checkBoxCapturing();
    });
    checkBoxCapturing();

});