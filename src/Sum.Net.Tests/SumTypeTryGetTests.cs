// <copyright file="SumTypeTryGetTests.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net.Tests
{
    using Xunit;

    public class SumTypeTryGetTests
    {
        [Fact]
        public void SumOneTryGetSucceedsTest()
        {
            SumType<int> sum = 1;

            Assert.True(sum.TryGet<int>(out var value));
            Assert.Equal(1, value);
        }

        [Fact]
        public void SumOneTryGetFailsTest()
        {
            SumType<int> sum = 1;

            Assert.False(sum.TryGet<string>(out var value));
            Assert.Equal(default, value);
        }

        [Fact]
        public void SumOneTryGetFailsDowncastTest()
        {
            SumType<int, string> upperSum = "foo";
            SumType<int> sum = upperSum;

            Assert.False(sum.TryGet<string>(out var value));
            Assert.Equal(default, value);
        }

        [Fact]
        public void SumTwoTryGetSucceedsTest()
        {
            SumType<int, string> sum = "foo";
            Assert.True(sum.TryGet<string>(out var stringValue));
            Assert.Equal("foo", stringValue);

            sum = 1;
            Assert.True(sum.TryGet<int>(out var intValue));
            Assert.Equal(1, intValue);
        }

        [Fact]
        public void SumTwoTryGetFailsTest()
        {
            SumType<int, string> sum = 1;
            Assert.False(sum.TryGet<string>(out var stringValue));
            Assert.Equal(default, stringValue);

            sum = "foo";
            Assert.False(sum.TryGet<int>(out var intValue));
            Assert.Equal(default, intValue);
        }

        [Fact]
        public void SumTwoTryGetDowncastUpcastSucceedsTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, string> upperSum = lowerSum;
            Assert.True(upperSum.TryGet<string>(out var stringValue));
            Assert.Equal("foo", stringValue);
        }

        [Fact]
        public void SumTwoTryGetDowncastUpcastFailsTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool> upperSum = lowerSum;
            Assert.False(upperSum.TryGet<string>(out var stringValue));
            Assert.Equal(default, stringValue);

            Assert.False(upperSum.TryGet<bool>(out var boolValue));
            Assert.Equal(default, stringValue);
        }
    }
}
