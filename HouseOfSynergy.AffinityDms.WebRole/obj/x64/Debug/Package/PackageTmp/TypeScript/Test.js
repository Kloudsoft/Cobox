var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var TestNameSpace;
(function (TestNameSpace) {
    var TestNameSpaceInner;
    (function (TestNameSpaceInner) {
        (function (TestEnum) {
            TestEnum[TestEnum["TestEnumMember1"] = 0] = "TestEnumMember1";
            TestEnum[TestEnum["TestEnumMember2"] = 1] = "TestEnumMember2";
        })(TestNameSpaceInner.TestEnum || (TestNameSpaceInner.TestEnum = {}));
        var TestEnum = TestNameSpaceInner.TestEnum;
        var TestClass = (function () {
            function TestClass() {
            }
            return TestClass;
        }());
        TestNameSpaceInner.TestClass = TestClass;
        var TestClassBase = (function () {
            function TestClassBase() {
                this.TestMemberArrayPrimirive = null;
                this.TestMemberArrayReference = new Array();
                this.TestMemberAny = "any";
                this.TestMemberDate = new Date(2016, 1, 1, 0, 0, 0, 0);
                this.TestMemberEnum = TestEnum.TestEnumMember1;
                this.TestMemberNumber = 12.2;
                this.TestMemberString = "hello";
                this.TestMemberBoolean = false;
                this.TestMemberReference = new TestClass();
                this.TestMemberArrayEnum = new Array();
                this.TestMemberArrayPrimirive = Array();
                this.TestMemberArrayReference = new Array();
            }
            return TestClassBase;
        }());
        TestNameSpaceInner.TestClassBase = TestClassBase;
        var TestClassDerived = (function (_super) {
            __extends(TestClassDerived, _super);
            function TestClassDerived() {
                TestNameSpaceInner.TestClassBase;
                _super.call(this);
            }
            return TestClassDerived;
        }(TestClassBase));
        TestNameSpaceInner.TestClassDerived = TestClassDerived;
    })(TestNameSpaceInner = TestNameSpace.TestNameSpaceInner || (TestNameSpace.TestNameSpaceInner = {}));
})(TestNameSpace || (TestNameSpace = {}));
//# sourceMappingURL=Test.js.map