Type.registerNamespace("Telerik.Web.UI");
(function(a,b){$telerik.toNotification=function(c){return c;
};
$telerik.findNotification=$find;
a.registerEnum(b,"NotificationLoad",{PageLoad:0,FirstShow:1,EveryShow:2,TimeInterval:3});
a.registerEnum(b,"NotificationPosition",{TopLeft:11,TopCenter:12,TopRight:13,MiddleLeft:21,Center:22,MiddleRight:23,BottomLeft:31,BottomCenter:32,BottomRight:33});
a.registerEnum(b,"NotificationHorizontalPosition",{Left:1,Center:2,Right:3});
a.registerEnum(b,"NotificationVerticalPosition",{Top:1,Middle:2,Bottom:3});
a.registerEnum(b,"NotificationScrolling",{Auto:0,None:1,X:2,Y:3,Both:4,Default:5});
a.registerEnum(b,"NotificationAnimation",{None:0,Resize:1,Fade:2,Slide:4,FlyIn:8});
b.RadNotification=function(c){b.RadNotification.initializeBase(this,[c]);
this._popupElement=$get(this.get_id()+"_popup");
this._titlebar=$get(this.get_id()+"_titlebar");
this._menuIcon=$get(this.get_id()+"_rnMenuIcon");
this._closeIcon=$get(this.get_id()+"_rnCloseIcon");
this._xmlPanel=$find(this.get_id()+"_XmlPanel");
this._titleMenu=$find(this.get_id()+"_TitleMenu");
this._position=b.NotificationPosition.BottomRight;
this._visibleTitlebar=true;
this._width="";
this._height="";
this._updateInterval=0;
this._showInterval=0;
this._loadContentOn=b.NotificationLoad.PageLoad;
this._keepOnMouseOver=true;
this._enabled=true;
this._text="";
this._title="";
this._audioUrl="";
this._audioMimeType="";
this._audioElement=null;
this._firstShow=true;
this._autoCloseRef=null;
this._horizontalPosition=b.NotificationHorizontalPosition.Right;
this._verticalPosition=b.NotificationVerticalPosition.Bottom;
this._cancelAutoClose=false;
this._popupTouchScroll=null;
this._canPlaySound=false;
};
b.RadNotification.prototype={initialize:function(){b.RadNotification.callBaseMethod(this,"initialize");
var c=this.get_element().style.zIndex||$telerik.getCurrentStyle(this.get_element(),"zIndex");
if(!parseInt(c,10)){c=10000;
}this.set_zIndex(c);
this._addToDocument();
this._attachXmlPanelHandlers();
this._registerPopupHandlers(true);
if(this.get_enableAriaSupport()){this._applyAriaSupport();
}if(this._shouldPlaySound()){this._renderAudioElement();
}if(this.get_visibleOnPageLoad()){setTimeout(Function.createDelegate(this,function(){this.show();
}),0);
}},dispose:function(){this._stopTimer("_updateTimer");
this._stopTimer("_showTimer");
this.cancelAutoCloseDelay();
this._registerPopupHandlers(false);
this._createTouchScrollExtender(false);
if(this._popupBehavior){this._popupBehavior.dispose();
}var c=this.get_popupElement();
if(Sys&&Sys.WebForms){var d=Sys.WebForms.PageRequestManager.getInstance();
if(d&&d.get_isInAsyncPostBack()){$telerik.disposeElement(c);
}}if(c&&c.parentNode){c.parentNode.removeChild(c);
}b.RadNotification.callBaseMethod(this,"dispose");
},_attachXmlPanelHandlers:function(){var c=this._xmlPanel;
c.add_responseEnding(Function.createDelegate(this,this._onResponseEnding));
c.add_responseEnded(Function.createDelegate(this,this._onResponseEnded));
c.add_responseError(Function.createDelegate(this,this._onResponseError));
},_registerPopupHandlers:function(c){this._registerMouseKeepHandlers(c);
this._registerTitlebarHandlers(c);
this._registerWindowResizeHandler(c);
},_registerMouseKeepHandlers:function(c){var d=this.get_popupElement();
if(true==c&&this.get_keepOnMouseOver()){this._mouseEnterHandler=Function.createDelegate(this,this._onMouseEnter);
$addHandler(d,"mouseover",this._mouseEnterHandler);
this._mouseLeaveHandler=Function.createDelegate(this,this._onMouseLeave);
$addHandler(d,"mouseout",this._mouseLeaveHandler);
}else{if(this._mouseEnterHandler||this._mouseLeaveHandler){$clearHandlers(d);
this._mouseEnterHandler=null;
this._mouseLeaveHandler=null;
}}},_registerTitlebarHandlers:function(c){if(true==c){if(this.get_showTitleMenu()&&this._menuIcon){this._showMenuHandler=Function.createDelegate(this,this._onMenuIconClick);
$addHandler(this._menuIcon,"click",this._showMenuHandler);
}if(this.get_showCloseButton()&&this._closeIcon){this._closeHandler=Function.createDelegate(this,this._close);
$addHandler(this._closeIcon,"click",this._closeHandler);
}}else{if(this._showMenuHandler&&this._menuIcon){$removeHandler(this._menuIcon,"click",this._showMenuHandler);
}if(this._closeHandler&&this._closeIcon){$removeHandler(this._closeIcon,"click",this._closeHandler);
}}},_registerWindowResizeHandler:function(c){if(c){this._onWindowResizeDelegate=Function.createDelegate(this,this._repositionOnResize);
$addHandler(window,"resize",this._onWindowResizeDelegate);
}else{if(this._onWindowResizeDelegate){$removeHandler(window,"resize",this._onWindowResizeDelegate);
this._onWindowResizeDelegate=null;
}}},_repositionOnResize:function(){if(this.isVisible()){var d=this.get_width();
var c=this.get_height();
if((d.toString().indexOf("%")>-1)||(c.toString().indexOf("%")>-1)){this.setSize(d,c);
}this._show();
if($telerik.isIE9Mode){this._show();
}this._popupBehavior.pin(this.get_pinned());
}},_createAudioElement:function(){var c=document.createElement("audio");
c.setAttribute("src",this._audioUrl);
c.setAttribute("preload","auto");
c.style.position="absolute";
c.style.left=0;
c.style.top=0;
this._soundPlayDelegate=Function.createDelegate(this,this._onSoundPlay);
$telerik.onEvent(c,"play",this._soundPlayDelegate);
this._audioElement=c;
},_createObjectElement:function(){var d=document.createElement("embed");
d.setAttribute("id",this.get_id()+"_playAudio");
d.setAttribute("src",this._audioUrl);
var c=(this.get_visibleOnPageLoad()&&this.get_animation()==b.NotificationAnimation.None);
d.setAttribute("autostart",c);
d.setAttribute("pluginspage","http://www.apple.com/quicktime/download/");
d.setAttribute("name","NotificationAudio");
d.setAttribute("enablejavascript","true");
d.setAttribute("type",this._audioMimeType);
d.setAttribute("class","rnEmbed");
if($telerik.isIE7){this._moveIE7EmbedTag(d);
}this._audioElement=d;
this._canPlaySound=true;
},_moveIE7EmbedTag:function(c){c.style.width="0px";
c.style.hegiht="0px";
c.style.position="absolute";
c.style.top="-99999px";
c.style.left="-99999px";
},_supportsAudio:function(){return this._audioElement&&this._audioElement.play&&this._audioElement.canPlayType&&this._audioElement.canPlayType(this._audioMimeType);
},_shouldPlaySound:function(){return this._audioUrl!=""&&this._audioUrl.toString().toLowerCase()!="none";
},_onSoundPlay:function(){this._canPlaySound=true;
$telerik.offEvent(this._audioElement,"play",this._soundPlayDelegate);
},_renderAudioElement:function(){this._createAudioElement();
if(!this._supportsAudio()||!this._audioElement){this._createObjectElement();
}if(this._audioElement){this._getDefaultParent().appendChild(this._audioElement);
}},_close:function(c){this._hide(true);
return $telerik.cancelRawEvent(c);
},_onMenuIconClick:function(c){if(this.get_titleMenu()){this.get_titleMenu().show(c);
}return $telerik.cancelRawEvent(c);
},_onMouseEnter:function(c){var f=$telerik.isMouseOverElementEx(this.get_popupElement(),c);
if(f){this._cancelAutoClose=true;
this.cancelAutoCloseDelay();
var d=this.get_popupElement();
if(this.get_animation()!=b.NotificationAnimation.None&&a){a(d).stop(true,true);
}this._stopTimer("_showTimer");
d.style.position=this.get_pinned()?"fixed":"absolute";
$telerik.setVisible(d,true);
}},_onMouseLeave:function(c){var d=$telerik.isMouseOverElementEx(this.get_popupElement(),c);
if(!d){this._cancelAutoClose=false;
this._resetAutoCloseDelay();
}},_onResponseEnding:function(d,c){this.raiseEvent("updating",c);
},_onResponseEnded:function(f,c){var e=$get(this.get_id()+"_hiddenState");
if(e&&e.value&&f._isCallbackPanel){this.set_value(e.value);
}var d=this.get_contentElement();
if(f._getWebServiceLoader()&&!f._isCallbackPanel&&d){d.innerHTML=c.get_content();
}this.raiseEvent("updated",c);
this.setSize(this.get_width(),this.get_height());
this._setOverflow();
if(this.isVisible()){this._popupBehavior.pin(false);
this._show();
this._popupBehavior.pin(this.get_pinned());
}},_onResponseError:function(d,c){this.raiseEvent("updateError",c);
},show:function(){if(!this.get_enabled()){return;
}if(this.isVisible()){if(this.get_animation()!=b.NotificationAnimation.None&&a){a(this.get_popupElement()).stop(true,true);
}}this.setSize(this.get_width(),this.get_height());
this._setOverflow();
var c=new Sys.CancelEventArgs();
this.raiseEvent("showing",c);
if(c.get_cancel()){return;
}if(!this._popupBehavior){this._popupBehavior=$create(Telerik.Web.PopupBehavior,{id:(new Date()-100)+"PopupBehavior",parentElement:null,overlay:this._overlay,keepInScreenBounds:false},null,null,this.get_popupElement());
}this._popupBehavior.pin(false);
var d=this.get_loadContentOn();
if(d==b.NotificationLoad.EveryShow||(d==b.NotificationLoad.FirstShow&&this._firstShow)){this.update();
}this._firstShow=false;
if(this.get_animation()==b.NotificationAnimation.None){this._show();
this._afterShow();
}else{this._playAnimation();
}},update:function(){if(!this.get_enabled()){return;
}this._xmlPanel.set_value(this.get_value());
},_show:function(){var c=this.getBounds();
this._setPopupVisible(c.x,c.y);
this.playSound();
var d=this.get_contentElement();
if(d){$telerik.repaintChildren(d);
}},playSound:function(){var c=this._audioElement;
if(this._shouldPlaySound()&&c){if(c.tagName.toLowerCase()=="embed"){setTimeout(function(){if(typeof(c.Play)!="undefined"){c.Play();
}},10);
}else{c.play();
}}},userInitSound:function(){var c=this._audioElement;
if(!this._shouldPlaySound()||!c){return;
}c.play();
c.pause();
},verifySound:function(){return this._shouldPlaySound()&&this._canPlaySound;
},moveTo:function(c,d){this._setPopupVisible(c,d);
},_afterShow:function(){var d=this.get_width();
var c=this.get_height();
if((d.toString().indexOf("%")>-1)||(c.toString().indexOf("%")>-1)){this.setSize(d,c);
}this._show();
this._popupBehavior.pin(this.get_pinned());
this._updateOpacity();
this._setOverflow();
this.raiseEvent("shown");
if(this.get_enableAriaSupport()){this.get_popupElement().setAttribute("aria-hidden","false");
}if(!this._cancelAutoClose){this._resetAutoCloseDelay();
}},_playAnimation:function(){var k=this.get_pinned();
this._popupBehavior.pin(k);
var f=this.getBounds();
if(!k&&($telerik.isMobileSafari||$telerik.isSafari||$telerik.isChrome)){var h=window.pageYOffset;
var g=window.pageXOffset;
if(document.documentElement.scrollTop==0&&document.body.scrollTop>0){f.y+=h;
}if(document.documentElement.scrollLeft==0&&document.body.scrollLeft>0){f.x+=g;
}}var i=Function.createDelegate(this,function(){this._setDocumentOverflow(true);
this._popupBehavior.pin(false);
this._show();
this._afterShow();
this._setDocumentOverflow(false);
if($telerik.isFirefox&&!this.get_pinned()){this._show();
}});
var c=this.get_popupElement();
var l=this.get_position();
var e=this.get_animation();
if(this._verticalPosition!=2){var o=(this._verticalPosition==1?3:1);
l=parseInt(o+""+this._horizontalPosition,10);
}var m=e==b.NotificationAnimation.Resize;
var n=$telerik.getBounds(document.documentElement);
if(m){n.width=0;
n.height=0;
n.x=f.x;
n.y=f.y;
}else{n.width=f.width;
n.height=f.height;
}var d=this.get_animationDuration();
var j=this.get_opacity()/100;
window.setTimeout(function(){b.Animations.playJQueryAnimation(c,e,n,f,l,null,i,d,j);
},0);
},hide:function(){if(!this.isVisible()){return;
}this._hide();
},_hide:function(f){var g=this.get_popupElement();
var c=this.get_animation()!=b.NotificationAnimation.None;
if(c&&a){a(g).stop(true,true);
}var d=new Sys.CancelEventArgs();
d._manualClose=f?f:false;
d.get_manualClose=function(){return this._manualClose;
};
this.raiseEvent("hiding",d);
if(d.get_cancel()){return;
}if(c&&!f&&a){var e=Function.createDelegate(this,this._afterHide);
a(g).fadeOut(this.get_animationDuration(),e);
}else{this._afterHide();
}},_afterHide:function(){if(this.get_titleMenu()){this.get_titleMenu().hide();
}this._popupBehavior.hide();
this._popupBehavior.pin(false);
this.cancelAutoCloseDelay();
if(this.get_keepOnMouseOver()){this._resetTimer("_showTimer",this.show,this.get_showInterval());
}this.raiseEvent("hidden");
if(this.get_enableAriaSupport()&&!this.isVisible()){this.get_popupElement().setAttribute("aria-hidden","true");
}},_setPopupVisible:function(e,f){if($telerik.isMobileSafari||$telerik.isSafari||$telerik.isChrome){var d=window.pageYOffset;
var c=window.pageXOffset;
if(document.documentElement.scrollTop==0&&document.body.scrollTop>0){f+=d;
}if(document.documentElement.scrollLeft==0&&document.body.scrollLeft>0){e+=c;
}}this._popupBehavior.set_x(e);
this._popupBehavior.set_y(f);
this._popupBehavior.show();
if(!this.get_width()){this.get_popupElement().style.width="";
}},_addToDocument:function(){var d=this.get_popupElement();
var c=this._getDefaultParent();
c.appendChild(d);
if($telerik.isRightToLeft(c)){Sys.UI.DomElement.addCssClass(d,"rnRtl");
}},_getDefaultParent:function(){var c=this.get_formID();
var d=c?document.getElementById(c):null;
if(!d){if(document.forms&&document.forms.length>0){d=document.forms[0];
}else{d=document.body;
}}return d;
},getBounds:function(){var f=this.get_popupElement();
var g=(f.style.display=="none")?true:false;
if(g){f.style.visibility="hidden";
}f.style.display="";
var d=document.documentElement;
this._popupBehavior.set_parentElement(d);
var e=$telerik.getBounds(f);
var c=this._getBoundsRelativeToBrowser(e);
if(g){f.style.display="none";
f.style.visibility="";
}return c;
},_getBoundsRelativeToBrowser:function(d){var c=this._horizontalPosition;
var g=this._verticalPosition;
var h=0;
var i=0;
var e=$telerik.getClientBounds();
var f=$telerik.getScrollOffset(document.compatMode&&document.compatMode!="BackCompat"?document.documentElement:document.body);
if("fixed"!=this.get_popupElement().style.position){h+=f.x;
i+=f.y;
}switch(c){case 2:h+=-parseInt(d.width/2-e.width/2,10);
break;
case 1:break;
case 3:default:h+=e.width;
h-=d.width;
break;
}switch(g){case 2:i+=-parseInt((d.height-e.height)/2,10);
break;
case 1:break;
case 3:default:i+=e.height;
i-=d.height;
break;
}h+=this.get_offsetX();
i+=this.get_offsetY();
return new Telerik.Web.UI.Bounds(h,i,d.width,d.height);
},get_popupElement:function(){return this._popupElement;
},get_contentElement:function(){return $get(this.get_id()+"_C");
},get_position:function(){return this._position;
},set_position:function(d){if(this._position!=d){this._position=d;
}var c=this.get_position();
this._horizontalPosition=this._getSide(c,true);
this._verticalPosition=this._getSide(c,false);
},get_visibleTitlebar:function(){return this._visibleTitlebar;
},set_visibleTitlebar:function(c){if(c!=this.get_visibleTitlebar()){this._visibleTitlebar=c;
this._titlebar.style.display=c?"":"none";
if(this.get_enableAriaSupport()){this._titlebar.setAttribute("aria-hidden",!c);
}this.set_height(this.get_height());
var d=this.get_popupElement();
if(c){Sys.UI.DomElement.removeCssClass(d,"rnNoTitleBar");
}else{Sys.UI.DomElement.addCssClass(d,"rnNoTitleBar");
}}},get_width:function(){return this._width;
},set_width:function(f){var e=this.get_popupElement();
var d=parseInt(f,10);
if(isNaN(d)||d<=0){return;
}if(f.toString().indexOf("%")>-1){this._width=f;
e.style.width=f;
d=e.offsetWidth;
}else{this._width=d;
e.style.width=d+"px";
}var c=this.get_contentElement();
if(!c){return;
}d-=$telerik.getPaddingBox(c).horizontal;
c.style.width=(isNaN(d)||d<=0)?"":d+"px";
},get_height:function(){return this._height;
},set_height:function(g){var e=this.get_popupElement();
var d=parseInt(g,10);
if(isNaN(d)||d<=0){return;
}if(g.toString().indexOf("%")>-1){this._height=g;
e.style.height=g;
d=e.offsetHeight;
}else{this._height=d;
e.style.height=d+"px";
}var c=this.get_contentElement();
if(!c){return;
}d-=$telerik.getPaddingBox(c).vertical+$telerik.getBorderBox(e).vertical;
var f=a(this._titlebar).height();
if(this.get_visibleTitlebar()&&this._titlebar&&!isNaN(d)&&d>f){d-=(f!=0?f+2:parseInt($telerik.getCurrentStyle(this._titlebar,"height"),10));
}c.style.height=(isNaN(d)||d<=0)?"":d+"px";
},get_showInterval:function(){return this._showInterval;
},set_showInterval:function(c){if(c!=this.get_showInterval()&&!isNaN(parseInt(c,10))){this._showInterval=c;
this._resetTimer("_showTimer",this.show,c);
}},get_updateInterval:function(){return this._updateInterval;
},set_updateInterval:function(c){if(c!=this.get_updateInterval()&&this.get_loadContentOn()==b.NotificationLoad.TimeInterval&&!isNaN(parseInt(c,10))){this._updateInterval=c;
this._resetTimer("_updateTimer",this.update,this.get_updateInterval());
}},get_loadContentOn:function(){return this._loadContentOn;
},set_loadContentOn:function(c){if(c!=this.get_loadContentOn()){if(this.get_loadContentOn()==b.NotificationLoad.TimeInterval){this._stopTimer("_updateTimer");
}this._loadContentOn=c;
}},_stopTimer:function(c){if(this[c]){clearInterval(this[c]);
this[c]=null;
}},_resetTimer:function(e,c,d){this._stopTimer(e);
if(d!=0){this[e]=setInterval(Function.createDelegate(this,c),d);
}},get_keepOnMouseOver:function(){return this._keepOnMouseOver;
},set_keepOnMouseOver:function(c){if(c!=this.get_keepOnMouseOver()){this._keepOnMouseOver=c;
this._registerMouseKeepHandlers(c);
}},get_enabled:function(){return this._enabled;
},set_enabled:function(c){if(c!=this.get_enabled()){this._enabled=c;
if(!c){this._stopTimer("_showTimer");
this._stopTimer("_updateTimer");
}else{this._resetTimer("_showTimer",this.show,this.get_showInterval());
this._resetTimer("_updateTimer",this.update,this.get_updateInterval());
}}},get_opacity:function(){return this._opacity;
},set_opacity:function(c){if(this.get_opacity()!=c){this._opacity=c>100?100:c;
this._opacity=c<0?0:c;
this._updateOpacity();
}},_updateOpacity:function(){var d=this.get_popupElement();
var c=this.get_opacity();
if(c<100){var e=d.style;
e.filter="alpha(opacity="+c+")";
e.opacity=(c/100);
}else{if($telerik.isIE){d.style.removeAttribute("filter");
d.style.removeAttribute("opacity");
}else{d.style.filter="";
d.style.opacity="";
}}},get_title:function(){return this._title;
},set_title:function(d){if(this._title!=d){this._title=d;
var c=$telerik.getChildByClassName(this._titlebar,"rnTitleBarTitle",1);
if(c){c.innerHTML=d;
}}},get_text:function(){return this._text;
},set_text:function(d){var c=$get(this.get_id()+"_simpleContentDiv");
if(this.get_text()!=d&&c){this._text=d;
c.innerHTML=d;
}},_getSide:function(e,d){var c=d?1:0;
return parseInt((e+"").charAt(c),10);
},_setOverflow:function(){var e=this.get_contentScrolling();
if(e==b.NotificationScrolling.Default){return;
}var c=this.get_contentElement();
if(!c){return;
}var f="";
var d=true;
var g=b.NotificationScrolling;
switch(e){case g.Auto:f="auto";
break;
case g.None:f="hidden";
d=false;
break;
case g.X:f="";
c.style.overflowX="scroll";
c.style.overflowY="hidden";
break;
case g.Y:f="";
c.style.overflowY="scroll";
c.style.overflowX="hidden";
break;
case g.Both:f="scroll";
}if(f!=""){c.style.overflow=f;
}if(d&&b.TouchScrollExtender._getNeedsScrollExtender()&&!this._popupTouchScroll){this._createTouchScrollExtender(true);
}},_createTouchScrollExtender:function(e){var c=this.get_contentElement();
if(c){var d=this._popupTouchScroll;
if(d){if(!e){d.dispose();
this._popupTouchScroll=null;
}}else{if(e){this._popupTouchScroll=new b.TouchScrollExtender(c);
this._popupTouchScroll.initialize();
}}}},_setDocumentOverflow:function(c){if(c){this._documentOverflowX=document.documentElement.style.overflowX;
this._scrollY=document.documentElement.scrollTop;
document.documentElement.style.overflowX="hidden";
}else{if(null!=this._documentOverflowX){document.documentElement.style.overflowX=this._documentOverflowX;
this._documentOverflowX=null;
if(this._scrollY){document.documentElement.scrollTop=this._scrollY;
}this._scrollY=null;
}}},_applyAriaSupport:function(){var c=this.get_popupElement();
c.setAttribute("role","alert");
c.setAttribute("aria-live","polite");
c.setAttribute("aria-atomic","true");
c.setAttribute("aria-hidden","true");
c.setAttribute("aria-relevant","additions text");
if(!this.get_visibleTitlebar()){this._titlebar.setAttribute("aria-hidden","true");
}else{this._titlebar.setAttribute("role","toolbar");
if(this._closeIcon){this._closeIcon.removeAttribute("href");
this._closeIcon.setAttribute("role","button");
}if(this._menuIcon){this._menuIcon.removeAttribute("href");
this._menuIcon.setAttribute("role","button");
}var d=$telerik.getChildByClassName(this._titlebar,"rnTitleBarTitle",1);
if(d){d.setAttribute("id",this.get_id()+"_title");
c.setAttribute("aria-labelledby",d.id);
}var e=$telerik.getChildByClassName(this._titlebar," rnCommands",1);
if(e){e.setAttribute("role","presentation");
}}if(this._xmlPanel){c.setAttribute("aria-describedby",this._xmlPanel.get_id());
}},isVisible:function(){var c=this.get_popupElement();
return(c&&c.style.display!="none");
},setSize:function(d,c){this.set_width(d);
this.set_height(c);
},_resetAutoCloseDelay:function(){this.cancelAutoCloseDelay();
if(this.get_autoCloseDelay()!=0){this._autoCloseRef=window.setTimeout(Function.createDelegate(this,function(){this.hide();
}),this.get_autoCloseDelay());
}},cancelAutoCloseDelay:function(){if(this._autoCloseRef){window.clearTimeout(this._autoCloseRef);
this._autoCloseRef=0;
}},get_titleMenu:function(){return this._titleMenu;
},get_zIndex:function(){return this._zIndex;
},set_zIndex:function(d){var e=parseInt(d,10);
if(isNaN(e)){return;
}if(this._zIndex!=d){this._zIndex=e;
this.get_popupElement().style.zIndex=e;
var c=this.get_titleMenu();
if(c){c._getContextMenuElement().style.zIndex=c._originalZIndex=c._defaultZIndex=e+100;
}}}};
a.registerControlProperties(b.RadNotification,{animation:b.NotificationAnimation.None,animationDuration:500,formID:null,offsetX:0,offsetY:0,pinned:true,overlay:false,contentScrolling:b.NotificationScrolling.Default,value:"",autoCloseDelay:3000,visibleOnPageLoad:false,showCloseButton:true,showTitleMenu:false,enableAriaSupport:false});
a.registerControlEvents(b.RadNotification,["hidden","hiding","shown","showing","updated","updateError","updating"]);
Telerik.Web.UI.RadNotification.registerClass("Telerik.Web.UI.RadNotification",b.RadWebControl);
})($telerik.$,Telerik.Web.UI);