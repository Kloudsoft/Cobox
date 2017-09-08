Type.registerNamespace("Telerik.Web.UI.Scheduler");
(function(a,c,d,b){var l=60000,k=l*60,j=k*24,f=8*k,e=18*k,h=8*k,g=17*k,i;
c.Scheduler.WeekModelBase=function(m){this._owner=m;
this._settings=this._owner.get_weekViewSettings();
this._defaultHeaderDateFormat="d";
this._defaultColumnHeaderDateFormat="ddd, d";
this._contentTable=null;
i=this._owner.get_renderMode()===c.RenderMode.Lite?2:4;
};
c.Scheduler.WeekModelBase.prototype={get_visibleRangeStart:function(){var m=this._owner,n=d.DateHelper.getStartOfWeek(m.get_selectedDate(),m.get_firstDayOfWeek());
if(!m.get_showAllDayRow()){n=b.add(n,this.get_effectiveDayStartTime());
}return n;
},get_visibleRangeEnd:function(){var m=this._owner,n=d.DateHelper.getEndOfWeek(m.get_selectedDate(),m.get_firstDayOfWeek(),this.get_numberOfDays()-1);
if(!m.get_showAllDayRow()){n=b.add(n,this.get_effectiveDayEndTime());
}else{n=b.add(n,j);
}return n;
},get_numberOfDays:function(){var m=this._owner;
return d.DateHelper.getWeekLength(m.get_selectedDate(),m.get_firstDayOfWeek(),m.get_lastDayOfWeek());
},get_maximumNumberOfDays:function(){return 7;
},get_dayStartTime:function(){if(!this._settings){return f;
}var m=this._settings.dayStartTime;
if(m==0||m){return m;
}return f;
},get_dayEndTime:function(){if(!this._settings){return e;
}return this._settings.dayEndTime||e;
},get_workDayStartTime:function(){if(!this._settings){return h;
}var m=this._settings.workDayStartTime;
if(m==0||m){return m;
}return h;
},get_workDayEndTime:function(){if(!this._settings){return g;
}return this._settings.workDayEndTime||g;
},get_effectiveDayStartTime:function(){return this._owner.get_showFullTime()?0:this.get_dayStartTime();
},get_effectiveDayEndTime:function(){var p=this._owner.get_showFullTime()?j:this.get_dayEndTime(),m=p-this.get_effectiveDayStartTime(),n=m/l,q=this._owner.get_minutesPerRow(),o=Math.round(Math.ceil(n/q));
return this.get_effectiveDayStartTime()+(o*q*l);
},get_slotsPerDay:function(){var m=(this.get_effectiveDayEndTime()-this.get_effectiveDayStartTime())/l;
return m/this._owner.get_minutesPerRow();
},get_nextPeriodDate:function(){return b.add(this._owner.get_selectedDate(),(this.get_maximumNumberOfDays()*j));
},get_previousPeriodDate:function(){return b.add(this._owner.get_selectedDate(),-(this.get_maximumNumberOfDays()*j));
},get_headerDateFormat:function(){if(!this._settings){return this._defaultHeaderDateFormat;
}return this._settings.headerDateFormat||this._defaultHeaderDateFormat;
},get_columnHeaderDateFormat:function(){if(!this._settings){return this._defaultColumnHeaderDateFormat;
}return this._settings.columnHeaderDateFormat||this._defaultColumnHeaderDateFormat;
},updateResizingAppointmentSize:function(t,r){var n=r.resizingElement,m=a(n).parents("td").get(0),p=m.offsetHeight,s=t.parentNode.rowIndex-m.parentNode.rowIndex,u,v,o=parseInt(n.style.top,10);
if(r.resizeFromStart){s=Math.max(s,this._getMaxAppointmentGrowth(m)*-1);
v=s*p;
u=v*-1;
if(o){u+=o;
}u+=a(n).outerHeight();
if(u<0){return;
}n.style.top=v+"px";
}else{s=Math.min(s,this._getMaxAppointmentGrowth(m));
u=Math.max(1,(s+1))*p;
var q=parseInt(n.style.paddingBottom,10);
q=isNaN(q)?0:q;
u-=q;
u-=i;
if(o){u-=o;
}}this._owner._setAppointmentElementPosition(n,u);
},_getResizingAppointmentOriginalSize:function(o,n){if(this._getAllowMultiColumnResizing()&&!this._owner._renderingManager){return{elements:this._owner._resizeHelper._getAppointmentOriginalElements(n)};
}var m=a(o);
return{height:m.height(),top:m.get(0).style.top};
},restoreResizingAppointmentSize:function(q){var o=q.originalSize;
if(this._getAllowMultiColumnResizing()){if(this._owner._renderingManager){this._owner._repaintAppointment(q.resizingAppointment);
}else{this._owner._resizeHelper._restoreAppointmentOriginalElements(q);
}}else{var p=o.top,n=o.height;
var m=a(q.resizingElement);
m.get(0).style.top=p;
m.height(n);
}q.resizingElement=null;
q.resizingAppointment=null;
q.originalSize=null;
},_getAllowMultiColumnResizing:function(){return true;
},_getResizingPlaneDelta:function(o,m){var p=a(o).index(),n=a(m).index();
return p-n;
},_getFirstModelRowIndex:function(m){return 0;
},isVisible:function(m){return this._isInsideVisibleRange(m);
},_isInsideVisibleRange:function(m){var r=b.getDate(this.get_visibleRangeStart());
for(var o=0,q=this.get_numberOfDays();
o<q;
o++){var p=b.add(r,j*o),n=b.add(p,this.get_effectiveDayEndTime());
p=b.add(p,this.get_effectiveDayStartTime());
if(m._isInRange(p,n)){return true;
}}return false;
},_startsInsideVisibleRange:function(m){var r=b.getDate(this.get_visibleRangeStart());
for(var o=0,q=this.get_numberOfDays();
o<q;
o++){var p=b.add(r,j*o),n=b.add(p,j),s=b.add(p,this.get_effectiveDayEndTime()),t=b.add(p,this.get_effectiveDayStartTime());
if(m._startsInRange(p,t)||m._startsInRange(s,n)){return false;
}}return true;
},_getContentTable:function(){if(!this._contentTable){this._contentTable=a("div.rsTopWrap table.rsContentTable",this._owner.get_element())[0];
}return this._contentTable;
},_getFirstDayStart:function(){var m=d.DateHelper.getStartOfWeek(this._owner.get_selectedDate(),this._owner.get_firstDayOfWeek());
return new b(b.getDate(m)).add(this.get_effectiveDayStartTime()).toDate();
},_getRowsTotalHeight:function(m){if(m>0){return(m*parseInt(this._owner.get_rowHeight(),10))-i;
}return 0;
},updateDraggingAppointmentSize:function(m,s,n){var r=this._owner,p=s.parentNode.parentNode.rows.length-s.parentNode.rowIndex,o=Math.min(r._draggingAppointmentHeight,this._getRowsTotalHeight(p)),q=Math.max(0,r._draggingSourceRowOffset-n.parentNode.rowIndex);
if(q>0){o-=this._getRowsTotalHeight(q);
}a(m).height(o);
},get_startOfMovedAppointment:function(m,u,s){var q=u.get_isAllDay(),p=s.get_isAllDay(),t=s.get_startTime(),v=u.get_startTime(),r=v,o=q?p:!p,n;
if(o&&t!=0){n=b.subtract(m.get_start(),t);
r=b.add(v,n);
}return r;
},getDurationOfMovedAppointment:function(m,q,r){var p=r.get_isAllDay(),o=q.get_isAllDay(),n=b.subtract(m.get_end(),m.get_start());
if(p&&!o){n=r.get_duration();
}if(o&&!p){n=this._owner.get_minutesPerRow()*this._owner.get_numberOfHoveredRows()*l;
}return n;
},_getSourceCellOffsetOfMovedAppointment:function(m,n){return n.cellIndex-a(m).parents("td")[0].cellIndex;
},_getSourceRowOffsetOfMovedAppointment:function(m,n){return n.parentNode.rowIndex-a(m).parents("tr")[0].rowIndex;
},_getMaxAppointmentGrowth:function(m){return m.parentNode.parentNode.rows.length;
},getDurationOfInsertedAppointment:function(m){if(m.get_isAllDay()){return m.get_duration();
}return m.get_duration()*this._owner.get_numberOfHoveredRows();
},_processRowSelection:function(u,v){var t=this._owner._rowSelectionState,m=t.rowSelectionEndSlot,p=this.getTimeSlotFromDomElement(u),s=t.rowSelectionStartSlot.get_rawIndex(),q=p.get_rawIndex();
if((q.viewPartIndex!=s.viewPartIndex)||(q.modelIndex!=s.modelIndex)){return;
}var n=(m)?true:false,r=true;
if(n){var o=m.get_rawIndex();
r=((q.cellIndex==o.cellIndex)&&(q.rowIndex==o.rowIndex)&&(q.viewPartIndex==o.viewPartIndex))?true:false;
}if(!(n&&r)){t.rowSelectionEndSlot=p;
v.apply(this._owner);
}},_getHiddenAppointmentIndicators:function(m,x,o,v){var w=this.get_numberOfDays(),r=this._getFirstDayStart(),p=b.add(b.getDate(r),this.get_effectiveDayEndTime());
for(var q=0;
q<w;
q++){var s=m.getAppointmentsInRange(b.getDate(r),r),t=s.find(function(y){return y.get_end()<=r;
})!=null,u=m.getAppointmentsStartingInRange(p,(new Date(p)).setHours(24)).get_count()>0,n=v*w+q;
x[n]=t+0;
o[n]=u+0;
r=b.add(r,j);
p=b.add(p,j);
}},_getFirstTimeSlot:function(){var m=this._getContentTable().rows[0].cells[0];
return this.getTimeSlotFromDomElement(m);
},getTimeSlotsBetween:function(m,p){if(!(m&&p)){return[];
}var n=m.get_rawIndex(),q=p.get_rawIndex();
if(n.viewPartIndex!=q.viewPartIndex){return[];
}if((n.cellIndex==q.cellIndex)&&(n.rowIndex==q.rowIndex)){return[m];
}if(!this._areTimeSlotsInAscendingOrder(m,p)){var v=m;
m=p;
p=v;
n=m.get_rawIndex();
q=p.get_rawIndex();
}var t=[m],r=(n.viewPartIndex==0)?1:this.get_slotsPerDay(),u;
if((q.cellIndex-n.cellIndex)!=0){u=((q.cellIndex-n.cellIndex)-1)*r+(r-(n.rowIndex+1))+(q.rowIndex+1);
}else{u=q.rowIndex-n.rowIndex;
}var s=m;
for(var o=0;
o<u;
o++){s=this._getNextTimeSlot(s);
if(s){Array.add(t,s);
}}return t;
},get_supportsFullTime:function(){return true;
}};
c.Scheduler.WeekModelBase.registerClass("Telerik.Web.UI.Scheduler.WeekModelBase",null,c.ISchedulerModel);
c.Scheduler.WeekModel=function(m){c.Scheduler.WeekModel.initializeBase(this,[m]);
if(d.Rendering.BlockCollection){this._blockCollection=new d.Rendering.BlockCollection();
}if(d.Rendering.HorizontalBlockCollection){this._allDayBlocks=new d.Rendering.HorizontalBlockCollection();
}};
c.Scheduler.WeekModel.prototype={initialize:function(){},getTimeSlotFromDomElement:function(m){var n=this._getRawIndexFromDomElement(m),o=this._getTimeFromIndex(n);
return this._createTimeSlot(n,o,m);
},_getTimeFromDomElement:function(m){var n=this._getRawIndexFromDomElement(m);
return this._getTimeFromIndex(n);
},_getTimeFromIndex:function(m){var n;
if(m.viewPartIndex==0){n=d.DateHelper.getStartOfWeek(this._owner.get_selectedDate(),this._owner.get_firstDayOfWeek());
}else{n=this._getFirstDayStart();
}var o=m.rowIndex*this._owner.get_minutesPerRow();
return new b(n).add(m.cellIndex*j).add(o*l).toDate();
},_getRawIndexFromDomElement:function(n){while(n&&n.tagName.toUpperCase()!="TD"&&n.tagName.toUpperCase()!="TH"){n=n.parentNode;
}if(n){var m=n.cellIndex,p=n.parentNode,q=p.rowIndex,o=Sys.UI.DomElement.containsCssClass(p,"rsAllDayRow"),r=o?0:1;
return{cellIndex:m,rowIndex:q,viewPartIndex:r};
}return null;
},getTimeSlotForAppointment:function(m){var o=m._isAllDay()&&this._owner.get_showAllDayRow(),r=o?0:1,n;
if(o){n=this._getAllDayTimeSlotIndices(m.get_start());
}else{n=this._getRegularTimeSlotIndices(m.get_start());
}n.viewPartIndex=r;
var q=this._getTimeSlotDomElement(r,n.rowIndex,n.cellIndex),p=this._getTimeFromIndex(n);
return this._createTimeSlot(n,p,q);
},_createTimeSlot:function(p,q,n){var o=60*24,m=(p.viewPartIndex==0)?o:this._owner.get_minutesPerRow();
return new c.Scheduler.WeekTimeSlot(p,q,m,n);
},_getAllDayTimeSlotIndices:function(p){var q=this.get_visibleRangeStart(),m=new b(p).subtract(q),n=Math.max(0,Math.round(m/j)),o=0;
return{rowIndex:o,cellIndex:n};
},_getAllDayTimeSlotForAppointmentPart:function(m){var o=this._getAllDayTimeSlotIndices(m.start),n;
o.viewPartIndex=0;
n=this._getTimeSlotDomElement(o.viewPartIndex,o.rowIndex,o.cellIndex);
return this._createTimeSlot(o,m.start,n);
},_getTimeSlotForAppointmentPart:function(m){var o=this._getRegularTimeSlotIndices(m.start),p=this._getTimeFromIndex(o),n=this._getTimeSlotDomElement(1,o.rowIndex,o.cellIndex);
return this._createTimeSlot(o,p,n);
},_getRegularTimeSlotIndices:function(q){var r=this._getFirstDayStart(),m=new b(q).subtract(r),n=Math.max(0,Math.floor(m/j)),o=m-(n*j),p=Math.max(0,Math.floor(o/(this._owner.get_minutesPerRow()*l)));
return{rowIndex:p,cellIndex:n};
},_getTimeSlotDomElement:function(p,o,m){var n=this._owner.get_element();
if(p==0){return a("div.rsTopWrap .rsAllDayRow",n).children()[m];
}return this._getContentTable().tBodies[0].rows[o].cells[m];
},removeFromBlock:function(m){this._allDayBlocks.remove(m);
this._blockCollection.remove(m);
},addToBlocks:function(m){if(m.isAllDay){this._allDayBlocks.add(m);
}else{this._blockCollection.add(m);
}},resetBlocks:function(){if(d.Rendering.BlockCollection){this._blockCollection=new d.Rendering.BlockCollection();
}if(d.Rendering.HorizontalBlockCollection){this._allDayBlocks=new d.Rendering.HorizontalBlockCollection();
}},_getRenderer:function(){if(!this._renderer){this._renderer=new c.Scheduler.Rendering.WeekViewRenderer(this);
}return this._renderer;
},get_supportsSlotSelection:function(){return true;
},_areTimeSlotsInAscendingOrder:function(m,o){var n=m.get_rawIndex(),p=o.get_rawIndex();
return !((p.cellIndex<n.cellIndex)||(p.cellIndex==n.cellIndex&&p.rowIndex<n.rowIndex));
},_getNextTimeSlot:function(r){var o=this.get_slotsPerDay()-1,n=this.get_numberOfDays()-1,m=r.get_rawIndex(),p={},s,q;
p.viewPartIndex=m.viewPartIndex;
if(m.viewPartIndex==0){o=0;
}if(m.rowIndex==o){if(m.cellIndex==n){return null;
}p.rowIndex=0;
p.cellIndex=m.cellIndex+1;
}else{p.rowIndex=m.rowIndex+1;
p.cellIndex=m.cellIndex;
}s=this._getTimeSlotDomElement(p.viewPartIndex,p.rowIndex,p.cellIndex);
q=this._getTimeFromIndex(p);
return this._createTimeSlot(p,q,s);
}};
c.Scheduler.WeekModel.registerClass("Telerik.Web.UI.Scheduler.WeekModel",c.Scheduler.WeekModelBase);
c.Scheduler.WeekTimeSlot=function(o,p,n,m){this._rawIndex=o;
this._startTime=p;
this._durationInMinutes=n;
this._domElement=m;
this._selected=false;
};
c.Scheduler.WeekTimeSlot.prototype={get_index:function(){var m=this.get_rawIndex();
return String.format("{0}:{1}:{2}",m.viewPartIndex,m.rowIndex,m.cellIndex);
},get_rawIndex:function(){return this._rawIndex;
},get_startTime:function(){return this._startTime;
},get_endTime:function(){return b.add(this.get_startTime(),this.get_duration());
},get_duration:function(){return this.get_durationInMinutes()*l;
},get_durationInMinutes:function(){return this._durationInMinutes;
},get_isAllDay:function(){return this.get_rawIndex().viewPartIndex==0;
},get_domElement:function(){return this._domElement;
},set_selected:function(n){var m=this.get_domElement();
if(m){a(m).toggleClass("rsSelectedSlot",n);
}this._selected=n;
},get_selected:function(){return this._selected;
}};
c.Scheduler.WeekTimeSlot.registerClass("Telerik.Web.UI.Scheduler.WeekTimeSlot",null,c.ISchedulerTimeSlot);
})($telerik.$,Telerik.Web.UI,Telerik.Web.UI.Scheduler,Telerik.Web.UI.Scheduler.DateTime);