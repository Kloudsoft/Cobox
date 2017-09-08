(function(b,a,e){var d=window.jQuery,c=window.$;
window.$=window.jQuery=a;
(function(g,f,j){var i=window.jQuery,h=window.$;
window.$=window.jQuery=f;
(function(l,k){k("util/main",["kendo.core"],l);
}(function(){(function(){var A=Math,x=window.kendo,p=x.deepExtend;
var s=A.PI/180,B=Number.MAX_VALUE,D=-Number.MAX_VALUE,W="undefined";
function q(Y){return typeof Y!==W;
}function P(aa,Z){var Y=G(Z);
return A.round(aa*Y)/Y;
}function G(Y){if(Y){return A.pow(10,Y);
}else{return 1;
}}function z(aa,Z,Y){return A.max(A.min(aa,Y),Z);
}function H(Y){return Y*s;
}function r(Y){return Y/s;
}function v(Y){return typeof Y==="number"&&!isNaN(Y);
}function X(Z,Y){return q(Z)?Z:Y;
}function T(Y){return Y*Y;
}function F(Z){var aa=[];
for(var Y in Z){aa.push(Y+Z[Y]);
}return aa.sort().join("");
}function t(aa){var Y=2166136261;
for(var Z=0;
Z<aa.length;
++Z){Y+=(Y<<1)+(Y<<4)+(Y<<7)+(Y<<8)+(Y<<24);
Y^=aa.charCodeAt(Z);
}return Y>>>0;
}function u(Y){return t(F(Y));
}var E=Date.now;
if(!E){E=function(){return new Date().getTime();
};
}function m(Y){var aa=Y.length,Z,ac=B,ab=D;
for(Z=0;
Z<aa;
Z++){ab=A.max(ab,Y[Z]);
ac=A.min(ac,Y[Z]);
}return{min:ac,max:ab};
}function o(Y){return m(Y).min;
}function n(Y){return m(Y).max;
}function S(Y){return Q(Y).min;
}function R(Y){return Q(Y).max;
}function Q(Y){var ac=B,ab=D;
for(var Z=0,aa=Y.length;
Z<aa;
Z++){var ad=Y[Z];
if(ad!==null&&isFinite(ad)){ac=A.min(ac,ad);
ab=A.max(ab,ad);
}}return{min:ac===B?j:ac,max:ab===D?j:ab};
}function y(Y){if(Y){return Y[Y.length-1];
}}function k(Y,Z){Y.push.apply(Y,Z);
return Y;
}function N(Y){return x.template(Y,{useWithBlock:false,paramName:"d"});
}function J(Y,Z){return q(Z)&&Z!==null?" "+Y+"='"+Z+"' ":"";
}function I(Y){var aa="";
for(var Z=0;
Z<Y.length;
Z++){aa+=J(Y[Z][0],Y[Z][1]);
}return aa;
}function M(Y){var aa="";
for(var Z=0;
Z<Y.length;
Z++){var ab=Y[Z][1];
if(q(ab)){aa+=Y[Z][0]+":"+ab+";";
}}if(aa!==""){return aa;
}}function L(Y){if(typeof Y!=="string"){Y+="px";
}return Y;
}function K(aa){var ab=[];
if(aa){var Z=x.toHyphens(aa).split("-");
for(var Y=0;
Y<Z.length;
Y++){ab.push("k-pos-"+Z[Y]);
}}return ab.join(" ");
}function w(Y){return Y===""||Y===null||Y==="none"||Y==="transparent"||!q(Y);
}function l(Z){var Y={1:"i",10:"x",100:"c",2:"ii",20:"xx",200:"cc",3:"iii",30:"xxx",300:"ccc",4:"iv",40:"xl",400:"cd",5:"v",50:"l",500:"d",6:"vi",60:"lx",600:"dc",7:"vii",70:"lxx",700:"dcc",8:"viii",80:"lxxx",800:"dccc",9:"ix",90:"xc",900:"cm",1000:"m"};
var ab=[1000,900,800,700,600,500,400,300,200,100,90,80,70,60,50,40,30,20,10,9,8,7,6,5,4,3,2,1];
var aa="";
while(Z>0){if(Z<ab[0]){ab.shift();
}else{aa+=Y[ab[0]];
Z-=ab[0];
}}return aa;
}function O(ab){ab=ab.toLowerCase();
var Y={i:1,v:5,x:10,l:50,c:100,d:500,m:1000};
var ad=0,aa=0;
for(var Z=0;
Z<ab.length;
++Z){var ac=Y[ab.charAt(Z)];
if(!ac){return null;
}ad+=ac;
if(ac>aa){ad-=2*aa;
}aa=ac;
}return ad;
}function C(Z){var Y=Object.create(null);
return function(){var ab="";
for(var aa=arguments.length;
--aa>=0;
){ab+=":"+arguments[aa];
}if(ab in Y){return Y[ab];
}return Z.apply(this,arguments);
};
}function U(ac){var ab=[],Y=0,aa=ac.length,ad,Z;
while(Y<aa){ad=ac.charCodeAt(Y++);
if(ad>=55296&&ad<=56319&&Y<aa){Z=ac.charCodeAt(Y++);
if((Z&64512)==56320){ab.push(((ad&1023)<<10)+(Z&1023)+65536);
}else{ab.push(ad);
Y--;
}}else{ab.push(ad);
}}return ab;
}function V(Y){return Y.map(function(aa){var Z="";
if(aa>65535){aa-=65536;
Z+=String.fromCharCode(aa>>>10&1023|55296);
aa=56320|aa&1023;
}Z+=String.fromCharCode(aa);
return Z;
}).join("");
}p(x,{util:{MAX_NUM:B,MIN_NUM:D,append:k,arrayLimits:m,arrayMin:o,arrayMax:n,defined:q,deg:r,hashKey:t,hashObject:u,isNumber:v,isTransparent:w,last:y,limitValue:z,now:E,objectKey:F,round:P,rad:H,renderAttr:J,renderAllAttr:I,renderPos:K,renderSize:L,renderStyle:M,renderTemplate:N,sparseArrayLimits:Q,sparseArrayMin:S,sparseArrayMax:R,sqr:T,valueOrDefault:X,romanToArabic:O,arabicToRoman:l,memoize:C,ucs2encode:V,ucs2decode:U}});
x.drawing.util=x.util;
x.dataviz.util=x.util;
}());
return window.kendo;
},typeof define=="function"&&define.amd?define:function(k,l,m){(m||l)();
}));
(function(l,k){k("util/text-metrics",["kendo.core","util/main"],l);
}(function(){(function(k){var o=document,p=window.kendo,l=p.Class,u=p.util,n=u.defined;
var r=l.extend({init:function(w){this._size=w;
this._length=0;
this._map={};
},put:function(x,A){var y=this,z=y._map,w={key:x,value:A};
z[x]=w;
if(!y._head){y._head=y._tail=w;
}else{y._tail.newer=w;
w.older=y._tail;
y._tail=w;
}if(y._length>=y._size){z[y._head.key]=null;
y._head=y._head.newer;
y._head.older=null;
}else{y._length++;
}},get:function(x){var y=this,w=y._map[x];
if(w){if(w===y._head&&w!==y._tail){y._head=w.newer;
y._head.older=null;
}if(w!==y._tail){if(w.older){w.older.newer=w.newer;
w.newer.older=w.older;
}w.older=y._tail;
w.newer=null;
y._tail.newer=w;
y._tail=w;
}return w.value;
}}});
var m=k("<div style='position: absolute !important; top: -4000px !important; width: auto !important; height: auto !important;padding: 0 !important; margin: 0 !important; border: 0 !important;line-height: normal !important; visibility: hidden !important; white-space: nowrap!important;' />")[0];
function v(){return{width:0,height:0,baseline:0};
}var t=l.extend({init:function(w){this._cache=new r(1000);
this._initOptions(w);
},options:{baselineMarkerSize:1},measure:function(F,D,x){if(!F){return v();
}var E=u.objectKey(D),z=u.hashKey(F+E),y=this._cache.get(z);
if(y){return y;
}var C=v();
var B=x?x:m;
var w=this._baselineMarker().cloneNode(false);
for(var A in D){var G=D[A];
if(n(G)){B.style[A]=G;
}}k(B).text(F);
B.appendChild(w);
o.body.appendChild(B);
if((F+"").length){C.width=B.offsetWidth-this.options.baselineMarkerSize;
C.height=B.offsetHeight;
C.baseline=w.offsetTop+this.options.baselineMarkerSize;
}if(C.width>0&&C.height>0){this._cache.put(z,C);
}B.parentNode.removeChild(B);
return C;
},_baselineMarker:function(){return k("<div class='k-baseline-marker' style='display: inline-block; vertical-align: baseline;width: "+this.options.baselineMarkerSize+"px; height: "+this.options.baselineMarkerSize+"px;overflow: hidden;' />")[0];
}});
t.current=new t();
function s(y,x,w){return t.current.measure(y,x,w);
}function q(y,w){var z=[];
if(y.length>0&&document.fonts){try{z=y.map(function(A){return document.fonts.load(A);
});
}catch(x){p.logToConsole(x);
}Promise.all(z).then(w,w);
}else{w();
}}p.util.TextMetrics=t;
p.util.LRUCache=r;
p.util.loadFonts=q;
p.util.measureText=s;
}(window.kendo.jQuery));
},typeof define=="function"&&define.amd?define:function(k,l,m){(m||l)();
}));
(function(l,k){k("util/base64",["util/main"],l);
}(function(){(function(){var o=window.kendo,k=o.deepExtend,n=String.fromCharCode;
var p="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
function l(y){var z="";
var q,r,s,t,u,v,w;
var x=0;
y=m(y);
while(x<y.length){q=y.charCodeAt(x++);
r=y.charCodeAt(x++);
s=y.charCodeAt(x++);
t=q>>2;
u=(q&3)<<4|r>>4;
v=(r&15)<<2|s>>6;
w=s&63;
if(isNaN(r)){v=w=64;
}else{if(isNaN(s)){w=64;
}}z=z+p.charAt(t)+p.charAt(u)+p.charAt(v)+p.charAt(w);
}return z;
}function m(s){var t="";
for(var r=0;
r<s.length;
r++){var q=s.charCodeAt(r);
if(q<128){t+=n(q);
}else{if(q<2048){t+=n(192|q>>>6);
t+=n(128|q&63);
}else{if(q<65536){t+=n(224|q>>>12);
t+=n(128|q>>>6&63);
t+=n(128|q&63);
}}}}return t;
}k(o.util,{encodeBase64:l,encodeUTF8:m});
}());
return window.kendo;
},typeof define=="function"&&define.amd?define:function(k,l,m){(m||l)();
}));
(function(l,k){k("mixins/observers",["kendo.core"],l);
}(function(){(function(k){var o=Math,n=window.kendo,l=n.deepExtend,m=k.inArray;
var p={observers:function(){this._observers=this._observers||[];
return this._observers;
},addObserver:function(q){if(!this._observers){this._observers=[q];
}else{this._observers.push(q);
}return this;
},removeObserver:function(q){var s=this.observers();
var r=m(q,s);
if(r!=-1){s.splice(r,1);
}return this;
},trigger:function(s,q){var u=this._observers;
var t;
var r;
if(u&&!this._suspended){for(r=0;
r<u.length;
r++){t=u[r];
if(t[s]){t[s](q);
}}}return this;
},optionsChange:function(q){this.trigger("optionsChange",q);
},geometryChange:function(q){this.trigger("geometryChange",q);
},suspend:function(){this._suspended=(this._suspended||0)+1;
return this;
},resume:function(){this._suspended=o.max((this._suspended||0)-1,0);
return this;
},_observerField:function(q,r){if(this[q]){this[q].removeObserver(this);
}this[q]=r;
r.addObserver(this);
}};
l(n,{mixins:{ObserversMixin:p}});
}(window.kendo.jQuery));
return window.kendo;
},typeof define=="function"&&define.amd?define:function(k,l,m){(m||l)();
}));
(function(l,k){k("kendo.dataviz.stock",["kendo.dataviz.chart"],l);
}(function(){var k={id:"dataviz.stockchart",name:"StockChart",category:"dataviz",description:"StockChart widget and associated financial series.",depends:["dataviz.chart"]};
(function(l,S){var B=window.kendo,q=B.Class,K=B.Observable,v=B.deepExtend,F=Math,L=l.proxy,T=B.util,C=T.last,M=T.renderTemplate,u=B.dataviz,w=T.defined,A=u.filterSeriesByType,P=B.template,p=u.ui.Chart,N=u.Selection,m=u.addDuration,D=T.limitValue,E=u.lteDateIndex,Q=u.toDate,R=u.toTime;
var n=28,o="change",t="k-",x="drag",y="dragEnd",I="_navigator",H=I,z=u.EQUALLY_SPACED_SERIES,V=3,U="zoom",W="zoomEnd";
var O=p.extend({init:function(X,Y){l(X).addClass(t+"chart");
p.fn.init.call(this,X,Y);
},_applyDefaults:function(Y,aa){var X=this,ab=X.element.width()||u.DEFAULT_WIDTH;
var Z={seriesDefaults:{categoryField:Y.dateField},axisDefaults:{categoryAxis:{name:"default",majorGridLines:{visible:false},labels:{step:2},majorTicks:{visible:false},maxDateGroups:F.floor(ab/n)}}};
if(aa){aa=v({},aa,Z);
}if(!X._navigator){G.setup(Y,aa);
}p.fn._applyDefaults.call(X,Y,aa);
},_initDataSource:function(ag){var ae=ag||{},X=ae.dataSource,ab=X&&X.serverFiltering,ac=[].concat(ae.categoryAxis)[0],ad=ae.navigator||{},af=ad.select,aa=af&&af.from&&af.to,Z,Y;
if(ab&&aa){Z=[].concat(X.filter||[]);
Y=new u.DateCategoryAxis(v({baseUnit:"fit"},ac,{categories:[af.from,af.to]}));
X.filter=G.buildFilter(Y.range().min,af.to).concat(Z);
}p.fn._initDataSource.call(this,ag);
},options:{name:"StockChart",dateField:"date",axisDefaults:{categoryAxis:{type:"date",baseUnit:"fit",justified:true},valueAxis:{narrowRange:true,labels:{format:"C"}}},navigator:{select:{},seriesDefaults:{markers:{visible:false},tooltip:{visible:true,template:"#= kendo.toString(category, 'd') #"},line:{width:2}},hint:{},visible:true},tooltip:{visible:true},legend:{visible:false}},_resize:function(){var X=this.options.transitions;
this.options.transitions=false;
this._fullRedraw();
this.options.transitions=X;
},_redraw:function(){var X=this,Y=X._navigator;
if(!this._dirty()&&Y&&Y.dataSource){Y.redrawSlaves();
}else{X._fullRedraw();
}},_dirty:function(){var Y=this.options;
var Z=[].concat(Y.series,Y.navigator.series);
var aa=l.grep(Z,function(ab){return ab&&ab.visible;
}).length;
var X=this._seriesCount!==aa;
this._seriesCount=aa;
return X;
},_fullRedraw:function(){var X=this,Y=X._navigator;
if(!Y){Y=X._navigator=new G(X);
}Y._setRange();
p.fn._redraw.call(X);
Y._initSelection();
},_onDataChanged:function(){var X=this;
p.fn._onDataChanged.call(X);
X._dataBound=true;
},_bindCategoryAxis:function(Y,ad,Z){var ab=this,aa=ab.options.categoryAxis,X=aa.length,ac;
p.fn._bindCategoryAxis.apply(this,arguments);
if(Y.name===H){while(Z<X){ac=aa[Z++];
if(ac.pane==I){ac.categories=Y.categories;
}}}},_trackSharedTooltip:function(Y){var X=this,aa=X._plotArea,Z=aa.paneByPoint(Y);
if(Z&&Z.options.name===I){X._unsetActivePoint();
}else{p.fn._trackSharedTooltip.call(X,Y);
}},destroy:function(){var X=this;
X._navigator.destroy();
p.fn.destroy.call(X);
}});
var G=K.extend({init:function(X){var Y=this;
Y.chart=X;
Y.options=v({},Y.options,X.options.navigator);
Y._initDataSource();
if(!w(Y.options.hint.visible)){Y.options.hint.visible=Y.options.visible;
}X.bind(x,L(Y._drag,Y));
X.bind(y,L(Y._dragEnd,Y));
X.bind(U,L(Y._zoom,Y));
X.bind(W,L(Y._zoomEnd,Y));
},options:{},_initDataSource:function(){var Z=this,aa=Z.options,X=aa.autoBind,Y=aa.dataSource;
if(!w(X)){X=Z.chart.options.autoBind;
}Z._dataChangedHandler=L(Z._onDataChanged,Z);
if(Y){Z.dataSource=B.data.DataSource.create(Y).bind(o,Z._dataChangedHandler);
if(X){Z.dataSource.fetch();
}}},_onDataChanged:function(){var ae=this,aa=ae.chart,ag=aa.options.series,ah,ai=ag.length,Z=aa.options.categoryAxis,Y,X=Z.length,ad=ae.dataSource.view(),ac,ab,af;
for(ah=0;
ah<ai;
ah++){ac=ag[ah];
if(ac.axis==H&&aa._isBindable(ac)){ac.data=ad;
}}for(Y=0;
Y<X;
Y++){ab=Z[Y];
if(ab.pane==I){if(ab.name==H){aa._bindCategoryAxis(ab,ad,Y);
af=ab.categories;
}else{ab.categories=af;
}}}if(aa._model){ae.redraw();
ae.filterAxes();
if(!aa.options.dataSource||aa.options.dataSource&&aa._dataBound){ae.redrawSlaves();
}}},destroy:function(){var Y=this,X=Y.dataSource;
if(X){X.unbind(o,Y._dataChangeHandler);
}if(Y.selection){Y.selection.destroy();
}},redraw:function(){this._redrawSelf();
this._initSelection();
},_initSelection:function(){var ae=this,Z=ae.chart,af=ae.options,X=ae.mainAxis(),Y=r(X),ag=X.range(),ad=ag.min,ac=ag.max,ab=X.options.categories,ah=ae.options.select,ai=ae.selection,aa=Q(ah.from),aj=Q(ah.to);
if(ab.length===0){return;
}if(ai){ai.destroy();
ai.wrapper.remove();
}Y.box=X.box;
ai=ae.selection=new N(Z,Y,{min:ad,max:ac,from:aa,to:aj,selectStart:l.proxy(ae._selectStart,ae),select:l.proxy(ae._select,ae),selectEnd:l.proxy(ae._selectEnd,ae),mousewheel:{zoom:"left"}});
if(af.hint.visible){ae.hint=new J(Z.element,{min:ad,max:ac,template:af.hint.template,format:af.hint.format});
}},_setRange:function(){var ac=this.chart._createPlotArea(true);
var X=ac.namedCategoryAxes[H];
var Y=X.options;
var ad=X.range();
var ab=ad.min;
var aa=m(ad.max,Y.baseUnitStep,Y.baseUnit);
var ae=this.options.select||{};
var Z=Q(ae.from)||ab;
if(Z<ab){Z=ab;
}var af=Q(ae.to)||aa;
if(af>aa){af=aa;
}this.options.select={from:Z,to:af};
this.filterAxes();
},_redrawSelf:function(Y){var X=this.chart._plotArea;
if(X){X.redraw(C(X.panes),Y);
}},redrawSlaves:function(){var Y=this,X=Y.chart,Z=X._plotArea,aa=Z.panes.slice(0,-1);
Z.srcSeries=X.options.series;
Z.redraw(aa);
},_drag:function(ab){var ae=this,Y=ae.chart,Z=Y._eventCoordinates(ab.originalEvent),af=ae.mainAxis(),ag=af.datesRange(),ad=af.pane.box.containsPoint(Z),X=Y._plotArea.categoryAxis,ah=ab.axisRanges[X.options.name],ai=ae.options.select,aj=ae.selection,aa,ac,ak;
if(!ah||ad||!aj){return;
}if(ai.from&&ai.to){aa=R(ai.to)-R(ai.from);
}else{aa=R(aj.options.to)-R(aj.options.from);
}ac=Q(D(R(ah.min),ag.min,R(ag.max)-aa));
ak=Q(D(R(ac)+aa,R(ag.min)+aa,ag.max));
ae.options.select={from:ac,to:ak};
if(ae._liveDrag()){ae.filterAxes();
ae.redrawSlaves();
}aj.set(ac,ak);
ae.showHint(ac,ak);
},_dragEnd:function(){var X=this;
X.filterAxes();
X.filterDataSource();
X.redrawSlaves();
if(X.hint){X.hint.hide();
}},_liveDrag:function(){var ab=B.support,aa=ab.touch,X=ab.browser,Y=X.mozilla,Z=X.msie&&X.version<9;
return !aa&&!Y&&!Z;
},readSelection:function(){var Y=this,Z=Y.selection,aa=Z.options,X=Y.options.select;
X.from=aa.from;
X.to=aa.to;
},filterAxes:function(){var ac=this,ad=ac.options.select||{},Z=ac.chart,X=Z.options.categoryAxis,aa=ad.from,ae=ad.to,ab,Y;
for(ab=0;
ab<X.length;
ab++){Y=X[ab];
if(Y.pane!==I){Y.min=Q(aa);
Y.max=Q(ae);
}}},filterDataSource:function(){var ab=this,ac=ab.options.select||{},Y=ab.chart,Z=Y.dataSource,aa=Z&&Z.options.serverFiltering,X;
if(ab.dataSource&&aa){X=new u.DateCategoryAxis(v({baseUnit:"fit"},Y.options.categoryAxis[0],{categories:[ac.from,ac.to]})).options;
Z.filter(G.buildFilter(m(X.min,-X.baseUnitStep,X.baseUnit),m(X.max,X.baseUnitStep,X.baseUnit)));
}},_zoom:function(ab){var ad=this,Z=ad.chart,aa=ab.delta,X=Z._plotArea.categoryAxis,ae=ad.options.select,af=ad.selection,Y=ad.mainAxis().options.categories,ac,ag;
if(!af){return;
}ac=E(af.options.from,Y);
ag=E(af.options.to,Y);
ab.originalEvent.preventDefault();
if(F.abs(aa)>1){aa*=V;
}if(ag-ac>1){af.expand(aa);
ad.readSelection();
}else{X.options.min=ae.from;
ae.from=X.scaleRange(-ab.delta).min;
}if(!B.support.touch){ad.filterAxes();
ad.redrawSlaves();
}af.set(ae.from,ae.to);
ad.showHint(ad.options.select.from,ad.options.select.to);
},_zoomEnd:function(X){this._dragEnd(X);
},showHint:function(Y,ab){var Z=this,X=Z.chart,aa=X._plotArea;
if(Z.hint){Z.hint.show(Y,ab,aa.backgroundBox());
}},_selectStart:function(Y){var X=this.chart;
X._selectStart.call(X,Y);
},_select:function(Y){var Z=this,X=Z.chart;
Z.showHint(Y.from,Y.to);
X._select.call(X,Y);
},_selectEnd:function(Y){var Z=this,X=Z.chart;
if(Z.hint){Z.hint.hide();
}Z.readSelection();
Z.filterAxes();
Z.filterDataSource();
Z.redrawSlaves();
X._selectEnd.call(X,Y);
},mainAxis:function(){var X=this.chart._plotArea;
if(X){return X.namedCategoryAxes[H];
}}});
G.setup=function(Y,ab){Y=Y||{};
ab=ab||{};
var X=v({},ab.navigator,Y.navigator),aa=Y.panes=[].concat(Y.panes),Z=v({},X.pane,{name:I});
if(!X.visible){Z.visible=false;
Z.height=0.1;
}aa.push(Z);
G.attachAxes(Y,X);
G.attachSeries(Y,X,ab);
};
G.attachAxes=function(ac,ab){var Y,af,ad=ab.series||[];
Y=ac.categoryAxis=[].concat(ac.categoryAxis);
af=ac.valueAxis=[].concat(ac.valueAxis);
var Z=A(ad,z);
var aa=Z.length===0;
var X=v({type:"date",pane:I,roundToBaseUnit:!aa,justified:aa,_collapse:false,majorTicks:{visible:true},tooltip:{visible:false},labels:{step:1},autoBind:!ab.dataSource,autoBaseUnitSteps:{minutes:[1],hours:[1,2],days:[1,2],weeks:[],months:[1],years:[1]},_overlap:false});
var ae=ab.categoryAxis;
Y.push(v({},X,{maxDateGroups:200},ae,{name:H,baseUnit:"fit",baseUnitStep:"auto",labels:{visible:false},majorTicks:{visible:false}}),v({},X,ae,{name:H+"_labels",maxDateGroups:20,baseUnitStep:"auto",plotBands:[],autoBaseUnitSteps:{minutes:[]}}),v({},X,ae,{name:H+"_ticks",maxDateGroups:200,majorTicks:{width:0.5},plotBands:[],labels:{visible:false,mirror:true}}));
af.push(v({name:H,pane:I,majorGridLines:{visible:false},visible:false},ab.valueAxis));
};
G.attachSeries=function(ab,aa,ae){var ac=ab.series=ab.series||[],Z=[].concat(aa.series||[]),ad=ae.seriesColors,X=aa.seriesDefaults,Y;
for(Y=0;
Y<Z.length;
Y++){ac.push(v({color:ad[Y%ad.length],categoryField:aa.dateField,visibleInLegend:false,tooltip:{visible:false}},X,Z[Y],{axis:H,categoryAxis:H,autoBind:!aa.dataSource}));
}};
G.buildFilter=function(X,Y){return[{field:"Date",operator:"gte",value:Q(X)},{field:"Date",operator:"lt",value:Q(Y)}];
};
var J=q.extend({init:function(X,Z){var Y=this;
Y.options=v({},Y.options,Z);
Y.container=X;
Y.chartPadding={top:parseInt(X.css("paddingTop"),10),left:parseInt(X.css("paddingLeft"),10)};
Y.template=Y.template;
if(!Y.template){Y.template=Y.template=M("<div class='"+t+"navigator-hint' style='display: none; position: absolute; top: 1px; left: 1px;'><div class='"+t+"tooltip "+t+"chart-tooltip'>&nbsp;</div><div class='"+t+"scroll' /></div>");
}Y.element=l(Y.template()).appendTo(X);
},options:{format:"{0:d} - {1:d}",hideDelay:500},show:function(Y,am,X){var Z=this,ac=Q(R(Y)+R(am-Y)/2),af=Z.options,al=B.format(Z.options.format,Y,am),an=Z.element.find("."+t+"tooltip"),aj=Z.element.find("."+t+"scroll"),ak=X.width()*0.4,ad=X.center().x-ak,ab=X.center().x,ag=ab-ad,ah=af.max-af.min,ai=ag/ah,ae=ac-af.min,aa;
if(Z._hideTimeout){clearTimeout(Z._hideTimeout);
}if(!Z._visible){Z.element.stop(false,true).css("visibility","hidden").show();
Z._visible=true;
}if(af.template){aa=P(af.template);
al=aa({from:Y,to:am});
}an.html(al).css({left:X.center().x-an.outerWidth()/2,top:X.y1});
aj.css({width:ak,left:ad+ae*ai,top:X.y1+parseInt(an.css("margin-top"),10)+parseInt(an.css("border-top-width"),10)+an.height()/2});
Z.element.css("visibility","visible");
},hide:function(){var X=this;
if(X._hideTimeout){clearTimeout(X._hideTimeout);
}X._hideTimeout=setTimeout(function(){X._visible=false;
X.element.fadeOut("slow");
},X.options.hideDelay);
}});
function s(){}function r(X){s.prototype=X;
return new s();
}u.ui.plugin(O);
v(u,{Navigator:G});
}(window.kendo.jQuery));
return window.kendo;
},typeof define=="function"&&define.amd?define:function(k,l,m){(m||l)();
}));
window.$=h;
window.jQuery=i;
})($telerik.$,$telerik.$);
window.$=c;
window.jQuery=d;
})($telerik.$,$telerik.$);
