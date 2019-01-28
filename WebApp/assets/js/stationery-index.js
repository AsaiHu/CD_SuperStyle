function Stationery(outBg,changeBackground,changeStyle) {
    var _this = this;
    this.circleFullWidth = 800;//最大圆的直径
    this.rectR = Math.PI * 3 / 200;//线段之间的距离
    this.draw = null;//绘制的圆对象
    this.line_num = 1; //小线段计数
    this.countNum = 0; //计数
    this.rectR2 = Math.PI * 3 / 1000; //小球每次移动的距离
    this.R3 = 340; //小圆所在的直径
    this.singleCircle = null; //小球对象
    this.s_circle = 18; //小圆半径
    this.frequency = 0; //记录小圆移动
    this.cBg = changeBackground;  //放大改变颜色的圆
    this.cStyle = changeStyle; //改变的样式
    this.round = false;
    this.images = document.getElementsByClassName("cartoon");
    
    //绘制最外层大圆
    this.drawCircle = function(){
        console.log('aaa');
        var x1, y1, x2, y2; //线段头尾坐标
        var R1 = 360; //线段所在的起始直径
        var rectRNum = 0; //初始化循环线段
        var R2; //小线段长度

        this.draw = SVG("drawing").size(this.circleFullWidth, this.circleFullWidth);

        for (rectRNum = 0, i = 0; rectRNum <= Math.PI * 3 / 2; rectRNum += this.rectR, i++) {
            
            if (i === 0 || (i + 1) % 20 === 0) {
                R2 = this.circleFullWidth / 2; //长线段直径

            } else {
                R2 = 380; //短线段直径
            } 

            x1 = 400 + R1 * Math.cos(rectRNum) * (-1);
            y1 = 400 + R1 * Math.sin(rectRNum) * (-1);
            x2 = 400 + R2 * Math.cos(rectRNum) * (-1);
            y2 = 400 + R2 * Math.sin(rectRNum) * (-1);
            
            this.draw.line(`${x1},${y1},${x2},${y2}`).stroke({
                color: '#f06',
                width: 1,
                linecap: 'round'
            });
        }

        this.smallBall();
        this.changeLine();
        this.changeBg();
        
    };

    //绘制小圆球
    this.smallBall = function(){
        var circleX = 400 + this.R3 * Math.cos(0) * (-1);
        var circleY = 400 + this.R3 * Math.sin(0) * (-1);

        this.singleCircle = this.draw.circle(this.s_circle).stroke('#f06').fill('#ffffff').move(circleX - this.s_circle / 2, circleY -
            this.s_circle / 2);

    };

    //线段颜色改变
    this.changeLine = function(){
        var changeNum = this.countNum + 1; //6个移动断点
        var lines = document.getElementsByTagName('line'); //所有的小短线
        var lineTotal = lines.length - 1; //所有线段index 排序
        var changeSpeed = this.countNum === 5 ? 50 : 5000/20; //变色时间
        var isRound = false; //判断是否执行了一周

       //处理圆返回
        // if(changeNum === 6){
            
        // }
        var changeColor = setInterval(function(){
            
             if (changeNum === 6) {
                _this.countNum = 0;
                
                lines[lineTotal].setAttribute("stroke", "#f06");
                lineTotal -= 1;
                initBall = true;
                
                if (lineTotal < 0) {
                    lineTotal = lines.length - 1;
                    _this.line_num = 1;
                    isRound = true;
                    _this.countNum = -1;
                    _this.initCircle();
                }
             } else {
                lines[_this.line_num - 1].setAttribute('stroke', '#ffffff');
                _this.line_num += 1;
                

             }

            if (_this.line_num === changeNum * 20 || isRound) {
                clearInterval(changeColor);
                _this.countNum += 1;
                console.log('do');
                //console.log(changeNum);

                //变色后执行小球移动
                _this.moveBall();
            
                _this.changeBg();
                

                //小球移动后继续线段变色
                setTimeout(function () {
                    _this.changeLine();
                    isRound = false;
                    _this.round = false;
                }, 550);
            }
        }, changeSpeed);
    };

    //小球移动
    this.moveBall = function(){
        var circleTimer = null;
        var moveX;
        var moveY;
        circleTimer = setInterval(function(){
            
            moveX = 400 + _this.R3 * Math.cos(_this.frequency) * -1;
            moveY = 400 + _this.R3 * Math.sin(_this.frequency) * -1;
            _this.singleCircle.move(moveX - _this.s_circle / 2, moveY - _this.s_circle / 2);
            _this.frequency += _this.rectR2;

            if (_this.frequency >= (_this.countNum) * ((Math.PI * 3) / 10) - _this.rectR) {
                clearInterval(circleTimer);
            }
        },5);
    
    };
    //小球位置复原
    this.initCircle = function(){
        console.log("滴滴滴");
        var moveX;
        var moveY;
        var vcircleTimerReturn = setInterval(function () {
            _this.frequency -= 2.5 * _this.rectR2;
            moveX = 400 + _this.R3 * Math.cos(_this.frequency) * -1;
            moveY = 400 + _this.R3 * Math.sin(_this.frequency) * -1;
            _this.singleCircle.move(moveX - _this.s_circle / 2, moveY - _this.s_circle / 2);
            if (_this.frequency <= 0) {
                _this.frequency = 0;
               _this.singleCircle.move(moveX - _this.s_circle / 2, moveY - _this.s_circle / 2);
               clearInterval(vcircleTimerReturn);
            }
        }, 5);
    };

    //背景圆变大变色
    this.changeBg = function(){
        if(this.cBg){
            var bgLength = this.cBg.length;
            var circleStyle = this.cStyle.circle;
            
            var currStyle = circleStyle[this.countNum];
            console.log(currStyle);
            console.log('hello: '+this.countNum);
            this.imageIn(this.countNum);
            if( !currStyle ){
                console.log('当前'+this.countNum+'的样式不存在');
                return;
            }
            //给圆添加样式
            var currBg = currStyle.bg; //当前背景色
            var currCircles = currStyle.circles; //当前圆样式数组
            
            for(var o = 0;o<bgLength;o++){
                if ( !currCircles[o] ){
                    console.log(`当前第${(o+1)}个圆的样式不存在`);
                    continue;
                }
                var circle_size = currCircles[o].size;
                var circle_x = currCircles[o].pos[0];
                var circle_y = currCircles[o].pos[1];
                this.cBg[o].style.cssText += `background:${currBg};width:${circle_size}px;height:${circle_size}px;left:${circle_x}px;top:${circle_y}px;`;
                bigger(this.cBg[o]);
                
            }
            //
            function bigger(circle){
                setTimeout(function(){
                    circle.style.cssText += 'transform:scale(8)';
                    // 人物进场
                    
                    setTimeout(function(){
                        circle.style.cssText += 'transform:scale(1)';
                    },850);
                },0);
                
            }
            setTimeout(function(){
                outBg.style.cssText += `background:${currBg}`;
            },850);
            
            console.log(currStyle);
            
        }else{
            console.log('改变颜色大小的圆不存在');
        }
    };

    //人物进场
    this.imageIn = function(num){
        var prevNum = num - 1 < 0 ? prevNum = 5 : prevNum = num -1 ;
        // if(num === 5){
        //     num = 0;
        // }
        console.log(num);
        if (_this.images[num].className.indexOf("showImage") === -1) {
            setTimeout(function(){
                _this.images[num].className += " showImage";
            },100);
        }
        if (_this.images[prevNum].className.indexOf("showImage") !== -1) {
            setTimeout(function () {
                _this.images[prevNum].className = _this.images[prevNum].className.replace(" showImage", "");
            },50);
        }
        console.log('num : '+num);
    };

}

