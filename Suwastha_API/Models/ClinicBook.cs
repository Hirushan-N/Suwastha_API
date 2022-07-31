namespace Suwastha_API.Models
{
    public class ClinicBook
    {
		public int? Id { get; set; }
		public string? ClinicBookID { get; set; }
		public string? PatientID { get; set; }
		public string? ClinicID { get; set; }
		public DateTime? NextDate { get; set; }
		public string? CreatedDoctor { get; set; }
		public string? CreatedSection { get; set; }
		public string? ReferenceLetter { get; set; }
		public string? ClosingNote { get; set; }
		public bool? IsActive { get; set; }
		public bool? IsDeleted { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
	}
}
