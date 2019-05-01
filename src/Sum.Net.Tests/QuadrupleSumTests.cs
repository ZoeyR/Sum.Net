// <copyright file="QuadrupleSumTests.cs" company="Daniel Griffen">
// Copyright (c) Daniel Griffen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Sum.Net.Tests
{
    using System;
    using Xunit;

    public class QuadrupleSumTests
    {
        [Fact]
        public void ExplicitCastSuccessTest()
        {
            SumType<int, string, bool, double> sum = 1;
            Assert.Equal(1, (int)sum);

            sum = "foo";
            Assert.Equal("foo", (string)sum);

            sum = true;
            Assert.True((bool)sum);

            sum = 4.5;
            Assert.Equal(4.5, (double)sum);
        }

        [Fact]
        public void ExplicitCastFailsTest()
        {
            SumType<int, string, bool, double> sum = 1;
            Assert.Throws<InvalidCastException>(() => (string)sum);
            Assert.Throws<InvalidCastException>(() => (bool)sum);
            Assert.Throws<InvalidCastException>(() => (double)sum);

            sum = "foo";
            Assert.Throws<InvalidCastException>(() => (int)sum);
            Assert.Throws<InvalidCastException>(() => (bool)sum);
            Assert.Throws<InvalidCastException>(() => (double)sum);

            sum = true;
            Assert.Throws<InvalidCastException>(() => (int)sum);
            Assert.Throws<InvalidCastException>(() => (string)sum);
            Assert.Throws<InvalidCastException>(() => (double)sum);

            sum = 4.5;
            Assert.Throws<InvalidCastException>(() => (int)sum);
            Assert.Throws<InvalidCastException>(() => (string)sum);
            Assert.Throws<InvalidCastException>(() => (bool)sum);
        }

        [Fact]
        public void ExplicitCastDowncastSucceeds()
        {
            SumType<int, string, bool, double> sum = 1;
            SumType<int, string, bool> tripleSum = sum;
            SumType<int, string> doubleSum = sum;
            SumType<int> singleSum = sum;

            Assert.Equal(1, (int)sum);
            Assert.Equal(1, (int)tripleSum);
            Assert.Equal(1, (int)doubleSum);
            Assert.Equal(1, (int)singleSum);
        }

        [Fact]
        public void ExplicitCastDowncastUpcastSucceeds()
        {
            SumType<int, string, bool, double> sum = 4.5;
            SumType<int, string, bool> tripleSum = sum;
            SumType<int, string> doubleSum = sum;
            SumType<int> singleSum = sum;

            sum = tripleSum;
            Assert.Equal(4.5, (double)sum);

            sum = doubleSum;
            Assert.Equal(4.5, (double)sum);

            sum = singleSum;
            Assert.Equal(4.5, (double)sum);
        }

        [Fact]
        public void IsTrueTest()
        {
            SumType<int, string, bool, double> sum = 1;
            Assert.True(sum.Value is int);

            sum = "foo";
            Assert.True(sum.Value is string);

            sum = true;
            Assert.True(sum.Value is bool);

            sum = 4.5;
            Assert.True(sum.Value is double);
        }

        [Fact]
        public void IsFalseTest()
        {
            SumType<int, string, bool, double> sum = 1;
            Assert.False(sum.Value is string);
            Assert.False(sum.Value is bool);
            Assert.False(sum.Value is double);

            sum = "foo";
            Assert.False(sum.Value is int);
            Assert.False(sum.Value is bool);
            Assert.False(sum.Value is double);

            sum = true;
            Assert.False(sum.Value is int);
            Assert.False(sum.Value is string);
            Assert.False(sum.Value is double);

            sum = 4.5;
            Assert.False(sum.Value is int);
            Assert.False(sum.Value is string);
            Assert.False(sum.Value is bool);
        }
    }
}
