
using System.ComponentModel.DataAnnotations;


namespace DD_Server.Models
{
    public class Audit : BaseDictionary
    {
        public Audit()
        {
        }

        public Audit(string container, string dataPoint, string dbColumnName, string fieldType, string dbDataType, string definition, string[] possibleValues, string[] synonyms, string calculatedInfo, string action, DateTime timeStamp, Guid dId, int uId)
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
            Action = action;
            TimeStamp = timeStamp;
            DId = dId;
            UId = uId;
        }

        [Key]
        public Guid Id { get; set; }
        public String Action { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid DId { get; set; }
        public int UId { get; set; }
        public AppUser user { get; set; }

    }
}