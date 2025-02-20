using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedicalCheckUpASP.ViewModels
{
    public class PackageHostipal
    {
        #region Properties Declarations
        public int Id { get; set; }


        #region Name

        public string Name { get; set; }

        #endregion

        #region PackagePrice

        public string PackagePrice { get; set; }

        #endregion

        #region Hospital
        public string? HospitalName { get; set; }
        #endregion

        #region OldOrNew
        public string OldOrNew { get; set; }
        #endregion

        #region Year
        public string? PackageYear { get; set; }
        #endregion

        #region UniqName

        [Column("uniq_name")]
        public required string UniqName { get; set; }

        #endregion
        #region ContactInfo

        [Column("contact_info")]
        public required string ContactInfo { get; set; }

        #endregion
        #endregion
    }
}
