
    function Pro_action() {
        this.t2 = document.getElementsByClassName('_scroll_bar')[0];
        this.t3 = document.getElementsByClassName('_panel-box')[0];
        this.scrollBar = document.getElementsByClassName('_scroll_bar')[0];
        this.t4 = 0;
        this.t5 = document.documentElement.clientHeight;

        this.sMouseWheel = "mousewheel";
        if (!("onmousewheel" in document)) { /*浏览器鼠标滚动事件的简单兼容*/
            this.sMouseWheel = "DOMMouseScroll";
        }

        Pro_action.prototype.srollShow = function(action){
            document.addEventListener(this.sMouseWheel, function () {
                try{
                    console.log(action);
                    action();
                }catch(e){
                    console.log("不是有效方法");
                }

            }.bind(this));

            this.scrollBar.onmousedown = function () {
                document.onmousemove = function () {
                    
                    try {
                        action();
                    } catch (e) {
                        console.log("不是有效方法");
                    }
                }.bind(this);
            }.bind(this);

            this.scrollBar.onmouseup = function () {
                document.onmousemove = function () {
                    return false;
                };
            };
        }

    }
    

