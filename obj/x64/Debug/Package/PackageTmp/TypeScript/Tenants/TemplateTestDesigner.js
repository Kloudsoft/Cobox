var AffinityDms;
(function (AffinityDms) {
    var Entities;
    (function (Entities) {
        var KeyValue = (function () {
            function KeyValue() {
            }
            return KeyValue;
        }());
        Entities.KeyValue = KeyValue;
        (function (ResizeAxis) {
            ResizeAxis[ResizeAxis["None"] = 0] = "None";
            ResizeAxis[ResizeAxis["Horizontal"] = 1] = "Horizontal";
            ResizeAxis[ResizeAxis["HorizontalForwardOnly"] = 2] = "HorizontalForwardOnly";
            ResizeAxis[ResizeAxis["HorizontalBackwardOnly"] = 3] = "HorizontalBackwardOnly";
            ResizeAxis[ResizeAxis["Vertical"] = 4] = "Vertical";
            ResizeAxis[ResizeAxis["VerticalUpwardOnly"] = 5] = "VerticalUpwardOnly";
            ResizeAxis[ResizeAxis["VerticalDownwardOnly"] = 6] = "VerticalDownwardOnly";
            ResizeAxis[ResizeAxis["All"] = 7] = "All";
        })(Entities.ResizeAxis || (Entities.ResizeAxis = {}));
        var ResizeAxis = Entities.ResizeAxis;
        var TemplateElementDetailViewModel = (function () {
            function TemplateElementDetailViewModel() {
                this.TemplateElementDetail = new Entities.TemplateElementDetail();
            }
            return TemplateElementDetailViewModel;
        }());
        Entities.TemplateElementDetailViewModel = TemplateElementDetailViewModel;
        var ElementsViewModel = (function () {
            function ElementsViewModel() {
            }
            return ElementsViewModel;
        }());
        Entities.ElementsViewModel = ElementsViewModel;
        var Drawable1 = (function () {
            function Drawable1() {
            }
            return Drawable1;
        }());
        Entities.Drawable1 = Drawable1;
        var Rectangle = (function () {
            function Rectangle() {
            }
            Object.defineProperty(Rectangle.prototype, "X1", {
                get: function () { return (this._X1); },
                set: function (value) { this._X1 = value; },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Rectangle.prototype, "Y1", {
                get: function () { return (this._Y1); },
                set: function (value) { this._Y1 = value; },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Rectangle.prototype, "X2", {
                get: function () { return (this._X2); },
                set: function (value) { this._X2 = value; },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Rectangle.prototype, "Y2", {
                get: function () { return (this._Y2); },
                set: function (value) { this._Y2 = value; },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Rectangle.prototype, "Width", {
                get: function () { return (this._Width); },
                set: function (value) { this._Width = value; },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Rectangle.prototype, "Height", {
                get: function () { return (this._Height); },
                set: function (value) { this._Height = value; },
                enumerable: true,
                configurable: true
            });
            Rectangle.prototype.ContainsPoint = function (x, y) {
                return ((x >= this.X1) && (x <= this.X2) && (y >= this.Y1) && (y <= this.Y2));
            };
            Rectangle.FromRelative = function (x, y, width, height) {
                var rectangle = new Rectangle();
                rectangle._X1 = x;
                rectangle._Y1 = y;
                rectangle._X2 = x + width;
                rectangle._Y2 = y + height;
                rectangle._Width = width;
                rectangle._Height = height;
                return (rectangle);
            };
            Rectangle.FromAbsolute = function (x1, y1, x2, y2) {
                var rectangle = new Rectangle();
                rectangle._X1 = x1;
                rectangle._Y1 = y1;
                rectangle._X2 = x2;
                rectangle._Y2 = y2;
                rectangle._Width = x2 - x1;
                rectangle._Height = y2 - y1;
                return (rectangle);
            };
            Rectangle.FromDivElement = function (divElement) {
                var rectangle = new Rectangle();
                rectangle._X1 = divElement.offsetLeft;
                rectangle._Y1 = divElement.offsetTop;
                rectangle._X2 = divElement.offsetLeft + divElement.offsetWidth;
                rectangle._Y2 = divElement.offsetTop + divElement.offsetHeight;
                rectangle._Width = divElement.offsetWidth;
                rectangle._Height = divElement.offsetHeight;
                return (rectangle);
            };
            return Rectangle;
        }());
        Entities.Rectangle = Rectangle;
        //export class ExtendingElementDetails extends AffinityDms.Entities.TemplateElementDetail {
        //    ElementDivId: string;
        //    TemplateId: number;
        //}
        var ElementProperties = (function () {
            function ElementProperties() {
            }
            ElementProperties.prototype.DisplayProperties = function (ImgUploader, AddColumn, ElementDataType, Name, Description, Text, X, Y, X2, Y2, Radius, Diameter, Width, Height, DivX, DivY, DivWidth, DivHeight, MinHeight, MinWidth, ForegroundColor, BackGroundColor, Hint, MinChar, MaxChar, DateTime, FontName, FontSize, FontStyle, FontColor, BorderStyle, BarcodeType, Value, ElementIndexType, SizeMode, IsSelected, Discreminator, FontGraphicsUnit, ElementMobileOrdinal, ElementIndexDataType) {
                var currentTargetId = document.getElementById("CurTarget");
                var DivElement = document.getElementById(currentTargetId.value.toString());
                if (ImgUploader == true) {
                    var fileuploadingdiv = document.getElementById("fileuploader");
                    fileuploadingdiv.style.display = "";
                }
                else {
                    var fileuploadingdiv = document.getElementById("fileuploader");
                    fileuploadingdiv.style.display = "none";
                }
                if (AddColumn == true) {
                    var AddColumndiv = document.getElementById("addcolumndivid");
                    AddColumndiv.style.display = "";
                }
                else {
                    var AddColumndiv = document.getElementById("addcolumndivid");
                    AddColumndiv.style.display = "none";
                }
                if (Name == true) {
                    var AddNamediv = document.getElementById("addnamedivid");
                    AddNamediv.style.display = "";
                    if ((DivElement.getAttribute("data-name") != null) && (DivElement.getAttribute("data-name") != "")) {
                        var NameElement = document.getElementById("addnameid");
                        NameElement.value = DivElement.getAttribute("data-name");
                    }
                    else {
                        var NameElement = document.getElementById("addnameid");
                        NameElement.value = "";
                    }
                }
                else {
                    var NameElement = document.getElementById("addnameid");
                    var AddNamediv = document.getElementById("addnamedivid");
                    AddNamediv.style.display = "none";
                    NameElement.value = "";
                }
                if (Description == true) {
                    var AddDescriptiondiv = document.getElementById("adddescriptiondivid");
                    var AddDescription = document.getElementById("adddescriptionid");
                    AddDescriptiondiv.style.display = "";
                    AddDescription.value = DivElement.getAttribute("data-Description");
                }
                else {
                    var AddDescriptiondiv = document.getElementById("adddescriptiondivid");
                    var AddDescription = document.getElementById("adddescriptionid");
                    AddDescriptiondiv.style.display = "none";
                    AddDescription.value = "";
                }
                if (Text == true) {
                    var AddTextdiv = document.getElementById("addtextdivid");
                    var AddText = document.getElementById("addtextid");
                    if (DivElement.getAttribute("data-Tool") == Entities.ElementType.Textbox.toString()) {
                        AddText.value = DivElement.textContent;
                    }
                    else if (DivElement.getAttribute("data-Tool") == Entities.ElementType.Radio.toString() || DivElement.getAttribute("data-Tool") == Entities.ElementType.Checkbox.toString()) {
                        AddText.value = DivElement.childNodes[0].textContent;
                    }
                    else if (DivElement.getAttribute("data-Tool") == Entities.ElementType.Barcode2D.toString()) {
                        AddText.value = DivElement.getAttribute("data-BarcodeText");
                    }
                    else if (DivElement.getAttribute("data-Tool") == Entities.ElementType.Label.toString()) {
                        AddText.value = DivElement.childNodes[0].textContent;
                    }
                    AddTextdiv.style.display = "";
                }
                else {
                    var AddTextdiv = document.getElementById("addtextdivid");
                    AddTextdiv.style.display = "none";
                    var AddText = document.getElementById("addtextid");
                    AddText.value = "";
                }
                if (X == true) {
                    var AddXdiv = document.getElementById("addxdivid");
                    var AddX = document.getElementById("addxid");
                    AddXdiv.style.display = "";
                    AddX.value = DivElement.style.left.replace("px", "");
                }
                else {
                    var AddXdiv = document.getElementById("addxdivid");
                    var AddX = document.getElementById("addxid");
                    AddX.value = "";
                    AddXdiv.style.display = "none";
                }
                if (Y == true) {
                    var AddYdiv = document.getElementById("addydivid");
                    var AddY = document.getElementById("addyid");
                    AddYdiv.style.display = "";
                    AddY.value = DivElement.style.top.replace("px", "");
                }
                else {
                    var AddYdiv = document.getElementById("addydivid");
                    var AddY = document.getElementById("addyid");
                    AddY.value = "";
                    AddYdiv.style.display = "none";
                }
                if (X2 == true) {
                    var AddX2div = document.getElementById("addx2divid");
                    var AddX2 = document.getElementById("addx2id");
                    AddX2div.style.display = "";
                }
                else {
                    var AddX2div = document.getElementById("addx2divid");
                    var AddX2 = document.getElementById("addx2id");
                    AddX2.value = "";
                    AddX2div.style.display = "none";
                }
                if (Y2 == true) {
                    var AddY2div = document.getElementById("addy2divid");
                    var AddY2 = document.getElementById("addy2id");
                    AddY2div.style.display = "";
                }
                else {
                    var AddY2div = document.getElementById("addy2divid");
                    var AddY2 = document.getElementById("addy2id");
                    AddY2.value = "";
                    AddY2div.style.display = "none";
                }
                if (MaxChar == true) {
                    var AddMaxChardiv = document.getElementById("addmaxchardivid");
                    var AddMaxChar = document.getElementById("addmaxcharid");
                    //  var childElement: HTMLElement = <HTMLElement>DivElement.childNodes[0];
                    AddMaxChar.value = DivElement.getAttribute("maxlength");
                    AddMaxChardiv.style.display = "";
                }
                else {
                    var AddMaxChardiv = document.getElementById("addmaxchardivid");
                    var AddMaxChar = document.getElementById("addmaxcharid");
                    AddMaxChar.value = "";
                    AddMaxChardiv.style.display = "none";
                }
                if (FontSize == true) {
                    var AddFontSizediv = document.getElementById("addfontsizedivid");
                    var AddFontSize = document.getElementById("addfontsizeid");
                    AddFontSize.value = DivElement.style.fontSize.replace("px", "");
                    AddFontSizediv.style.display = "";
                }
                else {
                    var AddFontSizediv = document.getElementById("addfontsizedivid");
                    var AddFontSize = document.getElementById("addfontsizeid");
                    AddFontSize.value = "";
                    AddFontSizediv.style.display = "none";
                }
                //if (FontFamily == true) {
                //    var AddFontSizediv: HTMLDivElement = <HTMLDivElement>document.getElementById("addy2divid");
                //    AddFontSizediv.style.display = "";
                //}
                //else {
                //    var AddFontSizediv: HTMLDivElement = <HTMLDivElement>document.getElementById("addy2divid");
                //    AddFontSizediv.style.display = "none";
                //}
                //if (ElementIndexType == true) {
                //    var AddelemIndexdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
                //    var AddelemIndex: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementindextypeid");
                //    AddelemIndexdiv.style.display = "";
                //    if (DivElement != null) {
                //        if (DivElement.getAttribute("data-IndexValueType") == "1") {
                //            AddelemIndex.checked = true;
                //            var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //            var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //            AddValues.value = DivElement.getAttribute("data-Value");
                //            AddValuesdiv.style.display = "";
                //            if (DivElement.getAttribute("data-IndexValueDataType") != null) {
                //                if (DivElement.getAttribute("data-IndexValueDataType") == "1") {
                //                    DivElement.setAttribute("data-IndexValueDataType", "0")
                //                }
                //            }
                //            else
                //            {
                //                DivElement.setAttribute("data-IndexValueDataType", "0")
                //            }
                //        }
                //        else {
                //            AddelemIndex.checked = false;
                //            var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //            var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //            AddValues.value = "";
                //            AddValuesdiv.style.display = "none";
                //        }
                //    }
                //}
                //else {
                //    var AddelemIndexdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
                //    var AddelemIndex: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementindextypeid");
                //    AddelemIndex.checked = false;
                //    AddelemIndexdiv.style.display = "none";
                //    var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //    AddValues.value = "";
                //    AddValuesdiv.style.display = "none";
                //}
                //if (DivElement != null) {
                //    if (DivElement.getAttribute("data-IndexValueType") == "1") {
                //        AddelemIndex.checked = true;
                //        var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //        var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //        AddValues.value = DivElement.getAttribute("data-Value");
                //        AddValuesdiv.style.display = "";
                //        if (DivElement.getAttribute("data-IndexValueDataType") != null) {
                //            if (DivElement.getAttribute("data-IndexValueDataType") == "1") {
                //                DivElement.setAttribute("data-IndexValueDataType", "0")
                //            }
                //        }
                //        else {
                //            DivElement.setAttribute("data-IndexValueDataType", "0")
                //        }
                //    }
                //    else {
                //        AddelemIndex.checked = false;
                //        var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //        var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //        AddValues.value = "";
                //        AddValuesdiv.style.display = "none";
                //    }
                //}
                //else {
                //    var AddelemIndexdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
                //    var AddelemIndex: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementindextypeid");
                //    AddelemIndex.checked = false;
                //    AddelemIndexdiv.style.display = "none";
                //    var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //    AddValues.value = "";
                //    AddValuesdiv.style.display = "none";
                //}
                if (ElementIndexType) {
                    //addindexradiodivid                    addindexradioid                     RADIO1
                    //addindexlabeldivid                    addindexlabelid                     LABEL ELEMENT
                    //addindexdatatyperadiodivid            addindexdatatyperadioid             RADIO2
                    //addindexvaluedivid                    addindexvalueid                     VALUE ELEMENT
                    var IndexLabelRadioDiv = document.getElementById("addindexradiodivid");
                    IndexLabelRadioDiv.style.display = "";
                    if (DivElement.getAttribute("data-ElementIndexType") == AffinityDms.Entities.ElementIndexType.Label.toString()) {
                        var IndexLabelRadio = document.getElementById("addindexradioid");
                        if (IndexLabelRadio != null) {
                            IndexLabelRadio.checked = true;
                        }
                        var IndexLabelDiv = document.getElementById("addindexlabeldivid");
                        if (IndexLabelDiv != null) {
                            IndexLabelDiv.style.display = "";
                            var IndexLabel = document.getElementById("addindexlabelid");
                            IndexLabel.value = DivElement.getAttribute("data-Value");
                        }
                    }
                    else {
                        var IndexLabelRadio = document.getElementById("addindexradioid");
                        if (IndexLabelRadio != null) {
                            IndexLabelRadio.checked = false;
                        }
                        var IndexLabelDiv = document.getElementById("addindexlabeldivid");
                        if (IndexLabelDiv != null) {
                            IndexLabelDiv.style.display = "none";
                            var IndexLabel = document.getElementById("addindexlabelid");
                            IndexLabel.value = "";
                        }
                    }
                }
                else {
                    var IndexLabelRadioDiv = document.getElementById("addindexradiodivid");
                    IndexLabelRadioDiv.style.display = "none";
                    var IndexLabelRadio = document.getElementById("addindexradioid");
                    if (IndexLabelRadio != null) {
                        IndexLabelRadio.checked = false;
                    }
                    var IndexLabelDiv = document.getElementById("addindexlabeldivid");
                    if (IndexLabelDiv != null) {
                        IndexLabelDiv.style.display = "none";
                        var IndexLabel = document.getElementById("addindexlabelid");
                        IndexLabel.value = "";
                    }
                }
                if (ElementIndexDataType) {
                    //addindexradiodivid                    addindexradioid                     RADIO1
                    //addindexlabeldivid                    addindexlabelid                     LABEL ELEMENT
                    //addindexdatatyperadiodivid            addindexdatatyperadioid             RADIO2
                    //addindexvaluedivid                    addindexvalueid                     VALUE ELEMENT
                    var IndexValueRadioDiv = document.getElementById("addindexdatatyperadiodivid");
                    IndexValueRadioDiv.style.display = "";
                    if (DivElement.getAttribute("data-ElementIndexType") == AffinityDms.Entities.ElementIndexType.Value.toString()) {
                        var IndexValueRadio = document.getElementById("addindexdatatyperadioid");
                        if (IndexValueRadio != null) {
                            IndexValueRadio.checked = true;
                        }
                        var IndexValueDiv = document.getElementById("addindexvaluedivid");
                        if (IndexValueDiv != null) {
                            IndexValueDiv.style.display = "";
                            var IndexValue = document.getElementById("addindexvalueid");
                            var index = parseInt(DivElement.getAttribute("data-Value"));
                            if (index == null) {
                                index = -1;
                            }
                            var IndexValueOpt = IndexValue.options[index];
                            IndexValueOpt.selected = true;
                        }
                    }
                    else {
                        var IndexValueRadio = document.getElementById("addindexdatatyperadioid");
                        if (IndexValueRadio != null) {
                            IndexValueRadio.checked = false;
                        }
                        var IndexValueDiv = document.getElementById("addindexvaluedivid");
                        if (IndexValueDiv != null) {
                            IndexValueDiv.style.display = "none";
                            var IndexValue = document.getElementById("addindexvalueid");
                            var index = -1;
                            var IndexValueOpt = IndexValue.options[index];
                            IndexValueOpt.selected = true;
                        }
                    }
                }
                else {
                    var IndexValueRadioDiv = document.getElementById("addindexdatatyperadiodivid");
                    IndexValueRadioDiv.style.display = "none";
                    var IndexValueRadio = document.getElementById("addindexdatatyperadioid");
                    if (IndexValueRadio != null) {
                        IndexValueRadio.checked = false;
                    }
                    var IndexValueDiv = document.getElementById("addindexvaluedivid");
                    if (IndexValueDiv != null) {
                        IndexValueDiv.style.display = "none";
                        var IndexValue = document.getElementById("addindexvalueid");
                        var index = -1;
                        var IndexValueOpt = IndexValue.options[index];
                    }
                }
                //var addelementindextypeid = <HTMLInputElement>document.getElementById("addelementindextypeid");
                //if (ElementIndexType == true) {
                //    var AddValuesDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //    var addelementdatatypeid: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypeid");
                //    var addelementdatatypevalueid: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypevalueid");
                //    var addelementdatatypevaluedivid: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
                //    if (DivElement.getAttribute("data-IndexValueType") != null) {
                //        var addelementindextypedivid = <HTMLDivElement>document.getElementById("addelementindextypedivid");
                //        addelementindextypedivid.style.display = "";
                //        var addelementdatatypedivid = <HTMLDivElement>document.getElementById("addelementdatatypedivid");
                //        addelementdatatypedivid.style.display = "";
                //        if (DivElement.getAttribute("data-IndexValueType") == AffinityDms.Entities.ElementIndexType.Label.toString()) {
                //            addelementindextypeid.checked = true;
                //            AddValuesDiv.style.display = "";
                //            addelementdatatypevaluedivid.style.display = "none";
                //        }
                //        else if (DivElement.getAttribute("data-IndexValueType") == AffinityDms.Entities.ElementIndexType.Value.toString())
                //        {
                //            addelementdatatypeid.checked = true;
                //            AddValuesDiv.style.display = "none"
                //            addelementdatatypevalueid.style.display = "";
                //        }
                //    }
                //    if (addelementindextypeid.checked) {
                //      //  var AddelemIndexdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
                //        //AddValuesDiv.style.display = "";
                //       // addelementindextypeid.style.display = "";
                //        if (DivElement != null) {
                //            DivElement.setAttribute("data-IndexValueType", AffinityDms.Entities.ElementIndexType.Label.toString());
                //            if (DivElement.getAttribute("data-Value") != null) {
                //                AddValues.value = DivElement.getAttribute("data-Value");
                //            }
                //            else {
                //                AddValues.value = "";
                //            }
                //        }
                //    }
                //}
                //if ((ElementIndexType == false) || (!(addelementindextypeid.checked))) {
                //    if (ElementIndexType == false)
                //    {
                //        addelementindextypeid.style.display = "none";
                //    }
                //    var AddValuesDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //    if (DivElement != null) {
                //        DivElement.setAttribute("data-IndexValueType", AffinityDms.Entities.ElementIndexType.Value.toString());
                //        DivElement.setAttribute("data-Value", "");
                //        AddValues.value = "";
                //        addelementindextypeid.checked = false;
                //        AddValuesDiv.style.display = "none";
                //    }
                //}
                //var addelementdatatypeid = <HTMLInputElement>document.getElementById("addelementdatatypeid");
                //if (ElementIndexDataType == true) {
                //    var AddValuesDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //    var addelementdatatypevalueid: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypevalueid");
                //    if (DivElement.getAttribute("data-IndexValueType") != null) {
                //        var addelementindextypedivid = <HTMLDivElement>document.getElementById("addelementindextypedivid");
                //        addelementindextypedivid.style.display = "";
                //        var addelementdatatypedivid = <HTMLDivElement>document.getElementById("addelementdatatypedivid");
                //        addelementdatatypedivid.style.display = "";
                //        if (DivElement.getAttribute("data-IndexValueType") == AffinityDms.Entities.ElementIndexType.Label.toString()) {
                //            AddValuesDiv.style.display = "";
                //            addelementdatatypeid.checked = true;
                //            addelementdatatypevalueid.style.display = "none";
                //        }
                //        else if (DivElement.getAttribute("data-IndexValueType") == AffinityDms.Entities.ElementIndexType.Value.toString()) {
                //            AddValuesDiv.style.display = "none"
                //            addelementindextypeid.checked = false;
                //            addelementdatatypevalueid.style.display = "";
                //        }
                //    }
                //    if (addelementdatatypeid.checked) {
                //        var AddValuesDTDiv: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypevaluedivid");
                //        AddValuesDTDiv.style.display = "";
                //        addelementdatatypeid.style.display = "";
                //        var AddValuesDT: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
                //        if (DivElement != null) {
                //            DivElement.setAttribute("data-IndexValueType", AffinityDms.Entities.ElementIndexType.Value.toString());
                //            if (DivElement.getAttribute("data-ValueDataType") != null) {
                //                var IndexValue = parseInt(DivElement.getAttribute("data-ValueDataType"));
                //                var opt = <HTMLOptionElement>AddValuesDT.options[IndexValue];
                //                opt.selected = true;
                //            }
                //            else {
                //                var IndexValue = parseInt(DivElement.getAttribute("data-ValueDataType"));
                //                var opt = <HTMLOptionElement>AddValuesDT.options[IndexValue];
                //                opt.selected = true;
                //                DivElement.setAttribute("data-ValueDataType","-1")
                //            }
                //        }
                //    }
                //}
                //if ((ElementIndexDataType == false) || (!(addelementdatatypeid.checked))) {
                //    // var AddelemIndexdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
                //    if (ElementIndexDataType == false)
                //    {
                //        addelementindextypeid.style.display = "none";
                //    }
                //    var AddValuesDTDiv: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypevaluedivid");
                //    var AddValuesDT: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
                //    if (DivElement != null) {
                //        DivElement.setAttribute("data-IndexValueType", AffinityDms.Entities.ElementIndexType.Label.toString());
                //        DivElement.setAttribute("data-ValueDataType", "-1");
                //        var IndexValue = parseInt(DivElement.getAttribute("data-ValueDataType"));
                //        var opt = <HTMLOptionElement>AddValuesDT.options[IndexValue];
                //        opt.selected = true;
                //        addelementindextypeid.checked = false;
                //        AddValuesDTDiv.style.display = "none";
                //    }
                //}
                //if (ElementIndexDataType == true) {
                //    var AddelemIndexDTdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypedivid");
                //    var AddelemIndexDT: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypeid");
                //    AddelemIndexDTdiv.style.display = "";
                //    if (DivElement != null) {
                //        if (DivElement.getAttribute("data-IndexValueType") == "1") {
                //            AddelemIndexDT.checked = true;
                //            var AddValuesDTdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
                //            var AddValuesDT: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
                //            var selectedopt = 0;
                //            if (DivElement.getAttribute("data-ValueDataType") != null) {
                //                selectedopt = parseInt(DivElement.getAttribute("data-ValueDataType"));
                //            }
                //            var opt = <HTMLOptionElement>AddValuesDT.options[selectedopt];
                //            opt.selected = true;
                //            AddValuesDTdiv.style.display = "";
                //            if (DivElement.getAttribute("data-IndexValueType") != null) {
                //                if (DivElement.getAttribute("data-IndexValueType") == "1") {
                //                    DivElement.setAttribute("data-IndexValueType", "0")
                //                }
                //            }
                //            else {
                //                DivElement.setAttribute("data-IndexValueType", "0")
                //            }
                //        }
                //        else {
                //            //AddelemIndex.setAttribute("checked", "checked");
                //            AddelemIndexDT.checked = false;
                //            var AddValuesDTdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
                //            var AddValuesDT: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
                //            AddValuesDT.value = "";
                //            var selectedopt = 0;
                //            //if (DivElement.getAttribute("data-ValueDataType") != null) {
                //            //    selectedopt = parseInt(DivElement.getAttribute("data-ValueDataType"));
                //            //}
                //            var opt = <HTMLOptionElement>AddValuesDT.options[selectedopt];
                //            opt.selected = true;
                //            AddValuesDTdiv.style.display = "none";
                //        }
                //    }
                //}
                //else {
                //    var AddelemIndexDTdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypedivid");
                //    var AddelemIndexDT: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypeid");
                //    AddelemIndexDT.checked = false;
                //    AddelemIndexDTdiv.style.display = "none";
                //    var AddValuesDTdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
                //    var AddValuesDT: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
                //    var selectedopt = 0;
                //    var opt = <HTMLOptionElement>AddValuesDT.options[selectedopt];
                //    opt.selected = true;
                //    AddValuesDTdiv.style.display = "none";
                //}
                if (Width == true) {
                    var AddWidthdiv = document.getElementById("addwidthdivid");
                    var AddWidth = document.getElementById("addwidthid");
                    DivElement.setAttribute("data-lastupdatedwidth", DivElement.style.width.replace("px", ""));
                    AddWidth.value = DivElement.style.width.replace("px", "");
                    AddWidthdiv.style.display = "";
                }
                else {
                    var AddWidthdiv = document.getElementById("addwidthdivid");
                    var AddWidth = document.getElementById("addwidthid");
                    AddWidth.value = "";
                    AddWidthdiv.style.display = "none";
                }
                if (Height == true) {
                    var AddHeightdiv = document.getElementById("addheightdivid");
                    var AddHeight = document.getElementById("addheightid");
                    DivElement.setAttribute("data-lastupdatedheight", DivElement.style.height.replace("px", ""));
                    AddHeight.value = DivElement.style.height.replace("px", "");
                    AddHeightdiv.style.display = "";
                }
                else {
                    var AddHeightdiv = document.getElementById("addheightdivid");
                    var AddWidth = document.getElementById("addheightid");
                    AddWidth.value = "";
                    AddHeightdiv.style.display = "none";
                }
                var hiddenDivId = document.getElementById("Hidfind");
                hiddenDivId.value = "0";
                var AddDeleteCoulndiv = document.getElementById("adddeletecolumnbtndivid");
                AddDeleteCoulndiv.style.display = "none";
                var AddDeletediv = document.getElementById("adddeletebtndivid");
                AddDeletediv.style.display = "";
            };
            return ElementProperties;
        }());
        Entities.ElementProperties = ElementProperties;
        var FontStyles;
        (function (FontStyles) {
            FontStyles[FontStyles["inherit"] = 0] = "inherit";
            FontStyles[FontStyles["initial"] = 1] = "initial";
            FontStyles[FontStyles["italic"] = 2] = "italic";
            FontStyles[FontStyles["normal"] = 3] = "normal";
            FontStyles[FontStyles["oblique"] = 4] = "oblique";
        })(FontStyles || (FontStyles = {}));
        ;
        var BorderStyles;
        (function (BorderStyles) {
            BorderStyles[BorderStyles["dashed"] = 0] = "dashed";
            BorderStyles[BorderStyles["dotted"] = 1] = "dotted";
            BorderStyles[BorderStyles["double"] = 2] = "double";
            BorderStyles[BorderStyles["grouve"] = 3] = "grouve";
            BorderStyles[BorderStyles["hidden"] = 4] = "hidden";
            BorderStyles[BorderStyles["inherit"] = 5] = "inherit";
            BorderStyles[BorderStyles["initial"] = 6] = "initial";
            BorderStyles[BorderStyles["inset"] = 7] = "inset";
            BorderStyles[BorderStyles["none"] = 8] = "none";
            BorderStyles[BorderStyles["outset"] = 9] = "outset";
            BorderStyles[BorderStyles["ridge"] = 10] = "ridge";
            BorderStyles[BorderStyles["solid"] = 11] = "solid";
        })(BorderStyles || (BorderStyles = {}));
        ;
        var FormTestDesigner = (function () {
            //public TemplateObject: Template;
            //public selectedElement: HTMLImageElement;
            function FormTestDesigner() {
                //this.selectedElement = null;
                this._dragging = false;
                this._elementId = "";
                this.select = "";
                this.divelement = null;
                ////////////public OnDivElementMouseMove(event): void {
                ////////////    // alert(this.divelement.id);
                ////////////    if (this.divelement != null && this._dragging == true) {
                ////////////        //fixPageXY(e)
                ////////////        this.divelement.style.left = event.pageX - 25 + 'px';
                ////////////        this.divelement.style.top = event.pageY - 25 + 'px';
                ////////////        this.divelement.onmouseup = this.OnDivElementMouseUp;
                ////////////        // document.getElementById('myElement').value = "left:" + self.style.left + " top:" + self.style.top;
                ////////////        //docele.text=self.style.left;
                ////////////        //.text=self.style.top;
                ////////////    }
                ////////////}
                //public pushArrayElements(id:number): void {
                this.count = 0;
                //public copy(event: string): any {
                //}
                this.ImageMoving = false;
                //this.TemplateObject = new Template();
                //// alert("abc");
                //this.TemplateObject.TemplateVersions = new Array<TemplateVersion>();
                //this.TemplateObject.TemplateVersions.push(new TemplateVersion());
                //this.TemplateObject.TemplateVersions[0].TemplatePages = new Array<TemplatePage>();
                //this.TemplateObject.TemplateVersions[0].TemplatePages.push(new TemplatePage());
                //var page = this.TemplateObject.TemplateVersions[0].TemplatePages[0];
                //page.Width = 100;
                //page.Height = 100;
            }
            FormTestDesigner.prototype.Run = function () {
                // var page = this.TemplateObject.TemplateVersions[0].TemplatePages[0];
                var divPage = document.getElementById("DivPage");
                //var uploaddiv: HTMLDivElement = <HTMLDivElement>document.getElementById("fileuploader");
                //uploaddiv.style.display = "none";
                // divPage.onmousemove = this.OnDivElementMouseMove;
            };
            FormTestDesigner.prototype.OnDragStart = function (event) {
                this._dragging = true;
                if (this.divelement != null) {
                    return false;
                }
                this._elementId = event.currentTarget.id;
                this.PX = event.x;
                this.PY = event.y;
                event.dataTransfer.setData("OnDragStart", event.currentTarget.id);
            };
            FormTestDesigner.prototype.OnResizeEnded = function () {
                var curtarget = document.getElementById("CurTarget");
                var divElement = document.getElementById(curtarget.value);
                var AddWidth = document.getElementById("addwidthid");
                var AddHeight = document.getElementById("addheightid");
                if (divElement.style.width.replace("px", "") != divElement.getAttribute("data-lastupdatedwidth") || divElement.style.height.replace("px", "") != divElement.getAttribute("data-lastupdatedheight")) {
                    AddWidth.value = divElement.style.width.replace("px", "");
                    AddHeight.value = divElement.style.height.replace("px", "");
                    divElement.setAttribute("data-lastupdatedwidth", AddWidth.value);
                    divElement.setAttribute("data-lastupdatedheight", AddHeight.value);
                }
            };
            FormTestDesigner.prototype.GetLeft = function (event) {
                var leftpanel = document.getElementById("LeftPane");
                var divpage = document.getElementById("DivPage");
                var templatetype = document.getElementById("TempType");
                if (templatetype.value == "Form") {
                    var left = "";
                    if (leftpanel != null) {
                        left = (((event.pageX) - ((divpage.offsetLeft + leftpanel.clientWidth + 20))).toString() + "px");
                    }
                    else {
                        left = (((event.pageX) - (divpage.offsetLeft + 20)).toString() + "px");
                    }
                }
                else {
                    var divpage = document.getElementById("DivPage");
                    var toolboxdiv = document.getElementById("DivToolBox");
                    var offsetparent = divpage.offsetParent;
                    var templateimageoffset = offsetparent.offsetLeft;
                    var left = event.offsetX + "px";
                }
                return left;
            };
            FormTestDesigner.prototype.GetTop = function (event) {
                var leftpanel = document.getElementById("LeftPane");
                var top = "";
                var templatetype = document.getElementById("TempType");
                if (templatetype.value == "Form") {
                    var divpage = document.getElementById("DivPage");
                    top = ((((event.pageY) - divpage.parentElement.offsetTop)).toString() + "px");
                }
                else {
                    var divpage = document.getElementById("DivPage");
                    var offsetparent = divpage.offsetParent;
                    var templateimageoffset = offsetparent.offsetTop;
                    top = event.offsetY + "px";
                }
                return top;
            };
            FormTestDesigner.prototype.GetScaleValue = function () {
                var parentDiv = document.getElementById("ElementsContainer");
                parentDiv.style.transformOrigin = "left top";
                var val = parentDiv.style.transform.toString().toLowerCase();
                var res = val.replace("scale(", "");
                res = res.replace(")", "");
                var result = parseFloat(res);
                return result;
            };
            FormTestDesigner.prototype.DragOffsetFix = function (ui, scale) {
                var changeLeft = ui.position.left - ui.originalPosition.left; // find change in left
                var newLeft = ui.originalPosition.left + changeLeft / scale; // adjust new left by our zoomScale
                var changeTop = ui.position.top - ui.originalPosition.top; // find change in top
                var newTop = ui.originalPosition.top + changeTop / scale; // adjust new top by our zoomScale
                return (new Array(newTop, newLeft));
            };
            FormTestDesigner.prototype.ResizeOffsetFix = function (ui, scale) {
                var changeWidth = ui.size.width - ui.originalSize.width; // find change in width
                var newWidth = ui.originalSize.width + changeWidth / scale; // adjust new width by our zoomScale
                var changeHeight = ui.size.height - ui.originalSize.height; // find change in height
                var newHeight = ui.originalSize.height + changeHeight / scale; // adjust new height by our zoomScale
                ui.size.width = newWidth * scale;
                ui.size.height = newHeight * scale;
                return (new Array(newWidth, newHeight));
            };
            FormTestDesigner.prototype.CreateElement = function (id, appendTo, classList, styleStr, attributes, childElements, width, height, left, top, X, Y, isDraggable, IsResizable, resizeAxis, elementType) {
                var resizeHandles = "all";
                if (resizeAxis == ResizeAxis.Horizontal) {
                    resizeHandles = 'e';
                }
                else if (resizeAxis == ResizeAxis.HorizontalForwardOnly) {
                    resizeHandles = 'e';
                }
                else if (resizeAxis == ResizeAxis.HorizontalBackwardOnly) {
                    resizeHandles = 'e';
                }
                else if (resizeAxis == ResizeAxis.Vertical) {
                    resizeHandles = 's';
                }
                else if (resizeAxis == ResizeAxis.VerticalUpwardOnly) {
                    resizeHandles = 's';
                }
                else if (resizeAxis == ResizeAxis.VerticalDownwardOnly) {
                    resizeHandles = 's';
                }
                else if (resizeAxis == ResizeAxis.All) {
                    resizeHandles = 's, e, se';
                }
                else if (resizeAxis == ResizeAxis.None) {
                    resizeHandles = '';
                    IsResizable = false;
                }
                //////////if (resizeAxis == ResizeAxis.Horizontal) {
                //////////    resizeHandles = 'e, w';
                //////////}
                //////////else if (resizeAxis == ResizeAxis.HorizontalForwardOnly) {
                //////////    resizeHandles = 'e';
                //////////}
                //////////else if (resizeAxis == ResizeAxis.HorizontalBackwardOnly) {
                //////////    resizeHandles = 'w';
                //////////}
                //////////else if (resizeAxis == ResizeAxis.Vertical) {
                //////////    resizeHandles = 'n, s';
                //////////}
                //////////else if (resizeAxis == ResizeAxis.VerticalUpwardOnly) {
                //////////    resizeHandles = 'n';
                //////////}
                //////////else if (resizeAxis == ResizeAxis.VerticalDownwardOnly) {
                //////////    resizeHandles = 's';
                //////////}
                //////////else if (resizeAxis == ResizeAxis.All) {
                //////////    resizeHandles = 'all';
                //////////}
                //////////else if (resizeAxis == ResizeAxis.None) {
                //////////    resizeHandles = '';
                //////////    IsResizable = false;
                //////////}
                var tempType = document.getElementById("TempType");
                var containment = ""; //"div" + appendTo;
                if (tempType.value == "Template") {
                    if (elementType != Entities.ElementType.TableColumn) {
                        containment = "img#TemplateImage";
                    }
                }
                else {
                    containment = "div#DivPage";
                }
                var newElementDiv = null;
                newElementDiv = $("<div>")
                    .attr("id", id)
                    .attr("style", styleStr)
                    .width(width)
                    .height(height)
                    .dblclick(function (e) {
                    if (elementType == Entities.ElementType.TableColumn) {
                        var hiddenDivId = document.getElementById("Hidfind");
                        hiddenDivId.value = id;
                        var AddDeleteColumndiv = document.getElementById("adddeletecolumnbtndivid");
                        AddDeleteColumndiv.style.display = "";
                    }
                })
                    .click(function (e) {
                    AffinityDms.Entities.FormTestDesigner.prototype.OnElementClick(e);
                });
                if (isDraggable) {
                    newElementDiv.draggable({
                        scroll: true,
                        cursor: "move",
                        containment: containment,
                        start: function (event, ui) {
                            ui.position.left = 0;
                            ui.position.top = 0;
                        },
                        drag: function (event, ui) {
                            AffinityDms.Entities.FormTestDesigner.prototype.OnElementClick(event);
                            var scale = AffinityDms.Entities.FormTestDesigner.prototype.GetScaleValue();
                            var newPostions = AffinityDms.Entities.FormTestDesigner.prototype.DragOffsetFix(ui, scale);
                            var TemplateImage = document.getElementById("TemplateImage");
                            if (TemplateImage != null) {
                            }
                            ui.position.top = newPostions[0];
                            ui.position.left = newPostions[1];
                            //event.srcElement.clientLeft = ui.position.left + ui.offset.left;
                            //event.srcElement.clientTop = ui.position.top + ui.offset.top;
                            //code returns false if your check does not go through
                            //your code to check if the user can drag anymore
                        }
                    });
                }
                if (IsResizable) {
                    newElementDiv.resizable({
                        handles: resizeHandles,
                        autoHide: true,
                        containment: containment,
                        minWidth: 15,
                        minHeight: 15,
                        resize: function (event, ui) {
                            var currentElement_H = document.getElementById("CurTarget");
                            currentElement_H.value = id;
                            var currentElement = document.getElementById(currentElement_H.value);
                            var tempType = document.getElementById("TempType");
                            if (currentElement.getAttribute("data-tool") == Entities.ElementType.Table.toString()) {
                                var offset = 0;
                                for (var i = 0; i < currentElement.childNodes.length; i++) {
                                    var tbleCol = currentElement.childNodes[i];
                                    if (!(tbleCol.classList.contains("ui-resizable-handle"))) {
                                        offset += (+(parseInt(tbleCol.style.width.replace("px", ""))));
                                        offset += ($(tbleCol.id).outerWidth() - $(tbleCol.id).innerWidth());
                                    }
                                }
                                var scale = AffinityDms.Entities.FormTestDesigner.prototype.GetScaleValue();
                                var newSize = AffinityDms.Entities.FormTestDesigner.prototype.ResizeOffsetFix(ui, scale);
                                if (tempType.value == "Template") {
                                    var TemplateImage = document.getElementById("TemplateImage");
                                    if (TemplateImage != null) {
                                        if ((newSize[0] + event.target.offsetLeft) > TemplateImage.offsetWidth || offset > (newSize[0] + event.target.offsetLeft)) {
                                            ui.size.width = TemplateImage.width - event.target.offsetLeft;
                                        }
                                        else {
                                            ui.size.width = newSize[0];
                                        }
                                        if ((newSize[1] + event.target.offsetTop) > TemplateImage.offsetHeight) {
                                            ui.size.height = TemplateImage.height - event.target.offsetTop;
                                        }
                                        else {
                                            ui.size.height = newSize[1];
                                        }
                                    }
                                }
                            }
                            else if (currentElement.getAttribute("data-tool") == Entities.ElementType.TableColumn.toString()) {
                                var idd = appendTo.replace("#", "");
                                idd = idd.replace(".", "");
                                var offset = 0;
                                var parentElement = document.getElementById(idd);
                                for (var i = 0; i < parentElement.childNodes.length; i++) {
                                    var tbleCol = parentElement.childNodes[i];
                                    if ((!(tbleCol.classList.contains("ui-resizable-handle"))) && (currentElement.id != parentElement.childNodes[i].id)) {
                                        offset += (parseInt(tbleCol.style.width.replace("px", "")));
                                        offset += ($(tbleCol.id).outerWidth() - $(tbleCol.id).innerWidth());
                                    }
                                }
                                var scale = AffinityDms.Entities.FormTestDesigner.prototype.GetScaleValue();
                                var newSize = AffinityDms.Entities.FormTestDesigner.prototype.ResizeOffsetFix(ui, scale);
                                if (offset + newSize[0] <= parentElement.offsetWidth) {
                                    ui.size.width = newSize[0];
                                }
                                else {
                                    ui.size.width = ui.originalSize.width;
                                }
                                ui.size.height = newSize[1];
                            }
                            else {
                                var scale = AffinityDms.Entities.FormTestDesigner.prototype.GetScaleValue();
                                var newSize = AffinityDms.Entities.FormTestDesigner.prototype.ResizeOffsetFix(ui, scale);
                                if (tempType.value == "Template") {
                                    var TemplateImage = document.getElementById("TemplateImage");
                                    if (TemplateImage != null) {
                                        if (scale > 0 && scale <= 1) {
                                            if ((newSize[0] + event.target.offsetLeft) >= TemplateImage.offsetWidth) {
                                                ui.size.width = TemplateImage.width - event.target.offsetLeft;
                                            }
                                            else {
                                                ui.size.width = newSize[0];
                                            }
                                            if ((newSize[1] + event.target.offsetTop) > TemplateImage.offsetHeight) {
                                                ui.size.height = TemplateImage.height - event.target.offsetTop;
                                            }
                                            else {
                                                ui.size.height = newSize[1];
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        stop: function (event, ui) {
                            var currentElement_H = document.getElementById("CurTarget");
                            currentElement_H.value = id;
                            var currentElement = document.getElementById(currentElement_H.value);
                            var tempType = document.getElementById("TempType");
                            if (currentElement.getAttribute("data-tool") == Entities.ElementType.Table.toString()) {
                                var offset = 0;
                                for (var i = 0; i < currentElement.childNodes.length; i++) {
                                    var tbleCol = currentElement.childNodes[i];
                                    if (!(tbleCol.classList.contains("ui-resizable-handle"))) {
                                        offset += (+(parseInt(tbleCol.style.width.replace("px", ""))));
                                        offset += ($(tbleCol.id).outerWidth() - $(tbleCol.id).innerWidth());
                                    }
                                }
                                var scale = AffinityDms.Entities.FormTestDesigner.prototype.GetScaleValue();
                                var newSize = AffinityDms.Entities.FormTestDesigner.prototype.ResizeOffsetFix(ui, scale);
                                if (tempType.value == "Template") {
                                    var TemplateImage = document.getElementById("TemplateImage");
                                    if (TemplateImage != null) {
                                        if ((newSize[0] + event.target.offsetLeft) > TemplateImage.offsetWidth || offset > (newSize[0] + event.target.offsetLeft)) {
                                            ui.size.width = TemplateImage.width - event.target.offsetLeft;
                                        }
                                    }
                                }
                                else {
                                    ui.size.width = newSize[0];
                                }
                                if ((newSize[1] + event.target.offsetTop) > TemplateImage.offsetHeight) {
                                    ui.size.height = TemplateImage.height - event.target.offsetTop;
                                }
                                else {
                                    ui.size.height = newSize[1];
                                }
                            }
                        }
                    });
                }
                newElementDiv.appendTo(appendTo);
                var createdElement = document.getElementById(id);
                if (top != "") {
                    createdElement.style.top = top;
                }
                if (left != "") {
                    createdElement.style.left = left;
                }
                //if (X != "") {
                //    createdElement.x = X;
                //}
                if (classList != null) {
                    for (var i = 0; i < classList.length; i++) {
                        createdElement.classList.add(classList[i]);
                    }
                }
                if (attributes != null) {
                    for (var i = 0; i < attributes.length; i++) {
                        createdElement.setAttribute(attributes[i].key, attributes[i].value);
                    }
                }
                if (childElements != null) {
                    for (var i = 0; i < childElements.length; i++) {
                        createdElement.appendChild(childElements[i]);
                    }
                }
                createdElement.click();
            };
            FormTestDesigner.prototype.OnDrop = function (event) {
                if (this.divelement != null) {
                    if (this.divelement != null) {
                        this.divelement.onmouseup = null;
                        this.divelement = null;
                        this._dragging = false;
                    }
                }
                event.preventDefault();
                var elementType = event.dataTransfer.getData("OnDragStart");
                var divcounter = document.getElementById("divcounter");
                if (this._dragging != false && this._elementId != "") {
                    if (this._elementId == "DivToolLabel") {
                        var styleStr = "left:0px;top:0px;opacity:1;position:absolute";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var lblfield = document.createElement("label");
                        lblfield.textContent = "Label";
                        lblfield.style.paddingRight = "10px";
                        childArray.push(lblfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "auto", "auto", this.GetLeft(event), this.GetTop(event), "", "", true, false, ResizeAxis.None, Entities.ElementType.Label);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolTextBox") {
                        var styleStr = "opacity:1;position:absolute";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Textbox.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-placeholder";
                        attributeKeyVal.value = "Textbox";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal.key = "data-maxLength";
                        attributeKeyVal.value = "55000";
                        attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var lblfield = document.createElement("label");
                        lblfield.textContent = "";
                        lblfield.style.cssFloat = "left";
                        lblfield.style.display = "inline-block";
                        lblfield.style.left = "0px";
                        lblfield.style.top = "0px";
                        lblfield.style.position = "absolute";
                        childArray.push(lblfield);
                        var imgfield = document.createElement("img");
                        imgfield.style.width = "100%";
                        imgfield.style.height = "100%";
                        imgfield.style.opacity = "1";
                        imgfield.style.left = "0px";
                        imgfield.style.top = "0px";
                        imgfield.style.minWidth = "10px";
                        imgfield.style.minHeight = "10px";
                        imgfield.className = "imgbackgroundelement";
                        imgfield.style.backgroundPosition = "50% 50%";
                        imgfield.style.backgroundRepeat = "no-repeat";
                        imgfield.src = "../../Images/Form/textbox.png";
                        childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "200px", "35px", this.GetLeft(event), this.GetTop(event), "", "", true, false, ResizeAxis.Horizontal, Entities.ElementType.Textbox);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolTextArea") {
                        var styleStr = "opacity:1;position:absolute";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Textarea.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-placeholder";
                        attributeKeyVal.value = "Textarea";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal.key = "data-maxLength";
                        attributeKeyVal.value = "55000";
                        attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var lblfield = document.createElement("label");
                        lblfield.textContent = "";
                        lblfield.style.cssFloat = "left";
                        lblfield.style.display = "inline-block";
                        lblfield.style.left = "0px";
                        lblfield.style.top = "0px";
                        lblfield.style.position = "absolute";
                        childArray.push(lblfield);
                        var imgfield = document.createElement("img");
                        imgfield.style.width = "100%";
                        imgfield.style.height = "100%";
                        imgfield.style.opacity = "1";
                        imgfield.style.left = "0px";
                        imgfield.style.top = "0px";
                        imgfield.style.minWidth = "10px";
                        imgfield.style.minHeight = "10px";
                        imgfield.className = "imgbackgroundelement";
                        imgfield.style.backgroundPosition = "50% 50%";
                        imgfield.style.backgroundRepeat = "no-repeat";
                        imgfield.src = "../../Images/Form/textarea.png";
                        childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "100px", "100px", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.Textarea);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolImage") {
                        var styleStr = "opacity:1;position:absolute";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Image.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-image";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal.key = "data-Tag";
                        attributeKeyVal.value = "Image";
                        attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var imgfield = document.createElement("img");
                        imgfield.style.width = "100%";
                        imgfield.style.height = "100%";
                        imgfield.style.opacity = "1";
                        imgfield.style.left = "0px";
                        imgfield.style.top = "0px";
                        imgfield.style.minWidth = "10px";
                        imgfield.style.minHeight = "10px";
                        imgfield.style.backgroundPosition = "50% 50%";
                        imgfield.style.backgroundRepeat = "no-repeat";
                        imgfield.src = "../../Images/Form/ImagePlaceHolder.png";
                        childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "100px", "100px", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.Image);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolRadioButton") {
                        var styleStr = "opacity:1;position:absolute";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Radio.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Description";
                        attributeKeyVal.value = "DemoDescription";
                        attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var lblfield = document.createElement("label");
                        lblfield.textContent = "Label";
                        lblfield.style.cssFloat = "right";
                        lblfield.style.paddingRight = "10px";
                        childArray.push(lblfield);
                        var imgfield = document.createElement("img");
                        imgfield.style.width = "10px";
                        imgfield.style.height = "10px";
                        imgfield.style.margin = "5px 5px 5px 5px";
                        imgfield.style.opacity = "1";
                        imgfield.style.minWidth = "10px";
                        imgfield.style.minHeight = "10px";
                        imgfield.style.backgroundPosition = "50% 50%";
                        imgfield.style.backgroundRepeat = "no-repeat";
                        imgfield.src = "../../Images/Form/radiobutton.png";
                        childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "auto", "auto", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.Radio);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolCheckBox") {
                        var styleStr = "opacity:1;position:absolute;padding-right:12.22px";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Checkbox.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Description";
                        attributeKeyVal.value = "DemoDescription";
                        attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var lblfield = document.createElement("label");
                        lblfield.textContent = "Label";
                        lblfield.style.cssFloat = "right";
                        lblfield.style.paddingRight = "10px";
                        childArray.push(lblfield);
                        var imgfield = document.createElement("img");
                        imgfield.style.width = "10px";
                        imgfield.style.height = "10px";
                        imgfield.style.margin = "5px 5px 5px 5px";
                        imgfield.style.opacity = "1";
                        imgfield.style.minWidth = "10px";
                        imgfield.style.minHeight = "10px";
                        imgfield.style.backgroundPosition = "50% 50%";
                        imgfield.style.backgroundRepeat = "no-repeat";
                        imgfield.src = "../../Images/Form/checkbox.png";
                        childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "auto", "auto", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.Checkbox);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolCircle") {
                        var styleStr = "opacity:0.7;position:absolute;z-index:1500";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Circle.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Description";
                        attributeKeyVal.value = "DemoDescription";
                        attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var imgfield = document.createElement("img");
                        imgfield.style.width = "100%";
                        imgfield.style.height = "100%";
                        imgfield.style.opacity = "1";
                        imgfield.style.left = "0px";
                        imgfield.style.top = "0px";
                        imgfield.style.minWidth = "10px";
                        imgfield.style.minHeight = "10px";
                        imgfield.style.backgroundPosition = "50% 50%";
                        imgfield.style.backgroundRepeat = "no-repeat";
                        imgfield.src = "../../Images/Form/circle.png";
                        childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "35px", "35px", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.Circle);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolLineHorizontal") {
                        var styleStr = "opacity:1;position:absolute;z-index:1500";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.HorizontalLine.toString(); // linehorizontal
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        //attributeKeyVal = new KeyValue();
                        //attributeKeyVal.key = "data-Description";
                        //attributeKeyVal.value = "DemoDescription";
                        //attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var imgfield = document.createElement("img");
                        imgfield.style.width = "100%";
                        imgfield.style.height = "100%";
                        imgfield.style.opacity = "1";
                        imgfield.style.left = "0px";
                        imgfield.style.top = "0px";
                        imgfield.style.minWidth = "10px";
                        imgfield.style.minHeight = "10px";
                        imgfield.style.display = "block";
                        imgfield.style.backgroundPosition = "50% 50%";
                        imgfield.style.backgroundRepeat = "no-repeat";
                        imgfield.src = "../../Images/Form/HorizontalLine.png";
                        childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "35px", "10px", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.HorizontalLine);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolLineVertical") {
                        var styleStr = "opacity:1;position:absolute;z-index:1500";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.VerticalLine.toString(); //"linevertical";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var imgfield = document.createElement("img");
                        imgfield.style.width = "100%";
                        imgfield.style.height = "100%";
                        imgfield.style.opacity = "1";
                        imgfield.style.left = "0px";
                        imgfield.style.top = "0px";
                        imgfield.style.minWidth = "10px";
                        imgfield.style.display = "block";
                        imgfield.style.minHeight = "10px";
                        imgfield.style.backgroundPosition = "50% 50%";
                        imgfield.style.backgroundRepeat = "no-repeat";
                        imgfield.src = "../../Images/Form/VerticalLine.png";
                        childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "10px", "35px", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.VerticalLine);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolRectangle") {
                        var styleStr = "opacity:0.7;position:absolute;z-index:1500";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Rectangle.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var imgfield = document.createElement("img");
                        imgfield.style.width = "100%";
                        imgfield.style.height = "100%";
                        imgfield.style.opacity = "1";
                        imgfield.style.left = "0px";
                        imgfield.style.top = "0px";
                        imgfield.style.minWidth = "10px";
                        imgfield.style.minHeight = "10px";
                        imgfield.style.backgroundPosition = "50% 50%";
                        imgfield.style.backgroundRepeat = "no-repeat";
                        imgfield.src = "../../Images/Form/Rectangle.png";
                        childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "35px", "35px", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.Rectangle);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolBarcode2D") {
                        var styleStr = "opacity:1;position:absolute;";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "FormControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Barcode2D.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Ordinal";
                        attributeKeyVal.value = divcounter.value;
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-image";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-BarcodeText";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tag";
                        attributeKeyVal.value = "Barcode2D";
                        attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var imgfield = document.createElement("img");
                        imgfield.style.width = "100%";
                        imgfield.style.height = "100%";
                        imgfield.style.opacity = "1";
                        imgfield.style.left = "0px";
                        imgfield.style.top = "0px";
                        imgfield.style.minWidth = "10px";
                        imgfield.style.minHeight = "10px";
                        imgfield.style.backgroundPosition = "50% 50%";
                        imgfield.style.backgroundRepeat = "no-repeat";
                        imgfield.src = "../../Images/Form/barcode.png";
                        childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "100px", "100px", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.Barcode2D);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolOcrRectangleZone") {
                        var styleStr = "opacity:0.7;position:absolute;z-index:1500;";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "OCRControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Rectangle.toString();
                        attributeArray.push(attributeKeyVal);
                        //attributeKeyVal = new KeyValue();
                        //attributeKeyVal.key = "data-Ordinal";
                        //attributeKeyVal.value = divcounter.value;
                        //attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementIndexType";
                        attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Value";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        //attributeKeyVal = new KeyValue();
                        //attributeKeyVal.key = "data-image";
                        //attributeKeyVal.value = "";
                        //attributeArray.push(attributeKeyVal);
                        //attributeKeyVal = new KeyValue();
                        //attributeKeyVal.key = "data-BarcodeText";
                        //attributeKeyVal.value = "";
                        //attributeArray.push(attributeKeyVal);
                        //attributeKeyVal = new KeyValue();
                        //attributeKeyVal.key = "data-Tag";
                        //attributeKeyVal.value = "Barcode2D";
                        //attributeArray.push(attributeKeyVal);
                        var childArray = null;
                        //var imgfield = document.createElement("img");
                        //imgfield.style.width = "100%";
                        //imgfield.style.height = "100%";
                        //imgfield.style.opacity = "1";
                        //imgfield.style.left = "0px";
                        //imgfield.style.top = "0px";
                        //imgfield.style.minWidth = "10px";
                        //imgfield.style.minHeight = "10px";
                        //imgfield.style.backgroundPosition = "50% 50%";
                        //imgfield.style.backgroundRepeat = "no-repeat";
                        //imgfield.src = "../../Images/Form/barcode.png";
                        //childArray.push(imgfield);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "100px", "100px", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.Rectangle);
                        this._dragging = false;
                        this._elementId = "";
                        var countter = parseInt(divcounter.value);
                        divcounter.value = (countter + 1) + "";
                    }
                    else if (this._elementId == "DivToolOcrTableZone") {
                        this.createOcrZone();
                    }
                    else if ((elementType.length >= "DivElement".length) && (elementType.substring(0, "DivElement".length - 1))) {
                        //if (event.target.id == "DivPage") {
                        var divPage = document.getElementById("DivPage");
                        var divElement = document.getElementById(elementType);
                        divElement.style.left = ((event.pageX - divPage.offsetLeft) - Math.abs(this.PX)).toString() + "px";
                        divElement.style.top = ((event.pageY - divPage.offsetTop) - Math.abs(this.PY)).toString() + "px";
                    }
                }
            };
            FormTestDesigner.prototype.OnDragOver = function (event) {
                event.preventDefault();
            };
            FormTestDesigner.prototype.OnElementClick = function (event) {
                //this.divelement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                //if (this.divelement == null || this.divelement == undefined)
                //{
                //    this.divelement = <HTMLDivElement>document.getElementById(event.target.id);
                //}
                var fileuploadingdiv = document.getElementById("fileuploader");
                var currentTargetId = document.getElementById("CurTarget");
                //currentTargetId.value = this.divelement.id;
                var id = "";
                if (event.currentTarget.id == null || event.currentTarget.id == undefined) {
                    id = event.target.id;
                }
                else {
                    id = event.currentTarget.id;
                }
                this.divelement = document.getElementById(id);
                if (this.divelement.getAttribute("data-tool") != Entities.ElementType.TableColumn.toString()) {
                    currentTargetId.value = id;
                }
                else {
                    var hiddenDivId = document.getElementById("Hidfind");
                    hiddenDivId.value = id;
                    var AddDeleteColumndiv = document.getElementById("adddeletecolumnbtndivid");
                    AddDeleteColumndiv.style.display = "";
                }
                if (this.divelement.getAttribute("data-ElementType") == "FormControl") {
                    if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.Label.toString()) {
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                    else if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.Textbox.toString()) {
                        //  var inputele: HTMLInputElement = <HTMLInputElement>this.divelement.childNodes[0];
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, true, false, true, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                    else if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.Radio.toString()) {
                        // var inputele: HTMLInputElement = <HTMLInputElement>this.divelement.childNodes[0];
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, true, false, false, false, true);
                    }
                    else if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.Checkbox.toString()) {
                        // var inputele: HTMLInputElement = <HTMLInputElement>this.divelement.childNodes[0];
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, true, false, false, false, true);
                    }
                    else if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.Textarea.toString()) {
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, true, false, true, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                    else if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.Image.toString()) {
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(true, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                    else if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.Barcode2D.toString()) {
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                    else if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.HorizontalLine.toString()) {
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                    else if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.VerticalLine.toString()) {
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                    else if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.Rectangle.toString()) {
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                    else if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.Circle.toString()) {
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                }
                else if (this.divelement.getAttribute("data-ElementType") == "OCRControl") {
                    var searchTable = this.divelement.id.search("DivElementTempRow");
                    if (searchTable == 0) {
                        if (this.divelement instanceof HTMLDivElement) {
                            //currentTargetId.value = event.currentTarget.id;
                            ElementProperties.prototype.DisplayProperties(false, true, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                        }
                    }
                    if (this.divelement.getAttribute("data-Tool") == Entities.ElementType.Rectangle.toString()) {
                        //currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                }
                else {
                    currentTargetId.value = "0";
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    var AddDeletediv = document.getElementById("adddeletebtndivid");
                    AddDeletediv.style.display = "none";
                    var hiddenDivId = document.getElementById("Hidfind");
                    hiddenDivId.value = "0";
                    var AddDeleteColumndiv = document.getElementById("adddeletecolumnbtndivid");
                    AddDeleteColumndiv.style.display = "none";
                }
            };
            //////////////public OnDivElementMouseDown(event): any {
            //////////////    //alert(event.currentTarget.id);
            //////////////    //alert(event.target.id);
            //////////////    //event.preventDefault();
            //////////////    var maindiv;
            //////////////    var mainImagediv: HTMLInputElement = <HTMLInputElement>document.getElementById("TempType");
            //////////////    if (mainImagediv.value == "Template") {
            //////////////        maindiv = <HTMLDivElement>document.getElementById("TemplateImage");
            //////////////    }
            //////////////    else {
            //////////////        maindiv = <HTMLDivElement>document.getElementById("DivPage");
            //////////////    }
            //////////////    this.divelement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////    var fileuploadingdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("fileuploader");
            //////////////    var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            //////////////    if (this.divelement.getAttribute("data-ElementType") == "FormControl") {
            //////////////        if (this.divelement.getAttribute("data-Tool").toLowerCase() == "label") {
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        }
            //////////////        else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "textbox") {
            //////////////            //  var inputele: HTMLInputElement = <HTMLInputElement>this.divelement.childNodes[0];
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, true, false, true, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        }
            //////////////        else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "radio") {
            //////////////            // var inputele: HTMLInputElement = <HTMLInputElement>this.divelement.childNodes[0];
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, true, false, false, false, true);
            //////////////        }
            //////////////        else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "checkbox") {
            //////////////            // var inputele: HTMLInputElement = <HTMLInputElement>this.divelement.childNodes[0];
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, true, false, false, false, true);
            //////////////        }
            //////////////        else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "textarea") {
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, true, false, true, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        }
            //////////////        else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "image") {
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(true, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        }
            //////////////        else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "barcode2d") {
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        }
            //////////////        //else if (this.divelement.childNodes[0].nodeName.toLowerCase() == "svg") {
            //////////////        //    var svg = this.divelement.childNodes[0];
            //////////////        else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "linehorizontal") {
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        }
            //////////////        else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "linevertical") {
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        }
            //////////////        else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "rectangle") {
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        }
            //////////////        else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "circle") {
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        }
            //////////////        //}
            //////////////    }
            //////////////    else if (this.divelement.getAttribute("data-ElementType") == "OCRControl") {
            //////////////        var searchTable = this.divelement.id.search("DivElementTempRow");
            //////////////        if (searchTable == 0) {
            //////////////            if (this.divelement instanceof HTMLDivElement) {
            //////////////                currentTargetId.value = event.currentTarget.id;
            //////////////                ElementProperties.prototype.DisplayProperties(false, true, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////            }
            //////////////        }
            //////////////        if (this.divelement.getAttribute("data-Tool").toLowerCase() == "rectangle") {// default rectangle we should change all these too enums
            //////////////            currentTargetId.value = event.currentTarget.id;
            //////////////            ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        }
            //////////////    }
            //////////////    //}
            //////////////    else {
            //////////////        currentTargetId.value = "0";
            //////////////        ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
            //////////////        var AddDeletediv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletebtndivid");
            //////////////        AddDeletediv.style.display = "none";
            //////////////        var hiddenDivId = <HTMLInputElement>document.getElementById("Hidfind");
            //////////////        hiddenDivId.value = "0";
            //////////////        var AddDeleteColumndiv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletecolumnbtndivid");
            //////////////        AddDeleteColumndiv.style.display = "none";
            //////////////    }
            //////////////    ////else {
            //////////////    ////    //fileuploadingdiv.style.display = "none";
            //////////////    ////    //// this.select = event.currentTarget.id;
            //////////////    ////    ////this.selectedElement = <HTMLImageElement>this.divelement.childNodes[0];
            //////////////    ////    //var devilIdea = <HTMLInputElement>document.getElementById("Hidfind");
            //////////////    ////    //devilIdea.value = "0";
            //////////////    ////}
            //////////////    //if (this.divelement.getAttribute("data-ElementType") == "FormControl") {
            //////////////    //    //if (this.divelement.childNodes[0] instanceof HTMLImageElement) {
            //////////////    //    //    currentTargetId.value = event.currentTarget.id;
            //////////////    //    //}
            //////////////    //    //else if (this.divelement.childNodes[0] instanceof HTMLLabelElement) {
            //////////////    //    //    currentTargetId.value = event.currentTarget.id;
            //////////////    //    //}
            //////////////    //    //else if (this.divelement.childNodes[0] instanceof HTMLTextAreaElement) {
            //////////////    //    //    currentTargetId.value = event.currentTarget.id;
            //////////////    //    //}
            //////////////    //    //else if (this.divelement.childNodes[0] instanceof HTMLInputElement) {
            //////////////    //    //    var elem = <HTMLInputElement>this.divelement.childNodes[0];
            //////////////    //    //    if (elem.type == "text" || elem.type == "radio" || elem.type == "checkbox") {
            //////////////    //    //        currentTargetId.value = event.currentTarget.id;
            //////////////    //    //    }
            //////////////    //    //}
            //////////////    //    currentTargetId.value = event.currentTarget.id;
            //////////////    //}
            //////////////    maindiv.onmousemove = (events: any) => {
            //////////////        var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            //////////////        var divelements = <HTMLDivElement>document.getElementById(currentTargetId.value);
            //////////////        var H_TemplateType = <HTMLInputElement>document.getElementById("TempType");
            //////////////        if (H_TemplateType != null && ((H_TemplateType.value == "Form") || (H_TemplateType.value == "Template"))) {
            //////////////            if (divelements != null && this._dragging == true) {
            //////////////                if (H_TemplateType.value == "Form") {
            //////////////                    var AddXdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addxdivid");
            //////////////                    var AddYdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addydivid");
            //////////////                    var leftpane: HTMLDivElement = <HTMLDivElement>document.getElementById("LeftPane");
            //////////////                    //if (leftpane != null) {
            //////////////                    //    var menu: HTMLUListElement = <HTMLUListElement>leftpane.childNodes[1];
            //////////////                    //    if (menu.style.display == "" || menu.style.display == "block") {
            //////////////                    //        divelements.style.left = (((events.pageX - maindiv.offsetLeft) - leftpane.clientWidth) - (divelements.clientWidth / 2)) + 'px';
            //////////////                    //        divelements.style.top = ((events.pageY - (maindiv.parentElement.offsetTop)) - (divelements.clientHeight / 2)) + 'px';
            //////////////                    //    }
            //////////////                    //    else {
            //////////////                    //        divelements.style.left = ((events.pageX - (maindiv.offsetLeft)) - (divelements.clientWidth / 2)) + 'px';
            //////////////                    //        divelements.style.top = ((events.pageY - (maindiv.parentElement.offsetTop)) - (divelements.clientHeight / 2)) + 'px';
            //////////////                    //    }
            //////////////                    //}
            //////////////                    //else {
            //////////////                    //    divelements.style.left = ((events.pageX - (maindiv.offsetLeft)) - (divelements.clientWidth / 2)) + 'px';
            //////////////                    //    divelements.style.top = ((events.pageY - (maindiv.parentElement.offsetTop)) - (divelements.clientHeight / 2)) + 'px';
            //////////////                    //}
            //////////////                    var ParentElement: HTMLElement = maindiv;
            //////////////                    var left: number = 0;
            //////////////                    var top: number = 0;
            //////////////                    while (ParentElement != null) {
            //////////////                        top += (ParentElement.offsetTop);
            //////////////                        left += (ParentElement.offsetLeft);
            //////////////                        ParentElement = ParentElement.parentElement;
            //////////////                    }
            //////////////                    divelements.style.left = (events.pageX - left) + "px"; //(((events.pageX - maindiv.offsetLeft) - leftpane.clientWidth) - (divelements.clientWidth / 2)) + 'px';
            //////////////                    divelements.style.top = (events.pageY - top) + "px"; //((events.pageY - (maindiv.parentElement.offsetTop)) - (divelements.clientHeight / 2)) + 'px';
            //////////////                    if (AddXdiv.style.display != "none") {
            //////////////                        var AddX: HTMLInputElement = <HTMLInputElement>document.getElementById("addxid");
            //////////////                        AddX.value = divelements.style.left.replace("px", "");
            //////////////                    }
            //////////////                    if (AddYdiv.style.display != "none") {
            //////////////                        var AddY: HTMLInputElement = <HTMLInputElement>document.getElementById("addyid");
            //////////////                        AddY.value = divelements.style.top.replace("px", "");
            //////////////                    }
            //////////////                    maindiv.onmouseup = (event) => {
            //////////////                        maindiv.onmousemove = null;
            //////////////                    }
            //////////////                }
            //////////////                else if (H_TemplateType.value == "Template") {
            //////////////                    var AddXdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addxdivid");
            //////////////                    var AddYdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addydivid");
            //////////////                    var leftpane: HTMLDivElement = <HTMLDivElement>document.getElementById("LeftPane");
            //////////////                    var ToolBoxDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivToolBox");
            //////////////                    var mainPage: HTMLDivElement = <HTMLDivElement>maindiv.offsetParent;
            //////////////                    if (leftpane != null) {
            //////////////                        var menu: HTMLUListElement = <HTMLUListElement>leftpane.childNodes[1];
            //////////////                        if (menu.style.display.toLowerCase() == "none") {
            //////////////                            if (events.offsetX != 0 || events.offsetY != 0) {
            //////////////                                divelements.style.left = ((events.offsetX)) + "px";//((events.pageX - maindiv.parentElement.offsetLeft) - (divelements.clientWidth / 2)) + "px";//events.offsetX + "px";//- (mainPage.offsetLeft)+ "px";//((events.pageX - maindiv.parentElement.offsetLeft) - (divelements.clientWidth / 2)) + "px";// - (divelements.clientWidth / 2))  + "px";
            //////////////                                divelements.style.top = ((events.offsetY)) + "px";//((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";// - (mainPage.offsetTop) + "px";//((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";// - (divelements.clientHeight / 2))  + "px";
            //////////////                            }
            //////////////                        }
            //////////////                        else {
            //////////////                            if (events.offsetX != 0 || events.offsetY != 0) {
            //////////////                                divelements.style.left = ((events.offsetX)) + "px";//(((events.pageX - maindiv.parentElement.offsetLeft) - leftpane.clientWidth) - (divelements.clientWidth / 2)) + "px";//events.offsetX + "px";// - (mainPage.offsetLeft + leftpane.clientWidth) + "px";// (((events.pageX - maindiv.parentElement.offsetLeft) - leftpane.clientWidth) - (divelements.clientWidth / 2)) + "px";//- (divelements.clientWidth / 2)) + "px";
            //////////////                                divelements.style.top = ((events.offsetY)) + "px";//((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";// events.offsetY + "px";//- (mainPage.offsetTop) + "px";// ((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";//- (divelements.clientHeight / 2)) + "px";
            //////////////                            }
            //////////////                        }
            //////////////                    }
            //////////////                    else {
            //////////////                        if (events.offsetX != 0 || events.offsetY != 0) {
            //////////////                            divelements.style.left = ((events.offsetX)) + "px";//((events.pageX - maindiv.parentElement.offsetLeft) - (divelements.clientWidth / 2)) + "px";//events.offsetX + "px";//- (mainPage.offsetLeft + leftpane.clientWidth) + "px";// ((events.pageX - maindiv.parentElement.offsetLeft) - (divelements.clientWidth / 2)) + "px";//- (divelements.clientWidth / 2)) + "px";
            //////////////                            divelements.style.top = ((events.offsetY)) + "px";//((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";//events.offsetY + "px";//- (mainPage.offsetTop) + "px";//((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";//+ (divelements.clientHeight / 2)) + "px";
            //////////////                        }
            //////////////                    }
            //////////////                    if (AddXdiv.style.display != "none") {
            //////////////                        var AddX: HTMLInputElement = <HTMLInputElement>document.getElementById("addxid");
            //////////////                        AddX.value = divelements.style.left.replace("px", "");
            //////////////                    }
            //////////////                    if (AddYdiv.style.display != "none") {
            //////////////                        var AddY: HTMLInputElement = <HTMLInputElement>document.getElementById("addyid");
            //////////////                        AddY.value = divelements.style.top.replace("px", "");
            //////////////                    }
            //////////////                    maindiv.onmouseup = (event) => {
            //////////////                        maindiv.onmousemove = null;
            //////////////                    }
            //////////////                }
            //////////////            }
            //////////////        }
            //////////////    }
            //////////////}
            //////////////public OnDivElementMouseUp(event): void {
            //////////////    if (this.divelement != null) {
            //////////////        this.divelement.onmouseup = null;
            //////////////        this.divelement = null;
            //////////////    }
            //////////////}
            //////////////public OnDivElementMouseOut(event): void {
            //////////////    if (this.divelement != null) {
            //////////////        this.divelement.onmouseup = null;
            //////////////        this.divelement = null;
            //////////////    }
            //////////////}
            FormTestDesigner.prototype.ActiveInactive = function (event) {
                var activeinactive = false;
                if (event.currentTarget.id == "btnActive") {
                    activeinactive = true;
                }
                else if (event.currentTarget.id == "btnInactive") {
                    activeinactive = false;
                }
                var H_Templateid = document.getElementById("H_Templateid");
                var H_TemplateidVal = -1;
                if (H_Templateid != null) {
                    H_TemplateidVal = parseInt(H_Templateid.value);
                }
                var templatetype = document.getElementById("TempType");
                var strurl = "";
                if (templatetype.value == "Form") {
                    strurl = "../../TenantFormDesign/ActiveInactive";
                }
                else if (templatetype.value == "Template") {
                    strurl = "../../TenantTemplateDesign/ActiveInactive";
                }
                $.ajax({
                    type: "POST",
                    url: strurl,
                    data: JSON.stringify({ templateId: H_TemplateidVal, activeInactive: activeinactive }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (activeChanged) {
                        var btnActive = document.getElementById("btnActive");
                        var btnInactive = document.getElementById("btnInactive");
                        if (activeChanged.toString().toLowerCase() == "true") {
                            if (activeinactive) {
                                btnActive.style.display = "none";
                                btnInactive.style.display = "block";
                            }
                            else {
                                btnInactive.style.display = "none";
                                btnActive.style.display = "block";
                            }
                            alert("Settings successfully updated!!");
                        }
                        else if (activeChanged.toString().toLowerCase() == "false") {
                            var status = "";
                            if (activeinactive) {
                                status = "Active";
                                btnInactive.style.display = "block";
                                btnActive.style.display = "none";
                            }
                            else {
                                status = "Inactive";
                                btnActive.style.display = "block";
                                btnInactive.style.display = "none";
                            }
                            alert("Current Template is already " + status);
                        }
                        else {
                            alert(activeChanged.toString());
                        }
                    },
                    error: function (responseText) {
                        alert("Oops an Error Occured");
                    }
                });
            };
            FormTestDesigner.prototype.onCheckin = function (event) {
                var H_Templateid = document.getElementById("H_Templateid");
                var H_TemplateidVal = -1;
                if (H_Templateid != null) {
                    H_TemplateidVal = parseInt(H_Templateid.value);
                }
                var templatetype = document.getElementById("TempType");
                var strurl = "";
                if (templatetype.value == "Form") {
                    strurl = "../../TenantFormDesign/Checkin";
                }
                else if (templatetype.value == "Template") {
                    strurl = "../../TenantTemplateDesign/Checkin";
                }
                $.ajax({
                    type: "POST",
                    url: strurl,
                    data: JSON.stringify({ templateId: H_TemplateidVal }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText) {
                        if (responseText.toString().toLowerCase() == "true") {
                            var btncheckin = document.getElementById("btnCheckin");
                            btncheckin.style.display = "none";
                            alert("Check In successfull!!");
                        }
                        else if (responseText.toString().toLowerCase() == "false") {
                            var btncheckin = document.getElementById("btnCheckin");
                            btncheckin.style.display = "block";
                        }
                        else {
                            alert("An Error Occurred " + responseText.toString());
                        }
                    },
                    error: function (responseText) {
                        alert("Oops an Error Occured");
                    }
                });
            };
            FormTestDesigner.prototype.onVersionSubmit = function (event) {
                var majortext = document.getElementById("VMajor");
                var minortext = document.getElementById("VMinor");
                var H_Templateid = document.getElementById("H_Templateid");
                var H_TemplateidVal = -1;
                if (H_Templateid != null) {
                    H_TemplateidVal = parseInt(H_Templateid.value);
                }
                var templatetype = document.getElementById("TempType");
                var strurl = "";
                if (templatetype.value == "Form") {
                    strurl = "../../TenantFormDesign/UpdateVersion";
                }
                else if (templatetype.value == "Template") {
                    strurl = "../../TenantTemplateDesign/UpdateVersion";
                }
                var major = parseInt(majortext.value);
                var minor = parseInt(minortext.value);
                $.ajax({
                    type: "POST",
                    url: strurl,
                    data: JSON.stringify({ templateId: H_TemplateidVal, verMajor: major, verMinor: minor }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText) {
                        if (responseText.toString().toLowerCase() == "true") {
                            alert("Version successfully updated!!");
                        }
                        else {
                            alert(responseText.toString());
                        }
                    },
                    error: function (responseText) {
                        alert("Oops an Error Occured: " + responseText.toString());
                    }
                });
            };
            //public onSubmitSettings(event): void {
            //    var finalid = <HTMLInputElement>document.getElementById("DesignFinalize");
            //    var statusid = <HTMLInputElement>document.getElementById("DesignStatus");
            //    var isfinal: boolean = finalid.checked;
            //    var isactive: boolean = statusid.checked;
            //    var H_Templateid: HTMLInputElement = <HTMLInputElement>document.getElementById("H_Templateid")
            //    var H_TemplateidVal = -1;
            //    if (H_Templateid != null) {
            //        H_TemplateidVal = parseInt(H_Templateid.value);
            //    }
            //    var templatetype = <HTMLInputElement>document.getElementById("TempType")
            //    var strurl: string = "";
            //    if (templatetype.value == "Form") {
            //        strurl = "../../TenantFormDesign/UpdateTemplateSettings"
            //    }
            //    else if (templatetype.value == "Template") {
            //        strurl = "../../TenantTemplateDesign/UpdateTemplateSettings"
            //    }
            //    $.ajax({
            //        type: "POST",
            //        url: strurl,
            //        data: JSON.stringify({ IsFinalized: isfinal, IsActive: isactive, templateid: H_TemplateidVal }),
            //        contentType: "application/json; charset=utf-8",
            //        dataType: 'json',
            //        success: function (responseText: string) {
            //            if (responseText.toString().toLowerCase() == "true") {
            //                alert("Settings successfully updated!!");
            //            }
            //            else
            //            {
            //                alert(responseText.toString());
            //            }
            //        },
            //        error: function (responseText: any) {
            //            alert("Oops an Error Occured");
            //        }
            //    });
            //}
            FormTestDesigner.prototype.onSubmitForm = function (event) {
                // var a = new AffinityDms.Entities.TemplateElement();
                var ElementArray = AffinityDms.Entities.FormTestDesigner.prototype.CreateArrayOfElements();
                if (ElementArray.length <= 0) {
                    alert("No Elements to Save");
                }
                else {
                    var ExElementDetailsArray = AffinityDms.Entities.FormTestDesigner.prototype.CreateArrayOfElementDetails();
                    ElementArray[0].ElementDetails = null;
                    var stringyElementArray = JSON.stringify(ElementArray);
                    var stringyElementDetailsArray = JSON.stringify(ExElementDetailsArray);
                    var H_Templateid = document.getElementById("H_Templateid");
                    var H_TemplateidVal = -1;
                    if (H_Templateid != null) {
                        H_TemplateidVal = parseInt(H_Templateid.value);
                    }
                    var templatetype = document.getElementById("TempType");
                    var strurl = "";
                    if (templatetype.value == "Form") {
                        strurl = "../../TenantFormDesign/SaveFormDesign";
                    }
                    else if (templatetype.value == "Template") {
                        strurl = "../../TenantTemplateDesign/SaveTemplateDesign";
                    }
                    $.ajax({
                        type: "POST",
                        url: strurl,
                        data: JSON.stringify({ Elements: stringyElementArray, ElementDetails: stringyElementDetailsArray, templateid: H_TemplateidVal }),
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        success: function (responseText) {
                            if (responseText.toString().toLowerCase() == "true") {
                                alert("Template Saved Successfully!!");
                                var templatetype = document.getElementById("TempType");
                                if (templatetype.value == "Template") {
                                    window.location.href = "/Tenants/Templates";
                                }
                                else if (templatetype.value == "Form") {
                                    window.location.href = "/Tenants/Forms";
                                }
                            }
                            else {
                                alert("Unable to perform save operation");
                            }
                        },
                        error: function (responseText) {
                            alert("Oops an Error Occured");
                        }
                    });
                }
            };
            //public elementsArray: Array<AffinityDms.Entities.Element> = new Array();
            //}
            FormTestDesigner.prototype.SetTemplateElementDetailViewModelArray = function (Id, ElementId, ElementDivId, ElementDetailId, ElementType, Name, Description, Text, X, Y, Width, Height, ForegroundColor, BackgroundColor, BorderStyle, Value, SizeMode) {
                // var elementclass = new AffinityDms.Entities.TemplateElementDetail();
                var elementclass = new TemplateElementDetailViewModel();
                elementclass.TemplateElementDetail.Id = Id;
                elementclass.ElementDivID = ElementDivId;
                elementclass.TemplateElementDetail.ElementId = ElementId;
                //elementclass.TemplateElementDetail.TemplateId = TemplateId;
                elementclass.TemplateElementDetail.ElementDetailId = ElementDetailId;
                elementclass.TemplateElementDetail.ElementType = ElementType;
                elementclass.TemplateElementDetail.Name = Name;
                elementclass.TemplateElementDetail.Description = Description;
                elementclass.TemplateElementDetail.Text = Text;
                elementclass.TemplateElementDetail.X = X;
                elementclass.TemplateElementDetail.Y = Y;
                elementclass.TemplateElementDetail.Width = Width;
                elementclass.TemplateElementDetail.Height = Height;
                elementclass.TemplateElementDetail.ForegroundColor = ForegroundColor;
                elementclass.TemplateElementDetail.BackgroundColor = BackgroundColor;
                elementclass.TemplateElementDetail.BorderStyle = BorderStyle;
                elementclass.TemplateElementDetail.Value = Value;
                elementclass.TemplateElementDetail.SizeMode = SizeMode;
                return elementclass;
            };
            FormTestDesigner.prototype.SetElementArray = function (Id, TemplateId, ElementId, ElementType, ElementDataType, Name, Description, Text, X, Y, X2, Y2, Width, Height, DivX, DivY, DivWidth, DivHeight, MinHeight, MinWidth, ForegroundColor, BackgroundColor, Hint, MinChar, MaxChar, DateTime, FontName, FontSize, FontStyle, FontColor, BorderStyle, BarcodeType, Value, ElementIndexType, ImageSource, SizeMode, IsSelected, Discriminator, FontGraphicsUnit, ColorForegroundA, ColorForegroundR, ColorForegroundG, ColorForegroundB, ColorBackgroundA, ColorBackgroundR, ColorBackgroundG, ColorBackgroundB, ElementMobileOrdinal, ElementValues, ElementDetails) {
                var elementclass = new Entities.TemplateElement();
                elementclass.Id = Id;
                elementclass.TemplateId = TemplateId;
                elementclass.ElementId = ElementId;
                elementclass.ElementType = ElementType;
                elementclass.ElementDataType = ElementDataType;
                elementclass.Name = Name;
                elementclass.Description = Description;
                elementclass.Text = Text;
                elementclass.X = X;
                elementclass.Y = Y;
                elementclass.X2 = X2;
                elementclass.Y2 = Y2;
                elementclass.Width = Width;
                elementclass.Height = Height;
                elementclass.DivX = DivX;
                elementclass.DivY = DivY;
                elementclass.DivWidth = DivWidth;
                elementclass.DivHeight = DivHeight;
                elementclass.MinHeight = MinHeight;
                elementclass.MinWidth = MinWidth;
                elementclass.ForegroundColor = ForegroundColor;
                elementclass.BackgroundColor = BackgroundColor;
                elementclass.Hint = Hint;
                elementclass.MinChar = MinChar;
                elementclass.MaxChar = MaxChar;
                elementclass.DateTime = DateTime;
                elementclass.FontName = FontName;
                elementclass.FontSize = FontSize;
                elementclass.FontStyle = FontStyle;
                elementclass.FontColor = FontColor;
                elementclass.BorderStyle = BorderStyle;
                elementclass.BarcodeType = BarcodeType;
                elementclass.ElementIndexType = ElementIndexType;
                elementclass.Value = "";
                if (ElementIndexType == AffinityDms.Entities.ElementIndexType.Label) {
                    elementclass.Value = Value;
                    elementclass.DocumentIndexDataType = 0;
                }
                else if (ElementIndexType == AffinityDms.Entities.ElementIndexType.Value) {
                    elementclass.Value = "";
                    var index = parseInt(Value);
                    if (index >= 0) {
                        elementclass.DocumentIndexDataType = index;
                    }
                    else {
                        elementclass.DocumentIndexDataType = 0;
                    }
                }
                elementclass.ImageSource = ImageSource;
                elementclass.SizeMode = SizeMode;
                elementclass.IsSelected = IsSelected;
                elementclass.Discriminator = Discriminator;
                elementclass.FontGraphicsUnit = FontGraphicsUnit;
                elementclass.ColorForegroundA = ColorForegroundA;
                elementclass.ColorForegroundR = ColorForegroundR;
                elementclass.ColorForegroundG = ColorForegroundG;
                elementclass.ColorForegroundB = ColorForegroundB;
                elementclass.ColorBackroundA = ColorBackgroundA;
                elementclass.ColorBackroundR = ColorBackgroundR;
                elementclass.ColorBackroundG = ColorBackgroundG;
                elementclass.ColorBackroundB = ColorBackgroundB;
                elementclass.ElementMobileOrdinal = ElementMobileOrdinal;
                if (ElementValues == null) {
                    var elementValuesArray = new Array();
                    elementclass.ElementValues = elementValuesArray;
                }
                else {
                    elementclass.ElementValues = ElementValues;
                }
                if (ElementDetails == null) {
                    var elementDetailsArray = new Array();
                    elementclass.ElementDetails = elementDetailsArray;
                }
                else {
                    elementclass.ElementDetails = ElementDetails;
                }
                return elementclass;
            };
            //public TemplateDataPostingArray(event): any {
            //    var inputeletempNo = document.getElementsByName("TemplateNo");
            //    var inputeleActive = document.getElementsByName("item.IsActive");
            //    var inputeleFinalize = document.getElementsByName("item.IsFinalized");
            //    //var inputeleFinalize = document.getElementsByName("item.IsFinalized");
            //    var nestedArray: Array<Array<string>> = new Array();
            //    for (var i = 0; i < inputeleActive.length; i++) {
            //        if (i % 2 == 0) {
            //            var temparray: Array<string> = new Array();
            //            var inputeleActiveValue = <HTMLInputElement>inputeleActive[i];
            //            var inputeleFinalizeValue = <HTMLInputElement>inputeleFinalize[i];
            //            //if (i != 0) {
            //            //    temparray.push(inputeletempNo[i - 1].textContent);
            //            //}
            //            //else {
            //            temparray.push(inputeletempNo[i / 2].textContent);
            //            //}
            //            //temparray.push(inputeletempNo[i].textContent);
            //            temparray.push("" + inputeleFinalizeValue.checked);
            //            temparray.push("" + inputeleActiveValue.checked);
            //            nestedArray.push(temparray);
            //        }
            //        //}
            //    }
            //    $.ajax({
            //        type: "POST",
            //        url: "/FormDesigner/TemplateListing",
            //        data: JSON.stringify(nestedArray),
            //        contentType: "application/json; charset=utf-8",
            //        dataType: 'json',
            //        success: function (responseText: string) {
            //            if (responseText.toString().search("An Error Occured:") == 0) {
            //                var messdiv = <HTMLDivElement>document.getElementById("MessageDiv");
            //                messdiv.style.color = "Red";
            //                messdiv.textContent = responseText.toString();
            //                messdiv.style.fontSize = "1.5em";
            //            }
            //            else {
            //                window.location.href = "/FormDesigner/TemplateListing";
            //                //var messdiv = <HTMLDivElement>document.getElementById("MessageDiv");
            //                //messdiv.textContent = responseText.toString();
            //                //messdiv.style.color = "Green";
            //                //messdiv.style.fontSize = "1.5em";
            //            }
            //        },
            //        error: function (responseText) {
            //        }
            //    });
            //}
            FormTestDesigner.prototype.CreateArrayOfElements = function () {
                //this.elementDetailArray = new Array();
                var count = 0;
                var elementsArray = new Array();
                var divPage = document.getElementById("DivPage");
                var templateid = -1;
                var discriminator = "Descriminator Default Value";
                for (var i = 0; i < divPage.childNodes.length; i++) {
                    var elementclass = new Entities.TemplateElement();
                    if (divPage.childNodes[i] instanceof (HTMLDivElement)) {
                        var divElement = divPage.childNodes[i];
                        if (divElement.getAttribute("data-elementtype") == "FormControl") {
                            if (divElement.getAttribute("data-Tool") == Entities.ElementType.Label.toString()) {
                                // var lblelement: HTMLLabelElement = <HTMLLabelElement>divElement.childNodes[0];
                                //var calcwidth: number = (+(divElement.style.width.replace("px", "")));
                                //var calcheight: number = (+(divElement.style.height.replace("px", "")));
                                var calcX = (+(divElement.style.left.replace("px", "")));
                                var calcY = (+(divElement.style.top.replace("px", "")));
                                var calcDivX = +(divElement.style.left.replace("px", ""));
                                var calcDivY = +(divElement.style.top.replace("px", ""));
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Label, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "", divElement.childNodes[0].textContent.toString(), calcX, calcY, -1, -1, divElement.offsetWidth.toString(), divElement.offsetHeight.toString(), calcDivX, calcDivY, divElement.offsetWidth.toString(), divElement.offsetHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, "", "", "", "", divElement.style.fontFamily, (+divElement.style.fontSize.replace("px", "")), -1, divElement.style.color, -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.Textbox.toString()) {
                                var calcwidth = (+(divElement.style.width.replace("px", "")));
                                var calcheight = (+(divElement.style.height.replace("px", "")));
                                var calcX = (+(divElement.style.left.replace("px", "")));
                                var calcY = (+(divElement.style.top.replace("px", "")));
                                var calcDivX = +(divElement.style.left.replace("px", ""));
                                var calcDivY = +(divElement.style.top.replace("px", ""));
                                var img = divElement.childNodes[1];
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Textbox, Entities.ElementDataType.Alphanumeric, divElement.getAttribute("data-name").toString(), "", "", calcX, calcY, -1, -1, calcwidth.toString(), calcheight.toString(), calcDivX, calcDivY, img.offsetWidth.toString(), img.offsetHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, divElement.getAttribute("data-placeholder").toString(), "", divElement.getAttribute("data-maxLength").toString(), "", divElement.style.fontFamily, (+divElement.style.fontSize.replace("px", "")), -1, divElement.style.color, -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.Radio.toString()) {
                                //   var radiolbl: HTMLLabelElement = <HTMLLabelElement>divElement.childNodes[1]
                                var calcX = (+(divElement.style.left.replace("px", "")));
                                var calcY = (+(divElement.style.top.replace("px", "")));
                                var calcDivX = (+(divElement.style.left.replace("px", "")));
                                var calcDivY = (+(divElement.style.top.replace("px", "")));
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Radio, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), divElement.getAttribute("data-Description"), divElement.childNodes[0].textContent.toString(), calcX, calcY, -1, -1, divElement.offsetWidth.toString(), divElement.offsetHeight.toString(), calcDivX, calcDivY, divElement.offsetWidth.toString(), divElement.offsetHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, "", "", "", "", divElement.style.fontFamily, (+divElement.style.fontSize.replace("px", "")), -1, divElement.style.color, -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, ("" + divElement.getAttribute("data-checked")), discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.Checkbox.toString()) {
                                var calcX = (+(divElement.style.left.replace("px", "")));
                                var calcY = (+(divElement.style.top.replace("px", "")));
                                var calcDivX = +(divElement.style.left.replace("px", ""));
                                var calcDivY = +(divElement.style.top.replace("px", ""));
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Checkbox, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), divElement.getAttribute("data-Description"), divElement.childNodes[0].textContent.toString(), calcX, calcY, -1, -1, divElement.offsetWidth.toString(), divElement.offsetHeight.toString(), calcDivX, calcDivY, divElement.offsetWidth.toString(), divElement.offsetHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, "", "", "", "", divElement.style.fontFamily, (+divElement.style.fontSize.replace("px", "")), -1, divElement.style.color, -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, ("" + divElement.getAttribute("data-checked")), discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.Textarea.toString()) {
                                //   var textareaelement: HTMLInputElement = <HTMLInputElement>divElement.childNodes[0];
                                var calcwidth = (+(divElement.style.width.replace("px", "")));
                                var calcheight = (+(divElement.style.height.replace("px", "")));
                                var calcX = (+(divElement.style.left.replace("px", "")));
                                var calcY = (+(divElement.style.top.replace("px", "")));
                                var calcDivX = +(divElement.style.left.replace("px", ""));
                                var calcDivY = +(divElement.style.top.replace("px", ""));
                                var img = divElement.childNodes[1];
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Textarea, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "", "", calcX, calcY, -1, -1, calcwidth.toString(), calcheight.toString(), calcDivX, calcDivY, img.offsetWidth.toString(), img.offsetHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, divElement.getAttribute("data-placeholder").toString(), "", divElement.getAttribute("data-maxLength").toString(), "", divElement.style.fontFamily, (+divElement.style.fontSize.replace("px", "")), -1, divElement.style.color, -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.Image.toString()) {
                                var calcwidth = (+(divElement.style.width.replace("px", "")));
                                var calcheight = (+(divElement.style.height.replace("px", "")));
                                var calcX = (+(divElement.style.left.replace("px", "")));
                                var calcY = (+(divElement.style.top.replace("px", "")));
                                var calcDivX = +(divElement.style.left.replace("px", ""));
                                var calcDivY = +(divElement.style.top.replace("px", ""));
                                var img = divElement.childNodes[0];
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Image, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), divElement.title.toString(), "", calcX, calcY, -1, -1, calcwidth.toString(), calcheight.toString(), calcDivX, calcDivY, divElement.offsetWidth.toString(), divElement.offsetHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), img.getAttribute("src"), -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.Barcode2D.toString()) {
                                var calcwidth = (+(divElement.style.width.replace("px", "")));
                                var calcheight = (+(divElement.style.height.replace("px", "")));
                                var calcX = (+(divElement.style.left.replace("px", "")));
                                var calcY = (+(divElement.style.top.replace("px", "")));
                                var calcDivX = +(divElement.style.left.replace("px", ""));
                                var calcDivY = +(divElement.style.top.replace("px", ""));
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Barcode2D, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), divElement.title.toString(), divElement.getAttribute("data-BarcodeText"), calcX, calcY, -1, -1, calcwidth.toString(), calcheight.toString(), calcDivX, calcDivY, divElement.offsetWidth.toString(), divElement.offsetHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), divElement.getAttribute("data-image").toString(), -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.HorizontalLine.toString()) {
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.HorizontalLine, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "LineDescription", "", -1, -1, -1, -1, "0", "0", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.VerticalLine.toString()) {
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.VerticalLine, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "LineDescription", "", -1, -1, -1, -1, "0", "0", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.Rectangle.toString()) {
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Rectangle, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "RectangleDescription", "", -1, -1, -1, -1, "0", "0", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.Circle.toString()) {
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Circle, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "CircleDescription", "", -1, -1, -1, -1, "0", "0", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                                count++;
                                elementsArray.push(elementclass);
                            }
                        }
                        else if (divElement.getAttribute("data-elementtype") == "OCRControl") {
                            //if (divElement.childNodes.length > 0) {
                            //    else if (divElement.childNodes[0] instanceof (HTMLDivElement)) {
                            //        elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), "Table", "", "Table", "TableDescription", "", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), -1, -1, divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), -1, -1, "", "", "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-IndexValueType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, null,  null);
                            //        elementsArray.push(elementclass);
                            //    }
                            //}
                            if (divElement.getAttribute("data-Tool") == Entities.ElementType.Rectangle.toString()) {
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Rectangle, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "RectangleDescription", "", -1, -1, -1, -1, "", "", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, null, null);
                                elementsArray.push(elementclass);
                                count++;
                            }
                            else if (divElement.getAttribute("data-Tool") == Entities.ElementType.Table.toString()) {
                                elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Table, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "TableDescription", "", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), -1, -1, divElement.offsetWidth.toString(), divElement.style.height.replace("px", "").toString(), -1, -1, "", "", "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, null, null);
                                elementsArray.push(elementclass);
                            }
                        }
                    }
                }
                return elementsArray;
            };
            FormTestDesigner.prototype.CreateArrayOfElementDetails = function () {
                var count = 0;
                var elementDetailArray = new Array();
                var divPage = document.getElementById("DivPage");
                for (var i = 0; i < divPage.childNodes.length; i++) {
                    var leftcalc = 0;
                    var leftcalcclient = 0;
                    if (divPage.childNodes[i] instanceof (HTMLDivElement)) {
                        var divElement = divPage.childNodes[i];
                        if ((divElement.getAttribute("data-elementtype") == "OCRControl") && divElement.getAttribute("data-Tool") == Entities.ElementType.Table.toString()) {
                            var elemeid = divElement.id;
                            //.clientWidth
                            for (var j = 0; j < divElement.childNodes.length; j++) {
                                var HTMLtbleRowElement = divElement.childNodes[j];
                                if (HTMLtbleRowElement.hasChildNodes) {
                                    if (!HTMLtbleRowElement.classList.contains("ui-resizable-handle")) {
                                        var HTMLColumnElement = HTMLtbleRowElement.childNodes[0];
                                        var elemDetArr = this.SetTemplateElementDetailViewModelArray(count, -1, elemeid, HTMLtbleRowElement.id, Entities.ElementType.TableColumn, HTMLtbleRowElement.getAttribute("data-name").toString(), "description", HTMLColumnElement.innerText, leftcalc, 0, HTMLtbleRowElement.offsetWidth.toString(), divElement.style.height.replace("px", ""), "", HTMLtbleRowElement.style.backgroundColor.toString(), -1, "", -1); //(((+(HTMLtbleRowElement.style.height.replace("%", ""))) / 100) * (+(divElement.style.height.replace("px", "")))).toString()
                                        count++;
                                        leftcalc = leftcalc + (+(HTMLtbleRowElement.offsetWidth));
                                        elementDetailArray.push(elemDetArr);
                                    }
                                }
                            }
                        }
                    }
                }
                return elementDetailArray;
            };
            FormTestDesigner.prototype.editTemplateDesign = function (elementsviewmodel) {
                //var page = this.TemplateObject.TemplateVersions[0].TemplatePages[0];
                var divPage = document.getElementById("DivPage");
                var elementsVMarray = JSON.parse(elementsviewmodel);
                var elementsarray = elementsVMarray.elements;
                var elementdetailsarray = elementsVMarray.elementsdetails;
                var divcounter = document.getElementById("divcounter");
                var previouscountter = 0;
                for (var i = 0; i < elementsarray.length; i++) {
                    var newcountter = parseInt(divcounter.value);
                    var divPage = document.getElementById("DivPage");
                    if (elementsarray[i].ElementType == Entities.ElementType.Rectangle) {
                        // var divElement: HTMLDivElement = document.createElement("div");
                        var idNumber = (parseInt(divcounter.value) + 1).toString();
                        if (newcountter < (parseInt(idNumber))) {
                            newcountter = parseInt(idNumber);
                        }
                        else {
                            newcountter = parseInt(idNumber);
                        }
                        var styleStr = "opacity:0.7;position:absolute;z-index:1500;";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "OCRControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Rectangle.toString();
                        attributeArray.push(attributeKeyVal);
                        if (elementsarray[i].ElementIndexType == Entities.ElementIndexType.Label) {
                            attributeKeyVal = new KeyValue();
                            attributeKeyVal.key = "data-ElementIndexType";
                            attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                            attributeArray.push(attributeKeyVal);
                            attributeKeyVal = new KeyValue();
                            attributeKeyVal.key = "data-Value";
                            attributeKeyVal.value = elementsarray[i].Value;
                            attributeArray.push(attributeKeyVal);
                        }
                        else if (elementsarray[i].ElementIndexType == Entities.ElementIndexType.Value) {
                            attributeKeyVal = new KeyValue();
                            attributeKeyVal.key = "data-ElementIndexType";
                            attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Value.toString();
                            attributeArray.push(attributeKeyVal);
                            attributeKeyVal = new KeyValue();
                            attributeKeyVal.key = "data-Value";
                            attributeKeyVal.value = elementsarray[i].DocumentIndexDataType.toString();
                            attributeArray.push(attributeKeyVal);
                        }
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = elementsarray[i].Name;
                        attributeArray.push(attributeKeyVal);
                        var childArray = null;
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElement" + idNumber, "#DivPage", classList, styleStr, attributeArray, childArray, elementsarray[i].DivWidth + "px", elementsarray[i].DivHeight + "px", elementsarray[i].DivX + "px", elementsarray[i].DivY + "px", "", "", true, true, ResizeAxis.All, Entities.ElementType.Rectangle);
                        this._dragging = false;
                        this._elementId = "";
                        divcounter.value = idNumber;
                    }
                    else if (elementsarray[i].ElementType == Entities.ElementType.Table) {
                        var divElement = document.createElement("div");
                        var idNumber = (parseInt(divcounter.value) + 1).toString();
                        if (newcountter < (parseInt(idNumber))) {
                            newcountter = parseInt(idNumber);
                        }
                        else {
                            newcountter = parseInt(idNumber);
                        }
                        var styleStr = "opacity:1;position:absolute;z-index:1500;margin:0px;display:inline-block";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "OCRControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.Table.toString();
                        attributeArray.push(attributeKeyVal);
                        //attributeKeyVal = new KeyValue();
                        //attributeKeyVal.key = "data-Ordinal";
                        //attributeKeyVal.value = divcounter.value;
                        //attributeArray.push(attributeKeyVal);
                        //attributeKeyVal = new KeyValue();
                        //attributeKeyVal.key = "data-name";
                        //attributeKeyVal.value = "";
                        //attributeArray.push(attributeKeyVal);
                        if (elementsarray[i].ElementIndexType == Entities.ElementIndexType.Label) {
                            attributeKeyVal = new KeyValue();
                            attributeKeyVal.key = "data-ElementIndexType";
                            attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                            attributeArray.push(attributeKeyVal);
                            attributeKeyVal = new KeyValue();
                            attributeKeyVal.key = "data-Value";
                            attributeKeyVal.value = elementsarray[i].Value;
                            attributeArray.push(attributeKeyVal);
                        }
                        else if (elementsarray[i].ElementIndexType == Entities.ElementIndexType.Value) {
                            attributeKeyVal = new KeyValue();
                            attributeKeyVal.key = "data-ElementIndexType";
                            attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Value.toString();
                            attributeArray.push(attributeKeyVal);
                            attributeKeyVal = new KeyValue();
                            attributeKeyVal.key = "data-Value";
                            attributeKeyVal.value = elementsarray[i].DocumentIndexDataType.toString();
                            attributeArray.push(attributeKeyVal);
                        }
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = elementsarray[i].Name;
                        attributeArray.push(attributeKeyVal);
                        var childArray = null;
                        var classList = new Array();
                        classList.push("ui-resizable");
                        var tempRowId = "DivElementTempRow" + newcountter;
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement(tempRowId, "#DivPage", classList, styleStr, attributeArray, childArray, (elementsarray[i].Width + "px"), (elementsarray[i].Height + "px"), (elementsarray[i].X + "px"), (elementsarray[i].Y + "px"), "", "", true, true, ResizeAxis.All, Entities.ElementType.Table);
                        this._dragging = false;
                        this._elementId = "";
                        this._dragging = false;
                        this._elementId = "";
                        divcounter.value = newcountter.toString();
                        AffinityDms.Entities.FormTestDesigner.prototype.editTemplateTableColumns(elementdetailsarray, tempRowId);
                    }
                }
                divcounter.value = ((parseInt(divcounter.value)) + 1).toString();
            };
            FormTestDesigner.prototype.editTemplateTableColumns = function (elementdetailsarray, tempRowId) {
                for (var j = 0; j < elementdetailsarray.length; j++) {
                    var colcounter = document.getElementById("colcounter");
                    var coloumncounter = 0;
                    if (colcounter.value != "0") {
                        coloumncounter = parseInt(colcounter.value);
                    }
                    // var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
                    var parentTableDiv = document.getElementById(tempRowId);
                    //search if its a table
                    //var search = parentTableDiv.id.search("DivElementTempRow");
                    var styleStr = "display:inline-block;background-color:grey";
                    var attributeArray = new Array();
                    var attributeKeyVal = new KeyValue();
                    attributeKeyVal.key = "data-ElementType";
                    attributeKeyVal.value = "OCRControl";
                    attributeArray.push(attributeKeyVal);
                    attributeKeyVal = new KeyValue();
                    attributeKeyVal.key = "data-Tool";
                    attributeKeyVal.value = Entities.ElementType.TableColumn.toString();
                    attributeArray.push(attributeKeyVal);
                    attributeKeyVal = new KeyValue();
                    attributeKeyVal.key = "data-name";
                    attributeKeyVal.value = elementdetailsarray[j].Name;
                    attributeArray.push(attributeKeyVal);
                    var childArray = new Array();
                    var divElementColumn = document.createElement("div");
                    divElementColumn.id = "DivElementColumn" + coloumncounter; //(parentTableDiv.childNodes.length + 1).toString();
                    divElementColumn.style.display = "inline-block";
                    divElementColumn.style.fontSize = "0.7em !important";
                    divElementColumn.innerText = "DivElementColumn" + coloumncounter; //(parentTableDiv.childNodes.length + 1).toString();
                    divElementColumn.style.height = "80%";
                    divElementColumn.style.wordBreak = "break-all !important";
                    divElementColumn.style["resize"] = "none !important";
                    divElementColumn.style["overflow"] = "hidden !important";
                    divElementColumn.style.textAlign = "center";
                    divElementColumn.style.border = "none !important";
                    childArray.push(divElementColumn);
                    var classList = new Array();
                    classList.push("ui-resizable");
                    AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElementColumnContainerDiv" + coloumncounter, "#" + tempRowId, classList, styleStr, attributeArray, childArray, elementdetailsarray[j].Width + "px", "100%", /*elementdetailsarray[j].X + */ "0px", elementdetailsarray[j].Y + "px", "", "", false, true, ResizeAxis.HorizontalForwardOnly, Entities.ElementType.TableColumn);
                    this._dragging = false;
                    this._elementId = "";
                    colcounter.value = (coloumncounter + 1) + "";
                }
            };
            //////////////public editFormDesign(editelements: any): void {
            //////////////    // var page = this.TemplateObject.TemplateVersions[0].TemplatePages[0];
            //////////////    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////    var uploaddiv: HTMLDivElement = <HTMLDivElement>document.getElementById("fileuploader");
            //////////////    uploaddiv.style.display = "none";
            //////////////    var elementsarray: Array<AffinityDms.Entities.TemplateElement> = JSON.parse(editelements);
            //////////////    var divcounter: HTMLInputElement = <HTMLInputElement>document.getElementById("divcounter");
            //////////////    var previouscountter: number = 0;
            //////////////    for (var i = 0; i < elementsarray.length; i++) {
            //////////////        var fontsize = elementsarray[i].FontSize;
            //////////////        var newcountter: number = parseInt(divcounter.value);
            //////////////        var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////        var divElement: HTMLDivElement = document.createElement("div");
            //////////////        if (elementsarray[i].ElementType == ElementType.Circle) {
            //////////////            //var divElement: HTMLDivElement = document.createElement("div");
            //////////////            //var idNumber: string = (parseInt(divcounter.value) + 1).toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            //else {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;//  (divPage.childNodes.length + 1).toString();
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-Tool", ElementType.Circle.toString());
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.style.width = elementsarray[i].DivWidth + "px";
            //////////////            divElement.style.height = elementsarray[i].DivHeight + "px";
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            // divElement.style.backgroundColor = "#FF0000";
            //////////////            divElement.style.opacity = "0.7";
            //////////////            divElement.draggable = true;
            //////////////            divElement.style.zIndex = "1500";
            //////////////            divElement.style.paddingBottom = "0px";
            //////////////            divElement.style.paddingRight = "0px";
            //////////////            divElement.style["resize"] = "both";
            //////////////            divElement.style["overflow"] = "hidden";
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                // divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var imgfield = document.createElement("img");
            //////////////            imgfield.style.width = "100%";
            //////////////            imgfield.style.height = "100%";
            //////////////            imgfield.style.opacity = "1";
            //////////////            imgfield.style.left = "0px";
            //////////////            imgfield.style.top = "0px";
            //////////////            imgfield.style.minWidth = "10px";
            //////////////            imgfield.style.minHeight = "10px";
            //////////////            imgfield.style.backgroundPosition = "50% 50%";
            //////////////            imgfield.style.backgroundRepeat = "no-repeat";
            //////////////            imgfield.src = "../../Images/Form/circle.png";
            //////////////            divElement.appendChild(imgfield);
            //////////////            divPage.appendChild(divElement);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1).toString();
            //////////////            // divcounter.value = idNumber;
            //////////////        }
            //////////////        else if (elementsarray[i].ElementType == ElementType.HorizontalLine) {
            //////////////            //var divElement: HTMLDivElement = document.createElement("div");
            //////////////            //var idNumber: string = (parseInt(divcounter.value) + 1).toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            //else {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;
            //////////////            divElement.style.zIndex = "1500";
            //////////////            divElement.style.width = elementsarray[i].DivWidth + "px";
            //////////////            divElement.style.height = elementsarray[i].DivHeight + "px";
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            divElement.style.opacity = "1";
            //////////////            divElement.draggable = true;
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-Tool", ElementType.HorizontalLine.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            //divElement.setAttribute("data-IndexValueType", elementsarray[i].ElementIndexType.toString());
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.style["resize"] = "horizontal";
            //////////////            divElement.style["overflow"] = "hidden";
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                // divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var imgfield = document.createElement("img");
            //////////////            imgfield.style.width = "100%";
            //////////////            imgfield.style.height = "100%";
            //////////////            imgfield.style.opacity = "1";
            //////////////            imgfield.style.left = "0px";
            //////////////            imgfield.style.top = "0px";
            //////////////            imgfield.style.minWidth = "10px";
            //////////////            imgfield.style.minHeight = "10px";
            //////////////            imgfield.style.display = "block";
            //////////////            imgfield.style.backgroundPosition = "50% 50%";
            //////////////            imgfield.style.backgroundRepeat = "no-repeat";
            //////////////            imgfield.src = "../../Images/Form/HorizontalLine.png";
            //////////////            divElement.appendChild(imgfield);
            //////////////            divPage.appendChild(divElement);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1).toString();
            //////////////        }
            //////////////        else if (elementsarray[i].ElementType == ElementType.VerticalLine) {
            //////////////            //var divElement: HTMLDivElement = document.createElement("div");
            //////////////            //var idNumber: string = (parseInt(divcounter.value) + 1).toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            //else {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;
            //////////////            divElement.style.zIndex = "1500";
            //////////////            divElement.style.width = elementsarray[i].DivWidth + "px";
            //////////////            divElement.style.height = elementsarray[i].DivHeight + "px";
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            divElement.style.opacity = "1";
            //////////////            divElement.draggable = true;
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-Tool", ElementType.VerticalLine.toString());
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.style["resize"] = "vertical";
            //////////////            divElement.style["overflow"] = "hidden";
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var imgfield = document.createElement("img");
            //////////////            imgfield.style.width = "100%";
            //////////////            imgfield.style.height = "100%";
            //////////////            imgfield.style.opacity = "1";
            //////////////            imgfield.style.left = "0px";
            //////////////            imgfield.style.top = "0px";
            //////////////            imgfield.style.minWidth = "10px";
            //////////////            imgfield.style.display = "block";
            //////////////            imgfield.style.minHeight = "10px";
            //////////////            imgfield.style.backgroundPosition = "50% 50%";
            //////////////            imgfield.style.backgroundRepeat = "no-repeat";
            //////////////            imgfield.src = "../../Images/Form/VerticalLine.png";
            //////////////            divElement.appendChild(imgfield);
            //////////////            divPage.appendChild(divElement);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1).toString();
            //////////////        }
            //////////////        else if (elementsarray[i].ElementType == ElementType.Rectangle) {
            //////////////            //var divElement: HTMLDivElement = document.createElement("div");
            //////////////            //var idNumber: string = (parseInt(divcounter.value) + 1).toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            //else {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;// (divPage.childNodes.length + 1).toString();
            //////////////            divElement.style.zIndex = "1500";
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-Tool", ElementType.Rectangle.toString());
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.style.width = elementsarray[i].DivWidth + "px";
            //////////////            divElement.style.height = elementsarray[i].DivHeight + "px";
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            divElement.style.opacity = "0.7";
            //////////////            divElement.draggable = true;
            //////////////            divElement.style["resize"] = "both";
            //////////////            divElement.style["overflow"] = "hidden";
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var imgfield = document.createElement("img");
            //////////////            imgfield.style.width = "100%";
            //////////////            imgfield.style.height = "100%";
            //////////////            imgfield.style.opacity = "1";
            //////////////            imgfield.style.left = "0px";
            //////////////            imgfield.style.top = "0px";
            //////////////            imgfield.style.minWidth = "10px";
            //////////////            imgfield.style.minHeight = "10px";
            //////////////            imgfield.style.backgroundPosition = "50% 50%";
            //////////////            imgfield.style.backgroundRepeat = "no-repeat";
            //////////////            imgfield.src = "../../Images/Form/Rectangle.png";
            //////////////            divPage.appendChild(divElement);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1).toString();
            //////////////        }
            //////////////        else if (elementsarray[i].ElementType == ElementType.Label) {
            //////////////            //var idNumber: string = elementsarray[i].ElementId.replace("lblElement", "").toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;//
            //////////////            divElement.style.width = "auto";//elementsarray[i].DivWidth;
            //////////////            divElement.style.height = "auto";//elementsarray[i].DivHeight;
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            //  divElement.style.backgroundColor = "#FF0000";
            //////////////            divElement.style.opacity = "1";
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-Tool", ElementType.Label.toString());
            //////////////            divElement.setAttribute("data-image", "");
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.draggable = true;
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                //   var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //   divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                // divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var lblfield = document.createElement("label");
            //////////////            lblfield.textContent = elementsarray[i].Text;
            //////////////            lblfield.style.paddingRight = "10px";
            //////////////            divElement.appendChild(lblfield);
            //////////////            divPage.appendChild(divElement);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1) + "";
            //////////////        }
            //////////////        else if (elementsarray[i].ElementType == ElementType.Textbox) {
            //////////////            //var idNumber: string = elementsarray[i].ElementId.replace("txtbxElement", "").toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;//idNumber;
            //////////////            divElement.style.width = elementsarray[i].DivWidth + "px";;
            //////////////            divElement.style.height = elementsarray[i].DivHeight + "px";;
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            // divElement.style.backgroundColor = "#FF0000";
            //////////////            divElement.style.opacity = "1";
            //////////////            divElement.draggable = true;
            //////////////            divElement.setAttribute("data-maxLength", elementsarray[i].MaxChar);
            //////////////            divElement.style["resize"] = "horizontal";
            //////////////            divElement.setAttribute("data-placeholder", "Textbox");
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-Tool", ElementType.Textbox.toString());
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.style["overflow"] = "hidden";
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                //var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                // divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var lblfield = document.createElement("label");
            //////////////            lblfield.textContent = elementsarray[i].Text;
            //////////////            lblfield.style.cssFloat = "left";
            //////////////            lblfield.style.display = "inline-block";
            //////////////            lblfield.style.left = "0px";
            //////////////            lblfield.style.top = "0px";
            //////////////            lblfield.style.position = "absolute";
            //////////////            divElement.appendChild(lblfield);
            //////////////            var imgfield = document.createElement("img");
            //////////////            imgfield.style.width = "100%";
            //////////////            imgfield.style.height = "100%";
            //////////////            imgfield.style.opacity = "1";
            //////////////            imgfield.style.left = "0px";
            //////////////            imgfield.style.top = "0px";
            //////////////            imgfield.style.minWidth = "10px";
            //////////////            imgfield.style.minHeight = "10px";
            //////////////            imgfield.className = "imgbackgroundelement";
            //////////////            imgfield.style.backgroundPosition = "50% 50%";
            //////////////            imgfield.style.backgroundRepeat = "no-repeat";
            //////////////            imgfield.src = "../../Images/Form/textbox.png";
            //////////////            divElement.appendChild(imgfield);
            //////////////            divPage.appendChild(divElement);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1).toString();
            //////////////        }
            //////////////        else if (elementsarray[i].ElementType == ElementType.Textarea) {
            //////////////            //var idNumber: string = elementsarray[i].ElementId.replace("txtareaElement", "").toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;//(divPage.childNodes.length + 1).toString();
            //////////////            divElement.style.width = elementsarray[i].DivWidth + "px";
            //////////////            divElement.style.height = elementsarray[i].DivHeight + "px";
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            //divElement.style.backgroundColor = "#FF0000";
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-maxLength", elementsarray[i].MaxChar);
            //////////////            divElement.setAttribute("data-placeholder", "Textarea");
            //////////////            divElement.setAttribute("data-Tool", ElementType.Textarea.toString());
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.style.opacity = "1";
            //////////////            divElement.draggable = true;
            //////////////            divElement.style["resize"] = "both";
            //////////////            divElement.style["overflow"] = "hidden";
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var lblfield = document.createElement("label");
            //////////////            lblfield.textContent = elementsarray[i].Text;
            //////////////            lblfield.style.cssFloat = "left";
            //////////////            lblfield.style.display = "inline-block";
            //////////////            lblfield.style.left = "0px";
            //////////////            lblfield.style.top = "0px";
            //////////////            lblfield.style.position = "absolute";
            //////////////            divElement.appendChild(lblfield);
            //////////////            var imgfield = document.createElement("img");
            //////////////            imgfield.style.width = "100%";
            //////////////            imgfield.style.height = "100%";
            //////////////            imgfield.style.opacity = "1";
            //////////////            imgfield.style.left = "0px";
            //////////////            imgfield.style.top = "0px";
            //////////////            imgfield.style.minWidth = "10px";
            //////////////            imgfield.style.minHeight = "10px";
            //////////////            imgfield.className = "imgbackgroundelement";
            //////////////            imgfield.style.backgroundPosition = "50% 50%";
            //////////////            imgfield.style.backgroundRepeat = "no-repeat";
            //////////////            imgfield.src = "../../Images/Form/textarea.png";
            //////////////            divElement.appendChild(imgfield);
            //////////////            divPage.appendChild(divElement);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1).toString();
            //////////////        }
            //////////////        else if (elementsarray[i].ElementType == ElementType.Image) {
            //////////////            //var idNumber: string = elementsarray[i].ElementId.replace("imgElement", "").toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;// idNumber;
            //////////////            divElement.style.width = elementsarray[i].DivWidth + "px";
            //////////////            divElement.style.height = elementsarray[i].DivHeight + "px";
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            // divElement.style.backgroundColor = "#FF0000";
            //////////////            divElement.style.opacity = "1";
            //////////////            divElement.draggable = true;
            //////////////            divElement.setAttribute("data-Tag", "Image");
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-Tool", ElementType.image.toString());
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.style["resize"] = "both";
            //////////////            divElement.style["overflow"] = "hidden";
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                //   var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //   divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                //   var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var imgfield = document.createElement("img");
            //////////////            imgfield.style.width = "100%";
            //////////////            imgfield.style.height = "100%";
            //////////////            imgfield.style.opacity = "1";
            //////////////            imgfield.style.left = "0px";
            //////////////            imgfield.style.top = "0px";
            //////////////            imgfield.style.minWidth = "10px";
            //////////////            imgfield.style.minHeight = "10px";
            //////////////            imgfield.style.backgroundPosition = "50% 50%";
            //////////////            imgfield.style.backgroundRepeat = "no-repeat";
            //////////////            imgfield.src = elementsarray[i].ImageSource;
            //////////////            divElement.appendChild(imgfield);
            //////////////            divPage.appendChild(divElement);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1).toString();
            //////////////        }
            //////////////        else if (elementsarray[i].ElementType == ElementType.Barcode2D) {
            //////////////            //this.ImgData();
            //////////////            //var idNumber: string = elementsarray[i].ElementId.replace("barcode2dElement", "").toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;// idNumber;
            //////////////            divElement.style.width = elementsarray[i].DivWidth + "px";;
            //////////////            divElement.style.height = elementsarray[i].DivHeight + "px";;
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            // divElement.style.backgroundColor = "#FF0000";
            //////////////            divElement.style.opacity = "1";
            //////////////            divElement.draggable = true;
            //////////////            divElement.setAttribute("data-image", "");
            //////////////            divElement.setAttribute("data-BarcodeText", elementsarray[i].BarcodeValue);
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-Tag", "Barcode2D");
            //////////////            divElement.setAttribute("data-Tool", ElementType.Barcode2D.toString());
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.style["resize"] = "both";
            //////////////            divElement.style["overflow"] = "hidden";
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                // divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var imgfield = document.createElement("img");
            //////////////            imgfield.style.width = "100%";
            //////////////            imgfield.style.height = "100%";
            //////////////            imgfield.style.opacity = "1";
            //////////////            imgfield.style.left = "0px";
            //////////////            imgfield.style.top = "0px";
            //////////////            imgfield.style.minWidth = "10px";
            //////////////            imgfield.style.minHeight = "10px";
            //////////////            imgfield.style.backgroundPosition = "50% 50%";
            //////////////            imgfield.style.backgroundRepeat = "no-repeat";
            //////////////            imgfield.src = "../../Images/Form/barcode.png";
            //////////////            divElement.appendChild(imgfield);
            //////////////            divPage.appendChild(divElement);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1).toString();
            //////////////        }
            //////////////        else if (elementsarray[i].ElementType == ElementType.Radio) {
            //////////////            //var idNumber: string = elementsarray[i].ElementId.replace("radiobtnElement", "").toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;//idNumber;
            //////////////            divElement.style.width = "auto";
            //////////////            divElement.style.height = "auto";
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            //    divElement.style.backgroundColor = "#FF0000";
            //////////////            divElement.style.opacity = "1";
            //////////////            divElement.draggable = true;
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-Tool", ElementType.Radio.toString());
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.style.paddingRight = "12.22px";
            //////////////            divElement.setAttribute("data-Description", elementsarray[i].Description);
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var lblfield = document.createElement("label");
            //////////////            lblfield.textContent = elementsarray[i].Text;
            //////////////            lblfield.style.cssFloat = "right";
            //////////////            lblfield.style.paddingRight = "10px";
            //////////////            divElement.appendChild(lblfield);
            //////////////            var imgfield = document.createElement("img");
            //////////////            imgfield.style.width = "10px";
            //////////////            imgfield.style.height = "10px";
            //////////////            imgfield.style.margin = "5px 5px 5px 5px";
            //////////////            imgfield.style.opacity = "1";
            //////////////            //imgfield.style.left = "0px";
            //////////////            //imgfield.style.top = "0px";
            //////////////            imgfield.style.minWidth = "10px";
            //////////////            imgfield.style.minHeight = "10px";
            //////////////            imgfield.style.backgroundPosition = "50% 50%";
            //////////////            imgfield.style.backgroundRepeat = "no-repeat";
            //////////////            imgfield.src = "../../Images/Form/radiobutton.png";
            //////////////            divElement.appendChild(imgfield);
            //////////////            divPage.appendChild(divElement);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1).toString();
            //////////////        }
            //////////////        else if (elementsarray[i].ElementType == ElementType.Checkbox) {
            //////////////            //var idNumber: string = elementsarray[i].ElementId.replace("radiobtnElement", "").toString();
            //////////////            //if (newcountter < (parseInt(idNumber))) {
            //////////////            //    newcountter = parseInt(idNumber);
            //////////////            //}
            //////////////            var divElement: HTMLDivElement = document.createElement("div");
            //////////////            divElement.id = "DivElement" + newcountter;
            //////////////            divElement.style.width = "auto";
            //////////////            divElement.style.height = "auto";
            //////////////            divElement.style.left = elementsarray[i].DivX + "px";
            //////////////            divElement.style.top = elementsarray[i].DivY + "px";
            //////////////            divElement.style.opacity = "1";
            //////////////            divElement.draggable = true;
            //////////////            divElement.style.paddingRight = "12.22px";
            //////////////            divElement.setAttribute("data-ElementType", "FormControl");
            //////////////            divElement.setAttribute("data-Tool", ElementType.Checkbox.toString());
            //////////////            divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
            //////////////            if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].Value);
            //////////////            }
            //////////////            else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
            //////////////                divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
            //////////////                divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
            //////////////            }
            //////////////            divElement.setAttribute("data-name", elementsarray[i].Name);
            //////////////            divElement.setAttribute("data-Description", elementsarray[i].Description);
            //////////////            divElement.style.minWidth = "5px";
            //////////////            divElement.style.minHeight = "5px";
            //////////////            divElement.ondragstart = this.OnDragStart;
            //////////////            divElement.onmousedown = this.OnDivElementMouseDown;
            //////////////            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //////////////            divElement.onmouseup = (event): any => {
            //////////////                maindiv.onmousemove = null;
            //////////////            }
            //////////////            divElement.onmouseover = (event: any): any => {
            //////////////                //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "darkgoldenrod";
            //////////////            }
            //////////////            divElement.onmouseout = (event: any): any => {
            //////////////                //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            //////////////                //  divElement.style.backgroundColor = "#FF0000";
            //////////////            }
            //////////////            divElement.style.position = "absolute";
            //////////////            var lblfield = document.createElement("label");
            //////////////            lblfield.textContent = elementsarray[i].Text;
            //////////////            lblfield.style.cssFloat = "right";
            //////////////            lblfield.style.paddingRight = "10px";
            //////////////            divElement.appendChild(lblfield);
            //////////////            var imgfield = document.createElement("img");
            //////////////            imgfield.style.width = "10px";
            //////////////            imgfield.style.height = "10px";
            //////////////            imgfield.style.margin = "5px 5px 5px 5px";
            //////////////            imgfield.style.opacity = "1";
            //////////////            //imgfield.style.left = "0px";
            //////////////            //imgfield.style.top = "0px";
            //////////////            imgfield.style.minWidth = "10px";
            //////////////            imgfield.style.minHeight = "10px";
            //////////////            imgfield.style.backgroundPosition = "50% 50%";
            //////////////            imgfield.style.backgroundRepeat = "no-repeat";
            //////////////            imgfield.src = "../../Images/Form/checkbox.png";
            //////////////            divElement.appendChild(imgfield);
            //////////////            divPage.appendChild(divElement);
            //////////////            // alert((divElement.offsetLeft + divElement.offsetWidth) - divPage.clientWidth);
            //////////////            this._dragging = false;
            //////////////            this._elementId = "";
            //////////////            divcounter.value = (newcountter + 1).toString();
            //////////////        }
            //////////////    }
            //////////////}
            FormTestDesigner.prototype.createOcrZone = function () {
                var divPage = document.getElementById("DivPage");
                var divElement = document.createElement("div");
                var divcounter = document.getElementById("divcounter");
                var styleStr = "opacity:1;position:absolute;z-index:1500;margin:0px;display:inline-block";
                var attributeArray = new Array();
                var attributeKeyVal = new KeyValue();
                attributeKeyVal.key = "data-ElementType";
                attributeKeyVal.value = "OCRControl";
                attributeArray.push(attributeKeyVal);
                attributeKeyVal = new KeyValue();
                attributeKeyVal.key = "data-Tool";
                attributeKeyVal.value = Entities.ElementType.Table.toString();
                attributeArray.push(attributeKeyVal);
                //attributeKeyVal = new KeyValue();
                //attributeKeyVal.key = "data-Ordinal";
                //attributeKeyVal.value = divcounter.value;
                //attributeArray.push(attributeKeyVal);
                //attributeKeyVal = new KeyValue();
                //attributeKeyVal.key = "data-name";
                //attributeKeyVal.value = "";
                //attributeArray.push(attributeKeyVal);
                attributeKeyVal = new KeyValue();
                attributeKeyVal.key = "data-ElementIndexType";
                attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                attributeArray.push(attributeKeyVal);
                attributeKeyVal = new KeyValue();
                attributeKeyVal.key = "data-Value";
                attributeKeyVal.value = "";
                attributeArray.push(attributeKeyVal);
                attributeKeyVal = new KeyValue();
                attributeKeyVal.key = "data-name";
                attributeKeyVal.value = "";
                attributeArray.push(attributeKeyVal);
                var childArray = null;
                //var imgfield = document.createElement("img");
                //imgfield.style.width = "100%";
                //imgfield.style.height = "100%";
                //imgfield.style.opacity = "1";
                //imgfield.style.left = "0px";
                //imgfield.style.top = "0px";
                //imgfield.style.minWidth = "10px";
                //imgfield.style.minHeight = "10px";
                //imgfield.style.backgroundPosition = "50% 50%";
                //imgfield.style.backgroundRepeat = "no-repeat";
                //imgfield.src = "../../Images/Form/barcode.png";
                //childArray.push(imgfield);
                var classList = new Array();
                classList.push("ui-resizable");
                AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElementTempRow" + divcounter.value, "#DivPage", classList, styleStr, attributeArray, childArray, "100px", "100px", this.GetLeft(event), this.GetTop(event), "", "", true, true, ResizeAxis.All, Entities.ElementType.Table);
                this._dragging = false;
                this._elementId = "";
                var countter = parseInt(divcounter.value);
                divcounter.value = (countter + 1) + "";
            };
            FormTestDesigner.prototype.AddColumnToOCRTableZone = function (event) {
                var currentTargetId = document.getElementById("CurTarget");
                var colcounter = document.getElementById("colcounter");
                var coloumncounter = 0;
                if (colcounter.value != "0") {
                    coloumncounter = parseInt(colcounter.value);
                }
                var parentTableDiv = document.getElementById(currentTargetId.value);
                //search if its a table
                var search = parentTableDiv.id.search("DivElementTempRow");
                if (search.toString() == "0") {
                    var offset = 0;
                    for (var i = 0; i < parentTableDiv.childNodes.length; i++) {
                        var tbleCol = parentTableDiv.childNodes[i];
                        if (!(tbleCol.classList.contains("ui-resizable-handle"))) {
                            offset += (+(tbleCol.style.width.replace("px", "")));
                        }
                    }
                    if (offset + 10 > parentTableDiv.offsetWidth) {
                        alert("Not enough space to add a column");
                    }
                    else {
                        var styleStr = "display:inline-block;background-color:grey";
                        var attributeArray = new Array();
                        var attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-ElementType";
                        attributeKeyVal.value = "OCRControl";
                        attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-Tool";
                        attributeKeyVal.value = Entities.ElementType.TableColumn.toString();
                        attributeArray.push(attributeKeyVal);
                        //attributeKeyVal = new KeyValue();
                        //attributeKeyVal.key = "data-Ordinal";
                        //attributeKeyVal.value = divcounter.value;
                        //attributeArray.push(attributeKeyVal);
                        attributeKeyVal = new KeyValue();
                        attributeKeyVal.key = "data-name";
                        attributeKeyVal.value = "";
                        attributeArray.push(attributeKeyVal);
                        //attributeKeyVal = new KeyValue();
                        //attributeKeyVal.key = "data-ElementIndexType";
                        //attributeKeyVal.value = AffinityDms.Entities.ElementIndexType.Label.toString();
                        //attributeArray.push(attributeKeyVal);
                        //attributeKeyVal = new KeyValue();
                        //attributeKeyVal.key = "data-Value";
                        //attributeKeyVal.value = "";
                        //attributeArray.push(attributeKeyVal);
                        var childArray = new Array();
                        var divElementColumn = document.createElement("div");
                        divElementColumn.id = "DivElementColumn" + coloumncounter; //(parentTableDiv.childNodes.length + 1).toString();
                        divElementColumn.style.display = "inline-block";
                        divElementColumn.style.fontSize = "0.7em !important";
                        divElementColumn.innerText = "DivElementColumn" + coloumncounter; //(parentTableDiv.childNodes.length + 1).toString();
                        divElementColumn.style.height = "80%";
                        divElementColumn.style.wordBreak = "break-all !important";
                        divElementColumn.style["resize"] = "none !important";
                        divElementColumn.style["overflow"] = "hidden !important";
                        divElementColumn.style.textAlign = "center";
                        divElementColumn.style.border = "none !important";
                        childArray.push(divElementColumn);
                        var classList = new Array();
                        classList.push("ui-resizable");
                        AffinityDms.Entities.FormTestDesigner.prototype.CreateElement("DivElementColumnContainerDiv" + coloumncounter, "#" + currentTargetId.value, classList, styleStr, attributeArray, childArray, "10px", "100%", 0 + "px", "0px", "", "", false, true, ResizeAxis.HorizontalForwardOnly, Entities.ElementType.TableColumn);
                        this._dragging = false;
                        this._elementId = "";
                        colcounter.value = (coloumncounter + 1) + "";
                    }
                }
            };
            FormTestDesigner.prototype.AddX = function (event) {
                var XElement = document.getElementById("addxid");
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                divelement.style.left = XElement.value.toString() + "px";
            };
            FormTestDesigner.prototype.AddY = function (event) {
                var YElement = document.getElementById("addyid");
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                divelement.style.top = YElement.value.toString() + "px";
            };
            FormTestDesigner.prototype.AddDescription = function (event) {
                var DescriptionElement = document.getElementById("adddescriptionid");
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                divelement.setAttribute("data-Description", DescriptionElement.value.toString());
            };
            FormTestDesigner.prototype.AddText = function (event) {
                var TextElement = document.getElementById("addtextid");
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                if (divelement.getAttribute("data-Tool") == Entities.ElementType.Label.toString()) {
                    divelement.childNodes[0].textContent = TextElement.value;
                }
                else if (divelement.getAttribute("data-Tool") == Entities.ElementType.Textbox.toString()) {
                }
                else if (divelement.getAttribute("data-Tool").toLowerCase() == Entities.ElementType.Radio.toString() || divelement.getAttribute("data-Tool") == Entities.ElementType.Checkbox.toString()) {
                    divelement.childNodes[0].textContent = TextElement.value;
                }
                else if (divelement.getAttribute("data-Tool") == Entities.ElementType.Barcode2D.toString()) {
                    divelement.setAttribute("data-BarcodeText", TextElement.value);
                }
            };
            FormTestDesigner.prototype.ChangeIndexValueType = function (event) {
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                var radiofor = event.currentTarget.getAttribute("data-radiofor");
                switch (radiofor) {
                    case "Label":
                        {
                            //addindexradiodivid                    addindexradioid                     RADIO1
                            //addindexlabeldivid                    addindexlabelid                     LABEL ELEMENT
                            //addindexdatatyperadiodivid            addindexdatatyperadioid             RADIO2
                            //addindexvaluedivid                    addindexvalueid                     VALUE ELEMENT
                            divelement.setAttribute("data-ElementIndexType", Entities.ElementIndexType.Label.toString());
                            divelement.setAttribute("data-Value", "");
                            var IndexLabelDiv = document.getElementById("addindexlabeldivid");
                            IndexLabelDiv.style.display = "";
                            var IndexLabel = document.getElementById("addindexlabelid");
                            IndexLabel.value = divelement.getAttribute("data-Value");
                            var IndexValueDiv = document.getElementById("addindexvaluedivid");
                            IndexValueDiv.style.display = "none";
                            var IndexValue = document.getElementById("addindexvalueid");
                            if (IndexValue != null) {
                                var opt = IndexValue.options[-1];
                                opt.selected = true;
                            }
                            //if (divelement.getAttribute("data-IndexValueDataType") != null) {
                            //    divelement.setAttribute("data-IndexValueDataType", "0");
                            //}
                            //var IndexDataTypeValueDiv = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
                            //if (IndexDataTypeValueDiv != null) {
                            //    IndexDataTypeValueDiv.style.display = "none";
                            //    var IndexValueDataType = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
                            //    if (IndexValueDataType != null)
                            //    {
                            //        var opt = <HTMLOptionElement>IndexValueDataType.options[0];
                            //        opt.selected = true;
                            //    }
                            //}
                            //var IndexValueDiv = <HTMLDivElement>document.getElementById("addvaluedivid");
                            //IndexValueDiv.style.display = "block";
                            //var IndexValue = <HTMLInputElement>document.getElementById("addvalueid");
                            //if (divelement.getAttribute("data-Value") != null) {
                            //    IndexValue.value = divelement.getAttribute("data-Value");
                            //}
                            //else
                            //{
                            //    IndexValue.value = "";
                            //}
                            //divelement.setAttribute("data-IndexValueType", ElementIndexType.Label.toString());
                            break;
                        }
                    case "Value":
                        {
                            //addindexradiodivid                    addindexradioid                     RADIO1
                            //addindexlabeldivid                    addindexlabelid                     LABEL ELEMENT
                            //addindexdatatyperadiodivid            addindexdatatyperadioid             RADIO2
                            //addindexvaluedivid                    addindexvalueid                     VALUE ELEMENT
                            divelement.setAttribute("data-ElementIndexType", Entities.ElementIndexType.Value.toString());
                            divelement.setAttribute("data-Value", "-1");
                            var IndexValueDiv = document.getElementById("addindexvaluedivid");
                            IndexValueDiv.style.display = "";
                            var IndexValue = document.getElementById("addindexvalueid");
                            if (IndexValue != null) {
                                var opt = IndexValue.options[-1];
                                opt.selected = true;
                            }
                            var IndexLabelDiv = document.getElementById("addindexlabeldivid");
                            IndexLabelDiv.style.display = "none";
                            var IndexLabel = document.getElementById("addindexlabelid");
                            if (IndexLabel != null) {
                                IndexLabel.value = "";
                            }
                        }
                }
                //var AddElementIndexTypediv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
                //var AddElementIndexType: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementindextypeid");
                //if (AddElementIndexType.checked) {
                //    divelement.setAttribute("data-IndexValueType", "1");
                //    var AddValuediv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //    AddValues.value = divelement.getAttribute("data-Value");
                //    AddValuediv.style.display = "";
                //}
                //else {
                //    divelement.setAttribute("data-IndexValueType", "0");
                //    var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
                //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
                //    AddValues.value = "";
                //    divelement.setAttribute("data-Value", "None");
                //    AddValuesdiv.style.display = "none";
                //}
            };
            //public AddIndexDataTypeValue(event): any {
            //    var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            //    var divelement: HTMLDivElement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            //    var AddValueDataType: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addvalueid");
            //    divelement.setAttribute("data-ValueDataType", AddValueDataType.selectedIndex.toString());
            //}
            FormTestDesigner.prototype.AddIndexDataTypeValue = function (event) {
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                var AddValues = document.getElementById("addindexvalueid");
                divelement.setAttribute("data-Value", AddValues.selectedIndex.toString());
            };
            FormTestDesigner.prototype.AddValue = function (event) {
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                var AddValues = document.getElementById("addindexlabelid");
                divelement.setAttribute("data-Value", AddValues.value.toString());
            };
            FormTestDesigner.prototype.AddMaxChar = function (event) {
                var MaxCharElement = document.getElementById("addmaxcharid");
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                // var childElement: HTMLElement = <HTMLElement>divele.childNodes[0];
                divelement.setAttribute("maxlength", MaxCharElement.value.toString());
            };
            FormTestDesigner.prototype.AddMinChar = function (event) {
                var MinCharElement = document.getElementById("addmincharid");
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                // var childElement: HTMLElement = <HTMLElement>divele.childNodes[0];
                divelement.setAttribute("minlength", MinCharElement.value.toString());
            };
            FormTestDesigner.prototype.AddFontSize = function (event) {
                var FontSizeElement = document.getElementById("addfontsizeid");
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                //var childElement: HTMLElement = <HTMLElement>divele.childNodes[0];
                divelement.style.fontSize = FontSizeElement.value.toString() + "px";
            };
            FormTestDesigner.prototype.AddWidth = function (event) {
                var WidthElement = document.getElementById("addwidthid");
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                divelement.style.width = WidthElement.value.toString() + "px";
            };
            FormTestDesigner.prototype.AddHeight = function (event) {
                var HeightElement = document.getElementById("addheightid");
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                divelement.style.height = HeightElement.value.toString() + "px";
            };
            FormTestDesigner.prototype.AddName = function (event) {
                var NameElement = document.getElementById("addnameid");
                var currentTargetId = document.getElementById("CurTarget");
                var divelement = document.getElementById(currentTargetId.value);
                divelement.setAttribute("data-name", NameElement.value.toString());
            };
            FormTestDesigner.prototype.DeleteColumnElement = function (event) {
                var hiddenDivId = document.getElementById("Hidfind");
                document.getElementById(hiddenDivId.value).remove();
                hiddenDivId.value = "0";
                var AddDeleteCoulndiv = document.getElementById("adddeletecolumnbtndivid");
                AddDeleteCoulndiv.style.display = "none";
            };
            FormTestDesigner.prototype.OnFileSelected = function (event) {
                //this.onImgFilesele(event);
                var file = event.target.files[0];
                var path = file.value;
                var filereader = new FileReader();
                filereader.addEventListener("load", function (e) {
                    //var hiddenDivId = <HTMLInputElement>document.getElementById("Hidfind");
                    var hiddenDivId = document.getElementById("CurTarget");
                    var divelement = document.getElementById(hiddenDivId.value);
                    if (divelement.getAttribute("data-Tool") == Entities.ElementType.Image.toString()) {
                        divelement.style.backgroundImage = ""; // "url(" + e.target.result + ")";
                        var imgelement = divelement.childNodes[0];
                        imgelement.setAttribute("src", e.target.result);
                        divelement.style.backgroundSize = "cover";
                        divelement.style.backgroundRepeat = "no-repeat";
                    }
                });
                filereader.readAsDataURL(file);
            };
            FormTestDesigner.prototype.DeleteElement = function (event) {
                var currentTargetId = document.getElementById("CurTarget");
                document.getElementById(currentTargetId.value).remove();
                ElementProperties.prototype.DisplayProperties(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                var AddDeletediv = document.getElementById("adddeletebtndivid");
                AddDeletediv.style.display = "none";
                currentTargetId.value = "0";
                var hiddenDivId = document.getElementById("Hidfind");
                hiddenDivId.value = "0";
                var AddDeleteCoulndiv = document.getElementById("adddeletecolumnbtndivid");
                AddDeleteCoulndiv.style.display = "none";
            };
            FormTestDesigner.prototype.ZoomInDiv = function (event) {
                var parentDiv = document.getElementById("ElementsContainer");
                parentDiv.style.transformOrigin = "left top";
                var val = parentDiv.style.transform.toString().toLowerCase();
                var res = val.replace("scale(", "");
                res = res.replace(")", "");
                parentDiv.style.transform = "scale(" + ((parseFloat(res)) + 0.01) + ")";
            };
            FormTestDesigner.prototype.ZoomOutDiv = function (event) {
                var parentDiv = document.getElementById("ElementsContainer");
                parentDiv.style.transformOrigin = "left top";
                var val = parentDiv.style.transform.toString().toLowerCase();
                var res = val.replace("scale(", "");
                res = res.replace(")", "");
                parentDiv.style.transform = "scale(" + ((parseFloat(res)) - 0.01) + ")";
            };
            FormTestDesigner.prototype.FitDiv = function (event) {
                var divImage = document.getElementById("TemplateImage");
                var parentDiv = document.getElementById("ElementsContainer");
                var MainContainer = document.getElementById("MainContainer");
                parentDiv.style.zoom = "1";
                var dph = divImage.clientHeight;
                var pdh = parentDiv.clientHeight;
                var dpw = divImage.clientWidth;
                var pdw = parentDiv.clientWidth;
                var val = parentDiv.style.transform.toString().toLowerCase();
                var res = val.replace("scale(", "");
                res = res.replace(")", "");
                var imgheight = (divImage.clientHeight * (parseFloat(res)));
                var imgwidth = (divImage.clientWidth * (parseFloat(res)));
                if ((imgheight > parentDiv.clientHeight) || (imgwidth > parentDiv.clientWidth)) {
                    while ((imgheight > parentDiv.clientHeight) || (imgwidth > parentDiv.clientWidth)) {
                        this.ZoomOutDiv(event);
                        val = parentDiv.style.transform.toString().toLowerCase();
                        res = val.replace("scale(", "");
                        res = res.replace(")", "");
                        imgheight = (divImage.clientHeight * (parseFloat(res)));
                        imgwidth = (divImage.clientWidth * (parseFloat(res)));
                    }
                }
                else {
                    while ((imgheight < parentDiv.clientHeight) && (imgwidth < parentDiv.clientWidth)) {
                        this.ZoomInDiv(event);
                        val = parentDiv.style.transform.toString().toLowerCase();
                        res = val.replace("scale(", "");
                        res = res.replace(")", "");
                        imgheight = (divImage.clientHeight * (parseFloat(res)));
                        imgwidth = (divImage.clientWidth * (parseFloat(res)));
                    }
                }
            };
            FormTestDesigner.prototype.OnDivMouseWheel = function (event) {
                if (event.ctrlKey == true) {
                    event.preventDefault();
                    if (event.wheelDelta > 0) {
                        this.ZoomInDiv(event);
                    }
                    else {
                        this.ZoomOutDiv(event);
                    }
                }
            };
            FormTestDesigner.prototype.PrintFormClick = function (event) {
                var printContent = document.getElementById("DivPage");
                var WinPrint = window.open('', '', 'left=0,top=0,toolbar=0,sta­tus=0,margin=none');
                WinPrint.document.write(printContent.innerHTML);
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
            };
            FormTestDesigner.prototype.OnDivImageMouseDown = function (event) {
                this.ImageMoving = true;
                var H_ImageMove = document.getElementById("ImageMove");
                H_ImageMove.value = "true";
                var divImage = document.getElementById("TemplateImage");
                var curYPos = event.pageY;
                var curXPos = event.pageX;
                var maindiv = document.getElementById("MainContainer");
                maindiv.onmousemove = function (e) {
                    var H_ImageMove = document.getElementById("ImageMove");
                    if (H_ImageMove.value == "true") {
                        maindiv.scrollTop = maindiv.scrollTop + (curYPos - e.pageY);
                        curYPos = e.pageY;
                        maindiv.scrollLeft = maindiv.scrollLeft + (curXPos - e.pageX); //e.pageX;
                        curXPos = e.pageX;
                    }
                    maindiv.onmouseup = function (ev) {
                        var H_ImageMove = document.getElementById("ImageMove");
                        H_ImageMove.value = "false";
                        var maindiv = document.getElementById("MainContainer");
                        maindiv.onmousemove = null;
                    };
                };
            };
            FormTestDesigner.prototype.OnDivImageMouseUp = function (event) {
                var H_ImageMove = document.getElementById("ImageMove");
                H_ImageMove.value = "false";
                var maindiv = document.getElementById("MainContainer");
                maindiv.onmousemove = null;
            };
            FormTestDesigner.prototype.SaveUserSelectionModal = function () {
                var SelectedTemplate = document.getElementById("SelectedTemplate");
                if (SelectedTemplate != null) {
                    //var TenantUsersSelectionListGrid = document.getElementById("TenantUsersSelectionListGrid");
                    //TenantUsersSelectionListGrid.textContent = "";
                    var SelectedUsersList = new Array();
                    var SelectedUsers = document.getElementsByName("CheckUserSelection");
                    if (SelectedUsers != null) {
                        for (var i = 0; i < SelectedUsers.length; i++) {
                            var SelectedUser = SelectedUsers[i];
                            if (SelectedUser.checked) {
                                var uid = SelectedUser.getAttribute("data-id");
                                if (uid != null) {
                                    var id = parseInt(uid);
                                    SelectedUsersList.push(id);
                                }
                            }
                        }
                    }
                    if (SelectedUsersList.length <= 0) {
                        alert("No Users Selected");
                    }
                    else {
                        var datatype = SelectedTemplate.getAttribute("data-type");
                        var url = "";
                        if (datatype == "Form") {
                            url = "../../TenantForms/AddRemoveUser";
                        }
                        else if (datatype == "Template") {
                            url = "../../TenantTemplates/AddRemoveUser";
                        }
                        var templateid = parseInt(SelectedTemplate.value);
                        var SelectedUsersListStringify = JSON.stringify(SelectedUsersList);
                        $.ajax({
                            type: "POST",
                            url: url,
                            data: JSON.stringify({ templateId: templateid, selectedUsers: SelectedUsersList }),
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            success: function (response) {
                                try {
                                    var users = JSON.parse(response);
                                    if (users.length > 0) {
                                        var modal = $('#UserSelectionModal');
                                        modal.modal('hide');
                                    }
                                }
                                catch (e) {
                                    alert(response);
                                }
                            },
                            error: function (responseText) {
                                alert("Oops an Error Occured");
                            }
                        });
                    }
                }
            };
            FormTestDesigner.prototype.SendExternalUserInvite = function (event) {
                try {
                    var EmailErrorDiv = document.getElementById("EmailErrorDiv");
                    EmailErrorDiv.style.display = "none";
                    EmailErrorDiv.textContent = "";
                    var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
                    EmailSuccessDiv.style.display = "none";
                    EmailSuccessDiv.textContent = "";
                    var SelectedTemplate = document.getElementById("SelectedTemplate");
                    if (SelectedTemplate != null) {
                        var InviteUserEmail = document.getElementById("InviteUserEmail");
                        if (InviteUserEmail != null) {
                            if (InviteUserEmail.value != "") {
                                var SelectedTemplateVal = SelectedTemplate.value;
                                var datatype = SelectedTemplate.getAttribute("data-type");
                                var url = "";
                                if (datatype == "Form") {
                                    url = "../../TenantForms/InviteUser";
                                }
                                else if (datatype == "Template") {
                                    url = "../../TenantTemplates/InviteUser";
                                }
                                $.ajax({
                                    type: "POST",
                                    url: url,
                                    data: JSON.stringify({ templateId: SelectedTemplateVal, email: InviteUserEmail.value }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: 'json',
                                    success: function (response) {
                                        try {
                                            var EmailErrorDiv = document.getElementById("EmailErrorDiv");
                                            var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
                                            var InviteUserName = document.getElementById("InviteUserName");
                                            var InviteUserEmail = document.getElementById("InviteUserEmail");
                                            if (response.toString() == "true") {
                                                EmailSuccessDiv.textContent = "Invitation successfully sent.";
                                                EmailSuccessDiv.style.display = "";
                                                EmailErrorDiv.style.display = "none";
                                                EmailErrorDiv.textContent = "";
                                                InviteUserEmail.value = "";
                                            }
                                            else if (response.toString() == "false") {
                                                EmailSuccessDiv.textContent = "";
                                                EmailSuccessDiv.style.display = "none";
                                                EmailErrorDiv.style.display = "";
                                                EmailErrorDiv.textContent = "Unable to send invitation. Please try resending an invitation";
                                            }
                                            else {
                                                throw response;
                                            }
                                        }
                                        catch (e) {
                                            EmailSuccessDiv.textContent = "";
                                            EmailSuccessDiv.style.display = "none";
                                            EmailErrorDiv.style.display = "";
                                            EmailErrorDiv.textContent = e.toString();
                                            InviteUserEmail.value = "";
                                        }
                                    },
                                    error: function (responseText) {
                                        alert("Oops an Error Occured");
                                    }
                                });
                            }
                            else {
                                throw "Email is Required";
                            }
                        }
                        else {
                            throw "Email is Required";
                        }
                    }
                    else {
                        throw "Unable to find the Following Dicourse";
                    }
                }
                catch (e) {
                    var EmailErrorDiv = document.getElementById("EmailErrorDiv");
                    EmailErrorDiv.style.display = "";
                    EmailErrorDiv.textContent = e;
                    var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
                    EmailSuccessDiv.style.display = "none";
                    EmailSuccessDiv.textContent = "";
                }
            };
            return FormTestDesigner;
        }());
        Entities.FormTestDesigner = FormTestDesigner;
    })(Entities = AffinityDms.Entities || (AffinityDms.Entities = {}));
})(AffinityDms || (AffinityDms = {}));
//.appendTo(appendTo)
//.draggable({
//    scroll: true,
//    cursor: "move",
//    start: function (event, ui: any) {
//        ui.position.left = 0;
//        ui.position.top = 0;
//    },
//    drag: function (event, ui) {
//        var scale = AffinityDms.Entities.FormTestDesigner.prototype.GetScaleValue();
//        var newPostions = AffinityDms.Entities.FormTestDesigner.prototype.DragOffsetFix(ui, scale);
//        ui.position.top = newPostions[0];
//        ui.position.left = newPostions[1];
//        //event.srcElement.clientLeft = ui.position.left + ui.offset.left;
//        //event.srcElement.clientTop = ui.position.top + ui.offset.top;
//        var position = ui.position;
//        var offset = ui.offset;
//        //code returns false if your check does not go through
//        //your code to check if the user can drag anymore
//    }
//})
//.resizable({
//    handles: resizeHandles,
//    autoHide: true,
//    //minWidth: -(contentElem.width()) * 10,  // these need to be large and negative
//    //minHeight: -(contentElem.height()) * 10, // so we can shrink our resizable while scaled
//    resize: function (event, ui) {
//        var currentElement_H: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
//        var currentElement: HTMLDivElement = <HTMLDivElement>document.getElementById(currentElement_H.value);
//        if (currentElement.getAttribute("data-tool") == "table") {
//            var offset = 0;
//            for (var i = 0; i < currentElement.childNodes.length; i++) {
//                var tbleCol: HTMLDivElement = <HTMLDivElement>currentElement.childNodes[i];
//                if (!(tbleCol.classList.contains("ui-resizable-handle"))) {
//                    offset += (+(tbleCol.style.width.replace("px", "")));
//                }
//            }
//            if (offset <= ui.size.width) {
//                var scale = AffinityDms.Entities.FormTestDesigner.prototype.GetScaleValue();
//                var newSize = AffinityDms.Entities.FormTestDesigner.prototype.ResizeOffsetFix(ui, scale);
//                ui.size.width = newSize[0];
//                ui.size.height = newSize[1];
//            }
//        }
//        else if (currentElement.getAttribute("data-tool") == "tablecolumn") {
//            var idd = appendTo.replace("#", "");
//            idd = idd.replace(".", "");
//            var offset = 0;
//            var parentElement = <HTMLDivElement>document.getElementById(idd)
//            for (var i = 0; i < parentElement.childNodes.length; i++) {
//                var tbleCol: HTMLDivElement = <HTMLDivElement>parentElement.childNodes[i];
//                if (!(tbleCol.classList.contains("ui-resizable-handle"))) {
//                    offset += (+(tbleCol.style.width.replace("px", "")));
//                }
//            }
//            if (offset <= parentElement.clientWidth) {
//                var scale = AffinityDms.Entities.FormTestDesigner.prototype.GetScaleValue();
//                var newSize = AffinityDms.Entities.FormTestDesigner.prototype.ResizeOffsetFix(ui, scale);
//                ui.size.width = newSize[0];
//                ui.size.height = newSize[1];
//            }
//        }
//        else {
//            //var scale = AffinityDms.Entities.FormTestDesigner.prototype.GetScaleValue();
//            //var newSize = AffinityDms.Entities.FormTestDesigner.prototype.ResizeOffsetFix(ui, scale);
//            //ui.size.width = newSize[0];
//            //ui.size.height = newSize[1];
//        }
//    }
//})
//.click(function (e: any) {
//    AffinityDms.Entities.FormTestDesigner.prototype.OnElementClick(e);
//}) 
//# sourceMappingURL=TemplateTestDesigner.js.map