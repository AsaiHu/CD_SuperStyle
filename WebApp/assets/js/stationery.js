var topBtn = document.getElementsByClassName('toTop')[0];

var _panel = document.getElementsByClassName('_panel-box')[0];

topBtn.addEventListener('click', function () {
    var _bar = document.getElementsByClassName('_scroll_bar')[0];
    _panel.classList.add('speed');
    _bar.classList.add('speed');
    setTimeout(function(){
        _panel.style.top = 0;
    },0);
    setTimeout(function () {
        _panel.classList.remove('speed');
        setTimeout(function () {
            _bar.style.top = 0;
            setTimeout(function(){
                _bar.classList.remove('speed');
            },500);
        }, 0);
    }, 500);
},false);

var cartoons = document.getElementsByClassName('cartoon_center');

// for(var o = 0;o<cartoons.length;o++){
    
// }
_panel.ondrag = function () {
    console.log(1);
    console.log(cartoons[0].offsetTop);
};