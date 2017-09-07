

    $(document).ready
	(
		function () {
		    $("Domain").focus().select();
		    if ($("#SigninErrorMessage") != null)
		    {
		        if ($("#SigninErrorMessageText").text() != "")
		        {
		            setTimeout(function () {
		                $('#SigninErrorMessage').fadeOut('fast');
		                $("#SigninErrorMessage").css("display", "none");
		                $("#SigninErrorMessageText").text("");
		            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
		        }
		    }
		    if($("#SigninSuccessMessage")!=null)
		    {
		        if ($("#SigninSuccessMessageText").text() != "") {
		            setTimeout(function () {
		                $('#SigninSuccessMessage').fadeOut('fast');
		                $("#SigninSuccessMessage").css("display", "none");
		                $("#SigninSuccessMessageText").text("");
		            }, AffinityDms.Settings.Constants.PopUpNotificationTimer);
		        }
		    }
		}
       
	);

function Validate() {
    var result = true;
    var validationResult = HouseOfSynergy.Library.Validation.ValidationResult.Empty;

    validationResult = HouseOfSynergy.Library.Validation.ValidationUtilities.ValidateDomain(window.TextBoxDomain.value);
    if (!validationResult.Validated) { result = false; window.SpanErrorDomain.innerText = validationResult.Message; }
    validationResult = HouseOfSynergy.Library.Validation.ValidationUtilities.ValidateUsername(window.TextBoxUsername.value);
    if (!validationResult.Validated) { result = false; window.SpanErrorUsername.innerText = validationResult.Message; }
    validationResult = HouseOfSynergy.Library.Validation.ValidationUtilities.ValidatePassword(window.TextBoxPassword.value);
    if (!validationResult.Validated) { result = false; window.SpanErrorPassword.innerText = validationResult.Message; }

    return (result);
}
