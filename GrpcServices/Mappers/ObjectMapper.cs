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

        public static TTarget ExcludeNullMapper<TSource, TTarget>(TSource source) where TTarget : new()
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

                // Ha a cél típus string, de a forrás null → string.Empty
                if (targetProp.PropertyType == typeof(string))
                {
                    var stringValue = sourceValue as string ?? string.Empty;
                    targetProp.SetValue(target, stringValue);
                }
                // Nullable value type-ok: null maradjon (pl. int?)
                else if (IsNullableValueType(targetProp.PropertyType))
                {
                    targetProp.SetValue(target, sourceValue);
                }
                // Protobuf StringValue
                else if (targetProp.PropertyType == typeof(Google.Protobuf.WellKnownTypes.StringValue) && sourceProp.PropertyType == typeof(string))
                {
                    var stringValue = (string?)sourceValue ?? string.Empty;
                    targetProp.SetValue(target, new Google.Protobuf.WellKnownTypes.StringValue { Value = stringValue });
                }
                // Protobuf Timestamp
                else if (targetProp.PropertyType == typeof(Google.Protobuf.WellKnownTypes.Timestamp) && sourceProp.PropertyType == typeof(DateTime))
                {
                    var dateTime = (DateTime)sourceValue;
                    targetProp.SetValue(target, Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)));
                }
                // Egyező típusnál, ha nem null, set
                else if (targetProp.PropertyType == sourceProp.PropertyType && sourceValue != null)
                {
                    targetProp.SetValue(target, sourceValue);
                }
            }

            return target;
        }

        private static bool IsNullableValueType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }
    

    }
}
