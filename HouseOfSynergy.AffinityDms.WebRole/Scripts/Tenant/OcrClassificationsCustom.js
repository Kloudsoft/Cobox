
   
   function getFileInfo(e) {
       return $.map(e.files, function(file) {
           var info = file.name;

           // File size is not available in all browsers
           if (file.size > 0) {
               info  += " (" + Math.ceil(file.size / 1024) + " KB)";
           }
           return info;
       }).join(", ");
   }


 
    
       function PreviewImage(event) {
           xOffset = 100;
           yOffset = 100;

           $("body").append("<p id='preview'><input type='button' class='btn btn-default' style='float:right' value='Close' onclick='javascript:RemovePreview()'><br/><br/><img src='" + event.target.getAttribute('src')+ "' style='width:600px;height:600px;overflow:scroll;' alt='Image preview' /></p>");
           $("#preview")
               .css("margin-top", (10) + "%")//(event.pageY - yOffset) + "px")
               .css("margin-left",(25) + "%") //(event.pageX - xOffset) + "px")
               .fadeIn("fast");
       }
   function RemovePreview() {
       $("#preview").remove();
   }
    
   function DisplayData(id) {
       xOffset = 100;
       yOffset = 100;
       var ocrdata = document.getElementById(id);
       $("body").append("<div id='preview' style='min-width:200px;min-height:200px'><input type='button' class='btn btn-default' style='float:right' value='Close' onclick='javascript:RemovePreview()'><br/><br/><pre style='padding:10px; display: block;width:600px;height:600px;overflow:scroll;background-color:white; color:black'>" +  ocrdata.textContent.toString() + "</pre></div>");
       $("#preview")
           .css("margin-top", (10) + "%")//(event.pageY - yOffset) + "px")
           .css("margin-left",(25) + "%") //(event.pageX - xOffset) + "px")
           .fadeIn("fast");
   }
   function RemoveDisplayData() {
       $("#preview").remove();
   }

