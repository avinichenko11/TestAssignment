using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RestClient.Core
{
    public class Deserializer
    {
        public static TDeserializedObject GetDeserializedObject<TDeserializedObject>(string jsonstring)
            where TDeserializedObject : class
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Error;
            settings.Error += onerror;
            TDeserializedObject result = JsonConvert.DeserializeObject<TDeserializedObject>(jsonstring, settings);

            if (result == null)
                throw new ApplicationException("Unable to deserialize JSON string to:" + typeof(TDeserializedObject));
            return result;
        }

        private static void onerror(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.ErrorContext.Error);
        }
    }
}
