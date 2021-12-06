(function($) {
"use strict"; 
    $('[data-toggle="tooltip"]').tooltip();

$('.categories-slider').slick({
  slidesToScroll: 3,
  slidesToShow: 8,
  arrows: true,
  responsive: [
    {
      breakpoint: 768,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 3
      }
    },
    {
      breakpoint: 480,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 3
      }
    }
  ]
});

$('.promo-slider').slick({
  slidesToShow: 3,
  arrows: true,
  dots: true,
  infinite: true,
  responsive: [
    {
      breakpoint: 768,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 1
      }
    },
    {
      breakpoint: 480,
      settings: {
        arrows: false,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 1
      }
    }
  ]
});

$('.osahan-slider').slick({
  centerMode: false,
  slidesToShow: 1,
  arrows: false,
  dots: true
});

$('.recommend-slider2').slick({
  infinite: true,
  speed: 300,
  slidesToShow: 1,
  adaptiveHeight: true,
  centerMode: true,
  arrows: false,
  dots: true,
  autoplay: true
  
});

$('.recommend-slider').slick({
  infinite: true,
  speed: 300,
  slidesToShow: 1,
  adaptiveHeight: true,
  arrows: false,
  dots: true,
  autoplay: true
});

$('.trending-slider').slick({
  centerPadding: '30px',
  slidesToShow: 4,
  arrows: true,
  autoplay: true,
  responsive: [
    {
      breakpoint: 768,
      settings: {
        arrows: true,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 1
      }
    },
    {
      breakpoint: 480,
      settings: {
        arrows: true,
        centerMode: true,
        centerPadding: '40px',
        slidesToShow: 1
      }
    }
  ]
});

var $main_nav = $('#main-nav');
  var $toggle = $('.toggle');

  var defaultOptions = {
      disableAt: false,
      customToggle: $toggle,
      levelSpacing: 40,
      navTitle: 'Self-Chef',
      levelTitles: true,
      levelTitleAsBack: true,
      pushContent: '#container',
      insertClose: 2
  };

var Nav = $main_nav.hcOffcanvasNav(defaultOptions);  

})(jQuery);

jQuery(document).ready(function(){
  // This button will increment the value
  $('.qtyplus').click(function(e){
      // Stop acting like a button
      e.preventDefault();
      // Get the field name
      fieldName = $(this).attr('field');
      // Get its current value
      var currentVal = parseInt($('input[name='+fieldName+']').val());
      // If is not undefined
      if (!isNaN(currentVal)) {
          // Increment
          $('input[name='+fieldName+']').val(currentVal + 1);
      } else {
          // Otherwise put a 0 there
          $('input[name='+fieldName+']').val(1);
      }
  });
  // This button will decrement the value till 0
  $(".qtyminus").click(function(e) {
      // Stop acting like a button
      e.preventDefault();
      // Get the field name
      fieldName = $(this).attr('field');
      // Get its current value
      var currentVal = parseInt($('input[name='+fieldName+']').val());
      // If it isn't undefined or its greater than 0
      if (!isNaN(currentVal) && currentVal > 1) {
          // Decrement one
          $('input[name='+fieldName+']').val(currentVal - 1);
      } else {
          // Otherwise put a 0 there
          $('input[name='+fieldName+']').val(1);
      }
  });
});