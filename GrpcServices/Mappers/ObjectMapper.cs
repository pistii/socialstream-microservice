namespace GrpcServices.Mappers
{
    public static class ObjectMapper
    {
        public static TTarget Map<TSource, TTarget>(TSource source) where TTarget : new()
        {
            var target = new TTarget();
            var sourceProps = typeof(TSource).GetProperties();
            var targetProps = typeof(TTarget).GetProperties();

            foreach (var sourceProp in sourceProps)
            {
                var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name);
                if (targetProp == null || !targetProp.CanWrite)
                    continue;

                var sourceValue = sourceProp.GetValue(source);
                if (sourceValue == null)
                    continue;

                // Speciális eset: StringValue
                if (targetProp.PropertyType == typeof(Google.Protobuf.WellKnownTypes.StringValue) && sourceProp.PropertyType == typeof(string))
                {
                    targetProp.SetValue(target, new Google.Protobuf.WellKnownTypes.StringValue { Value = (string)sourceValue });
                }
                // Speciális eset: Timestamp
                else if (targetProp.PropertyType == typeof(Google.Protobuf.WellKnownTypes.Timestamp) && sourceProp.PropertyType == typeof(DateTime))
                {
                    var dateTime = (DateTime)sourceValue;
                    targetProp.SetValue(target, Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)));
                }
                // Egyező típusnál sima set
                else if (targetProp.PropertyType == sourceProp.PropertyType)
                {
                    targetProp.SetValue(target, sourceValue);
                }
            }

            return target;
        }

    }
}
