using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DD_Server.Models;

namespace DD_Server.Model
{
    public class Audit : BaseDictionary
    {
        // public Audit(Guid id, string container, string dataPoint, string dbColumnName, string fieldType, string dbDataType, string definition, string[] possibleValues, string[] synonyms, string calculatedInfo, string status, DateTime timeStamp, Guid dId, int uId)
        // {
        //     Id = id;
        //     Container = container;
        //     DataPoint = dataPoint;
        //     DbColumnName = dbColumnName;
        //     FieldType = fieldType;
        //     DbDataType = dbDataType;
        //     Definition = definition;
        //     PossibleValues = possibleValues;
        //     Synonyms = synonyms;
        //     CalculatedInfo = calculatedInfo;
        //     Status = status;
        //     TimeStamp = timeStamp;
        //     DId = dId;
        //     UId = uId;
        // }

        [Key]
        public Guid Id { get; set; }
        public String Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid DId { get; set; }
       
        public int UId { get; set; }
        public AppUser user { get; set; }

    }
}