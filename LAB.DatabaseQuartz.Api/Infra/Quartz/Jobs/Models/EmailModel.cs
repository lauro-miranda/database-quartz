namespace LAB.DatabaseQuartz.Api.Infra.Quartz.Jobs.Models
{
    public class EmailModel
    {
        public EmailModel(string nomeFrom, string emailFrom)
        {
            NomeFrom = nomeFrom;
            EmailFrom = emailFrom;
        }

        public string NomeFrom { get; set; } = "";

        public string EmailFrom { get; set; } = "";
    }
}