// npm package: sweetalert2
// github link: https://github.com/sweetalert2/sweetalert2

$(function() {

    showSwal = function(type) {
    'use strict';
      if (type === 'basic') {
        swal.fire({
          text: 'Ł„ŁŲ±Ł… Ų§ŪŁ¾Ų³ŁŁ… Ł…ŲŖŁ† Ų³Ų§Ų®ŲŖŚÆŪ ŲØŲ§ ŲŖŁŁ„ŪŲÆ Ų³Ų§ŲÆŚÆŪ',
          confirmButtonText: 'ŲØŲ³ŲŖŁ†',
          confirmButtonClass: 'btn btn-danger',
        })
      } else if (type === 'title-and-text') {
        Swal.fire(
          'Ł„ŁŲ±Ł… Ų§ŪŁ¾Ų³ŁŁ…Ų',
          'Ł„ŁŲ±Ł… Ų§ŪŁ¾Ų³ŁŁ… Ł…ŲŖŁ† Ų³Ų§Ų®ŲŖŚÆŪ ŲØŲ§ ŲŖŁŁ„ŪŲÆ Ų³Ų§ŲÆŚÆŪ',
          'question'
        )
      } else if (type === 'title-icon-text-footer') {
        Swal.fire({
          icon: 'error',
          title: 'Ų§ŁŁ¾Ų³...',
          text: 'Ų®Ų·Ų§ŪŪ Ų±Ų® ŲÆŲ§ŲÆŁ‡ Ų§Ų³ŲŖ!',
          footer: '<a href>Ų¢ŪŲ§ Ų³ŁŲ§Ł„Ū ŲÆŲ§Ų±ŪŲÆŲ</a>'
        })
      } else if (type === 'custom-html') {
        Swal.fire({
          title: '<strong><u>Ł…Ų«Ų§Ł„</u> HTML</strong>',
          icon: 'info',
          html: 'Ų´Ł…Ų§ Ł…Ū ŲŖŁŲ§Ł†ŪŲÆ Ų§Ų² <b>Ł…ŲŖŁ† ŲŖŁ Ł¾Ł¸Ų±</b> Ų ' +
            '<a href="//google.com">Ł„ŪŁ†Ś©</a> ' +
            'Ł Ł‡Ų± ŲŖŚÆ HTML ŲÆŪŚÆŲ±Ū Ų§Ų³ŲŖŁŲ§ŲÆŁ‡ Ś©Ł†ŪŲÆ',
          showCloseButton: true,
          showCancelButton: true,
          focusConfirm: false,
          confirmButtonText:
            '<i class="fa fa-thumbs-up"></i> Ų¹Ų§Ł„Ū!',
          confirmButtonAriaLabel: 'Thumbs up, great!',
          cancelButtonText:
            '<i data-feather="thumbs-up"></i>',
          cancelButtonAriaLabel: 'Thumbs down',
        });
        feather.replace();
      } else if (type === 'custom-position') {
        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: 'Ų§Ų·Ł„Ų§Ų¹Ų§ŲŖ Ų´Ł…Ų§ ŲØŲ§ Ł…ŁŁŁ‚ŪŲŖ Ų°Ų®ŪŲ±Ł‡ Ų´ŲÆ',
          showConfirmButton: false,
          timer: 1500
        })
      } else if (type === 'passing-parameter-execute-cancel') {
        const swalWithBootstrapButtons = Swal.mixin({
          customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger me-2'
          },
          buttonsStyling: false,
        })
        
        swalWithBootstrapButtons.fire({
          title: 'Ų¢ŪŲ§ Ł…Ų·Ł…Ų¦Ł† Ł‡Ų³ŲŖŪŲÆŲ',
          text: "ŲØŲ¹ŲÆ Ų§Ų² Ų­Ų°Ł Ų´Ł…Ų§ Ł‚Ų§ŲÆŲ± ŲØŁ‡ ŲØŲ§Ų²ŚÆŲ±ŲÆŲ§Ł†Ū Ų§Ų·Ł„Ų§Ų¹Ų§ŲŖ Ł†Ų®ŁŲ§Ł‡ŪŲÆ ŲØŁŲÆ",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonClass: 'me-2',
          onfirmButtonText: 'ŲØŁ„Ł‡Ų Ų­Ų°Ł Ų´ŁŲÆ!',
          cancelButtonText: 'Ł†Ł‡Ų Ł„ŲŗŁ Ų´ŁŲÆ',
          reverseButtons: true
        }).then((result) => {
          if (result.value) {
            swalWithBootstrapButtons.fire(
              'Ų­Ų°Ł Ų´ŲÆ!',
              'ŁŲ§ŪŁ„ Ł…ŁŲ±ŲÆ Ł†ŲøŲ± Ų´Ł…Ų§ Ų­Ų°Ł Ų´ŲÆ!',
              'success'
            )
          } else if (
            // Read more about handling dismissals
            result.dismiss === Swal.DismissReason.cancel
          ) {
            swalWithBootstrapButtons.fire(
              'Ł„ŲŗŁ Ų´ŲÆ',
              'Ų´Ł…Ų§ Ų­Ų°Ł ŁŲ§ŪŁ„ Ų±Ų§ Ł„ŲŗŁ Ś©Ų±ŲÆŪŲÆ :)',
              'error'
            )
          }
        })
      } else if (type === 'message-with-auto-close') {
        let timerInterval
        Swal.fire({
          title: 'Ł‡Ų´ŲÆŲ§Ų± ŲØŲ§ Ł‚Ų§ŲØŁ„ŪŲŖ ŲØŲ³ŲŖŁ‡ Ų´ŲÆŁ† Ų®ŁŲÆŚ©Ų§Ų±!',
          html: 'Ų§ŪŁ† Ł‡Ų´ŲÆŲ§Ų± ŲØŲ¹ŲÆ Ų§Ų² <b></b> Ł…ŪŁ„Ū Ų«Ų§Ł†ŪŁ‡ ŲØŲ³ŲŖŁ‡ Ł…Ū Ų´ŁŲÆ.',
          timer: 2000,
          timerProgressBar: true,
          didOpen: () => {
            Swal.showLoading()
            timerInterval = setInterval(() => {
              const content = Swal.getHtmlContainer()
              if (content) {
                const b = content.querySelector('b')
                if (b) {
                  b.textContent = Swal.getTimerLeft()
                }
              }
            }, 100)
          },
          willClose: () => {
            clearInterval(timerInterval)
          }
        }).then((result) => {
          /* Read more about handling dismissals below */
          if (result.dismiss === Swal.DismissReason.timer) {
            console.log('I was closed by the timer')
          }
        })
      } else if (type === 'message-with-custom-image') {
        Swal.fire({
          title: 'Ł‡Ų´ŲÆŲ§Ų± Ų´ŪŲ±ŪŁ†!',
          text: 'Ł…ŲÆŲ§Ł„ ŲØŁ‡ Ł‡Ł…Ų±Ų§Ł‡ ŪŚ© Ų¹Ś©Ų³ Ų³ŁŲ§Ų±Ų´Ū',
          // imageUrl: '../../../../../../https@unsplash.it/400/200',
          imageUrl: '/assets/images/photos/img3.jpg',
          imageWidth: 400,
          imageHeight: 200,
          imageAlt: 'ŲŖŲµŁŪŲ± Ų³ŁŲ§Ų±Ų´Ū',
        })
      } else if (type === 'mixin') {
        const Toast = Swal.mixin({
          toast: true,
          position: 'top-start',
          showConfirmButton: false,
          timer: 3000,
          timerProgressBar: true,
        });
        
        Toast.fire({
          icon: 'success',
          title: 'Ų«ŲØŲŖ Ł†Ų§Ł… Ų´Ł…Ų§ ŲØŲ§ Ł…ŁŁŁ‚ŪŲŖ Ų§Ł†Ų¬Ų§Ł… Ų´ŲÆ'
        })
      }
    }
  
  });