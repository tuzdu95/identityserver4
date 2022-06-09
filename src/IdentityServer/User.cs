using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
	public class User
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
		public int? Age { get; set; }
		[Required]
		public string Gender { get; set; }
		[Required]
		public DateTime DOB { get; set; }
		public string Address { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string PasswordSalt { get; set; }
		public string PasswordHash { get; set; }
		public DateTime? ExpiredDate { get; set; }
		public string PrimaryPhone { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		public bool IsEmailConfirmed { get; set; }
		public DateTime? LastModificationTime { get; set; }
		public string LastModifierUserEmail { get; set; }
		[Required]
		public Guid TenantId { get; set; }
		public List<Claim> Claims = new List<Claim>();
	}
}
