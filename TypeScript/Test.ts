module TestNameSpace
{
	export module TestNameSpaceInner
	{
		export interface ITestInterface
		{
			TestInterfaceMember: number;
		}

		export enum TestEnum
		{
			TestEnumMember1 = 0,
			TestEnumMember2 = 1,
		}

		export class TestClass
		{
		}

		export abstract class TestClassBase
			implements ITestInterface
		{
			public TestInterfaceMember: number;

			public TestMemberAny: any;
			public TestMemberDate: Date;
			public TestMemberEnum: TestEnum;
			public TestMemberNumber: number;
			public TestMemberString: string;
			public TestMemberBoolean: boolean;
			public TestMemberReference: TestClass;
			public TestMemberArrayEnum: Array<TestEnum>;
			public TestMemberArrayPrimirive: Array<boolean> = null;
			public TestMemberArrayReference: Array<TestClass> = new Array<TestClassBase>();

			constructor()
			{
				this.TestMemberAny = "any";
				this.TestMemberDate = new Date(2016, 1, 1, 0, 0, 0, 0);
				this.TestMemberEnum = TestEnum.TestEnumMember1;
				this.TestMemberNumber = 12.2;
				this.TestMemberString = "hello";
				this.TestMemberBoolean = false;
				this.TestMemberReference = new TestClass();
				this.TestMemberArrayEnum = new Array<TestEnum>();
				this.TestMemberArrayPrimirive = Array<boolean>();
				this.TestMemberArrayReference = new Array<TestClass>();
			}
		}

		export class TestClassDerived
			extends TestClassBase
			implements ITestInterface
		{
			constructor()
			{
				TestNameSpaceInner.TestClassBase
				super();
			}
		}
	}
}