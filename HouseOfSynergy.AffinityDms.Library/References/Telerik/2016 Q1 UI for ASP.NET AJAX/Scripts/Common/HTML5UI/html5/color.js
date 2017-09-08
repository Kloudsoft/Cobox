(function(b,a,e){var d=window.jQuery,c=window.$;
window.$=window.jQuery=a;
(function(g,f,j){var i=window.jQuery,h=window.$;
window.$=window.jQuery=f;
(function(l,k){k("kendo.color",["kendo.core"],l);
}(function(){var k={id:"color",name:"Color utils",category:"framework",advanced:true,description:"Color utilities used across components",depends:["core"]};
(function(l,x,y){var r=function(G){var A=this,B=r.formats,F,E,D,C,z;
if(arguments.length===1){G=A.resolveColor(G);
for(C=0;
C<B.length;
C++){F=B[C].re;
E=B[C].process;
D=F.exec(G);
if(D){z=E(D);
A.r=z[0];
A.g=z[1];
A.b=z[2];
}}}else{A.r=arguments[0];
A.g=arguments[1];
A.b=arguments[2];
}A.r=A.normalizeByte(A.r);
A.g=A.normalizeByte(A.g);
A.b=A.normalizeByte(A.b);
};
r.prototype={toHex:function(){var A=this,C=A.padDigit,D=A.r.toString(16),B=A.g.toString(16),z=A.b.toString(16);
return"#"+C(D)+C(B)+C(z);
},resolveColor:function(z){z=z||"black";
if(z.charAt(0)=="#"){z=z.substr(1,6);
}z=z.replace(/ /g,"");
z=z.toLowerCase();
z=r.namedColors[z]||z;
return z;
},normalizeByte:function(z){return z<0||isNaN(z)?0:z>255?255:z;
},padDigit:function(z){return z.length===1?"0"+z:z;
},brightness:function(B){var z=this,A=Math.round;
z.r=A(z.normalizeByte(z.r*B));
z.g=A(z.normalizeByte(z.g*B));
z.b=A(z.normalizeByte(z.b*B));
return z;
},percBrightness:function(){var z=this;
return Math.sqrt(0.241*z.r*z.r+0.691*z.g*z.g+0.06800000000000001*z.b*z.b);
}};
r.formats=[{re:/^rgb\((\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3})\)$/,process:function(z){return[y(z[1],10),y(z[2],10),y(z[3],10)];
}},{re:/^(\w{2})(\w{2})(\w{2})$/,process:function(z){return[y(z[1],16),y(z[2],16),y(z[3],16)];
}},{re:/^(\w{1})(\w{1})(\w{1})$/,process:function(z){return[y(z[1]+z[1],16),y(z[2]+z[2],16),y(z[3]+z[3],16)];
}}];
r.namedColors={aliceblue:"f0f8ff",antiquewhite:"faebd7",aqua:"00ffff",aquamarine:"7fffd4",azure:"f0ffff",beige:"f5f5dc",bisque:"ffe4c4",black:"000000",blanchedalmond:"ffebcd",blue:"0000ff",blueviolet:"8a2be2",brown:"a52a2a",burlywood:"deb887",cadetblue:"5f9ea0",chartreuse:"7fff00",chocolate:"d2691e",coral:"ff7f50",cornflowerblue:"6495ed",cornsilk:"fff8dc",crimson:"dc143c",cyan:"00ffff",darkblue:"00008b",darkcyan:"008b8b",darkgoldenrod:"b8860b",darkgray:"a9a9a9",darkgrey:"a9a9a9",darkgreen:"006400",darkkhaki:"bdb76b",darkmagenta:"8b008b",darkolivegreen:"556b2f",darkorange:"ff8c00",darkorchid:"9932cc",darkred:"8b0000",darksalmon:"e9967a",darkseagreen:"8fbc8f",darkslateblue:"483d8b",darkslategray:"2f4f4f",darkslategrey:"2f4f4f",darkturquoise:"00ced1",darkviolet:"9400d3",deeppink:"ff1493",deepskyblue:"00bfff",dimgray:"696969",dimgrey:"696969",dodgerblue:"1e90ff",firebrick:"b22222",floralwhite:"fffaf0",forestgreen:"228b22",fuchsia:"ff00ff",gainsboro:"dcdcdc",ghostwhite:"f8f8ff",gold:"ffd700",goldenrod:"daa520",gray:"808080",grey:"808080",green:"008000",greenyellow:"adff2f",honeydew:"f0fff0",hotpink:"ff69b4",indianred:"cd5c5c",indigo:"4b0082",ivory:"fffff0",khaki:"f0e68c",lavender:"e6e6fa",lavenderblush:"fff0f5",lawngreen:"7cfc00",lemonchiffon:"fffacd",lightblue:"add8e6",lightcoral:"f08080",lightcyan:"e0ffff",lightgoldenrodyellow:"fafad2",lightgray:"d3d3d3",lightgrey:"d3d3d3",lightgreen:"90ee90",lightpink:"ffb6c1",lightsalmon:"ffa07a",lightseagreen:"20b2aa",lightskyblue:"87cefa",lightslategray:"778899",lightslategrey:"778899",lightsteelblue:"b0c4de",lightyellow:"ffffe0",lime:"00ff00",limegreen:"32cd32",linen:"faf0e6",magenta:"ff00ff",maroon:"800000",mediumaquamarine:"66cdaa",mediumblue:"0000cd",mediumorchid:"ba55d3",mediumpurple:"9370d8",mediumseagreen:"3cb371",mediumslateblue:"7b68ee",mediumspringgreen:"00fa9a",mediumturquoise:"48d1cc",mediumvioletred:"c71585",midnightblue:"191970",mintcream:"f5fffa",mistyrose:"ffe4e1",moccasin:"ffe4b5",navajowhite:"ffdead",navy:"000080",oldlace:"fdf5e6",olive:"808000",olivedrab:"6b8e23",orange:"ffa500",orangered:"ff4500",orchid:"da70d6",palegoldenrod:"eee8aa",palegreen:"98fb98",paleturquoise:"afeeee",palevioletred:"d87093",papayawhip:"ffefd5",peachpuff:"ffdab9",peru:"cd853f",pink:"ffc0cb",plum:"dda0dd",powderblue:"b0e0e6",purple:"800080",red:"ff0000",rosybrown:"bc8f8f",royalblue:"4169e1",saddlebrown:"8b4513",salmon:"fa8072",sandybrown:"f4a460",seagreen:"2e8b57",seashell:"fff5ee",sienna:"a0522d",silver:"c0c0c0",skyblue:"87ceeb",slateblue:"6a5acd",slategray:"708090",slategrey:"708090",snow:"fffafa",springgreen:"00ff7f",steelblue:"4682b4",tan:"d2b48c",teal:"008080",thistle:"d8bfd8",tomato:"ff6347",turquoise:"40e0d0",violet:"ee82ee",wheat:"f5deb3",white:"ffffff",whitesmoke:"f5f5f5",yellow:"ffff00",yellowgreen:"9acd32"};
var v=["transparent"];
for(var u in r.namedColors){if(r.namedColors.hasOwnProperty(u)){v.push(u);
}}v=new RegExp("^("+v.join("|")+")(\\W|$)","i");
function w(z,B){var A,C;
if(z==null||z=="none"){return null;
}if(z instanceof n){return z;
}z=z.toLowerCase();
if(A=v.exec(z)){if(A[1]=="transparent"){z=new q(1,1,1,0);
}else{z=w(r.namedColors[A[1]],B);
}z.match=[A[1]];
return z;
}if(A=/^#?([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})\b/i.exec(z)){C=new m(y(A[1],16),y(A[2],16),y(A[3],16),1);
}else{if(A=/^#?([0-9a-f])([0-9a-f])([0-9a-f])\b/i.exec(z)){C=new m(y(A[1]+A[1],16),y(A[2]+A[2],16),y(A[3]+A[3],16),1);
}else{if(A=/^rgb\(\s*([0-9]+)\s*,\s*([0-9]+)\s*,\s*([0-9]+)\s*\)/.exec(z)){C=new m(y(A[1],10),y(A[2],10),y(A[3],10),1);
}else{if(A=/^rgba\(\s*([0-9]+)\s*,\s*([0-9]+)\s*,\s*([0-9]+)\s*,\s*([0-9.]+)\s*\)/.exec(z)){C=new m(y(A[1],10),y(A[2],10),y(A[3],10),x(A[4]));
}else{if(A=/^rgb\(\s*([0-9]*\.?[0-9]+)%\s*,\s*([0-9]*\.?[0-9]+)%\s*,\s*([0-9]*\.?[0-9]+)%\s*\)/.exec(z)){C=new q(x(A[1])/100,x(A[2])/100,x(A[3])/100,1);
}else{if(A=/^rgba\(\s*([0-9]*\.?[0-9]+)%\s*,\s*([0-9]*\.?[0-9]+)%\s*,\s*([0-9]*\.?[0-9]+)%\s*,\s*([0-9.]+)\s*\)/.exec(z)){C=new q(x(A[1])/100,x(A[2])/100,x(A[3])/100,x(A[4]));
}}}}}}if(C){C.match=A;
}else{if(!B){throw new Error("Cannot parse color: "+z);
}}return C;
}function s(z,B,A){if(!A){A="0";
}z=z.toString(16);
while(B>z.length){z="0"+z;
}return z;
}function t(z,A,B){if(B<0){B+=1;
}if(B>1){B-=1;
}if(B<1/6){return z+(A-z)*6*B;
}if(B<1/2){return A;
}if(B<2/3){return z+(A-z)*(2/3-B)*6;
}return z;
}var n=kendo.Class.extend({toHSV:function(){return this;
},toRGB:function(){return this;
},toHex:function(){return this.toBytes().toHex();
},toBytes:function(){return this;
},toCss:function(){return"#"+this.toHex();
},toCssRgba:function(){var z=this.toBytes();
return"rgba("+z.r+", "+z.g+", "+z.b+", "+x((+this.a).toFixed(3))+")";
},toDisplay:function(){if(kendo.support.browser.msie&&kendo.support.browser.version<9){return this.toCss();
}return this.toCssRgba();
},equals:function(z){return z===this||z!==null&&this.toCssRgba()==w(z).toCssRgba();
},diff:function(A){if(A==null){return NaN;
}var z=this.toBytes();
A=A.toBytes();
return Math.sqrt(Math.pow((z.r-A.r)*0.3,2)+Math.pow((z.g-A.g)*0.59,2)+Math.pow((z.b-A.b)*0.11,2));
},clone:function(){var z=this.toBytes();
if(z===this){z=new m(z.r,z.g,z.b,z.a);
}return z;
}});
var q=n.extend({init:function(C,B,A,z){this.r=C;
this.g=B;
this.b=A;
this.a=z;
},toHSV:function(){var E,D,A,C,G,H;
var F=this.r,B=this.g,z=this.b;
E=Math.min(F,B,z);
D=Math.max(F,B,z);
H=D;
A=D-E;
if(A===0){return new p(0,0,H,this.a);
}if(D!==0){G=A/D;
if(F==D){C=(B-z)/A;
}else{if(B==D){C=2+(z-F)/A;
}else{C=4+(F-B)/A;
}}C*=60;
if(C<0){C+=360;
}}else{G=0;
C=-1;
}return new p(C,G,H,this.a);
},toHSL:function(){var G=this.r,B=this.g,z=this.b;
var E=Math.max(G,B,z),F=Math.min(G,B,z);
var C,H,D=(E+F)/2;
if(E==F){C=H=0;
}else{var A=E-F;
H=D>0.5?A/(2-E-F):A/(E+F);
switch(E){case G:C=(B-z)/A+(B<z?6:0);
break;
case B:C=(z-G)/A+2;
break;
case z:C=(G-B)/A+4;
break;
}C*=60;
H*=100;
D*=100;
}return new o(C,H,D,this.a);
},toBytes:function(){return new m(this.r*255,this.g*255,this.b*255,this.a);
}});
var m=q.extend({init:function(C,B,A,z){this.r=Math.round(C);
this.g=Math.round(B);
this.b=Math.round(A);
this.a=z;
},toRGB:function(){return new q(this.r/255,this.g/255,this.b/255,this.a);
},toHSV:function(){return this.toRGB().toHSV();
},toHSL:function(){return this.toRGB().toHSL();
},toHex:function(){return s(this.r,2)+s(this.g,2)+s(this.b,2);
},toBytes:function(){return this;
}});
var p=n.extend({init:function(A,B,C,z){this.h=A;
this.s=B;
this.v=C;
this.a=z;
},toRGB:function(){var C=this.h,H=this.s,J=this.v;
var D,G,B,z,A,E,F,I;
if(H===0){G=B=z=J;
}else{C/=60;
D=Math.floor(C);
A=C-D;
E=J*(1-H);
F=J*(1-H*A);
I=J*(1-H*(1-A));
switch(D){case 0:G=J;
B=I;
z=E;
break;
case 1:G=F;
B=J;
z=E;
break;
case 2:G=E;
B=J;
z=I;
break;
case 3:G=E;
B=F;
z=J;
break;
case 4:G=I;
B=E;
z=J;
break;
default:G=J;
B=E;
z=F;
break;
}}return new q(G,B,z,this.a);
},toHSL:function(){return this.toRGB().toHSL();
},toBytes:function(){return this.toRGB().toBytes();
}});
var o=n.extend({init:function(A,C,B,z){this.h=A;
this.s=C;
this.l=B;
this.a=z;
},toRGB:function(){var B=this.h,G=this.s,C=this.l;
var F,A,z;
if(G===0){F=A=z=C;
}else{B/=360;
G/=100;
C/=100;
var E=C<0.5?C*(1+G):C+G-C*G;
var D=2*C-E;
F=t(D,E,B+1/3);
A=t(D,E,B);
z=t(D,E,B-1/3);
}return new q(F,A,z,this.a);
},toHSV:function(){return this.toRGB().toHSV();
},toBytes:function(){return this.toRGB().toBytes();
}});
r.fromBytes=function(C,B,A,z){return new m(C,B,A,z!=null?z:1);
};
r.fromRGB=function(C,B,A,z){return new q(C,B,A,z!=null?z:1);
};
r.fromHSV=function(A,B,C,z){return new p(A,B,C,z!=null?z:1);
};
r.fromHSL=function(A,C,B,z){return new o(A,C,B,z!=null?z:1);
};
kendo.Color=r;
kendo.parseColor=w;
}(window.kendo.jQuery,parseFloat,parseInt));
},typeof define=="function"&&define.amd?define:function(k,l,m){(m||l)();
}));
window.$=h;
window.jQuery=i;
})($telerik.$,$telerik.$);
window.$=c;
window.jQuery=d;
})($telerik.$,$telerik.$);