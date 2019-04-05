// <copyright file="SumTypeIsTests.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net.Tests
{
    using Xunit;

    public class SumTypeIsTests
    {
        [Fact]
        public void SumOneIsTrueTest()
        {
            SumType<int> sum = 1;
            Assert.True(sum.Is<int>());
        }

        [Fact]
        public void SumOneIsFalseTest()
        {
            SumType<int> sum = 1;
            Assert.False(sum.Is<string>());
        }

        [Fact]
        public void SumOneIsFalseDowncastTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            Assert.False(lowerSum.Is<string>());
        }

        [Fact]
        public void SumTwoIsTrueTest()
        {
            SumType<int, string> sum = "foo";
            Assert.True(sum.Is<string>());

            sum = 1;
            Assert.True(sum.Is<int>());
        }

        [Fact]
        public void SumTwoIsFalseTest()
        {
            SumType<int, string> sum = 1;
            Assert.False(sum.Is<string>());

            sum = "foo";
            Assert.False(sum.Is<int>());
        }

        [Fact]
        public void SumTwoIsTrueDowncastUpcastTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, string> upperSum = lowerSum;
            Assert.True(upperSum.Is<string>());
        }

        [Fact]
        public void SumTwoIsFalseDowncastUpcastTest()
        {
            SumType<int, string> sum = "foo";
            SumType<int> lowerSum = sum;
            SumType<int, bool> upperSum = lowerSum;
            Assert.False(upperSum.Is<string>());
            Assert.False(upperSum.Is<bool>());
        }
    }
}
