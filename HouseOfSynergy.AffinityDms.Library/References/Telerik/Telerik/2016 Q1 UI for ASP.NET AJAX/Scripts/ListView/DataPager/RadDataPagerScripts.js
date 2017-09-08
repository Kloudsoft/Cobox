(function(){Type.registerNamespace("Telerik.Web.UI");
var a=Telerik.Web.UI;
$telerik.findDataPager=$find;
$telerik.toDataPager=function(c){return c;
};
function b(c){if(typeof(c)=="number"){return c;
}else{if(typeof(c)=="string"){return parseInt(c,10);
}}}a.RadDataPager=function(c){var d=["dataPagerCreating","dataPagerCreated","dataPagerDestroying","pageIndexChanging","pageSizeChanging"];
this._initializeEvents(d);
a.RadDataPager.initializeBase(this,[c]);
this._startRowIndex=0;
this._pageSize=10;
this._totalRowCount=-1;
this._currentPageIndex=1;
this._pageCount=1;
this._enableAriaSupport=false;
this._uniqueID=null;
this._postBackFunction="__doPostBack('{0}','{1}')";
};
a.RadDataPager._executePostBackEvent=function(){var c=Array.prototype.slice.call(arguments);
this._postBackFunction=this._postBackFunction.replace("{0}",this.get_uniqueID());
this._postBackFunction=this._postBackFunction.replace("{1}",c);
eval(this._postBackFunction);
};
a.RadDataPager._performSeoNavigation=function(c,e,f){var j=e.get_attributes().getAttribute("seoRedirectUrl");
var i=e.get_attributes().getAttribute("seoPagerKey");
var g=c.get_currentPageIndex()+1;
var d=g>c.get_totalRowCount()/f;
if(j){if(d){var h=new RegExp(i+"=\\d+_\\d+","g");
j=j.replace(h,String.format("{0}={1}_{2}",i,1,f));
}window.location.href=j;
return true;
}};
a.RadDataPager.prototype={initialize:function(){a.RadDataPager.callBaseMethod(this,"initialize");
this.raise_dataPagerCreating(new Sys.EventArgs());
if(this.get_enableAriaSupport()){this._initializeAriaSupport();
}this.raise_dataPagerCreated(new Sys.EventArgs());
},dispose:function(){this.raise_dataPagerDestroying(new Sys.EventArgs());
window.$clearHandlers(this.get_element());
a.RadDataPager.callBaseMethod(this,"dispose");
},get_uniqueID:function(){return this._uniqueID;
},get_pageCount:function(){return this._pageCount;
},get_startRowIndex:function(){return this._startRowIndex;
},get_pageSize:function(){return this._pageSize;
},get_totalRowCount:function(){return this._totalRowCount;
},get_currentPageIndex:function(){return this._currentPageIndex;
},get_enableAriaSupport:function(){return this._enableAriaSupport;
},set_currentPageIndex:function(e){var c=new a.DataPagerPageIndexChangingEventArgs(this._currentPageIndex,e);
this.raise_pageIndexChanging(c);
if(c.get_cancel()){return;
}var d=b(c.get_newPageIndex());
if(isNaN(d)||(this._currentPageIndex==d)){return;
}this._currentPageIndex=d;
this.fireCommand("Page",this._currentPageIndex);
},set_pageSize:function(e){var c=new a.DataPagerPageSizeChangingEventArgs(this._pageSize,e);
this.raise_pageSizeChanging(c);
if(c.get_cancel()){return;
}var d=b(c.get_newPageSize());
if(isNaN(d)||(this._pageSize==d)){return;
}this._pageSize=d;
this.fireCommand("PageSizeChange",this._pageSize);
},fireCommand:function(d,c){a.RadDataPager._executePostBackEvent.call(this,this._constructPostBackData(d,c));
},_constructPostBackData:function(d,c){return String.format("FireCommand:{0}|;{1}|;",d,c);
},_initializeEvents:function(c){if(c){var g=this;
for(var d=0,e=c.length;
d<e;
d++){var f=c[d];
g["add_"+f]=(function(h){return function(i){this.get_events().addHandler(h,i);
};
}(f));
g["remove_"+f]=(function(h){return function(i){this.get_events().removeHandler(h,i);
};
}(f));
g["raise_"+f]=(function(h){return function(i){this.raiseEvent(h,i);
};
}(f));
}}},_initializeAriaSupport:function(){var k=this.get_element();
k.setAttribute("role","presentation");
k.setAttribute("aria-atomic","true");
k.setAttribute("aria-label",k.id);
var d=k.getElementsByTagName("div");
for(var e=0;
e<d.length;
e++){var c=d[e];
if(c.className&&c.className.indexOf("rdpWrap")>-1){var g=c.getElementsByTagName("input");
for(var h=0;
h<g.length;
h++){var f=g[h];
if(f.type=="submit"&&f.id&&(f.id.indexOf("PrevButton")>-1||f.id.indexOf("FirstButton")>-1||f.id.indexOf("NextButton")>-1||f.id.indexOf("LastButton")>-1||f.id.indexOf("PageSizeButton")>-1||f.id.indexOf("GoToPageButton")>-1)){f.setAttribute("role","button");
}}}}}};
a.RadDataPager.registerClass("Telerik.Web.UI.RadDataPager",a.RadWebControl);
a.DataPagerPageIndexChangingEventArgs=function(d,c){a.DataPagerPageIndexChangingEventArgs.initializeBase(this);
this._newPageIndex=c;
this._oldPageIndex=d;
};
a.DataPagerPageIndexChangingEventArgs.prototype={get_oldPageIndex:function(){return this._oldPageIndex;
},get_newPageIndex:function(){return this._newPageIndex;
},set_newPageIndex:function(c){this._newPageIndex=c;
}};
a.DataPagerPageIndexChangingEventArgs.registerClass("Telerik.Web.UI.DataPagerPageIndexChangingEventArgs",Sys.CancelEventArgs);
a.DataPagerPageSizeChangingEventArgs=function(d,c){a.DataPagerPageSizeChangingEventArgs.initializeBase(this);
this._newPageSize=c;
this._oldPageSize=d;
};
a.DataPagerPageSizeChangingEventArgs.prototype={get_oldPageSize:function(){return this._oldPageSize;
},get_newPageSize:function(){return this._newPageSize;
},set_newPageSize:function(c){this._newPageSize=c;
}};
a.DataPagerPageSizeChangingEventArgs.registerClass("Telerik.Web.UI.DataPagerPageSizeChangingEventArgs",Sys.CancelEventArgs);
})();
Telerik.Web.UI.RadDataPager.ChangePageSizeComboHandler=function(g,a){var e;
if(a.get_item){e=a.get_item();
}else{if(a.get_index){e=g.get_items().getItem(a.get_index());
}}if(e){var c=e.get_attributes().getAttribute("dataPagerClientId");
var f=null;
if(g.get_value){if(g.get_value()){f=g.get_value();
}else{f=g.get_text();
}}else{if(e.get_value()){f=e.get_value();
}else{f=e.get_text();
}}if(c&&f){var b=$find(c);
var d=parseInt(f,10);
if(b){if(!Telerik.Web.UI.RadDataPager._performSeoNavigation(b,e,d)){b.set_pageSize(d);
}}}}};
