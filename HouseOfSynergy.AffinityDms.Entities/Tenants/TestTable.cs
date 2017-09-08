//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HouseOfSynergy.AffinityDms.Entities.Tenants.Custom
//{
//	public partial class TestTable:
//		IEntity<TestTable>
//	{
//		public virtual long Id { get; set; }

//		// Primitive types.
//		public virtual System.Byte ColumnPrimitiveByte { get; set; }
//		public virtual System.SByte ColumnPrimitiveSByte { get; set; }
//		public virtual System.Int16 ColumnPrimitiveInt16 { get; set; }
//		public virtual System.UInt16 ColumnPrimitiveUInt16 { get; set; }
//		public virtual System.Int32 ColumnPrimitiveInt32 { get; set; }
//		public virtual System.UInt32 ColumnPrimitiveUInt32 { get; set; }
//		public virtual System.Int64 ColumnPrimitiveInt64 { get; set; }
//		public virtual System.UInt64 ColumnPrimitiveUInt64 { get; set; }

//		// Native types.
//		public virtual DateTime ColumnNativeDateTime { get; set; }
//		public virtual System.Nullable<DateTime> ColumnNativeDateTimeNullable { get; set; }

//		// Arrays.
//		public virtual byte [] ColumnArrayByte { get; set; }
//		public virtual int [] ColumnArrayInt32 { get; set; }
//		public virtual DateTime [] ColumnArrayDateTime { get; set; }

//		// Special types.
//		public virtual string ColumnSpecialString { get; set; }

//		// Enum types.
//		public virtual FolderType FolderType { get; set; }

//		// Reference types.
//		public virtual Template Template { get; set; }
//		public virtual long TemplateId { get; set; }

//		// Collections.
//		public virtual ICollection<Document> Documents { get; set; }

//		public TestTable ()
//		{
//			this.Documents = new List<Document>();
//		}
//	}
//}