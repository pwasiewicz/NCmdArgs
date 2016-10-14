using System.Collections.Generic;
using System.Linq;
using NCmdArgs.Tests.Options;
using Xunit;

namespace NCmdArgs.Tests
{
    public class CollectionseParsingTests
    {
        [Fact]
        public void ParseEnumerable_SimpleCollection_ReturnsNull()
        {
            var args = new string[] {"--some", "a", "b"};
            var opt = new CollectionsTestOptions<IEnumerable<string>>();

            var p = new CommandLineParser();
            p.Parse(opt, args);


            Assert.Equal("a", opt.Some.First());
            Assert.Equal("b", opt.Some.Skip(1).First());
        }

        [Fact]
        public void ParseArray_SimpleCollection_ReturnsNull()
        {
            var args = new string[] {"--some", "a", "b"};
            var opt = new CollectionsTestOptions<string[]>();

            var p = new CommandLineParser();
            p.Parse(opt, args);


            Assert.Equal("a", opt.Some.First());
            Assert.Equal("b", opt.Some.Skip(1).First());
        }

        [Fact]
        public void ParseList_SimpleCollection_ReturnsNull()
        {
            var args = new[] {"--some", "a", "b"};
            var opt = new CollectionsTestOptions<List<string>>();

            var p = new CommandLineParser();
            p.Parse(opt, args);


            Assert.Equal("a", opt.Some.First());
            Assert.Equal("b", opt.Some.Skip(1).First());

        }

        [Fact]
        public void ParseHashSet_SimpleCollection_ReturnsNull()
        {
            var args = new string[] { "--some", "a", "b" };
            var opt = new CollectionsTestOptions<HashSet<string>>();

            var p = new CommandLineParser();
            p.Parse(opt, args);


            Assert.Equal("a", opt.Some.First());
            Assert.Equal("b", opt.Some.Skip(1).First());

        }
    }
}