using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumCore.Models
{
    class SegmentsResponseObject
    {

        public HttpResponseMessage httpResponseMessage { get; set; }
        public List<SewerPipe> sewer_pipes { get; set; }
        public List<Filme> filme { get; set; }
        public string format { get; set; }
        public string codeSystem { get; set; }
        public string language { get; set; }
    }
}
