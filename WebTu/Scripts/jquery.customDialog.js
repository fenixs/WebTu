//var script = document.createElement("script");
//script.type = "text/javascript";
//script.src = "jquery-1.10.2.js";
//document.getElementsByTagName("head")[0].appendChild(script);

(function ($) {
	$.fn.extend({
		/* booleanFactory : {"key":false}*/
		booleanFactory: function (deb) {
			if (typeof deb == "boolean") {
				return { "key": deb };
			}
			return { "key": false };

		},

		/* stringFactory : {"key" : ""}*/
		stringFactory: function (param) {
			if (typeof param == "string") {
				var tmp = $.trim(param);

				return { "key": tmp };
			}
			return { "key": "" }
		},

		/* optionsFactory: {"key" : ""} */
		optionsFactory: function (obj) {
			if (typeof (obj) == "object" && !$.isEmptyObject(obj)) {
				var type = this.typeFactory(obj.type).key;
				var themeColor = this.colorFactory(obj.themeColor).key;
				var dragDrop = this.booleanFactory(obj.dragDrop).key;
				return { "type": type, "themeColor": themeColor, "dragDrop": dragDrop };
			}
			return { "type": this.typeFactory().key, "themeColor": this.colorFactory().key, "dragDrop": this.booleanFactory().key };
		},

		/* typeFactory: {"key" : "alert"} */
		typeFactory: function (type) {
			if (typeof type == "string") {
				var key = $.trim(type);
				switch (key) {
					case "info":
						return { "key": "info" };
					case "confirm":
						return { "key": "confirm" };
					case "alert":
						return { "key": "alert" };
					default:
						return { "key": "alert" };
				}
			}
			return { "key": "alert" };
		},

		/* colorFactory : {"key" : "#FF0000"}*/
		colorFactory: function (color) {
			if (typeof color == "string") {
				var tmp = $.trim(color);
				if (/^\#[0-9a-fA-F]{0}&/.test(tmp)) {
					return { "key": tmp };
				}
			}
			return { "key": "#0088DD" };
		},

		/*设置遮罩层*/
		setMask: function (openMask) {
			var mask = $("<div class='maskLayer' style='width:99%;height:97.5%;background:gray;position:absolute;z-index:0;opacity:0.3;filter:alpha(opacity=30);'></div>");
			if (this.booleanFactory(openMask).key && openMask == true) {
				//为true将mask插入到body的第一个元素上面
				return $(mask).insertBefore($("body").children().first());
			}
			return $(this);
		},

		/*自定义框的初始位置*/
		setOriginSize: function (x, y) {
			$(".drag").css({ "top": x, "left": y });
		},

		/*confirmModal 确认模态组件*/
		confirmModal: function (msg, options, isOpen) {
			var content = this.stringFactory(msg).key,
				obj = this.optionsFactory(options),
				type = obj.type,
				themeColor = obj.themeColor,
				commonPart = $(
				"<div class='drag' style='border:1px " + themeColor + " solid;background:#FFFFFF;cursor:pointer;box-shadow: 10px 10px 5px #888888;text-align:center;width:180px;"
				+ "height:auto;margin:10px; position:absolute;z-index:1;'>"
				+ "<table style='background:" + themeColor + ";color:#FFFFFF;padding:2px 0 2px 5px;width:100%;'>"
				+ "<tr><td style='text-shadow:1px 1px 1px #666'>提示信息</td>"
				+ "<td><span title='关闭' id='close' style='float:right;background:url(\"image/close.png\") no-repeat right top;width:100%;'>&nbsp;&nbsp;&nbsp;</span></td></tr></table>"
				+ "<div style='background-position:left top;width:100%;'>"
				+ "<table style='padding:20px 0 0 0;width:96%;margin:2%;font-size:12px;border:1px solid " + themeColor + ";text-align:center;word-break:break-all;word-wrap:break-word;'>"
				+ "<tr><td style='text-shadow:1px 1px 1px #666;'>" + content + "</td></tr>"
				+ "<tr><td style='color:darkgray;'></td></tr></table></div>"
				);
			if (type == "info") {
				compnet = commonPart;
			}
			if (type == "confirm") {
				compnet = commonPart.append(
					"<div style='padding:0 5px 5px 5px;'>"
					+ "<input type='button' value='确定' id='ok' style='background:" + themeColor + ";color:#FFFFFF;border:1px solid " + themeColor + ";margin:0 5px 0 0;"
					+ "text-shadow:1px 1px 1px #666;line-height:16px;height:25px' />"
					+ "<input type='button' value='取消' id='cancel' style='background:" + themeColor + ";color:#FFFFFF;border:1px solid " + themeColor + ";"
					+ "text-shadow:1px 1px 1px #666;line-height:16px;height:25px' />"
					+ "</div>"
					);
			}
			if (type == "alert") {
				compnet = commonPart.append(
					"<div style='padding:0 5px 5px 5px;'>"
					+ "<input type='button' value='确定' id='ok' style='background:" + themeColor + ";color:#FFFFFF;border:1px solid " + themeColor + ";margin:0 5px 0 0;"
					+ "text-shadow:1px 1px 1px #666;line-height:16px;height:25px' />"
					+ "</div>"
					);
			}
			/* 设置是否可拖动*/
			var isDrag = this.booleanFactory(obj.dragDrop).key;
			if (isDrag) {
				var move = false;		//移动标记
				var _x = 0, _y = 0;
				$(compnet).mousedown(function (e) {
					move = true;
					_x = e.pageX - $(compnet).position().left;
					_y = e.pageY - $(compnet).position().top;
				});
				$(compnet).mousemove(function (e) {
					if (move) {
						var x = e.pageX - _x;
						var y = e.pageY - _y;
						$(compnet).css({ "top": y, "left": x });

						if (window.getSelection) {
							window.getSelection().removeAllRanges();
						} else if (document.selection) {
							document.selection.empty();
						}
					}
				}).mouseup(function () { move = false; });
			}

			$(compnet).css({ "top": "20%", "left": "50%" });
			$(window).resize(function (e) {
				$(compnet).css({ "top": "20%", "left": "50%" });
			});

			var close = compnet.find("span[id='close']");
			close.on("click", function () {
				$("body").children(".maskLayer").first().remove();
				$(compnet).remove();
			});

			compnet.find(":button[id='ok']").on("click", function () {
				$("body").children(".maskLayer").first().remove();
				$(compnet).remove();
			});
			compnet.find(":button[id='cancel']").on("click", function () {
				$("body").children(".maskLayer").first().remove();
				$(compnet).remove();
			});

			if (this.booleanFactory(isOpen).key) {
				$("body").mousemove(function (e) {
					compnet.children("div").find("table tr td:eq(1)").html("X axis:" + e.pageX + " | Y axis:" + e.pageY + "<br />width:" + $(".drag").width() + " | height:" + $(".drag").height());
				});
			}

			if (obj.type == "info") {
				var i = 5;
				var intervalId;
				intervalId = setInterval(function () {
					if (i == 0) {
						$("body").children(".maskLayer").first().remove();
						compnet.remove();
						clearInterval(intervalId);
					}
					compnet.children("div").find("table tr td:eq(1)").html("<font style='color:#FF0000'>" + i + "</font> 秒后自动消失");
					i--;
				}, 1000);
			}
			return $(this).append(compnet).find(".drag");
		}
		
	});
})(jQuery);