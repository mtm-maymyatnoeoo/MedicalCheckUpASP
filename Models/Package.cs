using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalCheckUpASP.Models
{
    [Table("packages")]
    public class Package
    {
        #region Enum Declarations
        public enum PackageType
        {
            [Display(Name = "Old Package")]
            Old = 0,  // Matches database INT values
            [Display(Name = "New Package")]
            New = 1
        }
        #endregion

        #region Properties Declarations
        #region Id
        [Key]  // Marks 'id' as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        #endregion

        #region Name

        [Column("name")]
        public required string Name { get; set; }

        #endregion

        #region PackagePrice

        [Column("package_price")]
        public required string PackagePrice { get; set; }

        #endregion

        #region Hospital
        [Column("hospital_id")]
        public int? HospitalId { get; set; }
        #endregion

        #region OldOrNew Package Type
        [Column("package_type")]
        public PackageType? packageType { get; set; }
        #endregion

        #region Year
        [Column("package_year")]
        public string? PackageYear { get; set; }
        #endregion
        #endregion
    }
}
