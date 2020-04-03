using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumCore.Models.Api.Entities
{
    class ApiProjectModel
    {
        public string name { get; set; } = "api project " + Guid.NewGuid();

        public int status { get; set; } = 0;

        public int leadId { get; set; } = 3;

        public Timeline timeline { get; set; } = new Timeline();

        public string description { get; set; } = "this project was created through an api call";
    }
}
