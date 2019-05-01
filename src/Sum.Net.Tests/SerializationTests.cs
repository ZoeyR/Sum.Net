// <copyright file="SerializationTests.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net.Tests
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Xunit;

    public class SerializationTests
    {
        [Fact]
        public void TransparentSerialization()
        {
            SumType<int, string, bool, double> sum = 4.5;
            var serializedSum = JsonConvert.SerializeObject(sum);

            Assert.Equal("4.5", serializedSum);
        }

        [Fact]
        public void ComplexTypeSerialization()
        {
            SumType<Dictionary<string, string>, string, ComplexClass, double> sum = new ComplexClass()
            {
                StringProperty = "foo",
                RenamedProperty = 42,
            };

            var serializedSum = JsonConvert.SerializeObject(sum);
            Assert.Equal("{\"StringProperty\":\"foo\",\"renamed\":42}", serializedSum);
        }

        [Fact]
        public void TransparentFloatDeserialization()
        {
            var sum = JsonConvert.DeserializeObject<SumType<int, bool, double, string>>("4.5");

            Assert.True(sum.Value is double);

            if (sum.Value is double doubleValue)
            {
                Assert.Equal(4.5, doubleValue);
            }
        }

        [Fact]
        public void TransparentBoolDeserialization()
        {
            var sum = JsonConvert.DeserializeObject<SumType<int, bool, double, string>>("true");

            Assert.True(sum.Value is bool);

            if (sum.Value is bool boolValue)
            {
                Assert.True(boolValue);
            }
        }

        [Fact]
        public void TransparentIntDeserialization()
        {
            var sum = JsonConvert.DeserializeObject<SumType<int, bool, double, string>>("42");

            Assert.True(sum.Value is int);

            if (sum.Value is int intValue)
            {
                Assert.Equal(42, intValue);
            }
        }

        [Fact]
        public void TransparentStringDeserialization()
        {
            var sum = JsonConvert.DeserializeObject<SumType<int, bool, double, string>>("\"true\"");

            Assert.True(sum.Value is string);

            if (sum.Value is string stringValue)
            {
                Assert.Equal("true", stringValue);
            }
        }

        [Fact]
        public void DeserializationUninhabited()
        {
            var sum = JsonConvert.DeserializeObject<SumType<int>>("true");

            Assert.False(sum.Value is int);
        }

        [Fact]
        public void ComplexTypeDeserialization()
        {
            var sum = JsonConvert.DeserializeObject<SumType<string, bool, ComplexClass, double>>("{\"StringProperty\":\"foo\",\"renamed\":42}");

            Assert.True(sum.Value is ComplexClass);

            if (sum.Value is ComplexClass complex)
            {
                Assert.Equal("foo", complex.StringProperty);
                Assert.Equal(42, complex.RenamedProperty);
            }
        }

        [DataContract]
        public class ComplexClass
        {
            [DataMember]
            public string StringProperty { get; set; }

            [DataMember(Name = "renamed")]
            public int RenamedProperty { get; set; }
        }
    }
}
