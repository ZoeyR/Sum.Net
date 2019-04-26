// <copyright file="SumConverter.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net
{
    using System;
    using System.Linq;
    using System.Reflection;
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
            var constructor = objectType.GetTypeInfo().DeclaredConstructors.First();
            foreach (var (index, value) in objectType.GenericTypeArguments.Select((Type value, int index) => (index, value)))
            {
                try
                {
                    object sumValue = null;
                    if (this.innerConverter.CanConvert(value))
                    {
                        sumValue = this.innerConverter.ReadJson(reader, value, null, serializer);
                    }
                    else
                    {
                        var token = JToken.ReadFrom(reader);
                        sumValue = token.ToObject(value, serializer);
                    }

                    object[] args = { index, sumValue };
                    var sum = constructor.Invoke(args);
                    return sum;
                }
                catch
                {
                    continue;
                }
            }

            object[] defaultArgs = { -1, null };
            var something = constructor.Invoke(defaultArgs);
            return something;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer = writer ?? throw new ArgumentNullException(nameof(writer));

            var valueField = value.GetType().GetTypeInfo().GetDeclaredField("Value");
            var sumValue = valueField.GetValue(value);
            JToken token = JToken.FromObject(sumValue);
            token.WriteTo(writer);
        }
    }
}
