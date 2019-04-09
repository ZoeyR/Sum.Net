// <copyright file="StrictPrimitiveConverter.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net
{
    using System;
    using Newtonsoft.Json;

    internal class StrictPrimitiveConverter : JsonConverter
    {
        private JsonSerializer defaultSerializer = new JsonSerializer();

        public override bool CanConvert(Type objectType)
        {
            return IsFloat(objectType) || IsBool(objectType) || IsInteger(objectType) || IsString(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Float:
                    return IsFloat(objectType) ? this.defaultSerializer.Deserialize(reader, objectType) : throw new InvalidCastException();
                case JsonToken.Integer:
                    return IsInteger(objectType) ? this.defaultSerializer.Deserialize(reader, objectType) : throw new InvalidCastException();
                case JsonToken.Boolean:
                    return IsBool(objectType) ? this.defaultSerializer.Deserialize(reader, objectType) : throw new InvalidCastException();
                case JsonToken.String:
                    return IsString(objectType) ? this.defaultSerializer.Deserialize(reader, objectType) : throw new InvalidCastException();
                default:
                    throw new InvalidOperationException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private static bool IsFloat(Type type)
        {
            return type == typeof(double)
                || type == typeof(float);
        }

        private static bool IsBool(Type type)
        {
            return type == typeof(bool);
        }

        private static bool IsInteger(Type type)
        {
            return type == typeof(int)
                || type == typeof(uint)
                || type == typeof(long)
                || type == typeof(ulong)
                || type == typeof(short)
                || type == typeof(ushort)
                || type == typeof(byte)
                || type == typeof(sbyte);
        }

        private static bool IsString(Type type)
        {
            return type == typeof(string);
        }
    }
}
