module AffinityDms.Entities {
//////////////    export class TemplateElementDetailViewModel {
//////////////        public TemplateElementDetail: TemplateElementDetail = new TemplateElementDetail();
//////////////        //public templateElementDetail:TemplateElementDetail;
//////////////        public ElementDivID: string;
//////////////        constructor(){ }
       
//////////////    }
//////////////    export class ElementsViewModel {
//////////////        public elements: Array<AffinityDms.Entities.TemplateElement>;
//////////////        public elementdetails: Array<AffinityDms.Entities.TemplateElementDetail>;
//////////////    }
//////////////    export class Drawable1 {
//////////////        public X: number;
//////////////        public Y: number;
//////////////        public PX: number;
//////////////        public PY: number;
//////////////    }
//////////////    export class Rectangle {
//////////////        private _X1: number;
//////////////        public get X1(): number { return (this._X1); }
//////////////        public set X1(value: number) { this._X1 = value; }

//////////////        private _Y1: number;
//////////////        public get Y1(): number { return (this._Y1); }
//////////////        public set Y1(value: number) { this._Y1 = value; }

//////////////        private _X2: number;
//////////////        public get X2(): number { return (this._X2); }
//////////////        public set X2(value: number) { this._X2 = value; }

//////////////        private _Y2: number;
//////////////        public get Y2(): number { return (this._Y2); }
//////////////        public set Y2(value: number) { this._Y2 = value; }

//////////////        private _Width: number;
//////////////        public get Width(): number { return (this._Width); }
//////////////        public set Width(value: number) { this._Width = value; }

//////////////        private _Height: number;
//////////////        public get Height(): number { return (this._Height); }
//////////////        public set Height(value: number) { this._Height = value; }

//////////////        public ContainsPoint(x: number, y: number): boolean {
//////////////            return ((x >= this.X1) && (x <= this.X2) && (y >= this.Y1) && (y <= this.Y2))
//////////////        }

//////////////        public static FromRelative(x: number, y: number, width: number, height: number): Rectangle {
//////////////            var rectangle: Rectangle = new Rectangle();

//////////////            rectangle._X1 = x;
//////////////            rectangle._Y1 = y;
//////////////            rectangle._X2 = x + width;
//////////////            rectangle._Y2 = y + height;
//////////////            rectangle._Width = width;
//////////////            rectangle._Height = height;

//////////////            return (rectangle);
//////////////        }

//////////////        public static FromAbsolute(x1: number, y1: number, x2: number, y2: number): Rectangle {
//////////////            var rectangle: Rectangle = new Rectangle();

//////////////            rectangle._X1 = x1;
//////////////            rectangle._Y1 = y1;
//////////////            rectangle._X2 = x2;
//////////////            rectangle._Y2 = y2;
//////////////            rectangle._Width = x2 - x1;
//////////////            rectangle._Height = y2 - y1;

//////////////            return (rectangle);
//////////////        }

//////////////        public static FromDivElement(divElement: HTMLDivElement): Rectangle {
//////////////            var rectangle: Rectangle = new Rectangle();

//////////////            rectangle._X1 = divElement.offsetLeft;
//////////////            rectangle._Y1 = divElement.offsetTop;
//////////////            rectangle._X2 = divElement.offsetLeft + divElement.offsetWidth;
//////////////            rectangle._Y2 = divElement.offsetTop + divElement.offsetHeight;
//////////////            rectangle._Width = divElement.offsetWidth;
//////////////            rectangle._Height = divElement.offsetHeight;

//////////////            return (rectangle);
//////////////        }
//////////////    }

//////////////    //export class ExtendingElementDetails extends AffinityDms.Entities.TemplateElementDetail {
//////////////    //    ElementDivId: string;
//////////////    //    TemplateId: number;
//////////////    //}

//////////////    export class ElementProperties {
//////////////        public DisplayProperties(ImgUploader: boolean, AddColumn: boolean, ElementDataType: boolean, Name: boolean, Description: boolean, Text: boolean, X: boolean, Y: boolean, X2: boolean, Y2: boolean, Radius: boolean, Diameter: boolean, Width: boolean, Height: boolean, DivX: boolean, DivY: boolean, DivWidth: boolean, DivHeight: boolean, MinHeight: boolean, MinWidth: boolean, ForegroundColor: boolean, BackGroundColor: boolean, Hint: boolean, MinChar: boolean, MaxChar: boolean, DateTime: boolean, FontName: boolean, FontSize: boolean, FontStyle: boolean, FontColor: boolean, BorderStyle: boolean, BarcodeType: boolean, Value: boolean, ElementIndexType: boolean, SizeMode: boolean, IsSelected: boolean, Discreminator: boolean, FontGraphicsUnit: boolean, ElementMobileOrdinal: boolean, ElementIndexDataType: boolean): any {

//////////////            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
//////////////            var DivElement: HTMLDivElement = <HTMLDivElement>document.getElementById(currentTargetId.value.toString());
//////////////            if (ImgUploader == true) {
//////////////                var fileuploadingdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("fileuploader");
//////////////                fileuploadingdiv.style.display = "";
//////////////            }
//////////////            else {
//////////////                var fileuploadingdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("fileuploader");
//////////////                fileuploadingdiv.style.display = "none";
//////////////            }
//////////////            if (AddColumn == true) {
//////////////                var AddColumndiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addcolumndivid");
//////////////                AddColumndiv.style.display = "";
//////////////            }
//////////////            else {
//////////////                var AddColumndiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addcolumndivid");
//////////////                AddColumndiv.style.display = "none";
//////////////            }
//////////////            if (Name == true) {
//////////////                var AddNamediv: HTMLDivElement = <HTMLDivElement>document.getElementById("addnamedivid");
//////////////                AddNamediv.style.display = "";
//////////////                if ((DivElement.getAttribute("data-name") != null) && (DivElement.getAttribute("data-name") != "")) {
//////////////                    var NameElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addnameid");
//////////////                    NameElement.value = DivElement.getAttribute("data-name");
//////////////                }
//////////////                else {
//////////////                    var NameElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addnameid");
//////////////                    NameElement.value = "";
//////////////                }
//////////////            }
//////////////            else {
//////////////                var NameElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addnameid");
//////////////                var AddNamediv: HTMLDivElement = <HTMLDivElement>document.getElementById("addnamedivid");
//////////////                AddNamediv.style.display = "none";
//////////////                NameElement.value = "";
//////////////            }
//////////////            if (Description == true) {
//////////////                var AddDescriptiondiv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddescriptiondivid");
//////////////                var AddDescription: HTMLInputElement = <HTMLInputElement>document.getElementById("adddescriptionid");
//////////////                AddDescriptiondiv.style.display = "";
//////////////                AddDescription.value = DivElement.getAttribute("data-Description");

//////////////            }
//////////////            else {
//////////////                var AddDescriptiondiv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddescriptiondivid");
//////////////                var AddDescription: HTMLInputElement = <HTMLInputElement>document.getElementById("adddescriptionid");
//////////////                AddDescriptiondiv.style.display = "none";
//////////////                AddDescription.value = "";
//////////////            }
//////////////            if (Text == true) {
//////////////                var AddTextdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addtextdivid");
//////////////                var AddText: HTMLInputElement = <HTMLInputElement>document.getElementById("addtextid");
//////////////                if (DivElement.getAttribute("data-Tool").toLowerCase() == "textbox") {
//////////////                    AddText.value = DivElement.textContent;
//////////////                }
//////////////                else if (DivElement.getAttribute("data-Tool").toLowerCase() == "radio" || DivElement.getAttribute("data-Tool").toLowerCase() == "checkbox") {
//////////////                    AddText.value = DivElement.childNodes[0].textContent;
//////////////                }
//////////////                else if (DivElement.getAttribute("data-Tool").toLowerCase() == "barcode2d") {
//////////////                    AddText.value = DivElement.getAttribute("data-BarcodeText");
//////////////                }
//////////////                else if (DivElement.getAttribute("data-Tool").toLowerCase() == "label") {
//////////////                    AddText.value = DivElement.childNodes[0].textContent;
//////////////                }
//////////////                AddTextdiv.style.display = "";
//////////////            }
//////////////            else {
//////////////                var AddTextdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addtextdivid");
//////////////                AddTextdiv.style.display = "none";
//////////////                var AddText: HTMLInputElement = <HTMLInputElement>document.getElementById("addtextid");
//////////////                AddText.value = "";
//////////////            }
//////////////            if (X == true) {
//////////////                var AddXdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addxdivid");
//////////////                var AddX: HTMLInputElement = <HTMLInputElement>document.getElementById("addxid");
//////////////                AddXdiv.style.display = "";
//////////////                AddX.value = DivElement.style.left.replace("px", "");
//////////////            }
//////////////            else {
//////////////                var AddXdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addxdivid");
//////////////                var AddX: HTMLInputElement = <HTMLInputElement>document.getElementById("addxid");
//////////////                AddX.value = "";
//////////////                AddXdiv.style.display = "none";
//////////////            }
//////////////            if (Y == true) {
//////////////                var AddYdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addydivid");
//////////////                var AddY: HTMLInputElement = <HTMLInputElement>document.getElementById("addyid");
//////////////                AddYdiv.style.display = "";
//////////////                AddY.value = DivElement.style.top.replace("px", "");
//////////////            }
//////////////            else {
//////////////                var AddYdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addydivid");
//////////////                var AddY: HTMLInputElement = <HTMLInputElement>document.getElementById("addyid");
//////////////                AddY.value = "";
//////////////                AddYdiv.style.display = "none";
//////////////            }
//////////////            if (X2 == true) {
//////////////                var AddX2div: HTMLDivElement = <HTMLDivElement>document.getElementById("addx2divid");
//////////////                var AddX2: HTMLInputElement = <HTMLInputElement>document.getElementById("addx2id");
//////////////                AddX2div.style.display = "";
//////////////            }
//////////////            else {
//////////////                var AddX2div: HTMLDivElement = <HTMLDivElement>document.getElementById("addx2divid");
//////////////                var AddX2: HTMLInputElement = <HTMLInputElement>document.getElementById("addx2id");
//////////////                AddX2.value = "";
//////////////                AddX2div.style.display = "none";
//////////////            }
//////////////            if (Y2 == true) {
//////////////                var AddY2div: HTMLDivElement = <HTMLDivElement>document.getElementById("addy2divid");
//////////////                var AddY2: HTMLInputElement = <HTMLInputElement>document.getElementById("addy2id");
//////////////                AddY2div.style.display = "";
//////////////            }
//////////////            else {
//////////////                var AddY2div: HTMLDivElement = <HTMLDivElement>document.getElementById("addy2divid");
//////////////                var AddY2: HTMLInputElement = <HTMLInputElement>document.getElementById("addy2id");
//////////////                AddY2.value = "";
//////////////                AddY2div.style.display = "none";
//////////////            }
//////////////            if (MaxChar == true) {
//////////////                var AddMaxChardiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addmaxchardivid");
//////////////                var AddMaxChar: HTMLInputElement = <HTMLInputElement>document.getElementById("addmaxcharid");

//////////////                //  var childElement: HTMLElement = <HTMLElement>DivElement.childNodes[0];
//////////////                AddMaxChar.value = DivElement.getAttribute("maxlength");
//////////////                AddMaxChardiv.style.display = "";
//////////////            }
//////////////            else {
//////////////                var AddMaxChardiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addmaxchardivid");
//////////////                var AddMaxChar: HTMLInputElement = <HTMLInputElement>document.getElementById("addmaxcharid");
//////////////                AddMaxChar.value = "";
//////////////                AddMaxChardiv.style.display = "none";
//////////////            }
//////////////            if (FontSize == true) {
//////////////                var AddFontSizediv: HTMLDivElement = <HTMLDivElement>document.getElementById("addfontsizedivid");
//////////////                var AddFontSize: HTMLInputElement = <HTMLInputElement>document.getElementById("addfontsizeid");
//////////////                AddFontSize.value = DivElement.style.fontSize.replace("px", "");
//////////////                AddFontSizediv.style.display = "";
//////////////            }
//////////////            else {
//////////////                var AddFontSizediv: HTMLDivElement = <HTMLDivElement>document.getElementById("addfontsizedivid");
//////////////                var AddFontSize: HTMLInputElement = <HTMLInputElement>document.getElementById("addfontsizeid");
//////////////                AddFontSize.value = "";
//////////////                AddFontSizediv.style.display = "none";
//////////////            }
//////////////            //if (FontFamily == true) {
//////////////            //    var AddFontSizediv: HTMLDivElement = <HTMLDivElement>document.getElementById("addy2divid");
//////////////            //    AddFontSizediv.style.display = "";
//////////////            //}
//////////////            //else {
//////////////            //    var AddFontSizediv: HTMLDivElement = <HTMLDivElement>document.getElementById("addy2divid");
//////////////            //    AddFontSizediv.style.display = "none";
//////////////            //}


//////////////            //if (ElementIndexType == true) {
//////////////            //    var AddelemIndexdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
//////////////            //    var AddelemIndex: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementindextypeid");
//////////////            //    AddelemIndexdiv.style.display = "";
//////////////            //    if (DivElement != null) {
//////////////            //        if (DivElement.getAttribute("data-IndexValueType") == "1") {
//////////////            //            AddelemIndex.checked = true;
//////////////            //            var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
//////////////            //            var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
//////////////            //            AddValues.value = DivElement.getAttribute("data-Value");
//////////////            //            AddValuesdiv.style.display = "";
//////////////            //            if (DivElement.getAttribute("data-IndexValueDataType") != null) {
//////////////            //                if (DivElement.getAttribute("data-IndexValueDataType") == "1") {
//////////////            //                    DivElement.setAttribute("data-IndexValueDataType", "0")
//////////////            //                }
//////////////            //            }
//////////////            //            else
//////////////            //            {
//////////////            //                DivElement.setAttribute("data-IndexValueDataType", "0")
//////////////            //            }
//////////////            //        }
//////////////            //        else {
//////////////            //            AddelemIndex.checked = false;
//////////////            //            var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
//////////////            //            var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
//////////////            //            AddValues.value = "";
//////////////            //            AddValuesdiv.style.display = "none";
//////////////            //        }
//////////////            //    }
//////////////            //}
//////////////            //else {
//////////////            //    var AddelemIndexdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
//////////////            //    var AddelemIndex: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementindextypeid");
//////////////            //    AddelemIndex.checked = false;
//////////////            //    AddelemIndexdiv.style.display = "none";

//////////////            //    var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
//////////////            //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
//////////////            //    AddValues.value = "";
//////////////            //    AddValuesdiv.style.display = "none";
//////////////            //}



//////////////                //if (DivElement != null) {
//////////////                //    if (DivElement.getAttribute("data-IndexValueType") == "1") {
//////////////                //        AddelemIndex.checked = true;
//////////////                //        var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
//////////////                //        var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
//////////////                //        AddValues.value = DivElement.getAttribute("data-Value");
//////////////                //        AddValuesdiv.style.display = "";
//////////////                //        if (DivElement.getAttribute("data-IndexValueDataType") != null) {
//////////////                //            if (DivElement.getAttribute("data-IndexValueDataType") == "1") {
//////////////                //                DivElement.setAttribute("data-IndexValueDataType", "0")
//////////////                //            }
//////////////                //        }
//////////////                //        else {
//////////////                //            DivElement.setAttribute("data-IndexValueDataType", "0")
//////////////                //        }
//////////////                //    }
//////////////                //    else {
//////////////                //        AddelemIndex.checked = false;
//////////////                //        var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
//////////////                //        var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
//////////////                //        AddValues.value = "";
//////////////                //        AddValuesdiv.style.display = "none";
//////////////                //    }
//////////////                //}
////////////////else {
//////////////            //    var AddelemIndexdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
//////////////            //    var AddelemIndex: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementindextypeid");
//////////////            //    AddelemIndex.checked = false;
//////////////            //    AddelemIndexdiv.style.display = "none";

//////////////            //    var AddValuesdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
//////////////            //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
//////////////            //    AddValues.value = "";
//////////////            //    AddValuesdiv.style.display = "none";
//////////////            //}




//////////////            if (ElementIndexType) {
//////////////                //addindexradiodivid                    addindexradioid                     RADIO1
//////////////                //addindexlabeldivid                    addindexlabelid                     LABEL ELEMENT
//////////////                //addindexdatatyperadiodivid            addindexdatatyperadioid             RADIO2
//////////////                //addindexvaluedivid                    addindexvalueid                     VALUE ELEMENT
//////////////                var IndexLabelRadioDiv = <HTMLDivElement>document.getElementById("addindexradiodivid");
//////////////                IndexLabelRadioDiv.style.display = "";
//////////////                if (DivElement.getAttribute("data-ElementIndexType") == AffinityDms.Entities.ElementIndexType.Label.toString()) {
//////////////                    var IndexLabelRadio = <HTMLInputElement>document.getElementById("addindexradioid");
//////////////                    if (IndexLabelRadio != null) {
//////////////                        IndexLabelRadio.checked = true;
//////////////                    }
//////////////                    var IndexLabelDiv = <HTMLDivElement>document.getElementById("addindexlabeldivid");
//////////////                    if (IndexLabelDiv != null) {
//////////////                        IndexLabelDiv.style.display = "";
//////////////                        var IndexLabel = <HTMLInputElement>document.getElementById("addindexlabelid");
//////////////                        IndexLabel.value = DivElement.getAttribute("data-Value");
//////////////                    }
//////////////                }
//////////////                else
//////////////                {
//////////////                    var IndexLabelRadio = <HTMLInputElement>document.getElementById("addindexradioid");
//////////////                    if (IndexLabelRadio != null) {
//////////////                        IndexLabelRadio.checked = false;
//////////////                    }
//////////////                    var IndexLabelDiv = <HTMLDivElement>document.getElementById("addindexlabeldivid");
//////////////                    if (IndexLabelDiv != null) {
//////////////                        IndexLabelDiv.style.display = "none";
//////////////                        var IndexLabel = <HTMLInputElement>document.getElementById("addindexlabelid");
//////////////                        IndexLabel.value = "";
//////////////                    }
//////////////                }
//////////////            }
//////////////            else
//////////////            {
//////////////                var IndexLabelRadioDiv = <HTMLDivElement>document.getElementById("addindexradiodivid");
//////////////                IndexLabelRadioDiv.style.display = "none";
//////////////                var IndexLabelRadio = <HTMLInputElement>document.getElementById("addindexradioid");
//////////////                if (IndexLabelRadio != null) {
//////////////                    IndexLabelRadio.checked = false;
//////////////                }
//////////////                var IndexLabelDiv = <HTMLDivElement>document.getElementById("addindexlabeldivid");
//////////////                if (IndexLabelDiv != null) {
//////////////                    IndexLabelDiv.style.display = "none";
//////////////                    var IndexLabel = <HTMLInputElement>document.getElementById("addindexlabelid");
//////////////                    IndexLabel.value = "";
//////////////                }
//////////////            }




            


//////////////            if (ElementIndexDataType) {
//////////////                //addindexradiodivid                    addindexradioid                     RADIO1
//////////////                //addindexlabeldivid                    addindexlabelid                     LABEL ELEMENT
//////////////                //addindexdatatyperadiodivid            addindexdatatyperadioid             RADIO2
//////////////                //addindexvaluedivid                    addindexvalueid                     VALUE ELEMENT
//////////////                var IndexValueRadioDiv = <HTMLDivElement>document.getElementById("addindexdatatyperadiodivid");
//////////////                IndexValueRadioDiv.style.display = "";
//////////////                if (DivElement.getAttribute("data-ElementIndexType") == AffinityDms.Entities.ElementIndexType.Value.toString()) {
//////////////                    var IndexValueRadio = <HTMLInputElement>document.getElementById("addindexdatatyperadioid");
//////////////                    if (IndexValueRadio != null) {
//////////////                        IndexValueRadio.checked = true;
//////////////                    }
//////////////                    var IndexValueDiv = <HTMLDivElement>document.getElementById("addindexvaluedivid");
//////////////                    if (IndexValueDiv != null) {
//////////////                        IndexValueDiv.style.display = "";
//////////////                        var IndexValue = <HTMLSelectElement>document.getElementById("addindexvalueid");
//////////////                        var index = parseInt(DivElement.getAttribute("data-Value"));
//////////////                        if (index == null)
//////////////                        {
//////////////                            index = -1;
//////////////                        }
//////////////                        var IndexValueOpt = <HTMLOptionElement>IndexValue.options[index];
//////////////                        IndexValueOpt.selected = true;
//////////////                    }
//////////////                }
//////////////                else {
//////////////                    var IndexValueRadio = <HTMLInputElement>document.getElementById("addindexdatatyperadioid");
//////////////                    if (IndexValueRadio != null) {
//////////////                        IndexValueRadio.checked = false;
//////////////                    }
//////////////                    var IndexValueDiv = <HTMLDivElement>document.getElementById("addindexvaluedivid");
//////////////                    if (IndexValueDiv != null) {
//////////////                        IndexValueDiv.style.display = "none";
//////////////                        var IndexValue = <HTMLSelectElement>document.getElementById("addindexvalueid");
//////////////                        var index = -1;
//////////////                        var IndexValueOpt = <HTMLOptionElement>IndexValue.options[index];
//////////////                        IndexValueOpt.selected = true;
//////////////                    }
//////////////                }
//////////////            }
//////////////            else {
//////////////                var IndexValueRadioDiv = <HTMLDivElement>document.getElementById("addindexdatatyperadiodivid");
//////////////                IndexValueRadioDiv.style.display = "none";
//////////////                var IndexValueRadio = <HTMLInputElement>document.getElementById("addindexdatatyperadioid");
//////////////                if (IndexValueRadio != null) {
//////////////                    IndexValueRadio.checked = false;
//////////////                }
//////////////                var IndexValueDiv = <HTMLDivElement>document.getElementById("addindexvaluedivid");
//////////////                if (IndexValueDiv != null) {
//////////////                    IndexValueDiv.style.display = "none";
//////////////                    var IndexValue = <HTMLSelectElement>document.getElementById("addindexvalueid");
//////////////                    var index = -1;
//////////////                    var IndexValueOpt = <HTMLOptionElement>IndexValue.options[index];
//////////////                }
//////////////            }




















//////////////            //var addelementindextypeid = <HTMLInputElement>document.getElementById("addelementindextypeid");
//////////////            //if (ElementIndexType == true) {
//////////////            //    var AddValuesDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
//////////////            //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
//////////////            //    var addelementdatatypeid: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypeid");
//////////////            //    var addelementdatatypevalueid: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypevalueid");
//////////////            //    var addelementdatatypevaluedivid: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
//////////////            //    if (DivElement.getAttribute("data-IndexValueType") != null) {
//////////////            //        var addelementindextypedivid = <HTMLDivElement>document.getElementById("addelementindextypedivid");
//////////////            //        addelementindextypedivid.style.display = "";
//////////////            //        var addelementdatatypedivid = <HTMLDivElement>document.getElementById("addelementdatatypedivid");
//////////////            //        addelementdatatypedivid.style.display = "";
//////////////            //        if (DivElement.getAttribute("data-IndexValueType") == AffinityDms.Entities.ElementIndexType.Label.toString()) {
//////////////            //            addelementindextypeid.checked = true;
//////////////            //            AddValuesDiv.style.display = "";
//////////////            //            addelementdatatypevaluedivid.style.display = "none";
//////////////            //        }
//////////////            //        else if (DivElement.getAttribute("data-IndexValueType") == AffinityDms.Entities.ElementIndexType.Value.toString())
//////////////            //        {
//////////////            //            addelementdatatypeid.checked = true;
//////////////            //            AddValuesDiv.style.display = "none"
//////////////            //            addelementdatatypevalueid.style.display = "";
//////////////            //        }
//////////////            //    }
//////////////            //    if (addelementindextypeid.checked) {
//////////////            //      //  var AddelemIndexdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
                   
//////////////            //        //AddValuesDiv.style.display = "";
//////////////            //       // addelementindextypeid.style.display = "";
                   
//////////////            //        if (DivElement != null) {
//////////////            //            DivElement.setAttribute("data-IndexValueType", AffinityDms.Entities.ElementIndexType.Label.toString());
//////////////            //            if (DivElement.getAttribute("data-Value") != null) {
//////////////            //                AddValues.value = DivElement.getAttribute("data-Value");
//////////////            //            }
//////////////            //            else {
//////////////            //                AddValues.value = "";
//////////////            //            }
//////////////            //        }
//////////////            //    }
               
//////////////            //}
//////////////            //if ((ElementIndexType == false) || (!(addelementindextypeid.checked))) {
//////////////            //    if (ElementIndexType == false)
//////////////            //    {
//////////////            //        addelementindextypeid.style.display = "none";
//////////////            //    }
//////////////            //    var AddValuesDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
//////////////            //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
//////////////            //    if (DivElement != null) {
//////////////            //        DivElement.setAttribute("data-IndexValueType", AffinityDms.Entities.ElementIndexType.Value.toString());
//////////////            //        DivElement.setAttribute("data-Value", "");
//////////////            //        AddValues.value = "";
//////////////            //        addelementindextypeid.checked = false;
//////////////            //        AddValuesDiv.style.display = "none";
//////////////            //    }
//////////////            //}


//////////////            //var addelementdatatypeid = <HTMLInputElement>document.getElementById("addelementdatatypeid");
//////////////            //if (ElementIndexDataType == true) {
//////////////            //    var AddValuesDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addvaluedivid");
//////////////            //    var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addvalueid");
//////////////            //    var addelementdatatypevalueid: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypevalueid");
//////////////            //    if (DivElement.getAttribute("data-IndexValueType") != null) {
//////////////            //        var addelementindextypedivid = <HTMLDivElement>document.getElementById("addelementindextypedivid");
//////////////            //        addelementindextypedivid.style.display = "";
//////////////            //        var addelementdatatypedivid = <HTMLDivElement>document.getElementById("addelementdatatypedivid");
//////////////            //        addelementdatatypedivid.style.display = "";
//////////////            //        if (DivElement.getAttribute("data-IndexValueType") == AffinityDms.Entities.ElementIndexType.Label.toString()) {
//////////////            //            AddValuesDiv.style.display = "";
//////////////            //            addelementdatatypeid.checked = true;
//////////////            //            addelementdatatypevalueid.style.display = "none";
//////////////            //        }
//////////////            //        else if (DivElement.getAttribute("data-IndexValueType") == AffinityDms.Entities.ElementIndexType.Value.toString()) {
//////////////            //            AddValuesDiv.style.display = "none"
//////////////            //            addelementindextypeid.checked = false;
//////////////            //            addelementdatatypevalueid.style.display = "";
//////////////            //        }
//////////////            //    }
//////////////            //    if (addelementdatatypeid.checked) {
//////////////            //        var AddValuesDTDiv: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypevaluedivid");
//////////////            //        AddValuesDTDiv.style.display = "";
//////////////            //        addelementdatatypeid.style.display = "";
//////////////            //        var AddValuesDT: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
//////////////            //        if (DivElement != null) {
//////////////            //            DivElement.setAttribute("data-IndexValueType", AffinityDms.Entities.ElementIndexType.Value.toString());
//////////////            //            if (DivElement.getAttribute("data-ValueDataType") != null) {
//////////////            //                var IndexValue = parseInt(DivElement.getAttribute("data-ValueDataType"));
//////////////            //                var opt = <HTMLOptionElement>AddValuesDT.options[IndexValue];
//////////////            //                opt.selected = true;
//////////////            //            }
//////////////            //            else {
//////////////            //                var IndexValue = parseInt(DivElement.getAttribute("data-ValueDataType"));
//////////////            //                var opt = <HTMLOptionElement>AddValuesDT.options[IndexValue];
//////////////            //                opt.selected = true;
//////////////            //                DivElement.setAttribute("data-ValueDataType","-1")
//////////////            //            }
//////////////            //        }
//////////////            //    }

//////////////            //}
//////////////            //if ((ElementIndexDataType == false) || (!(addelementdatatypeid.checked))) {
//////////////            //    // var AddelemIndexdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementindextypedivid");
//////////////            //    if (ElementIndexDataType == false)
//////////////            //    {
//////////////            //        addelementindextypeid.style.display = "none";
//////////////            //    }
//////////////            //    var AddValuesDTDiv: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypevaluedivid");
//////////////            //    var AddValuesDT: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
//////////////            //    if (DivElement != null) {
//////////////            //        DivElement.setAttribute("data-IndexValueType", AffinityDms.Entities.ElementIndexType.Label.toString());
//////////////            //        DivElement.setAttribute("data-ValueDataType", "-1");
//////////////            //        var IndexValue = parseInt(DivElement.getAttribute("data-ValueDataType"));
//////////////            //        var opt = <HTMLOptionElement>AddValuesDT.options[IndexValue];
//////////////            //        opt.selected = true;
//////////////            //        addelementindextypeid.checked = false;
//////////////            //        AddValuesDTDiv.style.display = "none";
//////////////            //    }
//////////////            //}




//////////////            //if (ElementIndexDataType == true) {
//////////////            //    var AddelemIndexDTdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypedivid");
//////////////            //    var AddelemIndexDT: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypeid");
//////////////            //    AddelemIndexDTdiv.style.display = "";
//////////////            //    if (DivElement != null) {
//////////////            //        if (DivElement.getAttribute("data-IndexValueType") == "1") {
//////////////            //            AddelemIndexDT.checked = true;
//////////////            //            var AddValuesDTdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
//////////////            //            var AddValuesDT: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
//////////////            //            var selectedopt = 0;
//////////////            //            if (DivElement.getAttribute("data-ValueDataType") != null) {
//////////////            //                selectedopt = parseInt(DivElement.getAttribute("data-ValueDataType"));

//////////////            //            }
//////////////            //            var opt = <HTMLOptionElement>AddValuesDT.options[selectedopt];
//////////////            //            opt.selected = true;
//////////////            //            AddValuesDTdiv.style.display = "";

//////////////            //            if (DivElement.getAttribute("data-IndexValueType") != null) {
//////////////            //                if (DivElement.getAttribute("data-IndexValueType") == "1") {
//////////////            //                    DivElement.setAttribute("data-IndexValueType", "0")
//////////////            //                }
//////////////            //            }
//////////////            //            else {
//////////////            //                DivElement.setAttribute("data-IndexValueType", "0")
//////////////            //            }
//////////////            //        }
//////////////            //        else {
//////////////            //            //AddelemIndex.setAttribute("checked", "checked");
//////////////            //            AddelemIndexDT.checked = false;
//////////////            //            var AddValuesDTdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
//////////////            //            var AddValuesDT: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
//////////////            //            AddValuesDT.value = "";
//////////////            //            var selectedopt = 0;
//////////////            //            //if (DivElement.getAttribute("data-ValueDataType") != null) {
//////////////            //            //    selectedopt = parseInt(DivElement.getAttribute("data-ValueDataType"));
//////////////            //            //}
//////////////            //            var opt = <HTMLOptionElement>AddValuesDT.options[selectedopt];
//////////////            //            opt.selected = true;
//////////////            //            AddValuesDTdiv.style.display = "none";
//////////////            //        }
//////////////            //    }
//////////////            //}
//////////////            //else {
//////////////            //    var AddelemIndexDTdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypedivid");
//////////////            //    var AddelemIndexDT: HTMLInputElement = <HTMLInputElement>document.getElementById("addelementdatatypeid");
//////////////            //    AddelemIndexDT.checked = false;
//////////////            //    AddelemIndexDTdiv.style.display = "none";

//////////////            //    var AddValuesDTdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
//////////////            //    var AddValuesDT: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
//////////////            //    var selectedopt = 0;
//////////////            //    var opt = <HTMLOptionElement>AddValuesDT.options[selectedopt];
//////////////            //    opt.selected = true;
//////////////            //    AddValuesDTdiv.style.display = "none";
//////////////            //}

//////////////            if (Width == true) {
//////////////                var AddWidthdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addwidthdivid");
//////////////                var AddWidth: HTMLInputElement = <HTMLInputElement>document.getElementById("addwidthid");
//////////////                DivElement.setAttribute("data-lastupdatedwidth", DivElement.style.width.replace("px", ""));

//////////////                AddWidth.value = DivElement.style.width.replace("px", "");
//////////////                AddWidthdiv.style.display = "";
//////////////                //if (DivElement.childNodes[0] instanceof HTMLImageElement) {
//////////////                //    AddWidth.value = DivElement.style.width.replace("px", "");
//////////////                //}
//////////////                //if (DivElement.childNodes[0] instanceof HTMLInputElement) {
//////////////                //    var htmlchildinput: HTMLInputElement = <HTMLInputElement>DivElement.childNodes[0];
//////////////                //    if (htmlchildinput.type == "text") {
//////////////                //        AddWidth.value = DivElement.style.width.replace("px", "");
//////////////                //    }
//////////////                //}
//////////////                //else if (DivElement.childNodes[0] instanceof HTMLTextAreaElement) {
//////////////                //    AddWidth.value = DivElement.style.width.replace("px", "");
//////////////                //}
//////////////                //else {
//////////////                //    if (DivElement.getAttribute("data-elementtype") == "OCRControl") {
//////////////                //        AddWidth.value = DivElement.style.width.replace("px", "");
//////////////                //    }
//////////////                //var htmlchild: HTMLElement = <HTMLElement>DivElement.childNodes[0];
//////////////                //AddWidth.value = htmlchild.style.width.replace("px", "");
//////////////                //}


//////////////            }
//////////////            else {
//////////////                var AddWidthdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addwidthdivid");
//////////////                var AddWidth: HTMLInputElement = <HTMLInputElement>document.getElementById("addwidthid");
//////////////                AddWidth.value = "";
//////////////                AddWidthdiv.style.display = "none";
//////////////            }
//////////////            if (Height == true) {
//////////////                var AddHeightdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addheightdivid");
//////////////                var AddHeight: HTMLInputElement = <HTMLInputElement>document.getElementById("addheightid");
//////////////                DivElement.setAttribute("data-lastupdatedheight", DivElement.style.height.replace("px", ""));
//////////////                AddHeight.value = DivElement.style.height.replace("px", "");
//////////////                AddHeightdiv.style.display = "";
//////////////                //if (DivElement.childNodes[0] instanceof HTMLInputElement) 
//////////////                //{
//////////////                //    var htmlchildinput: HTMLInputElement = <HTMLInputElement>DivElement.childNodes[0];
//////////////                //    if (htmlchildinput.type == "text") {
//////////////                //        AddHeight.value = DivElement.style.height.replace("px", "");
//////////////                //        AddHeightdiv.style.display = "";
//////////////                //    }
//////////////                //}
//////////////                //if (DivElement.childNodes[0] instanceof HTMLImageElement) {
//////////////                //    AddHeight.value = DivElement.style.height.replace("px", "");
//////////////                //    AddHeightdiv.style.display = "";
//////////////                //}
//////////////                //}
//////////////                //else if (DivElement.childNodes[0] instanceof HTMLTextAreaElement) {
//////////////                //    AddHeight.value = DivElement.style.height.replace("px", "");
//////////////                //    AddHeightdiv.style.display = "";
//////////////                //}
//////////////                //else {
//////////////                //    if (DivElement.getAttribute("data-elementtype") == "OCRControl") {
//////////////                //        AddHeight.value = DivElement.style.height.replace("px", "");
//////////////                //        AddHeightdiv.style.display = "";
//////////////                //    }
//////////////                //var htmlchild: HTMLElement = <HTMLElement>DivElement.childNodes[0];
//////////////                //AddHeight.value = htmlchild.style.height.replace("px", "");
//////////////                //}
//////////////            }
//////////////            else {
//////////////                var AddHeightdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addheightdivid");
//////////////                var AddWidth: HTMLInputElement = <HTMLInputElement>document.getElementById("addheightid");
//////////////                AddWidth.value = "";
//////////////                AddHeightdiv.style.display = "none";
//////////////            }
//////////////            var hiddenDivId: HTMLInputElement = <HTMLInputElement>document.getElementById("Hidfind");
//////////////            hiddenDivId.value = "0";
//////////////            var AddDeleteCoulndiv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletecolumnbtndivid");
//////////////            AddDeleteCoulndiv.style.display = "none";
//////////////            var AddDeletediv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletebtndivid");
//////////////            AddDeletediv.style.display = "";
//////////////        }
//////////////    }

    enum FontStyles { inherit, initial, italic, normal, oblique };
    enum BorderStyles { dashed, dotted, double, grouve, hidden, inherit, initial, inset, none, outset, ridge, solid };
    export class FormDesigner {

        //public TemplateObject: Template;
        //public selectedElement: HTMLImageElement;
        constructor() {
            //this.selectedElement = null;

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

        public Run(): void {
            // var page = this.TemplateObject.TemplateVersions[0].TemplatePages[0];
            var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            //var uploaddiv: HTMLDivElement = <HTMLDivElement>document.getElementById("fileuploader");
            //uploaddiv.style.display = "none";

            // divPage.onmousemove = this.OnDivElementMouseMove;
        }

        public PX: number;
        public PY: number;
        public _dragging: boolean = false;
        public _elementId: string = "";
        public OnDragStart(event): any {
            this._dragging = true;
            if (this.divelement != null) {
                return false;
            }
            this._elementId = event.currentTarget.id;
            this.PX = event.x;
            this.PY = event.y;
            event.dataTransfer.setData("OnDragStart", event.currentTarget.id);
        }
        public OnResizeEnded(): any {
            var curtarget: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(curtarget.value);
            var AddWidth: HTMLInputElement = <HTMLInputElement>document.getElementById("addwidthid");
            var AddHeight: HTMLInputElement = <HTMLInputElement>document.getElementById("addheightid");
            if (divElement.style.width.replace("px", "") != divElement.getAttribute("data-lastupdatedwidth") || divElement.style.height.replace("px", "") != divElement.getAttribute("data-lastupdatedheight")) {
                AddWidth.value = divElement.style.width.replace("px", "");
                AddHeight.value = divElement.style.height.replace("px", "");
                divElement.setAttribute("data-lastupdatedwidth", AddWidth.value);
                divElement.setAttribute("data-lastupdatedheight", AddHeight.value);
            }
        }
        public GetLeft(event): any {
            var leftpanel = <HTMLDivElement>document.getElementById("LeftPane");
            var divpage = <HTMLDivElement>document.getElementById("DivPage");
            var templatetype = <HTMLInputElement>document.getElementById("TempType")
            if (templatetype.value == "Form") {
                var left: string = "";
                if (leftpanel != null) {
                    left = (((event.pageX) - ((divpage.offsetLeft + leftpanel.clientWidth + 20))).toString() + "px");
                }
                else {
                    left = (((event.pageX) - (divpage.offsetLeft + 20)).toString() + "px");
                }
            }
            else {
                var divpage = <HTMLDivElement>document.getElementById("DivPage");
                var toolboxdiv = <HTMLDivElement>document.getElementById("DivToolBox");
                var offsetparent = <HTMLDivElement>divpage.offsetParent;
                var templateimageoffset = offsetparent.offsetLeft;
                var left: string = event.offsetX + "px";
            }

            return left;
        }
        public GetTop(event): any {
            var leftpanel = <HTMLDivElement>document.getElementById("LeftPane");
            var top: string = "";
            var templatetype = <HTMLInputElement>document.getElementById("TempType")
            if (templatetype.value == "Form") {
                var divpage = <HTMLDivElement>document.getElementById("DivPage");
                top = ((((event.pageY) - divpage.parentElement.offsetTop)).toString() + "px");
            }
            else {
                var divpage = <HTMLDivElement>document.getElementById("DivPage");
                var offsetparent = <HTMLDivElement>divpage.offsetParent;
                var templateimageoffset = offsetparent.offsetTop;
                top = event.offsetY + "px";
            }
            return top;
        }
        public OnDrop(event: any) {
            if (this.divelement != null) {
                if (this.divelement != null) {
                    this.divelement.onmouseup = null;
                    this.divelement = null;
                    this._dragging = false;

                }
            }
            event.preventDefault();
            var elementType = event.dataTransfer.getData("OnDragStart");
            var divcounter: HTMLInputElement = <HTMLInputElement>document.getElementById("divcounter");

            if (this._dragging != false && this._elementId != "") {
                if (this._elementId == "DivToolLabel") {
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;
                    divElement.style.width = "auto";
                    divElement.style.height = "auto";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    //divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "label");
                    divElement.setAttribute("data-Ordinal", divcounter.value);
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    divElement.draggable = true;
                    //divElement.style.backgroundImage = "url('../Images/Form/labelonly.png')";
                    //divElement.style.backgroundSize = "cover";
                    //divElement.style.backgroundRepeat = "no-repeat";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                        this.OnResizeEnded();
                    }
                    divElement.onmouseover = (event): any => {
                        //divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        //divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    var lblfield = document.createElement("label");
                    lblfield.textContent = "Label";
                    lblfield.style.paddingRight = "10px";
                    divElement.appendChild(lblfield);
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;
                    //hiddenfield.value = divcounter.value;
                    //divElement.appendChild(hiddenfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";


                }
                else if (this._elementId == "DivToolTextBox") {

                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;//  (divPage.childNodes.length + 1).toString();
                    divElement.style.width = "200px";
                    divElement.style.height = "35px";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    //divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.setAttribute("data-ElementType", "FormControl");
                    //divElement.style.backgroundImage = "url('../Images/Form/textbox.png')";
                    //divElement.style.backgroundSize = "cover";
                    //divElement.style.backgroundRepeat = "no-repeat";
                    divElement.setAttribute("data-Tool", "textbox");
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    divElement.setAttribute("data-placeholder", "Textbox");
                    divElement.setAttribute("data-Ordinal", divcounter.value);
                    divElement.setAttribute("data-maxLength", "55000");
                   
                    divElement.style["resize"] = "horizontal";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event): any => {
                        // divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        //divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;//  (divPage.childNodes.length + 1).toString();
                    //hiddenfield.value = divcounter.value;// (divPage.childNodes.length + 1).toString();
                    //divElement.appendChild(hiddenfield);

                    var lblfield = document.createElement("label");
                    lblfield.textContent = "";
                    lblfield.style.cssFloat = "left";
                    lblfield.style.display = "inline-block";
                    lblfield.style.left = "0px";
                    lblfield.style.top = "0px";
                    lblfield.style.position = "absolute";
                    divElement.appendChild(lblfield);
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";

                }
                else if (this._elementId == "DivToolTextArea") {
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;// (divPage.childNodes.length + 1).toString();
                    divElement.style.width = "100px";
                    divElement.style.height = "100px";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    //divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.setAttribute("data-ElementType", "FormControl");
                    //divElement.style.backgroundImage = "url('../Images/Form/textarea.png')";
                    //divElement.style.backgroundSize = "cover";
                    //divElement.style.backgroundRepeat = "no-repeat";
                    divElement.setAttribute("data-Tool", "textarea");
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    divElement.setAttribute("data-placeholder", "Textarea");
                    divElement.setAttribute("data-Ordinal", divcounter.value);
                    divElement.setAttribute("data-maxLength", "55000");
                    
                    divElement.style["resize"] = "both";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";

                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                        this.OnResizeEnded();
                    }
                    divElement.onmouseover = (event): any => {
                        // divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        //divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;
                    //hiddenfield.value = divcounter.value;
                    //divElement.appendChild(hiddenfield);


                    var lblfield = document.createElement("label");
                    lblfield.textContent = "";
                    lblfield.style.cssFloat = "left";
                    lblfield.style.display = "inline-block";
                    lblfield.style.left = "0px";
                    lblfield.style.top = "0px";
                    lblfield.style.position = "absolute";
                    divElement.appendChild(lblfield);
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";
                }
                else if (this._elementId == "DivToolImage") {
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;

                    divElement.style.width = "100px";
                    divElement.style.height = "100px";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    //divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.style["resize"] = "both";
                    divElement.setAttribute("data-ElementType", "FormControl");
                    //divElement.style.backgroundImage = "url('../Images/Form/ImagePlaceHolder.png')";
                    //divElement.style.backgroundSize = "cover";
                    //divElement.style.backgroundRepeat = "no-repeat";
                    divElement.setAttribute("data-Tool", "image");
                    divElement.setAttribute("data-image", "");
                    divElement.setAttribute("data-Ordinal", divcounter.value);
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    divElement.setAttribute("data-Tag", "Image");
                   
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event): any => {
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        // divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;//  (divPage.childNodes.length + 1).toString();
                    //hiddenfield.value = divcounter.value;// (divPage.childNodes.length + 1).toString();
                    //divElement.appendChild(hiddenfield);
                    //var lblfield = document.createElement("label");
                    //lblfield.textContent = "Label";
                    //lblfield.style.cssFloat = "left";
                    //lblfield.style.display = "inline-block";
                    //lblfield.style.left = "0px";
                    //lblfield.style.top = "0px";
                    //lblfield.style.position = "absolute";
                    //divElement.appendChild(lblfield);
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
                    divElement.appendChild(imgfield);

                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";
                }

                else if (this._elementId == "DivToolRadioButton") {

                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;// (divPage.childNodes.length + 1).toString();

                    divElement.style.width = "auto";
                    divElement.style.height = "auto";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    // divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.setAttribute("data-ElementType", "FormControl");
                    //divElement.style.backgroundImage = "url('../../Images/Form/radiobutton.png')";
                    //divElement.style.backgroundSize = "20px 20px";
                    //divElement.style.backgroundRepeat = "no-repeat";
                    divElement.setAttribute("data-Tool", "radio");
                    divElement.setAttribute("data-Ordinal", divcounter.value);
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    divElement.setAttribute("data-Description", "DemoDescription");
                    divElement.draggable = true;
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event): any => {
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        //   divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;// (divPage.childNodes.length + 1).toString();
                    //hiddenfield.value = divcounter.value;// (divPage.childNodes.length + 1).toString();
                    //divElement.appendChild(hiddenfield);
                    var lblfield = document.createElement("label");
                    lblfield.textContent = "Label";
                    lblfield.style.cssFloat = "right";
                    lblfield.style.paddingRight = "10px";
                    divElement.appendChild(lblfield);
                    var imgfield = document.createElement("img");
                    imgfield.style.width = "10px";
                    imgfield.style.height = "10px";
                    imgfield.style.margin = "5px 5px 5px 5px";
                    imgfield.style.opacity = "1";
                    //imgfield.style.left = "0px";
                    //imgfield.style.top = "0px";
                    imgfield.style.minWidth = "10px";
                    imgfield.style.minHeight = "10px";
                    imgfield.style.backgroundPosition = "50% 50%";
                    imgfield.style.backgroundRepeat = "no-repeat";
                    imgfield.src = "../../Images/Form/radiobutton.png";
                    divElement.appendChild(imgfield);

                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";
                }
                else if (this._elementId == "DivToolCheckBox") {

                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;//  (divPage.childNodes.length + 1).toString();
                    divElement.style.width = "auto";
                    divElement.style.height = "auto";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "checkbox");
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    divElement.setAttribute("data-Ordinal", divcounter.value);
            
                    divElement.setAttribute("data-Description", "DemoDescription");
                    //  divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.style.paddingRight = "12.22px";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event): any => {
                        // divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        //divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;//  (divPage.childNodes.length + 1).toString();
                    //hiddenfield.value = divcounter.value;// (divPage.childNodes.length + 1).toString();
                    //divElement.appendChild(hiddenfield);
                    var lblfield = document.createElement("label");
                    lblfield.textContent = "Label";
                    lblfield.style.cssFloat = "right";
                    lblfield.style.paddingRight = "10px";

                    divElement.appendChild(lblfield);
                    var imgfield = document.createElement("img");
                    imgfield.style.width = "10px";
                    imgfield.style.height = "10px";
                    imgfield.style.margin = "5px 5px 5px 5px";
                    imgfield.style.opacity = "1";
                    //imgfield.style.left = "0px";
                    //imgfield.style.top = "0px";
                    imgfield.style.minWidth = "10px";
                    imgfield.style.minHeight = "10px";
                    imgfield.style.backgroundPosition = "50% 50%";
                    imgfield.style.backgroundRepeat = "no-repeat";
                    imgfield.src = "../../Images/Form/checkbox.png";
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";
                }



                else if (this._elementId == "DivToolCircle") {
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;//  (divPage.childNodes.length + 1).toString();
                    divElement.setAttribute("data-ElementType", "FormControl");
                    //divElement.style.backgroundImage = "url('../../Images/Form/circle.png')";
                    //divElement.style.backgroundSize = "cover";
                    //divElement.style.backgroundRepeat = "no-repeat";
                    divElement.setAttribute("data-Tool", "circle");
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    divElement.setAttribute("data-Ordinal", divcounter.value);
                    divElement.style.width = "35px";
                    divElement.style.height = "35px";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    // divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "0.7";
                    divElement.draggable = true;
                    divElement.style["resize"] = "both";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.style.zIndex = "1500";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                        this.OnResizeEnded();
                    }
                    divElement.onmouseover = (event): any => {
                        // divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        //  divElement.style.backgroundColor = "#FF0000";
                    }

                    divElement.style.position = "absolute";
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;
                    //hiddenfield.value = divcounter.value;
                    //divElement.appendChild(hiddenfield);
                    //var lblfield = document.createElement("label");
                    //lblfield.textContent = "Label";
                    //lblfield.style.cssFloat = "left";
                    //lblfield.style.display = "inline-block";
                    //lblfield.style.left = "0px";
                    //lblfield.style.top = "0px";
                    //lblfield.style.position = "absolute";
                    //divElement.appendChild(lblfield);
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";
                }
                else if (this._elementId == "DivToolLineHorizontal") {
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;// (divPage.childNodes.length + 1).toString();
                    divElement.style.zIndex = "1500";
                    divElement.style.width = "35px";
                    divElement.style.height = "10px";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    // divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.setAttribute("data-Ordinal", divcounter.value);
                    divElement.setAttribute("data-ElementType", "FormControl");
                    //divElement.style.backgroundImage = "url('../../Images/Form/HorizontalLine.png')";
                    //divElement.style.backgroundSize = "cover";
                    //divElement.style.backgroundRepeat = "no-repeat";
                    divElement.setAttribute("data-Tool", "linehorizontal");
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
 
                    //divElement.style.paddingRight = "10px";
                    divElement.style["resize"] = "horizontal";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                        this.OnResizeEnded();
                    }
                    divElement.onmouseover = (event): any => {
                        // divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        // divElement.style.backgroundColor = "#FF0000";
                    }

                    divElement.style.position = "absolute";
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;
                    //hiddenfield.value = divcounter.value;
                    //divElement.appendChild(hiddenfield);
                    //var lblfield = document.createElement("label");
                    //lblfield.textContent = "Label";
                    //lblfield.style.cssFloat = "left";
                    //lblfield.style.display = "inline-block";
                    //lblfield.style.left = "0px";
                    //lblfield.style.top = "0px";
                    //lblfield.style.position = "absolute";
                    //divElement.appendChild(lblfield);
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";
                }
                else if (this._elementId == "DivToolLineVertical") {
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;// (divPage.childNodes.length + 1).toString();
                    divElement.style.zIndex = "1500";
                    divElement.style.width = "10px";
                    divElement.style.height = "35px";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    // divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Ordinal", divcounter.value);
                    //divElement.style.backgroundImage = "url('../../Images/Form/VerticalLine.png')";
                    //divElement.style.backgroundSize = "cover";
                    //divElement.style.backgroundRepeat = "no-repeat";
                    divElement.setAttribute("data-Tool", "linevertical");
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    //divElement.style.paddingRight = "10px";
                    divElement.style["resize"] = "Vertical";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                        this.OnResizeEnded();
                    }
                    divElement.onmouseover = (event): any => {
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        // divElement.style.backgroundColor = "#FF0000";
                    }

                    divElement.style.position = "absolute";
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;
                    //hiddenfield.value = divcounter.value;
                    //divElement.appendChild(hiddenfield);
                    //var lblfield = document.createElement("label");
                    //lblfield.textContent = "Label";
                    //lblfield.style.cssFloat = "left";
                    //lblfield.style.display = "inline-block";
                    //lblfield.style.left = "0px";
                    //lblfield.style.top = "0px";
                    //lblfield.style.position = "absolute";
                    //divElement.appendChild(lblfield);
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";
                }
                else if (this._elementId == "DivToolRectangle") {
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;// (divPage.childNodes.length + 1).toString();
                    divElement.setAttribute("data-ElementType", "FormControl");
                    //divElement.style.backgroundImage = "url('../../Images/Form/Rectangle.png')";
                    //divElement.style.backgroundSize = "cover";
                    //divElement.style.backgroundRepeat = "no-repeat";
                    divElement.setAttribute("data-Tool", "rectangle");
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    divElement.setAttribute("data-Ordinal", divcounter.value);
                    divElement.style.width = "35px";
                    divElement.style.height = "35px";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    // divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "0.7";
                    divElement.draggable = true;
                    divElement.style.zIndex = "1500";
                    divElement.style["resize"] = "both";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";

                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                        this.OnResizeEnded();
                    }
                    divElement.onmouseover = (event): any => {
                        //divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        // divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;
                    //hiddenfield.value = divcounter.value;
                    //divElement.appendChild(hiddenfield);
                    //var lblfield = document.createElement("label");
                    //lblfield.textContent = "Label";
                    //lblfield.style.cssFloat = "left";
                    //lblfield.style.display = "inline-block";
                    //lblfield.style.left = "0px";
                    //lblfield.style.top = "0px";
                    //lblfield.style.position = "absolute";
                    //divElement.appendChild(lblfield);
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";
                }
                else if (this._elementId == "DivToolBarcode2D") {
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;//  (divPage.childNodes.length + 1).toString();
                    divElement.style.width = "100px";
                    divElement.style.height = "100px";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    //divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.style["resize"] = "both";
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Ordinal", divcounter.value);
                    //divElement.style.backgroundImage = "url('../../Images/Form/barcode.png')";
                    //divElement.style.backgroundSize = "cover";
                    //divElement.style.backgroundRepeat = "no-repeat";
                    divElement.setAttribute("data-Tool", "barcode2d");
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    divElement.setAttribute("data-image", "");
                    divElement.setAttribute("data-BarcodeText", "");
                    divElement.setAttribute("data-Tag", "Barcode2D");
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event): any => {
                        // divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        //  divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    //var hiddenfield = document.createElement("input");
                    //hiddenfield.type = "hidden";
                    //hiddenfield.id = "HF_Order" + divcounter.value;//  (divPage.childNodes.length + 1).toString();
                    //hiddenfield.value = divcounter.value;// (divPage.childNodes.length + 1).toString();
                    //divElement.appendChild(hiddenfield);
                    //var lblfield = document.createElement("label");
                    //lblfield.textContent = "Label";
                    //lblfield.style.cssFloat = "left";
                    //lblfield.style.display = "inline-block";
                    //lblfield.style.left = "0px";
                    //lblfield.style.top = "0px";
                    //lblfield.style.position = "absolute";
                    //divElement.appendChild(lblfield);
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";
                }
                else if (this._elementId == "DivToolOcrRectangleZone")
                {
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + divcounter.value;
                    divElement.setAttribute("data-ElementType", "OCRControl");
                    divElement.setAttribute("data-Tool", "rectangle");
                    divElement.setAttribute("data-name", "");
                    divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                    divElement.setAttribute("data-Value", "");
                    divElement.style.width = "35px";
                    divElement.style.height = "35px";
                    divElement.style.left = this.GetLeft(event);
                    divElement.style.top = this.GetTop(event);
                    divElement.style.opacity = "0.7";
                    divElement.draggable = true;
                    divElement.style.border = "solid 5px grey";
                    divElement.style.paddingBottom = "5px";
                    divElement.style.zIndex = "1500";
                    divElement.style.paddingRight = "5px";
                    divElement.style["resize"] = "both";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var elementContainer = <HTMLDivElement>document.getElementById("TemplateImage");
                    divElement.onmouseup = (event): any => {
                        elementContainer.onmousemove = null;
                        this.OnResizeEnded();
                    }
                    divElement.onmouseover = (event): any => {
                        //divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event): any => {
                        // divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    var countter: number = parseInt(divcounter.value);
                    divcounter.value = (countter + 1) + "";
                }
                else if (this._elementId == "DivToolOcrTableZone") {
                    this.createOcrZone();

                }

                //}

                else if ((elementType.length >= "DivElement".length) && (elementType.substring(0, "DivElement".length - 1))) {
                    //if (event.target.id == "DivPage") {
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(elementType);
                    divElement.style.left = ((event.pageX - divPage.offsetLeft) - Math.abs(this.PX)).toString() + "px";
                    divElement.style.top = ((event.pageY - divPage.offsetTop) - Math.abs(this.PY)).toString() + "px";
                    //}
                    ////  alert("event X " + event.x + " event Y " + event.y + " div offsetleft " + divPage.offsetLeft + " div offsettop " + divPage.offsetTop + " mathpx " + Math.abs(this.PX) + " mathpy " + Math.abs(this.PY) + " finalleft" + divElement.style.left + "finalTop" + divElement.style.top + " layerx " + event.clientX + " layery " + event.clientY);

                }
            }
        }
        public OnDragOver(event: DragEvent): void {
            event.preventDefault();
        }
        public select: string = "";
        public divelement: HTMLDivElement = null;
        public OnDivElementMouseDown(event): any {
            //alert(event.currentTarget.id);
            //alert(event.target.id);
            //event.preventDefault();
            var maindiv;
            var mainImagediv: HTMLInputElement = <HTMLInputElement>document.getElementById("TempType");
            if (mainImagediv.value == "Template") {
                maindiv = <HTMLDivElement>document.getElementById("TemplateImage");
            }
            else {
                maindiv = <HTMLDivElement>document.getElementById("DivPage");
            }

            this.divelement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
            var fileuploadingdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("fileuploader");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            if (this.divelement.getAttribute("data-ElementType") == "FormControl") {

                if (this.divelement.getAttribute("data-Tool").toLowerCase() == "label") {

                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true, false, false, false, false, false, true);
                }
                else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "textbox") {

                    //  var inputele: HTMLInputElement = <HTMLInputElement>this.divelement.childNodes[0];
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, true, false, true, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                }
                else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "radio") {

                    // var inputele: HTMLInputElement = <HTMLInputElement>this.divelement.childNodes[0];
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, true, false, false, false, true);
                }
                else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "checkbox") {
                    // var inputele: HTMLInputElement = <HTMLInputElement>this.divelement.childNodes[0];
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, true, false, false, false, true);
                }
                else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "textarea") {
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, true, false, true, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                }
                else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "image") {
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(true, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                }
                else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "barcode2d") {
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, true, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                }
                //else if (this.divelement.childNodes[0].nodeName.toLowerCase() == "svg") {
                //    var svg = this.divelement.childNodes[0];
                else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "linehorizontal") {
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                }
                else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "linevertical") {
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                }
                else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "rectangle") {
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                }
                else if (this.divelement.getAttribute("data-Tool").toLowerCase() == "circle") {
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                }
                //}


            }
            else if (this.divelement.getAttribute("data-ElementType") == "OCRControl") {
                var searchTable = this.divelement.id.search("DivElementTempRow");
                if (searchTable == 0) {
                    if (this.divelement instanceof HTMLDivElement) {
                        currentTargetId.value = event.currentTarget.id;
                        ElementProperties.prototype.DisplayProperties(false, true, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                    }
                }
                if (this.divelement.getAttribute("data-Tool").toLowerCase() == "rectangle") {
                    currentTargetId.value = event.currentTarget.id;
                    ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, true, true, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                }
            }
            //}
            else {
                currentTargetId.value = "0";
                ElementProperties.prototype.DisplayProperties(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, true);
                var AddDeletediv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletebtndivid");
                AddDeletediv.style.display = "none";
                var hiddenDivId = <HTMLInputElement>document.getElementById("Hidfind");
                hiddenDivId.value = "0";
                var AddDeleteColumndiv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletecolumnbtndivid");
                AddDeleteColumndiv.style.display = "none";


            }



            ////else {
            ////    //fileuploadingdiv.style.display = "none";
            ////    //// this.select = event.currentTarget.id;
            ////    ////this.selectedElement = <HTMLImageElement>this.divelement.childNodes[0];
            ////    //var devilIdea = <HTMLInputElement>document.getElementById("Hidfind");
            ////    //devilIdea.value = "0";
            ////}

            //if (this.divelement.getAttribute("data-ElementType") == "FormControl") {
            //    //if (this.divelement.childNodes[0] instanceof HTMLImageElement) {
            //    //    currentTargetId.value = event.currentTarget.id;
            //    //}
            //    //else if (this.divelement.childNodes[0] instanceof HTMLLabelElement) {
            //    //    currentTargetId.value = event.currentTarget.id;
            //    //}
            //    //else if (this.divelement.childNodes[0] instanceof HTMLTextAreaElement) {
            //    //    currentTargetId.value = event.currentTarget.id;
            //    //}
            //    //else if (this.divelement.childNodes[0] instanceof HTMLInputElement) {
            //    //    var elem = <HTMLInputElement>this.divelement.childNodes[0];
            //    //    if (elem.type == "text" || elem.type == "radio" || elem.type == "checkbox") {
            //    //        currentTargetId.value = event.currentTarget.id;
            //    //    }

            //    //}
            //    currentTargetId.value = event.currentTarget.id;
            //}






            maindiv.onmousemove = (events: any) => {

                var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
                var divelements = <HTMLDivElement>document.getElementById(currentTargetId.value);
                var H_TemplateType = <HTMLInputElement>document.getElementById("TempType");
                if (H_TemplateType != null && ((H_TemplateType.value == "Form") || (H_TemplateType.value == "Template"))) {
                    if (divelements != null && this._dragging == true) {
                        if (H_TemplateType.value == "Form") {


                            var AddXdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addxdivid");
                            var AddYdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addydivid");

                            var leftpane: HTMLDivElement = <HTMLDivElement>document.getElementById("LeftPane");
                            //if (leftpane != null) {
                            //    var menu: HTMLUListElement = <HTMLUListElement>leftpane.childNodes[1];

                            //    if (menu.style.display == "" || menu.style.display == "block") {
                            //        divelements.style.left = (((events.pageX - maindiv.offsetLeft) - leftpane.clientWidth) - (divelements.clientWidth / 2)) + 'px';
                            //        divelements.style.top = ((events.pageY - (maindiv.parentElement.offsetTop)) - (divelements.clientHeight / 2)) + 'px';

                            //    }
                            //    else {
                            //        divelements.style.left = ((events.pageX - (maindiv.offsetLeft)) - (divelements.clientWidth / 2)) + 'px';
                            //        divelements.style.top = ((events.pageY - (maindiv.parentElement.offsetTop)) - (divelements.clientHeight / 2)) + 'px';
                            //    }
                            //}
                            //else {
                            //    divelements.style.left = ((events.pageX - (maindiv.offsetLeft)) - (divelements.clientWidth / 2)) + 'px';
                            //    divelements.style.top = ((events.pageY - (maindiv.parentElement.offsetTop)) - (divelements.clientHeight / 2)) + 'px';
                            //}
                            var ParentElement: HTMLElement = maindiv;
                            var left: number = 0;
                            var top: number = 0;
                            while (ParentElement != null) {
                                top += (ParentElement.offsetTop);
                                left += (ParentElement.offsetLeft);
                                ParentElement = ParentElement.parentElement;
                            }




                            divelements.style.left = (events.pageX - left) + "px"; //(((events.pageX - maindiv.offsetLeft) - leftpane.clientWidth) - (divelements.clientWidth / 2)) + 'px';
                            divelements.style.top = (events.pageY - top) + "px"; //((events.pageY - (maindiv.parentElement.offsetTop)) - (divelements.clientHeight / 2)) + 'px';


                            if (AddXdiv.style.display != "none") {
                                var AddX: HTMLInputElement = <HTMLInputElement>document.getElementById("addxid");
                                AddX.value = divelements.style.left.replace("px", "");
                            }
                            if (AddYdiv.style.display != "none") {
                                var AddY: HTMLInputElement = <HTMLInputElement>document.getElementById("addyid");
                                AddY.value = divelements.style.top.replace("px", "");
                            }

                            maindiv.onmouseup = (event) => {
                                maindiv.onmousemove = null;
                            }
                        }
                        else if (H_TemplateType.value == "Template") {
                            var AddXdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addxdivid");
                            var AddYdiv: HTMLDivElement = <HTMLDivElement>document.getElementById("addydivid");

                            var leftpane: HTMLDivElement = <HTMLDivElement>document.getElementById("LeftPane");
                            var ToolBoxDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivToolBox");
                            var mainPage: HTMLDivElement = <HTMLDivElement>maindiv.offsetParent;
                            if (leftpane != null) {
                                var menu: HTMLUListElement = <HTMLUListElement>leftpane.childNodes[1];
                                if (menu.style.display.toLowerCase() == "none") {
                                    if (events.offsetX != 0 || events.offsetY != 0) {
                                        divelements.style.left = ((events.offsetX)) + "px";//((events.pageX - maindiv.parentElement.offsetLeft) - (divelements.clientWidth / 2)) + "px";//events.offsetX + "px";//- (mainPage.offsetLeft)+ "px";//((events.pageX - maindiv.parentElement.offsetLeft) - (divelements.clientWidth / 2)) + "px";// - (divelements.clientWidth / 2))  + "px";
                                        divelements.style.top = ((events.offsetY)) + "px";//((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";// - (mainPage.offsetTop) + "px";//((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";// - (divelements.clientHeight / 2))  + "px";
                                    }
                                }
                                else {
                                    if (events.offsetX != 0 || events.offsetY != 0) {
                                        divelements.style.left = ((events.offsetX)) + "px";//(((events.pageX - maindiv.parentElement.offsetLeft) - leftpane.clientWidth) - (divelements.clientWidth / 2)) + "px";//events.offsetX + "px";// - (mainPage.offsetLeft + leftpane.clientWidth) + "px";// (((events.pageX - maindiv.parentElement.offsetLeft) - leftpane.clientWidth) - (divelements.clientWidth / 2)) + "px";//- (divelements.clientWidth / 2)) + "px";
                                        divelements.style.top = ((events.offsetY)) + "px";//((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";// events.offsetY + "px";//- (mainPage.offsetTop) + "px";// ((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";//- (divelements.clientHeight / 2)) + "px";
                                    }
                                }
                            }
                            else {
                                if (events.offsetX != 0 || events.offsetY != 0) {
                                    divelements.style.left = ((events.offsetX)) + "px";//((events.pageX - maindiv.parentElement.offsetLeft) - (divelements.clientWidth / 2)) + "px";//events.offsetX + "px";//- (mainPage.offsetLeft + leftpane.clientWidth) + "px";// ((events.pageX - maindiv.parentElement.offsetLeft) - (divelements.clientWidth / 2)) + "px";//- (divelements.clientWidth / 2)) + "px";
                                    divelements.style.top = ((events.offsetY)) + "px";//((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";//events.offsetY + "px";//- (mainPage.offsetTop) + "px";//((events.pageY - maindiv.parentElement.offsetTop) - (divelements.clientHeight / 2)) + "px";//+ (divelements.clientHeight / 2)) + "px";
                                }
                            }
                            if (AddXdiv.style.display != "none") {
                                var AddX: HTMLInputElement = <HTMLInputElement>document.getElementById("addxid");
                                AddX.value = divelements.style.left.replace("px", "");
                            }
                            if (AddYdiv.style.display != "none") {
                                var AddY: HTMLInputElement = <HTMLInputElement>document.getElementById("addyid");
                                AddY.value = divelements.style.top.replace("px", "");
                            }
                            maindiv.onmouseup = (event) => {
                                maindiv.onmousemove = null;
                            }
                        }
                    }
                }
            }
        }
        public OnDivElementMouseUp(event): void {
            if (this.divelement != null) {
                this.divelement.onmouseup = null;
                this.divelement = null;

            }

        }
        public OnDivElementMouseOut(event): void {
            if (this.divelement != null) {
                this.divelement.onmouseup = null;
                this.divelement = null;
            }

        }
        public ActiveInactive(event): void {
            var activeinactive: boolean = false;
            if (event.currentTarget.id == "btnActive") {
                activeinactive = true;
            }
            else if (event.currentTarget.id == "btnInactive") {
                activeinactive = false;
            }
            var H_Templateid: HTMLInputElement = <HTMLInputElement>document.getElementById("H_Templateid")
            var H_TemplateidVal = -1;
            if (H_Templateid != null) {
                H_TemplateidVal = parseInt(H_Templateid.value);
            }
            var templatetype = <HTMLInputElement>document.getElementById("TempType")
            var strurl: string = "";
            if (templatetype.value == "Form") {
                strurl = "../../TenantFormDesign/ActiveInactive"
            }
            else if (templatetype.value == "Template") {
                strurl = "../../TenantTemplateDesign/ActiveInactive"
            }
            $.ajax({
                type: "POST",
                url: strurl,
                data: JSON.stringify({ templateId: H_TemplateidVal, activeInactive: activeinactive }),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (activeChanged: string) {
                    var btnActive = <HTMLInputElement>document.getElementById("btnActive");
                    var btnInactive = <HTMLInputElement>document.getElementById("btnInactive");
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
                        var status: string = "";
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
                error: function (responseText: any) {
                    alert("Oops an Error Occured");
                }
            });
        }
        public onCheckin(event): void {
            var H_Templateid: HTMLInputElement = <HTMLInputElement>document.getElementById("H_Templateid")
            var H_TemplateidVal = -1;
            if (H_Templateid != null) {
                H_TemplateidVal = parseInt(H_Templateid.value);
            }
            var templatetype = <HTMLInputElement>document.getElementById("TempType")
            var strurl: string = "";
            if (templatetype.value == "Form") {
                strurl = "../../TenantFormDesign/Checkin"
            }
            else if (templatetype.value == "Template") {
                strurl = "../../TenantTemplateDesign/Checkin"
            }
            $.ajax({
                type: "POST",
                url: strurl,
                data: JSON.stringify({ templateId: H_TemplateidVal }),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (responseText: string) {
                    if (responseText.toString().toLowerCase() == "true") {
                        var btncheckin = <HTMLInputElement>document.getElementById("btnCheckin");
                        btncheckin.style.display = "none";
                        alert("Check In successfull!!");
                    }
                    else if (responseText.toString().toLowerCase() == "false") {
                        var btncheckin = <HTMLInputElement>document.getElementById("btnCheckin");
                        btncheckin.style.display = "block";
                    }
                    else {
                        alert("An Error Occurred " + responseText.toString());
                    }
                },
                error: function (responseText: any) {
                    alert("Oops an Error Occured");
                }
            });
        }
        public onVersionSubmit(event): void {
            var majortext = <HTMLInputElement>document.getElementById("VMajor");
            var minortext = <HTMLInputElement>document.getElementById("VMinor");
            var H_Templateid: HTMLInputElement = <HTMLInputElement>document.getElementById("H_Templateid")
            var H_TemplateidVal = -1;
            if (H_Templateid != null) {
                H_TemplateidVal = parseInt(H_Templateid.value);
            }
            var templatetype = <HTMLInputElement>document.getElementById("TempType")
            var strurl: string = "";
            if (templatetype.value == "Form") {
                strurl = "../../TenantFormDesign/UpdateVersion"
            }
            else if (templatetype.value == "Template") {
                strurl = "../../TenantTemplateDesign/UpdateVersion"
            }
            var major: number = parseInt(majortext.value);
            var minor: number = parseInt(minortext.value);
            $.ajax({
                type: "POST",
                url: strurl,
                data: JSON.stringify({ templateId: H_TemplateidVal, verMajor: major, verMinor: minor }),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (responseText: string) {
                    if (responseText.toString().toLowerCase() == "true") {
                        alert("Version successfully updated!!");
                    }
                    else {
                        alert(responseText.toString());
                    }
                },
                error: function (responseText: any) {
                    alert("Oops an Error Occured: " + responseText.toString());
                }
            });


        }
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
        public onSubmitForm(event): void {
           // var a = new AffinityDms.Entities.TemplateElement();
            var ElementArray: Array<AffinityDms.Entities.TemplateElement> = AffinityDms.Entities.FormDesigner.prototype.CreateArrayOfElements();
            if (ElementArray.length <= 0) {
                alert("No Elements to Save");
            }
            else
            {
                var ExElementDetailsArray: Array<TemplateElementDetailViewModel> = AffinityDms.Entities.FormDesigner.prototype.CreateArrayOfElementDetails();
                ElementArray[0].ElementDetails = null;
                var stringyElementArray = JSON.stringify(ElementArray);
                var stringyElementDetailsArray = JSON.stringify(ExElementDetailsArray);
                var H_Templateid: HTMLInputElement = <HTMLInputElement>document.getElementById("H_Templateid")
                var H_TemplateidVal = -1;
                if (H_Templateid != null) {
                    H_TemplateidVal = parseInt(H_Templateid.value);
                }
                var templatetype = <HTMLInputElement>document.getElementById("TempType")
                var strurl: string = "";
                if (templatetype.value == "Form") {
                    strurl = "../../TenantFormDesign/SaveFormDesign"
                }
                else if (templatetype.value == "Template") {
                    strurl = "../../TenantTemplateDesign/SaveTemplateDesign"
                }
                $.ajax({
                    type: "POST",
                    url: strurl,
                    data: JSON.stringify({ Elements: stringyElementArray, ElementDetails: stringyElementDetailsArray, templateid: H_TemplateidVal }),//JSON.stringify(ElementArray),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (responseText: string) {
                        if (responseText.toString().toLowerCase() == "true") {
                            alert("Template Saved Successfully!!");
                            var templatetype = <HTMLInputElement>document.getElementById("TempType")

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
                    error: function (responseText: any) {
                        alert("Oops an Error Occured");
                    }
                });
            }
        }
        public OnDivElementMouseMove(event): void {
            // alert(this.divelement.id);
            if (this.divelement != null && this._dragging == true) {
                //fixPageXY(e)

                this.divelement.style.left = event.pageX - 25 + 'px';
                this.divelement.style.top = event.pageY - 25 + 'px';
                this.divelement.onmouseup = this.OnDivElementMouseUp;
                // document.getElementById('myElement').value = "left:" + self.style.left + " top:" + self.style.top;
                //docele.text=self.style.left;
                //.text=self.style.top;

            }
        }


        //public pushArrayElements(id:number): void {
        public count: number = 0;
        //public elementsArray: Array<AffinityDms.Entities.Element> = new Array();
        //}


        public SetTemplateElementDetailViewModelArray(Id: number, ElementId: number, ElementDivId: string, ElementDetailId: string, ElementType: Entities.ElementType, Name: string, Description: string, Text: string, X: number, Y: number, Width: string, Height: string, ForegroundColor: string, BackgroundColor: string, BorderStyle: number, Value: string, SizeMode: number): TemplateElementDetailViewModel {
            // var elementclass = new AffinityDms.Entities.TemplateElementDetail();
            var elementclass: TemplateElementDetailViewModel = new TemplateElementDetailViewModel();
            
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
        }


        public SetElementArray(Id: number, TemplateId: number, ElementId: string, ElementType: Entities.ElementType, ElementDataType: Entities.ElementDataType, Name: string, Description: string, Text: string, X: number, Y: number, X2: number, Y2: number, Width: string, Height: string, DivX: number, DivY: number, DivWidth: string, DivHeight: string, MinHeight: string, MinWidth: string, ForegroundColor: string, BackgroundColor: string, Hint: string, MinChar: string, MaxChar: string, DateTime: string, FontName: string, FontSize: number, FontStyle: number, FontColor: string, BorderStyle: number, BarcodeType: number, Value: string, ElementIndexType: number, ImageSource: string, SizeMode: number, IsSelected: string, Discriminator: string, FontGraphicsUnit: number, ColorForegroundA: number, ColorForegroundR: number, ColorForegroundG: number, ColorForegroundB: number, ColorBackgroundA: number, ColorBackgroundR: number, ColorBackgroundG: number, ColorBackgroundB: number, ElementMobileOrdinal: number, ElementValues: Array<AffinityDms.Entities.TemplateElementValue>, ElementDetails: Array<AffinityDms.Entities.TemplateElementDetail>): AffinityDms.Entities.TemplateElement {

            var elementclass: TemplateElement = new TemplateElement();
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
            else if (ElementIndexType == AffinityDms.Entities.ElementIndexType.Value)
            {
                elementclass.Value = "";
                var index = parseInt(Value);
                if (index >= 0) {
                    elementclass.DocumentIndexDataType = index;
                }
                else
                {
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
                var elementValuesArray: Array<AffinityDms.Entities.TemplateElementValue> = new Array();
                elementclass.ElementValues = elementValuesArray;
            }
            else {
                elementclass.ElementValues = ElementValues;
            }
            if (ElementDetails == null) {
                var elementDetailsArray: Array<AffinityDms.Entities.TemplateElementDetail> = new Array();
                elementclass.ElementDetails = elementDetailsArray;
            }
            else {
                elementclass.ElementDetails = ElementDetails;
            }


            return elementclass;
        }
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
        public CreateArrayOfElements(): Array<AffinityDms.Entities.TemplateElement> {
            //this.elementDetailArray = new Array();
            var count: number = 0;
            var elementsArray: Array<AffinityDms.Entities.TemplateElement> = new Array();
            var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            var templateid: number = -1;
            var discriminator: string = "Descriminator Default Value"
            for (var i = 0; i < divPage.childNodes.length; i++) {
                var elementclass = new TemplateElement();
                if (divPage.childNodes[i] instanceof (HTMLDivElement)) {
                    var divElement: HTMLDivElement = <HTMLDivElement>divPage.childNodes[i];
                    if (divElement.getAttribute("data-elementtype") == "FormControl") {
                        if (divElement.getAttribute("data-Tool") == "label") {
                            // var lblelement: HTMLLabelElement = <HTMLLabelElement>divElement.childNodes[0];
                            //var calcwidth: number = (+(divElement.style.width.replace("px", "")));
                            //var calcheight: number = (+(divElement.style.height.replace("px", "")));
                            var calcX: number = (+(divElement.style.left.replace("px", "")));
                            var calcY: number = (+(divElement.style.top.replace("px", "")))
                            var calcDivX = +(divElement.style.left.replace("px", ""));
                            var calcDivY = +(divElement.style.top.replace("px", ""));
                            
                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Label, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "", divElement.childNodes[0].textContent.toString(), calcX, calcY, -1, -1, divElement.clientWidth.toString(), divElement.clientHeight.toString(), calcDivX, calcDivY, divElement.clientWidth.toString(), divElement.clientHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, "", "", "", "", divElement.style.fontFamily, (+divElement.style.fontSize.replace("px", "")), -1, divElement.style.color, -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                            count++;
                            elementsArray.push(elementclass);

                        }
                        // else if ((divElement.childNodes[0] instanceof (HTMLInputElement))) {
                        //   var inputelement: HTMLInputElement = <HTMLInputElement>divElement.childNodes[0];
                        else if (divElement.getAttribute("data-Tool") == "textbox") {
                            var calcwidth: number = (+(divElement.style.width.replace("px", "")));
                            var calcheight: number = (+(divElement.style.height.replace("px", "")));
                            var calcX: number = (+(divElement.style.left.replace("px", "")));
                            var calcY: number = (+(divElement.style.top.replace("px", "")))
                            var calcDivX = +(divElement.style.left.replace("px", ""));
                            var calcDivY = +(divElement.style.top.replace("px", ""));
                            var img = <HTMLImageElement>divElement.childNodes[1];
                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Textbox, Entities.ElementDataType.Alphanumeric, divElement.getAttribute("data-name").toString(), "", "", calcX, calcY, -1, -1, calcwidth.toString(), calcheight.toString(), calcDivX, calcDivY, img.clientWidth.toString(), img.clientHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, divElement.getAttribute("data-placeholder").toString(), "", divElement.getAttribute("data-maxLength").toString(), "", divElement.style.fontFamily, (+divElement.style.fontSize.replace("px", "")), -1, divElement.style.color, -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                            count++;
                            elementsArray.push(elementclass);
                        }
                        else if (divElement.getAttribute("data-Tool") == "radio") {
                            //   var radiolbl: HTMLLabelElement = <HTMLLabelElement>divElement.childNodes[1]
                            var calcX: number = (+(divElement.style.left.replace("px", "")));
                            var calcY: number = (+(divElement.style.top.replace("px", "")))
                            var calcDivX = (+(divElement.style.left.replace("px", "")));
                            var calcDivY = (+(divElement.style.top.replace("px", "")));

                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Radio, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), divElement.getAttribute("data-Description"), divElement.childNodes[0].textContent.toString(), calcX, calcY, -1, -1, divElement.clientWidth.toString(), divElement.clientHeight.toString(), calcDivX, calcDivY, divElement.clientWidth.toString(), divElement.clientHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, "", "", "", "", divElement.style.fontFamily, (+divElement.style.fontSize.replace("px", "")), -1, divElement.style.color, -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, ("" + divElement.getAttribute("data-checked")), discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                            count++;
                            elementsArray.push(elementclass);
                        }
                        else if (divElement.getAttribute("data-Tool") == "checkbox") {
                            var calcX: number = (+(divElement.style.left.replace("px", "")));
                            var calcY: number = (+(divElement.style.top.replace("px", "")))
                            var calcDivX = +(divElement.style.left.replace("px", ""));
                            var calcDivY = +(divElement.style.top.replace("px", ""));
                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Checkbox, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), divElement.getAttribute("data-Description"), divElement.childNodes[0].textContent.toString(), calcX, calcY, -1, -1, divElement.clientWidth.toString(), divElement.clientHeight.toString(), calcDivX, calcDivY, divElement.clientWidth.toString(), divElement.clientHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, "", "", "", "", divElement.style.fontFamily, (+divElement.style.fontSize.replace("px", "")), -1, divElement.style.color, -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, ("" + divElement.getAttribute("data-checked")), discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                            count++;
                            elementsArray.push(elementclass);
                        }
                        else if (divElement.getAttribute("data-Tool") == "textarea") {
                            //   var textareaelement: HTMLInputElement = <HTMLInputElement>divElement.childNodes[0];
                            var calcwidth: number = (+(divElement.style.width.replace("px", "")));
                            var calcheight: number = (+(divElement.style.height.replace("px", "")));
                            var calcX: number = (+(divElement.style.left.replace("px", "")));
                            var calcY: number = (+(divElement.style.top.replace("px", "")))
                            var calcDivX = +(divElement.style.left.replace("px", ""));
                            var calcDivY = +(divElement.style.top.replace("px", ""));
                            var img = <HTMLImageElement>divElement.childNodes[1];
                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Textarea, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "", "", calcX, calcY, -1, -1, calcwidth.toString(), calcheight.toString(), calcDivX, calcDivY, img.clientWidth.toString(), img.clientHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, divElement.getAttribute("data-placeholder").toString(), "", divElement.getAttribute("data-maxLength").toString(), "", divElement.style.fontFamily, (+divElement.style.fontSize.replace("px", "")), -1, divElement.style.color, -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                            count++;
                            elementsArray.push(elementclass);
                        }

                        else if (divElement.getAttribute("data-Tool") == "image") {
                            var calcwidth: number = (+(divElement.style.width.replace("px", "")));
                            var calcheight: number = (+(divElement.style.height.replace("px", "")));
                            var calcX: number = (+(divElement.style.left.replace("px", "")));
                            var calcY: number = (+(divElement.style.top.replace("px", "")))
                            var calcDivX = +(divElement.style.left.replace("px", ""));
                            var calcDivY = +(divElement.style.top.replace("px", ""));
                            var img = <HTMLImageElement>divElement.childNodes[0];
                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Image, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), divElement.title.toString(), "", calcX, calcY, -1, -1, calcwidth.toString(), calcheight.toString(), calcDivX, calcDivY, divElement.clientWidth.toString(), divElement.clientHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), img.getAttribute("src"), -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                            count++;
                            elementsArray.push(elementclass);
                        }
                        else if (divElement.getAttribute("data-Tool") == "barcode2d") {

                            var calcwidth: number = (+(divElement.style.width.replace("px", "")));
                            var calcheight: number = (+(divElement.style.height.replace("px", "")));
                            var calcX: number = (+(divElement.style.left.replace("px", "")));
                            var calcY: number = (+(divElement.style.top.replace("px", "")))
                            var calcDivX = +(divElement.style.left.replace("px", ""));
                            var calcDivY = +(divElement.style.top.replace("px", ""));
                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Barcode2D, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), divElement.title.toString(), divElement.getAttribute("data-BarcodeText"), calcX, calcY, -1, -1, calcwidth.toString(), calcheight.toString(), calcDivX, calcDivY, divElement.clientWidth.toString(), divElement.clientHeight.toString(), divElement.style.minHeight, divElement.style.minWidth, divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), divElement.getAttribute("data-image").toString(), -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                            count++;
                            elementsArray.push(elementclass);
                        }
                        else if (divElement.getAttribute("data-Tool") == "linehorizontal") {
                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.HorizontalLine, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "LineDescription", "", -1, -1, -1, -1, "0", "0", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                            count++;
                            elementsArray.push(elementclass);
                        }
                        else if (divElement.getAttribute("data-Tool") == "linevertical") {

                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.VerticalLine, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "LineDescription", "", -1, -1, -1, -1, "0", "0", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                            count++;
                            elementsArray.push(elementclass);
                        }
                        else if (divElement.getAttribute("data-Tool") == "rectangle") {
                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Rectangle, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "RectangleDescription", "", -1, -1, -1, -1, "0", "0", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, parseInt(divElement.getAttribute("data-Ordinal")), null, null);
                            count++;
                            elementsArray.push(elementclass);
                        }
                        else if (divElement.getAttribute("data-Tool") == "circle") {
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
                        if (divElement.getAttribute("data-Tool") == "rectangle") {
                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Rectangle, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "RectangleDescription", "", -1, -1, -1, -1, "", "", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, null, null);
                            elementsArray.push(elementclass);
                            count++;
                        }
                        else if (divElement.getAttribute("data-Tool") == "table") {
                            elementclass = this.SetElementArray(count, templateid, divElement.id.toString(), Entities.ElementType.Table, Entities.ElementDataType.None, divElement.getAttribute("data-name").toString(), "TableDescription", "", (+(divElement.style.left.replace("px", ""))), (+(divElement.style.top.replace("px", ""))), -1, -1, divElement.style.width.replace("px", "").toString(), divElement.style.height.replace("px", "").toString(), -1, -1, "", "", "", "", divElement.style.color, divElement.style.backgroundColor, "", "", "", "", "", -1, -1, "", -1, -1, divElement.getAttribute("data-Value"), +(divElement.getAttribute("data-ElementIndexType")), "", -1, "", discriminator, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, null, null);
                            elementsArray.push(elementclass);
                        }
                    }
                }

            }
            return elementsArray;
        }





        public CreateArrayOfElementDetails(): Array<TemplateElementDetailViewModel> {
            var count: number = 0;
            var elementDetailArray: Array<TemplateElementDetailViewModel> = new Array();
            var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");

            for (var i = 0; i < divPage.childNodes.length; i++) {
                var leftcalc = 0;
                var leftcalcclient = 0;
                if (divPage.childNodes[i] instanceof (HTMLDivElement)) {
                    var divElement: HTMLDivElement = <HTMLDivElement>divPage.childNodes[i];

                    if ((divElement.getAttribute("data-elementtype") == "OCRControl") && divElement.getAttribute("data-Tool") == "table") {
                        var elemeid = divElement.id;
                        //.clientWidth
                        for (var j = 0; j < divElement.childNodes.length; j++) {
                            var HTMLtbleRowElement: HTMLDivElement = <HTMLDivElement>divElement.childNodes[j];
                            if (HTMLtbleRowElement.hasChildNodes) {
                                var HTMLColumnElement: HTMLDivElement = <HTMLDivElement>HTMLtbleRowElement.childNodes[0];

                                var elemDetArr = this.SetTemplateElementDetailViewModelArray(count, -1, elemeid, HTMLtbleRowElement.id, Entities.ElementType.TableColumn, HTMLtbleRowElement.getAttribute("data-name").toString(), "description", HTMLColumnElement.innerText, leftcalc, 0, HTMLtbleRowElement.clientWidth.toString(), (((+(HTMLtbleRowElement.style.height.replace("%", ""))) / 100) * (+(divElement.style.height.replace("px", "")))).toString(), "", HTMLtbleRowElement.style.backgroundColor.toString(), -1, "", -1);
                                count++;
                                leftcalc = leftcalc + (+(HTMLtbleRowElement.clientWidth));
                                elementDetailArray.push(elemDetArr);

                            }
                        }
                    }
                }

            }
            return elementDetailArray;
        }

        public editTemplateDesign(elementsviewmodel: any): void {

            //var page = this.TemplateObject.TemplateVersions[0].TemplatePages[0];
            var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            var elementsVMarray: any = JSON.parse(elementsviewmodel);
            var elementsarray = <Array<AffinityDms.Entities.TemplateElement>>elementsVMarray.elements;
            var elementdetailsarray: Array<AffinityDms.Entities.TemplateElementDetail> = elementsVMarray.elementsdetails;
            var divcounter: HTMLInputElement = <HTMLInputElement>document.getElementById("divcounter");
            var previouscountter: number = 0;

            for (var i = 0; i < elementsarray.length; i++) {
                var newcountter: number = parseInt(divcounter.value);
                var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");

                if (elementsarray[i].ElementType == ElementType.Rectangle) {
                   // var divElement: HTMLDivElement = document.createElement("div");

                    var idNumber: string = (parseInt(divcounter.value) + 1).toString();
                    if (newcountter < (parseInt(idNumber))) {
                        newcountter = parseInt(idNumber);
                    }
                    else {
                        newcountter = parseInt(idNumber);
                    }
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + idNumber;
                    divElement.setAttribute("data-ElementType", "OCRControl");
                    divElement.setAttribute("data-Tool", "rectangle");
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    //if ((elementsarray[i].Value != null) || (elementsarray[i].Value != "")) {
                    //    divElement.setAttribute("data-Value", elementsarray[i].Value);
                    //    divElement.setAttribute("data-IndexValueType", elementsarray[i].ElementIndexType.toString());
                    //}
                    //else
                    //{
                    //    divElement.setAttribute("data-Value", "");
                    //    divElement.setAttribute("data-IndexValueType", "0");
                    //}
                   

                    //divElement.setAttribute("data-IndexValueType", ElementIndexType.Label.toString());
                    //divElement.setAttribute("data-Value", "");
                    //divElement.setAttribute("data-ValueDataType", "-1");



                    //if ((elementsarray[i] != null) || (elementsarray[i].Value != "")) {
                    //    divElement.setAttribute("data-Value", elementsarray[i].Value);
                    //    divElement.setAttribute("data-IndexValueType", elementsarray[i].ElementIndexType.toString());
                    //}
                    //else {
                    //    divElement.setAttribute("data-Value", "");
                    //    divElement.setAttribute("data-IndexValueType", "0");
                    //}
                    divElement.style.zIndex = "1500";
                    
                    divElement.style.width = elementsarray[i].DivWidth + "px";
                    divElement.style.height = elementsarray[i].DivHeight + "px";
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    divElement.style.opacity = "0.7";
                    divElement.draggable = true;
                    divElement.style.paddingBottom = "5px";
                    divElement.style.paddingRight = "5px";
                    divElement.style["resize"] = "both";
                    divElement.style.border = "solid 5px grey";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    var mainImagediv: HTMLInputElement = <HTMLInputElement>document.getElementById("TempType");
                    if (mainImagediv.value == "Template") {
                        divElement.onmousedown = this.OnDivElementMouseDown;
                        var maindivs = <HTMLImageElement>document.getElementById("TemplateImage");
                        divElement.onmouseup = (event): any => {
                            maindivs.onmousemove = null;
                        }
                    }
                    divElement.onmouseover = (event: any): any => {
                        var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        // divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = idNumber;
                }
                else if (elementsarray[i].ElementType == ElementType.Table) {
                    var divElement: HTMLDivElement = document.createElement("div");
                    var idNumber: string = (parseInt(divcounter.value) + 1).toString();
                    if (newcountter < (parseInt(idNumber))) {
                        newcountter = parseInt(idNumber);
                    }
                    else {
                        newcountter = parseInt(idNumber);
                    }

                    divElement.id = "DivElementTempRow" + newcountter;// (divPage.childNodes.length + 1).toString();
                    divElement.style.width = elementsarray[i].Width + "px";
                    divElement.style.height = elementsarray[i].Height + "px";
                    divElement.style.margin = "0px";
                    divElement.style.zIndex = "1500";
                    divElement.style.left = elementsarray[i].X + "px";
                    divElement.style.top = elementsarray[i].Y + "px";
                    // divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "0.7";
                    divElement.style.border = "solid 5px grey";
                    divElement.draggable = true;
                    divElement.style["resize"] = "both";
                    divElement.style["overflow"] = "auto";
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.setAttribute("data-ElementType", "OCRControl");
                    divElement.setAttribute("data-Tool", "table");
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.style.display = "flex";
                    divElement.ondragstart = this.OnDragStart;
                    var mainImagediv: HTMLInputElement = <HTMLInputElement>document.getElementById("TempType");
                    if (mainImagediv.value == "Template") {
                        divElement.onmousedown = this.OnDivElementMouseDown;
                        var maindivs = <HTMLImageElement>document.getElementById("TemplateImage");
                        divElement.onmouseup = (event): any => {
                            maindivs.onmousemove = null;
                        }
                    }
                    divElement.onmouseover = (event: any): any => {
                        var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //   divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = newcountter.toString();
                    for (var j = 0; j < elementdetailsarray.length; j++) {
                        if (elementdetailsarray[j].ElementId == elementsarray[i].Id) {
                            var colcounter: HTMLInputElement = <HTMLInputElement>document.getElementById("colcounter");
                            var coloumncounter = 0;
                            if (colcounter.value != "0") {
                                coloumncounter = parseInt(colcounter.value);
                            }
                            var divElements: HTMLDivElement = document.createElement("div");
                            divElements.id = "DivElementColumnContainerDiv" + coloumncounter;
                            divElements.style.display = "flex";
                            divElements.style.width = elementdetailsarray[j].Width + "px";
                            divElements.style.left = elementdetailsarray[j].X + "px";
                            divElements.style.top = elementdetailsarray[j].Y + "px";
                            divElement.setAttribute("data-name", elementsarray[i].Name);
                            divElements.setAttribute("data-ElementType", "OCRControl");
                            divElement.setAttribute("data-Tool", "tablecolumn");
                            divElements.style.height = "96%";
                            divElements.style.backgroundColor = "grey";
                            divElements.style["resize"] = "horizontal";
                            divElements.style["overflow"] = "auto";
                            divElements.style.border = "1px solid black";
                            divElements.onclick = (event: any): any => {
                                var hiddenDivId = <HTMLInputElement>document.getElementById("Hidfind");
                                hiddenDivId.value = event.currentTarget.id;
                                var AddDeleteColumndiv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletecolumnbtndivid");
                                AddDeleteColumndiv.style.display = "";
                            }
                            var divElementColumn: HTMLDivElement = document.createElement("div");
                            divElementColumn.id = "DivElementColumn" + coloumncounter;//(parentTableDiv.childNodes.length + 1).toString();
                            divElementColumn.style.display = "inline-block";
                            divElementColumn.style.fontSize = "0.7em !important";
                            divElementColumn.innerText = "DivElementColumn" + coloumncounter;//(parentTableDiv.childNodes.length + 1).toString();
                            divElementColumn.style.height = "80%";
                            divElementColumn.style.wordBreak = "break-all !important";
                            divElementColumn.style["resize"] = "none !important";
                            divElementColumn.style["overflow"] = "hidden !important";
                            divElementColumn.style.textAlign = "center";
                            divElementColumn.style.border = "none !important";
                            divElements.appendChild(divElementColumn);
                            divElement.appendChild(divElements);
                            colcounter.value = (coloumncounter + 1) + "";
                            this._dragging = false;
                            this._elementId = "";
                        }
                    }
                }
            }
            divcounter.value = ((parseInt(divcounter.value)) + 1).toString();
        }
        public editFormDesign(editelements: any): void {
           // var page = this.TemplateObject.TemplateVersions[0].TemplatePages[0];
            var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            var uploaddiv: HTMLDivElement = <HTMLDivElement>document.getElementById("fileuploader");
            uploaddiv.style.display = "none";
            var elementsarray: Array<AffinityDms.Entities.TemplateElement> = JSON.parse(editelements);
            var divcounter: HTMLInputElement = <HTMLInputElement>document.getElementById("divcounter");
            var previouscountter: number = 0;
            for (var i = 0; i < elementsarray.length; i++) {
                var fontsize = elementsarray[i].FontSize;
                var newcountter: number = parseInt(divcounter.value);
                var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                var divElement: HTMLDivElement = document.createElement("div");
                if (elementsarray[i].ElementType == ElementType.Circle) {
                    //var divElement: HTMLDivElement = document.createElement("div");
                    //var idNumber: string = (parseInt(divcounter.value) + 1).toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    //else {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;//  (divPage.childNodes.length + 1).toString();
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "circle");
                    
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.style.width = elementsarray[i].DivWidth + "px";
                    divElement.style.height = elementsarray[i].DivHeight + "px";
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    // divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "0.7";
                    divElement.draggable = true;
                    divElement.style.zIndex = "1500";
                    divElement.style.paddingBottom = "0px";
                    divElement.style.paddingRight = "0px";
                    divElement.style["resize"] = "both";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        // divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1).toString();
                    // divcounter.value = idNumber;
                }
                else if (elementsarray[i].ElementType == ElementType.HorizontalLine) {
                    //var divElement: HTMLDivElement = document.createElement("div");
                    //var idNumber: string = (parseInt(divcounter.value) + 1).toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    //else {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;
                    divElement.style.zIndex = "1500";
                    divElement.style.width = elementsarray[i].DivWidth + "px";
                    divElement.style.height = elementsarray[i].DivHeight + "px";
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "linehorizontal");
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    //divElement.setAttribute("data-IndexValueType", elementsarray[i].ElementIndexType.toString());
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.style["resize"] = "horizontal";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        // divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1).toString();
                }
                else if (elementsarray[i].ElementType == ElementType.VerticalLine) {
                    //var divElement: HTMLDivElement = document.createElement("div");
                    //var idNumber: string = (parseInt(divcounter.value) + 1).toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    //else {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;
                    divElement.style.zIndex = "1500";
                    divElement.style.width = elementsarray[i].DivWidth + "px";
                    divElement.style.height = elementsarray[i].DivHeight + "px";
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "linevertical");
                    
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.style["resize"] = "vertical";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1).toString();
                }
                else if (elementsarray[i].ElementType == ElementType.Rectangle) {
                    //var divElement: HTMLDivElement = document.createElement("div");
                    //var idNumber: string = (parseInt(divcounter.value) + 1).toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    //else {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;// (divPage.childNodes.length + 1).toString();

                    divElement.style.zIndex = "1500";
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "rectangle");
                    
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.style.width = elementsarray[i].DivWidth + "px";
                    divElement.style.height = elementsarray[i].DivHeight + "px";
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    divElement.style.opacity = "0.7";
                    divElement.draggable = true;

                    divElement.style["resize"] = "both";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
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
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1).toString();
                }
                else if (elementsarray[i].ElementType == ElementType.Label) {
                    //var idNumber: string = elementsarray[i].ElementId.replace("lblElement", "").toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;//
                    divElement.style.width = "auto";//elementsarray[i].DivWidth;
                    divElement.style.height = "auto";//elementsarray[i].DivHeight;
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    //  divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "label");
                    divElement.setAttribute("data-image", "");
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.draggable = true;

                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        //   var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //   divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        // divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    var lblfield = document.createElement("label");
                    lblfield.textContent = elementsarray[i].Text;
                    lblfield.style.paddingRight = "10px";
                    divElement.appendChild(lblfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1) + "";
                }
                else if (elementsarray[i].ElementType == ElementType.Textbox) {
                    //var idNumber: string = elementsarray[i].ElementId.replace("txtbxElement", "").toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;//idNumber;
                    divElement.style.width = elementsarray[i].DivWidth + "px";;
                    divElement.style.height = elementsarray[i].DivHeight + "px";;
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    // divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.setAttribute("data-maxLength", elementsarray[i].MaxChar);
                    divElement.style["resize"] = "horizontal";
                    divElement.setAttribute("data-placeholder", "Textbox");
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "textbox");
                    
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        //var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        // divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    var lblfield = document.createElement("label");
                    lblfield.textContent = elementsarray[i].Text;
                    lblfield.style.cssFloat = "left";
                    lblfield.style.display = "inline-block";
                    lblfield.style.left = "0px";
                    lblfield.style.top = "0px";
                    lblfield.style.position = "absolute";
                    divElement.appendChild(lblfield);
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1).toString();
                }
                else if (elementsarray[i].ElementType == ElementType.Textarea) {
                    //var idNumber: string = elementsarray[i].ElementId.replace("txtareaElement", "").toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;//(divPage.childNodes.length + 1).toString();
                    divElement.style.width = elementsarray[i].DivWidth + "px";
                    divElement.style.height = elementsarray[i].DivHeight + "px";
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    //divElement.style.backgroundColor = "#FF0000";
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-maxLength", elementsarray[i].MaxChar);
                    divElement.setAttribute("data-placeholder", "Textarea");
                    divElement.setAttribute("data-Tool", "textarea");
                    
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.style.opacity = "1";
                    divElement.draggable = true;

                    divElement.style["resize"] = "both";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    var lblfield = document.createElement("label");
                    lblfield.textContent = elementsarray[i].Text;
                    lblfield.style.cssFloat = "left";
                    lblfield.style.display = "inline-block";
                    lblfield.style.left = "0px";
                    lblfield.style.top = "0px";
                    lblfield.style.position = "absolute";
                    divElement.appendChild(lblfield);
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1).toString();
                }
                else if (elementsarray[i].ElementType == ElementType.Image) {
                    //var idNumber: string = elementsarray[i].ElementId.replace("imgElement", "").toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;// idNumber;
                    divElement.style.width = elementsarray[i].DivWidth + "px";
                    divElement.style.height = elementsarray[i].DivHeight + "px";
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    // divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.setAttribute("data-Tag", "Image");
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "image");
                    
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.style["resize"] = "both";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        //   var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //   divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        //   var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
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
                    imgfield.src = elementsarray[i].ImageSource;
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1).toString();
                }
                else if (elementsarray[i].ElementType == ElementType.Barcode2D) {
                    //this.ImgData();
                    //var idNumber: string = elementsarray[i].ElementId.replace("barcode2dElement", "").toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;// idNumber;
                    divElement.style.width = elementsarray[i].DivWidth + "px";;
                    divElement.style.height = elementsarray[i].DivHeight + "px";;
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    // divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.setAttribute("data-image", "");
                    divElement.setAttribute("data-BarcodeText", elementsarray[i].BarcodeValue);
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tag", "Barcode2D");
                    divElement.setAttribute("data-Tool", "barcode2d");
                    
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.style["resize"] = "both";
                    divElement.style["overflow"] = "hidden";
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        // var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        // divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
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
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1).toString();
                }
                else if (elementsarray[i].ElementType == ElementType.Radio) {
                    //var idNumber: string = elementsarray[i].ElementId.replace("radiobtnElement", "").toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;//idNumber;
                    divElement.style.width = "auto";
                    divElement.style.height = "auto";
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    //    divElement.style.backgroundColor = "#FF0000";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "radio");
                    
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.style.paddingRight = "12.22px";
                    divElement.setAttribute("data-Description", elementsarray[i].Description);
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    var lblfield = document.createElement("label");
                    lblfield.textContent = elementsarray[i].Text;
                    lblfield.style.cssFloat = "right";
                    lblfield.style.paddingRight = "10px";

                    divElement.appendChild(lblfield);
                    var imgfield = document.createElement("img");
                    imgfield.style.width = "10px";
                    imgfield.style.height = "10px";
                    imgfield.style.margin = "5px 5px 5px 5px";
                    imgfield.style.opacity = "1";
                    //imgfield.style.left = "0px";
                    //imgfield.style.top = "0px";
                    imgfield.style.minWidth = "10px";
                    imgfield.style.minHeight = "10px";
                    imgfield.style.backgroundPosition = "50% 50%";
                    imgfield.style.backgroundRepeat = "no-repeat";
                    imgfield.src = "../../Images/Form/radiobutton.png";
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1).toString();
                }
                else if (elementsarray[i].ElementType == ElementType.Checkbox) {
                    //var idNumber: string = elementsarray[i].ElementId.replace("radiobtnElement", "").toString();
                    //if (newcountter < (parseInt(idNumber))) {
                    //    newcountter = parseInt(idNumber);
                    //}
                    var divElement: HTMLDivElement = document.createElement("div");
                    divElement.id = "DivElement" + newcountter;
                    divElement.style.width = "auto";
                    divElement.style.height = "auto";
                    divElement.style.left = elementsarray[i].DivX + "px";
                    divElement.style.top = elementsarray[i].DivY + "px";
                    divElement.style.opacity = "1";
                    divElement.draggable = true;
                    divElement.style.paddingRight = "12.22px";
                    divElement.setAttribute("data-ElementType", "FormControl");
                    divElement.setAttribute("data-Tool", "checkbox");
                    
                    divElement.setAttribute("data-Ordinal", elementsarray[i].ElementMobileOrdinal.toString());
                    if (elementsarray[i].ElementIndexType == ElementIndexType.Label) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].Value);
                    }
                    else if (elementsarray[i].ElementIndexType == ElementIndexType.Value) {
                        divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Value.toString());
                        divElement.setAttribute("data-Value", elementsarray[i].DocumentIndexDataType.toString());
                    }
                    divElement.setAttribute("data-name", elementsarray[i].Name);
                    divElement.setAttribute("data-Description", elementsarray[i].Description);
                    divElement.style.minWidth = "5px";
                    divElement.style.minHeight = "5px";
                    divElement.ondragstart = this.OnDragStart;
                    divElement.onmousedown = this.OnDivElementMouseDown;
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
                    divElement.onmouseup = (event): any => {
                        maindiv.onmousemove = null;
                    }
                    divElement.onmouseover = (event: any): any => {
                        //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "darkgoldenrod";
                    }
                    divElement.onmouseout = (event: any): any => {
                        //  var divElement: HTMLDivElement = <HTMLDivElement>document.getElementById(event.currentTarget.id);
                        //  divElement.style.backgroundColor = "#FF0000";
                    }
                    divElement.style.position = "absolute";
                    var lblfield = document.createElement("label");
                    lblfield.textContent = elementsarray[i].Text;
                    lblfield.style.cssFloat = "right";
                    lblfield.style.paddingRight = "10px";

                    divElement.appendChild(lblfield);
                    var imgfield = document.createElement("img");
                    imgfield.style.width = "10px";
                    imgfield.style.height = "10px";
                    imgfield.style.margin = "5px 5px 5px 5px";
                    imgfield.style.opacity = "1";
                    //imgfield.style.left = "0px";
                    //imgfield.style.top = "0px";
                    imgfield.style.minWidth = "10px";
                    imgfield.style.minHeight = "10px";
                    imgfield.style.backgroundPosition = "50% 50%";
                    imgfield.style.backgroundRepeat = "no-repeat";
                    imgfield.src = "../../Images/Form/checkbox.png";
                    divElement.appendChild(imgfield);
                    divPage.appendChild(divElement);
                    // alert((divElement.offsetLeft + divElement.offsetWidth) - divPage.clientWidth);
                    this._dragging = false;
                    this._elementId = "";
                    divcounter.value = (newcountter + 1).toString();
                }
            }
        }
        public createOcrZone(): any {
            var divPage: HTMLDivElement = <HTMLDivElement>document.getElementById("DivPage");
            var divElement: HTMLDivElement = document.createElement("div");
            var divcounter: HTMLInputElement = <HTMLInputElement>document.getElementById("divcounter");
            divElement.id = "DivElementTempRow" + divcounter.value;// (divPage.childNodes.length + 1).toString();
            divElement.style.zIndex = "1500";
            divElement.style.width = "20px";
            divElement.style.height = "20px";
            divElement.style.margin = "0px";
            divElement.style.left = this.GetLeft(event);
            divElement.style.top = this.GetTop(event);
            divElement.style.border = "solid 5px grey";
            //   divElement.style.backgroundColor = "#FF0000";
            divElement.style.opacity = "0.7";
            divElement.draggable = true;
            //divElement.style.paddingBottom = "15px";
            // divElement.style.paddingRight = "10px";
            divElement.style["resize"] = "both";
            divElement.style["overflow"] = "auto";
            //divElement.style.minWidth = "5px";
            //divElement.style.minHeight = "5px";
            divElement.setAttribute("data-ElementType", "OCRControl");
            divElement.setAttribute("data-Tool", "table");
            divElement.setAttribute("data-ElementIndexType", AffinityDms.Entities.ElementIndexType.Label.toString());
            divElement.setAttribute("data-Value", "");
            divElement.style.display = "flex";
            divElement.ondragstart = this.OnDragStart;
            var mainImagediv: HTMLInputElement = <HTMLInputElement>document.getElementById("TempType");
            if (mainImagediv.value == "Template") {
                divElement.onmousedown = this.OnDivElementMouseDown;
                var maindivs = <HTMLImageElement>document.getElementById("TemplateImage");
                divElement.onmouseup = (event): any => {
                    maindivs.onmousemove = null;
                }
            }
            divElement.onmouseover = (event): any => {
                //  divElement.style.backgroundColor = "darkgoldenrod";
            }
            divElement.onmouseout = (event): any => {
                // divElement.style.backgroundColor = "#FF0000";
            }
            divElement.style.position = "absolute";
            divPage.appendChild(divElement);
            this._dragging = false;
            this._elementId = "";
            divcounter.value = ((parseInt(divcounter.value)) + 1) + "";
        }
        public AddColumnToOCRTableZone(event): any {
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var colcounter: HTMLInputElement = <HTMLInputElement>document.getElementById("colcounter");
            var coloumncounter = 0;
            if (colcounter.value != "0") {
                coloumncounter = parseInt(colcounter.value);
            }
            var parentTableDiv: HTMLDivElement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            //search if its a table
            var split = parentTableDiv.id.search("DivElementTempRow");
            if (split.toString() == "0") {
                var offset = 0;
                for (var i = 0; i < parentTableDiv.childNodes.length; i++) {
                    var tbleCol: HTMLDivElement = <HTMLDivElement>parentTableDiv.childNodes[i];
                    offset += (+(tbleCol.style.width.replace("px", "")));
                }
                var divElement: HTMLDivElement = document.createElement("div");
                divElement.id = "DivElementColumnContainerDiv" + coloumncounter;//(parentTableDiv.childNodes.length + 1).toString();
                divElement.style.display = "flex";
                divElement.style.width = "10px";
                divElement.style.left = offset + "px";
                divElement.style.top = 0 + "px";
                divElement.setAttribute("data-name", "");
                divElement.setAttribute("data-ElementType", "OCRControl");
                divElement.setAttribute("data-Tool", "tablecolumn");
                divElement.style.height = "96%";
                divElement.style.backgroundColor = "grey";
                divElement.style["resize"] = "horizontal";
                divElement.style["overflow"] = "auto";
                divElement.style.border = "1px solid black";
                divElement.onclick = (event: any): any => {
                    var hiddenDivId = <HTMLInputElement>document.getElementById("Hidfind");
                    hiddenDivId.value = event.currentTarget.id;
                    var AddDeleteColumndiv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletecolumnbtndivid");
                    AddDeleteColumndiv.style.display = "";
                }
                var divElementColumn: HTMLDivElement = document.createElement("div");
                divElementColumn.id = "DivElementColumn" + coloumncounter;//(parentTableDiv.childNodes.length + 1).toString();
                divElementColumn.style.display = "inline-block";
                divElementColumn.style.fontSize = "0.7em !important";
                divElementColumn.innerText = "DivElementColumn" + coloumncounter;//(parentTableDiv.childNodes.length + 1).toString();
                divElementColumn.style.height = "80%";
                divElementColumn.style.wordBreak = "break-all !important";
                divElementColumn.style["resize"] = "none !important";
                divElementColumn.style["overflow"] = "hidden !important";
                divElementColumn.style.textAlign = "center";
                divElementColumn.style.border = "none !important";
                divElement.appendChild(divElementColumn);
                parentTableDiv.appendChild(divElement);
                colcounter.value = (coloumncounter + 1) + "";
                this._dragging = false;
                this._elementId = "";
            }
        }
        public AddX(event): any {
            var XElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addxid");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            divelement.style.left = XElement.value.toString() + "px";
        }
        public AddY(event): any {
            var YElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addyid");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            divelement.style.top = YElement.value.toString() + "px";
        }
        public AddDescription(event): any {
            var DescriptionElement: HTMLInputElement = <HTMLInputElement>document.getElementById("adddescriptionid");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            divelement.setAttribute("data-Description", DescriptionElement.value.toString());
        }
        public AddText(event): any {
            var TextElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addtextid");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            if (divelement.getAttribute("data-Tool").toLowerCase() == "label") {

                divelement.childNodes[0].textContent = TextElement.value;
            }
            else if (divelement.getAttribute("data-Tool").toLowerCase() == "textbox") {
            }
            else if (divelement.getAttribute("data-Tool").toLowerCase() == "radio" || divelement.getAttribute("data-Tool").toLowerCase() == "checkbox") {
                divelement.childNodes[0].textContent = TextElement.value;
            }
            else if (divelement.getAttribute("data-Tool").toLowerCase() == "barcode2d") {
                divelement.setAttribute("data-BarcodeText", TextElement.value);
            }
        }
        public ChangeIndexValueType(event): any {

            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement: HTMLDivElement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            var radiofor = event.currentTarget.getAttribute("data-radiofor");
            switch (radiofor) {
                case "Label":
                    {
                        //addindexradiodivid                    addindexradioid                     RADIO1
                        //addindexlabeldivid                    addindexlabelid                     LABEL ELEMENT
                        //addindexdatatyperadiodivid            addindexdatatyperadioid             RADIO2
                        //addindexvaluedivid                    addindexvalueid                     VALUE ELEMENT

                        divelement.setAttribute("data-ElementIndexType", ElementIndexType.Label.toString());
                        divelement.setAttribute("data-Value", "");
                        var IndexLabelDiv = <HTMLDivElement>document.getElementById("addindexlabeldivid");
                        IndexLabelDiv.style.display = "";
                        var IndexLabel = <HTMLInputElement>document.getElementById("addindexlabelid");
                        IndexLabel.value = divelement.getAttribute("data-Value");
                        var IndexValueDiv = <HTMLDivElement>document.getElementById("addindexvaluedivid");
                        IndexValueDiv.style.display = "none";
                        var IndexValue = <HTMLSelectElement>document.getElementById("addindexvalueid");
                        if (IndexValue != null)
                        {
                            var opt = <HTMLOptionElement>IndexValue.options[-1];
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




                        divelement.setAttribute("data-ElementIndexType", ElementIndexType.Value.toString());
                        divelement.setAttribute("data-Value", "-1");
                        var IndexValueDiv = <HTMLDivElement>document.getElementById("addindexvaluedivid");
                        IndexValueDiv.style.display = "";
                        var IndexValue = <HTMLSelectElement>document.getElementById("addindexvalueid");
                        if (IndexValue != null) {
                            var opt = <HTMLOptionElement>IndexValue.options[-1];
                            opt.selected = true;
                        }
                        var IndexLabelDiv = <HTMLDivElement>document.getElementById("addindexlabeldivid");
                        IndexLabelDiv.style.display = "none";
                        var IndexLabel = <HTMLInputElement>document.getElementById("addindexlabelid");
                        if (IndexLabel != null) {
                            IndexLabel.value = "";
                        }


                        //divelement.setAttribute("data-ElementIndexType", ElementIndexType.Value.toString());
                        //divelement.setAttribute("data-Value", "-1");
                        //var IndexLabelDiv = <HTMLDivElement>document.getElementById("addindexlabeldivid");
                        //IndexLabelDiv.style.display = "none";
                        //var IndexLabel = <HTMLInputElement>document.getElementById("addindexlabelid");
                        //IndexLabel.value = "";
                        //var IndexValueDiv = <HTMLDivElement>document.getElementById("addindexvaluedivid");
                        //IndexValueDiv.style.display = "";
                        //var IndexValue = <HTMLSelectElement>document.getElementById("addindexvalueid");
                        //if (IndexValue != null) {
                        //    //var index = parseInt(divelement.getAttribute("data-Value"));
                        //    //if (index == null)
                        //    //{
                        //    //    index = -1;
                        //    //}
                        //    var index = -1;
                        //    var opt = <HTMLOptionElement>IndexValue.options[index];
                        //    opt.selected = true;
                        //}



                       ///*

                       //  divElement.setAttribute("data-IndexValueType", ElementIndexType.Label.toString());
                       // divElement.setAttribute("data-Value", elementsarray[i].Value);
                       // divElement.setAttribute("data-ValueDataType", "-1");
                       // */



                       // //addelementindextypedivid      addelementindextypeid
                       // //addvaluedivid                 addvalueid
                       // //addelementdatatypeid          addelementdatatypeid
                       // //addelementdatatypevaluedivid  addelementdatatypevalueid

                       // if (divelement.getAttribute("data-IndexValueType") != null)
                       // {
                       //     divelement.setAttribute("data-IndexValueType", ElementIndexType.Value.toString());
                       //     divelement.setAttribute("data-Value", "");
                       // }
                       // var IndexTypeValueDiv = <HTMLDivElement>document.getElementById("addvaluedivid");
                       // var IndexTypeValue = <HTMLInputElement>document.getElementById("addvalueid");
                       // if (IndexTypeValueDiv != null)
                       // {
                       //     IndexTypeValueDiv.style.display = "none";
                       //     IndexTypeValue.value = "";
                       // }
                        
                       // var IndexDataTypeValueDiv = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
                       // var IndexValueDataType = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
                        
                       // if (IndexDataTypeValueDiv != null) {
                       //     IndexDataTypeValueDiv.style.display = "block";
                       //     if (IndexValueDataType != null) {
                       //         var SelectedDatatype;
                       //         if (divelement.getAttribute("data-ValueDataType") != null) {
                       //             SelectedDatatype = parseInt(divelement.getAttribute("data-ValueDataType"));
                       //         }
                       //         else
                       //         {
                       //             SelectedDatatype = 0;
                       //         }
                       //         var opt = <HTMLOptionElement>IndexValueDataType.options[SelectedDatatype];
                       //         opt.selected = true;
                       //     }
                       // }
                       // divelement.setAttribute("data-IndexValueType", ElementIndexType.Value.toString());
                       // break;

                        //if (divelement.getAttribute("data-IndexValueDataType") != null) {
                        //    divelement.setAttribute("data-IndexValueDataType", "0");
                        //    var IndexValueDataType = <HTMLSelectElement>document.getElementById("addelementdatatypevalueid");
                        //    var opt = <HTMLOptionElement>IndexValueDataType.options[0];
                        //    opt.selected = true;
                        //}
                        //var IndexDataTypeValueDiv = <HTMLDivElement>document.getElementById("addelementdatatypevaluedivid");
                        //if (IndexDataTypeValueDiv != null) {
                        //    IndexDataTypeValueDiv.style.display = "none";
                        //}

                        //var IndexValueDiv = <HTMLDivElement>document.getElementById("addvaluedivid");
                        //IndexValueDiv.style.display = "block";
                        //var IndexValue = <HTMLInputElement>document.getElementById("addvalueid");

                        //if (divelement.getAttribute("data-Value") != null) {
                        //    IndexValue.value = divelement.getAttribute("data-Value");
                        //}
                        //else {
                        //    IndexValue.value = "";
                        //}
                        //divelement.setAttribute("data-IndexValueType", "1");
                        //break;
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
        }
        
        //public AddIndexDataTypeValue(event): any {
        //    var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
        //    var divelement: HTMLDivElement = <HTMLDivElement>document.getElementById(currentTargetId.value);
        //    var AddValueDataType: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addvalueid");
        //    divelement.setAttribute("data-ValueDataType", AddValueDataType.selectedIndex.toString());
        //}
        public AddIndexDataTypeValue (event):any
        {
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement: HTMLDivElement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            var AddValues: HTMLSelectElement = <HTMLSelectElement>document.getElementById("addindexvalueid");
            divelement.setAttribute("data-Value", AddValues.selectedIndex.toString());
        }
        public AddValue(event): any {
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement: HTMLDivElement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            var AddValues: HTMLInputElement = <HTMLInputElement>document.getElementById("addindexlabelid");
            divelement.setAttribute("data-Value", AddValues.value.toString());
        }
        public AddMaxChar(event): any {
            var MaxCharElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addmaxcharid");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            // var childElement: HTMLElement = <HTMLElement>divele.childNodes[0];
            divelement.setAttribute("maxlength", MaxCharElement.value.toString());
        }
        public AddMinChar(event): any {
            var MinCharElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addmincharid");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            // var childElement: HTMLElement = <HTMLElement>divele.childNodes[0];
            divelement.setAttribute("minlength", MinCharElement.value.toString());
        }
        public AddFontSize(event): any {
            var FontSizeElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addfontsizeid");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            //var childElement: HTMLElement = <HTMLElement>divele.childNodes[0];
            divelement.style.fontSize = FontSizeElement.value.toString() + "px";
        }
        public AddWidth(event): any {
            var WidthElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addwidthid");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            divelement.style.width = WidthElement.value.toString() + "px";
        }
        public AddHeight(event): any {
            var HeightElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addheightid");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            divelement.style.height = HeightElement.value.toString() + "px";
        }

        public AddName(event): any {
            var NameElement: HTMLInputElement = <HTMLInputElement>document.getElementById("addnameid");
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            var divelement = <HTMLDivElement>document.getElementById(currentTargetId.value);
            divelement.setAttribute("data-name", NameElement.value.toString());
        }
        public DeleteColumnElement(event): any {
            var hiddenDivId: HTMLInputElement = <HTMLInputElement>document.getElementById("Hidfind");
            document.getElementById(hiddenDivId.value).remove();
            hiddenDivId.value = "0";
            var AddDeleteCoulndiv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletecolumnbtndivid");
            AddDeleteCoulndiv.style.display = "none";
        }
        public OnFileSelected(event): any {
            //this.onImgFilesele(event);
            var file: any = event.target.files[0];
            var path: string = file.value;
            var filereader: FileReader = new FileReader();
            filereader.addEventListener("load", (e: any) => {
                //var hiddenDivId = <HTMLInputElement>document.getElementById("Hidfind");
                var hiddenDivId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
                var divelement: HTMLDivElement = <HTMLDivElement>document.getElementById(hiddenDivId.value);
                if (divelement.getAttribute("data-Tool").toLowerCase() == "image") {
                    divelement.style.backgroundImage = "";// "url(" + e.target.result + ")";
                    var imgelement = <HTMLImageElement>divelement.childNodes[0];
                    imgelement.setAttribute("src", e.target.result);
                    divelement.style.backgroundSize = "cover";
                    divelement.style.backgroundRepeat = "no-repeat";
                }
            });
            filereader.readAsDataURL(file);
        }
        public DeleteElement(event): any {
            var currentTargetId: HTMLInputElement = <HTMLInputElement>document.getElementById("CurTarget");
            document.getElementById(currentTargetId.value).remove();
            ElementProperties.prototype.DisplayProperties(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true);
            var AddDeletediv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletebtndivid");
            AddDeletediv.style.display = "none";
            currentTargetId.value = "0";
            var hiddenDivId: HTMLInputElement = <HTMLInputElement>document.getElementById("Hidfind");
            hiddenDivId.value = "0";
            var AddDeleteCoulndiv: HTMLDivElement = <HTMLDivElement>document.getElementById("adddeletecolumnbtndivid");
            AddDeleteCoulndiv.style.display = "none";
        }
        public ZoomInDiv(event: any): any {
            var parentDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("ElementsContainer");
            parentDiv.style.transformOrigin = "left top";
            var val = parentDiv.style.transform.toString().toLowerCase();
            var res = val.replace("scale(", "");
            res = res.replace(")", "");
            parentDiv.style.transform = "scale(" + ((parseFloat(res)) + 0.01) + ")";
        }
        public ZoomOutDiv(event: any): any {
            var parentDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("ElementsContainer");
            parentDiv.style.transformOrigin = "left top";
            var val = parentDiv.style.transform.toString().toLowerCase();
            var res = val.replace("scale(", "");
            res = res.replace(")", "");
            parentDiv.style.transform = "scale(" + ((parseFloat(res)) - 0.01) + ")";
        }
        public FitDiv(event: any): any {
            var divImage: HTMLDivElement = <HTMLDivElement>document.getElementById("TemplateImage");
            var parentDiv: HTMLDivElement = <HTMLDivElement>document.getElementById("ElementsContainer");
            var MainContainer: HTMLDivElement = <HTMLDivElement>document.getElementById("MainContainer");
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

        }
        public OnDivMouseWheel(event: MouseWheelEvent): any {
            if (event.wheelDelta > 0) {
                this.ZoomInDiv(event);
            }
            else {
                this.ZoomOutDiv(event);
            }
        }
        public PrintFormClick(event: any): any {
            var printContent = document.getElementById("DivPage");
            var WinPrint = window.open('', '', 'left=0,top=0,toolbar=0,sta­tus=0,margin=none');
            WinPrint.document.write(printContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
        //public copy(event: string): any {

        //}
        public ImageMoving = false;
        public OnDivImageMouseDown(event: MouseEvent): any {

            this.ImageMoving = true;
            var H_ImageMove: HTMLInputElement = <HTMLInputElement>document.getElementById("ImageMove");
            H_ImageMove.value = "true";
            var divImage: HTMLDivElement = <HTMLDivElement>document.getElementById("TemplateImage");
            var curYPos = event.pageY;
            var curXPos = event.pageX;
            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("MainContainer");
            maindiv.onmousemove = (e: any) => {
                var H_ImageMove: HTMLInputElement = <HTMLInputElement>document.getElementById("ImageMove");
                if (H_ImageMove.value == "true") {
                    maindiv.scrollTop = maindiv.scrollTop + (curYPos - e.pageY);
                    curYPos = e.pageY;
                    maindiv.scrollLeft = maindiv.scrollLeft + (curXPos - e.pageX);//e.pageX;
                    curXPos = e.pageX;
                }
                maindiv.onmouseup = (ev: any) => {
                    var H_ImageMove: HTMLInputElement = <HTMLInputElement>document.getElementById("ImageMove");
                    H_ImageMove.value = "false";
                    var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("MainContainer");
                    maindiv.onmousemove = null;
                }
            }
        }
        public OnDivImageMouseUp(event: MouseEvent): any {
            var H_ImageMove: HTMLInputElement = <HTMLInputElement>document.getElementById("ImageMove");
            H_ImageMove.value = "false";
            var maindiv: HTMLDivElement = <HTMLDivElement>document.getElementById("MainContainer");
            maindiv.onmousemove = null;
        }



        public SaveUserSelectionModal(): any {
            var SelectedTemplate = <HTMLInputElement>document.getElementById("SelectedTemplate");
            if (SelectedTemplate != null) {
                //var TenantUsersSelectionListGrid = document.getElementById("TenantUsersSelectionListGrid");
                //TenantUsersSelectionListGrid.textContent = "";
                var SelectedUsersList = new Array<number>();
                var SelectedUsers = <NodeList>document.getElementsByName("CheckUserSelection");
                if (SelectedUsers != null) {
                    for (var i = 0; i < SelectedUsers.length; i++) {
                        var SelectedUser = <HTMLInputElement>SelectedUsers[i];
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
                    var url: string = "";
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
                        success: function (response: any) {
                            try {
                                var users: Array<User> = <Array<User>>JSON.parse(response);
                                if (users.length > 0) {
                                    var modal: any = $('#UserSelectionModal');
                                    modal.modal('hide');
                                }
                            }
                            catch (e) {
                                alert(response);
                            }
                        },
                        error: function (responseText: any) {
                            alert("Oops an Error Occured");
                        }
                    });
                }

            }
        }

        public SendExternalUserInvite(event): any {
            try {
                var EmailErrorDiv = document.getElementById("EmailErrorDiv");
                EmailErrorDiv.style.display = "none";
                EmailErrorDiv.textContent = "";
                var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
                EmailSuccessDiv.style.display = "none";
                EmailSuccessDiv.textContent = "";
                var SelectedTemplate = <HTMLInputElement>document.getElementById("SelectedTemplate");
                if (SelectedTemplate != null) {
                    var InviteUserEmail = <HTMLInputElement>document.getElementById("InviteUserEmail");
                    if (InviteUserEmail != null) {
                        if (InviteUserEmail.value != "") {
                            var SelectedTemplateVal = SelectedTemplate.value;
                            var datatype = SelectedTemplate.getAttribute("data-type");
                            var url: string = "";
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
                                success: function (response: string) {
                                    try {
                                        var EmailErrorDiv = document.getElementById("EmailErrorDiv");
                                        var EmailSuccessDiv = document.getElementById("EmailSuccessDiv");
                                        var InviteUserName = <HTMLInputElement>document.getElementById("InviteUserName");
                                        var InviteUserEmail = <HTMLInputElement>document.getElementById("InviteUserEmail");
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
                                error: function (responseText: any) {
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
        }

    }
}
