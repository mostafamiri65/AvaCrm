// npm package: bootstrap-maxlength
// github link: https://github.com/mimo84/bootstrap-maxlength

$(function() {
    'use strict';
  
    $('#defaultconfig').maxlength({
      warningClass: "badge mt-1 bg-success",
      limitReachedClass: "badge mt-1 bg-danger"
    });
  
    $('#defaultconfig-2').maxlength({
      alwaysShow: true,
      threshold: 20,
      warningClass: "badge mt-1 bg-success",
      limitReachedClass: "badge mt-1 bg-danger"
    });
  
    $('#defaultconfig-3').maxlength({
      alwaysShow: true,
      threshold: 10,
      warningClass: "badge mt-1 bg-success",
      limitReachedClass: "badge mt-1 bg-danger",
      separator: ' Ų§Ų² ',
      preText: 'Ų´Ł…Ų§ ',
      postText: ' Ś©Ų§Ų±Ų§Ś©ŲŖŲ± Ų±Ų§ Ų§Ų³ŲŖŁŲ§ŲÆŁ‡ Ś©Ų±ŲÆŁ‡ Ų§ŪŲÆ',
      validate: true
    });
  
    $('#maxlength-textarea').maxlength({
      alwaysShow: true,
      warningClass: "badge mt-1 bg-success",
      limitReachedClass: "badge mt-1 bg-danger"
    });
  });