using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;


namespace WindowsFormsApp1.Model
{
    public class Entities
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Properties { get; set; }
        public byte Type { get; set; }
    }
}
