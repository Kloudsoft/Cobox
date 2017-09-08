using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.BusinessLayer
{
	public abstract class BusinessLayerException:
		AffinityException
	{
		public BusinessLayerException (string message) : base(message) { }
		public BusinessLayerException (string message, Exception innerException) : base(message, innerException) { }
	}

	public class AuthenticationException:
		BusinessLayerException
	{
		public bool? IsValidDomain { get; private set; }
		public bool? IsValidUsername { get; private set; }
		public bool? IsValidPassword { get; private set; }

		public AuthenticationException (bool? isValidDomain = false, bool? isValidUsername = false, bool? isValidPassword = false) : this("The provided credentials are not valid.", null, isValidDomain, isValidUsername, isValidPassword) { }
		public AuthenticationException (string message, bool? isValidDomain = false, bool? isValidUsername = false, bool? isValidPassword = false) : this(message, null, isValidDomain, isValidUsername, isValidPassword) { }
		public AuthenticationException (string message, Exception innerException, bool? isValidDomain = false, bool? isValidUsername = false, bool? isValidPassword = false) : base(message, innerException) { this.IsValidDomain = isValidDomain; this.IsValidUsername = IsValidUsername; this.IsValidPassword = isValidPassword; }
	}

	public abstract class DomainException:
		AuthenticationException
	{
		public DomainException () : base("The provided domain is not valid.") { }
		public DomainException (string message) : base(message) { }
		public DomainException (string message, Exception innerException) : base(message, innerException) { }
	}

	public class DomainInvalidException:
		DomainException
	{
		public DomainInvalidException () : base("The provided domain is not valid.") { }
		public DomainInvalidException (string message) : base(message) { }
		public DomainInvalidException (string message, Exception innerException) : base(message, innerException) { }
	}

	public class DomainNotFoundException:
		DomainException
	{
		public DomainNotFoundException () : base("The provided domain was not found.") { }
		public DomainNotFoundException (string message) : base(message) { }
		public DomainNotFoundException (string message, Exception innerException) : base(message, innerException) { }
	}

	public class TokenException:
		AuthenticationException
	{
		public TokenException () : base("The provided token is not valid.") { }
		public TokenException (string message) : base(message) { }
		public TokenException (string message, Exception innerException) : base(message, innerException) { }
	}

	public sealed class TokenInvalidException:
		TokenException
	{
		public TokenInvalidException () : base("The provided token is not authorized.") { }
		public TokenInvalidException (string message) : base(message) { }
		public TokenInvalidException (string message, Exception innerException) : base(message, innerException) { }
	}

	public sealed class TokenExpiredException:
		TokenException
	{
		public TokenExpiredException () : base("The provided token has expired.") { }
		public TokenExpiredException (string message) : base(message) { }
		public TokenExpiredException (string message, Exception innerException) : base(message, innerException) { }
	}

	public sealed class UserNotFoundException:
		AuthenticationException
	{
		public UserNotFoundException () : base("The specified user was not found.") { }
		public UserNotFoundException (string message) : base(message) { }
		public UserNotFoundException (string message, Exception innerException) : base(message, innerException) { }
	}

	public sealed class TenantNotAuthorizedException:
		BusinessLayerException
	{
		public TenantNotAuthorizedException () : base("The tenant is not authorized to allow this operation.") { }
		public TenantNotAuthorizedException (string message) : base(message) { }
		public TenantNotAuthorizedException (string message, Exception innerException) : base(message, innerException) { }
	}

	public sealed class UserNotAuthorizedException:
		BusinessLayerException
	{
		public UserNotAuthorizedException () : base("The user is not authorized to perform this operation.") { }
		public UserNotAuthorizedException (string message) : base(message) { }
		public UserNotAuthorizedException (string message, Exception innerException) : base(message, innerException) { }
	}

	public sealed class BlobTypeException:
		BusinessLayerException
	{
		public BlobTypeException () : base("The blob type is incompatible.") { }
		public BlobTypeException (string message) : base(message) { }
		public BlobTypeException (string message, Exception innerException) : base(message, innerException) { }
	}

	public sealed class DocumentStateException:
		BusinessLayerException
	{
		public DocumentStateException () : base("The document is in an invalid state.") { }
		public DocumentStateException (string message) : base(message) { }
		public DocumentStateException (string message, Exception innerException) : base(message, innerException) { }
	}

	public sealed class DocumentHashMismatchException:
		BusinessLayerException
	{
		public DocumentHashMismatchException () : base("The document hash in the data store did not match the blob hash.") { }
		public DocumentHashMismatchException (string message) : base(message) { }
		public DocumentHashMismatchException (string message, Exception innerException) : base(message, innerException) { }
	}

	public sealed class RowNotFoundException:
		BusinessLayerException
	{
		public RowNotFoundException () : base("Row not found.") { }
		public RowNotFoundException (string message) : base(message) { }
		public RowNotFoundException (string message, Exception innerException) : base(message, innerException) { }
	}

	public sealed class DocumentAlreadyExistsException:
		BusinessLayerException
	{
		public Document Document { get; private set; }

		public DocumentAlreadyExistsException () : base("A document with the same hash already exists.") { }
		public DocumentAlreadyExistsException (string message) : base(message) { }
		public DocumentAlreadyExistsException (string message, Exception innerException) : base(message, innerException) { }

		public DocumentAlreadyExistsException (Document document) : base("The document already exists.") { }
		public DocumentAlreadyExistsException (Document document, string message) : base(message) { this.Document = document; }
		public DocumentAlreadyExistsException (Document document, string message, Exception innerException) : base(message, innerException) { this.Document = document; }
	}

	public sealed class DocumentTypeException:
		BusinessLayerException
	{
		public DocumentTypeException () : base("The document type is incompatible.") { }
		public DocumentTypeException (string message) : base(message) { }
		public DocumentTypeException (string message, Exception innerException) : base(message, innerException) { }
	}
}