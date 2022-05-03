using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gergergergerg.Models
{
    public class InquiryHeader
    {
        [Key]
        public int Id { get; set; }
        public string IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public IdentityUser IdentityUser { get; set; }
        public DateTime InquiryDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}
