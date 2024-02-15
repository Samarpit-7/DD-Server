using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD_Server.Models
{
    public class BaseDictionary
    {
        public String Container { get; set; }
        public String DataPoint { get; set; }
        public String DbColumnName { get; set; }
        public String FieldType { get; set; }
        public String DbDataType { get; set; }
        public String Definition { get; set; }
        public String[] PossibleValues { get; set; }
        public String[] Synonyms { get; set; }
        public String CalculatedInfo { get; set; }
    }
}