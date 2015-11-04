using System;
using FluentAssertions;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace Aqueduct.Sitecore.DynamicPlaceholders.Tests
{
    [TestFixture]
    public class PlaceholderKeyHelperTests
    {
        [Test]
        public void IsDynamicPlaceholder_WhenPlaceholderIsDynamic_ReturnsTrue()
        {
            PlaceholderKeyHelper.IsDynamicPlaceholder(Keys.DynamicPlaceholderKey).Should().BeTrue();
        }

        [Test]
        public void IsDynamicPlaceholder_WhenPlaceholderIsNotDynamic_ReturnsFalse()
        {
            PlaceholderKeyHelper.IsDynamicPlaceholder(Keys.PlaceholderKey).Should().BeFalse();
        }

        [Test]
        public void IsDynamicPlaceholder_WhenPlaceholderIsEmptyString_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => PlaceholderKeyHelper.IsDynamicPlaceholder(string.Empty));
        }

        [Test]
        public void ExtractPlaceholderKey_WhenPlaceholderIsDynamic_ReturnsPlaceholder()
        {
            PlaceholderKeyHelper.ExtractPlaceholderKey(Keys.DynamicPlaceholderKey).Should().Be(Keys.PlaceholderKey);
        }

        [Test]
        public void ExtractPlaceholderKey_WhenPlaceholderIsNotDynamic_ReturnsPlaceholder()
        {
            PlaceholderKeyHelper.ExtractPlaceholderKey(Keys.PlaceholderKey).Should().Be(Keys.PlaceholderKey);
        }

        [Test]
        public void ExtractPlaceholderKey_WhenPlaceholderIsEmptyString_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => PlaceholderKeyHelper.ExtractPlaceholderKey(string.Empty));
        }

        [TestCase("/content/12col_somerandomguid/8col_anotherrandomguid", "/content/12col_somerandomguid/8col")]
        [TestCase("/content/12col_somerandomguid", "/content/12col")]
        public void ExtractPlaceholderKey_WhenPlaceholderIsNested_ReturnsCorrectKey(string originalKey,
            string expectedKey)
        {
            PlaceholderKeyHelper.ExtractPlaceholderKey(originalKey).Should().Be(expectedKey);
        }
    }
}