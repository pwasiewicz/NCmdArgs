using NCmdArgs.Helpers.Types;
using System;
using System.Collections.Generic;
using System.IO;

namespace NCmdArgs
{
    public class ParserConfiguration
    {
        public delegate void OnVerbDelegate(object verbOptions, string name);

        private string longSwitch;
        private string shortSwitch;

        private readonly TypeHandlersCollection typeHandlers;
        private Func<Type, object> instanceFactory;

        private LinkedList<OnVerbDelegate> onVerbCallbacks;
        private Dictionary<Type, LinkedList<VerbCallback>> concreteVerbCallbacks;

        public ParserConfiguration()
        {
            this.LongSwitch = "--";
            this.shortSwitch = "-";
            this.InstanceFactory = Activator.CreateInstance;

            this.typeHandlers = new TypeHandlersCollection();
            this.onVerbCallbacks = new LinkedList<OnVerbDelegate>();
            this.concreteVerbCallbacks = new Dictionary<Type, LinkedList<VerbCallback>>();

            this.ErrorOutput = null;
        }

        internal TypeHandlersCollection TypeHandler => this.typeHandlers;

        public TextWriter ErrorOutput { get; set; }

        public Func<Type, object> InstanceFactory
        {
            get { return this.instanceFactory; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                this.instanceFactory = value;
            }
        }

        public string CommandCamelCaseNameSeparator { get; set; }

        public string ShortSwitch
        {
            get { return this.shortSwitch; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Value cannot be null or empty", nameof(value));

                if (!this.ValidateSwitches())
                    throw new ArgumentException("Long switch cannot be parf of short switch");

                this.shortSwitch = value;
            }
        }

        public string LongSwitch
        {
            get { return this.longSwitch; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Value cannot be null or empty", nameof(value));

                if (!this.ValidateSwitches())
                    throw new ArgumentException("Long switch cannot be parf of short switch");

                this.longSwitch = value;
            }
        }

        public ParserConfiguration OnVerb(OnVerbDelegate callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));

            this.onVerbCallbacks.AddLast(callback);

            return this;
        }

        public ParserConfiguration WhenVerb<T>(Action<T> callback)
        {
            if (!this.concreteVerbCallbacks.ContainsKey(typeof(T)))
            {
                this.concreteVerbCallbacks.Add(typeof(T), new LinkedList<VerbCallback>());
            }

            this.concreteVerbCallbacks[typeof(T)].AddLast(new VerbCallback<T>(callback));

            return this;
        }


        public ParserConfiguration RegisterTypeHandler(TypeHandler typeHandler)
        {
            if (typeHandler == null) throw new ArgumentNullException(nameof(typeHandler));
            this.typeHandlers.RegisterHandler(typeHandler);

            return this;
        }

        internal void VerbParsed(object verb)
        {
            if (verb == null) throw new ArgumentNullException(nameof(verb));

            var verbType = verb.GetType();
            if (!this.concreteVerbCallbacks.ContainsKey(verbType)) return;

            foreach (var callback in this.concreteVerbCallbacks[verbType])
            {
                callback.Call(verb);
            }
        }

        internal bool ValidateSwitches()
        {
            return this.ShortSwitch == null || !this.ShortSwitch.Contains(this.LongSwitch);
        }

        internal bool IsArgSwitch(string arg)
        {
            if (arg == null) throw new ArgumentNullException(nameof(arg));

            return arg.StartsWith(this.ShortSwitch) || arg.StartsWith(this.LongSwitch);
        }

        internal string GetArgFromSwitch(string arg)
        {
            if (arg == null) throw new ArgumentNullException(nameof(arg));

            if (arg.StartsWith(this.LongSwitch))
            {
                return arg.Substring(this.LongSwitch.Length);
            }

            if (arg.StartsWith(this.ShortSwitch))
            {
                return arg.Substring(this.ShortSwitch.Length);
            }

            throw new InvalidOperationException("Invalid switch.");
        }

        private abstract class VerbCallback
        {
            public abstract void Call(object value);
        }

        private class VerbCallback<T> : VerbCallback
        {
            private readonly Action<T> callback;

            public VerbCallback(Action<T> callback)
            {
                this.callback = callback;
            }

            public override void Call(object value)
            {
                this.callback((T)value);
            }
        }
    }
}
