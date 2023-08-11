using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace Core.Query
{
    public class QueryParametersBuilder
    {
        private Dictionary<string, QueryParameter> _parameters = new Dictionary<string, QueryParameter>();

        public int Count => _parameters.Count;
        public string Parameters
        {
            get
            {
                if (Count == 0) return string.Empty;
                return string.Join(",", _parameters.Select(x => x.Value.Name));
            }
        }            

        public void Add(QueryParameter parameter)
        {
            if (_parameters.ContainsKey(parameter.Name)) return;
            _parameters.Add(parameter.Name, parameter);
        }
        public void Construct(SqlCommand command)
        { 
            foreach (KeyValuePair<string, QueryParameter> item in _parameters)
            {
                var parameter = item.Value;
                bool sizable = parameter.Size > 0;
                if (sizable) command.Parameters.Add(parameter.Name, parameter.Type, parameter.Size);
                else command.Parameters.Add(parameter.Name, parameter.Type);

                command.Parameters[parameter.Name].Direction = parameter.Direction;
                if (parameter.Value != null) command.Parameters[parameter.Name].Value = parameter.Value;
            }
        }
        public void RetrieveOutput(SqlCommand command)
        {
            foreach (var item in _parameters)
            {
                if (item.Value.Direction != ParameterDirection.Output) continue;
                item.Value.Value = command.Parameters[item.Key].Value;
            }
        }

        public QueryParameter this[int index] => _parameters.Values.ToArray()[index];
        public QueryParameter this[string parameterName] => _parameters[parameterName];
    }
}