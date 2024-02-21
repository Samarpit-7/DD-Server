using System.ComponentModel.DataAnnotations;


namespace DD_Server.Models
{
    public class Dictionary : BaseDictionary
    {
        public Dictionary()
        {
        }

        public Dictionary(string container, string dataPoint, string dbColumnName, string fieldType, string dbDataType, string definition, string[] possibleValues, string[] synonyms, string calculatedInfo, DateTime timeStamp, int uId )
        {

            Container = container;
            DataPoint = dataPoint;
            DbColumnName = dbColumnName;
            FieldType = fieldType;
            DbDataType = dbDataType;
            Definition = definition;
            PossibleValues = possibleValues;
            Synonyms[0] = dataPoint;
            CalculatedInfo = calculatedInfo;
            TimeStamp = timeStamp;
            UId = uId;
        }

        [Key]
        public Guid Id { get; set; }       
        public bool IsLocked { get; set; } = false;
        public DateTime TimeStamp { get; set; }
        public int UId { get; set; } 
        public AppUser User { get; set; }

    }
}