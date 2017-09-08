(function(b,a,e){var d=window.jQuery,c=window.$;
window.$=window.jQuery=a;
(function(g,f,j){var i=window.jQuery,h=window.$;
window.$=window.jQuery=f;
(function(l,k){k("kendo.sortable",["kendo.draganddrop"],l);
}(function(){var k={id:"sortable",name:"Sortable",category:"framework",depends:["draganddrop"]};
(function(l,C){var x=window.kendo,D=x.ui.Widget,B="start",p="beforeMove",z="move",w="end",r="change",q="cancel",o="sort",n="remove",m="receive",t=">*",y=-1;
function s(G,E){try{return l.contains(G,E)||G==E;
}catch(F){return false;
}}function u(E){return E.clone();
}function v(E){return E.clone().removeAttr("id").css("visibility","hidden");
}var A=D.extend({init:function(E,F){var G=this;
D.fn.init.call(G,E,F);
if(!G.options.placeholder){G.options.placeholder=v;
}if(!G.options.hint){G.options.hint=u;
}G.draggable=G._createDraggable();
},events:[B,p,z,w,r,q],options:{name:"Sortable",hint:null,placeholder:null,filter:t,holdToDrag:false,disabled:null,container:null,connectWith:null,handler:null,cursorOffset:null,axis:null,ignore:null,autoScroll:false,cursor:"auto",moveOnDragEnter:false},destroy:function(){this.draggable.destroy();
D.fn.destroy.call(this);
},_createDraggable:function(){var G=this,E=G.element,F=G.options;
return new x.ui.Draggable(E,{filter:F.filter,hint:x.isFunction(F.hint)?F.hint:l(F.hint),holdToDrag:F.holdToDrag,container:F.container?l(F.container):null,cursorOffset:F.cursorOffset,axis:F.axis,ignore:F.ignore,autoScroll:F.autoScroll,dragstart:l.proxy(G._dragstart,G),dragcancel:l.proxy(G._dragcancel,G),drag:l.proxy(G._drag,G),dragend:l.proxy(G._dragend,G)});
},_dragstart:function(H){var G=this.draggedElement=H.currentTarget,F=this.options.disabled,I=this.options.handler,E=this.options.placeholder,J=this.placeholder=x.isFunction(E)?l(E.call(this,G)):l(E);
if(F&&G.is(F)){H.preventDefault();
}else{if(I&&!l(H.initialTarget).is(I)){H.preventDefault();
}else{if(this.trigger(B,{item:G,draggableEvent:H})){H.preventDefault();
}else{G.css("display","none");
G.before(J);
this._setCursor();
}}}},_dragcancel:function(){this._cancel();
this.trigger(q,{item:this.draggedElement});
this._resetCursor();
},_drag:function(J){var I=this.draggedElement,P=this._findTarget(J),Q,G={left:J.x.location,top:J.y.location},N,F={x:J.x.delta,y:J.y.delta},H,O,L,E=this.options.axis,M=this.options.moveOnDragEnter,K={item:I,list:this,draggableEvent:J};
if(E==="x"||E==="y"){this._movementByAxis(E,G,F[E],K);
return;
}if(P){Q=this._getElementCenter(P.element);
N={left:Math.round(G.left-Q.left),top:Math.round(G.top-Q.top)};
l.extend(K,{target:P.element});
if(P.appendToBottom){this._movePlaceholder(P,null,K);
return;
}if(P.appendAfterHidden){this._movePlaceholder(P,"next",K);
}if(this._isFloating(P.element)){if(F.x<0&&(M||N.left<0)){H="prev";
}else{if(F.x>0&&(M||N.left>0)){H="next";
}}}else{if(F.y<0&&(M||N.top<0)){H="prev";
}else{if(F.y>0&&(M||N.top>0)){H="next";
}}}if(H){L=H==="prev"?g.fn.prev:g.fn.next;
O=L.call(P.element);
while(O.length&&!O.is(":visible")){O=L.call(O);
}if(O[0]!=this.placeholder[0]){this._movePlaceholder(P,H,K);
}}}},_dragend:function(J){var M=this.placeholder,H=this.draggedElement,I=this.indexOf(H),N=this.indexOf(M),G=this.options.connectWith,E,L,K,F;
this._resetCursor();
K={action:o,item:H,oldIndex:I,newIndex:N,draggableEvent:J};
if(N>=0){L=this.trigger(w,K);
}else{E=M.parents(G).getKendoSortable();
K.action=n;
F=l.extend({},K,{action:m,oldIndex:y,newIndex:E.indexOf(M)});
L=!(!this.trigger(w,K)&&!E.trigger(w,F));
}if(L||N===I){this._cancel();
return;
}M.replaceWith(H);
H.show();
this.draggable.dropped=true;
K={action:this.indexOf(H)!=y?o:n,item:H,oldIndex:I,newIndex:this.indexOf(H),draggableEvent:J};
this.trigger(r,K);
if(E){F=l.extend({},K,{action:m,oldIndex:y,newIndex:E.indexOf(H)});
E.trigger(r,F);
}},_findTarget:function(F){var G=this._findElementUnderCursor(F),H,E=this.options.connectWith,I;
if(l.contains(this.element[0],G)){H=this.items();
I=H.filter(G)[0]||H.has(G)[0];
return I?{element:l(I),sortable:this}:null;
}else{if(this.element[0]==G&&this._isEmpty()){return{element:this.element,sortable:this,appendToBottom:true};
}else{if(this.element[0]==G&&this._isLastHidden()){I=this.items().eq(0);
return{element:I,sortable:this,appendAfterHidden:true};
}else{if(E){return this._searchConnectedTargets(G,F);
}}}}},_findElementUnderCursor:function(F){var G=x.elementUnderCursor(F),E=F.sender;
if(s(E.hint[0],G)){E.hint.hide();
G=x.elementUnderCursor(F);
if(!G){G=x.elementUnderCursor(F);
}E.hint.show();
}return G;
},_searchConnectedTargets:function(G,F){var E=l(this.options.connectWith),K,I,J;
for(var H=0;
H<E.length;
H++){K=E.eq(H).getKendoSortable();
if(l.contains(E[H],G)){if(K){I=K.items();
J=I.filter(G)[0]||I.has(G)[0];
if(J){K.placeholder=this.placeholder;
return{element:l(J),sortable:K};
}else{return null;
}}}else{if(E[H]==G){if(K&&K._isEmpty()){return{element:E.eq(H),sortable:K,appendToBottom:true};
}else{if(this._isCursorAfterLast(K,F)){J=K.items().last();
return{element:J,sortable:K};
}}}}}},_isCursorAfterLast:function(J,G){var H=J.items().last(),E={left:G.x.location,top:G.y.location},I,F;
I=x.getOffset(H);
I.top+=H.outerHeight();
I.left+=H.outerWidth();
if(this._isFloating(H)){F=I.left-E.left;
}else{F=I.top-E.top;
}return F<0?true:false;
},_movementByAxis:function(E,F,H,I){var G=E==="x"?F.left:F.top,J=H<0?this.placeholder.prev():this.placeholder.next(),K;
if(J.length&&!J.is(":visible")){J=H<0?J.prev():J.next();
}l.extend(I,{target:J});
K=this._getElementCenter(J);
if(K){K=E==="x"?K.left:K.top;
}if(J.length&&H<0&&G-K<0){this._movePlaceholder({element:J,sortable:this},"prev",I);
}else{if(J.length&&H>0&&G-K>0){this._movePlaceholder({element:J,sortable:this},"next",I);
}}},_movePlaceholder:function(H,E,F){var G=this.placeholder;
if(!H.sortable.trigger(p,F)){if(!E){H.element.append(G);
}else{if(E==="prev"){H.element.before(G);
}else{if(E==="next"){H.element.after(G);
}}}H.sortable.trigger(z,F);
}},_setCursor:function(){var F=this.options.cursor,E;
if(F&&F!=="auto"){E=l(document.body);
this._originalCursorType=E.css("cursor");
E.css({cursor:F});
if(!this._cursorStylesheet){this._cursorStylesheet=l("<style>* { cursor: "+F+" !important; }</style>");
}this._cursorStylesheet.appendTo(E);
}},_resetCursor:function(){if(this._originalCursorType){l(document.body).css("cursor",this._originalCursorType);
this._originalCursorType=null;
this._cursorStylesheet.remove();
}},_getElementCenter:function(F){var E=F.length?x.getOffset(F):null;
if(E){E.top+=F.outerHeight()/2;
E.left+=F.outerWidth()/2;
}return E;
},_isFloating:function(E){return/left|right/.test(E.css("float"))||/inline|table-cell/.test(E.css("display"));
},_cancel:function(){this.draggedElement.show();
this.placeholder.remove();
},_items:function(){var E=this.options.filter,F;
if(E){F=this.element.find(E);
}else{F=this.element.children();
}return F;
},indexOf:function(F){var G=this._items(),H=this.placeholder,E=this.draggedElement;
if(H&&F[0]==H[0]){return G.not(E).index(F);
}else{return G.not(H).index(F);
}},items:function(){var F=this.placeholder,E=this._items();
if(F){E=E.not(F);
}return E;
},_isEmpty:function(){return !this.items().length;
},_isLastHidden:function(){return this.items().length===1&&this.items().is(":hidden");
}});
x.ui.plugin(A);
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
