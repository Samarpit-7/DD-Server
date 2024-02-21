
using DD_Server.Models;
using DD_Server.Persistence;

namespace DD_Server.Helper
{
    public class ComparingExceptGuid
    {
        private readonly AppDbContext _context;

        public ComparingExceptGuid() {}

        public ComparingExceptGuid(AppDbContext context) {
            _context = context;
        }
        public bool AreEqualExceptGuid(BaseDictionary obj1, Dictionary obj2)
        {
            return obj1.Container == obj2.Container &&
            // obj1.DataPoint == obj2.DataPoint &&
            obj1.DbColumnName == obj2.DbColumnName &&
            obj1.FieldType == obj2.FieldType &&
            obj1.DbDataType == obj2.DbDataType &&
            obj1.Definition == obj2.Definition &&
            (obj1.PossibleValues == null && obj2.PossibleValues == null || 
            obj1.PossibleValues != null && obj2.PossibleValues != null && obj1.PossibleValues.SequenceEqual(obj2.PossibleValues)) &&
            (obj1.Synonyms == null && obj2.Synonyms == null || 
            obj1.Synonyms != null && obj2.Synonyms != null && obj1.Synonyms.SequenceEqual(obj2.Synonyms)) &&
            obj1.CalculatedInfo == obj2.CalculatedInfo;
        }

        public Audit Convert_Dictionary_to_Audit(Dictionary tempDictionary) {
            Audit Newaudit = new(
                tempDictionary.Container,
                tempDictionary.DataPoint,
                tempDictionary.DbColumnName,
                tempDictionary.FieldType,
                tempDictionary.DbDataType,
                tempDictionary.Definition,
                tempDictionary.PossibleValues,
                tempDictionary.Synonyms,
                tempDictionary.CalculatedInfo,
                "Rejected",
                tempDictionary.TimeStamp,
                tempDictionary.Id,
                tempDictionary.UId);
            return Newaudit;
        }


        public Request Convert_Dictionary_to_Request(Dictionary tempDictionary) {
            Request NewRequest = new(
                "Rejected",
                tempDictionary.TimeStamp,
                tempDictionary.Container,
                tempDictionary.DataPoint,
                tempDictionary.DbColumnName,
                tempDictionary.FieldType,
                tempDictionary.DbDataType,
                tempDictionary.Definition,
                tempDictionary.PossibleValues,
                tempDictionary.Synonyms,
                tempDictionary.CalculatedInfo,    
                tempDictionary.Id,
                tempDictionary.UId);
            return NewRequest;
        }
    }
}