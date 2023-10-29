//Customize Notification Function   ----Fahad ------
(function ($) {
    showMessageAlert = function (a) {
        var H, T, I;
        if (a.status == null) {

        } else {
            if (a.status === 'SUCCESS') {
                I = 'success';
            }
            else if (a.status === 'FAIL') {
                I = 'error';
            }
            else {
                I = 'warning';
            }
            T = a.message;
            H = a.status;
            'use strict';
            resetToastPosition();


            $.toast({
                heading: H,//'Success',
                text: T,//'And these were just the basic demos! Scroll down to check further details on how to customize the output.',
                showHideTransition: 'slide',
                icon: I,//'success',
                loaderBg: '#f96868',
                position: 'top-right'
            })
            if (a.status === 'FAIL') {
                return 0;
            } else {
                return 1;
            }
        };
        }
       
    



//---------------------------------------------------
    showFailToast = function (a) {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Fail',
            text: a+'.',
            showHideTransition: 'slide',
            icon: 'error',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };



showSuccessToast = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading:'Success',
            text:'And these were just the basic demos! Scroll down to check further details on how to customize the output.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };
  showInfoToast = function() {
    'use strict';
    resetToastPosition();
    $.toast({
      heading: 'Info',
      text: 'And these were just the basic demos! Scroll down to check further details on how to customize the output.',
      showHideTransition: 'slide',
      icon: 'info',
      loaderBg: '#46c35f',
      position: 'top-right'
    })
  };
  showWarningToast = function() {
    'use strict';
    resetToastPosition();
    $.toast({
      heading: 'Warning',
      text: 'And these were just the basic demos! Scroll down to check further details on how to customize the output.',
      showHideTransition: 'slide',
      icon: 'warning',
      loaderBg: '#57c7d4',
      position: 'top-right'
    })
  };
  showDangerToast = function() {
    'use strict';
    resetToastPosition();
    $.toast({
      heading: 'Danger',
      text: 'And these were just the basic demos! Scroll down to check further details on how to customize the output.',
      showHideTransition: 'slide',
      icon: 'error',
      loaderBg: '#f2a654',
      position: 'top-right'
    })
  };
  showToastPosition = function(position) {
    'use strict';
    resetToastPosition();
    $.toast({
      heading: 'Positioning',
      text: 'Specify the custom position object or use one of the predefined ones',
      position: String(position),
      icon: 'info',
      stack: false,
      loaderBg: '#f96868'
    })
  }
  showToastInCustomPosition = function() {
    'use strict';
    resetToastPosition();
    $.toast({
      heading: 'Custom positioning',
      text: 'Specify the custom position object or use one of the predefined ones',
      icon: 'info',
      position: {
        left: 120,
        top: 120
      },
      stack: false,
      loaderBg: '#f96868'
    })
  }
  resetToastPosition = function() {
    $('.jq-toast-wrap').removeClass('bottom-left bottom-right top-left top-right mid-center'); // to remove previous position class
    $(".jq-toast-wrap").css({
      "top": "",
      "left": "",
      "bottom": "",
      "right": ""
    }); //to remove previous position style
  }
})(jQuery);