// <copyright file="SumConverter.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class SumConverter : JsonConverter
    {
        private StrictPrimitiveConverter innerConverter = new StrictPrimitiveConverter();

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);

            serializer.Converters.Add(this.innerConverter);

            foreach (var (index, value) in objectType.GenericTypeArguments.Select((value, index) => (index, value)))
            {
                try
                {
                    var sumValue = token.ToObject(value, serializer);
                    object[] args = { index, sumValue };
                    return objectType.Assembly.CreateInstance(objectType.FullName, false, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, args, null, null);
                }
                catch
                {
                    continue;
                }
            }

            serializer.Converters.Remove(this.innerConverter);

            object[] defaultArgs = { -1, null };
            return objectType.Assembly.CreateInstance(objectType.FullName, false, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, defaultArgs, null, null);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer = writer ?? throw new ArgumentNullException(nameof(writer));

            var sumValue = value.GetType().GetField("Value", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(value);
            JToken token = JToken.FromObject(sumValue);
            token.WriteTo(writer);
        }
    }
}
