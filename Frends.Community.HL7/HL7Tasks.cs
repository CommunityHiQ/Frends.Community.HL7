using HL7.Dotnetcore;
using System;
using System.Linq;

namespace Frends.Community.HL7
{
    public static class HL7Tasks
    {
        public class ParseHL7MessageParameters
        {
            public string MessageString { get; set; }
        }

        public class ParseHL7MessageOutput
        {
            public HL7Message Message { get; set; }
        }

        public class HL7Message : Message
        {
            public HL7Message()
            {
            }

            public HL7Message(string strMessage) : base(strMessage)
            {
            }

            public HL7Segment[] MessageSegments
            {
                get
                {
                    return Segments().Select(o => new HL7Segment(o)).ToArray();
                }
            }
        }

        public class HL7Field
        {
            private readonly Field field;

            public HL7Field(Field field)
            {
                this.field = field;
            }

            public string Value { get => field.Value; }

            public bool IsComponentized { get => field.IsComponentized; }

            public string[] Components { get => field.Components().Select(o => o.Value).ToArray(); }
        }

        public class HL7Segment
        {
            private readonly Segment segment;

            public HL7Segment(Segment segment)
            {
                this.segment = segment;
            }

            public HL7Field[] Fields { get => segment.GetAllFields().Select(o => new HL7Field(o)).ToArray(); }

            public string Name { get => segment.Name; }

            public string StringValue { get => segment.Value; }
        }

        public static ParseHL7MessageOutput ParseHL7Message(ParseHL7MessageParameters parameters)
        {
            var message = new HL7Message(parameters.MessageString);
            message.ParseMessage();
            return new ParseHL7MessageOutput { Message = message };
        }
    }
}
