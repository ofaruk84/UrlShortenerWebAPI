#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete.DTOs
{
    public class UrlRequestModal : IDto
    {

        public string LongUrl { get; set; }
        public string? CustomName { get; set; }
        public DateTime DateCreated => DateTime.Now;
    }
}
