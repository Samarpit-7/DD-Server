
using DD_Server.Models;
using DD_Server.Persistence;

namespace DD_Server.Helper
{
    public class ComparingExceptGuid
    {
        private readonly AppDbContext _context;

        public ComparingExceptGuid() { }

        public ComparingExceptGuid(AppDbContext context)
        {
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

        public Audit Convert_Dictionary_to_Audit(Dictionary tempDictionary)
        {
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

        public Dictionary Convert_Audit_to_Dictionary(Audit audit)
        {
            Dictionary newDictionary = new Dictionary()
            {
                Container = audit.Container,
                DataPoint = audit.DataPoint,
                DbColumnName = audit.DbColumnName,
                FieldType = audit.FieldType,
                DbDataType = audit.DbDataType,
                Definition = audit.Definition,
                PossibleValues = audit.PossibleValues,
                Synonyms = audit.Synonyms,
                CalculatedInfo = audit.CalculatedInfo,
                TimeStamp = audit.TimeStamp,
                UId = audit.UId
            };

            return newDictionary;
        }


        public Request Convert_Dictionary_to_Request(Dictionary tempDictionary,String action)
        {
            Request NewRequest = new(
                "Pending",
                action,
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