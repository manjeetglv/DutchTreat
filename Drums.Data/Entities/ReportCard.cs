using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Drums.Data.Entities
{
    public class ReportCard
    {
        public int Id { get; set; }
        [Required]
        public string SchoolYear { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Period { get; set; }
        [Required]
        public string Grade { get; set; }
        [Required]
        public string StudentFullName { get; set; }
        [Required]
        public string ReportType { get; set; }
        public ICollection<LayoutSetting> LayoutSettings { get; set; }
        public ICollection<ReportCardContentSetting> ReportCardContentSettings { get; set; }
        public ICollection<GradeSpecificSetting> GradeSpecificSettings { get; set; }
    }
}