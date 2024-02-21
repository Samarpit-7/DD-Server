
using System.ComponentModel.DataAnnotations;


namespace DD_Server.Models
{
    public class Audit : BaseDictionary
    {
        public Audit()
        {
        }

        public Audit(string container, string dataPoint, string dbColumnName, string fieldType, string dbDataType, string definition, string[] possibleValues, string[] synonyms, string calculatedInfo, string status, DateTime timeStamp, Guid dId, int uId)
        {
            Container = container;
            DataPoint = dataPoint;
            DbColumnName = dbColumnName;
            FieldType = fieldType;
            DbDataType = dbDataType;
            Definition = definition;
            PossibleValues = possibleValues;
            Synonyms = synonyms;
            CalculatedInfo = calculatedInfo;
            Status = status;
            TimeStamp = timeStamp;
            DId = dId;
            UId = uId;
        }

        [Key]
        public Guid Id { get; set; }
        public String Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid DId { get; set; }
        public int UId { get; set; }
        public AppUser user { get; set; }

    }
}