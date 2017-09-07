interface JQuery {
   spectrum(options: { color?: string; }): any;
}

module HTML5Demos {

   export module Dialogs {
      enum ImageProcessingCommand {
         Flip,
         Rotate,
         BorderRemove,
         Deskew,
         HolePunchRemove,
      }

      export class ImageProcessingEventArgs {
         public command: ImageProcessingCommand;
         public param: any;
      }

      export var commandNames: { [command: number]: string } = {
         0: "Flip",
         1: "Rotate",
         2: "BorderRemove",
         3: "Deskew",
         4: "HolePunchRemove"
      }

      export class ImageProcessingDlg {
         private _scanningService: lt.Scanning.JavaScript.IScanning;
         set scanningService(value: lt.Scanning.JavaScript.IScanning) {
            this._scanningService = value;
         }

         private _currentCommand: ImageProcessingCommand;
         private _currentPageNumber: number;
         private _currentCommandParam: any;

         // Create shortcuts for the dialog UI elements
         private dialogUI = {
            dialog: "#imageProcessingDialog", /* The whole dialog */
            loading: "#imageProcessingDialog_Loading",
            flipTab: {
               tabBtn: "#flipTabBtn",
               flipSelectElement: "#flip",
               previewImage: "#flipPreview"
            },
            rotateTab: {
               tabBtn: "#rotateTabBtn",
               rotateAngleSelectElement: "#rotateAngle",
               previewImage: "#rotatePreview"
            },
            borderRemoveTab: {
               tabBtn: "#borderRemoveTabBtn",
               allInput: ".borderRemoveParam",
               numericInput: ".borderRemoveParam.numeric",
               borderToRemove: {
                  leftBorderCheckbox: "#leftBorder",
                  rightBorderCheckbox: "#rightBorder",
                  topBorderCheckbox: "#topBorder",
                  bottomBorderCheckbox: "#bottomBorder"
               },
               flags: {
                  useVarianceCheckbox: "#useVariance"
               },
               borderPercentInput: "#borderPercent",
               whiteNoiseLengthInput: "#whiteNoiseLength",
               varianceInput: "#variance",
               previewImage: "#borderRemovePreview"
            },
            deskewTab: {
               tabBtn: "#deskewTabBtn",
               allInput: ".deskewParam",
               fill: {
                  checkbox: "#fill",
                  color: "#fillColor"
               },
               thresholdCheckbox: "#threshold",
               qualityRadioBtnsGroup: "input[name=quality]",
               typeRadioBtnsGroup: "input[name=type]",
               speedRadioBtnsGroup: "input[name=speed]",
               previewImage: "#deskewPreview"
            },
            holePunchRemoveTab: {
               tabBtn: "#holePunchRemoveTabBtn",
               allInput: ".holePunchRemoveParam",
               numericInput: ".holePunchRemoveParam.numeric",
               flags: {
                  useCountCheckbox: "#useCount",
                  useLocationCheckbox: "#useLocation",
                  useDPICheckbox: "#useDPI",
                  useSizeCheckbox: "#useSize"
               },
               locationRadioBtnsGroup: "input[name=location]",
               options: {
                  count: {
                     min: "#count-min",
                     max: "#count-max"
                  },
                  width: {
                     min: "#width-min",
                     max: "#width-max"
                  },
                  height: {
                     min: "#height-min",
                     max: "#height-max"
                  }
               },
               previewImage: "#holePunchRemovePreview"
            },
            argumentsErrorDiv: "#argumentsErrorDiv",
            errorDetailsText: "#errorDetailsText",
            resetBtn: "#reset",
            applyBtn: "#apply"
         }

         // Events 
         private _applyClick: { (args: ImageProcessingEventArgs): void };
         set applyClick(value: { (args: ImageProcessingEventArgs): void }) {
            this._applyClick = value;
         }

         constructor() {
            this.Init();
         }

         private Init(): void {
            // Events on tabs selection
            $(this.dialogUI.flipTab.tabBtn).bind("click", $.proxy(this.flipTabBtn_Click, this));
            $(this.dialogUI.rotateTab.tabBtn).bind("click", $.proxy(this.rotateTabBtn_Click, this));
            $(this.dialogUI.borderRemoveTab.tabBtn).bind("click", $.proxy(this.borderRemoveTabBtn_Click, this));
            $(this.dialogUI.deskewTab.tabBtn).bind("click", $.proxy(this.deskewTabBtn_Click, this));
            $(this.dialogUI.holePunchRemoveTab.tabBtn).bind("click", $.proxy(this.holePunchRemoveTabBtn_Click, this));

            // Init color picker
            $(this.dialogUI.deskewTab.fill.color).spectrum({
               color: "#000"
            });

            $(this.dialogUI.flipTab.flipSelectElement).bind("change", $.proxy(this.flipSelectElement_SelectedIndexChanged, this));
            $(this.dialogUI.rotateTab.rotateAngleSelectElement).bind("change", $.proxy(this.rotateAngleSelectElement_SelectedIndexChanged, this));
            $(this.dialogUI.borderRemoveTab.allInput).bind("change", $.proxy(this.borderRemoveInput_Changed, this));
            $(this.dialogUI.deskewTab.allInput).bind("change", $.proxy(this.deskewInput_Changed, this));
            $(this.dialogUI.holePunchRemoveTab.allInput).bind("change", $.proxy(this.holePunchRemoveInput_Changed, this));

            $(this.dialogUI.holePunchRemoveTab.allInput).bind("change", $.proxy(this.holePunchRemoveInput_Changed, this));
            $(this.dialogUI.resetBtn).bind("click", $.proxy(this.resetImageProcessingDialog, this));
            $(this.dialogUI.applyBtn).bind("click", $.proxy(this.applyBtn_Click, this));
         }

         public show(pageNumber: number): void {
            this._currentPageNumber = pageNumber;
            this.showErrorDiv(false, "");
            $(this.dialogUI.flipTab.tabBtn).click();
            $(this.dialogUI.dialog).modal();
         }

         private flipTabBtn_Click(e: JQueryEventObject): void {
            this._currentCommand = ImageProcessingCommand.Flip;
            this.updateFlipPreview();
         }

         private rotateTabBtn_Click(e: JQueryEventObject): void {
            this._currentCommand = ImageProcessingCommand.Rotate;
            this.updateRotatePreview();
         }

         private borderRemoveTabBtn_Click(e: JQueryEventObject): void {
            this._currentCommand = ImageProcessingCommand.BorderRemove;
            this.updateBorderRemovePreview();
         }

         private deskewTabBtn_Click(e: JQueryEventObject): void {
            this._currentCommand = ImageProcessingCommand.Deskew;
            this.updateDeskewPreview();
         }

         private holePunchRemoveTabBtn_Click(e: JQueryEventObject): void {
            this._currentCommand = ImageProcessingCommand.HolePunchRemove;
            this.updateHolePunchRemovePreview();
         }

         private flipSelectElement_SelectedIndexChanged(e: JQueryEventObject): void {
            this.updateFlipPreview();
         }

         private rotateAngleSelectElement_SelectedIndexChanged(e: JQueryEventObject): void {
            this.updateRotatePreview();
         }

         private borderRemoveInput_Changed(e: JQueryEventObject): void {
            if ($(e.currentTarget)[0] == $(this.dialogUI.borderRemoveTab.flags.useVarianceCheckbox)[0]) {
               if ($(e.currentTarget).is(':checked'))
                  $(this.dialogUI.borderRemoveTab.varianceInput).prop('disabled', false);
               else
                  $(this.dialogUI.borderRemoveTab.varianceInput).prop('disabled', true);
            }

            this.updateBorderRemovePreview();
         }

         private deskewInput_Changed(e: JQueryEventObject): void {
            this.updateDeskewPreview();
         }

         private holePunchRemoveInput_Changed(e: JQueryEventObject): void {
            if ($(e.currentTarget)[0] == $(this.dialogUI.holePunchRemoveTab.flags.useSizeCheckbox)[0]) {
               if ($(e.currentTarget).is(':checked')) {
                  $(this.dialogUI.holePunchRemoveTab.options.width.min).prop('disabled', false);
                  $(this.dialogUI.holePunchRemoveTab.options.width.max).prop('disabled', false);
                  $(this.dialogUI.holePunchRemoveTab.options.height.min).prop('disabled', false);
                  $(this.dialogUI.holePunchRemoveTab.options.height.max).prop('disabled', false);
               } else {
                  $(this.dialogUI.holePunchRemoveTab.options.width.min).prop('disabled', true);
                  $(this.dialogUI.holePunchRemoveTab.options.width.max).prop('disabled', true);
                  $(this.dialogUI.holePunchRemoveTab.options.height.min).prop('disabled', true);
                  $(this.dialogUI.holePunchRemoveTab.options.height.max).prop('disabled', true);
               }
            } else if ($(e.currentTarget)[0] == $(this.dialogUI.holePunchRemoveTab.flags.useCountCheckbox)[0]) {
               if ($(e.currentTarget).is(':checked')) {
                  $(this.dialogUI.holePunchRemoveTab.options.count.min).prop('disabled', false);
                  $(this.dialogUI.holePunchRemoveTab.options.count.max).prop('disabled', false);
               } else {
                  $(this.dialogUI.holePunchRemoveTab.options.count.min).prop('disabled', true);
                  $(this.dialogUI.holePunchRemoveTab.options.count.max).prop('disabled', true);
               }
            } else if ($(e.currentTarget)[0] == $(this.dialogUI.holePunchRemoveTab.flags.useLocationCheckbox)[0]) {
               if ($(e.currentTarget).is(':checked')) {
                  $(this.dialogUI.holePunchRemoveTab.locationRadioBtnsGroup).prop('disabled', false);
               } else {
                  $(this.dialogUI.holePunchRemoveTab.locationRadioBtnsGroup).prop('disabled', true);
               }
            }

            this.updateHolePunchRemovePreview();
         }

         public resetImageProcessingDialog(): void {
            // Flip tab
            $(this.dialogUI.flipTab.flipSelectElement).val("horizontal");
            // Rotate tab
            $(this.dialogUI.rotateTab.rotateAngleSelectElement).val("90");
            // Border remove tab
            $(this.dialogUI.borderRemoveTab.borderToRemove.leftBorderCheckbox).prop('checked', true);
            $(this.dialogUI.borderRemoveTab.borderToRemove.rightBorderCheckbox).prop('checked', true);
            $(this.dialogUI.borderRemoveTab.borderToRemove.topBorderCheckbox).prop('checked', true);
            $(this.dialogUI.borderRemoveTab.borderToRemove.bottomBorderCheckbox).prop('checked', true);
            $(this.dialogUI.borderRemoveTab.flags.useVarianceCheckbox).prop('checked', true);
            $(this.dialogUI.borderRemoveTab.varianceInput).prop('disabled', false);
            $(this.dialogUI.borderRemoveTab.borderPercentInput).val("20");
            $(this.dialogUI.borderRemoveTab.whiteNoiseLengthInput).val("9");
            $(this.dialogUI.borderRemoveTab.varianceInput).val("3");
            // Deskew tab
            $(this.dialogUI.deskewTab.fill.checkbox).prop('checked', true);
            $(this.dialogUI.deskewTab.fill.color).spectrum({ color: "#000" });
            $(this.dialogUI.deskewTab.thresholdCheckbox).prop('checked', true);
            $(this.dialogUI.deskewTab.qualityRadioBtnsGroup + "[value='0']").prop('checked', true);
            $(this.dialogUI.deskewTab.typeRadioBtnsGroup + "[value='0']").prop('checked', true);
            $(this.dialogUI.deskewTab.speedRadioBtnsGroup + "[value='0']").prop('checked', true);
            // Hole punch remove tab
            $(this.dialogUI.holePunchRemoveTab.flags.useCountCheckbox).prop('checked', true);
            $(this.dialogUI.holePunchRemoveTab.options.count.min).prop('disabled', false);
            $(this.dialogUI.holePunchRemoveTab.options.count.max).prop('disabled', false);
            $(this.dialogUI.holePunchRemoveTab.flags.useLocationCheckbox).prop('checked', true);
            $(this.dialogUI.holePunchRemoveTab.locationRadioBtnsGroup).prop('disabled', false);
            $(this.dialogUI.holePunchRemoveTab.flags.useDPICheckbox).prop('checked', true);
            $(this.dialogUI.holePunchRemoveTab.flags.useSizeCheckbox).prop('checked', false);
            $(this.dialogUI.holePunchRemoveTab.options.width.min).prop('disabled', true);
            $(this.dialogUI.holePunchRemoveTab.options.width.max).prop('disabled', true);
            $(this.dialogUI.holePunchRemoveTab.options.height.min).prop('disabled', true);
            $(this.dialogUI.holePunchRemoveTab.options.height.max).prop('disabled', true);
            $(this.dialogUI.holePunchRemoveTab.locationRadioBtnsGroup + "[value='1']").prop('checked', true);
            $(this.dialogUI.holePunchRemoveTab.options.count.min).val("2");
            $(this.dialogUI.holePunchRemoveTab.options.count.max).val("4");
            $(this.dialogUI.holePunchRemoveTab.options.width.min).val("0");
            $(this.dialogUI.holePunchRemoveTab.options.width.max).val("50");
            $(this.dialogUI.holePunchRemoveTab.options.height.min).val("0");
            $(this.dialogUI.holePunchRemoveTab.options.height.max).val("50");

            this.updateFlipPreview();
            this.updateRotatePreview();
            this.updateBorderRemovePreview();
            this.updateHolePunchRemovePreview();
            this.updateDeskewPreview();
         }

         private updateFlipPreview(): void {
            var param: any = new Object();

            $(this.dialogUI.loading).show();
            (<HTMLImageElement>$(this.dialogUI.flipTab.previewImage)[0]).onload = () => {
               $(this.dialogUI.loading).hide();
            };

            (<HTMLImageElement>$(this.dialogUI.flipTab.previewImage)[0]).onerror = () => {
               $(this.dialogUI.loading).hide();
               this.showLastError();
            };

            if ($(this.dialogUI.flipTab.flipSelectElement).val() == "horizontal") {
               param.horizontal = true;
            } else {
               param.horizontal = false;
            }

            (<HTMLImageElement>$(this.dialogUI.flipTab.previewImage)[0]).src = this._scanningService.getImageProcessingPreview(this._currentPageNumber, commandNames[this._currentCommand], param, 260, 260, 0, 0);

            this._currentCommandParam = param;
         }

         private updateRotatePreview(): void {
            var param: any = new Object();
            param.angle = parseInt($(this.dialogUI.rotateTab.rotateAngleSelectElement).val());

            $(this.dialogUI.loading).show();
            (<HTMLImageElement>$(this.dialogUI.rotateTab.previewImage)[0]).onload = () => {
               $(this.dialogUI.loading).hide();
            };

            (<HTMLImageElement>$(this.dialogUI.rotateTab.previewImage)[0]).onerror = () => {
               $(this.dialogUI.loading).hide();
               this.showLastError();
            };

            (<HTMLImageElement>$(this.dialogUI.rotateTab.previewImage)[0]).src = this._scanningService.getImageProcessingPreview(this._currentPageNumber, commandNames[this._currentCommand], param, 260, 260, 0, 0);

            this._currentCommandParam = param;
         }

         private updateBorderRemovePreview(): void {
            if (this.checkNumericInputValidity($(this.dialogUI.borderRemoveTab.numericInput))) {
               var param: any = new Object();

               if ($(this.dialogUI.borderRemoveTab.flags.useVarianceCheckbox).is(':checked'))
                  param.flags = 2048;
               else
                  param.flags = 0;

               param.border = 0;

               if ($(this.dialogUI.borderRemoveTab.borderToRemove.leftBorderCheckbox).is(':checked'))
                  param.border |= 1;
               if ($(this.dialogUI.borderRemoveTab.borderToRemove.rightBorderCheckbox).is(':checked'))
                  param.border |= 2;
               if ($(this.dialogUI.borderRemoveTab.borderToRemove.topBorderCheckbox).is(':checked'))
                  param.border |= 4;
               if ($(this.dialogUI.borderRemoveTab.borderToRemove.bottomBorderCheckbox).is(':checked'))
                  param.border |= 8;

               param.percent = $(this.dialogUI.borderRemoveTab.borderPercentInput).val();

               param.whiteNoiseLength = $(this.dialogUI.borderRemoveTab.whiteNoiseLengthInput).val();

               param.variance = $(this.dialogUI.borderRemoveTab.varianceInput).val();

               $(this.dialogUI.loading).show();
               (<HTMLImageElement>$(this.dialogUI.borderRemoveTab.previewImage)[0]).onload = () => {
                  $(this.dialogUI.loading).hide();
               };

               (<HTMLImageElement>$(this.dialogUI.borderRemoveTab.previewImage)[0]).onerror = () => {
                  $(this.dialogUI.loading).hide();
                  this.showLastError();
               };

               (<HTMLImageElement>$(this.dialogUI.borderRemoveTab.previewImage)[0]).src = this._scanningService.getImageProcessingPreview(this._currentPageNumber, commandNames[this._currentCommand], param, 260, 260, 0, 0);

               this._currentCommandParam = param;
            }
         }

         private updateDeskewPreview(): void {
            var param: any = new Object();

            var fillColor: any = new Object();

            var color = $(this.dialogUI.deskewTab.fill.color).spectrum("get").toRgb();

            fillColor.A = color.a * 255;
            fillColor.R = color.r;
            fillColor.G = color.g;
            fillColor.B = color.b;

            param.fillColor = JSON.stringify(fillColor);

            param.flags = 0;

            if (!$(this.dialogUI.deskewTab.fill.checkbox).is(':checked'))
               param.flags |= 16;

            if ($(this.dialogUI.deskewTab.thresholdCheckbox).is(':checked'))
               param.flags |= 256;

            param.flags |= $(this.dialogUI.deskewTab.qualityRadioBtnsGroup + ":checked").val();
            param.flags |= $(this.dialogUI.deskewTab.typeRadioBtnsGroup + ":checked").val();
            param.flags |= $(this.dialogUI.deskewTab.speedRadioBtnsGroup + ":checked").val();

            param.angleRange = 0;//ignored
            param.angleResolution = 0;//ignored

            $(this.dialogUI.loading).show();
            (<HTMLImageElement>$(this.dialogUI.deskewTab.previewImage)[0]).onload = () => {
               $(this.dialogUI.loading).hide();
            };

            (<HTMLImageElement>$(this.dialogUI.deskewTab.previewImage)[0]).onerror = () => {
               $(this.dialogUI.loading).hide();
               this.showLastError();
            };

            (<HTMLImageElement>$(this.dialogUI.deskewTab.previewImage)[0]).src = this._scanningService.getImageProcessingPreview(this._currentPageNumber, commandNames[this._currentCommand], param, 260, 260, 0, 0);

            this._currentCommandParam = param;
         }

         private updateHolePunchRemovePreview(): void {
            if (this.checkNumericInputValidity($(this.dialogUI.holePunchRemoveTab.numericInput))) {
               var param: any = new Object();

               param.flags = 0;

               if ($(this.dialogUI.holePunchRemoveTab.flags.useDPICheckbox).is(':checked'))
                  param.flags |= 1;

               if ($(this.dialogUI.holePunchRemoveTab.flags.useSizeCheckbox).is(':checked'))
                  param.flags |= 32;

               if ($(this.dialogUI.holePunchRemoveTab.flags.useCountCheckbox).is(':checked'))
                  param.flags |= 64;

               if ($(this.dialogUI.holePunchRemoveTab.flags.useLocationCheckbox).is(':checked'))
                  param.flags |= 128;

               param.location = 0;

               param.location |= $(this.dialogUI.holePunchRemoveTab.locationRadioBtnsGroup + ":checked").val();

               param.minimumHoleCount = $(this.dialogUI.holePunchRemoveTab.options.count.min).val();
               param.maximumHoleCount = $(this.dialogUI.holePunchRemoveTab.options.count.max).val();

               param.minimumHoleWidth = $(this.dialogUI.holePunchRemoveTab.options.width.min).val();
               param.minimumHoleHeight = $(this.dialogUI.holePunchRemoveTab.options.height.min).val();

               param.maximumHoleWidth = $(this.dialogUI.holePunchRemoveTab.options.width.max).val();
               param.maximumHoleHeight = $(this.dialogUI.holePunchRemoveTab.options.height.max).val();

               $(this.dialogUI.loading).show();
               (<HTMLImageElement>$(this.dialogUI.holePunchRemoveTab.previewImage)[0]).onload = () => {
                  $(this.dialogUI.loading).hide();
               };

               (<HTMLImageElement>$(this.dialogUI.holePunchRemoveTab.previewImage)[0]).onerror = () => {
                  $(this.dialogUI.loading).hide();
                  this.showLastError();
               };

               (<HTMLImageElement>$(this.dialogUI.holePunchRemoveTab.previewImage)[0]).src = this._scanningService.getImageProcessingPreview(this._currentPageNumber, commandNames[this._currentCommand], param, 260, 260, 0, 0);

               this._currentCommandParam = param;
            }
         }

         private checkNumericInputValidity(numericInput: JQuery): boolean {
            var errorDetails: string = "";
            var isValid = true;

            if (numericInput.length > 0) {
               for (var i: number = 0; i < numericInput.length; i++) {
                  var inputValue: number = $(numericInput[i]).val();
                  var min: number = parseInt($(numericInput[i]).attr("min"));
                  var max: number = parseInt($(numericInput[i]).attr("max"));

                  // Check for valid integer input
                  if (inputValue.toString().search(new RegExp("^-?[0-9]+$")) == 0) {
                     $(numericInput[i]).removeClass("inputError");
                     // Check for valid in range input
                     if (inputValue >= min && inputValue <= max) {
                        $(numericInput[i]).removeClass("inputError");
                     } else {
                        isValid = false;
                        $(numericInput[i]).addClass("inputError");
                        errorDetails += "-  " + $(numericInput[i]).attr("name") + " : Out of Range";
                        errorDetails += "\n";
                     }
                  } else {
                     isValid = false;
                     $(numericInput[i]).addClass("inputError");
                     errorDetails += "-  Enter Valid Integer Number For " + $(numericInput[i]).attr("name");
                     errorDetails += "\n";
                  }
               }
            }

            this.showErrorDiv(!isValid, errorDetails);
            return isValid;
         }

         public hideHolePunchRemoveTab(): void {
            $(this.dialogUI.holePunchRemoveTab.tabBtn).hide();
         }

         private showErrorDiv(show: boolean, errorDetails: string): void {
            // New line for IE9
            if (lt.LTHelper.browser == lt.LTBrowser.internetExplorer) {
               if (lt.LTDevice.desktop && lt.LTHelper.version == 9) {
                  errorDetails = errorDetails.replace(new RegExp('\n', 'g'), '\n\r');
               }
            }

            $(this.dialogUI.errorDetailsText).text(errorDetails);
            show ? $(this.dialogUI.argumentsErrorDiv).css("display", "block") : $(this.dialogUI.argumentsErrorDiv).css("display", "none");
         }

         private showLastError(): void {
            this._scanningService.getStatus((status) => {
               if (status.errCode != 1)
                  window.alert(status.errMessage);
            }, () => { });
         }

         private applyBtn_Click(e: JQueryEventObject): void {
            var args = new ImageProcessingEventArgs();
            args.command = this._currentCommand;
            args.param = this._currentCommandParam;
            if (this._applyClick != null)
               this._applyClick(args);
         }
      }
   }
}  