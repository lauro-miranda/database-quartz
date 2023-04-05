namespace LAB.DatabaseQuartz.Api.Infra.Quartz.Jobs.Models
{
    public class ConciliationModel
    {
        public ConciliationModel(int startWith, ConciliationType type)
        {
            StartWith = startWith;
            Type = type;
        }

        public int StartWith { get; set; }

        public ConciliationType Type { get; set; }

        public enum ConciliationType
        {
            Daily = 1,
            Monthly = 2
        }
    }
}