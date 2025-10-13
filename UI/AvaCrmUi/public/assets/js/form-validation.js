// npm package: jquery-validation
// github link: https://github.com/jquery-validation/jquery-validation

$(function() {
    'use strict';
  
    $.validator.setDefaults({
      submitHandler: function() {
        alert("Ų§Ų±Ų³Ų§Ł„ Ų´ŲÆ!");
      }
    });
    $(function() {
      // validate signup form on keyup and submit
      $("#signupForm").validate({
        rules: {
          name: {
            required: true,
            minlength: 3
          },
          email: {
            required: true,
            email: true
          },
          age_select: {
            required: true
          },
          gender_radio: {
            required: true
          },
          skill_check: {
            required: true
          },
          password: {
            required: true,
            minlength: 5
          },
          confirm_password: {
            required: true,
            minlength: 5,
            equalTo: "#password"
          },
          terms_agree: "required"
        },
        messages: {
          name: {
            required: "Ł„Ų·ŁŲ§ Ł†Ų§Ł… Ų®ŁŲÆ Ų±Ų§ ŁŲ§Ų±ŲÆ Ś©Ł†ŪŲÆ",
            minlength: "Ł†Ų§Ł… Ł†ŲØŲ§ŪŲÆ Ś©Ł…ŲŖŲ± Ų§Ų² 3 Ś©Ų§Ų±Ų§Ś©ŲŖŲ± ŲØŲ§Ų´ŲÆ"
          },
          email: "Ł„Ų·ŁŲ§ ŪŚ© Ų¢ŲÆŲ±Ų³ Ų§ŪŁ…ŪŁ„ Ł…Ų¹ŲŖŲØŲ± ŁŲ§Ų±ŲÆ Ś©Ł†ŪŲÆ",
          age_select: "Ł„Ų·ŁŲ§ Ų³Ł† Ų®ŁŲÆ Ų±Ų§ Ų§Ł†ŲŖŲ®Ų§ŲØ Ś©Ł†ŪŲÆ",
          skill_check: "Ł„Ų·ŁŲ§ Ł…Ł‡Ų§Ų±ŲŖ Ų®ŁŲÆ Ų±Ų§ Ų§Ł†ŲŖŲ®Ų§ŲØ Ś©Ł†ŪŲÆ",
          gender_radio: "Ł„Ų·ŁŲ§ Ų¬Ł†Ų³ŪŲŖ Ų®ŁŲÆ Ų±Ų§ Ų§Ł†ŲŖŲ®Ų§ŲØ Ś©Ł†ŪŲÆ",
          password: {
            required: "Ł„Ų·ŁŲ§ Ų±Ł…Ų² Ų¹ŲØŁŲ± Ų®ŁŲÆ Ų±Ų§ ŁŲ§Ų±ŲÆ Ś©Ł†ŪŲÆ",
            minlength: "Ų·ŁŁ„ Ų±Ł…Ų² Ų¹ŲØŁŲ± Ł†ŲØŲ§ŪŲÆ Ś©Ł…ŲŖŲ± Ų§Ų² 5 Ś©Ų§Ų±Ų§Ś©ŲŖŲ± ŲØŲ§Ų´ŲÆ"
          },
          confirm_password: {
            required: "Ł„Ų·ŁŲ§ Ų±Ł…Ų² Ų¹ŲØŁŲ± Ų®ŁŲÆ Ų±Ų§ ŁŲ§Ų±ŲÆ Ś©Ł†ŪŲÆ",
            minlength: "Ų·ŁŁ„ Ų±Ł…Ų² Ų¹ŲØŁŲ± Ł†ŲØŲ§ŪŲÆ Ś©Ł…ŲŖŲ± Ų§Ų² 5 Ś©Ų§Ų±Ų§Ś©ŲŖŲ± ŲØŲ§Ų´ŲÆ",
            equalTo: "Ł„Ų·ŁŲ§ ŲŖŚ©Ų±Ų§Ų± Ų±Ł…Ų² Ų¹ŲØŁŲ± Ų±Ų§ ŲµŲ­ŪŲ­ ŁŲ§Ų±ŲÆ Ś©Ł†ŪŲÆ"
          },
          terms_agree: "Ł„Ų·ŁŲ§ ŲØŲ§ Ł‚ŁŲ§Ł†ŪŁ† Ł Ł…Ł‚Ų±Ų±Ų§ŲŖ Ł…ŁŲ§ŁŁ‚ŲŖ Ś©Ł†ŪŲÆ"
        },
        errorPlacement: function(error, element) {
          error.addClass( "invalid-feedback" );
  
          if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
          }
          else if (element.prop('type') === 'radio' && element.parent('.radio-inline').length) {
            error.insertAfter(element.parent().parent());
          }
          else if (element.prop('type') === 'checkbox' || element.prop('type') === 'radio') {
            error.appendTo(element.parent().parent());
          }
          else {
            error.insertAfter(element);
          }
        },
        highlight: function(element, errorClass) {
          if ($(element).prop('type') != 'checkbox' && $(element).prop('type') != 'radio') {
            $( element ).addClass( "is-invalid" ).removeClass( "is-valid" );
          }
        },
        unhighlight: function(element, errorClass) {
          if ($(element).prop('type') != 'checkbox' && $(element).prop('type') != 'radio') {
            $( element ).addClass( "is-valid" ).removeClass( "is-invalid" );
          }
        }
      });
    });
  });