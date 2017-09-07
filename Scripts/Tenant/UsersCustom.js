

    function RoleChange(event) {
        var chkbox = event.currentTarget;
        if (chkbox.checked) {
            chkbox.setAttribute("checked", "checked");
            chkbox.setAttribute("value", "true");
        }
        else {
            chkbox.removeAttribute("checked");
            chkbox.setAttribute("value", "false");
        }
    }
