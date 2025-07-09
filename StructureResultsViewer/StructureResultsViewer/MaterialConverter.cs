using GoferAnalysisDTOs.Models;
using GoferAnalysisDTOs.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StructureResultsViewer
{

    public class MaterialConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IAnalysisMaterialDto);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }

        private AnalysisMaterialType ParseType(JToken obj)
        {
            var strVal = obj.Value<string>();
            switch (strVal)
            {
                case "LINEARELASTIC":
                    return AnalysisMaterialType.LinearElastic;
                case "MOHRCOULOMB":
                    return AnalysisMaterialType.MohrCoulomb;

                default:
                    var intval = obj.Value<int>();
                    return (AnalysisMaterialType)intval;
            }
        }

        public override object? ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer
        )
        {
            JObject jsonObject;
            try
            {
                jsonObject = JObject.Load(reader);
            }
            catch (Exception)
            {
                return null;
            }

            IAnalysisMaterialDto material;

            AnalysisMaterialType type = ParseType(
                jsonObject["MaterialType"] ?? jsonObject["materialType"] ?? 0
            );

            switch (type)
            {
                case AnalysisMaterialType.MohrCoulomb:
                    material = new MohrCoulombMaterialDto();
                    break;
                case AnalysisMaterialType.LinearElastic:
                    material = new LinearElasticMaterialDto();
                    break;
                default:
                    throw new Exception("Unknown: AnalysisMaterialType");
            }

            serializer.Populate(jsonObject.CreateReader(), material);
            return material;
        }
    }

    public class TupleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Tuple<double, double>) == objectType;
        }

        public override object? ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer
        )
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jObject = JObject.Load(reader);

            var target = new Tuple<double, double>(
                (double)(jObject["item1"] ?? 0),
                (double)(jObject["item2"] ?? 0)
            );

            return target;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}