using System.IO;
using NCmdArgs.Exceptions;
using NCmdArgs.Tests.Options;
using Xunit;

namespace NCmdArgs.Tests
{
    public class ParsingTests
    {
        [Fact]
        public void Parse_SimpleStringNoRequiredValueSupplied_ReturnsValueFromCommandLine()
        {
            var args = new[] {   "--hello", "sampleval" };
            var opt = new SimpleStringOptions();

            var p = new CommandLineParser();
            p.Parse(opt, args);

            Assert.Equal("sampleval", opt.Hello);
        }

        [Fact]
        public void Parse_SimpleStringRequiredValueSupplied_ReturnsValueFromCommandLine()
        {
            var args = new[] { "--hello", "sampleval" };
            var opt = new RequriedSImpleStringOptions();

            var p = new CommandLineParser();
            p.Parse(opt, args);

            Assert.Equal("sampleval", opt.Hello);
        }

        [Fact]
        public void Parse_SimpleStringNoRequiredValueNotSupplied_ReturnsNull()
        {
            var args = new string[0];
            var opt = new SimpleStringOptions();

            var p = new CommandLineParser();
            p.Parse(opt, args);

            Assert.Null(opt.Hello);
        }

        [Fact]
        public void Parse_SimpleStringRequiredValueNotSupplied_ReturnsNull()
        {
            var args = new string[0];
            var opt = new RequriedSImpleStringOptions();

            var p = new CommandLineParser();

            Assert.Throws<MissingArgumentException>(() => p.Parse(opt, args));
        }

        [Fact]
        public void Usage_SimpleStringRequiredValueNotSupplied_PrintsHelp()
        {
            string result;

            var p = new CommandLineParser();
            using (var w = new StringWriter())
            {
                p.Usage<RequriedSImpleStringOptions>(w);

                result = w.ToString();
            }

            Assert.True(result.Contains("--Hello"));
        }

        [Fact]
        public void Parse_Position_ValidResult()
        {
            var args = new[] { "sampleval", "sampleval2" };
            var opt = new SimpleStringOptions();

            var p = new CommandLineParser();
            p.Parse(opt, args);

            Assert.Equal("sampleval", opt.Hello);
            Assert.Equal("sampleval2", opt.HelloSecond);
        }
    }
}
