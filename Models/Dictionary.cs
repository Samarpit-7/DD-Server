using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD_Server.Model
{
    public class Dictionary 
    {
        public Dictionary()
        {
        }

        public Dictionary(Guid id, string container, string dataPoint, string dbColumnName, string fieldType, string dbDataType, string definition, string[] possibleValues, string[] synonyms, string calculatedInfo, bool isLocked)
        {
            Id = id;
            Container = container;
            DataPoint = dataPoint;
            DbColumnName = dbColumnName;
            FieldType = fieldType;
            DbDataType = dbDataType;
            Definition = definition;
            PossibleValues = possibleValues;
            Synonyms[0] = dataPoint;
            CalculatedInfo = calculatedInfo;
            IsLocked = isLocked;
        }

        [Key]
        public Guid Id { get; set; }

                public String Container { get; set; }
        public String DataPoint { get; set; }
        public String DbColumnName { get; set; }
        public String FieldType { get; set; }
        public String DbDataType { get; set; }
        public String Definition { get; set; }
        public String[] PossibleValues { get; set; }
        public String[] Synonyms { get; set; }
        public String CalculatedInfo { get; set; }
       
        public bool IsLocked { get; set; } = false;

    }
}