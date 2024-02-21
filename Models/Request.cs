using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DD_Server.Models;

namespace DD_Server.Model
{
    public class Request : BaseDictionary
    {
        public Request(string status, DateTime timeStamp, string container, string dataPoint, string dbColumnName, string fieldType, string dbDataType, string definition, string[] possibleValues, string[] synonyms, string calculatedInfo, Guid dId, int uId)
        {
            Status = status;
            TimeStamp = timeStamp;
            Container = container;
            DataPoint = dataPoint;
            DbColumnName = dbColumnName;
            FieldType = fieldType;
            DbDataType = dbDataType;
            Definition = definition;
            PossibleValues = possibleValues;
            Synonyms = synonyms;
            CalculatedInfo = calculatedInfo;
            DId = dId;
            UId = uId;
        }

        [Key]
        public Guid Id { get; set; }
        public String Status { get; set; } = "Pending";
        public DateTime TimeStamp { get; set; }
        public Guid DId { get; set; }
        public int UId { get; set; }
        public AppUser user { get; set; }

    }
}