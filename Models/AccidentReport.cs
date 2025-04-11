using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AccidentReport
{
     [Key]
     public int Id { get; set; }

     [Required]
     public string Location { get; set; }

     [Required]
     public DateTime Date { get; set; }

     [Required]
     public string Description { get; set; }

     // Foreign Key for User (Reporter)
     [ForeignKey("User")]
     public int UserId { get; set; }
     public virtual User User { get; set; }
}