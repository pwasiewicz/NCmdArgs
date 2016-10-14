using System;
using NCmdArgs.Exceptions;

namespace NCmdArgs.Helpers.Types.Handlers.Helpers
{
    internal class NullableTryParseFactory
    {
        public static ParsableBaseHandler<T?>.ParseDelegate Produce<T>(
            ParsableBaseHandler<T>.ParseDelegate original)
            where T : struct
        {
            return (string input, out T? value) =>
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    value = new T?();
                    return true;
                }

                try
                {
                    T result;
                    var pr = original(input, out result);

                    value = result;
                    return pr;
                }
                catch (Exception ex)
                {
                    throw new ParsingException(ex);
                }
            };
        }
    }
}
