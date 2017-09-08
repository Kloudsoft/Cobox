Type.registerNamespace("Telerik.Web.UI");
Type.registerNamespace("Telerik.Web.UI.PivotGridDragDropBase");
(function(a,b){a.PivotGridDragDropBase.CreateDragElement=function(d,f,c){var e=d.cloneNode(true);
var g=document.createElement("div");
g.className="rpgDragItem RadPivotGrid RadPivotGrid_"+f;
g.appendChild(e);
if(c){g.setAttribute("aria-grabbed","true");
}var h=g.style;
if(typeof(h.filter)!="undefined"){h.filter="alpha(opacity=80);";
}if(typeof(h.MozOpacity)!="undefined"){h.MozOpacity=0.8;
}if(typeof(h.opacity)!="undefined"){h.opacity=0.8;
}h.position="absolute";
h.zIndex=4000;
document.body.appendChild(g);
return g;
};
a.PivotGridDragDropBase.PositionDragElement=function(e,f){var c=$telerik.isTouchDevice?$telerik.getTouchEventLocation(f).x:f.clientX;
var d=$telerik.isTouchDevice?$telerik.getTouchEventLocation(f).y:f.clientY;
e.style.top=d+$telerik.getDocumentElementScrollTop()+1+"px";
e.style.left=c+$telerik.getDocumentElementScrollLeft()+1+"px";
if($telerik.isOpera){e.style.top=parseInt(e.style.top,10)-document.body.scrollTop+"px";
}};
a.PivotGridDragDropBase.CreateOrderIndicatorPair=function(f,c,e,g){var d=[];
d.push(a.PivotGridDragDropBase.CreateOrderIndicator(f[0],c[0],e,g));
d.push(a.PivotGridDragDropBase.CreateOrderIndicator(f[1],c[1],e,g));
return d;
};
a.PivotGridDragDropBase.DestroyOrderIndicatorPair=function(c){a.PivotGridDragDropBase.DestroyOrderIndicator(c[0]);
a.PivotGridDragDropBase.DestroyOrderIndicator(c[1]);
};
a.PivotGridDragDropBase.MoveOrderIndicatorPair=function(d,i,g,h){var f=i[0];
var k=i[1];
if(f!=null&&k!=null){f.style.visibility="visible";
f.style.display="";
k.style.visibility="visible";
k.style.display="";
var c=a.PivotGrid.GetBounds(d);
var e=[];
var j=[];
if(g){e=["left","x","offsetWidth","width"];
j=["top","y","offsetHeight","height"];
}else{e=["top","y","offsetHeight","height"];
j=["left","x","offsetWidth","width"];
}f.style[e[0]]=c[e[1]]-f[e[2]]+"px";
if(h){f.style[j[0]]=c[j[1]]+"px";
}else{f.style[j[0]]=c[j[1]]+c[j[3]]+"px";
}k.style[e[0]]=c[e[1]]+d[e[2]]+"px";
k.style[j[0]]=f.style[j[0]];
}};
a.PivotGridDragDropBase.HideOrderIndicatorPair=function(c){if(c[0].style.display!="none"){a.PivotGridDragDropBase.HideOrderIndicator(c[0]);
a.PivotGridDragDropBase.HideOrderIndicator(c[1]);
}};
a.PivotGridDragDropBase.CreateOrderIndicator=function(f,c,e,g){var d=document.createElement("span");
if(e==""){d.innerHTML=f;
}else{d.className=String.format("{0} {0}_{1}",c,e);
}d.style.visibility="hidden";
d.style.display="none";
d.style.position="absolute";
d.style.zIndex=g;
document.body.appendChild(d);
return d;
};
a.PivotGridDragDropBase.DestroyOrderIndicator=function(c){if(c!=null){document.body.removeChild(c);
c=null;
}};
a.PivotGridDragDropBase.HideOrderIndicator=function(c){c.style.visibility="hidden";
c.style.display="none";
};
a.PivotGridDragDropBase.GetFieldPlacementData=function(p,g,f,m){if(g.className&&g.className.indexOf("rpgRowsZone")!=-1){if(g.id.endsWith("_DropFieldHereCell")){return{element:g,isFirstElement:true,index:0};
}g=g.parentNode;
}var h=$telerik.getElementsByClassName(g,"rpgFieldItem","span");
for(var j=0;
j<h.length;
j++){if(h[j].style.display=="none"||h[j].className.indexOf("rpgValues")!=-1){h.splice(j--,1);
}}if(h.length==0){return{element:g,isFirstElement:true,index:0};
}var k=h.length-1;
var c=a.PivotGrid.GetBounds(h[k]);
var o=$telerik.getTouchEventLocation(f,"client");
var d=!$telerik.isTouchDevice?$telerik.getDocumentRelativeCursorPosition(f):{top:o.y,left:o.x};
if(m){while(c.y+c.height>d.top){k--;
if(k<0){break;
}c=a.PivotGrid.GetBounds(h[k]);
}}else{var n=d.left;
while(c.x+c.width>n){k--;
if(k<0){break;
}c=a.PivotGrid.GetBounds(h[k]);
}}k+=1;
var l=false;
if(k==0){l=true;
g=h[0];
}else{k--;
g=h[k];
}if(g.className.indexOf("rpgValues")!=-1){if(h.length==k){k--;
}else{if(h.length==1){g=g.parentNode;
l=true;
k=0;
}else{if(k==0){g=h[k+1];
l=true;
}else{g=h[k-1];
}}}}return{element:g,isFirstElement:l,index:k};
};
a.PivotGridDragDropBase.GetZoneIndex=function(g,d,e,c){var f=0;
if(e==null){f==d.get_zoneIndex();
}else{if(g==d.get_zoneType()&&!d.get_isHidden()){if(d.get_zoneIndex()>e.get_zoneIndex()&&!c){f=e.get_nextField().get_zoneIndex();
}else{f=e.get_zoneIndex();
}}else{if(c){f=e.get_zoneIndex()-1;
}else{f=e.get_zoneIndex()+1;
}}}return f;
};
})(Telerik.Web.UI,undefined);