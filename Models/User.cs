using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
     // Primary key with auto-increment
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public int Id { get; set; }

     // Required name field
     [Required]
     public string Name { get; set; }

     // Required email field with validation
     [Required]
     [EmailAddress]
     public string Email { get; set; }

     // List of accident reports associated with the user
     public virtual ICollection<AccidentReport> AccidentReports { get; set; } = new List<AccidentReport>();
}