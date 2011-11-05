using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Xml.Linq;
using System.Dynamic;

namespace Veggerby.Utility.Mvc.Security
{
    public class UserState : DynamicObject
    {
        public UserState()
        {
            this._Fields = new Dictionary<string, string>();
        }

        private readonly Dictionary<string, string> _Fields;

        public string UserId
        {
            get;
            set;
        }

        public string Identifier
        {
            get;
            set;
        }

        public string this[string field]
        {
            get { return this._Fields.ContainsKey(field) ? this._Fields[field] : null; }
            set { this._Fields[field] = value; }
        }

        public override string ToString()
        {
            if (this.UserId == null)
            {
                throw new SecurityException("Cannot serialize user state without valid user id");
            }

            return new XElement(
                "UserState",
                new XAttribute("UserId", this.UserId),
                new XElement("Identifier", this.Identifier),
                from field in this._Fields.Keys
                select new XElement("Field",
                    new XAttribute("Name", field),
                    new XAttribute("Value", this._Fields[field] ?? string.Empty))
                ).ToString();
        }

        public static bool TryParse(string item, out UserState state)
        {
            var e = XElement.Parse(item);
            var userId = e.Attributes("UserId").Select(a => a.Value).FirstOrDefault();
            var identifier = e.Elements("Identifier").Select(x => x.Value).FirstOrDefault();

            if (!string.IsNullOrEmpty(userId))
            {
                state = new UserState { UserId = userId, Identifier = identifier };

                foreach (var fieldElement in e.Elements("Field"))
                {
                    var fieldAttribute = fieldElement.Attribute("Name");
                    var valueAttribute = fieldElement.Attribute("Value");
                    if ((fieldAttribute != null) && (valueAttribute != null))
                    {
                        var field = fieldAttribute.Value;
                        var value = valueAttribute.Value;
                        state[field] = value;
                    }
                }

                return true;
            }

            state = null;
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if ((value is string) || (value == null))
            {
                this[binder.Name] = value as string;
                return true;
            }

            return false;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this[binder.Name];
            return true;
        }
    }
}
