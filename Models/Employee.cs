using MedicalCheckUpASP.DbContexts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalCheckUpASP.Models
{
    [Table("employee")]  // Explicitly set the table name
    public class Employee
    {
        #region Properties Declarations

        #region Id
        [Key]  // Marks 'id' as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        #endregion

        #region EmployeeNumber

        [Column("employee_number")]
        [Required(ErrorMessage = "Employee Number is required.")]
        [RegularExpression(@"^E-?\d{5}$", ErrorMessage = "Invalid code format. Correct format: E00347 or E-00347.")]
        public required string EmployeeNumber { get; set; }

        #endregion

        #region Name

        [Column("name")]
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(255,ErrorMessage = "Name lenght must be under 255 words.")]
        public required string Name { get; set; }

        #endregion

        #region EntryDate

        [Required(ErrorMessage = "Entry Date is required.")]
        [Column("entry_date")]
        public DateOnly EntryDate { get; set; }

        #endregion

        #region ResignDate

        [Column("resign_date")]
        public DateOnly? ResignDate { get; set; }

        #endregion

        #region Position
        [Required(ErrorMessage = "Position is required.")]
        [Column("position")]
        public required string Position { get; set; }

        #endregion

        #region NRC

        [Column("nrc")]
        public required string NRC { get; set; }

        #endregion

        #region DOB

        [Column("dob")]
        [Required(ErrorMessage = "DOB is required.")]

        public DateOnly DOB { get; set; }

        #endregion

        #region FirstImportStatus

        [Column("first_import_status")]
        public bool first_import_status { get; set; }

        #endregion


        #region CreatedAt, UpdatedAt

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        #endregion

        #region ProfileImagePath
        [Column("profile_image_path")]
        public string? ProfileImagePath { get; set; }

        [Required(ErrorMessage = "Please select a profile picture")]
        public IFormFile? ProfileImage { get; set; } // For file upload
        #endregion
        #region CheckupHistories
        public ICollection<CheckupHistory> CheckupHistories { get; set; }
        #endregion
        #endregion
    }
}
