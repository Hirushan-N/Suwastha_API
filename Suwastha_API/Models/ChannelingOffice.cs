using Suwastha_API.Configs;

namespace Suwastha_API.Models
{
	public class ChannelingOffice:BaseEntity
	{
		public int? Id { get; set; }
		public string? ChannelingOfficeID { get; set; }
		public string? Name { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Password { get; set; }
		public string? ConsultantID { get; set; }
		public string? ConsultantName { get; set; }
		public string? DeputyConsultantID { get; set; }
		public string? DeputyConsultantName { get; set; }

		public void setChannelingOfficeID()
        {
			this.ChannelingOfficeID = IDManager.GenarateNewIDFor("tbl_ChannelingOffice", "COF");
        }

		public byte[]? getEncryptedPassword()
        {
            if (!String.IsNullOrEmpty(this.Password))
            {
				return PasswordManager.Encrypt(this.Password);

			}
            else
            {
				return null;
			}
		}
	}
}
