namespace Group14.Rambo.Lib.Dto
{
    using Newtonsoft.Json;

    /// <summary>
    /// Common base class for a all data transfer objects
    /// </summary>
    public class DtoBase
    {
        /// <summary>
        /// Converts this DTO object to a JSON string
        /// </summary>
        /// <returns>JSON string representing the DTO object</returns>
        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Converts a JSON string to a DTO object
        /// </summary>
        /// <returns>A new instance of the DTO object</returns>
        public static T FromJsonString<T>(string json) where T : DtoBase
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
