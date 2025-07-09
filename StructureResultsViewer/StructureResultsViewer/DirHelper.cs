using Newtonsoft.Json;

namespace StructureResultsViewer
{
    internal class DirHelper
    {
        #region Fields
        private readonly string _path;
        private readonly JsonSerializerSettings _settings;
        #endregion

        #region Constructor
        public DirHelper(string path)
        {
            _path = path;

            _settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new MaterialConverter(),
                    new TupleConverter()
                }
            };
        }
        #endregion

        #region Private Members
        private string? ReadAllText(string name)
        {
            string fullName = Path.Combine(_path, name);
            return !File.Exists(fullName) ? null : File.ReadAllText(fullName);
        }
        #endregion

        #region Public Members
        public TypeName? ReadData<TypeName>(string name)
        {
            string? jsonData = ReadAllText(name);
            return jsonData == null
                ? default
                : JsonConvert.DeserializeObject<TypeName>(jsonData, _settings);
        }
        #endregion
    }
}
