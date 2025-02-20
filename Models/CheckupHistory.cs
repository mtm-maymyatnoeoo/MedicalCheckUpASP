using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalCheckUpASP.Models
{
    [Table("checkup_histories")]
    public class CheckupHistory
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
        #region CheckupDate

        [Required(ErrorMessage = "Checkup Date is required.")]
        [Column("checkup_date")]
        public DateOnly CheckupDate { get; set; }

        #endregion

        #region Package
        [Column("package_id")]
        public int? PackageId { get; set; }
        #endregion

        #region CheckFlg
        [Column("check_flg")]
        public Boolean CheckFlg { get; set; }
        #endregion

        #region IsVaccine
        [Column("is_vaccine")]
        public Boolean IsVaccine { get; set; }
        #endregion
        #region IsCancel
        [Column("is_cancel")]
        public Boolean IsCancel { get; set; }
        #endregion
        #region DeadlineDate

        [Column("deadline_date")]
        public DateOnly DeadlineDate { get; set; }

        #endregion

        #region IsAdminConfirm
        [Column("is_admin_confirm")]
        public Boolean IsAdminConfirm { get; set; }
        #endregion


        #region Transportation Info
        [Column("transportation_info_id")]
        public int TransportationInfoId { get; set; }
        #endregion
        #endregion
    }
}
