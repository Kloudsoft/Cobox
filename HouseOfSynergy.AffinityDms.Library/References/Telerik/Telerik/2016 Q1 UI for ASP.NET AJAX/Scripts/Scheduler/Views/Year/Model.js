(function(a){Type.registerNamespace("Telerik.Web.UI.Scheduler.Views.Year");
var c=Telerik.Web.UI;
var d=c.Scheduler;
var b=d.DateTime;
var e=d.Views.Year;
var h=60000;
var g=h*60;
var f=g*24;
var i=f*7;
e.ModelBase=function(j){this._owner=j;
this._settings=this._owner.get_yearViewSettings();
};
e.ModelBase.prototype={get_visibleRangeStart:function(){var j=this._owner.get_selectedDate();
return d.DateHelper.getFirstDayOfYear(j);
},get_visibleRangeEnd:function(){var j=new Date(this._owner.get_selectedDate());
j.setFullYear(j.getFullYear()+1);
return d.DateHelper.getFirstDayOfYear(j);
},get_nextPeriodDate:function(){var j=this._owner.get_selectedDate();
return new Date(j.getFullYear()+1,j.getMonth(),j.getDate());
},get_previousPeriodDate:function(){var j=this._owner.get_selectedDate();
return new Date(j.getFullYear()-1,j.getMonth(),j.getDate());
},get_headerDateFormat:function(){return this._settings.headerDateFormat||"yyyy";
},get_dayHeaderDateFormat:function(){return this._settings.dayHeaderDateFormat||"dd";
},get_monthHeaderDateFormat:function(){return this._settings.monthHeaderDateFormat||"MMMM";
},get_showMonthHeaders:function(){return this._settings.showMonthHeaders||true;
},get_weekLength:function(){var j=this._owner;
return d.DateHelper.getWeekLength(j.get_selectedDate(),j.get_firstDayOfWeek(),j.get_lastDayOfWeek());
},isVisible:function(j){return j._isInRange(this.get_visibleRangeStart(),this.get_visibleRangeEnd());
},get_visibleAppointmentsPerDay:function(){return 1;
},_getRawIndexFromStartTime:function(n){var l=d.DateHelper.getFirstDayOfMonth(n);
var o=d.DateHelper.getStartOfWeek(l,this._owner.get_firstDayOfWeek());
var j=b.subtract(n,o);
var m=Math.floor(j/i);
var p=j-(m*i);
var k=Math.floor(p/f);
return{monthIndex:n.getMonth(),dayIndex:(this.get_weekLength()*m)+k};
},_getStartTimeFromRawIndex:function(m){var l=new Date(this.get_visibleRangeStart().setMonth(m.monthIndex));
var p=d.DateHelper.getStartOfWeek(l,this._owner.get_firstDayOfWeek());
var o=this.get_weekLength();
var k=7;
var n=Math.floor(m.dayIndex/o);
var j=n*k+(m.dayIndex%o);
return b.add(p,j*f);
},_getContentElement:function(){if(!this._contentElement){this._contentElement=a(this._owner.get_element()).find("div.rsTopWrap .rsYearMonthsWrap")[0];
}return this._contentElement;
}};
e.ModelBase.registerClass("Telerik.Web.UI.Scheduler.Views.Year.ModelBase",null,Telerik.Web.UI.ISchedulerModel);
e.Model=function(k,j){e.Model.initializeBase(this,[k]);
};
e.Model.prototype={initialize:function(){},_getRenderer:function(){if(!this._renderer){this._renderer=new c.Scheduler.Rendering.YearViewRenderer(this);
}return this._renderer;
},getTimeSlotForAppointment:function(j){return this._getTimeSlotFromStartTime(j.get_start());
},_getTimeSlotFromStartTime:function(l){var k=this._getRawIndexFromStartTime(l);
var j=this._getTimeSlotDomElement(k);
return new e.TimeSlot(k,l,j);
},getTimeSlotFromDomElement:function(j){var k=this._getRawIndexFromDomElement(j);
var l=this._getStartTimeFromRawIndex(k);
return new e.TimeSlot(k,l,j);
},_getRawIndexFromDomElement:function(l){while(l&&(l.tagName.toUpperCase()!="TD")){l=l.parentNode;
}if(l){var n=l.parentNode;
var k=n.cells.length;
var j=l.cellIndex;
var o=a(n).index();
var m=a(l).parents(".rsYearMonthWrap").index();
return{monthIndex:m,dayIndex:(k*o)+j};
}return null;
},_getTimeSlotDomElement:function(j){return a(this._getContentElement()).find(".rsYearMonthWrap").eq(j.monthIndex).find("td").eq(j.dayIndex)[0];
}};
e.Model.registerClass("Telerik.Web.UI.Scheduler.Views.Year.Model",e.ModelBase);
e.TimeSlot=function(k,l,j){this._rawIndex=k;
this._startTime=l;
this._domElement=j;
};
e.TimeSlot.prototype={get_index:function(){var j=this.get_rawIndex();
return String.format("{0}:{1}",j.monthIndex,j.dayIndex);
},get_rawIndex:function(){return this._rawIndex;
},get_isAllDay:function(){return true;
},get_startTime:function(){return this._startTime;
},get_endTime:function(){return b.add(this.get_startTime(),this.get_duration());
},get_duration:function(){return this.get_durationInMinutes()*h;
},get_durationInMinutes:function(){return 1440;
},get_domElement:function(){return this._domElement;
}};
e.TimeSlot.registerClass("Telerik.Web.UI.Scheduler.Views.Year.TimeSlot",null,c.ISchedulerTimeSlot);
})($telerik.$);
