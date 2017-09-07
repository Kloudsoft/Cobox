var HouseOfSynergy;
(function (HouseOfSynergy) {
    var Library;
    (function (Library) {
        var Company = (function () {
            function Company() {
            }
            Object.defineProperty(Company.prototype, "Name", {
                get: function () { return ("House of Synergy (SMC-Private) Limited"); },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Company.prototype, "Email", {
                get: function () { return ("info@houseofsynergy.com"); },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Company.prototype, "Website", {
                get: function () { return ("http://www.houseofsynergy.com/"); },
                enumerable: true,
                configurable: true
            });
            return Company;
        }());
        Library.Company = Company;
        var Version = (function () {
            function Version(major, minor, build, revision) {
                this._Major = 0;
                this._Minor = 0;
                this._Build = 0;
                this._Revision = 0;
                this._Major = major;
                this._Minor = minor;
                this._Build = build;
                this._Revision = revision;
            }
            Object.defineProperty(Version.prototype, "Major", {
                get: function () { return (this._Major); },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Version.prototype, "Minor", {
                get: function () { return (this._Minor); },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Version.prototype, "Build", {
                get: function () { return (this._Build); },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Version.prototype, "Revision", {
                get: function () { return (this._Revision); },
                enumerable: true,
                configurable: true
            });
            Version.prototype.toString = function () {
                return (this._Major.toString() + "." + this._Minor.toString() + "." + this._Build.toString() + "." + this._Revision.toString());
            };
            return Version;
        }());
        Library.Version = Version;
        var Info = (function () {
            function Info() {
            }
            Object.defineProperty(Info, "Name", {
                get: function () { return ("House of Synergy - JavaScript Library"); },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Info, "Version", {
                get: function () { return (new Version(1, 0, 0, 0)); },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Info, "Date", {
                get: function () { return (new Date(2016, 3, 19, 0, 0, 0, 0)); },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Info, "Company", {
                get: function () { return (new Company()); },
                enumerable: true,
                configurable: true
            });
            return Info;
        }());
        Library.Info = Info;
        var Validation;
        (function (Validation) {
            var ValidationResult = (function () {
                function ValidationResult(validated, message) {
                    this._Message = "";
                    this._Validated = false;
                    this._Message = message;
                    this._Validated = validated;
                }
                Object.defineProperty(ValidationResult.prototype, "Message", {
                    get: function () { return (this._Message); },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(ValidationResult.prototype, "Validated", {
                    get: function () { return (this._Validated); },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(ValidationResult, "True", {
                    get: function () { return (new ValidationResult(true, "")); },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(ValidationResult, "False", {
                    get: function () { return (new ValidationResult(false, "")); },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(ValidationResult, "Empty", {
                    get: function () { return (new ValidationResult(false, "")); },
                    enumerable: true,
                    configurable: true
                });
                return ValidationResult;
            }());
            Validation.ValidationResult = ValidationResult;
            var ValidationUtilities = (function () {
                function ValidationUtilities() {
                }
                ValidationUtilities.ValidateDomain = function (domain) {
                    var result = new ValidationResult(false, "Please enter a valid domain.");
                    if ((typeof (domain) !== 'undefined') && (domain !== null) && (domain.replace(" ", "").length > 0)) {
                        result = ValidationResult.True;
                    }
                    return (result);
                };
                ValidationUtilities.ValidateUsername = function (username) {
                    var result = new ValidationResult(false, "Please enter a valid username.");
                    if ((typeof (username) !== 'undefined') && (username !== null) && (username.replace(" ", "").length > 0)) {
                        result = ValidationResult.True;
                    }
                    return (result);
                };
                ValidationUtilities.ValidatePassword = function (password) {
                    var result = new ValidationResult(false, "Please enter a password.");
                    if ((typeof (password) !== 'undefined') && (password !== null) && (password.replace(" ", "").length > 0)) {
                        result = ValidationResult.True;
                    }
                    return (result);
                };
                return ValidationUtilities;
            }());
            Validation.ValidationUtilities = ValidationUtilities;
        })(Validation = Library.Validation || (Library.Validation = {}));
    })(Library = HouseOfSynergy.Library || (HouseOfSynergy.Library = {}));
})(HouseOfSynergy || (HouseOfSynergy = {}));
Date.prototype.Minimum = function () { return (new Date()); };
//# sourceMappingURL=HouseOfSynergy.js.map