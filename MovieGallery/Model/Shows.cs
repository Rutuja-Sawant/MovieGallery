namespace MovieGallery.Model
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public partial class Shows
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rank")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Rank { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("fullTitle")]
        public string FullTitle { get; set; }

        [JsonProperty("year")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Year { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("crew")]
        public string Crew { get; set; }

        [JsonProperty("imDbRating")]
        public string ImDbRating { get; set; }

        [JsonProperty("imDbRatingCount")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ImDbRatingCount { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }
    }

    public enum TypeEnum { Show };

    public partial class Shows
    {
        public static Shows FromJson(string json) => JsonConvert.DeserializeObject<Shows>(json, MovieGallery.Model.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Shows self) => JsonConvert.SerializeObject(self, MovieGallery.Model.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var value = serializer.Deserialize<string>(reader);
            if (long.TryParse(value, out long l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Show")
            {
                return TypeEnum.Show;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            if (value == TypeEnum.Show)
            {
                serializer.Serialize(writer, "Show");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}