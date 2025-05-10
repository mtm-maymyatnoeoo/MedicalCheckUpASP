using MedicalCheckUpASP.Common;
using MedicalCheckUpASP.DbContexts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalCheckUpASP.Models
{
    #region Enum Declarations
    public enum Role
    {
        User = 0,  // Matches database INT values
        Admin = 1
    }
    public enum Status
    {
        //[Display(Name = "Gr")]
        Active = 1,
        Inactive = 0
    }

    #endregion

    [Table("users")]  // Explicitly set the table name
    public class User
    {

        #region Properties Declarations

        #region Id
        [Key]  // Marks 'id' as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        #endregion

        #region UserName

        [Column("user_name")]
        [Required(ErrorMessage = "Username is required.")]
        public required string UserName { get; set; }

        #endregion

        #region Email

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Column("email")] // Maps "Email" property to "email" column
        public required string Email { get; set; }

        #endregion

        #region Password and ConfirmPassword

        [Required(ErrorMessage = "Password is required.")]
        [Column("password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public required string Password { get; set; }

        [NotMapped]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [CompareIfPasswordNotNull("Password", ErrorMessage = "Passwords do not match")]
        public required string ConfirmPassword { get; set; }
        #endregion

        #region Role

        [Column("role")]
        public Role Role { get; set; }

        #endregion

        #region Status

        [Column("status")]
        public Status Status { get; set; }

        [NotMapped]
        public bool StatusBool
        {
            get { return Status == Status.Active; }
            set { Status = value ? Status.Active : Status.Inactive; }
        }

        #endregion

        #region CreatedAt, UpdatedAt

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("created_at")]

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        #endregion

        #region EmployeeNumber

        [Column("employee_number")]
        [Required(ErrorMessage = "Employee Number is required.")]
        [RegularExpression(@"^E-?\d{5}$", ErrorMessage = "Invalid code format. Correct format: E00347 or E-00347.")]
        public required string EmployeeNumber { get; set; }

        #endregion

        #endregion PropertiesDeclaration
    }
}
