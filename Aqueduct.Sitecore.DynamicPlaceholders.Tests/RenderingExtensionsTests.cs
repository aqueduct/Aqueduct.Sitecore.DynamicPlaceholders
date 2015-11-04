using System;
using FluentAssertions;
using NUnit.Framework;

namespace Aqueduct.Sitecore.DynamicPlaceholders.Tests
{
    [TestFixture]
    public class RenderingExtensionsTests
    {
        [TestCase("")]
        [TestCase(null)]
        public void GetParentRenderingIdsForRendering_WhenPlaceholderKeyIsInvalid_ThrowsException(string key)
        {
            Assert.Throws<ArgumentNullException>(() => RenderingExtensions.GetParentRenderingIdsForRendering(key));
        }

        [Test]
        public void GetParentRenderingIdsForRendering_WhenPlaceholderIsNotDynamic_ReturnsEmptyEnumerable()
        {
            var parents = RenderingExtensions.GetParentRenderingIdsForRendering(Keys.PlaceholderKey);

            parents.Should().NotBeNull();
            parents.Should().BeEmpty();
        }

        [TestCase("/content/12col_somerandomguid/8col_anotherrandomguid", "somerandomguid", "anotherrandomguid")]
        [TestCase("/content/12col_somerandomguid", "somerandomguid")]
        public void GetParentRenderingIdsForRendering_WhenPlaceholderIsNested_ReturnsCorrectParents(string originalKey, params string[] expectedParents)
        {
            var parents = RenderingExtensions.GetParentRenderingIdsForRendering(originalKey);

            parents.Should().NotBeNull();
            parents.Should().NotBeEmpty();
            parents.Should().BeEquivalentTo(expectedParents);
        }
    }
}