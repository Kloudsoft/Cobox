module HouseOfSynergy
{
	export module Library
	{
		export class Company
		{
			public get Name(): string { return ("House of Synergy (SMC-Private) Limited"); }
			public get Email(): string { return ("info@houseofsynergy.com"); }
			public get Website(): string { return ("http://www.houseofsynergy.com/"); }
		}

		export class Version
		{
			private _Major: number = 0;
			private _Minor: number = 0;
			private _Build: number = 0;
			private _Revision: number = 0;

			public get Major(): number { return (this._Major); }
			public get Minor(): number { return (this._Minor); }
			public get Build(): number { return (this._Build); }
			public get Revision(): number { return (this._Revision); }

			constructor(major: number, minor: number, build: number, revision: number)
			{
				this._Major = major;
				this._Minor = minor;
				this._Build = build;
				this._Revision = revision;
			}

			public toString(): string
			{
				return (this._Major.toString() + "." + this._Minor.toString() + "." + this._Build.toString() + "." + this._Revision.toString());
			}
		}

		export class Info
		{
			public static get Name(): string { return ("House of Synergy - JavaScript Library"); }
			public static get Version(): HouseOfSynergy.Library.Version { return (new Version(1, 0, 0, 0)); }
			public static get Date(): Date { return (new Date(2016, 3, 19, 0, 0, 0, 0)); }
			public static get Company(): HouseOfSynergy.Library.Company { return (new Company()); }
		}

		export module Validation
		{
			export class ValidationResult
			{
				private _Message: string = "";
				private _Validated: boolean = false;

				public get Message(): string { return (this._Message); }
				public get Validated(): boolean { return (this._Validated); }

				constructor(validated: boolean, message: string)
				{
					this._Message = message;
					this._Validated = validated;
				}

				public static get True(): HouseOfSynergy.Library.Validation.ValidationResult { return (new ValidationResult(true, "")); }
				public static get False(): HouseOfSynergy.Library.Validation.ValidationResult { return (new ValidationResult(false, "")); }
				public static get Empty(): HouseOfSynergy.Library.Validation.ValidationResult { return (new ValidationResult(false, "")); }
			}

			export class ValidationUtilities
			{
				public static ValidateDomain(domain: string): ValidationResult
				{
					var result = new ValidationResult(false, "Please enter a valid domain.");

					if ((typeof (domain) !== 'undefined') && (domain !== null) && (domain.replace(" ", "").length > 0))
					{
						result = ValidationResult.True;
					}

					return (result);
				}

				public static ValidateUsername(username: string): ValidationResult
				{
					var result = new ValidationResult(false, "Please enter a valid username.");

					if ((typeof (username) !== 'undefined') && (username !== null) && (username.replace(" ", "").length > 0))
					{
						result = ValidationResult.True;
					}

					return (result);
				}

				public static ValidatePassword(password: string): ValidationResult
				{
					var result = new ValidationResult(false, "Please enter a password.");

					if ((typeof (password) !== 'undefined') && (password !== null) && (password.replace(" ", "").length > 0))
					{
						result = ValidationResult.True;

						//var countDigits = 0;
						//var countSymbols = 0;
						//var countLetters = 0;
						//var value = password.replace(" ", "");

						//for (var i = 0; i < value.length; i++)
						//{
						//	if ((value.charCodeAt(i) >= "0".charCodeAt(0)) && (value.charCodeAt(i) <= "9".charCodeAt(0)))
						//	{
						//		countDigits++;
						//	}
						//	else if (((value.charCodeAt(i) >= "A".charCodeAt(0)) && (value.charCodeAt(i) <= "Z".charCodeAt(0))) || ((value.charCodeAt(i) >= "a".charCodeAt(0)) && (value.charCodeAt(i) <= "z".charCodeAt(0))))
						//	{
						//		countLetters++;
						//	}
						//	else
						//	{
						//		countSymbols++;
						//	}
						//}

						//if ((countLetters > 1) && (countDigits > 1) && (countSymbols > 1))
						//{
						//	result = ValidationResult.True;
						//}
					}

					return (result);
				}
			}
		}
	}
}

interface Date
{
    Minimum: Date;
}

(<any> Date.prototype).Minimum = function () { return (new Date()); }