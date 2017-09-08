Type.registerNamespace("Telerik.Web.UI");
Telerik.Web.UI.TreeListItemDrag=function(){Telerik.Web.UI.TreeListItemDrag.initializeBase(this);
this._owner=null;
this._draggedItems=[];
this._draggedContainer=null;
this._dropClue=null;
this._originalDragItem=null;
this._isDragging=false;
this._dropTargetDataCache=[];
this._mouseDownDelegate=null;
this._documentMouseMoveDelegate=null;
this._documentMouseUpDelegate=null;
};
Telerik.Web.UI.TreeListItemDrag.prototype={initialize:function(){Telerik.Web.UI.TreeListItemDrag.callBaseMethod(this,"initialize");
if(this._owner){var a=this._owner.get_element();
this._mouseDownDelegate=$telerik.addMobileHandler(this,a,"mousedown",this._mouseDown);
}},dispose:function(){this._cleanUp();
Telerik.Web.UI.TreeListItemDrag.callBaseMethod(this,"dispose");
},_mouseDown:function(b){var f=$telerik.isTouchDevice?f=$telerik.getTouchTarget(b):(b.target||b.srcElement);
var c=!!f.id&&f.id.indexOf("RowDragHandle")>-1&&!!this._owner._data._useDragDropColumn;
var a=this._owner._canRaiseItemEvent(b);
if(!!this._owner._data._useDragDropColumn){if(!c||b.ctrlKey||b.shiftKey||(b.rawEvent&&b.rawEvent.metaKey)){return;
}}else{if(!a||b.ctrlKey||b.shiftKey||(b.rawEvent&&b.rawEvent.metaKey)){return;
}}var d=this._owner.getTargetItem(b);
if(!d){return;
}this._originalDragItem=d;
if(!this._shouldInitializeDrag(d)){return;
}this._attachDragHandlers();
Telerik.Web.UI.RadTreeList.ClearDocumentEvents();
if(!$telerik.isTouchDevice){return false;
}},_shouldInitializeDrag:function(a){return a.get_selected()||(this._owner._selection&&this._owner._clientSettings._selecting&&!this._owner._clientSettings._selecting._useSelectColumnOnly);
},_createDraggedContainer:function(d){if(!d.length){return;
}this._draggedContainer=document.createElement("div");
var a=d[0].get_element().parentNode.parentNode.getElementsByTagName("colgroup")[0];
var b=a?a.innerHTML:"";
var g=[];
for(var c=0,e=d.length;
c<e;
c++){var f=d[c].get_element();
g[g.length]=String.format("<tr class='{0}'>",f.className)+f.innerHTML+"</tr>";
this._draggedItems[this._draggedItems.length]=d[c];
}this._draggedContainer.innerHTML=String.format("<div style='overflow:hidden;'><table class='{0}'><colgroup>{1}</colgroup><tbody>{2}</tbody></table></div>",d[0].get_element().parentNode.parentNode.className,b,g.join(""));
this._copyAttributes(this._draggedContainer,this._owner.get_element());
this._copyAttributes(this._draggedContainer.getElementsByTagName("table")[0],d[0].get_element().parentNode.parentNode);
this._draggedContainer.id=this._owner.get_clientID()+"_DraggedContainer";
this._draggedContainer.style.position="absolute";
this._draggedContainer.className=String.format("{0} TreeListDraggedRows_{1}",this._owner.get_element().className,this._getSkin());
this._draggedContainer.style.zIndex=99999;
this._draggedContainer.style.display="none";
this._draggedContainer.style.width=this._owner.get_element().offsetWidth-2+"px";
if(this._owner.get_enableAriaSupport()){this._draggedContainer.setAttribute("aria-grabbed","true");
}this._originalCursor=document.body.style.cursor;
document.body.style.cursor="default";
document.body.appendChild(this._draggedContainer);
this._removeServiceCells(this._draggedContainer);
this._createDropClue();
},_removeServiceCells:function(g){g.style.marginLeft=-10000+"px";
g.style.display="";
var b=[];
var c=[];
var m=0;
var f=[];
var a;
var e=g.getElementsByTagName("colgroup")[0];
if(e){f=e.getElementsByTagName("col");
}var o=g.getElementsByTagName("tr");
for(var k=0;
k<o.length;
k++){var n=o[k];
var h=false;
var q=0;
for(var l=0;
l<n.cells.length&&!h;
l++){a=n.cells[l];
var d=f[l];
if(a.className.indexOf("rtlL")>-1){b.push(a);
if(d&&d.style.width){q+=parseInt(d.style.width,10);
}else{q+=a.offsetWidth;
}}if(a.className.indexOf("rtlCF")>-1){c.push(a);
h=true;
}}m=Math.max(m,q);
}g.style.width=g.offsetWidth-m+"px";
while(b.length){a=b.pop();
a.parentNode.removeChild(a);
}while(c.length){a=c.pop();
a.removeAttribute("colspan");
}if(e){e.parentNode.removeChild(e);
}var p=$get(this._owner.get_id()+"_rtlData");
if(p){this._draggedContainer.getElementsByTagName("div")[0].scrollLeft=p.scrollLeft;
}g.getElementsByTagName("table")[0].style.tableLayout="auto";
g.style.marginLeft="";
},_copyAttributes:function(b,a){if(b.mergeAttributes){b.mergeAttributes(a);
}else{Telerik.Web.UI.RadTreeList.CopyAttributes(b,a);
}},_destroyDraggedContainer:function(){if(this._draggedContainer){this._destroyDropClue();
document.body.style.cursor=this._originalCursor;
this._originalCursor=null;
this._draggedContainer.parentNode.removeChild(this._draggedContainer);
this._draggedContainer=null;
}},_createDropClue:function(){if(this._draggedContainer){this._dropClue=document.createElement("div");
this._dropClue.id=this._owner.get_clientID()+"_DropClue";
this._dropClue.className="rtlDrag";
this._draggedContainer.appendChild(this._dropClue);
}},_destroyDropClue:function(){if(this._dropClue){this._dropClue.parentNode.removeChild(this._dropClue);
this._dropClue=null;
}},_setDropClueVisibility:function(a){if(!this._dropClue){return;
}if(a){this._dropClue.style.visibility="";
}else{this._dropClue.style.visibility="hidden";
}},_getDropTargetData:function(c){var g;
if($telerik.isTouchDevice){g=$telerik.getTouchTarget(c);
}else{g=c.target||c.srcElement;
}if(!g){return null;
}for(var d=0,f=this._dropTargetDataCache.length;
d<f;
d++){var a=this._dropTargetDataCache[d];
if(a&&a.target===g){return a;
}}var b={};
b.target=g;
b.item=this._owner.getContainerItem(g);
b.isOverHeader=!b.item&&this._isOverHeader(c);
b.canDrop=b.isOverHeader;
if(!b.isOverHeader){if(!b.item||(b.item!==this._originalDragItem&&!this._isDraggedItemOrChild(b.item))){b.canDrop=true;
}else{b.item=null;
b.canDrop=false;
}}this._dropTargetDataCache[this._dropTargetDataCache.length]=b;
return b;
},_isDraggedItemOrChild:function(c){var b=function(j,e){var f=j.get_childItems();
for(var g=0,h=f.length;
g<h;
g++){if(f[g]===e||b(f[g],e)){return true;
}}return false;
};
for(var a=0,d=this._draggedItems.length;
a<d;
a++){if(this._draggedItems[a]===c||b(this._draggedItems[a],c)){return true;
}}return false;
},_positionDragElement:function(c,d){var f=13,g=15;
var a,b;
if($telerik.isTouchDevice){var e=$telerik.getTouchEventLocation(d);
a=e.x;
b=e.y;
}else{a=d.clientX;
b=d.clientY;
}c.style.top=b+document.documentElement.scrollTop+g+document.body.scrollTop+1+"px";
c.style.left=a+document.documentElement.scrollLeft+f+document.body.scrollLeft+1+"px";
if($telerik.isOpera){c.style.top=parseInt(c.style.top,10)-document.body.scrollTop+"px";
}},_getMousePosition:function(a){var d=$telerik.getScrollOffset(document.body,true);
var b=a.clientX;
var c=a.clientY;
b+=d.x;
c+=d.y;
return{x:b,y:c};
},_getSkin:function(){var a=this._owner.get_element().className;
var d=a.split("RadTreeList");
for(var b=0,c=d.length;
b<c;
b++){if(d[b].length>0&&d[b][0]==="_"){return d[b].substr(1);
}}return"";
},_attachDragHandlers:function(){this._documentMouseMoveDelegate=$telerik.addMobileHandler(this,document,"mousemove",this._mouseMove,null,true);
this._documentMouseUpDelegate=$telerik.addMobileHandler(this,document,"mouseup",this._mouseUp,null,true);
},_detachDragHandlers:function(){if(this._documentMouseMoveDelegate){$telerik.removeMobileHandler(document,"mousemove",this._documentMouseMoveDelegate,null,true);
this._documentMouseMoveDelegate=null;
}if(this._documentMouseUpDelegate){$telerik.removeMobileHandler(document,"mouseup",this._documentMouseUpDelegate,null,true);
this._documentMouseUpDelegate=null;
}},_mouseMove:function(c){if(this._isDragging){if(this._draggedContainer){this._draggedContainer.style.position="absolute";
this._draggedContainer.style.display="";
this._positionDragElement(this._draggedContainer,c);
var d=this._getDropTargetData(c);
this._setDropClueVisibility(d.canDrop&&(d.item||d.isOverHeader));
if(this._owner.get_events()&&this._owner.get_events()._list.itemDragging){var b=this._owner._buildEventArgs(new Sys.EventArgs(),{draggedContainer:this._draggedContainer,domEvent:c,canDrop:d.canDrop,targetItem:d.item,isOverHeaderItem:d.isOverHeader});
var a=this;
b.set_canDrop=function(e){b._canDrop=d.canDrop=e;
};
b.set_dropClueVisible=function(e){a._setDropClueVisibility(e);
};
this._owner.raise_itemDragging(b);
}}}else{if(!this._originalDragItem.get_selected()&&this._owner._selection){if(this._owner._clientSettings._selecting._allowToggleSelection){this._originalDragItem.set_selected(true);
}else{this._owner._selection.exclusiveSelectItem(this._originalDragItem);
this._owner.updateClientStateIfModified();
}}this._setupDrag(c);
this._isDragging=true;
}return false;
},_setupDrag:function(d){if(!this._originalDragItem){return;
}var c=this._getDraggedItems();
if(!c.length){c[c.length]=this._originalDragItem;
}this._createDraggedContainer(c);
var a=this._owner._buildEventArgs(new Sys.CancelEventArgs(),{item:this._originalDragItem,draggedItems:c,draggedContainer:this._draggedContainer});
a.set_draggedContainer=function(e){a._draggedContainer=e;
};
this._owner.raise_itemDragStarted(a);
if(a.get_cancel()){this._destroyDraggedContainer();
return;
}else{if(a.get_draggedContainer()!==this._draggedContainer){var b=a.get_draggedContainer();
if(b&&b.tagName){var f=document.createElement("div");
f.id=this._draggedContainer.id;
f.style.position="absolute";
f.style.zIndex=99999;
document.body.appendChild(f);
f.appendChild(b);
this._destroyDraggedContainer();
this._draggedContainer=f;
}}}if(this._draggedContainer&&d){this._positionDragElement(this._draggedContainer,d);
}},_getDraggedItems:function(){var a=[];
var d=this._owner.get_selectedIndexes();
var b;
var c;
if(d.length>0){if(d.length==1){return this._owner.get_selectedItems();
}else{var e=[];
for(b=0,c=d.length;
b<c;
b++){e[d[b]]=this._owner.get_dataItems()[d[b]];
}for(b=0,c=e.length;
b<c;
b++){if(e[b]){a[a.length]=e[b];
}}}}return a;
},_isOverHeader:function(a){var c;
if($telerik.isTouchDevice){c=$telerik.getTouchTarget(a);
}else{c=a.target||a.srcElement;
}var b=this._owner.get_element().getElementsByTagName("thead")[0];
return b&&$telerik.isDescendantOrSelf(b,c);
},_mouseUp:function(b){if(this._draggedContainer){var d=this._getDropTargetData(b);
if(d.canDrop){var c={targetDataItem:d.item,destinationHtmlElement:d.target,draggedItems:this._draggedItems,isOverHeader:d.isOverHeader};
var a=this._owner._buildEventArgs(new Sys.CancelEventArgs(),c);
a.set_destinationHtmlElement=function(e){a._destinationHtmlElement=e;
};
this._owner.raise_itemDropping(a);
if(!a.get_cancel()){this._cleanUp();
this._owner.raise_itemDropped(this._owner._buildEventArgs(new Sys.EventArgs(),c));
this._saveEventData(a);
this._owner.fireCommand("ItemDrop",String.format("{0};{1}",a._targetDataItem?a._targetDataItem.get_displayIndex():d.isOverHeader?-1:"",a._destinationHtmlElement?a._destinationHtmlElement.id:""));
return;
}}}this._cleanUp();
},_saveEventData:function(a){this._owner._draggedIndexes=[];
for(var b=0,c=a._draggedItems.length;
b<c;
b++){Array.add(this._owner._draggedIndexes,a._draggedItems[b].get_displayIndex());
}this._owner.updateClientState();
},_cleanUp:function(){this._isDragging=false;
this._detachDragHandlers();
this._destroyDraggedContainer();
this._originalDragItem=null;
this._draggedItems=[];
this._dropTargetDataCache=[];
Telerik.Web.UI.RadTreeList.RestoreDocumentEvents();
},get_owner:function(){return this._owner;
},set_owner:function(a){this._owner=a;
}};
Telerik.Web.UI.TreeListItemDrag.registerClass("Telerik.Web.UI.TreeListItemDrag",Sys.Component);