using NCmdArgs.Tests.Options;
using System.IO;
using System.Linq;
using Xunit;

namespace NCmdArgs.Tests
{
    public class VerbParsing
    {
        [Fact]
        public void Parse_MyVerb_ParsersValid()
        {
            var args = new[] { "myverb", "--hello", "test" };
            var opt = new VerbOptions();

            var p = new CommandLineParser();
            p.Parse(opt, args);


            Assert.Equal("test", opt.MyVerb.Hello);
        }

        [Fact]
        public void Parse_MyVerb_LastVerbIsValid()
        {
            var args = new[] { "myverb", "--hello", "test" };
            var opt = new VerbOptions();

            var p = new CommandLineParser();

            p.Parse(opt, args);

            Assert.Same(opt.MyVerb, p.LastVerb);
            Assert.Equal("myverb", p.VerbPath);
        }

        [Fact]
        public void Parse_MyVerb_PropertVerCallbackIsCalled()
        {
            var args = new[] { "myverb", "--hello", "test" };
            var opt = new VerbOptions();

            var called = false;

            var p = new CommandLineParser();
            p.Configuration.WhenVerb<VerbCommand>(command => called = true);

            p.Parse(opt, args);

            Assert.True(called);
        }

        [Fact]
        public void TryPredictVerbs_Sample_ValidPath()
        {
            var args = new[] { "myverb", "--hello", "test" };

            var p = new CommandLineParser();

            string verbPath;
            string[] leftArgs;

            p.TryPredictVerbs(args, out verbPath, out leftArgs);

            Assert.Equal("myverb", verbPath);
            Assert.Equal("--hello", leftArgs.First());
        }

        [Fact]
        public void Usage_Verb_PrintsHelp()
        {
            string result;

            var p = new CommandLineParser();
            using (var w = new StringWriter())
            {
                p.Usage<VerbOptions>(w);

                result = w.ToString();
            }

            Assert.True(result.Contains("--Hello"));
        }

    }
}
