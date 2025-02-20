using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalCheckUpASP.Models
{
    [Table("hospital_info")]
    public class HospitalInfo
    {
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
        #region UniqName

        [Column("uniq_name")]
        public required string UniqName { get; set; }

        #endregion
        #region ContactInfo

        [Column("contact_info")]
        public required string ContactInfo { get; set; }

        #endregion

        #region Remark

        [Column("remark")]
        public required string Remark { get; set; }

        #endregion
        #endregion
    }
}
