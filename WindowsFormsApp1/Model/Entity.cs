using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;


namespace WindowsFormsApp1.Model
{
    public class Entity
    {
        public Entity(string value, string properties, EntitiesType type)
        {
            this.Value = value;
            this.Properties = properties;
            this.Type = type;
        }

        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Properties { get; set; }
        public EntitiesType Type { get; set; }
    }
}
