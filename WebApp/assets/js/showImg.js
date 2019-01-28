window.onload = function(){
    var t1 = document.getElementsByClassName('cartoon_center');
    var t2 = document.getElementsByClassName('_scroll_bar')[0];
    var t3 = document.getElementsByClassName('_panel-box')[0];
    var t4 = 0;
    var t5 = document.documentElement.clientHeight;
    
    var sMouseWheel = "mousewheel";
    if (!("onmousewheel" in document)) { /*浏览器鼠标滚动事件的简单兼容*/
        sMouseWheel = "DOMMouseScroll";
    }

    document.addEventListener(sMouseWheel, function () {
        t4 = t3.offsetTop;
        for(var oo = 0; oo < t1.length; oo++){
            if(t1[oo].offsetTop + t4 <= t5 -200){
                if (!t1[oo].getAttribute('isLoad')) {
                    t1[oo].setAttribute('isLoad', true);
                    loadLogo(t1[oo]);
                    loads(t1[oo]);
                    loadBanner(t1[oo]);
                }
                
            }
        }
    });

    var scrollBar = document.getElementsByClassName('_scroll_bar')[0];
    scrollBar.onmousedown = function () {
        document.onmousemove = function () {
            t4 = t3.offsetTop;
            for (var oo = 0; oo < t1.length; oo++) {
                if (t1[oo].offsetTop + t4 <= t5 - 200) {
                    if (!t1[oo].getAttribute('isLoad')) {
                        t1[oo].setAttribute('isLoad', true);
                        loadLogo(t1[oo]);
                        loads(t1[oo]);
                        loadBanner(t1[oo]);
                    }

                }
            }

        }
    }

    scrollBar.onmouseup = function () {
        document.onmousemove = function () {
            return false;
        };
    };





    function movePer(lists,cur) {//移动小图
        setTimeout(function(){
            lists[cur].style.cssText += 'transition: all 0.8s ease;opacity:1;left:0;';
        },(cur+2) *500);
    }
    function loads(total) {
        var a = total.getElementsByClassName('per_img');
        var aLength = a.length;
        var c = 0;

        function imgL() {
            a[c].src = a[c].getAttribute('data-src');

            a[c].onload = function () {
                c += 1;
                if (c === aLength) {
                    for(var p = 0;p<c;p++){
                         movePer(a,p);
                    }
                } else {
                    imgL();
                }
            };
        }
        imgL();
    }
    
    function loadLogo(imgLogo){
        var logos = imgLogo.getElementsByClassName('per_logo')[0];
        logos.src = logos.getAttribute('data-src');
        logos.onload = function(){
            var _this = this;
            setTimeout(function(){
                _this.style.cssText += 'transition:all 0.5s ease;opacity:1;left:0;';

            },200);
        };
    }
    function loadBanner(imgBanner) {
        var banner = imgBanner.getElementsByClassName('bigger_banner')[0];
        banner.src = banner.getAttribute('data-src');
        banner.onload = function () {
            var _this = this;
            setTimeout(function () {
                _this.style.cssText += 'transition:all 0.8s ease;opacity:1;';
            }, 500);
        };
    }
};



