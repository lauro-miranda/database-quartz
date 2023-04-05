namespace LAB.DatabaseQuartz.Api.Infra.Quartz.Models
{
    public class JobModel
    {
        public JobModel(Guid code, string name)
        {
            Code = code;
            Name = name;
        }

        public Guid Code { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string CronExpression { get; set; } = "0/30 * * ? * * *";

        public GroupModel Group { get; set; }

        public class GroupModel
        {
            public GroupModel(Guid code)
            {
                Code = code;
                Name = code.ToString();
            }

            public Guid Code { get; set; } = Guid.NewGuid();

            public string Name { get; set; }
        }
    }
}