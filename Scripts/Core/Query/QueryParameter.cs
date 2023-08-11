using System;
using System.Collections.Generic;
using System.Data;

namespace Core.Query
{
    public class QueryParameter
    {
        public string Name;
        public SqlDbType Type;
        public ushort Size;
        public object Value;
        public ParameterDirection Direction;
    }
}